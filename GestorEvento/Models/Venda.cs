using System;
using System.Collections.Generic;

namespace GestorEvento.Models
{
    public class Venda
    {
        public int IdVenda { get; set; }
        public int IdPontoVenda { get; set; }
        public DateTime DtVenda { get; set; }
        public decimal VlTotal { get; set; }
        public string CdStatus { get; set; } // Pendente, Concluida
        public List<ItemVenda> Itens { get; set; }

        // Constructors
        public Venda()
        {
            Itens = new List<ItemVenda>();
        }

        public Venda(int idPontoVenda)
        {
            IdPontoVenda = idPontoVenda;
            DtVenda = DateTime.Now;
            CdStatus = "Pendente";
            VlTotal = 0;
            Itens = new List<ItemVenda>();
        }

        public void AdicionarItem(ItemVenda item)
        {
            Itens.Add(item);
            RecalcularTotal();
        }

        public void RemoverItem(int index)
        {
            if (index >= 0 && index < Itens.Count)
            {
                Itens.RemoveAt(index);
                RecalcularTotal();
            }
        }

        private void RecalcularTotal()
        {
            VlTotal = 0;
            foreach (var item in Itens)
            {
                VlTotal += item.Subtotal;
            }
        }
    }

    public class ItemVenda
    {
        public int IdItemVenda { get; set; }
        public int IdVenda { get; set; }
        public int IdProduto { get; set; }
        public string NomeProduto { get; set; }
        public int Quantidade { get; set; }
        public decimal VlUnitario { get; set; }
        public decimal Subtotal { get; set; }

        // Constructors
        public ItemVenda() { }

        public ItemVenda(int idProduto, string nomeProduto, int quantidade, decimal vlUnitario)
        {
            IdProduto = idProduto;
            NomeProduto = nomeProduto;
            Quantidade = quantidade;
            VlUnitario = vlUnitario;
            Subtotal = quantidade * vlUnitario;
        }
    }
}
