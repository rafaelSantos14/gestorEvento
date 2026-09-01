using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Models.Exceptions;

namespace GestorEvento.Repositories
{
    public class InscricaoEventoRepository
    {
        private readonly string _connectionString;

        public InscricaoEventoRepository()
        {
            _connectionString = Connection.GetConnection();
        }
        
        public List<InscricaoEvento> Buscar(int idEvento, string filtroNome = null, string filtroCpf = null, string filtroEmail = null, string cdStatus = InscricaoEvento.StatusPendente)
        {
            var inscricoes = new List<InscricaoEvento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_inscricao_evento, id_evento, nm_participante, ds_email, nr_cpf_cnpj, nr_celular, qtde_antecipada, cd_status, dt_criacao, dt_retirada " +
                                   "FROM INSCRICAO_EVENTO " +
                                   "WHERE id_evento = @idEvento";

                    if (!string.IsNullOrWhiteSpace(cdStatus))
                        query += " AND cd_status = @cdStatus";
                    if (!string.IsNullOrWhiteSpace(filtroNome))
                        query += " AND nm_participante LIKE @filtroNome";
                    if (!string.IsNullOrWhiteSpace(filtroCpf))
                        query += " AND nr_cpf_cnpj LIKE @filtroCpf";
                    if (!string.IsNullOrWhiteSpace(filtroEmail))
                        query += " AND ds_email LIKE @filtroEmail";

                    query += " ORDER BY nm_participante";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idEvento", idEvento);
                        if (!string.IsNullOrWhiteSpace(cdStatus))
                            command.Parameters.AddWithValue("@cdStatus", cdStatus);
                        if (!string.IsNullOrWhiteSpace(filtroNome))
                            command.Parameters.AddWithValue("@filtroNome", $"%{filtroNome}%");
                        if (!string.IsNullOrWhiteSpace(filtroCpf))
                            command.Parameters.AddWithValue("@filtroCpf", $"%{filtroCpf}%");
                        if (!string.IsNullOrWhiteSpace(filtroEmail))
                            command.Parameters.AddWithValue("@filtroEmail", $"%{filtroEmail}%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                inscricoes.Add(MapearInscricao(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao pesquisar inscrições do evento: {ex.Message}", ex);
            }

