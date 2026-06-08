using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class ReimpressaoRepository
    {
        private string _connectionString;

        public ReimpressaoRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Registra uma nova reimpressão com seus itens (transação)
        /// </summary>
        public int RegistrarReimpressao(Reimpressao reimpressao)
        {
            int idReimpressaoInserida = 0;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    MySqlTransaction transaction = connection.BeginTransaction();

                    try
                    {
                        // 1. Inserir header da reimpressão
                        string queryReimpressao = @"
                            INSERT INTO REIMPRESSAO (dt_reimpressao, id_motivo, id_evento, id_ponto_venda, vl_total)
                            VALUES (@dtReimpressao, @idMotivo, @idEvento, @idPontoVenda, @vlTotal);
                            SELECT LAST_INSERT_ID();";

                        using (MySqlCommand command = new MySqlCommand(queryReimpressao, connection, transaction))
                        {
                            command.Parameters.AddWithValue("@dtReimpressao", reimpressao.DtReimpressao);
                            command.Parameters.AddWithValue("@idMotivo", reimpressao.IdMotivo);
                            command.Parameters.AddWithValue("@idEvento", reimpressao.IdEvento);
                            command.Parameters.AddWithValue("@idPontoVenda", reimpressao.IdPontoVenda);
                            command.Parameters.AddWithValue("@vlTotal", reimpressao.VlTotal);

                            idReimpressaoInserida = Convert.ToInt32(command.ExecuteScalar());
                        }

                        // 2. Inserir itens da reimpressão
                        string queryItens = @"
                            INSERT INTO REIMPRESSAO_ITENS (id_reimpressao, id_produto_evento, qtde_reimpressao, vl_unitario, vl_subtotal)
                            VALUES (@idReimpressao, @idProdutoEvento, @qtdeReimpressao, @vlUnitario, @vlSubtotal)";

                        foreach (var item in reimpressao.Itens)
                        {
                            using (MySqlCommand command = new MySqlCommand(queryItens, connection, transaction))
                            {
                                command.Parameters.AddWithValue("@idReimpressao", idReimpressaoInserida);
                                command.Parameters.AddWithValue("@idProdutoEvento", item.IdProdutoEvento);
                                command.Parameters.AddWithValue("@qtdeReimpressao", item.QtdeReimpressao);
                                command.Parameters.AddWithValue("@vlUnitario", item.VlUnitario);
                                command.Parameters.AddWithValue("@vlSubtotal", item.VlSubtotal);

                                command.ExecuteNonQuery();
                            }
                        }

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw new Exception($"Erro ao registrar reimpressão: {ex.Message}", ex);
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro na operação de reimpressão: {ex.Message}", ex);
            }

            return idReimpressaoInserida;
        }

        /// <summary>
        /// Obtém uma reimpressão específica pelo ID com seus itens
        /// </summary>
        public Reimpressao GetReimpressaoById(int idReimpressao)
        {
            Reimpressao reimpressao = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Buscar header
                    string queryReimpressao = @"
                        SELECT id_reimpressao, dt_reimpressao, id_motivo, id_evento, id_ponto_venda, vl_total
                        FROM REIMPRESSAO
                        WHERE id_reimpressao = @idReimpressao";

                    using (MySqlCommand command = new MySqlCommand(queryReimpressao, connection))
                    {
                        command.Parameters.AddWithValue("@idReimpressao", idReimpressao);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                reimpressao = new Reimpressao
                                {
                                    IdReimpressao = reader.GetInt32("id_reimpressao"),
                                    DtReimpressao = reader.GetDateTime("dt_reimpressao"),
                                    IdMotivo = reader.GetInt32("id_motivo"),
                                    IdEvento = reader.GetInt32("id_evento"),
                                    IdPontoVenda = reader.GetInt32("id_ponto_venda"),
                                    VlTotal = reader.GetDecimal("vl_total")
                                };
                            }
                        }
                    }

                    // Buscar itens com JOIN para descrição do produto
                    if (reimpressao != null)
                    {
                        string queryItens = @"
                            SELECT 
                                ri.id_reimpressao_item, 
                                ri.id_reimpressao, 
                                ri.id_produto_evento, 
                                ri.qtde_reimpressao, 
                                ri.vl_unitario, 
                                ri.vl_subtotal,
                                COALESCE(p.nm_produto, 'Produto') AS descricao_produto
                            FROM REIMPRESSAO_ITENS ri
                            LEFT JOIN PRODUTO_EVENTO pe ON ri.id_produto_evento = pe.id_produto_evento
                            LEFT JOIN PRODUTO p ON pe.id_produto = p.id_produto
                            WHERE ri.id_reimpressao = @idReimpressao";

                        using (MySqlCommand command = new MySqlCommand(queryItens, connection))
                        {
                            command.Parameters.AddWithValue("@idReimpressao", idReimpressao);

                            using (MySqlDataReader reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var item = new ReimpressaoItem
                                    {
                                        IdReimpressaoItem = reader.GetInt32("id_reimpressao_item"),
                                        IdReimpressao = reader.GetInt32("id_reimpressao"),
                                        IdProdutoEvento = reader.GetInt32("id_produto_evento"),
                                        QtdeReimpressao = reader.GetInt32("qtde_reimpressao"),
                                        VlUnitario = reader.GetDecimal("vl_unitario"),
                                        VlSubtotal = reader.GetDecimal("vl_subtotal"),
                                        DescricaoProduto = reader.GetString("descricao_produto")
                                    };
                                    reimpressao.Itens.Add(item);
                                }
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter reimpressão: {ex.Message}", ex);
            }

            return reimpressao;
        }

        /// <summary>
        /// Obtém todas as reimpressões de um evento específico
        /// </summary>
        public List<Reimpressao> GetReimpressoesPorEvento(int idEvento)
        {
            var reimpressoes = new List<Reimpressao>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // Buscar headers
                    string queryReimpressoes = @"
                        SELECT id_reimpressao, dt_reimpressao, id_motivo, id_evento, id_ponto_venda, vl_total
                        FROM REIMPRESSAO
                        WHERE id_evento = @idEvento
                        ORDER BY dt_reimpressao DESC";

                    using (MySqlCommand command = new MySqlCommand(queryReimpressoes, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var reimpressao = new Reimpressao
                                {
                                    IdReimpressao = reader.GetInt32("id_reimpressao"),
                                    DtReimpressao = reader.GetDateTime("dt_reimpressao"),
                                    IdMotivo = reader.GetInt32("id_motivo"),
                                    IdEvento = reader.GetInt32("id_evento"),
                                    IdPontoVenda = reader.GetInt32("id_ponto_venda"),
                                    VlTotal = reader.GetDecimal("vl_total")
                                };
                                reimpressoes.Add(reimpressao);
                            }
                        }
                    }

                    // Buscar itens para cada reimpressão com descrição do produto
                    string queryItens = @"
                        SELECT 
                            ri.id_reimpressao_item, 
                            ri.id_reimpressao, 
                            ri.id_produto_evento, 
                            ri.qtde_reimpressao, 
                            ri.vl_unitario, 
                            ri.vl_subtotal,
                            COALESCE(p.nm_produto, 'Produto') AS descricao_produto
                        FROM REIMPRESSAO_ITENS ri
                        LEFT JOIN PRODUTO_EVENTO pe ON ri.id_produto_evento = pe.id_produto_evento
                        LEFT JOIN PRODUTO p ON pe.id_produto = p.id_produto
                        WHERE ri.id_reimpressao IN (SELECT id_reimpressao FROM REIMPRESSAO WHERE id_evento = @idEvento)";

                    using (MySqlCommand command = new MySqlCommand(queryItens, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                int idReimpressaoItem = reader.GetInt32("id_reimpressao");
                                var reimpressao = reimpressoes.Find(r => r.IdReimpressao == idReimpressaoItem);

                                if (reimpressao != null)
                                {
                                    var item = new ReimpressaoItem
                                    {
                                        IdReimpressaoItem = reader.GetInt32("id_reimpressao_item"),
                                        IdReimpressao = reader.GetInt32("id_reimpressao"),
                                        IdProdutoEvento = reader.GetInt32("id_produto_evento"),
                                        QtdeReimpressao = reader.GetInt32("qtde_reimpressao"),
                                        VlUnitario = reader.GetDecimal("vl_unitario"),
                                        VlSubtotal = reader.GetDecimal("vl_subtotal"),
                                        DescricaoProduto = reader.GetString("descricao_produto")
                                    };
                                    reimpressao.Itens.Add(item);
                                }
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter reimpressões do evento: {ex.Message}", ex);
            }

            return reimpressoes;
        }
    }
}
