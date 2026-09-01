using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestorEvento.Models;
using GestorEvento.Services;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormVincularProdutoEvento : Form
    {
        private readonly ProdutoEventoService _produtoEventoService;
        private readonly int _idProduto;
        private readonly int _idEvento;

        public decimal PrecoDigitado { get; set; }
        public int QuantidadeDigitada { get; set; }
        public bool PermiteValorZerado { get; set; }
        public bool Antecipado { get; set; }
        public string NomeProduto { get; private set; }

        public FormVincularProdutoEvento(string nomeProduto, int idProduto, int idEvento, decimal precoAtual = 0, int quantidadeAtual = 0, bool permiteValorZeradoAtual = false, bool antecipadoAtual = false)
        {
            InitializeComponent();
            NomeProduto = nomeProduto;
            _idProduto = idProduto;
            _idEvento = idEvento;
            PrecoDigitado = precoAtual;
            QuantidadeDigitada = quantidadeAtual;
            PermiteValorZerado = permiteValorZeradoAtual;
            Antecipado = antecipadoAtual;

            _produtoEventoService = new ProdutoEventoService();

            // Aplicar estilos

            this.BackColor = System.Drawing.Color.White;
            EstiloManager.AplicarEstiloSalvar(btnSalvar);
            EstiloManager.AplicarEstiloLimpar(btnCancelar);
            ConfigurarGridHistorico();
        }

        private void FormVinculacaoProduto_Load(object sender, EventArgs e)
        {
            lblProduto.Text = $"{NomeProduto}";

            if (PrecoDigitado > 0)
                txtPreco.Text = PrecoDigitado.ToString("F2");

            if (QuantidadeDigitada > 0)
                txtQuantidade.Text = QuantidadeDigitada.ToString();

            chkPermiteValorZerado.Checked = PermiteValorZerado;
            chkAntecipado.Checked = Antecipado;

            // Focar no campo de preço
            txtPreco.Focus();

            CarregarHistorico();
        }

        private void ConfigurarGridHistorico()
        {
            dgvHistorico.AutoGenerateColumns = false;
            dgvHistorico.Columns.Clear();

            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn { Name = "DataHora", HeaderText = "Data/Hora", FillWeight = 130 });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecoAnterior", HeaderText = "Preço Antes", FillWeight = 100 });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecoNovo", HeaderText = "Preço Depois", FillWeight = 100 });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn { Name = "QtdeAnterior", HeaderText = "Qtde Antes", FillWeight = 100 });
            dgvHistorico.Columns.Add(new DataGridViewTextBoxColumn { Name = "QtdeNova", HeaderText = "Qtde Depois", FillWeight = 100 });

            dgvHistorico.DefaultCellStyle.ForeColor = Color.Black;
            dgvHistorico.DefaultCellStyle.BackColor = Color.White;
            dgvHistorico.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvHistorico.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvHistorico.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvHistorico.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
            dgvHistorico.ColumnHeadersHeight = 40;
            dgvHistorico.EnableHeadersVisualStyles = false;
            dgvHistorico.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvHistorico.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void CarregarHistorico()
        {
            dgvHistorico.Rows.Clear();

            var historico = _produtoEventoService.GetHistoricoMovimentacoes(_idProduto, _idEvento);
            foreach (var item in historico)
            {
                dgvHistorico.Rows.Add(
                    item.DataMovimentacao.ToString("dd/MM/yyyy HH:mm"),
                    item.ValorAnterior.ToString("F2"),
                    item.ValorNovo.ToString("F2"),
                    item.QuantidadeAnterior,
                    item.QuantidadeNova
                );
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
                return;

            PrecoDigitado = decimal.Parse(txtPreco.Text);
            QuantidadeDigitada = int.Parse(txtQuantidade.Text);
            PermiteValorZerado = chkPermiteValorZerado.Checked;
            Antecipado = chkAntecipado.Checked;

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

            if (!decimal.TryParse(txtPreco.Text, out decimal preco) || preco < 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado("Aviso", "Preço deve ser um número válido e não pode ser negativo", TipoDialogo.Aviso, TipoButton.Ok);
                dialogo.ShowDialog();
                txtPreco.Clear();
                txtPreco.Focus();
                return false;
            }

            if (preco == 0 && !chkPermiteValorZerado.Checked)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado("Aviso", "Preço deve ser um número maior que zero (ou marque \"Já pago na inscrição\" para permitir valor zerado)", TipoDialogo.Aviso, TipoButton.Ok);
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

        private void TxtPreco_TextChanged(object sender, EventArgs e)
        {
            // Remove caracteres não numéricos
            string texto = new string(txtPreco.Text.Where(c => char.IsDigit(c)).ToArray());

            // Se vazio, mostra "0"
            if (string.IsNullOrEmpty(texto))
            {
                texto = "0";
            }

            // Formata com 2 casas decimais
            decimal valor = decimal.Parse(texto) / 100;
            
            // Guarda o index do cursor
            int cursorPos = txtPreco.SelectionStart;
            
            // Atualiza o texto formatado
            txtPreco.Text = valor.ToString("F2");
            
            // Reposiciona o cursor no final
            txtPreco.SelectionStart = txtPreco.Text.Length;
        }

        private void TxtPreco_Leave(object sender, EventArgs e)
        {
            // Validar se é um número válido
            if (decimal.TryParse(txtPreco.Text, out decimal valor))
            {
                if (valor < 0)
                {
                    txtPreco.Text = "0";
                }
            }
            else if (!string.IsNullOrWhiteSpace(txtPreco.Text))
            {
                txtPreco.Text = "0";
            }
        }
    }
}
