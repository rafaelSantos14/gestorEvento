using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestorEvento.Utilities;
using GestorEvento.Services;

namespace GestorEvento.Views
{
    public partial class FormSelecionarPontoVenda : Form
    {
        private int _eventoIdSelecionado = 0;
        private PontoVendaService _pontoVendaService;
        private static FormPDV _formPDVGlobal = null;

        public FormSelecionarPontoVenda(int eventoId)
        {
            InitializeComponent();
            _eventoIdSelecionado = eventoId;
            _pontoVendaService = new PontoVendaService();
            
            // Configurar grid
            ConfigurarDataGridView();
            
            // Subscrever ao evento Load
            this.Load += FormSelecionarPontoVenda_Load;
        }

        private void FormSelecionarPontoVenda_Load(object sender, EventArgs e)
        {
            // Carregar caixas após o form estar carregado
            CarregarCaixasAbertas();
        }

        private void ConfigurarDataGridView()
        {
            dgvCaixas.Columns.Clear();

            // Coluna ID (hidden)
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                Width = 50,
                Visible = false,
                ReadOnly = true
            };
            dgvCaixas.Columns.Add(colId);

            // Coluna Caixa
            var colCaixa = new DataGridViewTextBoxColumn
            {
                Name = "Caixa",
                HeaderText = "Caixa",
                Width = 150,
                ReadOnly = true
            };
            dgvCaixas.Columns.Add(colCaixa);

            // Coluna Descrição
            var colDescricao = new DataGridViewTextBoxColumn
            {
                Name = "Descricao",
                HeaderText = "Descrição",
                Width = 150,
                ReadOnly = true
            };
            dgvCaixas.Columns.Add(colDescricao);

            // Coluna Status
            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 100,
                ReadOnly = true
            };
            dgvCaixas.Columns.Add(colStatus);

            // Coluna Valor Inicial
            var colValorInicial = new DataGridViewTextBoxColumn
            {
                Name = "ValorInicial",
                HeaderText = "V.Inicial",
                Width = 100,
                ReadOnly = true
            };
            dgvCaixas.Columns.Add(colValorInicial);

            // Coluna Abertura
            var colAbertura = new DataGridViewTextBoxColumn
            {
                Name = "DtAbertura",
                HeaderText = "Dt. Abertura",
                Width = 130,
                ReadOnly = true
            };
            dgvCaixas.Columns.Add(colAbertura);

            // Coluna Ação: Registrar Venda
            var colRegistrarVenda = new DataGridViewButtonColumn
            {
                Name = "RegistrarVenda",
                HeaderText = "",
                Text = "💰",
                UseColumnTextForButtonValue = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvCaixas.Columns.Add(colRegistrarVenda);

            // Coluna Ação: Fechar Caixa
            var colFecharCaixa = new DataGridViewButtonColumn
            {
                Name = "FecharCaixa",
                HeaderText = "",
                Text = "🔒",
                UseColumnTextForButtonValue = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvCaixas.Columns.Add(colFecharCaixa);

            // Configurar cores
            dgvCaixas.DefaultCellStyle.ForeColor = Color.Black;
            dgvCaixas.DefaultCellStyle.BackColor = Color.White;
            dgvCaixas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvCaixas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvCaixas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvCaixas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvCaixas.AllowUserToAddRows = false;
        }

        private void CarregarCaixasAbertas()
        {
            dgvCaixas.Rows.Clear();

            try
            {
                // Obter caixas abertas do evento
                var caixas = _pontoVendaService.GetCaixasAbertas(_eventoIdSelecionado);
                
                if (caixas.Count == 0)
                {
                    DialogoCustomizado info = new DialogoCustomizado(
                        "Informação",
                        "Nenhuma caixa aberta para este evento",
                        TipoDialogo.Informacao,
                        TipoButton.Ok
                    );
                    info.ShowDialog();
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                foreach (var caixa in caixas)
                {
                    dgvCaixas.Rows.Add(
                        caixa.IdPontoVenda,
                        $"Caixa #{caixa.NoPontoVenda}",
                        string.IsNullOrWhiteSpace(caixa.DsPontoVenda) ? "-" : caixa.DsPontoVenda,
                        "Aberto",
                        caixa.VlInicial.ToString("F2"),
                        caixa.DtAbertura.ToString("dd/MM/yyyy HH:mm:ss")
                    );
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar caixas: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
            }
        }

        private void dgvCaixas_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0)
                return;

            var row = dgvCaixas.Rows[e.RowIndex];
            
            if (!int.TryParse(row.Cells["ID"].Value?.ToString() ?? "0", out int caixaId))
                return;

            try
            {
                // Ação: Registrar Venda
                if (e.ColumnIndex == dgvCaixas.Columns["RegistrarVenda"].Index)
                {
                    // Verificar se FormPDV já está aberta
                    if (_formPDVGlobal != null && !_formPDVGlobal.IsDisposed)
                    {
                        // Exibir mensagem de aviso
                        DialogoCustomizado dialogo = new DialogoCustomizado(
                            "Informação",
                            "Já existe uma janela de PDV aberta!",
                            TipoDialogo.Informacao,
                            TipoButton.Ok
                        );
                        dialogo.ShowDialog();
                        
                        // Ativar a janela existente
                        _formPDVGlobal.BringToFront();
                        _formPDVGlobal.Activate();
                    }
                    else
                    {
                        // Criar nova FormPDV
                        _formPDVGlobal = new FormPDV(caixaId);
                        
                        // Limpar referência quando fechada
                        _formPDVGlobal.FormClosed += (s, args) =>
                        {
                            _formPDVGlobal = null;
                        };
                        
                        _formPDVGlobal.Show();
                    }
                }
                // Ação: Fechar Caixa
                else if (e.ColumnIndex == dgvCaixas.Columns["FecharCaixa"].Index)
                {
                    // Abrir FormFecharCaixa como dialog
                    FormFecharCaixa formFecharCaixa = new FormFecharCaixa(caixaId);
                    formFecharCaixa.ShowDialog();

                    // Fechar FormSelecionarPontoVenda
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao executar ação: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }

        private void dgvCaixas_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex < 0)
                return;

            // Colorir botão Registrar Venda em Azul
            if (dgvCaixas.Columns[e.ColumnIndex].Name == "RegistrarVenda")
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.FromArgb(25, 118, 210);
                e.CellStyle.SelectionBackColor = Color.White;
                e.CellStyle.SelectionForeColor = Color.FromArgb(25, 118, 210);
            }
            // Colorir botão Fechar Caixa em Vermelho
            else if (dgvCaixas.Columns[e.ColumnIndex].Name == "FecharCaixa")
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.FromArgb(244, 67, 54);
                e.CellStyle.SelectionBackColor = Color.White;
                e.CellStyle.SelectionForeColor = Color.FromArgb(244, 67, 54);
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
