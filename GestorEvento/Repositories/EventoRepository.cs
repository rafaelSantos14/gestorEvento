using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class EventoRepository
    {
        private readonly string _connectionString;

        public EventoRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Obtém todos os eventos
        /// </summary>
        public List<Evento> GetAllEventos()
        {
            var eventos = new List<Evento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_evento, nm_evento, dt_evento,
                                            IFNULL(cd_status, 'Ativo') AS cd_status,
                                            dt_encerramento
                                     FROM EVENTO
                                     ORDER BY dt_evento DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            eventos.Add(MapEvento(reader));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter eventos: {ex.Message}", ex);
            }

            return eventos;
        }

        /// <summary>
        /// Obtém um evento por ID
        /// </summary>
        public Evento GetEventoById(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_evento, nm_evento, dt_evento,
                                            IFNULL(cd_status, 'Ativo') AS cd_status,
                                            dt_encerramento
                                     FROM EVENTO
                                     WHERE id_evento = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return MapEvento(reader);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter evento por ID: {ex.Message}", ex);
            }

            return null;
        }

        /// <summary>
        /// Cria um novo evento
        /// </summary>
        public bool CreateEvento(Evento evento)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "INSERT INTO EVENTO (nm_evento, dt_evento, cd_status, dt_encerramento) VALUES (@nome, @data, @status, @dtEncerramento)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", evento.Nome);
                        command.Parameters.AddWithValue("@data", evento.DataEvento);
                        command.Parameters.AddWithValue("@status", string.IsNullOrWhiteSpace(evento.CdStatus) ? Evento.StatusAtivo : evento.CdStatus);
                        command.Parameters.AddWithValue("@dtEncerramento", (object)evento.DtEncerramento ?? DBNull.Value);

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar evento: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Atualiza um evento existente
        /// </summary>
        public bool UpdateEvento(Evento evento)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "UPDATE EVENTO SET nm_evento = @nome, dt_evento = @data WHERE id_evento = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", evento.Nome);
                        command.Parameters.AddWithValue("@data", evento.DataEvento);
                        command.Parameters.AddWithValue("@id", evento.Id);

                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar evento: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Deleta um evento por ID
        /// </summary>
        public bool DeleteEvento(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "DELETE FROM EVENTO WHERE id_evento = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao deletar evento: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Busca eventos por nome
        /// </summary>
        public List<Evento> SearchEventos(string nome)
        {
            var eventos = new List<Evento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_evento, nm_evento, dt_evento,
                                            IFNULL(cd_status, 'Ativo') AS cd_status,
                                            dt_encerramento
                                     FROM EVENTO
                                     WHERE UPPER(nm_evento) LIKE UPPER(@nome)
                                     ORDER BY dt_evento DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", $"%{nome}%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventos.Add(MapEvento(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar eventos: {ex.Message}", ex);
            }

            return eventos;
        }

        /// <summary>
        /// Busca eventos por nome e/ou data e/ou status
        /// </summary>
        public List<Evento> SearchEventosByNameDateAndStatus(string nome, DateTime? data, string status = null)
        {
            var eventos = new List<Evento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT id_evento, nm_evento, dt_evento,
                                            IFNULL(cd_status, 'Ativo') AS cd_status,
                                            dt_encerramento
                                     FROM EVENTO
                                     WHERE 1=1";

                    if (!string.IsNullOrWhiteSpace(nome))
                    {
                        query += " AND UPPER(nm_evento) LIKE UPPER(@nome)";
                    }

                    if (data.HasValue)
                    {
                        query += " AND DATE(dt_evento) = @data";
                    }

                    if (!string.IsNullOrWhiteSpace(status) && !string.Equals(status, "Todos", StringComparison.OrdinalIgnoreCase))
                    {
                        query += " AND IFNULL(cd_status, 'Ativo') = @status";
                    }

                    query += " ORDER BY dt_evento DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        if (!string.IsNullOrWhiteSpace(nome))
                        {
                            command.Parameters.AddWithValue("@nome", $"%{nome}%");
                        }

                        if (data.HasValue)
                        {
                            command.Parameters.AddWithValue("@data", data.Value.Date);
                        }

                        if (!string.IsNullOrWhiteSpace(status) && !string.Equals(status, "Todos", StringComparison.OrdinalIgnoreCase))
                        {
                            command.Parameters.AddWithValue("@status", status);
                        }

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                eventos.Add(MapEvento(reader));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao buscar eventos: {ex.Message}", ex);
            }

            return eventos;
        }

        public int GetQtdeCaixasAbertos(int eventoId)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"SELECT COUNT(1)
                                     FROM PONTO_VENDA
                                     WHERE id_evento = @eventoId
                                       AND cd_status = 'Aberto'";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@eventoId", eventoId);
                        return Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao validar caixas abertos do evento: {ex.Message}", ex);
            }
        }

        public bool UpdateStatusEvento(int eventoId, string novoStatus, DateTime? dtEncerramento)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = @"UPDATE EVENTO
                                     SET cd_status = @status,
                                         dt_encerramento = @dtEncerramento
                                     WHERE id_evento = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@status", novoStatus);
                        command.Parameters.AddWithValue("@dtEncerramento", (object)dtEncerramento ?? DBNull.Value);
                        command.Parameters.AddWithValue("@id", eventoId);

                        return command.ExecuteNonQuery() > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar status do evento: {ex.Message}", ex);
            }
        }

        public bool EncerrarEvento(int eventoId)
        {
            return UpdateStatusEvento(eventoId, Evento.StatusEncerrado, DateTime.Now);
        }

        public bool ReabrirEvento(int eventoId)
        {
            return UpdateStatusEvento(eventoId, Evento.StatusAtivo, null);
        }

        private Evento MapEvento(MySqlDataReader reader)
        {
            return new Evento
            {
                Id = Convert.ToInt32(reader["id_evento"]),
                Nome = reader["nm_evento"].ToString(),
                DataEvento = Convert.ToDateTime(reader["dt_evento"]),
                CdStatus = reader["cd_status"].ToString(),
                DtEncerramento = reader["dt_encerramento"] != DBNull.Value
                    ? Convert.ToDateTime(reader["dt_encerramento"])
                    : (DateTime?)null
            };
        }
    }
}
