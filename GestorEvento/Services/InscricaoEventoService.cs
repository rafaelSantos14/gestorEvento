using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using ExcelDataReader;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    public class InscricaoEventoService
    {
        private readonly InscricaoEventoRepository _repository;

        public InscricaoEventoService()
        {
            _repository = new InscricaoEventoRepository();
        }

        
        public List<InscricaoEvento> Buscar(int idEvento, string filtroNome = null, string filtroCpf = null, string filtroEmail = null, string cdStatus = InscricaoEvento.StatusPendente)
        {
            if (idEvento <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return new List<InscricaoEvento>();
            }

            try
            {
                string cpfNormalizado = NormalizarCpfCnpj(filtroCpf);
                return _repository.Buscar(idEvento, filtroNome, string.IsNullOrEmpty(cpfNormalizado) ? null : cpfNormalizado, filtroEmail, cdStatus);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao pesquisar inscrições: {ex.Message}");
                return new List<InscricaoEvento>();
            }
        }

        
        public InscricaoEvento GetById(int id)
        {
            if (id <= 0)
                return null;

            try
            {
                return _repository.GetById(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter inscrição: {ex.Message}");
                return null;
            }
        }

        
        public PreparacaoImportacaoInscricao PrepararImportacao(int idEvento, string caminhoArquivo)
        {
            var resultado = new PreparacaoImportacaoInscricao();
            var linhasValidas = new List<(string nome, string email, string cpfCnpj, string celular, int qtde)>();

            try
            {
                using (var stream = File.Open(caminhoArquivo, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
                using (var excelReader = ExcelReaderFactory.CreateReader(stream))
                {
                    var dataSet = excelReader.AsDataSet(new ExcelDataSetConfiguration
                    {
                        ConfigureDataTable = _ => new ExcelDataTableConfiguration { UseHeaderRow = true }
                    });

                    if (dataSet.Tables.Count == 0)
                    {
                        resultado.Avisos.Add("Planilha vazia ou em formato não reconhecido.");
                        return resultado;
                    }

                    DataTable tabela = dataSet.Tables[0];

                    int colNome = EncontrarColuna(tabela, "nome");
                    int colEmail = EncontrarColuna(tabela, "email", "e-mail");
                    int colCpf = EncontrarColuna(tabela, "cpf/cnpj", "cpf", "cnpj");
                    int colCelular = EncontrarColuna(tabela, "celular", "telefone", "tel", "whatsapp");
                    int colQtde = EncontrarColuna(tabela, "qtde. almoço", "qtde almoço", "qtd. almoço", "qtde", "quantidade");

                    if (colNome < 0 || colCpf < 0 || colQtde < 0)
                    {
                        resultado.Avisos.Add("Planilha não contém as colunas esperadas (Nome, CPF/CNPJ e Qtde).");
                        return resultado;
                    }

                    int numeroLinhaPlanilha = 1; 
                    foreach (DataRow row in tabela.Rows)
                    {
                        numeroLinhaPlanilha++;
                        resultado.TotalLinhasLidas++;

                        string nome = NormalizarNome(row[colNome]?.ToString()?.Trim());
                        string email = colEmail >= 0 ? row[colEmail]?.ToString()?.Trim() : null;
                        string cpf = NormalizarCpfCnpj(row[colCpf]?.ToString());
                        string celular = colCelular >= 0 ? NormalizarCelular(row[colCelular]?.ToString()) : null;
                        string qtdeBruta = row[colQtde]?.ToString();

                        if (string.IsNullOrWhiteSpace(nome) && string.IsNullOrWhiteSpace(cpf))
                        {
                            resultado.TotalLinhasLidas--;
                            continue;
                        }

                        if (cpf.Length != 11 && cpf.Length != 14)
                        {
                            resultado.TotalIgnoradasInvalidas++;
                            resultado.Avisos.Add($"Linha {numeroLinhaPlanilha} ignorada (CPF/CNPJ inválido ou ausente): {nome}");
                            continue;
                        }

                        string qtdeDigitos = new string((qtdeBruta ?? "").Where(char.IsDigit).ToArray());
                        if (!int.TryParse(qtdeDigitos, out int qtde) || qtde <= 0)
                        {
                            resultado.TotalIgnoradasInvalidas++;
                            resultado.Avisos.Add($"Linha {numeroLinhaPlanilha} ignorada (quantidade inválida): {nome}");
                            continue;
                        }

                        linhasValidas.Add((nome, email, cpf, celular, qtde));
                    }
                }
            }
            catch (Exception ex)
            {
                resultado.Avisos.Add($"Erro ao ler o arquivo: {ex.Message}");
                return resultado;
            }

            var deduplicadas = linhasValidas
                .GroupBy(l => l.cpfCnpj)
                .Select(g => (
                    nome: g.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.nome)).nome ?? g.First().nome,
                    email: g.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.email)).email,
                    cpfCnpj: g.Key,
                    celular: g.FirstOrDefault(x => !string.IsNullOrWhiteSpace(x.celular)).celular,
                    qtde: g.Sum(x => x.qtde)
                ))
                .ToList();

            resultado.ItensParaGravar = deduplicadas;


            try
            {
                var cpfsDaPlanilha = new HashSet<string>(deduplicadas.Select(d => d.cpfCnpj));
                var pendentesAtuais = _repository.Buscar(idEvento, cdStatus: InscricaoEvento.StatusPendente);
                resultado.RegistrosSumidos = pendentesAtuais.Where(p => !cpfsDaPlanilha.Contains(p.CpfCnpj)).ToList();
            }
            catch (Exception ex)
            {
                resultado.Avisos.Add($"Erro ao verificar inscrições sumidas: {ex.Message}");
            }

            return resultado;
        }

        
        public ImportacaoInscricaoResultado ConfirmarImportacao(int idEvento, List<(string nome, string email, string cpf, string celular, int qtde)> itensParaGravar, List<int> idsParaExcluir)
        {
            var resultado = new ImportacaoInscricaoResultado();

            try
            {
                var (inseridas, atualizadas, ignoradasJaRetiradas, excluidas) = _repository.ImportarLote(idEvento, itensParaGravar, idsParaExcluir);
                resultado.TotalInseridas = inseridas;
                resultado.TotalAtualizadas = atualizadas;
                resultado.TotalIgnoradasJaRetiradas = ignoradasJaRetiradas;
                resultado.TotalExcluidas = excluidas;

                if (ignoradasJaRetiradas > 0)
                    resultado.Avisos.Add($"{ignoradasJaRetiradas} inscrição(ões) já haviam sido retiradas anteriormente e não foram sobrescritas.");
            }
            catch (Exception ex)
            {
                resultado.Avisos.Add($"Erro ao gravar inscrições: {ex.Message}");
            }

            return resultado;
        }

        private int EncontrarColuna(DataTable tabela, params string[] nomesPossiveis)
        {
            for (int i = 0; i < tabela.Columns.Count; i++)
            {
                string nomeColuna = tabela.Columns[i].ColumnName?.Trim().ToLowerInvariant();
                if (nomesPossiveis.Any(n => nomeColuna == n.ToLowerInvariant()))
                    return i;
            }
            return -1;
        }
      
        private string NormalizarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                return nome;

            string semEspacosDuplicados = string.Join(" ", nome.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));

            var culturaPtBr = new System.Globalization.CultureInfo("pt-BR");
            return culturaPtBr.TextInfo.ToTitleCase(semEspacosDuplicados.ToLower(culturaPtBr));
        }

        private string NormalizarCpfCnpj(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return "";
            return new string(valor.Where(char.IsDigit).ToArray());
        }

        private string NormalizarCelular(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                return null;

            string digitos = new string(valor.Where(char.IsDigit).ToArray());
            return digitos.Length > 0 ? digitos : null;
        }
    }
}
