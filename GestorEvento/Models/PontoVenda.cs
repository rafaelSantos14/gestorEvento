using System;

namespace GestorEvento.Models
{
    public class PontoVenda
    {
        public int IdPontoVenda { get; set; }
        public int IdEvento { get; set; }
        public int NoPontoVenda { get; set; } // Número sequencial do caixa por evento
        public string DsPontoVenda { get; set; } // Nome/descrição do ponto de venda
        public string CdStatus { get; set; } // Aberto, Fechado
        public DateTime DtAbertura { get; set; }
        public decimal VlInicial { get; set; }
        public DateTime? DtFechamento { get; set; }
        public decimal? VlFinal { get; set; }
        public string ObsCaixa { get; set; }

        // Constructors
        public PontoVenda() { }

        public PontoVenda(int idEvento, decimal vlInicial)
        {
            IdEvento = idEvento;
            VlInicial = vlInicial;
            CdStatus = "Aberto";
            DtAbertura = DateTime.Now;
        }
    }
}
