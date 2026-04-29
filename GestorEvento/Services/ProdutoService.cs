using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    public class ProdutoService
    {
        private readonly ProdutoRepository _repository;

        public ProdutoService()
        {
            _repository = new ProdutoRepository();
        }

        /// <summary>
        /// Obtém todos os produtos
        /// </summary>
        public List<Produto> GetAllProducts()
        {
            try
            {
                return _repository.GetAllProducts();
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter todos os produtos: {ex.Message}");
                return new List<Produto>();
            }
        }

        /// <summary>
        /// Obtém um produto por ID
        /// </summary>
        public Produto GetProductById(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do produto inválido");
                return null;
            }

            try
            {
                return _repository.GetProductById(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter produto por ID: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Cria um novo produto com validações
        /// </summary>
        public bool CreateProduct(Produto produto)
        {
            // Validações
            if (produto == null)
            {
                UiHelper.ExibirAviso("Aviso", "Produto não pode ser nulo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                UiHelper.ExibirAviso("Aviso", "Nome do produto não pode ser vazio");
                return false;
            }

            if (produto.Nome.Length > 255)
            {
                UiHelper.ExibirAviso("Aviso", "Nome do produto não pode ter mais de 255 caracteres");
                return false;
            }

            try
            {
                return _repository.CreateProduct(produto);
            }
            catch (MySqlException mySqlEx)
            {
                // Erro 1062 = Duplicate Entry (chave única violada)
                if (mySqlEx.Number == 1062)
                {
                    UiHelper.ExibirAviso("Aviso", "Já existe um produto com esse nome. Por favor, escolha outro nome.");
                }
                else
                {
                    UiHelper.ExibirErro("Erro", $"Erro ao criar produto: {mySqlEx.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao criar produto: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Atualiza um produto existente com validações
        /// </summary>
        public bool UpdateProduct(Produto produto)
        {
            // Validações
            if (produto == null)
            {
                UiHelper.ExibirAviso("Aviso", "Produto não pode ser nulo");
                return false;
            }

            if (produto.Id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do produto inválido");
                return false;
            }

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                UiHelper.ExibirAviso("Aviso", "Nome do produto não pode ser vazio");
                return false;
            }

            if (produto.Nome.Length > 255)
            {
                UiHelper.ExibirAviso("Aviso", "Nome do produto não pode ter mais de 255 caracteres");
                return false;
            }

            try
            {
                return _repository.UpdateProduct(produto);
            }
            catch (MySqlException mySqlEx)
            {
                // Erro 1062 = Duplicate Entry (chave única violada)
                if (mySqlEx.Number == 1062)
                {
                    UiHelper.ExibirAviso("Aviso", "Já existe um produto com esse nome. Por favor, escolha outro nome.");
                }
                else
                {
                    UiHelper.ExibirErro("Erro", $"Erro ao atualizar produto: {mySqlEx.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao atualizar produto: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Deleta um produto por ID
        /// </summary>
        public bool DeleteProduct(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do produto inválido");
                return false;
            }

            try
            {
                return _repository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao deletar produto: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Busca produtos por nome
        /// </summary>
        public List<Produto> SearchProducts(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                return GetAllProducts();
            }

            try
            {
                return _repository.SearchProducts(nome);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao buscar produtos: {ex.Message}");
                return new List<Produto>();
            }
        }
    }
}
