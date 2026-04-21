using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormEventos : Form
    {
        public FormEventos()
        {
            InitializeComponent();
            
            // Aplicar estilos padrão
            EstiloManager.AplicarEstiloSalvar(btnSalvar);
            EstiloManager.AplicarEstiloLimpar(btnLimpar);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // TODO: Implementar salvamento
            DialogoCustomizado dialogo = new DialogoCustomizado(
                "Sucesso",
                "Evento salvo com sucesso!",
                TipoDialogo.Sucesso,
                TipoButton.Ok
            );
            dialogo.ShowDialog();
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNomeEvento.Clear();
            dtpDataEvento.Value = DateTime.Now;
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            DialogoCustomizado dialogo = new DialogoCustomizado(
                "Confirmação",
                "Deseja realmente deletar este evento?",
                TipoDialogo.Aviso,
                TipoButton.SimNao
            );
            
            if (dialogo.ShowDialog() == DialogResult.Yes)
            {
                // TODO: Implementar deleção
                DialogoCustomizado confirmacao = new DialogoCustomizado(
                    "Sucesso",
                    "Evento deletado com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                confirmacao.ShowDialog();
            }
        }

        // Eventos da barra de título customizada
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
            // Não permite arrasto em forms MDI filhos
        }

        private void PanelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            // Não permite arrasto em forms MDI filhos
        }

        private void PanelTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            // Não permite arrasco em forms MDI filhos
        }
    }
}
