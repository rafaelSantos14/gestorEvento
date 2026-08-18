using System;

namespace GestorEvento.Models
{
    public class ProdutoEventoMovimentacao
    {
        public int Id { get; set; }
        public int IdProdutoEvento { get; set; }
        public decimal ValorAnterior { get; set; }
        public decimal ValorNovo { get; set; }
        public int QuantidadeAnterior { get; set; }
        public int QuantidadeNova { get; set; }
        public DateTime DataMovimentacao { get; set; }

        public ProdutoEventoMovimentacao() { }

        public ProdutoEventoMovimentacao(int idProdutoEvento, decimal valorAnterior, decimal valorNovo, int quantidadeAnterior, int quantidadeNova)
        {
            IdProdutoEvento = idProdutoEvento;
            ValorAnterior = valorAnterior;
            ValorNovo = valorNovo;
            QuantidadeAnterior = quantidadeAnterior;
            QuantidadeNova = quantidadeNova;
        }
    }
}
