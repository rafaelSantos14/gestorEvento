namespace GestorEvento.Models
{
    public class ReimpressaoItem
    {
        public int IdReimpressaoItem { get; set; }
        public int IdReimpressao { get; set; }
        public int IdProdutoEvento { get; set; }
        public int QtdeReimpressao { get; set; }
        public decimal VlUnitario { get; set; }
        public decimal VlSubtotal { get; set; }
        public string DescricaoProduto { get; set; }
    }
}
