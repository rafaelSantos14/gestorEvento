using System;
using System.Collections.Generic;
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
        /// Registra uma venda com seus itens usando uma transação existente
        /// NOTA: Não faz commit, caller é responsável por isso
        /// </summary>
        public int RegistrarVendaComTransacao(MySqlConnection connection, MySqlTransaction transaction, Venda venda)
        {
            try
            {
                // 1. Inserir venda
                string queryVenda = @"INSERT INTO VENDA 
                                      (id_ponto_venda, dt_venda, vl_total, cd_status, tp_operacao) 
                                      VALUES 
                                      (@idPontoVenda, @dtVenda, @vlTotal, @cdStatus, @tpOperacao);
                                      SELECT LAST_INSERT_ID();";

                int idVenda = 0;
                using (MySqlCommand command = new MySqlCommand(queryVenda, connection, transaction))
                {
                    command.Parameters.AddWithValue("@idPontoVenda", venda.IdPontoVenda);
                    command.Parameters.AddWithValue("@dtVenda", venda.DtVenda);
                    command.Parameters.AddWithValue("@vlTotal", venda.VlTotal);
                    command.Parameters.AddWithValue("@cdStatus", "Concluida"); // Status sempre Concluida ao registrar
                    command.Parameters.AddWithValue("@tpOperacao", venda.TipoOperacao ?? "VENDA"); // Tipo de operação: VENDA ou CORTESIA

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
                        command.Parameters.AddWithValue("@idProdutoEvento", item.IdProdutoEvento);
                        command.Parameters.AddWithValue("@qtdeVendida", item.Quantidade);
                        command.Parameters.AddWithValue("@vlUnitario", item.VlUnitario);
                        command.Parameters.AddWithValue("@vlSubtotal", item.Subtotal);

                        command.ExecuteNonQuery();
                    }
                }

                return idVenda;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar venda em transação: {ex.Message}", ex);
            }
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
                                      (id_ponto_venda, dt_venda, vl_total, cd_status, tp_operacao) 
                                      VALUES 
                                      (@idPontoVenda, @dtVenda, @vlTotal, @cdStatus, @tpOperacao);
                                      SELECT LAST_INSERT_ID();";

                int idVenda = 0;
                using (MySqlCommand command = new MySqlCommand(queryVenda, connection, transaction))
                {
                    command.Parameters.AddWithValue("@idPontoVenda", venda.IdPontoVenda);
                    command.Parameters.AddWithValue("@dtVenda", venda.DtVenda);
                    command.Parameters.AddWithValue("@vlTotal", venda.VlTotal);
                    command.Parameters.AddWithValue("@cdStatus", "Concluida"); // Status sempre Concluida ao registrar
                    command.Parameters.AddWithValue("@tpOperacao", venda.TipoOperacao ?? "VENDA"); // Tipo de operação: VENDA ou CORTESIA

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
                        command.Parameters.AddWithValue("@idProdutoEvento", item.IdProdutoEvento);
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

                throw new Exception($"Erro ao registrar venda: {ex.Message}", ex);
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

                    string query = @"SELECT id_venda, id_ponto_venda, dt_venda, vl_total, cd_status, tp_operacao 
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
                                    CdStatus = reader["cd_status"].ToString(),
                                    TipoOperacao = reader["tp_operacao"].ToString()
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
                throw new Exception($"Erro ao obter venda: {ex.Message}", ex);
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

                    string query = @"SELECT iv.id_item_venda, iv.id_venda, iv.id_produto_evento, iv.qtde_vendida, iv.vl_unitario, iv.vl_subtotal, p.nm_produto
                                     FROM ITEM_VENDA iv
                                     LEFT JOIN PRODUTO_EVENTO pe ON pe.id_produto_evento = iv.id_produto_evento
                                     LEFT JOIN Produto p ON p.id_produto = pe.id_produto
                                     WHERE iv.id_venda = @idVenda";

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
                                    IdProdutoEvento = Convert.ToInt32(reader["id_produto_evento"]),
                                    NomeProduto = reader["nm_produto"] == DBNull.Value ? "Produto Removido" : reader["nm_produto"].ToString(),
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
                throw new Exception($"Erro ao obter itens da venda: {ex.Message}", ex);
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

                    string query = @"SELECT id_venda, id_ponto_venda, dt_venda, vl_total, cd_status, tp_operacao 
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
                                    CdStatus = reader["cd_status"].ToString(),
                                    TipoOperacao = reader["tp_operacao"].ToString()
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
                throw new Exception($"Erro ao obter vendas: {ex.Message}", ex);
            }

            return vendas;
        }

        /// <summary>
        /// Obtém resumo de vendas de um ponto de venda (apenas id, data e valor) para fechamento de caixa
        /// </summary>
        public List<(int idVenda, DateTime dtVenda, decimal vlTotal, string tipoOperacao)> GetResumoVendasByPontoVenda(int idPontoVenda)
        {
            var vendas = new List<(int, DateTime, decimal, string)>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_venda, dt_venda, vl_total, tp_operacao 
                                     FROM VENDA 
                                     WHERE id_ponto_venda = @idPontoVenda
                                     ORDER BY dt_venda ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idPontoVenda", idPontoVenda);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idVenda = Convert.ToInt32(reader["id_venda"]);
                                DateTime dtVenda = Convert.ToDateTime(reader["dt_venda"]);
                                decimal vlTotal = Convert.ToDecimal(reader["vl_total"]);
                                string tipoOperacao = reader["tp_operacao"].ToString();
                                vendas.Add((idVenda, dtVenda, vlTotal, tipoOperacao));
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter resumo de vendas: {ex.Message}", ex);
            }

            return vendas;
        }

        /// <summary>
        /// Obtém todas as vendas de um evento (por ponto de venda do evento)
        /// </summary>
        public List<Venda> ObterVendasPorEvento(int idEvento)
        {
            var vendas = new List<Venda>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT v.id_venda, v.id_ponto_venda, v.dt_venda, v.vl_total, v.cd_status, v.tp_operacao 
                                     FROM VENDA v
                                     INNER JOIN PONTO_VENDA pv ON v.id_ponto_venda = pv.id_ponto_venda
                                     WHERE pv.id_evento = @idEvento
                                     ORDER BY v.dt_venda DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);

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
                                    CdStatus = reader["cd_status"].ToString(),
                                    TipoOperacao = reader["tp_operacao"].ToString()
                                };
                                // Carregar itens da venda
                                venda.Itens = GetItensByVendaId(venda.IdVenda);
                                vendas.Add(venda);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter vendas por evento: {ex.Message}", ex);
            }

            return vendas;
        }

        /// <summary>
        /// Obtém total de vendas de um evento
        /// </summary>
        public int ObterTotalVendasPorEvento(int idEvento)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT COUNT(*) as total 
                                     FROM VENDA v
                                     INNER JOIN PONTO_VENDA pv ON v.id_ponto_venda = pv.id_ponto_venda
                                     WHERE pv.id_evento = @idEvento";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);
                        int total = Convert.ToInt32(command.ExecuteScalar());
                        connection.Close();
                        return total;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter total de vendas por evento: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém resumo de produtos vendidos por evento, agrupado por produto e valor unitário
        /// </summary>
        public List<(string nomeProduto, int quantidadeInicial, int quantidadeVendida, int quantidadeCortesia, int quantidadeDisponivel, decimal precoUnitario, decimal valorTotalVendido)> ObterResumoProdutosVendidosPorEvento(int idEvento)
        {
            var produtos = new List<(string, int, int, int, int, decimal, decimal)>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT p.nm_produto,
                                            pe.qtde_produto,
                                            SUM(CASE WHEN v.tp_operacao = 'VENDA' THEN iv.qtde_vendida ELSE 0 END) AS qtde_vendida,
                                            SUM(CASE WHEN v.tp_operacao = 'CORTESIA' THEN iv.qtde_vendida ELSE 0 END) AS qtde_cortesia,
                                            (pe.qtde_produto - COALESCE(pe.qtde_vendida, 0)) AS qtde_disponivel,
                                            iv.vl_unitario,
                                            SUM(CASE WHEN v.tp_operacao = 'VENDA' THEN iv.vl_subtotal ELSE 0 END) AS vl_total_vendido
                                     FROM ITEM_VENDA iv
                                     INNER JOIN VENDA v ON v.id_venda = iv.id_venda
                                     INNER JOIN PONTO_VENDA pv ON pv.id_ponto_venda = v.id_ponto_venda
                                     INNER JOIN PRODUTO_EVENTO pe ON pe.id_produto_evento = iv.id_produto_evento
                                     INNER JOIN PRODUTO p ON p.id_produto = pe.id_produto
                                     WHERE pv.id_evento = @idEvento
                                       AND v.cd_status = 'Concluida'
                                     GROUP BY iv.vl_unitario, pe.id_produto_evento
                                     ORDER BY p.nm_produto ASC, iv.vl_unitario ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nomeProduto = reader["nm_produto"].ToString();
                                int quantidadeInicial = Convert.ToInt32(reader["qtde_produto"]);
                                int quantidadeVendida = Convert.ToInt32(reader["qtde_vendida"]);
                                int quantidadeCortesia = Convert.ToInt32(reader["qtde_cortesia"]);
                                int quantidadeDisponivel = Convert.ToInt32(reader["qtde_disponivel"]);
                                decimal precoUnitario = Convert.ToDecimal(reader["vl_unitario"]);
                                decimal valorTotalVendido = Convert.ToDecimal(reader["vl_total_vendido"]);

                                produtos.Add((nomeProduto, quantidadeInicial, quantidadeVendida, quantidadeCortesia, quantidadeDisponivel, precoUnitario, valorTotalVendido));
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter resumo de produtos vendidos por evento: {ex.Message}", ex);
            }

            return produtos;
        }

        /// <summary>
        /// Obtém resumo de produtos em cortesia por evento, agrupado por produto e valor unitário
        /// </summary>
        public List<(string nomeProduto, int quantidadeInicial, int quantidadeVendida, int quantidadeCortesia, int quantidadeDisponivel, decimal precoUnitario, decimal valorTotalVendido)> ObterResumoProdutosCortesiaPorEvento(int idEvento)
        {
            var produtos = new List<(string, int, int, int, int, decimal, decimal)>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT p.nm_produto,
                                            pe.qtde_produto,
                                            SUM(CASE WHEN v.tp_operacao = 'VENDA' THEN iv.qtde_vendida ELSE 0 END) AS qtde_vendida,
                                            SUM(CASE WHEN v.tp_operacao = 'CORTESIA' THEN iv.qtde_vendida ELSE 0 END) AS qtde_cortesia,
                                            (pe.qtde_produto - COALESCE(pe.qtde_vendida, 0)) AS qtde_disponivel,
                                            iv.vl_unitario,
                                            SUM(CASE WHEN v.tp_operacao = 'CORTESIA' THEN iv.vl_subtotal ELSE 0 END) AS vl_total_cortesia
                                     FROM ITEM_VENDA iv
                                     INNER JOIN VENDA v ON v.id_venda = iv.id_venda
                                     INNER JOIN PONTO_VENDA pv ON pv.id_ponto_venda = v.id_ponto_venda
                                     INNER JOIN PRODUTO_EVENTO pe ON pe.id_produto_evento = iv.id_produto_evento
                                     INNER JOIN PRODUTO p ON p.id_produto = pe.id_produto
                                     WHERE pv.id_evento = @idEvento
                                       AND v.cd_status = 'Concluida'
                                     GROUP BY iv.vl_unitario, pe.id_produto_evento
                                     ORDER BY p.nm_produto ASC, iv.vl_unitario ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                string nomeProduto = reader["nm_produto"].ToString();
                                int quantidadeInicial = Convert.ToInt32(reader["qtde_produto"]);
                                int quantidadeVendida = Convert.ToInt32(reader["qtde_vendida"]);
                                int quantidadeCortesia = Convert.ToInt32(reader["qtde_cortesia"]);
                                int quantidadeDisponivel = Convert.ToInt32(reader["qtde_disponivel"]);
                                decimal precoUnitario = Convert.ToDecimal(reader["vl_unitario"]);
                                decimal valorTotalCortesia = Convert.ToDecimal(reader["vl_total_cortesia"]);

                                produtos.Add((nomeProduto, quantidadeInicial, quantidadeVendida, quantidadeCortesia, quantidadeDisponivel, precoUnitario, valorTotalCortesia));
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter resumo de produtos em cortesia por evento: {ex.Message}", ex);
            }

            return produtos;
        }
    }
}
