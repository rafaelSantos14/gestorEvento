using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;

namespace GestorEvento.Services
{
    public class VendaService
    {
        private readonly VendaRepository _repository;

        public VendaService()
        {
            _repository = new VendaRepository();
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
    }
}
