using System;
using System.Collections.Generic;
using System.Linq;
using GestorEvento.Models;
using GestorEvento.Repositories;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    public class RelatorioVendaService
    {
        private readonly VendaRepository _vendaRepository;
        private readonly RecebimentoRepository _recebimentoRepository;
        private readonly FormaPagamentoRepository _formaPagamentoRepository;
        private readonly PontoVendaRepository _pontoVendaRepository;

        public RelatorioVendaService()
        {
            _vendaRepository = new VendaRepository();
            _recebimentoRepository = new RecebimentoRepository();
            _formaPagamentoRepository = new FormaPagamentoRepository();
            _pontoVendaRepository = new PontoVendaRepository();
        }

        /// <summary>
        /// Obtém os dados consolidados do relatório de vendas para um evento
        /// </summary>
        public RelatorioVendaData ObterDadosRelatorio(int idEvento)
        {
            if (idEvento <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return new RelatorioVendaData();
            }

            try
            {
                var resultado = new RelatorioVendaData();

                // 1. Obter todas as vendas do evento
                var todasAsVendas = _vendaRepository.ObterVendasPorEvento(idEvento);
                
                // Separar vendas por tipo de operação
                var vendas = todasAsVendas.Where(v => v.TipoOperacao != "CORTESIA").ToList();
                var cortesias = todasAsVendas.Where(v => v.TipoOperacao == "CORTESIA").ToList();
                
                // Métricas de VENDA (operação financeira)
                resultado.TotalQuantidadeVendas = vendas.Count;
                resultado.ValorTotalVendido = vendas.Sum(v => v.VlTotal);
                
                // Métricas de CORTESIA (operação não financeira)
                resultado.TotalQuantidadeCortesia = cortesias.Count;
                resultado.ValorTotalCortesia = cortesias.Sum(v => v.VlTotal);

                // 2. Obter todos os recebimentos do evento (apenas VENDA gera recebimento)
                var recebimentos = _recebimentoRepository.ObterRecebimentosPorEvento(idEvento);

                // 3. Calcular valor total recebido e troco (apenas de VENDA)
                decimal valorTotalRecebido = recebimentos.Sum(r => r.VlRecebimento);
                resultado.ValorTotalTroco = valorTotalRecebido - resultado.ValorTotalVendido;

                // 4. Agrupar dados por forma de pagamento
                var recebimentosAgrupados = recebimentos
                    .GroupBy(r => r.IdFormaPagamento)
                    .ToList();

                foreach (var grupo in recebimentosAgrupados)
                {
                    var idFormaPagamento = grupo.Key;
                    var formaPagamento = _formaPagamentoRepository.GetFormaPagamentoById(idFormaPagamento);
                    var totalPagamento = grupo.Sum(r => r.VlRecebimento);
                    var quantidadeRecebimentos = grupo.Count();

                    if (formaPagamento != null)
                    {
                        resultado.DadosPorFormaPagamento.Add(new DadosPagamento
                        {
                            NomeFormaPagamento = formaPagamento.NmFormaPagamento,
                            ValorTotal = totalPagamento,
                            Quantidade = quantidadeRecebimentos
                        });
                    }
                }

                // 5. Agrupar dados por ponto de venda (caixa) - apenas VENDA
                var vendasAgrupadas = vendas
                    .GroupBy(v => v.IdPontoVenda)
                    .ToList();

                foreach (var grupo in vendasAgrupadas)
                {
                    var idPontoVenda = grupo.Key;
                    var pontoVenda = _pontoVendaRepository.GetPontoVendaById(idPontoVenda);
                    var totalCaixa = grupo.Sum(v => v.VlTotal);
                    var quantidadeCaixa = grupo.Count();
                    var idsVendasCaixa = grupo.Select(v => v.IdVenda).ToHashSet();
                    decimal totalRecebidoCaixa = recebimentos
                        .Where(r => idsVendasCaixa.Contains(r.IdVenda))
                        .Sum(r => r.VlRecebimento);
                    decimal totalTrocoCaixa = totalRecebidoCaixa - totalCaixa;

                    if (pontoVenda != null)
                    {
                        resultado.DadosPorCaixa.Add(new DadosCaixa
                        {
                            IdCaixa = pontoVenda.IdPontoVenda,
                            NomeCaixa = pontoVenda.DsPontoVenda ?? $"Caixa {pontoVenda.NoPontoVenda}",
                            NumeroCaixa = pontoVenda.NoPontoVenda,
                            ValorTotal = totalCaixa,
                            ValorTroco = totalTrocoCaixa,
                            QuantidadeVendas = quantidadeCaixa
                        });
                    }
                }

                // 7. Ordenar dados para melhor visualização
                resultado.DadosPorFormaPagamento = resultado.DadosPorFormaPagamento
                    .OrderByDescending(d => d.ValorTotal)
                    .ToList();

                resultado.DadosPorCaixa = resultado.DadosPorCaixa
                    .OrderBy(d => d.NumeroCaixa)
                    .ToList();

                // 8. Produtos vendidos (agrupados por produto e valor unitário)
                var produtosVendidos = _vendaRepository.ObterResumoProdutosVendidosPorEvento(idEvento);
                foreach (var (nomeProduto, quantidadeInicial, quantidadeVendida, quantidadeCortesia, quantidadeDisponivel, precoUnitario, valorTotalVendido) in produtosVendidos)
                {
                    decimal percentualTotalVendas = resultado.ValorTotalVendido > 0
                        ? (valorTotalVendido / resultado.ValorTotalVendido) * 100
                        : 0;

                    resultado.DadosProdutosVendidos.Add(new DadosProdutoVendido
                    {
                        NomeProduto = nomeProduto,
                        QuantidadeInicial = quantidadeInicial,
                        QuantidadeVendida = quantidadeVendida,
                        QuantidadeCortesia = quantidadeCortesia,
                        QuantidadeDisponivel = quantidadeDisponivel,
                        PrecoUnitario = precoUnitario,
                        ValorTotalVendido = valorTotalVendido,
                        PercentualTotalVendas = percentualTotalVendas
                    });
                }

                return resultado;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter dados do relatório: {ex.Message}");
                return new RelatorioVendaData();
            }
        }

        /// <summary>
        /// Obtém os dados consolidados do relatório de cortesia para um evento
        /// </summary>
        public RelatorioVendaData ObterDadosRelatorioCortesia(int idEvento)
        {
            if (idEvento <= 0)
            {
                UiHelper.ExibirAviso("Aviso", "ID do evento inválido");
                return new RelatorioVendaData();
            }

            try
            {
                var resultado = new RelatorioVendaData();

                var todasAsVendas = _vendaRepository.ObterVendasPorEvento(idEvento);
                var cortesias = todasAsVendas
                    .Where(v => string.Equals(v.TipoOperacao, "CORTESIA", StringComparison.OrdinalIgnoreCase))
                    .ToList();

                // Contar quantidade TOTAL DE ITENS de cortesia (não quantidade de vendas-cortesia)
                resultado.TotalQuantidadeCortesia = cortesias.SelectMany(v => v.Itens).Sum(i => i.Quantidade);
                resultado.ValorTotalCortesia = cortesias.Sum(v => v.VlTotal);

                var cortesiasPorCaixa = cortesias
                    .GroupBy(v => v.IdPontoVenda)
                    .ToList();

                foreach (var grupo in cortesiasPorCaixa)
                {
                    var idPontoVenda = grupo.Key;
                    var pontoVenda = _pontoVendaRepository.GetPontoVendaById(idPontoVenda);
                    if (pontoVenda == null)
                    {
                        continue;
                    }

                    // Contar quantidade TOTAL DE ITENS por caixa (não quantidade de vendas-cortesia)
                    resultado.DadosPorCaixa.Add(new DadosCaixa
                    {
                        IdCaixa = pontoVenda.IdPontoVenda,
                        NomeCaixa = pontoVenda.DsPontoVenda ?? $"Caixa {pontoVenda.NoPontoVenda}",
                        NumeroCaixa = pontoVenda.NoPontoVenda,
                        ValorTotal = grupo.Sum(v => v.VlTotal),
                        ValorTroco = 0m,
                        QuantidadeVendas = grupo.SelectMany(v => v.Itens).Sum(i => i.Quantidade)
                    });
                }

                resultado.DadosPorCaixa = resultado.DadosPorCaixa
                    .OrderBy(d => d.NumeroCaixa)
                    .ToList();

                var produtosCortesia = _vendaRepository.ObterResumoProdutosCortesiaPorEvento(idEvento);
                foreach (var (nomeProduto, quantidadeInicial, quantidadeVendida, quantidadeCortesia, quantidadeDisponivel, precoUnitario, valorTotalCortesia) in produtosCortesia)
                {
                    decimal percentualTotal = resultado.ValorTotalCortesia > 0
                        ? (valorTotalCortesia / resultado.ValorTotalCortesia) * 100
                        : 0;

                    resultado.DadosProdutosVendidos.Add(new DadosProdutoVendido
                    {
                        NomeProduto = nomeProduto,
                        QuantidadeInicial = quantidadeInicial,
                        QuantidadeVendida = quantidadeVendida,
                        QuantidadeCortesia = quantidadeCortesia,
                        QuantidadeDisponivel = quantidadeDisponivel,
                        PrecoUnitario = precoUnitario,
                        ValorTotalVendido = valorTotalCortesia,
                        PercentualTotalVendas = percentualTotal
                    });
                }

                return resultado;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao obter dados do relatório de cortesia: {ex.Message}");
                return new RelatorioVendaData();
            }
        }
    }
}
