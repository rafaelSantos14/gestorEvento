using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    public class SetorService
    {
        private readonly SetorRepository _repository;

        public SetorService()
        {
            _repository = new SetorRepository();
        }

        /// <summary>
        /// Obtém todos os setores (ativos e inativos), para a tela de cadastro
        /// </summary>
        public List<Setor> GetAll()
        {
            try
            {
                return _repository.GetAll();
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter setores: {ex.Message}");
                return new List<Setor>();
            }
        }

        /// <summary>
        /// Obtém os setores ativos, para seleção na tela de identificação da cortesia
        /// </summary>
        public List<Setor> GetSetoresAtivos()
        {
            try
            {
                return _repository.GetAtivos();
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter setores ativos: {ex.Message}");
                return new List<Setor>();
            }
        }

        /// <summary>
        /// Obtém um setor por ID
        /// </summary>
        public Setor GetById(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do setor inválido");
                return null;
            }

            try
            {
                return _repository.GetById(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter setor por ID: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Cria um novo setor com validações
        /// </summary>
        public bool Create(Setor setor)
        {
            if (setor == null || string.IsNullOrWhiteSpace(setor.NmSetor))
            {
                UiHelper.ExibirAviso("Aviso", "Nome do setor não pode ser vazio");
                return false;
            }

            if (setor.NmSetor.Length > 50)
            {
                UiHelper.ExibirAviso("Aviso", "Nome do setor não pode ter mais de 50 caracteres");
                return false;
            }

            try
            {
                return _repository.Create(setor);
            }
            catch (MySqlException mySqlEx)
            {
                if (mySqlEx.Number == 1062)
                {
                    UiHelper.ExibirAviso("Aviso", "Já existe um setor com esse nome. Por favor, escolha outro nome.");
                }
                else
                {
                    UiHelper.ExibirErro("Erro", $"Erro ao criar setor: {mySqlEx.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao criar setor: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Atualiza um setor existente com validações
        /// </summary>
        public bool Update(Setor setor)
        {
            if (setor == null || setor.IdSetor <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "Setor inválido");
                return false;
            }

            if (string.IsNullOrWhiteSpace(setor.NmSetor))
            {
                UiHelper.ExibirAviso("Aviso", "Nome do setor não pode ser vazio");
                return false;
            }

            if (setor.NmSetor.Length > 50)
            {
                UiHelper.ExibirAviso("Aviso", "Nome do setor não pode ter mais de 50 caracteres");
                return false;
            }

            try
            {
                return _repository.Update(setor);
            }
            catch (MySqlException mySqlEx)
            {
                if (mySqlEx.Number == 1062)
                {
                    UiHelper.ExibirAviso("Aviso", "Já existe um setor com esse nome. Por favor, escolha outro nome.");
                }
                else
                {
                    UiHelper.ExibirErro("Erro", $"Erro ao atualizar setor: {mySqlEx.Message}");
                }
                return false;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao atualizar setor: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Inativa um setor por ID (sem exclusão física)
        /// </summary>
        public bool Inativar(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do setor inválido");
                return false;
            }

            try
            {
                return _repository.Inativar(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao inativar setor: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Reativa um setor por ID
        /// </summary>
        public bool Reativar(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do setor inválido");
                return false;
            }

            try
            {
                return _repository.Reativar(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao reativar setor: {ex.Message}");
                return false;
            }
        }
    }
}
