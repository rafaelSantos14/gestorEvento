using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class MotivoReimpressaoRepository
    {
        private string _connectionString;

        public MotivoReimpressaoRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Obtém todos os motivos de reimpressão ativos
        /// </summary>
        public List<MotivoReimpressao> GetAllMotivos()
        {
            var motivos = new List<MotivoReimpressao>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_motivo, cd_motivo, ds_motivo, fl_ativo FROM MOTIVOS_REIMPRESSAO WHERE fl_ativo = TRUE ORDER BY ds_motivo ASC";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var motivo = new MotivoReimpressao
                                {
                                    IdMotivo = reader.GetInt32("id_motivo"),
                                    CdMotivo = reader.GetString("cd_motivo"),
                                    DsMotivo = reader.GetString("ds_motivo"),
                                    FlAtivo = reader.GetBoolean("fl_ativo")
                                };
                                motivos.Add(motivo);
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter motivos de reimpressão: {ex.Message}", ex);
            }

            return motivos;
        }

        /// <summary>
        /// Obtém um motivo específico por ID
        /// </summary>
        public MotivoReimpressao GetMotivoById(int idMotivo)
        {
            MotivoReimpressao motivo = null;

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();
                    string query = "SELECT id_motivo, cd_motivo, ds_motivo, fl_ativo FROM MOTIVOS_REIMPRESSAO WHERE id_motivo = @idMotivo";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@idMotivo", idMotivo);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                motivo = new MotivoReimpressao
                                {
                                    IdMotivo = reader.GetInt32("id_motivo"),
                                    CdMotivo = reader.GetString("cd_motivo"),
                                    DsMotivo = reader.GetString("ds_motivo"),
                                    FlAtivo = reader.GetBoolean("fl_ativo")
                                };
                            }
                        }
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter motivo de reimpressão: {ex.Message}", ex);
            }

            return motivo;
        }
    }
}
