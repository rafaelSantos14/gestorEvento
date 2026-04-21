using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;

namespace GestorEvento.Services
{
    public class ProdutoEventoService
    {
        private readonly ProdutoEventoRepository _repository;

        public ProdutoEventoService()
        {
            _repository = new ProdutoEventoRepository();
        }

        /// <summary>
        /// Obtém todos os produtos vinculados a um evento com seus dados (preço, quantidade)
        /// </summary>
        public List<ProdutoEvento> GetProdutosVinculados(int eventoId)
        {
            try
            {
                return _repository.GetProdutosVinculadosByEvento(eventoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter produtos vinculados do evento: {ex.Message}");
            }
        }

        /// <summary>
        /// Obtém todos os IDs de produtos vinculados a um evento
        /// </summary>
        public List<int> GetProdutosByEvento(int eventoId)
        {
            try
            {
                return _repository.GetProdutosByEvento(eventoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter produtos do evento: {ex.Message}");
            }
        }

        /// <summary>
        /// Vincula um produto a um evento com preço e quantidade
        /// </summary>
        public bool VincularProduto(int produtoId, int eventoId, decimal preco, int quantidade)
        {
            try
            {
                if (preco <= 0)
                    throw new Exception("Preço deve ser maior que zero");
                if (quantidade <= 0)
                    throw new Exception("Quantidade deve ser maior que zero");

                return _repository.CreateVinculacao(produtoId, eventoId, preco, quantidade);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao vincular produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Desvincula um produto de um evento
        /// </summary>
        public bool DesvincularProduto(int produtoId, int eventoId)
        {
            try
            {
                return _repository.DeleteVinculacao(produtoId, eventoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao desvincular produto: {ex.Message}");
            }
        }
    }
}
