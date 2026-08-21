using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    public class DoacaoVendaService
    {
        private readonly DoacaoVendaRepository _repository;

        public DoacaoVendaService()
        {
            _repository = new DoacaoVendaRepository();
        }

        /// <summary>
        /// Obtém resumo de doações agrupadas por forma de pagamento para um ponto de venda
        /// </summary>
        public List<(int idFormaPagamento, string nomeFormaPagamento, decimal totalDoacao)> GetResumoDoacoesByPontoVenda(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return new List<(int, string, decimal)>();
            }

            try
            {
                return _repository.GetResumoDoacoesByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter resumo de doações: {ex.Message}");
                return new List<(int, string, decimal)>();
            }
        }

        /// <summary>
        /// Obtém todas as doações de uma venda
        /// </summary>
        public List<DoacaoVenda> GetDoacoesByVendaId(int idVenda)
        {
            if (idVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID da venda inválido");
                return new List<DoacaoVenda>();
            }

            try
            {
                return _repository.GetDoacoesByVendaId(idVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter doações da venda: {ex.Message}");
                return new List<DoacaoVenda>();
            }
        }

        /// <summary>
        /// Obtém o total de doações em dinheiro para um ponto de venda
        /// </summary>
        public decimal GetTotalDoacaoDinheiro(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return 0;
            }

            try
            {
                return _repository.GetTotalDoacaoDinheiro(idPontoVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter total de doação em dinheiro: {ex.Message}");
                return 0;
            }
        }
    }
}
