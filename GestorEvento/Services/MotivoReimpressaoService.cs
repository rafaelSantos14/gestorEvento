using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;

namespace GestorEvento.Services
{
    public class MotivoReimpressaoService
    {
        private MotivoReimpressaoRepository _repository;

        public MotivoReimpressaoService()
        {
            _repository = new MotivoReimpressaoRepository();
        }

        /// <summary>
        /// Obtém todos os motivos de reimpressão ativos
        /// </summary>
        public List<MotivoReimpressao> GetMotivosAtivos()
        {
            try
            {
                return _repository.GetAllMotivos();
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter motivos: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém um motivo específico pelo ID
        /// </summary>
        public MotivoReimpressao GetMotivoById(int idMotivo)
        {
            try
            {
                return _repository.GetMotivoById(idMotivo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter motivo: {ex.Message}", ex);
            }
        }
    }
}
