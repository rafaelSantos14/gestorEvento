using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestorEvento.Models;

namespace GestorEvento.Views
{
    public partial class DialogIdentificacaoCortesia : Form
    {
        private List<Setor> _setores;
        public Setor SetorSelecionado { get; private set; }
        public string Observacao { get; private set; }

        public DialogIdentificacaoCortesia(List<Setor> setores)
        {
            InitializeComponent();
            _setores = setores ?? new List<Setor>();
        }

        private void DialogIdentificacaoCortesia_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarSetores();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar setores: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }

        private void CarregarSetores()
        {
            cmbSetor.Items.Clear();
            cmbSetor.Items.Add("Selecione um setor");

            foreach (var setor in _setores)
            {
                cmbSetor.Items.Add(setor.NmSetor);
            }

            cmbSetor.SelectedIndex = 0;
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar seleção: setor é obrigatório para CORTESIA
                if (cmbSetor.SelectedIndex <= 0)
                {
                    DialogoCustomizado aviso = new DialogoCustomizado(
                        "Aviso",
                        "Por favor, selecione um setor",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    aviso.ShowDialog();
                    return;
                }

                // O índice real é SelectedIndex - 1 (porque há um item de instrução no início)
                int indiceSetor = cmbSetor.SelectedIndex - 1;
                SetorSelecionado = _setores[indiceSetor];

                // Observação é opcional: não há validação de preenchimento
                Observacao = string.IsNullOrWhiteSpace(txtObservacao.Text) ? null : txtObservacao.Text.Trim();

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao confirmar identificação da cortesia: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
