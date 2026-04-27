using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;

namespace GestorEvento.Services
{
    public class MovimentacaoService
    {
        private readonly MovimentacaoRepository _repository;

        public MovimentacaoService()
        {
            _repository = new MovimentacaoRepository();
        }

        /// <summary>
        /// Registra uma movimentação genérica
        /// </summary>
        public int RegistrarMovimentacao(Movimentacao movimentacao)
        {
            try
            {
                if (movimentacao.IdPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                if (movimentacao.VlMovimento <= 0)
                    throw new ArgumentException("Valor da movimentação deve ser maior que zero");

                return _repository.RegistrarMovimentacao(movimentacao);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar movimentação: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra um troco automaticamente
        /// </summary>
        public int RegistrarTroco(int idPontoVenda, int idVenda, decimal vlTroco)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                if (idVenda <= 0)
                    throw new ArgumentException("ID da venda inválido");

                if (vlTroco <= 0)
                    throw new ArgumentException("Valor do troco deve ser maior que zero");

                return _repository.RegistrarTroco(idPontoVenda, idVenda, vlTroco);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar troco: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra uma sangria manual
        /// </summary>
        public int RegistrarSangria(int idPontoVenda, decimal vlSangria, string descricao = null)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                if (vlSangria <= 0)
                    throw new ArgumentException("Valor da sangria deve ser maior que zero");

                return _repository.RegistrarSangria(idPontoVenda, vlSangria, descricao);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar sangria: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra uma entrada de troco manual
        /// </summary>
        public int RegistrarEntradaTroco(int idPontoVenda, decimal vlEntrada, string descricao = null)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                if (vlEntrada <= 0)
                    throw new ArgumentException("Valor da entrada deve ser maior que zero");

                return _repository.RegistrarEntradaTroco(idPontoVenda, vlEntrada, descricao);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar entrada de troco: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém todas as movimentações de um ponto de venda
        /// </summary>
        public List<Movimentacao> GetMovimentacoesByPontoVenda(int idPontoVenda)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                return _repository.GetMovimentacoesByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter movimentações: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém o total de movimentações de um tipo específico
        /// </summary>
        public decimal GetTotalMovimentacaoPorTipo(int idPontoVenda, TipoMovimento tipo)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                return _repository.GetTotalMovimentacaoPorTipo(idPontoVenda, tipo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter total de movimentação: {ex.Message}", ex);
            }
        }
    }
}
