using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;

namespace GestorEvento.Services
{
    public class PontoVendaService
    {
        private readonly PontoVendaRepository _repository;

        public PontoVendaService()
        {
            _repository = new PontoVendaRepository();
        }

        /// <summary>
        /// Abre um novo ponto de venda (caixa) para um evento
        /// </summary>
        public int AbrirPontoVenda(int eventoId, decimal valorInicial, string descricao = null)
        {
            try
            {
                if (eventoId <= 0)
                    throw new ArgumentException("ID do evento inválido");

                if (valorInicial < 0)
                    throw new ArgumentException("Valor inicial não pode ser negativo");

                return _repository.AbrirPontoVenda(eventoId, valorInicial, descricao);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao abrir ponto de venda: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém um ponto de venda por ID
        /// </summary>
        public PontoVenda GetPontoVendaById(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                return _repository.GetPontoVendaById(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter ponto de venda: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Obtém todos os caixas abertos de um evento
        /// </summary>
        public List<PontoVenda> GetCaixasAbertas(int eventoId)
        {
            try
            {
                if (eventoId <= 0)
                    throw new ArgumentException("ID do evento inválido");

                return _repository.GetCaixasAbertas(eventoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao obter caixas abertas: {ex.Message}", ex);
            }
        }

        /// <summary>
        /// Fecha um ponto de venda (caixa)
        /// </summary>
        public bool FecharPontoVenda(int id, decimal valorFinal, string observacoes)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID do ponto de venda inválido");

                if (valorFinal < 0)
                    throw new ArgumentException("Valor final não pode ser negativo");

                return _repository.FecharPontoVenda(id, valorFinal, observacoes);
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao fechar ponto de venda: {ex.Message}", ex);
            }
        }
    }
}
