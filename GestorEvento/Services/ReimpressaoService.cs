using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;

namespace GestorEvento.Services
{
    public class ReimpressaoService
    {
        private ReimpressaoRepository _repository;

        public ReimpressaoService()
        {
            _repository = new ReimpressaoRepository();
        }

        /// <summary>
        /// Registra uma nova reimpressão com validações
        /// </summary>
        public int RegistrarReimpressao(Reimpressao reimpressao, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            try
            {
                // 1. Validações
                if (reimpressao == null)
                    throw new Exception("Reimpressão não pode ser nula");

                if (reimpressao.Itens == null || reimpressao.Itens.Count == 0)
                    throw new Exception("Reimpressão deve conter pelo menos um item");

                if (reimpressao.IdMotivo <= 0)
                    throw new Exception("Motivo de reimpressão deve ser selecionado");

                // 2. Calcula total
                decimal totalCalculado = 0;
                foreach (var item in reimpressao.Itens)
                {
                    totalCalculado += item.VlSubtotal;
                }
                reimpressao.VlTotal = totalCalculado;

                // 3. Registra no banco de dados
                int idReimpressao = _repository.RegistrarReimpressao(reimpressao);

                // 4. Recupera dados da reimpressão para impressão
                Reimpressao reimpressaoRegistrada = null;
                try
                {
                    reimpressaoRegistrada = GetReimpressaoById(idReimpressao);
                }
                catch (Exception exDb)
                {
                    // Erro ao recuperar do banco - falha a operação
                    throw new Exception($"Erro ao recuperar dados da reimpressão para impressão: {exDb.Message}", exDb);
                }

                // 5. Tenta imprimir (SEM debitar estoque)
                try
                {
                    // Preparar lista de itens EXATAMENTE como em vendas
                    // Cada item é adicionado separadamente pela sua quantidade
                    var itensParaImprimir = new List<string>();
                    foreach (var item in reimpressaoRegistrada.Itens)
                    {
                        string descricao = !string.IsNullOrEmpty(item.DescricaoProduto) 
                            ? item.DescricaoProduto 
                            : $"Produto #{item.IdProdutoEvento}";
                        
                        // Adiciona cada item qtde vezes (como em vendas)
                        for (int i = 0; i < item.QtdeReimpressao; i++)
                        {
                            itensParaImprimir.Add(descricao);
                        }
                    }
                    
                    PrinterServiceFactory.ImprimirReimpressao(idReimpressao, itensParaImprimir, numeroCaixa, descricaoCaixa);
                }
                catch (Exception exPrint)
                {
                    // Log do erro de impressão, mas não falha a reimpressão
                    System.Diagnostics.Debug.WriteLine($"Aviso: Erro ao imprimir reimpressão {idReimpressao}: {exPrint.Message}");
                }

                return idReimpressao;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar reimpressão: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém uma reimpressão específica pelo ID
        /// </summary>
        public Reimpressao GetReimpressaoById(int idReimpressao)
        {
            try
            {
                return _repository.GetReimpressaoById(idReimpressao);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter reimpressão: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém todas as reimpressões de um evento específico
        /// </summary>
        public List<Reimpressao> GetReimpressoesPorEvento(int idEvento)
        {
            try
            {
                return _repository.GetReimpressoesPorEvento(idEvento);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter reimpressões do evento: {ex.Message}", ex);
            }
        }
    }
}
