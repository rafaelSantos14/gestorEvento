using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    public class RecebimentoService
    {
        private readonly RecebimentoRepository _repository;

        public RecebimentoService()
        {
            _repository = new RecebimentoRepository();
        }

        /// <summary>
        /// Registra um recebimento (pagamento) de uma venda
        /// Valida se o valor é positivo antes de registrar
        /// </summary>
        public int RegistrarRecebimento(int idVenda, int idFormaPagamento, decimal vlRecebimento)
        {
            if (idVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID da venda inválido");
                return 0;
            }

            if (idFormaPagamento <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID da forma de pagamento inválido");
                return 0;
            }

            if (vlRecebimento <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "O valor do recebimento deve ser maior que zero");
                return 0;
            }

            try
            {
                var recebimento = new Recebimento(idVenda, idFormaPagamento, vlRecebimento);
                return _repository.RegistrarRecebimento(recebimento);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao registrar recebimento: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Obtém um recebimento por ID
        /// </summary>
        public Recebimento GetRecebimentoById(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do recebimento inválido");
                return null;
            }

            try
            {
                return _repository.GetRecebimentoById(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter recebimento: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtém todos os recebimentos de uma venda
        /// </summary>
        public List<Recebimento> GetRecebimentosByVendaId(int idVenda)
        {
            if (idVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID da venda inválido");
                return new List<Recebimento>();
            }

            try
            {
                return _repository.GetRecebimentosByVendaId(idVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter recebimentos da venda: {ex.Message}");
                return new List<Recebimento>();
            }
        }

        /// <summary>
        /// Obtém resumo de recebimentos agrupados por forma de pagamento para um ponto de venda
        /// </summary>
        public List<(int idFormaPagamento, string nomeFormaPagamento, decimal totalRecebimento)> GetResumoRecebimentosByPontoVenda(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return new List<(int, string, decimal)>();
            }

            try
            {
                return _repository.GetResumoRecebimentosByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter resumo de recebimentos: {ex.Message}");
                return new List<(int, string, decimal)>();
            }
        }

        /// <summary>
        /// Obtém o total de recebimentos em dinheiro para um ponto de venda
        /// </summary>
        public decimal GetTotalRecebimentoDinheiro(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return 0;
            }

            try
            {
                return _repository.GetTotalRecebimentoDinheiro(idPontoVenda);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter total dinheiro: {ex.Message}");
                return 0;
            }
        }
    }
}
