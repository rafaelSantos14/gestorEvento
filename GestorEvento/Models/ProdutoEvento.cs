namespace GestorEvento.Models
{
    public class ProdutoEvento
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdEvento { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }

        public ProdutoEvento() { }

        public ProdutoEvento(int id, int idProduto, int idEvento, decimal preco, int quantidade)
        {
            Id = id;
            IdProduto = idProduto;
            IdEvento = idEvento;
            Preco = preco;
            Quantidade = quantidade;
        }
    }
}
