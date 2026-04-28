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

        public RelatorioVendaData()
        {
            DadosPorFormaPagamento = new List<DadosPagamento>();
            DadosPorCaixa = new List<DadosCaixa>();
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
        public int QuantidadeVendas { get; set; }
    }
}
