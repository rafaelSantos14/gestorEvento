using System;
using System.Collections.Generic;

namespace GestorEvento.Models
{
    /// <summary>
    /// DTO com todos os dados necessários para exibir na tela de fechamento de caixa
    /// </summary>
    public class ResumoFechamentoCaixa
    {
        public int IdPontoVenda { get; set; }
        public int NoPontoVenda { get; set; }
        public string NomePontoVenda { get; set; }
        public DateTime DtAbertura { get; set; }
        public decimal VlInicial { get; set; }

        // Totalizadores
        public decimal TotalVendasDinheiro { get; set; }
        public decimal TotalEsperado { get; set; }

        // Só a parte em Dinheiro (é a que efetivamente afeta a conferência do caixa físico;
        // o detalhamento com todas as formas fica em DoacoesPorForma)
        public decimal TotalDoacoesDinheiro { get; set; }

        // Listas de detalhamento
        public List<ResumoRecebimentoPorForma> RecebimentosPorForma { get; set; }
        public List<ResumoDoacaoPorForma> DoacoesPorForma { get; set; }
        public List<ResumoVendaFechamento> Vendas { get; set; }
        public List<MovimentacaoDetalhada> Movimentacoes { get; set; }

        public ResumoFechamentoCaixa()
        {
            RecebimentosPorForma = new List<ResumoRecebimentoPorForma>();
            DoacoesPorForma = new List<ResumoDoacaoPorForma>();
            Vendas = new List<ResumoVendaFechamento>();
            Movimentacoes = new List<MovimentacaoDetalhada>();
        }
    }

    /// <summary>
    /// Resumo de recebimentos agrupados por forma de pagamento
    /// </summary>
    public class ResumoRecebimentoPorForma
    {
        public int IdFormaPagamento { get; set; }
        public string NomeFormaPagamento { get; set; }
        public decimal TotalRecebimento { get; set; }
    }

    /// <summary>
    /// Resumo de doações agrupadas por forma de pagamento
    /// </summary>
    public class ResumoDoacaoPorForma
    {
        public int IdFormaPagamento { get; set; }
        public string NomeFormaPagamento { get; set; }
        public decimal TotalDoacao { get; set; }
    }

    /// <summary>
    /// Informações simplificadas de uma venda para exibição no fechamento
    /// </summary>
    public class ResumoVendaFechamento
    {
        public int IdVenda { get; set; }
        public DateTime DtVenda { get; set; }
        public decimal VlTotal { get; set; }
        public string TipoOperacao { get; set; }
        public string NomeFormaPagamento { get; set; } // Principal forma de pagamento
    }

    /// <summary>
    /// Informações simplificadas de uma movimentação para exibição no fechamento
    /// </summary>
    public class MovimentacaoDetalhada
    {
        public int IdMovimentacao { get; set; }
        public string TipoMovimento { get; set; }
        public decimal VlMovimento { get; set; }
        public DateTime DtMovimento { get; set; }
        public string Descricao { get; set; }
    }
}
