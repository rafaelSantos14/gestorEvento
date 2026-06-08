using System;
using System.Collections.Generic;
using System.Windows.Forms;
using GestorEvento.Models;

namespace GestorEvento.Views
{
    public partial class DialogMotivoReimpressao : Form
    {
        private List<MotivoReimpressao> _motivos;
        public MotivoReimpressao MotivoSelecionado { get; private set; }

        public DialogMotivoReimpressao(List<MotivoReimpressao> motivos)
        {
            InitializeComponent();
            _motivos = motivos ?? new List<MotivoReimpressao>();
        }

        private void DialogMotivoReimpressao_Load(object sender, EventArgs e)
        {
            try
            {
                CarregarMotivos();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar motivos: {ex.Message}", "Erro");
            }
        }

        private void CarregarMotivos()
        {
            cmbMotivo.Items.Clear();
            cmbMotivo.Items.Add("Selecione um motivo");

            foreach (var motivo in _motivos)
            {
                cmbMotivo.Items.Add(motivo.DsMotivo);
            }
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar seleção
                if (cmbMotivo.SelectedIndex <= 0)
                {
                    MessageBox.Show("Por favor, selecione um motivo", "Aviso");
                    return;
                }

                // O índice real é SelectedIndex - 1 (porque há um item de instrução no início)
                int indiceMotivo = cmbMotivo.SelectedIndex - 1;
                MotivoSelecionado = _motivos[indiceMotivo];

                DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao confirmar motivo: {ex.Message}", "Erro");
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
