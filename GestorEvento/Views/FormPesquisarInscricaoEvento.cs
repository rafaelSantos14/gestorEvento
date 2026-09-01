using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestorEvento.Models;
using GestorEvento.Services;

namespace GestorEvento.Views
{
    public partial class FormPesquisarInscricaoEvento : Form
    {
        private readonly InscricaoEventoService _service;
        private readonly int _idEvento;
        private readonly Timer _debounceTimer;
        private bool _carregado = false;

        public InscricaoEvento InscricaoSelecionada { get; private set; }

        public FormPesquisarInscricaoEvento(int idEvento)
        {
            InitializeComponent();
            _idEvento = idEvento;
            _service = new InscricaoEventoService();

            // Pesquisa automática enquanto o operador digita, sem disparar uma query por tecla
            _debounceTimer = new Timer { Interval = 400 };
            _debounceTimer.Tick += DebounceTimer_Tick;

            ConfigurarGrid();
            cmbStatus.SelectedIndex = 0;
        }

        private void FormPesquisarInscricaoEvento_Load(object sender, EventArgs e)
        {
            _carregado = true;
            Pesquisar();
            txtNome.Focus();
        }

        private void CmbStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_carregado)
                Pesquisar();
        }
       
        private void CampoFiltro_TextChanged(object sender, EventArgs e)
        {
            if (!_carregado)
                return;

            _debounceTimer.Stop();
            _debounceTimer.Start();
        }

        private void DebounceTimer_Tick(object sender, EventArgs e)
        {
            _debounceTimer.Stop();
            Pesquisar();
        }

        private void ConfigurarGrid()
        {
            dgvResultados.AutoGenerateColumns = false;
            dgvResultados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResultados.MultiSelect = false;

            dgvResultados.Columns.Clear();
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Visible = false });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome", Width = 320 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cpf", HeaderText = "CPF/CNPJ", Width = 140 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Celular", HeaderText = "Celular", Width = 120 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "E-mail", Width = 160 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Qtde", HeaderText = "Qtde. Antecipada", Width = 130 });
            dgvResultados.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", MinimumWidth = 90, AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });

            dgvResultados.DefaultCellStyle.ForeColor = Color.Black;
            dgvResultados.DefaultCellStyle.BackColor = Color.White;
            dgvResultados.DefaultCellStyle.Font = new Font("Segoe UI", 11F);
            dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResultados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            dgvResultados.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvResultados.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvResultados.ColumnHeadersHeight = 50;
            dgvResultados.RowTemplate.Height = 32;
            dgvResultados.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvResultados.EnableHeadersVisualStyles = false;
        }

        private void Pesquisar()
        {
            try
            {
                var inscricoes = _service.Buscar(_idEvento, txtNome.Text.Trim(), txtCpf.Text.Trim(), txtEmail.Text.Trim(), ObterStatusFiltro());

                dgvResultados.Rows.Clear();
                foreach (var inscricao in inscricoes)
                {
                    dgvResultados.Rows.Add(
                        inscricao.Id,
                        inscricao.NomeParticipante,
                        FormatarCpfCnpj(inscricao.CpfCnpj),
                        FormatarCelular(inscricao.Celular),
                        inscricao.Email,
                        inscricao.QtdeAntecipada,
                        inscricao.CdStatus
                    );
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao pesquisar inscrições: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }
       
        private string ObterStatusFiltro()
        {
            switch (cmbStatus.SelectedItem?.ToString())
            {
                case "Pendente":
                    return InscricaoEvento.StatusPendente;
                case "Retirado":
                    return InscricaoEvento.StatusRetirado;
                default:
                    return null;
            }
        }
       
        private string FormatarCpfCnpj(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            if (valor.Length == 11)
                return $"{valor.Substring(0, 3)}.{valor.Substring(3, 3)}.{valor.Substring(6, 3)}-{valor.Substring(9, 2)}";

            if (valor.Length == 14)
                return $"{valor.Substring(0, 2)}.{valor.Substring(2, 3)}.{valor.Substring(5, 3)}/{valor.Substring(8, 4)}-{valor.Substring(12, 2)}";

            return valor;
        }

        // Celular com 11 dígitos (com o 9) ou 10 (sem o 9) recebe máscara; qualquer outro tamanho
        // é exibido sem formatação (fallback), evitando erro de índice fora do padrão esperado
        private string FormatarCelular(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            if (valor.Length == 11)
                return $"({valor.Substring(0, 2)}) {valor.Substring(2, 5)}-{valor.Substring(7, 4)}";

            if (valor.Length == 10)
                return $"({valor.Substring(0, 2)}) {valor.Substring(2, 4)}-{valor.Substring(6, 4)}";

            return valor;
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            Pesquisar();
        }

        private void CampoFiltro_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                _debounceTimer.Stop();
                Pesquisar();
            }
        }

        private void DgvResultados_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                TentarSelecionar(e.RowIndex);
            }
        }

        private void btnSelecionar_Click(object sender, EventArgs e)
        {
            if (dgvResultados.SelectedRows.Count == 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Selecione uma inscrição na lista",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            TentarSelecionar(dgvResultados.SelectedRows[0].Index);
        }

        private void TentarSelecionar(int rowIndex)
        {
            int id = Convert.ToInt32(dgvResultados.Rows[rowIndex].Cells["Id"].Value);
           
            var inscricao = _service.GetById(id);
            if (inscricao == null || !inscricao.IsPendente)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Esta inscrição já foi retirada por outra venda. A lista será atualizada.",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                Pesquisar();
                return;
            }

            InscricaoSelecionada = inscricao;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
