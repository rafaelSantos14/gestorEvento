using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class VendaRepository
    {
        private readonly string _connectionString;

        public VendaRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Registra uma venda com seus itens
        /// </summary>
        public int RegistrarVenda(Venda venda)
        {
            MySqlConnection connection = null;
            MySqlTransaction transaction = null;

            try
            {
                connection = new MySqlConnection(_connectionString);
                connection.Open();
                transaction = connection.BeginTransaction();

                // 1. Inserir venda
                string queryVenda = @"INSERT INTO VENDA 
                                      (id_ponto_venda, dt_venda, vl_total, cd_status) 
                                      VALUES 
                                      (@idPontoVenda, @dtVenda, @vlTotal, @cdStatus);
                                      SELECT LAST_INSERT_ID();";

                int idVenda = 0;
                using (MySqlCommand command = new MySqlCommand(queryVenda, connection, transaction))
                {
                    command.Parameters.AddWithValue("@idPontoVenda", venda.IdPontoVenda);
                    command.Parameters.AddWithValue("@dtVenda", venda.DtVenda);
                    command.Parameters.AddWithValue("@vlTotal", venda.VlTotal);
                    command.Parameters.AddWithValue("@cdStatus", "Concluida"); // Status sempre Concluida ao registrar

                    object result = command.ExecuteScalar();
                    idVenda = Convert.ToInt32(result);
                }

                // 2. Inserir itens da venda
                string queryItem = @"INSERT INTO ITEM_VENDA 
                                     (id_venda, id_produto_evento, qtde_vendida, vl_unitario, vl_subtotal) 
                                     VALUES 
                                     (@idVenda, @idProdutoEvento, @qtdeVendida, @vlUnitario, @vlSubtotal);";

                foreach (var item in venda.Itens)
                {
                    using (MySqlCommand command = new MySqlCommand(queryItem, connection, transaction))
                    {
                        command.Parameters.AddWithValue("@idVenda", idVenda);
                        command.Parameters.AddWithValue("@idProdutoEvento", item.IdProduto);
                        command.Parameters.AddWithValue("@qtdeVendida", item.Quantidade);
                        command.Parameters.AddWithValue("@vlUnitario", item.VlUnitario);
                        command.Parameters.AddWithValue("@vlSubtotal", item.Subtotal);

                        command.ExecuteNonQuery();
                    }
                }

                transaction.Commit();
                connection.Close();

                return idVenda;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                    transaction.Rollback();

                Debug.WriteLine($"Erro ao registrar venda: {ex.Message}");
                throw;
            }
            finally
            {
                if (connection != null)
                    connection.Close();
            }
        }

        /// <summary>
        /// Obtém uma venda por ID
        /// </summary>
        public Venda GetVendaById(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_venda, id_ponto_venda, dt_venda, vl_total, cd_status 
                                     FROM VENDA 
                                     WHERE id_venda = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var venda = new Venda
                                {
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdPontoVenda = Convert.ToInt32(reader["id_ponto_venda"]),
                                    DtVenda = Convert.ToDateTime(reader["dt_venda"]),
                                    VlTotal = Convert.ToDecimal(reader["vl_total"]),
                                    CdStatus = reader["cd_status"].ToString()
                                };

                                connection.Close();

                                // Obter itens da venda
                                venda.Itens = GetItensByVendaId(venda.IdVenda);

                                return venda;
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter venda: {ex.Message}");
                throw;
            }

            return null;
        }

        /// <summary>
        /// Obtém todos os itens de uma venda
        /// </summary>
        public List<ItemVenda> GetItensByVendaId(int idVenda)
        {
            var itens = new List<ItemVenda>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_item_venda, id_venda, id_produto_evento, qtde_vendida, vl_unitario, vl_subtotal 
                                     FROM ITEM_VENDA 
                                     WHERE id_venda = @idVenda";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idVenda", idVenda);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var item = new ItemVenda
                                {
                                    IdItemVenda = Convert.ToInt32(reader["id_item_venda"]),
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdProduto = Convert.ToInt32(reader["id_produto_evento"]),
                                    Quantidade = Convert.ToInt32(reader["qtde_vendida"]),
                                    VlUnitario = Convert.ToDecimal(reader["vl_unitario"]),
                                    Subtotal = Convert.ToDecimal(reader["vl_subtotal"])
                                };
                                itens.Add(item);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter itens da venda: {ex.Message}");
                throw;
            }

            return itens;
        }

        /// <summary>
        /// Lista todas as vendas de um ponto de venda
        /// </summary>
        public List<Venda> GetVendasByPontoVenda(int idPontoVenda)
        {
            var vendas = new List<Venda>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_venda, id_ponto_venda, dt_venda, vl_total, cd_status 
                                     FROM VENDA 
                                     WHERE id_ponto_venda = @idPontoVenda
                                     ORDER BY dt_venda DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPontoVenda", idPontoVenda);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var venda = new Venda
                                {
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdPontoVenda = Convert.ToInt32(reader["id_ponto_venda"]),
                                    DtVenda = Convert.ToDateTime(reader["dt_venda"]),
                                    VlTotal = Convert.ToDecimal(reader["vl_total"]),
                                    CdStatus = reader["cd_status"].ToString()
                                };
                                vendas.Add(venda);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter vendas: {ex.Message}");
                throw;
            }

            return vendas;
        }
    }
}
