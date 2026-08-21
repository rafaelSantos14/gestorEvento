using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestorEvento.Models;
using GestorEvento.Services;

namespace GestorEvento.Views
{
    public partial class FormConsultaVenda : Form
    {
        private readonly VendaService _vendaService;
        private readonly PontoVendaService _pontoVendaService;
        private readonly RecebimentoService _recebimentoService;
        private readonly DoacaoVendaService _doacaoVendaService;
        private readonly MovimentacaoService _movimentacaoService;
        private readonly FormaPagamentoService _formaPagamentoService;

        public FormConsultaVenda()
        {
            InitializeComponent();

            _vendaService = new VendaService();
            _pontoVendaService = new PontoVendaService();
            _recebimentoService = new RecebimentoService();
            _doacaoVendaService = new DoacaoVendaService();
            _movimentacaoService = new MovimentacaoService();
            _formaPagamentoService = new FormaPagamentoService();

            ConfigurarGrids();
        }

        private void ConfigurarGrids()
        {
            ConfigurarGridProdutos();
            ConfigurarGridFormas(dgvPagamentos);
            ConfigurarGridFormas(dgvDoacoes);
        }

        private void ConfigurarGridProdutos()
        {
            dgvProdutos.AutoGenerateColumns = false;
            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProdutos.MultiSelect = false;

            dgvProdutos.Columns.Clear();
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Produto", HeaderText = "Produto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantidade", HeaderText = "Quantidade", Width = 100 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "ValorUnitario", HeaderText = "Valor Unitário", Width = 130 });
            dgvProdutos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Subtotal", HeaderText = "Subtotal", Width = 130 });

            EstilizarGrid(dgvProdutos);
        }

        private void ConfigurarGridFormas(DataGridView grid)
        {
            grid.AutoGenerateColumns = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.MultiSelect = false;

            grid.Columns.Clear();
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "FormaPagamento", HeaderText = "Forma de Pagamento", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            grid.Columns.Add(new DataGridViewTextBoxColumn { Name = "Valor", HeaderText = "Valor", Width = 100 });

            EstilizarGrid(grid);
        }

        private void EstilizarGrid(DataGridView grid)
        {
            grid.DefaultCellStyle.ForeColor = Color.Black;
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            grid.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            grid.EnableHeadersVisualStyles = false;
        }

        private void TxtIdVenda_TextChanged(object sender, EventArgs e)
        {
            string texto = new string(txtIdVenda.Text.Where(c => char.IsDigit(c)).ToArray());
            if (texto != txtIdVenda.Text)
            {
                txtIdVenda.Text = texto;
                txtIdVenda.SelectionStart = txtIdVenda.Text.Length;
            }
        }

        private void TxtIdVenda_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                BtnPesquisar_Click(sender, EventArgs.Empty);
            }
        }

        private void BtnPesquisar_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtIdVenda.Text, out int idVenda) || idVenda <= 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Informe um ID de venda válido",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            PesquisarVenda(idVenda);
        }

        private void PesquisarVenda(int idVenda)
        {
            try
            {
                var venda = _vendaService.GetVendaById(idVenda);
                if (venda == null)
                {
                    LimparResultado();
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        "Venda não encontrada",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                var pontoVenda = _pontoVendaService.GetPontoVendaById(venda.IdPontoVenda);
                var recebimentos = _recebimentoService.GetRecebimentosByVendaId(idVenda);
                var doacoes = _doacaoVendaService.GetDoacoesByVendaId(idVenda);
                decimal vlTroco = _movimentacaoService.GetTrocoByVendaId(idVenda);

                var formasPagamento = _formaPagamentoService.GetAllFormasPagamento();
                var nomesFormaPagamento = formasPagamento.ToDictionary(f => f.Id, f => f.NmFormaPagamento);

                PreencherDadosVenda(venda, pontoVenda, vlTroco);
                PreencherProdutos(venda.Itens);
                PreencherFormas(dgvPagamentos, recebimentos.Select(r => (r.IdFormaPagamento, r.VlRecebimento)), nomesFormaPagamento);
                PreencherFormas(dgvDoacoes, doacoes.Select(d => (d.IdFormaPagamento, d.VlDoacao)), nomesFormaPagamento);
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao consultar venda: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }

        private void PreencherDadosVenda(Venda venda, PontoVenda pontoVenda, decimal vlTroco)
        {
            lblIdVendaValor.Text = venda.IdVenda.ToString();
            lblDataValor.Text = venda.DtVenda.ToString("dd/MM/yyyy HH:mm:ss");
            lblStatusValor.Text = venda.CdStatus;
            lblValorTotalValor.Text = $"R$ {venda.VlTotal:F2}";
            lblTrocoValor.Text = $"R$ {vlTroco:F2}";

            if (pontoVenda != null)
            {
                string descricao = string.IsNullOrWhiteSpace(pontoVenda.DsPontoVenda) ? "" : $" - {pontoVenda.DsPontoVenda}";
                lblCaixaValor.Text = $"{pontoVenda.NoPontoVenda}{descricao}";
            }
            else
            {
                lblCaixaValor.Text = "-";
            }

            bool isCortesia = string.Equals(venda.TipoOperacao, "CORTESIA", StringComparison.OrdinalIgnoreCase);
            lblTipoValor.Text = isCortesia ? "CORTESIA" : "VENDA";
            lblTipoValor.ForeColor = isCortesia ? Color.FromArgb(255, 152, 0) : Color.FromArgb(25, 118, 210);
        }

        private void PreencherProdutos(List<ItemVenda> itens)
        {
            dgvProdutos.Rows.Clear();
            foreach (var item in itens)
            {
                dgvProdutos.Rows.Add(
                    item.NomeProduto,
                    item.Quantidade,
                    $"R$ {item.VlUnitario:F2}",
                    $"R$ {item.Subtotal:F2}"
                );
            }
        }

        private void PreencherFormas(DataGridView grid, IEnumerable<(int IdFormaPagamento, decimal Valor)> itens, Dictionary<int, string> nomesFormaPagamento)
        {
            grid.Rows.Clear();
            foreach (var item in itens)
            {
                string nomeForma = nomesFormaPagamento.TryGetValue(item.IdFormaPagamento, out string nome) ? nome : "Desconhecida";
                grid.Rows.Add(
                    nomeForma,
                    $"R$ {item.Valor:F2}"
                );
            }
        }

        private void LimparResultado()
        {
            lblIdVendaValor.Text = "-";
            lblDataValor.Text = "-";
            lblCaixaValor.Text = "-";
            lblStatusValor.Text = "-";
            lblValorTotalValor.Text = "-";
            lblTrocoValor.Text = "-";
            lblTipoValor.Text = "-";
            lblTipoValor.ForeColor = Color.Black;

            dgvProdutos.Rows.Clear();
            dgvPagamentos.Rows.Clear();
            dgvDoacoes.Rows.Clear();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
