using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Models.Exceptions;

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

                    string query = "SELECT id_produto_evento, id_produto, id_evento, vl_produto, qtde_produto, COALESCE(qtde_vendida, 0) as qtde_vendida, fl_permite_vl_zerado, fl_antecipado FROM PRODUTO_EVENTO WHERE id_evento = @eventoId AND fl_ativo = 'SIM'";

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
                                    Quantidade = Convert.ToInt32(reader["qtde_produto"]),
                                    QuantidadeVendida = Convert.ToInt32(reader["qtde_vendida"]),
                                    PermiteValorZerado = reader["fl_permite_vl_zerado"]?.ToString() == "SIM",
                                    Antecipado = reader["fl_antecipado"]?.ToString() == "SIM"
                                };
                                produtos.Add(produtoEvento);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter produtos do evento: {ex.Message}", ex);
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
                throw new Exception($"Erro ao obter produtos do evento: {ex.Message}", ex);
            }

            return produtoIds;
        }

        /// <summary>
        /// Vincula um produto a um evento com preço e quantidade
        /// </summary>
        public bool CreateVinculacao(int produtoId, int eventoId, decimal preco, int quantidade, bool permiteValorZerado, bool antecipado = false)
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
                            // Se já existe, obter id_produto_evento, preço/quantidade atuais e quantidade já vendida
                            int idProdutoEvento;
                            decimal precoAnterior;
                            int quantidadeAnterior;
                            int qtdeVendida;

                            string getAtualQuery = "SELECT id_produto_evento, vl_produto, qtde_produto, COALESCE(qtde_vendida, 0) as qtde_vendida FROM PRODUTO_EVENTO WHERE id_produto = @produtoId AND id_evento = @eventoId";
                            using (MySqlCommand getAtualCommand = new MySqlCommand(getAtualQuery, connection))
                            {
                                getAtualCommand.Parameters.AddWithValue("@produtoId", produtoId);
                                getAtualCommand.Parameters.AddWithValue("@eventoId", eventoId);

                                using (MySqlDataReader reader = getAtualCommand.ExecuteReader())
                                {
                                    reader.Read();
                                    idProdutoEvento = Convert.ToInt32(reader["id_produto_evento"]);
                                    precoAnterior = Convert.ToDecimal(reader["vl_produto"]);
                                    quantidadeAnterior = Convert.ToInt32(reader["qtde_produto"]);
                                    qtdeVendida = Convert.ToInt32(reader["qtde_vendida"]);
                                }
                            }

                            // Validação: não permitir reduzir a quantidade para menos que o já vendido
                            if (quantidade < qtdeVendida)
                            {
                                throw new Exception($"Não é permitido reduzir a quantidade para {quantidade} pois já foram vendidas {qtdeVendida} unidades neste evento. Quantidade mínima: {qtdeVendida}");
                            }

                            // Se já existe e passou na validação, atualiza preco e quantidade e registra a movimentação
                            // numa mesma transação para o histórico nunca ficar fora de sincronia com o dado atual
                            using (MySqlTransaction transaction = connection.BeginTransaction())
                            {
                                try
                                {
                                    string updateQuery = "UPDATE PRODUTO_EVENTO SET vl_produto = @preco, qtde_produto = @quantidade, fl_permite_vl_zerado = @permiteValorZerado, fl_antecipado = @antecipado WHERE id_produto = @produtoId AND id_evento = @eventoId";
                                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection, transaction))
                                    {
                                        updateCommand.Parameters.AddWithValue("@preco", preco);
                                        updateCommand.Parameters.AddWithValue("@quantidade", quantidade);
                                        updateCommand.Parameters.AddWithValue("@permiteValorZerado", permiteValorZerado ? "SIM" : "NAO");
                                        updateCommand.Parameters.AddWithValue("@antecipado", antecipado ? "SIM" : "NAO");
                                        updateCommand.Parameters.AddWithValue("@produtoId", produtoId);
                                        updateCommand.Parameters.AddWithValue("@eventoId", eventoId);

                                        updateCommand.ExecuteNonQuery();
                                    }

                                    if (preco != precoAnterior || quantidade != quantidadeAnterior)
                                    {
                                        RegistrarMovimentacao(connection, transaction, idProdutoEvento, precoAnterior, preco, quantidadeAnterior, quantidade);
                                    }

                                    transaction.Commit();
                                    return true;
                                }
                                catch
                                {
                                    transaction.Rollback();
                                    throw;
                                }
                            }
                        }
                    }

                    // Insere nova vinculação
                    string query = "INSERT INTO PRODUTO_EVENTO (id_produto, id_evento, vl_produto, qtde_produto, fl_permite_vl_zerado, fl_antecipado) VALUES (@produtoId, @eventoId, @preco, @quantidade, @permiteValorZerado, @antecipado)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@produtoId", produtoId);
                        command.Parameters.AddWithValue("@eventoId", eventoId);
                        command.Parameters.AddWithValue("@preco", preco);
                        command.Parameters.AddWithValue("@quantidade", quantidade);
                        command.Parameters.AddWithValue("@permiteValorZerado", permiteValorZerado ? "SIM" : "NAO");
                        command.Parameters.AddWithValue("@antecipado", antecipado ? "SIM" : "NAO");

                        command.ExecuteNonQuery();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao vincular produto ao evento: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém o histórico de alterações de preço/quantidade de um produto em um evento, mais recente primeiro
        /// </summary>
        public List<ProdutoEventoMovimentacao> GetHistoricoMovimentacoes(int produtoId, int eventoId)
        {
            var historico = new List<ProdutoEventoMovimentacao>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT m.id_produto_evento_movimentacao, m.id_produto_evento, " +
                                   "m.vl_produto_anterior, m.vl_produto_novo, " +
                                   "m.qtde_produto_anterior, m.qtde_produto_novo, m.dt_movimentacao " +
                                   "FROM PRODUTO_EVENTO_MOVIMENTACAO m " +
                                   "INNER JOIN PRODUTO_EVENTO pe ON pe.id_produto_evento = m.id_produto_evento " +
                                   "WHERE pe.id_produto = @produtoId AND pe.id_evento = @eventoId " +
                                   "ORDER BY m.dt_movimentacao DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@produtoId", produtoId);
                        command.Parameters.AddWithValue("@eventoId", eventoId);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                historico.Add(new ProdutoEventoMovimentacao
                                {
                                    Id = Convert.ToInt32(reader["id_produto_evento_movimentacao"]),
                                    IdProdutoEvento = Convert.ToInt32(reader["id_produto_evento"]),
                                    ValorAnterior = Convert.ToDecimal(reader["vl_produto_anterior"]),
                                    ValorNovo = Convert.ToDecimal(reader["vl_produto_novo"]),
                                    QuantidadeAnterior = Convert.ToInt32(reader["qtde_produto_anterior"]),
                                    QuantidadeNova = Convert.ToInt32(reader["qtde_produto_novo"]),
                                    DataMovimentacao = Convert.ToDateTime(reader["dt_movimentacao"])
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter histórico de movimentações: {ex.Message}", ex);
            }

            return historico;
        }

        /// <summary>
        /// Registra em PRODUTO_EVENTO_MOVIMENTACAO uma alteração de preço/quantidade de um produto do evento
        /// </summary>
        private void RegistrarMovimentacao(MySqlConnection connection, MySqlTransaction transaction, int idProdutoEvento,
            decimal valorAnterior, decimal valorNovo, int qtdeAnterior, int qtdeNova)
        {
            string query = "INSERT INTO PRODUTO_EVENTO_MOVIMENTACAO (id_produto_evento, vl_produto_anterior, vl_produto_novo, qtde_produto_anterior, qtde_produto_novo) " +
                            "VALUES (@idProdutoEvento, @valorAnterior, @valorNovo, @qtdeAnterior, @qtdeNova)";

            using (MySqlCommand command = new MySqlCommand(query, connection, transaction))
            {
                command.Parameters.AddWithValue("@idProdutoEvento", idProdutoEvento);
                command.Parameters.AddWithValue("@valorAnterior", valorAnterior);
                command.Parameters.AddWithValue("@valorNovo", valorNovo);
                command.Parameters.AddWithValue("@qtdeAnterior", qtdeAnterior);
                command.Parameters.AddWithValue("@qtdeNova", qtdeNova);

                command.ExecuteNonQuery();
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
                throw new Exception($"Erro ao remover vinculação: {ex.Message}", ex);
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
                throw new Exception($"Erro ao remover vinculações do evento: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Reduz a quantidade vendida de um produto em um evento (ao confirmar venda)
        /// COM VALIDAÇÃO: Impede vender mais do que está disponível
        /// </summary>
        public bool ReduzirQuantidadeVendida(int idProdutoEvento, int quantidade)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    // 1. PRIMEIRO: Verificar se há quantidade suficiente
                    string selectQuery = "SELECT qtde_produto, COALESCE(qtde_vendida, 0) as qtde_vendida FROM PRODUTO_EVENTO WHERE id_produto_evento = @idProdutoEvento";
                    
                    int qtdeTotal = 0;
                    int qtdeJaVendida = 0;

                    using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection))
                    {
                        selectCommand.Parameters.AddWithValue("@idProdutoEvento", idProdutoEvento);

                        using (MySqlDataReader reader = selectCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                qtdeTotal = Convert.ToInt32(reader["qtde_produto"]);
                                qtdeJaVendida = Convert.ToInt32(reader["qtde_vendida"]);
                            }
                            else
                            {
                                throw new Exception($"Produto evento ID {idProdutoEvento} não encontrado");
                            }
                        }
                    }

                    // 2. Calcular quantidade disponível (estoque real no banco)
                    int quantidadeDisponivel = qtdeTotal - qtdeJaVendida;

                    // 3. VALIDAR: Quantidade solicitada não pode exceder o disponível
                    if (quantidade > quantidadeDisponivel)
                    {
                        throw new Exception($"Estoque insuficiente! Disponível: {quantidadeDisponivel}, Solicitado: {quantidade}");
                    }

                    // 4. UPDATE seguro - agora sabemos que há estoque
                    string updateQuery = "UPDATE PRODUTO_EVENTO SET qtde_vendida = COALESCE(qtde_vendida, 0) + @quantidade WHERE id_produto_evento = @idProdutoEvento";

                    using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection))
                    {
                        updateCommand.Parameters.AddWithValue("@idProdutoEvento", idProdutoEvento);
                        updateCommand.Parameters.AddWithValue("@quantidade", quantidade);

                        int rowsAffected = updateCommand.ExecuteNonQuery();
                        
                        if (rowsAffected > 0)
                        {
                            return true;
                        }
                        else
                        {
                            throw new Exception($"Falha ao atualizar estoque para ID {idProdutoEvento}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao registrar estoque: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Valida e debita estoque atomicamente dentro de uma transação existente
        /// CRÍTICO: Usa SELECT...FOR UPDATE para lock pessimista (previne race condition)
        /// </summary>
        /// <remarks>
        /// Este método DEVE ser chamado dentro de uma transação aberta
        /// Lança EstoqueInsuficienteException se estoque for insuficiente
        /// </remarks>
        public void ValidarEDebitarEstoqueComTransacao(MySqlConnection connection, MySqlTransaction transaction, 
            int idProdutoEvento, int quantidade, string nomeProduto)
        {
            try
            {
                if (connection == null || transaction == null)
                    throw new ArgumentNullException("Connection e Transaction devem estar abertos");

                if (idProdutoEvento <= 0)
                    throw new ArgumentException("ID do produto evento inválido");

                if (quantidade <= 0)
                    throw new ArgumentException("Quantidade deve ser maior que zero");

                // 1. SELECIONAR COM LOCK PESSIMISTA (previne race condition)
                string selectQuery = "SELECT id_produto_evento, qtde_produto, COALESCE(qtde_vendida, 0) as qtde_vendida " +
                                     "FROM PRODUTO_EVENTO " +
                                     "WHERE id_produto_evento = @idProdutoEvento " +
                                     "FOR UPDATE";

                int qtdeTotal = 0;
                int qtdeJaVendida = 0;
                int qtdeDisponivel = 0;

                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection, transaction))
                {
                    selectCommand.Parameters.AddWithValue("@idProdutoEvento", idProdutoEvento);
                    selectCommand.CommandTimeout = 30; // Timeout para evitar deadlock

                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            qtdeTotal = Convert.ToInt32(reader["qtde_produto"]);
                            qtdeJaVendida = Convert.ToInt32(reader["qtde_vendida"]);
                            qtdeDisponivel = qtdeTotal - qtdeJaVendida;
                        }
                        else
                        {
                            throw new EstoqueInsuficienteException(idProdutoEvento, nomeProduto, 0, quantidade);
                        }
                    }
                }

                // 2. VALIDAR: Quantidade solicitada não pode exceder a disponível
                if (quantidade > qtdeDisponivel)
                {
                    throw new EstoqueInsuficienteException(idProdutoEvento, nomeProduto, qtdeDisponivel, quantidade);
                }

                // 3. DEBITAR: UPDATE seguro (ainda dentro do lock)
                string updateQuery = "UPDATE PRODUTO_EVENTO " +
                                     "SET qtde_vendida = qtde_vendida + @quantidade " +
                                     "WHERE id_produto_evento = @idProdutoEvento";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection, transaction))
                {
                    updateCommand.Parameters.AddWithValue("@idProdutoEvento", idProdutoEvento);
                    updateCommand.Parameters.AddWithValue("@quantidade", quantidade);
                    updateCommand.CommandTimeout = 30;

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    
                    if (rowsAffected <= 0)
                    {
                        throw new Exception($"Falha ao atualizar estoque para produto {nomeProduto}");
                    }
                }
            }
            catch (EstoqueInsuficienteException)
            {
                // Propagar exception específica de estoque
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao validar e debitar estoque do produto {nomeProduto}: {ex.Message}", ex);
            }
        }
    }
}
