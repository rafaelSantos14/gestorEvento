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

namespace GestorEvento.Views
{
    public partial class FormPrincipal : Form
    {
        private bool arrastandoJanela = false;
        private Point pontoInicial;

        public FormPrincipal()
        {
            InitializeComponent();
            
            // Configurar como MDI Container
            this.IsMdiContainer = true;
            
            // Mudar cor de fundo da área MDI para branco
            foreach (Control ctrl in this.Controls)
            {
                if (ctrl is MdiClient)
                {
                    ctrl.BackColor = Color.White;
                }
            }
            
            // Aplicar estilos aos botões
            EstiloManager.AplicarEstiloInfo(btnProdutos);
            EstiloManager.AplicarEstiloInfo(btnEventos);
            EstiloManager.AplicarEstiloInfo(btnVincular);
            EstiloManager.AplicarEstiloInfo(btnCaixa);            
            EstiloManager.AplicarEstiloAviso(btnSair);
        }

        private void btnProdutos_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se já existe uma janela aberta
                foreach (Form f in this.MdiChildren)
                {
                    if (f is FormProdutos)
                    {
                        f.Activate();
                        return;
                    }
                }
                
                // Abre uma nova instância
                FormProdutos form = new FormProdutos();
                form.Text = "Cadastro de Produtos";
                form.MdiParent = this;
                form.Show();
                
                // Dimensionar DEPOIS de Show() para resetar qualquer configuração anterior
                // Desconta: panelMenu (202px) + barra de título (40px) + espaço abas (35px)
                form.Location = new Point(0, 0);
                form.Size = new Size(this.ClientSize.Width - panelMenu.Width - 5, this.ClientSize.Height - panelTitulo.Height - 35);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir FormProdutos: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
        }

        private void btnEventos_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se já existe uma janela aberta
                foreach (Form f in this.MdiChildren)
                {
                    if (f is FormEventos)
                    {
                        f.Activate();
                        return;
                    }
                }
                
                // Abre uma nova instância
                FormEventos form = new FormEventos();
                form.Text = "Cadastro de Eventos";
                form.MdiParent = this;
                form.Show();
                
                // Dimensionar DEPOIS de Show() para resetar qualquer configuração anterior
                // Desconta: panelMenu (202px) + barra de título (40px) + espaço abas (35px)
                form.Location = new Point(0, 0);
                form.Size = new Size(this.ClientSize.Width - panelMenu.Width - 5, this.ClientSize.Height - panelTitulo.Height - 35);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir FormEventos: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
        }

        private void btnVincular_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Produto-Evento - em desenvolvimento", "Info");
        }

        private void btnCaixa_Click(object sender, EventArgs e)
        {
            try
            {
                // Verifica se já existe uma janela aberta
                foreach (Form f in this.MdiChildren)
                {
                    if (f is FormEventosAtivos)
                    {
                        f.Activate();
                        return;
                    }
                }
                
                // Abre uma nova instância
                FormEventosAtivos form = new FormEventosAtivos();
                form.Text = "Seleção de caixa";
                form.MdiParent = this;
                form.Show();
                
                // Dimensionar DEPOIS de Show() para resetar qualquer configuração anterior
                // Desconta: panelMenu (202px) + barra de título (40px) + espaço abas (35px)
                form.Location = new Point(0, 0);
                form.Size = new Size(this.ClientSize.Width - panelMenu.Width - 5, this.ClientSize.Height - panelTitulo.Height - 35);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir FormEventosAtivos: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
        }       

        private void btnRelatorios_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Relatórios - em desenvolvimento", "Info");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            DialogoCustomizado dialogo = new DialogoCustomizado(
                 "Confirmação",
                 "Deseja realmente sair da aplicação?",
                 TipoDialogo.Aviso,
                 TipoButton.SimNao
             );

            if (dialogo.ShowDialog() == DialogResult.Yes)
            {
                Application.Exit();
            }
        }        
       
        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PanelTitulo_MouseDown(object sender, MouseEventArgs e)
        {            
            if (this.WindowState != FormWindowState.Maximized)
            {
                arrastandoJanela = true;
                pontoInicial = e.Location;
            }
        }

        private void PanelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (arrastandoJanela)
            {
                Point novaLocacao = this.Location;
                novaLocacao.X += e.X - pontoInicial.X;
                novaLocacao.Y += e.Y - pontoInicial.Y;
                this.Location = novaLocacao;
            }
        }

        private void PanelTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            arrastandoJanela = false;
        }

        private void btnPdv_Click_1(object sender, EventArgs e)
        {
            try
            {
                // Abrir FormSelecionarPDV como dialog
                FormSelecionarPDV form = new FormSelecionarPDV();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir FormSelecionarPDV: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
        }
    }
}
