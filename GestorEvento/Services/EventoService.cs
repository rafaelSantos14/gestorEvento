using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

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
                UiHelper.ExibirErro("Erro", $"Erro ao obter todos os eventos: {ex.Message}");
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
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return null;
            }

            try
            {
                return _repository.GetEventoById(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter evento por ID: {ex.Message}");
                return null;
            }
        }

        public bool EventoEstaEncerrado(int eventoId)
        {
            var evento = GetEventoById(eventoId);
            if (evento == null)
            {
                return false;
            }

            return evento.IsEncerrado;
        }

        public bool EncerrarEvento(int eventoId)
        {
            if (eventoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return false;
            }

            try
            {
                var evento = _repository.GetEventoById(eventoId);
                if (evento == null)
                {
                    UiHelper.ExibirAviso("Aviso", "Evento não encontrado.");
                    return false;
                }

                if (evento.IsEncerrado)
                {
                    UiHelper.ExibirInfo("Informação", "Este evento já está encerrado.");
                    return false;
                }

                int qtdeCaixasAbertos = _repository.GetQtdeCaixasAbertos(eventoId);
                if (qtdeCaixasAbertos > 0)
                {
                    UiHelper.ExibirAviso("Aviso", "Não é possível encerrar o evento porque existem caixas abertos.");
                    return false;
                }

                bool sucesso = _repository.EncerrarEvento(eventoId);
                if (sucesso)
                {
                    UiHelper.ExibirSucesso("Sucesso", "Evento encerrado com sucesso!");
                }

                return sucesso;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao encerrar evento: {ex.Message}");
                return false;
            }
        }

        public bool ReabrirEvento(int eventoId)
        {
            if (eventoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return false;
            }

            try
            {
                var evento = _repository.GetEventoById(eventoId);
                if (evento == null)
                {
                    UiHelper.ExibirAviso("Aviso", "Evento não encontrado.");
                    return false;
                }

                if (!evento.IsEncerrado)
                {
                    UiHelper.ExibirInfo("Informação", "Este evento já está ativo.");
                    return false;
                }

                bool sucesso = _repository.ReabrirEvento(eventoId);
                if (sucesso)
                {
                    UiHelper.ExibirSucesso("Sucesso", "Evento reaberto com sucesso!");
                }

                return sucesso;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao reabrir evento: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Cria um novo evento com validações
        /// </summary>
        public bool CreateEvento(Evento evento)
        {
            if (evento == null)
            {
                UiHelper.ExibirAviso("Aviso", "Evento não pode ser nulo");
                return false;
            }

            if (string.IsNullOrWhiteSpace(evento.Nome))
            {
                UiHelper.ExibirAviso("Aviso", "Nome do evento não pode ser vazio");
                return false;
            }

            if (evento.Nome.Length > 255)
            {
                UiHelper.ExibirAviso("Aviso", "Nome do evento não pode ter mais de 255 caracteres");
                return false;
            }

            if (evento.DataEvento == default(DateTime))
            {
                UiHelper.ExibirAviso("Aviso", "Data do evento é obrigatória");
                return false;
            }

            if (string.IsNullOrWhiteSpace(evento.CdStatus))
            {
                evento.CdStatus = Evento.StatusAtivo;
            }

            try
            {
                return _repository.CreateEvento(evento);
            }
            catch (MySqlException mySqlEx)
            {
                if (mySqlEx.Number == 1062)
                {
                    UiHelper.ExibirAviso("Aviso", "Já existe um evento com esse nome na mesma data. Por favor, escolha outro nome ou data.");
                }
                else
                {
                    UiHelper.ExibirErro("Erro", $"Erro ao criar evento: {mySqlEx.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao criar evento: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Atualiza um evento existente com validações
        /// </summary>
        public bool UpdateEvento(Evento evento)
        {
            if (evento == null)
            {
                UiHelper.ExibirAviso("Aviso", "Evento não pode ser nulo");
                return false;
            }

            if (evento.Id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return false;
            }

            if (EventoEstaEncerrado(evento.Id))
            {
                UiHelper.ExibirAviso("Aviso", "Evento encerrado não pode ser editado.");
                return false;
            }

            if (string.IsNullOrWhiteSpace(evento.Nome))
            {
                UiHelper.ExibirAviso("Aviso", "Nome do evento não pode ser vazio");
                return false;
            }

            if (evento.Nome.Length > 255)
            {
                UiHelper.ExibirAviso("Aviso", "Nome do evento não pode ter mais de 255 caracteres");
                return false;
            }

            if (evento.DataEvento == default(DateTime))
            {
                UiHelper.ExibirAviso("Aviso", "Data do evento é obrigatória");
                return false;
            }

            try
            {
                return _repository.UpdateEvento(evento);
            }
            catch (MySqlException mySqlEx)
            {
                if (mySqlEx.Number == 1062)
                {
                    UiHelper.ExibirAviso("Aviso", "Já existe um evento com esse nome na mesma data. Por favor, escolha outro nome ou data.");
                }
                else
                {
                    UiHelper.ExibirErro("Erro", $"Erro ao atualizar evento: {mySqlEx.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao atualizar evento: {ex.Message}");
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
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return false;
            }

            try
            {
                return _repository.DeleteEvento(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao deletar evento: {ex.Message}");
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
                UiHelper.ExibirAviso("Aviso", "Digite um nome para buscar");
                return new List<Evento>();
            }

            try
            {
                return _repository.SearchEventos(nome);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao buscar eventos: {ex.Message}");
                return new List<Evento>();
            }
        }

        /// <summary>
        /// Busca eventos por nome e/ou data e/ou status
        /// </summary>
        public List<Evento> SearchEventosByNameDateAndStatus(string nome, DateTime? data, string status = null)
        {
            bool semNome = string.IsNullOrWhiteSpace(nome);
            bool semData = !data.HasValue;
            bool semStatusUtil = string.IsNullOrWhiteSpace(status) || string.Equals(status, "Todos", StringComparison.OrdinalIgnoreCase);

            if (semNome && semData && semStatusUtil)
            {
                UiHelper.ExibirAviso("Aviso", "Preencha ao menos um filtro (nome, data ou status)");
                return new List<Evento>();
            }

            try
            {
                return _repository.SearchEventosByNameDateAndStatus(nome, data, status);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao buscar eventos: {ex.Message}");
                return new List<Evento>();
            }
        }
    }
}
