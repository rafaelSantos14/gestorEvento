using GestorEvento.Components;

namespace GestorEvento.Views
{
    partial class FormRelatorioCaixa
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFiltro = new System.Windows.Forms.Panel();
            this.txtBuscaEvento = new System.Windows.Forms.TextBox();
            this.cmbStatusFiltro = new System.Windows.Forms.ComboBox();
            this.cmbEventoResultados = new System.Windows.Forms.ComboBox();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblResultados = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.panelCards = new System.Windows.Forms.Panel();
            this.pnlCardTroco = new GestorEvento.Components.ModernCard();
            this.lblTrocoTotalValor = new System.Windows.Forms.Label();
            this.lblTrocoTotalLabel = new System.Windows.Forms.Label();
            this.pnlCardTicket = new GestorEvento.Components.ModernCard();
            this.lblTicketMedioValor = new System.Windows.Forms.Label();
            this.lblTicketMedioLabel = new System.Windows.Forms.Label();
            this.pnlCardValor = new GestorEvento.Components.ModernCard();
            this.lblValorVendidoValor = new System.Windows.Forms.Label();
            this.lblValorVendidoLabel = new System.Windows.Forms.Label();
            this.pnlCardCaixas = new GestorEvento.Components.ModernCard();
            this.lblCaixasValor = new System.Windows.Forms.Label();
            this.lblCaixasLabel = new System.Windows.Forms.Label();
            this.panelGraficos = new System.Windows.Forms.Panel();
            this.panelGraficoPizza = new System.Windows.Forms.Panel();
            this.lblGraficoPizza = new System.Windows.Forms.Label();
            this.chartPizza = new LiveCharts.WinForms.PieChart();
            this.panelGraficoBarras = new System.Windows.Forms.Panel();
            this.lblGraficoBarras = new System.Windows.Forms.Label();
            this.chartBarras = new LiveCharts.WinForms.CartesianChart();
            this.panelResumo = new System.Windows.Forms.Panel();
            this.dgvResumoCaixas = new System.Windows.Forms.DataGridView();
            this.lblResumo = new System.Windows.Forms.Label();
            this.panelTitulo.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.pnlCardTroco.SuspendLayout();
            this.pnlCardTicket.SuspendLayout();
            this.pnlCardValor.SuspendLayout();
            this.pnlCardCaixas.SuspendLayout();
            this.panelGraficos.SuspendLayout();
            this.panelGraficoPizza.SuspendLayout();
            this.panelGraficoBarras.SuspendLayout();
            this.panelResumo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumoCaixas)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTitulo.Controls.Add(this.btnMinimizar);
            this.panelTitulo.Controls.Add(this.btnFechar);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(1017, 50);
            this.panelTitulo.TabIndex = 0;
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(927, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 50);
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.Text = "−";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.Transparent;
            this.btnFechar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(972, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(45, 50);
            this.btnFechar.TabIndex = 3;
            this.btnFechar.Text = "✕";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(271, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "RELATÓRIO DE CAIXAS (PDV)";
            // 
            // panelFiltro
            // 
            this.panelFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelFiltro.Controls.Add(this.txtBuscaEvento);
            this.panelFiltro.Controls.Add(this.cmbStatusFiltro);
            this.panelFiltro.Controls.Add(this.cmbEventoResultados);
            this.panelFiltro.Controls.Add(this.lblEvento);
            this.panelFiltro.Controls.Add(this.lblResultados);
            this.panelFiltro.Controls.Add(this.lblStatus);
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltro.Location = new System.Drawing.Point(0, 50);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Padding = new System.Windows.Forms.Padding(15);
            this.panelFiltro.Size = new System.Drawing.Size(1017, 100);
            this.panelFiltro.TabIndex = 1;
            // 
            // txtBuscaEvento
            // 
            this.txtBuscaEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscaEvento.Location = new System.Drawing.Point(147, 20);
            this.txtBuscaEvento.Name = "txtBuscaEvento";
            this.txtBuscaEvento.Size = new System.Drawing.Size(320, 25);
            this.txtBuscaEvento.TabIndex = 1;
            this.txtBuscaEvento.TextChanged += new System.EventHandler(this.TxtBuscaEvento_TextChanged);
            // 
            // cmbStatusFiltro
            // 
            this.cmbStatusFiltro.DropDownHeight = 100;
            this.cmbStatusFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatusFiltro.FormattingEnabled = true;
            this.cmbStatusFiltro.IntegralHeight = false;
            this.cmbStatusFiltro.Location = new System.Drawing.Point(535, 20);
            this.cmbStatusFiltro.Name = "cmbStatusFiltro";
            this.cmbStatusFiltro.Size = new System.Drawing.Size(150, 25);
            this.cmbStatusFiltro.TabIndex = 3;
            this.cmbStatusFiltro.SelectedIndexChanged += new System.EventHandler(this.CmbStatusFiltro_SelectedIndexChanged);
            // 
            // cmbEventoResultados
            // 
            this.cmbEventoResultados.DropDownHeight = 150;
            this.cmbEventoResultados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventoResultados.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEventoResultados.FormattingEnabled = true;
            this.cmbEventoResultados.IntegralHeight = false;
            this.cmbEventoResultados.Location = new System.Drawing.Point(147, 50);
            this.cmbEventoResultados.Name = "cmbEventoResultados";
            this.cmbEventoResultados.Size = new System.Drawing.Size(320, 25);
            this.cmbEventoResultados.TabIndex = 2;
            this.cmbEventoResultados.SelectedIndexChanged += new System.EventHandler(this.CmbEventoResultados_SelectedIndexChanged);
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEvento.Location = new System.Drawing.Point(15, 23);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(115, 19);
            this.lblEvento.TabIndex = 0;
            this.lblEvento.Text = "Pesquisar evento:";
            // 
            // lblResultados
            // 
            this.lblResultados.AutoSize = true;
            this.lblResultados.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblResultados.Location = new System.Drawing.Point(15, 53);
            this.lblResultados.Name = "lblResultados";
            this.lblResultados.Size = new System.Drawing.Size(60, 19);
            this.lblResultados.TabIndex = 4;
            this.lblResultados.Text = "Eventos:";
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(479, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 19);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status:";
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.pnlCardTroco);
            this.panelCards.Controls.Add(this.pnlCardTicket);
            this.panelCards.Controls.Add(this.pnlCardValor);
            this.panelCards.Controls.Add(this.pnlCardCaixas);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 150);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.Size = new System.Drawing.Size(1017, 140);
            this.panelCards.TabIndex = 2;
            // 
            // pnlCardTroco
            // 
            this.pnlCardTroco.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(230)))));
            this.pnlCardTroco.BorderRadius = 12;
            this.pnlCardTroco.Controls.Add(this.lblTrocoTotalValor);
            this.pnlCardTroco.Controls.Add(this.lblTrocoTotalLabel);
            this.pnlCardTroco.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTroco.Location = new System.Drawing.Point(496, 20);
            this.pnlCardTroco.Name = "pnlCardTroco";
            this.pnlCardTroco.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTroco.ShadowSize = 4;
            this.pnlCardTroco.Size = new System.Drawing.Size(226, 100);
            this.pnlCardTroco.TabIndex = 2;
            // 
            // lblTrocoTotalValor
            // 
            this.lblTrocoTotalValor.AutoSize = true;
            this.lblTrocoTotalValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTrocoTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.lblTrocoTotalValor.Location = new System.Drawing.Point(10, 45);
            this.lblTrocoTotalValor.Name = "lblTrocoTotalValor";
            this.lblTrocoTotalValor.Size = new System.Drawing.Size(69, 37);
            this.lblTrocoTotalValor.TabIndex = 1;
            this.lblTrocoTotalValor.Text = "R$ -";
            // 
            // lblTrocoTotalLabel
            // 
            this.lblTrocoTotalLabel.AutoSize = true;
            this.lblTrocoTotalLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrocoTotalLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTrocoTotalLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTrocoTotalLabel.Name = "lblTrocoTotalLabel";
            this.lblTrocoTotalLabel.Size = new System.Drawing.Size(75, 19);
            this.lblTrocoTotalLabel.TabIndex = 0;
            this.lblTrocoTotalLabel.Text = "Total Troco";
            // 
            // pnlCardTicket
            // 
            this.pnlCardTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(235)))), ((int)(((byte)(255)))));
            this.pnlCardTicket.BorderRadius = 12;
            this.pnlCardTicket.Controls.Add(this.lblTicketMedioValor);
            this.pnlCardTicket.Controls.Add(this.lblTicketMedioLabel);
            this.pnlCardTicket.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTicket.Location = new System.Drawing.Point(754, 20);
            this.pnlCardTicket.Name = "pnlCardTicket";
            this.pnlCardTicket.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTicket.ShadowSize = 4;
            this.pnlCardTicket.Size = new System.Drawing.Size(226, 100);
            this.pnlCardTicket.TabIndex = 3;
            // 
            // lblTicketMedioValor
            // 
            this.lblTicketMedioValor.AutoSize = true;
            this.lblTicketMedioValor.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTicketMedioValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(123)))), ((int)(((byte)(31)))), ((int)(((byte)(162)))));
            this.lblTicketMedioValor.Location = new System.Drawing.Point(10, 45);
            this.lblTicketMedioValor.Name = "lblTicketMedioValor";
            this.lblTicketMedioValor.Size = new System.Drawing.Size(62, 32);
            this.lblTicketMedioValor.TabIndex = 1;
            this.lblTicketMedioValor.Text = "R$ -";
            // 
            // lblTicketMedioLabel
            // 
            this.lblTicketMedioLabel.AutoSize = true;
            this.lblTicketMedioLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTicketMedioLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTicketMedioLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTicketMedioLabel.Name = "lblTicketMedioLabel";
            this.lblTicketMedioLabel.Size = new System.Drawing.Size(87, 19);
            this.lblTicketMedioLabel.TabIndex = 0;
            this.lblTicketMedioLabel.Text = "Ticket Médio";
            // 
            // pnlCardValor
            // 
            this.pnlCardValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.pnlCardValor.BorderRadius = 12;
            this.pnlCardValor.Controls.Add(this.lblValorVendidoValor);
            this.pnlCardValor.Controls.Add(this.lblValorVendidoLabel);
            this.pnlCardValor.ForeColor = System.Drawing.Color.Black;
            this.pnlCardValor.Location = new System.Drawing.Point(258, 20);
            this.pnlCardValor.Name = "pnlCardValor";
            this.pnlCardValor.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardValor.ShadowSize = 4;
            this.pnlCardValor.Size = new System.Drawing.Size(226, 100);
            this.pnlCardValor.TabIndex = 1;
            // 
            // lblValorVendidoValor
            // 
            this.lblValorVendidoValor.AutoSize = true;
            this.lblValorVendidoValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValorVendidoValor.ForeColor = System.Drawing.Color.Green;
            this.lblValorVendidoValor.Location = new System.Drawing.Point(10, 45);
            this.lblValorVendidoValor.Name = "lblValorVendidoValor";
            this.lblValorVendidoValor.Size = new System.Drawing.Size(69, 37);
            this.lblValorVendidoValor.TabIndex = 1;
            this.lblValorVendidoValor.Text = "R$ -";
            // 
            // lblValorVendidoLabel
            // 
            this.lblValorVendidoLabel.AutoSize = true;
            this.lblValorVendidoLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblValorVendidoLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblValorVendidoLabel.Location = new System.Drawing.Point(10, 10);
            this.lblValorVendidoLabel.Name = "lblValorVendidoLabel";
            this.lblValorVendidoLabel.Size = new System.Drawing.Size(127, 19);
            this.lblValorVendidoLabel.TabIndex = 0;
            this.lblValorVendidoLabel.Text = "Valor Total Vendido";
            // 
            // pnlCardCaixas
            // 
            this.pnlCardCaixas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlCardCaixas.BorderRadius = 12;
            this.pnlCardCaixas.Controls.Add(this.lblCaixasValor);
            this.pnlCardCaixas.Controls.Add(this.lblCaixasLabel);
            this.pnlCardCaixas.ForeColor = System.Drawing.Color.Black;
            this.pnlCardCaixas.Location = new System.Drawing.Point(20, 20);
            this.pnlCardCaixas.Name = "pnlCardCaixas";
            this.pnlCardCaixas.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardCaixas.ShadowSize = 4;
            this.pnlCardCaixas.Size = new System.Drawing.Size(226, 100);
            this.pnlCardCaixas.TabIndex = 0;
            // 
            // lblCaixasValor
            // 
            this.lblCaixasValor.AutoSize = true;
            this.lblCaixasValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblCaixasValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblCaixasValor.Location = new System.Drawing.Point(10, 40);
            this.lblCaixasValor.Name = "lblCaixasValor";
            this.lblCaixasValor.Size = new System.Drawing.Size(37, 51);
            this.lblCaixasValor.TabIndex = 1;
            this.lblCaixasValor.Text = "-";
            // 
            // lblCaixasLabel
            // 
            this.lblCaixasLabel.AutoSize = true;
            this.lblCaixasLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCaixasLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCaixasLabel.Location = new System.Drawing.Point(10, 10);
            this.lblCaixasLabel.Name = "lblCaixasLabel";
            this.lblCaixasLabel.Size = new System.Drawing.Size(181, 19);
            this.lblCaixasLabel.TabIndex = 0;
            this.lblCaixasLabel.Text = "Quantidade Total de Vendas";
            // 
            // panelGraficos
            // 
            this.panelGraficos.BackColor = System.Drawing.Color.White;
            this.panelGraficos.Controls.Add(this.panelGraficoPizza);
            this.panelGraficos.Controls.Add(this.panelGraficoBarras);
            this.panelGraficos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGraficos.Location = new System.Drawing.Point(0, 290);
            this.panelGraficos.Name = "panelGraficos";
            this.panelGraficos.Padding = new System.Windows.Forms.Padding(15);
            this.panelGraficos.Size = new System.Drawing.Size(1017, 260);
            this.panelGraficos.TabIndex = 3;
            // 
            // panelGraficoPizza
            // 
            this.panelGraficoPizza.Controls.Add(this.lblGraficoPizza);
            this.panelGraficoPizza.Controls.Add(this.chartPizza);
            this.panelGraficoPizza.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelGraficoPizza.Location = new System.Drawing.Point(527, 15);
            this.panelGraficoPizza.Name = "panelGraficoPizza";
            this.panelGraficoPizza.Size = new System.Drawing.Size(475, 230);
            this.panelGraficoPizza.TabIndex = 1;
            // 
            // lblGraficoPizza
            // 
            this.lblGraficoPizza.AutoSize = true;
            this.lblGraficoPizza.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGraficoPizza.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGraficoPizza.ForeColor = System.Drawing.Color.Black;
            this.lblGraficoPizza.Location = new System.Drawing.Point(0, 0);
            this.lblGraficoPizza.Name = "lblGraficoPizza";
            this.lblGraficoPizza.Size = new System.Drawing.Size(242, 20);
            this.lblGraficoPizza.TabIndex = 0;
            this.lblGraficoPizza.Text = "Valores por Forma de Pagamento";
            // 
            // chartPizza
            // 
            this.chartPizza.BackColor = System.Drawing.Color.White;
            this.chartPizza.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPizza.Location = new System.Drawing.Point(0, 0);
            this.chartPizza.Name = "chartPizza";
            this.chartPizza.Size = new System.Drawing.Size(475, 230);
            this.chartPizza.TabIndex = 1;
            this.chartPizza.Text = "chartPizza";
            // 
            // panelGraficoBarras
            // 
            this.panelGraficoBarras.Controls.Add(this.lblGraficoBarras);
            this.panelGraficoBarras.Controls.Add(this.chartBarras);
            this.panelGraficoBarras.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelGraficoBarras.Location = new System.Drawing.Point(15, 15);
            this.panelGraficoBarras.Name = "panelGraficoBarras";
            this.panelGraficoBarras.Size = new System.Drawing.Size(490, 230);
            this.panelGraficoBarras.TabIndex = 0;
            // 
            // lblGraficoBarras
            // 
            this.lblGraficoBarras.AutoSize = true;
            this.lblGraficoBarras.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGraficoBarras.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGraficoBarras.ForeColor = System.Drawing.Color.Black;
            this.lblGraficoBarras.Location = new System.Drawing.Point(0, 0);
            this.lblGraficoBarras.Name = "lblGraficoBarras";
            this.lblGraficoBarras.Size = new System.Drawing.Size(175, 20);
            this.lblGraficoBarras.TabIndex = 0;
            this.lblGraficoBarras.Text = "Valor Vendido por Caixa";
            // 
            // chartBarras
            // 
            this.chartBarras.BackColor = System.Drawing.Color.White;
            this.chartBarras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBarras.Location = new System.Drawing.Point(0, 0);
            this.chartBarras.Name = "chartBarras";
            this.chartBarras.Size = new System.Drawing.Size(490, 230);
            this.chartBarras.TabIndex = 1;
            this.chartBarras.Text = "chartBarras";
            // 
            // panelResumo
            // 
            this.panelResumo.BackColor = System.Drawing.Color.White;
            this.panelResumo.Controls.Add(this.dgvResumoCaixas);
            this.panelResumo.Controls.Add(this.lblResumo);
            this.panelResumo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelResumo.Location = new System.Drawing.Point(0, 550);
            this.panelResumo.Name = "panelResumo";
            this.panelResumo.Padding = new System.Windows.Forms.Padding(15);
            this.panelResumo.Size = new System.Drawing.Size(1017, 220);
            this.panelResumo.TabIndex = 4;
            // 
            // dgvResumoCaixas
            // 
            this.dgvResumoCaixas.BackgroundColor = System.Drawing.Color.White;
            this.dgvResumoCaixas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResumoCaixas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumoCaixas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResumoCaixas.Location = new System.Drawing.Point(15, 35);
            this.dgvResumoCaixas.Name = "dgvResumoCaixas";
            this.dgvResumoCaixas.Size = new System.Drawing.Size(987, 170);
            this.dgvResumoCaixas.TabIndex = 1;
            // 
            // lblResumo
            // 
            this.lblResumo.AutoSize = true;
            this.lblResumo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResumo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblResumo.ForeColor = System.Drawing.Color.Black;
            this.lblResumo.Location = new System.Drawing.Point(15, 15);
            this.lblResumo.Name = "lblResumo";
            this.lblResumo.Size = new System.Drawing.Size(181, 20);
            this.lblResumo.TabIndex = 0;
            this.lblResumo.Text = "Resumo por Caixa (PDV)";
            // 
            // FormRelatorioCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1034, 700);
            this.Controls.Add(this.panelResumo);
            this.Controls.Add(this.panelGraficos);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.panelFiltro);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatorioCaixa";
            this.Text = "Relatório de Caixas";
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.panelCards.ResumeLayout(false);
            this.pnlCardTroco.ResumeLayout(false);
            this.pnlCardTroco.PerformLayout();
            this.pnlCardTicket.ResumeLayout(false);
            this.pnlCardTicket.PerformLayout();
            this.pnlCardValor.ResumeLayout(false);
            this.pnlCardValor.PerformLayout();
            this.pnlCardCaixas.ResumeLayout(false);
            this.pnlCardCaixas.PerformLayout();
            this.panelGraficos.ResumeLayout(false);
            this.panelGraficoPizza.ResumeLayout(false);
            this.panelGraficoPizza.PerformLayout();
            this.panelGraficoBarras.ResumeLayout(false);
            this.panelGraficoBarras.PerformLayout();
            this.panelResumo.ResumeLayout(false);
            this.panelResumo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumoCaixas)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFiltro;
        private System.Windows.Forms.TextBox txtBuscaEvento;
        private System.Windows.Forms.ComboBox cmbEventoResultados;
        private System.Windows.Forms.ComboBox cmbStatusFiltro;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblResultados;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Panel panelCards;
        private ModernCard pnlCardTroco;
        private System.Windows.Forms.Label lblTrocoTotalValor;
        private System.Windows.Forms.Label lblTrocoTotalLabel;
        private ModernCard pnlCardTicket;
        private System.Windows.Forms.Label lblTicketMedioValor;
        private System.Windows.Forms.Label lblTicketMedioLabel;
        private ModernCard pnlCardValor;
        private System.Windows.Forms.Label lblValorVendidoValor;
        private System.Windows.Forms.Label lblValorVendidoLabel;
        private ModernCard pnlCardCaixas;
        private System.Windows.Forms.Label lblCaixasValor;
        private System.Windows.Forms.Label lblCaixasLabel;
        private System.Windows.Forms.Panel panelGraficos;
        private System.Windows.Forms.Panel panelGraficoPizza;
        private System.Windows.Forms.Label lblGraficoPizza;
        private LiveCharts.WinForms.PieChart chartPizza;
        private System.Windows.Forms.Panel panelGraficoBarras;
        private System.Windows.Forms.Label lblGraficoBarras;
        private LiveCharts.WinForms.CartesianChart chartBarras;
        private System.Windows.Forms.Panel panelResumo;
        private System.Windows.Forms.DataGridView dgvResumoCaixas;
        private System.Windows.Forms.Label lblResumo;
    }
}
