using System;

namespace GestorEvento.Models.Exceptions
{
    /// <summary>
    /// Exception lançada quando há insuficiência de estoque para uma venda
    /// </summary>
    public class EstoqueInsuficienteException : Exception
    {
        public int IdProdutoEvento { get; set; }
        public string NomeProduto { get; set; }
        public int QuantidadeDisponivel { get; set; }
        public int QuantidadeSolicitada { get; set; }

        public EstoqueInsuficienteException(int idProdutoEvento, string nomeProduto, int quantidadeDisponivel, int quantidadeSolicitada)
            : base($"Estoque insuficiente para {nomeProduto}. Disponível: {quantidadeDisponivel}, Solicitado: {quantidadeSolicitada}")
        {
            IdProdutoEvento = idProdutoEvento;
            NomeProduto = nomeProduto;
            QuantidadeDisponivel = quantidadeDisponivel;
            QuantidadeSolicitada = quantidadeSolicitada;
        }
    }
}
