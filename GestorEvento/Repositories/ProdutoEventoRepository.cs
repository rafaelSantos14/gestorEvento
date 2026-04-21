using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class ProdutoEventoRepository
    {
        private readonly string _connectionString;

        public ProdutoEventoRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Obtém todos os produtos vinculados a um evento com seus dados
        /// </summary>
        public List<ProdutoEvento> GetProdutosVinculadosByEvento(int eventoId)
        {
            var produtos = new List<ProdutoEvento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_produto_evento, id_produto, id_evento, vl_produto, qtde_produto FROM PRODUTO_EVENTO WHERE id_evento = @eventoId AND fl_ativo = 'SIM'";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventoId", eventoId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var produtoEvento = new ProdutoEvento
                                {
                                    Id = Convert.ToInt32(reader["id_produto_evento"]),
                                    IdProduto = Convert.ToInt32(reader["id_produto"]),
                                    IdEvento = Convert.ToInt32(reader["id_evento"]),
                                    Preco = Convert.ToDecimal(reader["vl_produto"]),
                                    Quantidade = Convert.ToInt32(reader["qtde_produto"])
                                };
                                produtos.Add(produtoEvento);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter produtos do evento: {ex.Message}");
            }

            return produtos;
        }

        /// <summary>
        /// Obtém todos os IDs de produtos vinculados a um evento (compatibilidade)
        /// </summary>
        public List<int> GetProdutosByEvento(int eventoId)
        {
            var produtoIds = new List<int>();

            try
            {
                var produtos = GetProdutosVinculadosByEvento(eventoId);
                foreach (var p in produtos)
                {
                    produtoIds.Add(p.IdProduto);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter produtos do evento: {ex.Message}");
            }

            return produtoIds;
        }

        /// <summary>
        /// Vincula um produto a um evento com preço e quantidade
        /// </summary>
        public bool CreateVinculacao(int produtoId, int eventoId, decimal preco, int quantidade)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Verifica se já não existe
                    string checkQuery = "SELECT COUNT(*) FROM PRODUTO_EVENTO WHERE id_produto = @produtoId AND id_evento = @eventoId";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@produtoId", produtoId);
                        checkCommand.Parameters.AddWithValue("@eventoId", eventoId);

                        int count = Convert.ToInt32(checkCommand.ExecuteScalar());
                        if (count > 0)
                        {
                            // Se já existe, apenas atualiza preco e quantidade
                            string updateQuery = "UPDATE PRODUTO_EVENTO SET vl_produto = @preco, qtde_produto = @quantidade WHERE id_produto = @produtoId AND id_evento = @eventoId";
                            using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                            {
                                updateCommand.Parameters.AddWithValue("@preco", preco);
                                updateCommand.Parameters.AddWithValue("@quantidade", quantidade);
                                updateCommand.Parameters.AddWithValue("@produtoId", produtoId);
                                updateCommand.Parameters.AddWithValue("@eventoId", eventoId);

                                updateCommand.ExecuteNonQuery();
                                return true;
                            }
                        }
                    }

                    // Insere nova vinculação
                    string query = "INSERT INTO PRODUTO_EVENTO (id_produto, id_evento, vl_produto, qtde_produto) VALUES (@produtoId, @eventoId, @preco, @quantidade)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@produtoId", produtoId);
                        command.Parameters.AddWithValue("@eventoId", eventoId);
                        command.Parameters.AddWithValue("@preco", preco);
                        command.Parameters.AddWithValue("@quantidade", quantidade);

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao vincular produto ao evento: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove a vinculação de um produto a um evento
        /// </summary>
        public bool DeleteVinculacao(int produtoId, int eventoId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM PRODUTO_EVENTO WHERE id_produto = @produtoId AND id_evento = @eventoId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@produtoId", produtoId);
                        command.Parameters.AddWithValue("@eventoId", eventoId);

                        int rowsAffected = command.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao remover vinculação: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove todas as vinculações de um evento
        /// </summary>
        public bool DeleteAllByEvento(int eventoId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM PRODUTO_EVENTO WHERE id_evento = @eventoId";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventoId", eventoId);

                        int rowsAffected = command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao remover vinculações do evento: {ex.Message}");
            }
        }
    }
}
