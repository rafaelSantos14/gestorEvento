using System;

namespace GestorEvento.Models
{
    public class DoacaoVenda
    {
        public int IdDoacao { get; set; }
        public int IdVenda { get; set; }
        public int IdFormaPagamento { get; set; }
        public decimal VlDoacao { get; set; }
        public DateTime DtDoacao { get; set; }

        public DoacaoVenda() { }

        public DoacaoVenda(int idVenda, int idFormaPagamento, decimal vlDoacao)
        {
            IdVenda = idVenda;
            IdFormaPagamento = idFormaPagamento;
            VlDoacao = vlDoacao;
            DtDoacao = DateTime.Now;
        }
    }
}
