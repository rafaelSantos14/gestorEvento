using System;
using System.Drawing;
using System.Windows.Forms;
using GestorEvento.Utilities;
using GestorEvento.Services;
using GestorEvento.Models;

namespace GestorEvento.Views
{
    public partial class FormSetores : Form
    {
        private SetorService _service;

        public FormSetores()
        {
            InitializeComponent();

            _service = new SetorService();

            EstiloManager.AplicarEstiloSalvar(btnSalvar);
            EstiloManager.AplicarEstiloInfo(btnPesquisar);

            ConfigurarDataGridView();
        }

        private void ConfigurarDataGridView()
        {
            dgvSetores.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            };
            dgvSetores.Columns.Add(colId);

            var colNome = new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Nome",
                Width = 250,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dgvSetores.Columns.Add(colNome);

            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 100,
                ReadOnly = true
            };
            dgvSetores.Columns.Add(colStatus);

            dgvSetores.DefaultCellStyle.ForeColor = Color.Black;
            dgvSetores.DefaultCellStyle.BackColor = Color.White;
            dgvSetores.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvSetores.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvSetores.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);

            dgvSetores.CellDoubleClick += DataGridViewSetores_CellDoubleClick;
            dgvSetores.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private string DescricaoStatus(string flAtivo)
        {
            return string.Equals(flAtivo, "SIM", StringComparison.OrdinalIgnoreCase) ? "Ativo" : "Inativo";
        }

        private void CarregarSetoresDoDb()
        {
            dgvSetores.Rows.Clear();

            var setores = _service.GetAll();

            foreach (var setor in setores)
            {
                dgvSetores.Rows.Add(setor.IdSetor, setor.NmSetor, DescricaoStatus(setor.FlAtivo));
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            CarregarSetoresDoDb();
        }

        private void DataGridViewSetores_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || dgvSetores.Rows.Count == 0)
                return;

            AbrirEdicao(e.RowIndex);
        }

        private void AbrirEdicao(int rowIndex)
        {
            if (dgvSetores.Rows[rowIndex].Cells["ID"].Value == null)
                return;

            int setorId = Convert.ToInt32(dgvSetores.Rows[rowIndex].Cells["ID"].Value);

            var formEditar = new FormEditarSetor(setorId);
            if (formEditar.ShowDialog(this) == DialogResult.OK)
            {
                CarregarSetoresDoDb();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNomeSetor.Text))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, preencha o nome do setor",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            var setor = new Setor
            {
                NmSetor = txtNomeSetor.Text.Trim()
            };

            if (_service.Create(setor))
            {
                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    "Setor salvo com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();

                txtNomeSetor.Clear();
                CarregarSetoresDoDb();
            }
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNomeSetor.Clear();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvSetores.SelectedRows.Count == 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Informação",
                    "Selecione um setor na lista para editar",
                    TipoDialogo.Informacao,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            AbrirEdicao(dgvSetores.SelectedRows[0].Index);
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = txtPesquisar.Text.Trim();
            dgvSetores.Rows.Clear();

            var setores = _service.GetAll();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                setores = setores.FindAll(s => s.NmSetor != null && s.NmSetor.IndexOf(filtro, StringComparison.OrdinalIgnoreCase) >= 0);
            }

            if (setores.Count == 0)
            {
                DialogoCustomizado info = new DialogoCustomizado(
                    "Informação",
                    $"Nenhum setor encontrado com o filtro: '{filtro}'",
                    TipoDialogo.Informacao,
                    TipoButton.Ok
                );
                info.ShowDialog();
                CarregarSetoresDoDb();
                return;
            }

            foreach (var setor in setores)
            {
                dgvSetores.Rows.Add(setor.IdSetor, setor.NmSetor, DescricaoStatus(setor.FlAtivo));
            }
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PanelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void PanelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void PanelTitulo_MouseUp(object sender, MouseEventArgs e)
        {
        }
    }
}
