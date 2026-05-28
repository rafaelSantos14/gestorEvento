using System;
using System.Collections.Generic;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    public class PontoVendaService
    {
        private readonly PontoVendaRepository _repository;
        private readonly VendaService _vendaService;
        private readonly RecebimentoService _recebimentoService;
        private readonly MovimentacaoService _movimentacaoService;
        private readonly EventoRepository _eventoRepository;

        public PontoVendaService()
        {
            _repository = new PontoVendaRepository();
            _vendaService = new VendaService();
            _recebimentoService = new RecebimentoService();
            _movimentacaoService = new MovimentacaoService();
            _eventoRepository = new EventoRepository();
        }

        /// <summary>
        /// Abre um novo ponto de venda (caixa) para um evento
        /// </summary>
        public int AbrirPontoVenda(int eventoId, decimal valorInicial, string descricao = null)
        {
            if (eventoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return 0;
            }

            if (valorInicial < 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor inicial não pode ser negativo");
                return 0;
            }

            try
            {
                var evento = _eventoRepository.GetEventoById(eventoId);
                if (evento == null)
                {
                    UiHelper.ExibirAviso("Aviso", "Evento não encontrado.");
                    return 0;
                }

                if (evento.IsEncerrado)
                {
                    UiHelper.ExibirAviso("Aviso", "Evento encerrado. Não é possível abrir caixa.");
                    return 0;
                }

                return _repository.AbrirPontoVenda(eventoId, valorInicial, descricao);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao abrir ponto de venda: {ex.Message}");
                return 0;
            }
        }

        /// <summary>
        /// Obtém um ponto de venda por ID
        /// </summary>
        public PontoVenda GetPontoVendaById(int id)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return null;
            }

            try
            {
                return _repository.GetPontoVendaById(id);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter ponto de venda: {ex.Message}");
                return null;
            }
        }

        /// <summary>
        /// Obtém todos os caixas abertos de um evento
        /// </summary>
        public List<PontoVenda> GetCaixasAbertas(int eventoId)
        {
            if (eventoId <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return new List<PontoVenda>();
            }

            try
            {
                return _repository.GetCaixasAbertas(eventoId);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter caixas abertas: {ex.Message}");
                return new List<PontoVenda>();
            }
        }

        /// <summary>
        /// Fecha um ponto de venda (caixa)
        /// </summary>
        public bool FecharPontoVenda(int id, decimal valorFinal, string observacoes)
        {
            if (id <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return false;
            }

            if (valorFinal < 0)
            {
                UiHelper.ExibirAviso("Aviso", "Valor final não pode ser negativo");
                return false;
            }

            try
            {
                return _repository.FecharPontoVenda(id, valorFinal, observacoes);
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao fechar ponto de venda: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Obtém resumo completo para fechamento de caixa (resumo executivo + detalhes)
        /// </summary>
        public ResumoFechamentoCaixa GetResumoFechamento(int idPontoVenda)
        {
            if (idPontoVenda <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do ponto de venda inválido");
                return null;
            }

            try
            {
                // Obter dados básicos do ponto de venda
                var pontoVenda = GetPontoVendaById(idPontoVenda);
                if (pontoVenda == null)
                {
                    UiHelper.ExibirAviso("Aviso", "Ponto de venda não encontrado");
                    return null;
                }

                // Calcular total de vendas em dinheiro
                decimal totalVendasDinheiro = _recebimentoService.GetTotalRecebimentoDinheiro(idPontoVenda);

                // Obter movimentações (troco, sangria, entrada)
                var movimentacoes = _movimentacaoService.GetMovimentacoesByPontoVenda(idPontoVenda);
                decimal totalTroco = _movimentacaoService.GetTotalMovimentacaoPorTipo(idPontoVenda, TipoMovimento.TROCO);
                decimal totalSangria = _movimentacaoService.GetTotalMovimentacaoPorTipo(idPontoVenda, TipoMovimento.SANGRIA);
                decimal totalEntradaTroco = _movimentacaoService.GetTotalMovimentacaoPorTipo(idPontoVenda, TipoMovimento.ENTRADA_TROCO);

                // Calcular total esperado com movimentações
                // TotalEsperado = VlInicial + DINHEIRO - TROCO - SANGRIA + ENTRADA_TROCO
                decimal totalEsperado = pontoVenda.VlInicial 
                    + totalVendasDinheiro 
                    - totalTroco 
                    - totalSangria 
                    + totalEntradaTroco;

                // Obter resumo por forma de pagamento
                var recebimentosPorForma = new List<ResumoRecebimentoPorForma>();
                foreach (var (idFormaPagamento, nomeFormaPagamento, totalRecebimento) in _recebimentoService.GetResumoRecebimentosByPontoVenda(idPontoVenda))
                {
                    recebimentosPorForma.Add(new ResumoRecebimentoPorForma
                    {
                        IdFormaPagamento = idFormaPagamento,
                        NomeFormaPagamento = nomeFormaPagamento,
                        TotalRecebimento = totalRecebimento
                    });
                }

                // Obter vendas simplificadas
                var vendas = new List<ResumoVendaFechamento>();
                foreach (var (idVenda, dtVenda, vlTotal) in _vendaService.GetResumoVendasByPontoVenda(idPontoVenda))
                {
                    // Obter a principal forma de pagamento da venda
                    var recebimentos = _recebimentoService.GetRecebimentosByVendaId(idVenda);
                    string nomeFormaPagamento = ""; // Default
                    if (recebimentos.Count > 0)
                    {
                        // Obter o primeiro/principal recebimento
                        var recebimento = recebimentos[0];
                        // TODO: Buscar o nome da forma de pagamento (pode ser necessário criar um method no FormaPagamentoService)
                    }

                    vendas.Add(new ResumoVendaFechamento
                    {
                        IdVenda = idVenda,
                        DtVenda = dtVenda,
                        VlTotal = vlTotal,
                        NomeFormaPagamento = nomeFormaPagamento
                    });
                }

                // Montar lista de movimentações detalhadas para exibição
                var movimentacoesDetalhadas = new List<MovimentacaoDetalhada>();
                foreach (var mov in movimentacoes)
                {
                    movimentacoesDetalhadas.Add(new MovimentacaoDetalhada
                    {
                        IdMovimentacao = mov.IdMovimentacao,
                        TipoMovimento = mov.TipoMovimento.ToString(),
                        VlMovimento = mov.VlMovimento,
                        DtMovimento = mov.DtMovimento,
                        Descricao = mov.Descricao
                    });
                }

                // Montar o DTO final
                return new ResumoFechamentoCaixa
                {
                    IdPontoVenda = idPontoVenda,
                    NoPontoVenda = pontoVenda.NoPontoVenda,
                    NomePontoVenda = pontoVenda.DsPontoVenda,
                    DtAbertura = pontoVenda.DtAbertura,
                    VlInicial = pontoVenda.VlInicial,
                    TotalVendasDinheiro = totalVendasDinheiro,
                    TotalEsperado = totalEsperado,
                    RecebimentosPorForma = recebimentosPorForma,
                    Vendas = vendas,
                    Movimentacoes = movimentacoesDetalhadas
                };
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter resumo de fechamento: {ex.Message}");
                return null;
            }
        }
    }
}
