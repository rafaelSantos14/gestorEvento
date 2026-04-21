using System;
using System.Collections.Generic;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Views;

namespace GestorEvento.Services
{
    public class EventoService
    {
        private readonly EventoRepository _repository;

        public EventoService()
        {
            _repository = new EventoRepository();
        }

        /// <summary>
        /// Obtém todos os eventos
        /// </summary>
        public List<Evento> GetAllEventos()
        {
            try
            {
                return _repository.GetAllEventos();
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao obter todos os eventos: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao obter todos os eventos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return new List<Evento>();
            }
        }

        /// <summary>
        /// Obtém um evento por ID
        /// </summary>
        public Evento GetEventoById(int id)
        {
            if (id <= 0)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "ID do evento inválido", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("ID do evento inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return null;
            }

            try
            {
                return _repository.GetEventoById(id);
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao obter evento por ID: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao obter evento por ID: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return null;
            }
        }

        /// <summary>
        /// Cria um novo evento com validações
        /// </summary>
        public bool CreateEvento(Evento evento)
        {
            // Validações
            if (evento == null)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Evento não pode ser nulo", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Evento não pode ser nulo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (string.IsNullOrWhiteSpace(evento.Nome))
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Nome do evento não pode ser vazio", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Nome do evento não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (evento.Nome.Length > 255)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Nome do evento não pode ter mais de 255 caracteres", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Nome do evento não pode ter mais de 255 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (evento.DataEvento == null || evento.DataEvento == default(DateTime))
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Data do evento é obrigatória", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Data do evento é obrigatória", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            try
            {
                return _repository.CreateEvento(evento);
            }
            catch (MySqlException mySqlEx)
            {
                // Erro 1062 = Duplicate Entry (chave única violada)
                if (mySqlEx.Number == 1062)
                {
                    try
                    {
                        var dialogo = new DialogoCustomizado("Aviso", "Já existe um evento com esse nome na mesma data. Por favor, escolha outro nome ou data.", TipoDialogo.Aviso, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show("Já existe um evento com esse nome na mesma data. Por favor, escolha outro nome ou data.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    try
                    {
                        var dialogo = new DialogoCustomizado("Erro", $"Erro ao criar evento: {mySqlEx.Message}", TipoDialogo.Erro, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show($"Erro ao criar evento: {mySqlEx.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao criar evento: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao criar evento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }

        /// <summary>
        /// Atualiza um evento existente com validações
        /// </summary>
        public bool UpdateEvento(Evento evento)
        {
            // Validações
            if (evento == null)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Evento não pode ser nulo", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Evento não pode ser nulo", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (evento.Id <= 0)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "ID do evento inválido", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("ID do evento inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (string.IsNullOrWhiteSpace(evento.Nome))
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Nome do evento não pode ser vazio", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Nome do evento não pode ser vazio", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (evento.Nome.Length > 255)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Nome do evento não pode ter mais de 255 caracteres", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Nome do evento não pode ter mais de 255 caracteres", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            if (evento.DataEvento == null || evento.DataEvento == default(DateTime))
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Data do evento é obrigatória", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Data do evento é obrigatória", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            try
            {
                return _repository.UpdateEvento(evento);
            }
            catch (MySqlException mySqlEx)
            {
                // Erro 1062 = Duplicate Entry (chave única violada)
                if (mySqlEx.Number == 1062)
                {
                    try
                    {
                        var dialogo = new DialogoCustomizado("Aviso", "Já existe um evento com esse nome na mesma data. Por favor, escolha outro nome ou data.", TipoDialogo.Aviso, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show("Já existe um evento com esse nome na mesma data. Por favor, escolha outro nome ou data.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                else
                {
                    try
                    {
                        var dialogo = new DialogoCustomizado("Erro", $"Erro ao atualizar evento: {mySqlEx.Message}", TipoDialogo.Erro, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch
                    {
                        MessageBox.Show($"Erro ao atualizar evento: {mySqlEx.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                return false;
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao atualizar evento: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao atualizar evento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }

        /// <summary>
        /// Deleta um evento por ID
        /// </summary>
        public bool DeleteEvento(int id)
        {
            if (id <= 0)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "ID do evento inválido", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("ID do evento inválido", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return false;
            }

            try
            {
                return _repository.DeleteEvento(id);
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao deletar evento: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao deletar evento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return false;
            }
        }

        /// <summary>
        /// Busca eventos por nome
        /// </summary>
        public List<Evento> SearchEventos(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Digite um nome para buscar", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Digite um nome para buscar", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return new List<Evento>();
            }

            try
            {
                return _repository.SearchEventos(nome);
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao buscar eventos: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao buscar eventos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return new List<Evento>();
            }
        }

        /// <summary>
        /// Busca eventos por nome e/ou data
        /// </summary>
        public List<Evento> SearchEventosByNameAndDate(string nome, DateTime? data)
        {
            // Se ambos estão vazios, avisar
            if (string.IsNullOrWhiteSpace(nome) && !data.HasValue)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Aviso", "Preencha ao menos um filtro (nome ou data)", TipoDialogo.Aviso, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show("Preencha ao menos um filtro (nome ou data)", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                return new List<Evento>();
            }

            try
            {
                return _repository.SearchEventosByNameAndDate(nome, data);
            }
            catch (Exception ex)
            {
                try
                {
                    var dialogo = new DialogoCustomizado("Erro", $"Erro ao buscar eventos: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                    dialogo.ShowDialog();
                }
                catch
                {
                    MessageBox.Show($"Erro ao buscar eventos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return new List<Evento>();
            }
        }
    }
}
