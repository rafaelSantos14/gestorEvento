using System;
using System.Collections.Generic;
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

                    string query = @"INSERT INTO RECEBIMENTO_VENDA 
                                     (id_venda, id_forma_pagamento, vl_recebimento_venda, dt_recebimento_venda) 
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
                throw new Exception($"Erro ao registrar recebimento: {ex.Message}", ex);
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

                    string query = @"SELECT id_recebimento_venda, id_venda, id_forma_pagamento, vl_recebimento_venda, dt_recebimento_venda 
                                     FROM RECEBIMENTO_VENDA 
                                     WHERE id_recebimento_venda = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new Recebimento
                                {
                                    IdRecebimento = Convert.ToInt32(reader["id_recebimento_venda"]),
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdFormaPagamento = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    VlRecebimento = Convert.ToDecimal(reader["vl_recebimento_venda"]),
                                    DtRecebimento = Convert.ToDateTime(reader["dt_recebimento_venda"])
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter recebimento: {ex.Message}", ex);
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

                    string query = @"SELECT id_recebimento_venda, id_venda, id_forma_pagamento, vl_recebimento_venda, dt_recebimento_venda 
                                     FROM RECEBIMENTO_VENDA 
                                     WHERE id_venda = @idVenda
                                     ORDER BY dt_recebimento_venda";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idVenda", idVenda);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                recebimentos.Add(new Recebimento
                                {
                                    IdRecebimento = Convert.ToInt32(reader["id_recebimento_venda"]),
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdFormaPagamento = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    VlRecebimento = Convert.ToDecimal(reader["vl_recebimento_venda"]),
                                    DtRecebimento = Convert.ToDateTime(reader["dt_recebimento_venda"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter recebimentos da venda: {ex.Message}", ex);
            }

            return recebimentos;
        }

        /// <summary>
        /// Obtém resumo de recebimentos agrupados por forma de pagamento para um ponto de venda
        /// Retorna: (idFormaPagamento, nomeFormaPagamento, totalRecebimento)
        /// </summary>
        public List<(int idFormaPagamento, string nomeFormaPagamento, decimal totalRecebimento)> GetResumoRecebimentosByPontoVenda(int idPontoVenda)
        {
            var resumo = new List<(int, string, decimal)>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT fp.id_forma_pagamento, fp.nm_forma_pagamento, SUM(r.vl_recebimento_venda) as total_recebimento
                                     FROM RECEBIMENTO_VENDA r
                                     INNER JOIN VENDA v ON r.id_venda = v.id_venda
                                     INNER JOIN FORMA_PAGAMENTO fp ON r.id_forma_pagamento = fp.id_forma_pagamento
                                     WHERE v.id_ponto_venda = @idPontoVenda
                                     GROUP BY fp.id_forma_pagamento, fp.nm_forma_pagamento
                                     ORDER BY fp.nm_forma_pagamento";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPontoVenda", idPontoVenda);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idFormaPagamento = Convert.ToInt32(reader["id_forma_pagamento"]);
                                string nomeFormaPagamento = reader["nm_forma_pagamento"].ToString();
                                decimal totalRecebimento = Convert.ToDecimal(reader["total_recebimento"]);
                                resumo.Add((idFormaPagamento, nomeFormaPagamento, totalRecebimento));
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter resumo de recebimentos: {ex.Message}", ex);
            }

            return resumo;
        }

        /// <summary>
        /// Obtém o total de recebimentos em uma forma de pagamento específica para um ponto de venda
        /// </summary>
        public decimal GetTotalRecebimentoByFormaPagamento(int idPontoVenda, int idFormaPagamento)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT SUM(r.vl_recebimento_venda) as total
                                     FROM RECEBIMENTO_VENDA r
                                     INNER JOIN VENDA v ON r.id_venda = v.id_venda
                                     WHERE v.id_ponto_venda = @idPontoVenda 
                                     AND r.id_forma_pagamento = @idFormaPagamento";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPontoVenda", idPontoVenda);
                        command.Parameters.AddWithValue("@idFormaPagamento", idFormaPagamento);

                        object result = command.ExecuteScalar();
                        if (result != null && !Convert.IsDBNull(result))
                        {
                            return Convert.ToDecimal(result);
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter total de recebimento: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém o total de recebimentos em dinheiro para um ponto de venda (desde a abertura do caixa)
        /// </summary>
        public decimal GetTotalRecebimentoDinheiro(int idPontoVenda)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Filtrar por código da forma de pagamento "DINHEIRO"
                    string query = @"SELECT SUM(r.vl_recebimento_venda) as total
                                     FROM RECEBIMENTO_VENDA r
                                     INNER JOIN VENDA v ON r.id_venda = v.id_venda
                                     INNER JOIN FORMA_PAGAMENTO fp ON r.id_forma_pagamento = fp.id_forma_pagamento
                                     WHERE v.id_ponto_venda = @idPontoVenda 
                                     AND fp.cd_forma_pagamento = @cdFormaPagamento";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPontoVenda", idPontoVenda);
                        command.Parameters.AddWithValue("@cdFormaPagamento", "DINHEIRO");

                        object result = command.ExecuteScalar();
                        if (result != null && !Convert.IsDBNull(result))
                        {
                            return Convert.ToDecimal(result);
                        }
                        return 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter total dinheiro: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra um recebimento dentro de uma transação existente
        /// </summary>
        public int RegistrarRecebimentoComTransacao(MySqlConnection connection, MySqlTransaction transaction, Recebimento recebimento)
        {
            try
            {
                string query = @"INSERT INTO RECEBIMENTO_VENDA 
                                 (id_venda, id_forma_pagamento, vl_recebimento_venda, dt_recebimento_venda) 
                                 VALUES 
                                 (@idVenda, @idFormaPagamento, @vlRecebimento, @dtRecebimento);
                                 SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@idVenda", recebimento.IdVenda);
                    command.Parameters.AddWithValue("@idFormaPagamento", recebimento.IdFormaPagamento);
                    command.Parameters.AddWithValue("@vlRecebimento", recebimento.VlRecebimento);
                    command.Parameters.AddWithValue("@dtRecebimento", recebimento.DtRecebimento);

                    object result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar recebimento em transação: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém todos os recebimentos de um evento
        /// </summary>
        public List<Recebimento> ObterRecebimentosPorEvento(int idEvento)
        {
            var recebimentos = new List<Recebimento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT r.id_recebimento_venda, r.id_venda, r.id_forma_pagamento, r.vl_recebimento_venda, r.dt_recebimento_venda
                                     FROM RECEBIMENTO_VENDA r
                                     INNER JOIN VENDA v ON r.id_venda = v.id_venda
                                     INNER JOIN PONTO_VENDA pv ON v.id_ponto_venda = pv.id_ponto_venda
                                     WHERE pv.id_evento = @idEvento
                                     ORDER BY r.dt_recebimento_venda DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                recebimentos.Add(new Recebimento
                                {
                                    IdRecebimento = Convert.ToInt32(reader["id_recebimento_venda"]),
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdFormaPagamento = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    VlRecebimento = Convert.ToDecimal(reader["vl_recebimento_venda"]),
                                    DtRecebimento = Convert.ToDateTime(reader["dt_recebimento_venda"])
                                });
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter recebimentos por evento: {ex.Message}", ex);
            }

            return recebimentos;
        }

        /// <summary>
        /// Obtém todos os recebimentos de uma venda específica
        /// </summary>
        public List<Recebimento> ObterRecebimentosPorVenda(int idVenda)
        {
            return GetRecebimentosByVendaId(idVenda);
        }
    }
}
