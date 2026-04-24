using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class RecebimentoRepository
    {
        private readonly string _connectionString;

        public RecebimentoRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Registra um recebimento (pagamento) de uma venda
        /// </summary>
        public int RegistrarRecebimento(Recebimento recebimento)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO RECEBIMENTO 
                                     (id_venda, id_forma_pagamento, vl_recebimento, dt_recebimento) 
                                     VALUES 
                                     (@idVenda, @idFormaPagamento, @vlRecebimento, @dtRecebimento);
                                     SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idVenda", recebimento.IdVenda);
                        command.Parameters.AddWithValue("@idFormaPagamento", recebimento.IdFormaPagamento);
                        command.Parameters.AddWithValue("@vlRecebimento", recebimento.VlRecebimento);
                        command.Parameters.AddWithValue("@dtRecebimento", recebimento.DtRecebimento);

                        object result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao registrar recebimento: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtém um recebimento por ID
        /// </summary>
        public Recebimento GetRecebimentoById(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_recebimento, id_venda, id_forma_pagamento, vl_recebimento, dt_recebimento 
                                     FROM RECEBIMENTO 
                                     WHERE id_recebimento = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Recebimento
                                {
                                    IdRecebimento = Convert.ToInt32(reader["id_recebimento"]),
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdFormaPagamento = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    VlRecebimento = Convert.ToDecimal(reader["vl_recebimento"]),
                                    DtRecebimento = Convert.ToDateTime(reader["dt_recebimento"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter recebimento: {ex.Message}");
                throw;
            }

            return null;
        }

        /// <summary>
        /// Obtém todos os recebimentos de uma venda
        /// </summary>
        public List<Recebimento> GetRecebimentosByVendaId(int idVenda)
        {
            List<Recebimento> recebimentos = new List<Recebimento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_recebimento, id_venda, id_forma_pagamento, vl_recebimento, dt_recebimento 
                                     FROM RECEBIMENTO 
                                     WHERE id_venda = @idVenda
                                     ORDER BY dt_recebimento";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idVenda", idVenda);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                recebimentos.Add(new Recebimento
                                {
                                    IdRecebimento = Convert.ToInt32(reader["id_recebimento"]),
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdFormaPagamento = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    VlRecebimento = Convert.ToDecimal(reader["vl_recebimento"]),
                                    DtRecebimento = Convert.ToDateTime(reader["dt_recebimento"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter recebimentos da venda: {ex.Message}");
                throw;
            }

            return recebimentos;
        }
    }
}
