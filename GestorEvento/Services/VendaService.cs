using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Repositories;

namespace GestorEvento.Services
{
    public class VendaService
    {
        private readonly VendaRepository _repository;
        private readonly RecebimentoRepository _recebimentoRepository;
        private readonly MovimentacaoRepository _movimentacaoRepository;

        public VendaService()
        {
            _repository = new VendaRepository();
            _recebimentoRepository = new RecebimentoRepository();
            _movimentacaoRepository = new MovimentacaoRepository();
        }

        /// <summary>
        /// Registra uma venda com seus itens
        /// </summary>
        public int RegistrarVenda(Venda venda)
        {
            try
            {
                if (venda == null)
                    throw new ArgumentNullException("Venda não pode ser nula");

                if (venda.IdPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                if (venda.Itens == null || venda.Itens.Count == 0)
                    throw new ArgumentException("Venda deve ter pelo menos um item");

                if (venda.VlTotal <= 0)
                    throw new ArgumentException("Valor total da venda deve ser maior que zero");

                // Validar cada item
                foreach (var item in venda.Itens)
                {
                    if (item.IdProduto <= 0)
                        throw new ArgumentException($"ID do produto inválido no item");

                    if (string.IsNullOrWhiteSpace(item.NomeProduto))
                        throw new ArgumentException($"Nome do produto não pode estar vazio");

                    if (item.Quantidade <= 0)
                        throw new ArgumentException($"Quantidade deve ser maior que zero");

                    if (item.VlUnitario < 0)
                        throw new ArgumentException($"Valor unitário não pode ser negativo");

                    if (item.Subtotal <= 0)
                        throw new ArgumentException($"Subtotal do item deve ser maior que zero");
                }

                return _repository.RegistrarVenda(venda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar venda: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém uma venda por ID
        /// </summary>
        public Venda GetVendaById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID da venda inválido");

                return _repository.GetVendaById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter venda: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Lista todas as vendas de um ponto de venda
        /// </summary>
        public List<Venda> GetVendasByPontoVenda(int idPontoVenda)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                return _repository.GetVendasByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter vendas: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém resumo simplificado de vendas para fechamento de caixa (id, data, valor)
        /// </summary>
        public List<(int idVenda, DateTime dtVenda, decimal vlTotal)> GetResumoVendasByPontoVenda(int idPontoVenda)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                return _repository.GetResumoVendasByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter resumo de vendas: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra uma venda com recebimentos e troco em uma transação atômica
        /// Se algo falhar, faz rollback de tudo
        /// </summary>
        public int RegistrarVendaComTrocoComTransacao(Venda venda, List<(int idFormaPagamento, decimal valor)> recebimentos, decimal vlTroco)
        {
            MySqlConnection connection = null;
            MySqlTransaction transaction = null;

            try
            {
                // Validações iniciais
                if (venda == null)
                    throw new ArgumentNullException("Venda não pode ser nula");

                if (venda.IdPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                if (venda.VlTotal <= 0)
                    throw new ArgumentException("Valor total da venda deve ser maior que zero");

                if (recebimentos == null || recebimentos.Count == 0)
                    throw new ArgumentException("Venda deve ter pelo menos um recebimento");

                // Abrir conexão e transação
                connection = new MySqlConnection(Connection.GetConnection());
                connection.Open();
                transaction = connection.BeginTransaction();

                // 1. REGISTRAR VENDA E ITENS (usa transação internamente via RegistrarVenda)
                int idVenda = RegistrarVenda(venda);

                // 2. REGISTRAR RECEBIMENTOS em transação
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

                // 3. REGISTRAR TROCO em transação (se houver)
                if (vlTroco > 0)
                {
                    _movimentacaoRepository.RegistrarTrocoComTransacao(connection, transaction, venda.IdPontoVenda, idVenda, vlTroco);
                }

                // Se chegou aqui, tudo OK → commit
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
                        System.Diagnostics.Debug.WriteLine($"[TRANSAÇÃO] Rollback executado: {ex.Message}");
                    }
                    catch (Exception exRollback)
                    {
                        System.Diagnostics.Debug.WriteLine($"Erro ao fazer rollback: {exRollback.Message}");
                    }
                }

                throw new Exception($"Erro ao registrar venda com troco (transação revertida): {ex.Message}", ex);
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
    }
}
