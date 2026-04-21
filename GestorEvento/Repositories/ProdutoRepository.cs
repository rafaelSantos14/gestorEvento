using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class ProdutoRepository
    {
        private string _connectionString;

        public ProdutoRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Obtém todos os produtos do banco de dados
        /// </summary>
        public List<Produto> GetAllProducts()
        {
            var produtos = new List<Produto>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_produto, nm_produto FROM Produto ORDER BY nm_produto ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var produto = new Produto
                                {
                                    Id = reader.GetInt32("id_produto"),
                                    Nome = reader.GetString("nm_produto")
                                };
                                produtos.Add(produto);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao obter todos os produtos: {ex.Message}");
            }

            return produtos;
        }

        /// <summary>
        /// Obtém um produto específico por ID
        /// </summary>
        public Produto GetProductById(int id)
        {
            Produto produto = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_produto, nm_produto FROM Produto WHERE id_produto = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                produto = new Produto
                                {
                                    Id = reader.GetInt32("id_produto"),
                                    Nome = reader.GetString("nm_produto")
                                };
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao obter produto por ID: {ex.Message}");
            }

            return produto;
        }

        /// <summary>
        /// Cria um novo produto no banco de dados
        /// </summary>
        public bool CreateProduct(Produto produto)
        {
            if (string.IsNullOrWhiteSpace(produto.Nome))
                return false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO Produto (nm_produto) VALUES (@nome)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", produto.Nome);
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao criar produto: {ex.Message}");
                throw; // Lança exceção para o Service capturar
            }
        }

        /// <summary>
        /// Atualiza um produto existente
        /// </summary>
        public bool UpdateProduct(Produto produto)
        {
            if (produto.Id <= 0 || string.IsNullOrWhiteSpace(produto.Nome))
                return false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE Produto SET nm_produto = @nome WHERE id_produto = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", produto.Nome);
                        command.Parameters.AddWithValue("@id", produto.Id);
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar produto: {ex.Message}");
                throw; // Lança exceção para o Service capturar
            }
        }

        /// <summary>
        /// Deleta um produto por ID
        /// </summary>
        public bool DeleteProduct(int id)
        {
            if (id <= 0)
                return false;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "DELETE FROM Produto WHERE id_produto = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao deletar produto: {ex.Message}");
                throw; // Lança exceção para o Service capturar
            }
        }

        /// <summary>
        /// Busca produtos por nome (LIKE)
        /// </summary>
        public List<Produto> SearchProducts(string nome)
        {
            var produtos = new List<Produto>();

            if (string.IsNullOrWhiteSpace(nome))
                return GetAllProducts();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_produto, nm_produto FROM Produto WHERE nm_produto LIKE UPPER(@nome) ORDER BY nm_produto ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", $"%{nome}%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var produto = new Produto
                                {
                                    Id = reader.GetInt32("id_produto"),
                                    Nome = reader.GetString("nm_produto")
                                };
                                produtos.Add(produto);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao buscar produtos: {ex.Message}");
            }

            return produtos;
        }
    }
}
