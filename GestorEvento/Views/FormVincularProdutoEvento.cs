using System;
using System.Windows.Forms;
using GestorEvento.Models;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormVincularProdutoEvento : Form
    {
        public decimal PrecoDigitado { get; set; }
        public int QuantidadeDigitada { get; set; }
        public string NomeProduto { get; private set; }

        public FormVincularProdutoEvento(string nomeProduto, decimal precoAtual = 0, int quantidadeAtual = 0)
        {
            InitializeComponent();
            NomeProduto = nomeProduto;
            PrecoDigitado = precoAtual;
            QuantidadeDigitada = quantidadeAtual;

            // Aplicar estilos
            
            this.BackColor = System.Drawing.Color.White;
            EstiloManager.AplicarEstiloSalvar(btnSalvar);
            EstiloManager.AplicarEstiloLimpar(btnCancelar);
        }

        private void FormVinculacaoProduto_Load(object sender, EventArgs e)
        {
            lblProduto.Text = $"{NomeProduto}";
            
            if (PrecoDigitado > 0)
                txtPreco.Text = PrecoDigitado.ToString("F2");
            
            if (QuantidadeDigitada > 0)
                txtQuantidade.Text = QuantidadeDigitada.ToString();

            // Focar no campo de preço
            txtPreco.Focus();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            PrecoDigitado = decimal.Parse(txtPreco.Text);
            QuantidadeDigitada = int.Parse(txtQuantidade.Text);

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado("Aviso", "Informe o preço", TipoDialogo.Aviso, TipoButton.Ok);
                dialogo.ShowDialog();
                txtPreco.Focus();
                return false;
            }

            if (!decimal.TryParse(txtPreco.Text, out decimal preco) || preco <= 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado("Aviso", "Preço deve ser um número maior que zero", TipoDialogo.Aviso, TipoButton.Ok);
                dialogo.ShowDialog();
                txtPreco.Clear();
                txtPreco.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtQuantidade.Text))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado("Aviso", "Informe a quantidade", TipoDialogo.Aviso, TipoButton.Ok);
                dialogo.ShowDialog();
                txtQuantidade.Focus();
                return false;
            }

            if (!int.TryParse(txtQuantidade.Text, out int quantidade) || quantidade <= 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado("Aviso", "Quantidade deve ser um número inteiro maior que zero", TipoDialogo.Aviso, TipoButton.Ok);
                dialogo.ShowDialog();
                txtQuantidade.Clear();
                txtQuantidade.Focus();
                return false;
            }

            return true;
        }
    }
}
