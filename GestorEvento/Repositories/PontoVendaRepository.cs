using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class PontoVendaRepository
    {
        private readonly string _connectionString;

        public PontoVendaRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Abre um novo ponto de venda (caixa)
        /// </summary>
        public int AbrirPontoVenda(int eventoId, decimal valorInicial, string descricao = null)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Calcular o próximo número de caixa para este evento
                    string queryMaxNo = @"SELECT COALESCE(MAX(no_ponto_venda), 0) + 1 FROM PONTO_VENDA WHERE id_evento = @eventoId";
                    using (MySqlCommand cmdMaxNo = new MySqlCommand(queryMaxNo, connection))
                    {
                        cmdMaxNo.Parameters.AddWithValue("@eventoId", eventoId);
                        int proximoNo = Convert.ToInt32(cmdMaxNo.ExecuteScalar());

                        string query = @"INSERT INTO PONTO_VENDA 
                                         (id_evento, no_ponto_venda, ds_ponto_venda, cd_status, dt_abertura, vl_inicial) 
                                         VALUES 
                                         (@eventoId, @noPonto, @descricao, @status, @dtAbertura, @vlInicial);
                                         SELECT LAST_INSERT_ID();";

                        using (MySqlCommand command = new MySqlCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@eventoId", eventoId);
                            command.Parameters.AddWithValue("@noPonto", proximoNo);
                            command.Parameters.AddWithValue("@descricao", (object)descricao ?? DBNull.Value);
                            command.Parameters.AddWithValue("@status", "Aberto");
                            command.Parameters.AddWithValue("@dtAbertura", DateTime.Now);
                            command.Parameters.AddWithValue("@vlInicial", valorInicial);

                            object result = command.ExecuteScalar();
                            int novoId = Convert.ToInt32(result);

                            connection.Close();
                            return novoId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao abrir ponto de venda: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Obtém um ponto de venda por ID
        /// </summary>
        public PontoVenda GetPontoVendaById(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_ponto_venda, id_evento, no_ponto_venda, ds_ponto_venda, cd_status, dt_abertura, vl_inicial, 
                                            dt_fechamento, vl_final, obs_caixa 
                                     FROM PONTO_VENDA 
                                     WHERE id_ponto_venda = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var pontoVenda = new PontoVenda
                                {
                                    IdPontoVenda = Convert.ToInt32(reader["id_ponto_venda"]),
                                    IdEvento = Convert.ToInt32(reader["id_evento"]),
                                    NoPontoVenda = Convert.ToInt32(reader["no_ponto_venda"]),
                                    DsPontoVenda = reader["ds_ponto_venda"] != DBNull.Value ? reader["ds_ponto_venda"].ToString() : null,
                                    CdStatus = reader["cd_status"].ToString(),
                                    DtAbertura = Convert.ToDateTime(reader["dt_abertura"]),
                                    VlInicial = Convert.ToDecimal(reader["vl_inicial"]),
                                    DtFechamento = reader["dt_fechamento"] != DBNull.Value ? Convert.ToDateTime(reader["dt_fechamento"]) : (DateTime?)null,
                                    VlFinal = reader["vl_final"] != DBNull.Value ? Convert.ToDecimal(reader["vl_final"]) : (decimal?)null,
                                    ObsCaixa = reader["obs_caixa"].ToString()
                                };

                                connection.Close();
                                return pontoVenda;
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter ponto de venda: {ex.Message}");
                throw;
            }

            return null;
        }

        /// <summary>
        /// Obtém todos os pontos de venda abertos de um evento
        /// </summary>
        public List<PontoVenda> GetCaixasAbertas(int eventoId)
        {
            var pontos = new List<PontoVenda>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_ponto_venda, id_evento, no_ponto_venda, ds_ponto_venda, cd_status, dt_abertura, vl_inicial, 
                                            dt_fechamento, vl_final, obs_caixa 
                                     FROM PONTO_VENDA 
                                     WHERE id_evento = @eventoId AND cd_status = 'Aberto'
                                     ORDER BY no_ponto_venda ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventoId", eventoId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var pontoVenda = new PontoVenda
                                {
                                    IdPontoVenda = Convert.ToInt32(reader["id_ponto_venda"]),
                                    IdEvento = Convert.ToInt32(reader["id_evento"]),
                                    NoPontoVenda = Convert.ToInt32(reader["no_ponto_venda"]),
                                    DsPontoVenda = reader["ds_ponto_venda"] != DBNull.Value ? reader["ds_ponto_venda"].ToString() : null,
                                    CdStatus = reader["cd_status"].ToString(),
                                    DtAbertura = Convert.ToDateTime(reader["dt_abertura"]),
                                    VlInicial = Convert.ToDecimal(reader["vl_inicial"]),
                                    DtFechamento = reader["dt_fechamento"] != DBNull.Value ? Convert.ToDateTime(reader["dt_fechamento"]) : (DateTime?)null,
                                    VlFinal = reader["vl_final"] != DBNull.Value ? Convert.ToDecimal(reader["vl_final"]) : (decimal?)null,
                                    ObsCaixa = reader["obs_caixa"].ToString()
                                };
                                pontos.Add(pontoVenda);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter caixas abertas: {ex.Message}");
                throw;
            }

            return pontos;
        }

        /// <summary>
        /// Fecha um ponto de venda (caixa)
        /// </summary>
        public bool FecharPontoVenda(int id, decimal valorFinal, string observacoes)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"UPDATE PONTO_VENDA 
                                     SET cd_status = 'Fechado', dt_fechamento = @dtFechamento, vl_final = @vlFinal, obs_caixa = @obs 
                                     WHERE id_ponto_venda = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.Parameters.AddWithValue("@dtFechamento", DateTime.Now);
                        command.Parameters.AddWithValue("@vlFinal", valorFinal);
                        command.Parameters.AddWithValue("@obs", observacoes ?? "");

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao fechar ponto de venda: {ex.Message}");
                throw;
            }
        }
    }
}
