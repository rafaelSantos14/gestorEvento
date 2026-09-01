using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Models.Exceptions;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    public class VendaService
    {
        private readonly VendaRepository _repository;
        private readonly RecebimentoRepository _recebimentoRepository;
        private readonly MovimentacaoRepository _movimentacaoRepository;
        private readonly DoacaoVendaRepository _doacaoVendaRepository;
        private readonly PontoVendaRepository _pontoVendaRepository;
        private readonly EventoRepository _eventoRepository;
        private readonly ProdutoEventoRepository _produtoEventoRepository;
        private readonly InscricaoEventoRepository _inscricaoEventoRepository;

        public VendaService()
        {
            _repository = new VendaRepository();
            _recebimentoRepository = new RecebimentoRepository();
            _movimentacaoRepository = new MovimentacaoRepository();
            _doacaoVendaRepository = new DoacaoVendaRepository();
            _pontoVendaRepository = new PontoVendaRepository();
            _eventoRepository = new EventoRepository();
            _produtoEventoRepository = new ProdutoEventoRepository();
            _inscricaoEventoRepository = new InscricaoEventoRepository();
        }

        /// <summary>
        /// Registra uma venda com seus itens
        /// </summary>
        public int RegistrarVenda(Venda venda)
        {
            if (venda == null)
            {
                UiHelper.ExibirAviso("Aviso", "Venda nÃ£o pode ser nula");
                return 0;
            }

            if (venda.IdPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda invÃ¡lido");
                return 0;
            }

            if (venda.Itens == null || venda.Itens.Count == 0)
            {
                UiHelper.ExibirAviso("Aviso", "Venda deve ter pelo menos um item");
                return 0;
            }

            // ValidaÃ§Ã£o de valor total: VENDA exige > 0, CORTESIA permite = 0
            string tipoOperacao = venda.TipoOperacao ?? "VENDA";
            if (tipoOperacao == "VENDA" && venda.VlTotal <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor total da venda deve ser maior que zero");
                return 0;
            }
            
            if (tipoOperacao == "CORTESIA" && venda.VlTotal < 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor total nÃ£o pode ser negativo");
                return 0;
            }

            // Validar cada item
            foreach (var item in venda.Itens)
            {
                if (item.IdProdutoEvento <= 0)
                {
                    UiHelper.ExibirAviso("Aviso", "ID do produto-evento invÃ¡lido no item");
                    return 0;
                }

                if (string.IsNullOrWhiteSpace(item.NomeProduto))
                {
                    UiHelper.ExibirAviso("Aviso", "Nome do produto nÃ£o pode estar vazio");
                    return 0;
                }

                if (item.Quantidade <= 0)
                {
                    UiHelper.ExibirAviso("Aviso", "Quantidade deve ser maior que zero");
                    return 0;
                }

                if (item.VlUnitario < 0)
                {
                    UiHelper.ExibirAviso("Aviso", "Valor unitÃ¡rio nÃ£o pode ser negativo");
                    return 0;
                }

                if (item.Subtotal <= 0)
                {
                    UiHelper.ExibirAviso("Aviso", "Subtotal do item deve ser maior que zero");
                    return 0;
                }
            }

            try
            {
                if (!PodeRegistrarVendaNoEvento(venda.IdPontoVenda))
                {
                    return 0;
                }

                return _repository.RegistrarVenda(venda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao registrar venda: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// ObtÃ©m uma venda por ID
        /// </summary>
        public Venda GetVendaById(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID da venda invÃ¡lido");
                return null;
            }

            try
            {
                return _repository.GetVendaById(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter venda: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Lista todas as vendas de um ponto de venda
        /// </summary>
        public List<Venda> GetVendasByPontoVenda(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda invÃ¡lido");
                return new List<Venda>();
            }

            try
            {
                return _repository.GetVendasByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter vendas: {ex.Message}");
                return new List<Venda>();
            }
        }

        /// <summary>
        /// ObtÃ©m resumo simplificado de vendas para fechamento de caixa (id, data, valor)
        /// </summary>
        public List<(int idVenda, DateTime dtVenda, decimal vlTotal, string tipoOperacao)> GetResumoVendasByPontoVenda(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda invÃ¡lido");
                return new List<(int, DateTime, decimal, string)>();
            }

            try
            {
                return _repository.GetResumoVendasByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter resumo de vendas: {ex.Message}");
                return new List<(int, DateTime, decimal, string)>();
            }
        }

        /// <summary>
        /// VALIDAÇÃO COMPARTILHADA: Valida dados básicos de venda antes de registrar
        /// Lança ArgumentException ou ArgumentNullException se houver erro
        /// </summary>
        private void ValidarVendaParaRegistro(Venda venda, List<(int idFormaPagamento, decimal valor)> recebimentos)
        {
            if (venda == null)
                throw new ArgumentNullException(nameof(venda), "Venda não pode ser nula");

            if (venda.IdPontoVenda <= 0)
                throw new ArgumentException("ID do ponto de venda inválido");

            if (venda.VlTotal < 0)
                throw new ArgumentException("Valor total da venda não pode ser negativo");

            if (recebimentos == null || recebimentos.Count == 0)
            {
                // CORTESIA pode não ter recebimento; VENDA com total zerado (itens com valor permitido zerado) também não precisa
                if (venda.TipoOperacao != "CORTESIA" && venda.VlTotal > 0)
                    throw new ArgumentException("Venda deve ter pelo menos um recebimento");
            }

            if (!PodeRegistrarVendaNoEvento(venda.IdPontoVenda))
                throw new ArgumentException("Não é possível registrar venda para este caixa.");
        }

        /// <summary>
        /// Registra uma venda com recebimentos, troco E validação de estoque em uma transação atômica
        /// Usa SELECT...FOR UPDATE para lock (previne race condition)
        /// Valida e debita estoque ANTES de registrar venda (garante atomicidade)
        /// Se algo falhar, faz rollback de tudo (venda + recebimento + estoque não são alterados)
        /// </summary>
        public int RegistrarVendaComEstoqueComTransacao(Venda venda, List<(int idFormaPagamento, decimal valor)> recebimentos, decimal vlTroco, List<(int idFormaPagamento, decimal valor)> doacoes = null, int? idInscricaoEvento = null)
        {
            MySqlConnection connection = null;
            MySqlTransaction transaction = null;

            try
            {
                // 1. VALIDAÇÕES COMPARTILHADAS
                ValidarVendaParaRegistro(venda, recebimentos);

                // Abrir conexão e transação com isolation level READ_COMMITTED
                connection = new MySqlConnection(Connection.GetConnection());
                connection.Open();
                transaction = connection.BeginTransaction(System.Data.IsolationLevel.ReadCommitted);

                // 2. SE HOUVER INSCRIÇÃO ANTECIPADA VINCULADA: travar e retirar PRIMEIRO (fail-fast)
                // ⚠️ CRÍTICO: se outro terminal já retirou esta inscrição, aborta ANTES de debitar estoque
                if (idInscricaoEvento.HasValue)
                {
                    _inscricaoEventoRepository.ValidarERetirarComTransacao(connection, transaction, idInscricaoEvento.Value);
                }

                // 3. VALIDAR E DEBITAR ESTOQUE (SELECT...FOR UPDATE + UPDATE) - ATÔMICO
                // ⚠️ CRÍTICO: Fazer isso ANTES de registrar venda garante que se falhar, nada é inserido
                foreach (var item in venda.Itens)
                {
                    _produtoEventoRepository.ValidarEDebitarEstoqueComTransacao(
                        connection, transaction,
                        item.IdProdutoEvento,
                        item.Quantidade,
                        item.NomeProduto
                    );
                }

                // 4. SE ESTOQUE PASSOU: Registrar venda e itens na MESMA transação
                // ⚠️ AGORA USA: RegistrarVendaComTransacao que NÃO cria transação interna
                venda.IdInscricaoEvento = idInscricaoEvento;
                int idVenda = _repository.RegistrarVendaComTransacao(connection, transaction, venda);

                // 5. REGISTRAR RECEBIMENTOS (INSERT RECEBIMENTO) - apenas para VENDA
                if (venda.TipoOperacao == "VENDA")
                {
                    foreach (var (idFormaPagamento, valor) in recebimentos)
                    {
                        if (valor > 0)
                        {
                            var recebimento = new Recebimento
                            {
                                IdVenda = idVenda,
                                IdFormaPagamento = idFormaPagamento,
                                VlRecebimento = valor,
                                DtRecebimento = DateTime.Now
                            };

                            _recebimentoRepository.RegistrarRecebimentoComTransacao(connection, transaction, recebimento);
                        }
                    }

                    // 6. REGISTRAR TROCO (INSERT MOVIMENTACAO) - apenas para VENDA
                    if (vlTroco > 0)
                    {
                        _movimentacaoRepository.RegistrarTrocoComTransacao(connection, transaction, venda.IdPontoVenda, idVenda, vlTroco);
                    }

                    // 6.1 REGISTRAR DOAÇÕES (INSERT DOACAO_VENDA) - apenas para VENDA
                    // Independente do troco/recebimento: não entra em nenhuma validação nem desconta o troco registrado acima
                    if (doacoes != null)
                    {
                        foreach (var (idFormaPagamento, valor) in doacoes)
                        {
                            if (valor > 0)
                            {
                                var doacao = new DoacaoVenda(idVenda, idFormaPagamento, valor);
                                _doacaoVendaRepository.RegistrarDoacaoComTransacao(connection, transaction, doacao);
                            }
                        }
                    }
                }

                // 7. Se chegou aqui, tudo OK → commit
                transaction.Commit();

                return idVenda;
            }
            catch (Exception ex)
            {
                // Se algo falhou, rollback de tudo (inclui reverter a retirada da inscrição, se houver)
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                    }
                }

                throw;
            }
            finally
            {
                if (connection != null)
                {
                    try
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    catch { }
                }
            }
        }

        /// <summary>
        /// Registra uma venda com recebimentos e troco em uma transação atômica (SEM validação de estoque)
        /// Se algo falhar, faz rollback de tudo
        /// </summary>
        public int RegistrarVendaComTrocoComTransacao(Venda venda, List<(int idFormaPagamento, decimal valor)> recebimentos, decimal vlTroco)
        {
            MySqlConnection connection = null;
            MySqlTransaction transaction = null;

            try
            {
                // VALIDAÇÕES COMPARTILHADAS
                ValidarVendaParaRegistro(venda, recebimentos);

                // Abrir conexÃ£o e transaÃ§Ã£o
                connection = new MySqlConnection(Connection.GetConnection());
                connection.Open();
                transaction = connection.BeginTransaction();

                // 1. REGISTRAR VENDA E ITENS (usa transaÃ§Ã£o internamente via RegistrarVenda)
                int idVenda = RegistrarVenda(venda);

                // 2. REGISTRAR RECEBIMENTOS em transaÃ§Ã£o (apenas para VENDA)
                if (venda.TipoOperacao == "VENDA")
                {
                    foreach (var (idFormaPagamento, valor) in recebimentos)
                    {
                        if (valor > 0)
                        {
                            var recebimento = new Recebimento
                            {
                                IdVenda = idVenda,
                                IdFormaPagamento = idFormaPagamento,
                                VlRecebimento = valor,
                                DtRecebimento = DateTime.Now
                            };

                            _recebimentoRepository.RegistrarRecebimentoComTransacao(connection, transaction, recebimento);
                        }
                    }

                    // 3. REGISTRAR TROCO em transaÃ§Ã£o (se houver, apenas para VENDA)
                    if (vlTroco > 0)
                    {
                        _movimentacaoRepository.RegistrarTrocoComTransacao(connection, transaction, venda.IdPontoVenda, idVenda, vlTroco);
                    }
                }
                // Para CORTESIA, nÃ£o registra recebimento nem troco

                // Se chegou aqui, tudo OK â†’ commit
                transaction.Commit();

                return idVenda;
            }
            catch (Exception ex)
            {
                // Se algo falhou, rollback de tudo
                if (transaction != null)
                {
                    try
                    {
                        transaction.Rollback();
                    }
                    catch (Exception exRollback)
                    {
                    }
                }

                throw new Exception($"Erro ao registrar venda com troco (transaÃ§Ã£o revertida): {ex.Message}", ex);
            }
            finally
            {
                if (connection != null)
                {
                    try
                    {
                        connection.Close();
                        connection.Dispose();
                    }
                    catch { }
                }
            }
        }

        private bool PodeRegistrarVendaNoEvento(int idPontoVenda)
        {
            var pontoVenda = _pontoVendaRepository.GetPontoVendaById(idPontoVenda);
            if (pontoVenda == null)
            {
                UiHelper.ExibirAviso("Aviso", "Ponto de venda nÃ£o encontrado.");
                return false;
            }

            if (!string.Equals(pontoVenda.CdStatus, "Aberto", StringComparison.OrdinalIgnoreCase))
            {
                UiHelper.ExibirAviso("Aviso", "Caixa fechado. NÃ£o Ã© possÃ­vel registrar venda.");
                return false;
            }

            var evento = _eventoRepository.GetEventoById(pontoVenda.IdEvento);
            if (evento == null)
            {
                UiHelper.ExibirAviso("Aviso", "Evento nÃ£o encontrado.");
                return false;
            }

            if (evento.IsEncerrado)
            {
                UiHelper.ExibirAviso("Aviso", "Evento encerrado. NÃ£o Ã© possÃ­vel registrar venda.");
                return false;
            }

            return true;
        }
    }
}

