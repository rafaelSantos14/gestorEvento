using System;

namespace GestorEvento.Models
{
    /// <summary>
    /// Enum de tipos de movimentação no ponto de venda
    /// </summary>
    public enum TipoMovimento
    {
        TROCO = 1,           // Saída automática quando entrega troco em uma venda
        SANGRIA = 2,         // Saída manual quando gerente retira dinheiro do caixa
        ENTRADA_TROCO = 3    // Entrada manual quando traz dinheiro para trocar
    }

    /// <summary>
    /// Modelo de movimentação de entrada/saída do ponto de venda
    /// </summary>
    public class Movimentacao
    {
        public int IdMovimentacao { get; set; }
        public int IdPontoVenda { get; set; }
        public TipoMovimento TipoMovimento { get; set; }
        public decimal VlMovimento { get; set; }
        public DateTime DtMovimento { get; set; }
        public string Descricao { get; set; }
        public int? IdVenda { get; set; } // Nullable: não preenchido para sangria/entrada

        // Constructors
        public Movimentacao() { }

        public Movimentacao(int idPontoVenda, TipoMovimento tipoMovimento, decimal vlMovimento)
        {
            IdPontoVenda = idPontoVenda;
            TipoMovimento = tipoMovimento;
            VlMovimento = vlMovimento;
            DtMovimento = DateTime.Now;
        }

        public Movimentacao(int idPontoVenda, TipoMovimento tipoMovimento, decimal vlMovimento, string descricao)
            : this(idPontoVenda, tipoMovimento, vlMovimento)
        {
            Descricao = descricao;
        }
    }
}
