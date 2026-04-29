using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

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
            if (eventoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return new List<ProdutoEvento>();
            }

            try
            {
                return _repository.GetProdutosVinculadosByEvento(eventoId);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter produtos vinculados do evento: {ex.Message}");
                return new List<ProdutoEvento>();
            }
        }

        /// <summary>
        /// Obtém todos os IDs de produtos vinculados a um evento
        /// </summary>
        public List<int> GetProdutosByEvento(int eventoId)
        {
            if (eventoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return new List<int>();
            }

            try
            {
                return _repository.GetProdutosByEvento(eventoId);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter produtos do evento: {ex.Message}");
                return new List<int>();
            }
        }

        /// <summary>
        /// Vincula um produto a um evento com preço e quantidade
        /// </summary>
        public bool VincularProduto(int produtoId, int eventoId, decimal preco, int quantidade)
        {
            if (produtoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do produto inválido");
                return false;
            }

            if (eventoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return false;
            }

            if (preco <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Preço deve ser maior que zero");
                return false;
            }

            if (quantidade <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Quantidade deve ser maior que zero");
                return false;
            }

            try
            {
                return _repository.CreateVinculacao(produtoId, eventoId, preco, quantidade);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao vincular produto: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Remove um produto de um evento
        /// </summary>
        public bool RemoverProdutoDoEvento(int produtoId, int eventoId)
        {
            if (produtoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do produto inválido");
                return false;
            }

            if (eventoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return false;
            }

            try
            {
                return _repository.DeleteVinculacao(produtoId, eventoId);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao remover produto do evento: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Reduz a quantidade vendida de um produto em um evento (ao confirmar venda)
        /// </summary>
        public bool RegistrarVendaProduto(int idProdutoEvento, int quantidade)
        {
            if (idProdutoEvento <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do produto evento inválido");
                return false;
            }

            if (quantidade <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Quantidade deve ser maior que zero");
                return false;
            }

            try
            {
                return _repository.ReduzirQuantidadeVendida(idProdutoEvento, quantidade);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao registrar venda do produto: {ex.Message}");
                return false;
            }
        }
    }
}
