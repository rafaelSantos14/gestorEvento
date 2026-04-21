using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;

namespace GestorEvento.Services
{
    public class FormaPagamentoService
    {
        private readonly FormaPagamentoRepository _repository;

        public FormaPagamentoService()
        {
            _repository = new FormaPagamentoRepository();
        }

        /// <summary>
        /// Obtém todas as formas de pagamento ativas
        /// </summary>
        public List<FormaPagamento> GetAllFormasPagamento()
        {
            try
            {
                return _repository.GetAllFormasPagamento();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter formas de pagamento: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém uma forma de pagamento por ID
        /// </summary>
        public FormaPagamento GetFormaPagamentoById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new Exception("ID da forma de pagamento inválido");

                return _repository.GetFormaPagamentoById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter forma de pagamento: {ex.Message}");
            }
        }
    }
}
