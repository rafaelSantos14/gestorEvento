using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Views;

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
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao obter todos os produtos: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao obter todos os produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "ID do produto inválido", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("ID do produto inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return null;
            }

            try
            {
                return _repository.GetProductById(id);
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao obter produto por ID: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao obter produto por ID: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Produto não pode ser nulo", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Produto não pode ser nulo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Nome do produto não pode ser vazio", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Nome do produto não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (produto.Nome.Length > 255)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Nome do produto não pode ter mais de 255 caracteres", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Nome do produto não pode ter mais de 255 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                    try
                    {
                        var dialogo = new DialogoCustomizado("Aviso", "Já existe um produto com esse nome. Por favor, escolha outro nome.", TipoDialogo.Aviso, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show("Já existe um produto com esse nome. Por favor, escolha outro nome.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    try
                    {
                        var dialogo = new DialogoCustomizado("Erro", $"Erro ao criar produto: {mySqlEx.Message}", TipoDialogo.Erro, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show($"Erro ao criar produto: {mySqlEx.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao criar produto: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao criar produto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Produto não pode ser nulo", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Produto não pode ser nulo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (produto.Id <= 0)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "ID do produto inválido", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("ID do produto inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (string.IsNullOrWhiteSpace(produto.Nome))
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Nome do produto não pode ser vazio", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Nome do produto não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (produto.Nome.Length > 255)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Nome do produto não pode ter mais de 255 caracteres", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Nome do produto não pode ter mais de 255 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
                    try
                    {
                        var dialogo = new DialogoCustomizado("Aviso", "Já existe um produto com esse nome. Por favor, escolha outro nome.", TipoDialogo.Aviso, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show("Já existe um produto com esse nome. Por favor, escolha outro nome.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    try
                    {
                        var dialogo = new DialogoCustomizado("Erro", $"Erro ao atualizar produto: {mySqlEx.Message}", TipoDialogo.Erro, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show($"Erro ao atualizar produto: {mySqlEx.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao atualizar produto: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao atualizar produto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "ID do produto inválido", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("ID do produto inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            try
            {
                return _repository.DeleteProduct(id);
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao deletar produto: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao deletar produto: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao buscar produtos: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao buscar produtos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return new List<Produto>();
            }
        }
    }
}
