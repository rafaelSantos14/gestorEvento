using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

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
                UiHelper.ExibirErro("Erro", $"Erro ao obter formas de pagamento: {ex.Message}");
                return new List<FormaPagamento>();
            }
        }

        /// <summary>
        /// Obtém uma forma de pagamento por ID
        /// </summary>
        public FormaPagamento GetFormaPagamentoById(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID da forma de pagamento inválido");
                return null;
            }

            try
            {
                return _repository.GetFormaPagamentoById(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter forma de pagamento: {ex.Message}");
                return null;
            }
        }
    }
}
