using System;

namespace GestorEvento.Models
{
    public class Recebimento
    {
        public int IdRecebimento { get; set; }
        public int IdVenda { get; set; }
        public int IdFormaPagamento { get; set; }
        public decimal VlRecebimento { get; set; }
        public DateTime DtRecebimento { get; set; }

        // Constructors
        public Recebimento() { }

        public Recebimento(int idVenda, int idFormaPagamento, decimal vlRecebimento)
        {
            IdVenda = idVenda;
            IdFormaPagamento = idFormaPagamento;
            VlRecebimento = vlRecebimento;
            DtRecebimento = DateTime.Now;
        }
    }
}
