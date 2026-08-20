using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class DoacaoVendaRepository
    {
        private readonly string _connectionString;

        public DoacaoVendaRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Registra uma doação dentro de uma transação existente
        /// </summary>
        public int RegistrarDoacaoComTransacao(MySqlConnection connection, MySqlTransaction transaction, DoacaoVenda doacao)
        {
            try
            {
                string query = @"INSERT INTO DOACAO_VENDA
                                 (id_venda, id_forma_pagamento, vl_doacao_venda, dt_doacao_venda)
                                 VALUES
                                 (@idVenda, @idFormaPagamento, @vlDoacao, @dtDoacao);
                                 SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@idVenda", doacao.IdVenda);
                    command.Parameters.AddWithValue("@idFormaPagamento", doacao.IdFormaPagamento);
                    command.Parameters.AddWithValue("@vlDoacao", doacao.VlDoacao);
                    command.Parameters.AddWithValue("@dtDoacao", doacao.DtDoacao);

                    object result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar doação em transação: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém todas as doações de uma venda
        /// </summary>
        public List<DoacaoVenda> GetDoacoesByVendaId(int idVenda)
        {
            var doacoes = new List<DoacaoVenda>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_doacao_venda, id_venda, id_forma_pagamento, vl_doacao_venda, dt_doacao_venda
                                     FROM DOACAO_VENDA
                                     WHERE id_venda = @idVenda
                                     ORDER BY dt_doacao_venda";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idVenda", idVenda);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                doacoes.Add(new DoacaoVenda
                                {
                                    IdDoacao = Convert.ToInt32(reader["id_doacao_venda"]),
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdFormaPagamento = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    VlDoacao = Convert.ToDecimal(reader["vl_doacao_venda"]),
                                    DtDoacao = Convert.ToDateTime(reader["dt_doacao_venda"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter doações da venda: {ex.Message}", ex);
            }

            return doacoes;
        }

        /// <summary>
        /// Obtém todas as doações de um evento
        /// </summary>
        public List<DoacaoVenda> ObterDoacoesPorEvento(int idEvento)
        {
            var doacoes = new List<DoacaoVenda>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT d.id_doacao_venda, d.id_venda, d.id_forma_pagamento, d.vl_doacao_venda, d.dt_doacao_venda
                                     FROM DOACAO_VENDA d
                                     INNER JOIN VENDA v ON d.id_venda = v.id_venda
                                     INNER JOIN PONTO_VENDA pv ON v.id_ponto_venda = pv.id_ponto_venda
                                     WHERE pv.id_evento = @idEvento
                                     ORDER BY d.dt_doacao_venda DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                doacoes.Add(new DoacaoVenda
                                {
                                    IdDoacao = Convert.ToInt32(reader["id_doacao_venda"]),
                                    IdVenda = Convert.ToInt32(reader["id_venda"]),
                                    IdFormaPagamento = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    VlDoacao = Convert.ToDecimal(reader["vl_doacao_venda"]),
                                    DtDoacao = Convert.ToDateTime(reader["dt_doacao_venda"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter doações por evento: {ex.Message}", ex);
            }

            return doacoes;
        }

        /// <summary>
        /// Obtém resumo de doações agrupadas por forma de pagamento para um ponto de venda
        /// </summary>
        public List<(int idFormaPagamento, string nomeFormaPagamento, decimal totalDoacao)> GetResumoDoacoesByPontoVenda(int idPontoVenda)
        {
            var resumo = new List<(int, string, decimal)>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT fp.id_forma_pagamento, fp.nm_forma_pagamento, SUM(d.vl_doacao_venda) as total_doacao
                                     FROM DOACAO_VENDA d
                                     INNER JOIN VENDA v ON d.id_venda = v.id_venda
                                     INNER JOIN FORMA_PAGAMENTO fp ON d.id_forma_pagamento = fp.id_forma_pagamento
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
                                decimal totalDoacao = Convert.ToDecimal(reader["total_doacao"]);
                                resumo.Add((idFormaPagamento, nomeFormaPagamento, totalDoacao));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter resumo de doações: {ex.Message}", ex);
            }

            return resumo;
        }

        /// <summary>
        /// Obtém o total de doações em dinheiro para um ponto de venda (desde a abertura do caixa)
        /// Usado para ajustar o dinheiro esperado em caixa no fechamento, já que a doação em
        /// dinheiro fica fisicamente no caixa sem passar por RECEBIMENTO_VENDA nem por MOVIMENTACAO_PONTO_VENDA
        /// </summary>
        public decimal GetTotalDoacaoDinheiro(int idPontoVenda)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT SUM(d.vl_doacao_venda) as total
                                     FROM DOACAO_VENDA d
                                     INNER JOIN VENDA v ON d.id_venda = v.id_venda
                                     INNER JOIN FORMA_PAGAMENTO fp ON d.id_forma_pagamento = fp.id_forma_pagamento
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
                throw new Exception($"Erro ao obter total de doação em dinheiro: {ex.Message}", ex);
            }
        }
    }
}
