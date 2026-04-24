namespace GestorEvento.Models
{
    public class ProdutoEvento
    {
        public int Id { get; set; }
        public int IdProduto { get; set; }
        public int IdEvento { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; } // qtde_produto (total permitido)
        public int QuantidadeVendida { get; set; } // qtde_vendida (já vendido)

        // Propriedade calculada: quantidade disponível para venda
        public int QuantidadeDisponivel
        {
            get { return Quantidade - QuantidadeVendida; }
        }

        public ProdutoEvento() { }

        public ProdutoEvento(int id, int idProduto, int idEvento, decimal preco, int quantidade, int quantidadeVendida = 0)
        {
            Id = id;
            IdProduto = idProduto;
            IdEvento = idEvento;
            Preco = preco;
            Quantidade = quantidade;
            QuantidadeVendida = quantidadeVendida;
        }
    }
}
