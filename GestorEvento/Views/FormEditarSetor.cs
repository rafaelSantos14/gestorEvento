using System;
using System.Windows.Forms;
using GestorEvento.Services;
using GestorEvento.Models;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormEditarSetor : Form
    {
        private SetorService _service;
        private int _setorId;

        public FormEditarSetor(int setorId)
        {
            InitializeComponent();
            _setorId = setorId;
            _service = new SetorService();

            EstiloManager.AplicarEstiloSalvar(btnSalvar);
            EstiloManager.AplicarEstiloLimpar(btnCancelar);

            CarregarSetor();
        }

        private void CarregarSetor()
        {
            var setor = _service.GetById(_setorId);

            if (setor == null)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    "Setor não encontrado.",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
                return;
            }

            txtNome.Text = setor.NmSetor;
            toggleAtivo.Checked = string.Equals(setor.FlAtivo, "SIM", StringComparison.OrdinalIgnoreCase);
            txtNome.Focus();
            txtNome.SelectAll();
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, preencha o nome do setor",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                txtNome.Focus();
                return;
            }

            var setor = new Setor
            {
                IdSetor = _setorId,
                NmSetor = txtNome.Text.Trim()
            };

            if (!_service.Update(setor))
                return;

            bool statusOk = toggleAtivo.Checked ? _service.Reativar(_setorId) : _service.Inativar(_setorId);
            if (!statusOk)
                return;

            DialogoCustomizado sucesso = new DialogoCustomizado(
                "Sucesso",
                "Setor atualizado com sucesso!",
                TipoDialogo.Sucesso,
                TipoButton.Ok
            );
            sucesso.ShowDialog();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
