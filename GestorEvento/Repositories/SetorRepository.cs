using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class SetorRepository
    {
        private readonly string _connectionString;

        public SetorRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Obtém todos os setores (ativos e inativos), para a tela de cadastro
        /// </summary>
        public List<Setor> GetAll()
        {
            var setores = new List<Setor>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_setor, nm_setor, fl_ativo FROM SETOR ORDER BY nm_setor ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            setores.Add(new Setor
                            {
                                IdSetor = reader.GetInt32("id_setor"),
                                NmSetor = reader.GetString("nm_setor"),
                                FlAtivo = reader["fl_ativo"].ToString()
                            });
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter todos os setores: {ex.Message}", ex);
            }

            return setores;
        }

        /// <summary>
        /// Obtém apenas os setores ativos, para seleção no PDV (identificação da cortesia)
        /// </summary>
        public List<Setor> GetAtivos()
        {
            var setores = new List<Setor>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_setor, nm_setor, fl_ativo FROM SETOR WHERE fl_ativo = 'SIM' ORDER BY nm_setor ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            setores.Add(new Setor
                            {
                                IdSetor = reader.GetInt32("id_setor"),
                                NmSetor = reader.GetString("nm_setor"),
                                FlAtivo = reader["fl_ativo"].ToString()
                            });
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter setores ativos: {ex.Message}", ex);
            }

            return setores;
        }

        /// <summary>
        /// Obtém um setor específico por ID
        /// </summary>
        public Setor GetById(int id)
        {
            Setor setor = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_setor, nm_setor, fl_ativo FROM SETOR WHERE id_setor = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                setor = new Setor
                                {
                                    IdSetor = reader.GetInt32("id_setor"),
                                    NmSetor = reader.GetString("nm_setor"),
                                    FlAtivo = reader["fl_ativo"].ToString()
                                };
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter setor por ID: {ex.Message}", ex);
            }

            return setor;
        }

        /// <summary>
        /// Cria um novo setor
        /// </summary>
        public bool Create(Setor setor)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "INSERT INTO SETOR (nm_setor, fl_ativo) VALUES (@nome, 'SIM')";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", setor.NmSetor);
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao criar setor: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Atualiza o nome de um setor existente
        /// </summary>
        public bool Update(Setor setor)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE SETOR SET nm_setor = @nome WHERE id_setor = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nome", setor.NmSetor);
                        command.Parameters.AddWithValue("@id", setor.IdSetor);
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao atualizar setor: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Inativa um setor (fl_ativo = 'NAO'). Não é exclusão física: setores podem estar
        /// referenciados em vendas de cortesia já registradas
        /// </summary>
        public bool Inativar(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE SETOR SET fl_ativo = 'NAO' WHERE id_setor = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao inativar setor: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Reativa um setor previamente inativado (fl_ativo = 'SIM')
        /// </summary>
        public bool Reativar(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "UPDATE SETOR SET fl_ativo = 'SIM' WHERE id_setor = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);
                        command.ExecuteNonQuery();
                        connection.Close();
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao reativar setor: {ex.Message}", ex);
            }
        }
    }
}
