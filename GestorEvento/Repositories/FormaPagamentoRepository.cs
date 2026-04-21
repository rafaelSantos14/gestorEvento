using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;

namespace GestorEvento.Repositories
{
    public class FormaPagamentoRepository
    {
        private readonly string _connectionString;

        public FormaPagamentoRepository()
        {
            _connectionString = Connection.GetConnection();
        }

        /// <summary>
        /// Obtém todas as formas de pagamento ativas
        /// </summary>
        public List<FormaPagamento> GetAllFormasPagamento()
        {
            var formas = new List<FormaPagamento>();

            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_forma_pagamento, nm_forma_pagamento, cd_forma_pagamento, fl_ativo FROM FORMA_PAGAMENTO WHERE fl_ativo = 'SIM'";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var forma = new FormaPagamento
                                {
                                    Id = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    NmFormaPagamento = reader["nm_forma_pagamento"].ToString(),
                                    CdFormaPagamento = reader["cd_forma_pagamento"].ToString(),
                                    FlAtivo = reader["fl_ativo"].ToString()
                                };
                                formas.Add(forma);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter formas de pagamento: {ex.Message}");
            }

            return formas;
        }

        /// <summary>
        /// Obtém uma forma de pagamento por ID
        /// </summary>
        public FormaPagamento GetFormaPagamentoById(int id)
        {
            try
            {
                using (MySqlConnection connection = new MySqlConnection(_connectionString))
                {
                    connection.Open();

                    string query = "SELECT id_forma_pagamento, nm_forma_pagamento, cd_forma_pagamento, fl_ativo FROM FORMA_PAGAMENTO WHERE id_forma_pagamento = @id";

                    using (MySqlCommand command = new MySqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@id", id);

                        using (MySqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                return new FormaPagamento
                                {
                                    Id = Convert.ToInt32(reader["id_forma_pagamento"]),
                                    NmFormaPagamento = reader["nm_forma_pagamento"].ToString(),
                                    CdFormaPagamento = reader["cd_forma_pagamento"].ToString(),
                                    FlAtivo = reader["fl_ativo"].ToString()
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter forma de pagamento: {ex.Message}");
            }

            return null;
        }
    }
}
