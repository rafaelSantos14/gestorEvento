using System;
using System.Collections.Generic;

namespace GestorEvento.Models
{
    public class Reimpressao
    {
        public int IdReimpressao { get; set; }
        public DateTime DtReimpressao { get; set; }
        public int IdMotivo { get; set; }
        public int IdEvento { get; set; }
        public int IdPontoVenda { get; set; }
        public decimal VlTotal { get; set; }
        
        public List<ReimpressaoItem> Itens { get; set; } = new List<ReimpressaoItem>();
    }
}
