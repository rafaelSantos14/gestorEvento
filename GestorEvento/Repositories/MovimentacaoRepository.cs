using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class MovimentacaoRepository
    {
        private readonly string _connectionString;

        public MovimentacaoRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Registra uma movimentação genérica no ponto de venda
        /// </summary>
        public int RegistrarMovimentacao(Movimentacao movimentacao)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"INSERT INTO MOVIMENTACAO_PONTO_VENDA 
                                     (id_ponto_venda, tipo_movimento, vl_movimento, dt_movimento, descricao, id_venda) 
                                     VALUES 
                                     (@idPontoVenda, @tipoMovimento, @vlMovimento, @dtMovimento, @descricao, @idVenda);
                                     SELECT LAST_INSERT_ID();";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPontoVenda", movimentacao.IdPontoVenda);
                        command.Parameters.AddWithValue("@tipoMovimento", movimentacao.TipoMovimento.ToString());
                        command.Parameters.AddWithValue("@vlMovimento", movimentacao.VlMovimento);
                        command.Parameters.AddWithValue("@dtMovimento", movimentacao.DtMovimento);
                        command.Parameters.AddWithValue("@descricao", movimentacao.Descricao ?? "");
                        
                        // Tratamento de IdVenda nullable (compatível com C# 7.3)
                        object idVendaValue = movimentacao.IdVenda.HasValue ? (object)movimentacao.IdVenda.Value : DBNull.Value;
                        command.Parameters.AddWithValue("@idVenda", idVendaValue);

                        object result = command.ExecuteScalar();
                        return Convert.ToInt32(result);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar movimentação: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra um troco (saída automática de dinheiro)
        /// </summary>
        public int RegistrarTroco(int idPontoVenda, int idVenda, decimal vlTroco, string descricao = null)
        {
            var movimentacao = new Movimentacao
            {
                IdPontoVenda = idPontoVenda,
                TipoMovimento = TipoMovimento.TROCO,
                VlMovimento = vlTroco,
                DtMovimento = DateTime.Now,
                Descricao = descricao ?? $"Troco da venda #{idVenda}",
                IdVenda = idVenda
            };

            return RegistrarMovimentacao(movimentacao);
        }

        /// <summary>
        /// Registra uma sangria (saída manual de dinheiro)
        /// </summary>
        public int RegistrarSangria(int idPontoVenda, decimal vlSangria, string descricao = null)
        {
            var movimentacao = new Movimentacao
            {
                IdPontoVenda = idPontoVenda,
                TipoMovimento = TipoMovimento.SANGRIA,
                VlMovimento = vlSangria,
                DtMovimento = DateTime.Now,
                Descricao = descricao ?? "Sangria de caixa",
                IdVenda = null
            };

            return RegistrarMovimentacao(movimentacao);
        }

        /// <summary>
        /// Registra uma entrada de troco (entrada manual de dinheiro)
        /// </summary>
        public int RegistrarEntradaTroco(int idPontoVenda, decimal vlEntrada, string descricao = null)
        {
            var movimentacao = new Movimentacao
            {
                IdPontoVenda = idPontoVenda,
                TipoMovimento = TipoMovimento.ENTRADA_TROCO,
                VlMovimento = vlEntrada,
                DtMovimento = DateTime.Now,
                Descricao = descricao ?? "Entrada de troco",
                IdVenda = null
            };

            return RegistrarMovimentacao(movimentacao);
        }

        /// <summary>
        /// Obtém todas as movimentações de um ponto de venda
        /// </summary>
        public List<Movimentacao> GetMovimentacoesByPontoVenda(int idPontoVenda)
        {
            var movimentacoes = new List<Movimentacao>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_movimentacao, id_ponto_venda, tipo_movimento, vl_movimento, dt_movimento, descricao, id_venda
                                     FROM MOVIMENTACAO_PONTO_VENDA
                                     WHERE id_ponto_venda = @idPontoVenda
                                     ORDER BY dt_movimento DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPontoVenda", idPontoVenda);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                // Tratamento de IdVenda nullable (compatível com C# 7.3)
                                int? idVendaValue = null;
                                if (reader["id_venda"] != DBNull.Value)
                                {
                                    idVendaValue = Convert.ToInt32(reader["id_venda"]);
                                }

                                movimentacoes.Add(new Movimentacao
                                {
                                    IdMovimentacao = Convert.ToInt32(reader["id_movimentacao"]),
                                    IdPontoVenda = Convert.ToInt32(reader["id_ponto_venda"]),
                                    TipoMovimento = (TipoMovimento)Enum.Parse(typeof(TipoMovimento), reader["tipo_movimento"].ToString()),
                                    VlMovimento = Convert.ToDecimal(reader["vl_movimento"]),
                                    DtMovimento = Convert.ToDateTime(reader["dt_movimento"]),
                                    Descricao = reader["descricao"].ToString(),
                                    IdVenda = idVendaValue
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter movimentações: {ex.Message}", ex);
            }

            return movimentacoes;
        }

        /// <summary>
        /// Obtém o total de movimentações de um tipo específico para um ponto de venda
        /// </summary>
        public decimal GetTotalMovimentacaoPorTipo(int idPontoVenda, TipoMovimento tipo)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT SUM(vl_movimento) as total
                                     FROM MOVIMENTACAO_PONTO_VENDA
                                     WHERE id_ponto_venda = @idPontoVenda 
                                     AND tipo_movimento = @tipoMovimento";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPontoVenda", idPontoVenda);
                        command.Parameters.AddWithValue("@tipoMovimento", tipo.ToString());

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
                throw new Exception($"Erro ao obter total de movimentação: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Registra um troco dentro de uma transação existente
        /// </summary>
        public int RegistrarTrocoComTransacao(MySqlConnection connection, MySqlTransaction transaction, int idPontoVenda, int idVenda, decimal vlTroco)
        {
            try
            {
                string query = @"INSERT INTO MOVIMENTACAO_PONTO_VENDA 
                                 (id_ponto_venda, tipo_movimento, vl_movimento, dt_movimento, descricao, id_venda) 
                                 VALUES 
                                 (@idPontoVenda, @tipoMovimento, @vlMovimento, @dtMovimento, @descricao, @idVenda);
                                 SELECT LAST_INSERT_ID();";

                using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
                {
                    command.Parameters.AddWithValue("@idPontoVenda", idPontoVenda);
                    command.Parameters.AddWithValue("@tipoMovimento", TipoMovimento.TROCO.ToString());
                    command.Parameters.AddWithValue("@vlMovimento", vlTroco);
                    command.Parameters.AddWithValue("@dtMovimento", DateTime.Now);
                    command.Parameters.AddWithValue("@descricao", $"Troco da venda #{idVenda}");
                    command.Parameters.AddWithValue("@idVenda", idVenda);

                    object result = command.ExecuteScalar();
                    return Convert.ToInt32(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar troco em transação: {ex.Message}", ex);
            }
        }
    }
}