            return inscricoes;
        }

   
        public InscricaoEvento GetById(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_inscricao_evento, id_evento, nm_participante, ds_email, nr_cpf_cnpj, nr_celular, qtde_antecipada, cd_status, dt_criacao, dt_retirada " +
                                   "FROM INSCRICAO_EVENTO WHERE id_inscricao_evento = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                                return MapearInscricao(reader);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter inscrição: {ex.Message}", ex);
            }

            return null;
        }

        public void ValidarERetirarComTransacao(MySqlConnection connection, MySqlTransaction transaction, int idInscricaoEvento)
        {
            try
            {
                if (connection == null || transaction == null)
                    throw new ArgumentNullException("Connection e Transaction devem estar abertos");

                string selectQuery = "SELECT id_inscricao_evento, nm_participante, cd_status " +
                                     "FROM INSCRICAO_EVENTO " +
                                     "WHERE id_inscricao_evento = @id " +
                                     "FOR UPDATE";

                string nomeParticipante = null;
                string statusAtual = null;

                using (MySqlCommand selectCommand = new MySqlCommand(selectQuery, connection, transaction))
                {
                    selectCommand.Parameters.AddWithValue("@id", idInscricaoEvento);
                    selectCommand.CommandTimeout = 30; // Timeout para evitar deadlock

                    using (MySqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            nomeParticipante = reader["nm_participante"].ToString();
                            statusAtual = reader["cd_status"].ToString();
                        }
                        else
                        {
                            throw new InscricaoIndisponivelException(idInscricaoEvento, "(não encontrada)", "Inexistente");
                        }
                    }
                }

                if (!string.Equals(statusAtual, InscricaoEvento.StatusPendente, StringComparison.OrdinalIgnoreCase))
                {
                    throw new InscricaoIndisponivelException(idInscricaoEvento, nomeParticipante, statusAtual);
                }

                string updateQuery = "UPDATE INSCRICAO_EVENTO " +
                                     "SET cd_status = @statusRetirado, dt_retirada = NOW() " +
                                     "WHERE id_inscricao_evento = @id AND cd_status = @statusPendente";

                using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection, transaction))
                {
                    updateCommand.Parameters.AddWithValue("@statusRetirado", InscricaoEvento.StatusRetirado);
                    updateCommand.Parameters.AddWithValue("@statusPendente", InscricaoEvento.StatusPendente);
                    updateCommand.Parameters.AddWithValue("@id", idInscricaoEvento);
                    updateCommand.CommandTimeout = 30;

                    int rowsAffected = updateCommand.ExecuteNonQuery();
                    if (rowsAffected <= 0)
                    {                        
                        throw new InscricaoIndisponivelException(idInscricaoEvento, nomeParticipante, statusAtual);
                    }
                }
            }
            catch (InscricaoIndisponivelException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao validar e retirar inscrição: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Importa em lote as inscrições já deduplicadas/normalizadas pelo Service (uma linha por CPF).
        /// Para cada item: se já existe (id_evento, cpf) e está Pendente, atualiza nome/email/qtde;
        /// se já existe e está Retirado, ignora (evita corromper uma inscrição já utilizada); senão, insere.
        /// idsParaExcluir (opcional): registros Pendentes que o operador decidiu excluir por terem
        /// sumido da planilha nova (ver InscricaoEventoService.PrepararImportacao/ConfirmarImportacao).
        /// Executa tudo (insert/update + exclusões) em uma única transação.
        /// </summary>
        public (int inseridas, int atualizadas, int ignoradasJaRetiradas, int excluidas) ImportarLote(int idEvento, List<(string nome, string email, string cpfCnpj, string celular, int qtde)> itens, List<int> idsParaExcluir = null)
        {
            int inseridas = 0;
            int atualizadas = 0;
            int ignoradasJaRetiradas = 0;
            int excluidas = 0;

            if ((itens == null || itens.Count == 0) && (idsParaExcluir == null || idsParaExcluir.Count == 0))
                return (0, 0, 0, 0);

            MySqlConnection connection = null;
            MySqlTransaction transaction = null;

            try
            {
                connection = new MySqlConnection(_connectionString);
                connection.Open();
                transaction = connection.BeginTransaction();

                foreach (var item in itens ?? new List<(string, string, string, string, int)>())
                {
                    string statusExistente = null;
                    int idExistente = 0;

                    string checkQuery = "SELECT id_inscricao_evento, cd_status FROM INSCRICAO_EVENTO WHERE id_evento = @idEvento AND nr_cpf_cnpj = @cpf";
                    using (MySqlCommand checkCommand = new MySqlCommand(checkQuery, connection, transaction))
                    {
                        checkCommand.Parameters.AddWithValue("@idEvento", idEvento);
                        checkCommand.Parameters.AddWithValue("@cpf", item.cpfCnpj);

                        using (MySqlDataReader reader = checkCommand.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                idExistente = Convert.ToInt32(reader["id_inscricao_evento"]);
                                statusExistente = reader["cd_status"].ToString();
                            }
                        }
                    }

                    if (idExistente > 0)
                    {
                        if (!string.Equals(statusExistente, InscricaoEvento.StatusPendente, StringComparison.OrdinalIgnoreCase))
                        {
                            // Já foi retirada - não sobrescrever, apenas reportar como ignorada
                            ignoradasJaRetiradas++;
                            continue;
                        }

                        string updateQuery = "UPDATE INSCRICAO_EVENTO SET nm_participante = @nome, ds_email = @email, nr_celular = @celular, qtde_antecipada = @qtde, dt_criacao = NOW() WHERE id_inscricao_evento = @id";
                        using (MySqlCommand updateCommand = new MySqlCommand(updateQuery, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@nome", item.nome);
                            updateCommand.Parameters.AddWithValue("@email", (object)item.email ?? DBNull.Value);
                            updateCommand.Parameters.AddWithValue("@celular", (object)item.celular ?? DBNull.Value);
                            updateCommand.Parameters.AddWithValue("@qtde", item.qtde);
                            updateCommand.Parameters.AddWithValue("@id", idExistente);
                            updateCommand.ExecuteNonQuery();
                        }
                        atualizadas++;
                    }
                    else
                    {
                        string insertQuery = "INSERT INTO INSCRICAO_EVENTO (id_evento, nm_participante, ds_email, nr_cpf_cnpj, nr_celular, qtde_antecipada, cd_status) " +
                                             "VALUES (@idEvento, @nome, @email, @cpf, @celular, @qtde, @status)";
                        using (MySqlCommand insertCommand = new MySqlCommand(insertQuery, connection, transaction))
                        {
                            insertCommand.Parameters.AddWithValue("@idEvento", idEvento);
                            insertCommand.Parameters.AddWithValue("@nome", item.nome);
                            insertCommand.Parameters.AddWithValue("@email", (object)item.email ?? DBNull.Value);
                            insertCommand.Parameters.AddWithValue("@cpf", item.cpfCnpj);
                            insertCommand.Parameters.AddWithValue("@celular", (object)item.celular ?? DBNull.Value);
                            insertCommand.Parameters.AddWithValue("@qtde", item.qtde);
                            insertCommand.Parameters.AddWithValue("@status", InscricaoEvento.StatusPendente);
                            insertCommand.ExecuteNonQuery();
                        }
                        inseridas++;
                    }
                }

                // Exclusões decididas pelo operador (registros que sumiram da planilha nova) - guarda extra
                // por cd_status='Pendente': nunca exclui um Retirado, e um Pendente nunca tem VENDA
                // referenciando-o (id_inscricao_evento só é setado quando o status vira Retirado), então
                // não há risco de violar a FK fk_venda_inscricao_evento.
                if (idsParaExcluir != null)
                {
                    foreach (var id in idsParaExcluir)
                    {
                        string deleteQuery = "DELETE FROM INSCRICAO_EVENTO WHERE id_inscricao_evento = @id AND id_evento = @idEvento AND cd_status = @statusPendente";
                        using (MySqlCommand deleteCommand = new MySqlCommand(deleteQuery, connection, transaction))
                        {
                            deleteCommand.Parameters.AddWithValue("@id", id);
                            deleteCommand.Parameters.AddWithValue("@idEvento", idEvento);
                            deleteCommand.Parameters.AddWithValue("@statusPendente", InscricaoEvento.StatusPendente);
                            excluidas += deleteCommand.ExecuteNonQuery();
                        }
                    }
                }

                transaction.Commit();
                return (inseridas, atualizadas, ignoradasJaRetiradas, excluidas);
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    try { transaction.Rollback(); } catch { }
                }
                throw new Exception($"Erro ao importar inscrições: {ex.Message}", ex);
            }
            finally
            {
                if (connection != null)
                {
                    try { connection.Close(); connection.Dispose(); } catch { }
                }
            }
        }

        private InscricaoEvento MapearInscricao(MySqlDataReader reader)
        {
            return new InscricaoEvento
            {
                Id = Convert.ToInt32(reader["id_inscricao_evento"]),
                IdEvento = Convert.ToInt32(reader["id_evento"]),
                NomeParticipante = reader["nm_participante"].ToString(),
                Email = reader["ds_email"] == DBNull.Value ? null : reader["ds_email"].ToString(),
                CpfCnpj = reader["nr_cpf_cnpj"].ToString(),
                Celular = reader["nr_celular"] == DBNull.Value ? null : reader["nr_celular"].ToString(),
                QtdeAntecipada = Convert.ToInt32(reader["qtde_antecipada"]),
                CdStatus = reader["cd_status"].ToString(),
                DtCriacao = Convert.ToDateTime(reader["dt_criacao"]),
                DtRetirada = reader["dt_retirada"] == DBNull.Value ? (DateTime?)null : Convert.ToDateTime(reader["dt_retirada"])
            };
        }
    }
}
