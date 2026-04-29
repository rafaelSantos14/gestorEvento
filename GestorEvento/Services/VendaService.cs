using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

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
            if (venda == null)
            {
                UiHelper.ExibirAviso("Aviso", "Venda não pode ser nula");
                return 0;
            }

            if (venda.IdPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return 0;
            }

            if (venda.Itens == null || venda.Itens.Count == 0)
            {
                UiHelper.ExibirAviso("Aviso", "Venda deve ter pelo menos um item");
                return 0;
            }

            if (venda.VlTotal <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor total da venda deve ser maior que zero");
                return 0;
            }

            // Validar cada item
            foreach (var item in venda.Itens)
            {
                if (item.IdProdutoEvento <= 0)
                {
                    UiHelper.ExibirAviso("Aviso", "ID do produto-evento inválido no item");
                    return 0;
                }

                if (string.IsNullOrWhiteSpace(item.NomeProduto))
                {
                    UiHelper.ExibirAviso("Aviso", "Nome do produto não pode estar vazio");
                    return 0;
                }

                if (item.Quantidade <= 0)
                {
                    UiHelper.ExibirAviso("Aviso", "Quantidade deve ser maior que zero");
                    return 0;
                }

                if (item.VlUnitario < 0)
                {
                    UiHelper.ExibirAviso("Aviso", "Valor unitário não pode ser negativo");
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
                return _repository.RegistrarVenda(venda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao registrar venda: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Obtém uma venda por ID
        /// </summary>
        public Venda GetVendaById(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID da venda inválido");
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
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
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
        /// Obtém resumo simplificado de vendas para fechamento de caixa (id, data, valor)
        /// </summary>
        public List<(int idVenda, DateTime dtVenda, decimal vlTotal)> GetResumoVendasByPontoVenda(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return new List<(int, DateTime, decimal)>();
            }

            try
            {
                return _repository.GetResumoVendasByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter resumo de vendas: {ex.Message}");
                return new List<(int, DateTime, decimal)>();
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
                    }
                    catch (Exception exRollback)
                    {
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
