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
            EstiloManager.AplicarEstiloInfo(btnRelatorios);
            EstiloManager.AplicarEstiloInfo(btnCaixa);
            EstiloManager.AplicarEstiloInfo(btnConfiguracoes);
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
            // Criar o menu de contexto dinamicamente
            ContextMenuStrip menuRelatorios = new ContextMenuStrip();
            
            // Adicionar item Relatórios Consolidados (novo)
            ToolStripMenuItem itemConsolidado = new ToolStripMenuItem("📈 Relatórios Consolidados");
            itemConsolidado.Click += (s, args) => AbrirRelatoriosConsolidados();
            menuRelatorios.Items.Add(itemConsolidado);
            
            // Separador
            menuRelatorios.Items.Add(new ToolStripSeparator());
            
            // Adicionar item Relatório de Vendas
            ToolStripMenuItem itemVendas = new ToolStripMenuItem("📊 Vendas");
            itemVendas.Click += (s, args) => AbrirRelatorioVendas();
            menuRelatorios.Items.Add(itemVendas);

            // Adicionar item Relatório de Caixas (Ponto de Venda)
            ToolStripMenuItem itemCaixas = new ToolStripMenuItem("💰 Caixas (PDV)");
            itemCaixas.Click += (s, args) => AbrirRelatorioCaixas();
            menuRelatorios.Items.Add(itemCaixas);

            // Adicionar item Relatório de Cortesia
            ToolStripMenuItem itemCortesia = new ToolStripMenuItem("🎁 Cortesias");
            itemCortesia.Click += (s, args) => AbrirRelatorioCortesias();
            menuRelatorios.Items.Add(itemCortesia);

            // Adicionar item Relatório de Reimpressões
            ToolStripMenuItem itemReimpressoes = new ToolStripMenuItem("🖨️ Reimpressões");
            itemReimpressoes.Click += (s, args) => AbrirRelatorioReimpressoes();
            menuRelatorios.Items.Add(itemReimpressoes);
            
            // adicionar mais relatórios no futuro:
            // ToolStripMenuItem itemTeste = new ToolStripMenuItem("📦 Estoque");
            // itemTeste.Click += (s, args) => AbrirRelatorioTeste();
            // menuRelatorios.Items.Add(itemTeste);
            
            // Mostrar o menu na frente do botão (ao lado direito)
            menuRelatorios.Show(btnRelatorios, new Point(btnRelatorios.Width, 0));
        }

        private void btnConfiguracoes_Click(object sender, EventArgs e)
        {
            // Criar o menu de contexto dinamicamente (mesmo padrão do btnRelatorios_Click)
            ContextMenuStrip menuConfiguracoes = new ContextMenuStrip();

            ToolStripMenuItem itemSetores = new ToolStripMenuItem("Setores");
            itemSetores.Click += (s, args) => AbrirSetores();
            menuConfiguracoes.Items.Add(itemSetores);

            // Mostrar o menu na frente do botão (ao lado direito)
            menuConfiguracoes.Show(btnConfiguracoes, new Point(btnConfiguracoes.Width, 0));
        }

        private void AbrirSetores()
        {
            // Verifica se já existe uma janela aberta (mesmo padrão do btnProdutos_Click)
            foreach (Form f in this.MdiChildren)
            {
                if (f is FormSetores)
                {
                    f.Activate();
                    return;
                }
            }

            FormSetores form = new FormSetores();
            form.Text = "Cadastro de Setores";
            form.MdiParent = this;
            form.Show();

            form.Location = new Point(0, 0);
            form.Size = new Size(this.ClientSize.Width - panelMenu.Width - 5, this.ClientSize.Height - panelTitulo.Height - 35);
        }

        private void AbrirRelatoriosConsolidados()
        {
            try
            {
                FormRelatoriosConsolidados form = new FormRelatoriosConsolidados();
                form.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir Relatórios Consolidados: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
        }

        private void AbrirRelatorioVendas()
        {
            try
            {
                // Verifica se já existe uma janela aberta
                foreach (Form f in this.MdiChildren)
                {
                    if (f is FormRelatorioVenda)
                    {
                        f.Activate();
                        return;
                    }
                }
                
                // Abre uma nova instância
                FormRelatorioVenda form = new FormRelatorioVenda();
                form.Text = "Relatório de Vendas";
                form.MdiParent = this;
                form.Show();
                
                // Dimensionar DEPOIS de Show() para resetar qualquer configuração anterior
                // Desconta: panelMenu (202px) + barra de título (40px) + espaço abas (35px)
                form.Location = new Point(0, 0);
                form.Size = new Size(this.ClientSize.Width - panelMenu.Width - 5, this.ClientSize.Height - panelTitulo.Height - 35);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir FormRelatorioVenda: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
        }

        private void AbrirRelatorioCaixas()
        {
            try
            {
                foreach (Form f in this.MdiChildren)
                {
                    if (f is FormRelatorioCaixa)
                    {
                        f.Activate();
                        return;
                    }
                }

                FormRelatorioCaixa form = new FormRelatorioCaixa();
                form.Text = "Relatório de Caixas";
                form.MdiParent = this;
                form.Show();

                form.Location = new Point(0, 0);
                form.Size = new Size(this.ClientSize.Width - panelMenu.Width - 5, this.ClientSize.Height - panelTitulo.Height - 35);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir FormRelatorioCaixa: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
        }

        private void AbrirRelatorioCortesias()
        {
            try
            {
                foreach (Form f in this.MdiChildren)
                {
                    if (f is FormRelatorioCortesia)
                    {
                        f.Activate();
                        return;
                    }
                }

                FormRelatorioCortesia form = new FormRelatorioCortesia();
                form.Text = "Relatório de Cortesias";
                form.MdiParent = this;
                form.Show();

                form.Location = new Point(0, 0);
                form.Size = new Size(this.ClientSize.Width - panelMenu.Width - 5, this.ClientSize.Height - panelTitulo.Height - 35);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir FormRelatorioCortesia: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
        }

        private void AbrirRelatorioReimpressoes()
        {
            try
            {
                foreach (Form f in this.MdiChildren)
                {
                    if (f is FormRelatorioReimpressao)
                    {
                        f.Activate();
                        return;
                    }
                }

                FormRelatorioReimpressao form = new FormRelatorioReimpressao();
                form.Text = "Relatório de Reimpressões";
                form.MdiParent = this;
                form.Show();

                form.Location = new Point(0, 0);
                form.Size = new Size(this.ClientSize.Width - panelMenu.Width - 5, this.ClientSize.Height - panelTitulo.Height - 35);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao abrir FormRelatorioReimpressao: " + ex.Message + "\n" + ex.StackTrace, "Erro");
            }
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

        private void btnFechar_Click(object sender, EventArgs e)
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
    }
}
