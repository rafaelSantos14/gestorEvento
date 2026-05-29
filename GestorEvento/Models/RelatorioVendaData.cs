using System;
using System.Collections.Generic;

namespace GestorEvento.Models
{
    /// <summary>
    /// DTO para consolidar dados de relatório de vendas
    /// </summary>
    public class RelatorioVendaData
    {
        public int TotalQuantidadeVendas { get; set; }
        public decimal ValorTotalVendido { get; set; }
        public decimal ValorTotalTroco { get; set; }
        public List<DadosPagamento> DadosPorFormaPagamento { get; set; }
        public List<DadosCaixa> DadosPorCaixa { get; set; }
        public List<DadosProdutoVendido> DadosProdutosVendidos { get; set; }

        public RelatorioVendaData()
        {
            DadosPorFormaPagamento = new List<DadosPagamento>();
            DadosPorCaixa = new List<DadosCaixa>();
            DadosProdutosVendidos = new List<DadosProdutoVendido>();
        }
    }

    /// <summary>
    /// Dados agregados por forma de pagamento
    /// </summary>
    public class DadosPagamento
    {
        public string NomeFormaPagamento { get; set; }
        public decimal ValorTotal { get; set; }
        public int Quantidade { get; set; }
    }

    /// <summary>
    /// Dados agregados por ponto de venda (caixa)
    /// </summary>
    public class DadosCaixa
    {
        public int IdCaixa { get; set; }
        public string NomeCaixa { get; set; }
        public int NumeroCaixa { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorTroco { get; set; }
        public int QuantidadeVendas { get; set; }
    }

    /// <summary>
    /// Dados de produtos vendidos no evento (agrupados por produto e valor unitario)
    /// </summary>
    public class DadosProdutoVendido
    {
        public string NomeProduto { get; set; }
        public int QuantidadeVendida { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal ValorTotalVendido { get; set; }
        public decimal PercentualTotalVendas { get; set; }
    }
}
