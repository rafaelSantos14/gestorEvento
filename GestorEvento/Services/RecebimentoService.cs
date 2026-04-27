using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;

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
            if (vlRecebimento <= 0)
            {
                throw new ArgumentException("O valor do recebimento deve ser maior que zero");
            }

            var recebimento = new Recebimento(idVenda, idFormaPagamento, vlRecebimento);
            return _repository.RegistrarRecebimento(recebimento);
        }

        /// <summary>
        /// Obtém um recebimento por ID
        /// </summary>
        public Recebimento GetRecebimentoById(int id)
        {
            return _repository.GetRecebimentoById(id);
        }

        /// <summary>
        /// Obtém todos os recebimentos de uma venda
        /// </summary>
        public List<Recebimento> GetRecebimentosByVendaId(int idVenda)
        {
            return _repository.GetRecebimentosByVendaId(idVenda);
        }

        /// <summary>
        /// Obtém resumo de recebimentos agrupados por forma de pagamento para um ponto de venda
        /// </summary>
        public List<(int idFormaPagamento, string nomeFormaPagamento, decimal totalRecebimento)> GetResumoRecebimentosByPontoVenda(int idPontoVenda)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                return _repository.GetResumoRecebimentosByPontoVenda(idPontoVenda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter resumo de recebimentos: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém o total de recebimentos em dinheiro para um ponto de venda
        /// </summary>
        public decimal GetTotalRecebimentoDinheiro(int idPontoVenda)
        {
            try
            {
                if (idPontoVenda <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                return _repository.GetTotalRecebimentoDinheiro(idPontoVenda);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter total dinheiro: {ex.Message}", ex);
            }
        }
    }
}
