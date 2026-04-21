using System;
using System.Collections.Generic;
using System.Diagnostics;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Repositories;

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

                    string query = "SELECT id_evento, nm_evento, dt_evento FROM EVENTO ORDER BY dt_evento DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var evento = new Evento
                                {
                                    Id = Convert.ToInt32(reader["id_evento"]),
                                    Nome = reader["nm_evento"].ToString(),
                                    DataEvento = Convert.ToDateTime(reader["dt_evento"])
                                };
                                eventos.Add(evento);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter eventos: {ex.Message}");
                throw;
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

                    string query = "SELECT id_evento, nm_evento, dt_evento FROM EVENTO WHERE id_evento = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                var evento = new Evento
                                {
                                    Id = Convert.ToInt32(reader["id_evento"]),
                                    Nome = reader["nm_evento"].ToString(),
                                    DataEvento = Convert.ToDateTime(reader["dt_evento"])
                                };

                                connection.Close();
                                return evento;
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao obter evento por ID: {ex.Message}");
                throw;
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

                    string query = "INSERT INTO EVENTO (nm_evento, dt_evento) VALUES (@nome, @data)";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", evento.Nome);
                        command.Parameters.AddWithValue("@data", evento.DataEvento);

                        command.ExecuteNonQuery();
                    }

                    connection.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao criar evento: {ex.Message}");
                throw;
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

                    connection.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao atualizar evento: {ex.Message}");
                throw;
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

                    connection.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao deletar evento: {ex.Message}");
                throw;
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

                    string query = "SELECT id_evento, nm_evento, dt_evento FROM EVENTO WHERE nm_evento LIKE @nome ORDER BY dt_evento DESC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", $"%{nome}%");

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var evento = new Evento
                                {
                                    Id = Convert.ToInt32(reader["id_evento"]),
                                    Nome = reader["nm_evento"].ToString(),
                                    DataEvento = Convert.ToDateTime(reader["dt_evento"])
                                };
                                eventos.Add(evento);
                            }
                        }
                    }

                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar eventos: {ex.Message}");
                throw;
            }

            return eventos;
        }

        /// <summary>
        /// Busca eventos por nome e/ou data
        /// </summary>
        public List<Evento> SearchEventosByNameAndDate(string nome, DateTime? data)
        {
            var eventos = new List<Evento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_evento, nm_evento, dt_evento FROM EVENTO WHERE 1=1";
                    
                    // Adiciona filtro de nome se preenchido
                    if (!string.IsNullOrWhiteSpace(nome))
                    {
                        query += " AND UPPER(nm_evento) LIKE UPPER(@nome)";
                    }
                    
                    // Adiciona filtro de data se preenchido
                    if (data.HasValue)
                    {
                        query += " AND DATE(dt_evento) = @data";
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

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var evento = new Evento
                                {
                                    Id = Convert.ToInt32(reader["id_evento"]),
                                    Nome = reader["nm_evento"].ToString(),
                                    DataEvento = Convert.ToDateTime(reader["dt_evento"])
                                };
                                eventos.Add(evento);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Erro ao buscar eventos: {ex.Message}");
                throw;
            }

            return eventos;
        }
    }
}
