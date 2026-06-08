using System;

namespace GestorEvento.Utilities
{
    /// <summary>
    /// Representa um item de produto para impressão com nome e preço
    /// </summary>
    public class ItemImpressao
    {
        public string Nome { get; set; }
        public decimal Preco { get; set; }

        public ItemImpressao(string nome, decimal preco)
        {
            Nome = nome;
            Preco = preco;
        }

        public override string ToString()
        {
            return $"{Nome} - R$ {Preco.ToString("F2")}";
        }
    }
}
