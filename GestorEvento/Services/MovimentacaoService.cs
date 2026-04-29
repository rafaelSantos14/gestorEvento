using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

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
            if (movimentacao.IdPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return 0;
            }

            if (movimentacao.VlMovimento <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor da movimentação deve ser maior que zero");
                return 0;
            }

            try
            {
                return _repository.RegistrarMovimentacao(movimentacao);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao registrar movimentação: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Registra um troco automaticamente
        /// </summary>
        public int RegistrarTroco(int idPontoVenda, int idVenda, decimal vlTroco)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return 0;
            }

            if (idVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID da venda inválido");
                return 0;
            }

            if (vlTroco <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor do troco deve ser maior que zero");
                return 0;
            }

            try
            {
                return _repository.RegistrarTroco(idPontoVenda, idVenda, vlTroco);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao registrar troco: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Registra uma sangria manual
        /// </summary>
        public int RegistrarSangria(int idPontoVenda, decimal vlSangria, string descricao = null)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return 0;
            }

            if (vlSangria <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor da sangria deve ser maior que zero");
                return 0;
            }

            try
            {
                return _repository.RegistrarSangria(idPontoVenda, vlSangria, descricao);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao registrar sangria: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Registra uma entrada de troco manual
        /// </summary>
        public int RegistrarEntradaTroco(int idPontoVenda, decimal vlEntrada, string descricao = null)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return 0;
            }

            if (vlEntrada <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor da entrada deve ser maior que zero");
                return 0;
            }

            try
            {
                return _repository.RegistrarEntradaTroco(idPontoVenda, vlEntrada, descricao);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao registrar entrada de troco: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Obtém todas as movimentações de um ponto de venda
        /// </summary>
        public List<Movimentacao> GetMovimentacoesByPontoVenda(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return new List<Movimentacao>();
            }

            try
            {
                return _repository.GetMovimentacoesByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter movimentações: {ex.Message}");
                return new List<Movimentacao>();
            }
        }

        /// <summary>
        /// Obtém o total de movimentações de um tipo específico
        /// </summary>
        public decimal GetTotalMovimentacaoPorTipo(int idPontoVenda, TipoMovimento tipo)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return 0;
            }

            try
            {
                return _repository.GetTotalMovimentacaoPorTipo(idPontoVenda, tipo);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter total de movimentação: {ex.Message}");
                return 0;
            }
        }
    }
}
