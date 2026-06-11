using GestorEvento.Components;

namespace GestorEvento.Views
{
    partial class RelatorioCaixaUserControl
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

        private void InitializeComponent()
        {
            this.panelCards = new System.Windows.Forms.Panel();
            this.pnlCardTicket = new GestorEvento.Components.ModernCard();
            this.lblTicketMedioValor = new System.Windows.Forms.Label();
            this.lblTicketMedioLabel = new System.Windows.Forms.Label();
            this.pnlCardTroco = new GestorEvento.Components.ModernCard();
            this.lblTrocoTotalValor = new System.Windows.Forms.Label();
            this.lblTrocoTotalLabel = new System.Windows.Forms.Label();
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
            
            this.panelCards.SuspendLayout();
            this.pnlCardTicket.SuspendLayout();
            this.pnlCardTroco.SuspendLayout();
            this.pnlCardValor.SuspendLayout();
            this.pnlCardCaixas.SuspendLayout();
            this.panelGraficos.SuspendLayout();
            this.panelGraficoPizza.SuspendLayout();
            this.panelGraficoBarras.SuspendLayout();
            this.panelResumo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvResumoCaixas)).BeginInit();
            this.SuspendLayout();

            // panelCards
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.pnlCardTicket);
            this.panelCards.Controls.Add(this.pnlCardTroco);
            this.panelCards.Controls.Add(this.pnlCardValor);
            this.panelCards.Controls.Add(this.pnlCardCaixas);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 0);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.Size = new System.Drawing.Size(992, 140);
            this.panelCards.TabIndex = 2;

            // pnlCardTicket
            this.pnlCardTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlCardTicket.BorderRadius = 12;
            this.pnlCardTicket.Controls.Add(this.lblTicketMedioValor);
            this.pnlCardTicket.Controls.Add(this.lblTicketMedioLabel);
            this.pnlCardTicket.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTicket.Location = new System.Drawing.Point(682, 20);
            this.pnlCardTicket.Name = "pnlCardTicket";
            this.pnlCardTicket.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTicket.ShadowSize = 4;
            this.pnlCardTicket.Size = new System.Drawing.Size(290, 100);
            this.pnlCardTicket.TabIndex = 0;

            // lblTicketMedioValor
            this.lblTicketMedioValor.AutoSize = true;
            this.lblTicketMedioValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTicketMedioValor.ForeColor = System.Drawing.Color.DimGray;
            this.lblTicketMedioValor.Location = new System.Drawing.Point(10, 45);
            this.lblTicketMedioValor.Name = "lblTicketMedioValor";
            this.lblTicketMedioValor.Size = new System.Drawing.Size(69, 37);
            this.lblTicketMedioValor.TabIndex = 1;
            this.lblTicketMedioValor.Text = "R$ -";

            // lblTicketMedioLabel
            this.lblTicketMedioLabel.AutoSize = true;
            this.lblTicketMedioLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTicketMedioLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTicketMedioLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTicketMedioLabel.Name = "lblTicketMedioLabel";
            this.lblTicketMedioLabel.Size = new System.Drawing.Size(100, 19);
            this.lblTicketMedioLabel.TabIndex = 0;
            this.lblTicketMedioLabel.Text = "Ticket Médio";

            // pnlCardTroco
            this.pnlCardTroco.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(230)))));
            this.pnlCardTroco.BorderRadius = 12;
            this.pnlCardTroco.Controls.Add(this.lblTrocoTotalValor);
            this.pnlCardTroco.Controls.Add(this.lblTrocoTotalLabel);
            this.pnlCardTroco.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTroco.Location = new System.Drawing.Point(347, 20);
            this.pnlCardTroco.Name = "pnlCardTroco";
            this.pnlCardTroco.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTroco.ShadowSize = 4;
            this.pnlCardTroco.Size = new System.Drawing.Size(290, 100);
            this.pnlCardTroco.TabIndex = 0;

            // lblTrocoTotalValor
            this.lblTrocoTotalValor.AutoSize = true;
            this.lblTrocoTotalValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTrocoTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.lblTrocoTotalValor.Location = new System.Drawing.Point(10, 45);
            this.lblTrocoTotalValor.Name = "lblTrocoTotalValor";
            this.lblTrocoTotalValor.Size = new System.Drawing.Size(69, 37);
            this.lblTrocoTotalValor.TabIndex = 1;
            this.lblTrocoTotalValor.Text = "R$ -";

            // lblTrocoTotalLabel
            this.lblTrocoTotalLabel.AutoSize = true;
            this.lblTrocoTotalLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrocoTotalLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTrocoTotalLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTrocoTotalLabel.Name = "lblTrocoTotalLabel";
            this.lblTrocoTotalLabel.Size = new System.Drawing.Size(100, 19);
            this.lblTrocoTotalLabel.TabIndex = 0;
            this.lblTrocoTotalLabel.Text = "Troco Total";

            // pnlCardValor
            this.pnlCardValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.pnlCardValor.BorderRadius = 12;
            this.pnlCardValor.Controls.Add(this.lblValorVendidoValor);
            this.pnlCardValor.Controls.Add(this.lblValorVendidoLabel);
            this.pnlCardValor.ForeColor = System.Drawing.Color.Black;
            this.pnlCardValor.Location = new System.Drawing.Point(12, 20);
            this.pnlCardValor.Name = "pnlCardValor";
            this.pnlCardValor.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardValor.ShadowSize = 4;
            this.pnlCardValor.Size = new System.Drawing.Size(290, 100);
            this.pnlCardValor.TabIndex = 0;

            // lblValorVendidoValor
            this.lblValorVendidoValor.AutoSize = true;
            this.lblValorVendidoValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValorVendidoValor.ForeColor = System.Drawing.Color.Green;
            this.lblValorVendidoValor.Location = new System.Drawing.Point(10, 45);
            this.lblValorVendidoValor.Name = "lblValorVendidoValor";
            this.lblValorVendidoValor.Size = new System.Drawing.Size(69, 37);
            this.lblValorVendidoValor.TabIndex = 1;
            this.lblValorVendidoValor.Text = "R$ -";

            // lblValorVendidoLabel
            this.lblValorVendidoLabel.AutoSize = true;
            this.lblValorVendidoLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblValorVendidoLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblValorVendidoLabel.Location = new System.Drawing.Point(10, 10);
            this.lblValorVendidoLabel.Name = "lblValorVendidoLabel";
            this.lblValorVendidoLabel.Size = new System.Drawing.Size(127, 19);
            this.lblValorVendidoLabel.TabIndex = 0;
            this.lblValorVendidoLabel.Text = "Valor Total Vendido";

            // pnlCardCaixas
            this.pnlCardCaixas.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlCardCaixas.BorderRadius = 12;
            this.pnlCardCaixas.Controls.Add(this.lblCaixasValor);
            this.pnlCardCaixas.Controls.Add(this.lblCaixasLabel);
            this.pnlCardCaixas.ForeColor = System.Drawing.Color.Black;
            this.pnlCardCaixas.Location = new System.Drawing.Point(347, 20);
            this.pnlCardCaixas.Name = "pnlCardCaixas";
            this.pnlCardCaixas.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardCaixas.ShadowSize = 4;
            this.pnlCardCaixas.Size = new System.Drawing.Size(290, 100);
            this.pnlCardCaixas.TabIndex = 0;

            // lblCaixasValor
            this.lblCaixasValor.AutoSize = true;
            this.lblCaixasValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblCaixasValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblCaixasValor.Location = new System.Drawing.Point(10, 40);
            this.lblCaixasValor.Name = "lblCaixasValor";
            this.lblCaixasValor.Size = new System.Drawing.Size(37, 51);
            this.lblCaixasValor.TabIndex = 1;
            this.lblCaixasValor.Text = "-";

            // lblCaixasLabel
            this.lblCaixasLabel.AutoSize = true;
            this.lblCaixasLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCaixasLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblCaixasLabel.Location = new System.Drawing.Point(10, 10);
            this.lblCaixasLabel.Name = "lblCaixasLabel";
            this.lblCaixasLabel.Size = new System.Drawing.Size(110, 19);
            this.lblCaixasLabel.TabIndex = 0;
            this.lblCaixasLabel.Text = "Total de Caixas";

            // panelGraficos
            this.panelGraficos.BackColor = System.Drawing.Color.White;
            this.panelGraficos.Controls.Add(this.panelGraficoPizza);
            this.panelGraficos.Controls.Add(this.panelGraficoBarras);
            this.panelGraficos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGraficos.Location = new System.Drawing.Point(0, 140);
            this.panelGraficos.Name = "panelGraficos";
            this.panelGraficos.Padding = new System.Windows.Forms.Padding(15);
            this.panelGraficos.Size = new System.Drawing.Size(992, 320);
            this.panelGraficos.TabIndex = 3;

            // panelGraficoPizza
            this.panelGraficoPizza.Controls.Add(this.lblGraficoPizza);
            this.panelGraficoPizza.Controls.Add(this.chartPizza);
            this.panelGraficoPizza.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelGraficoPizza.Location = new System.Drawing.Point(502, 15);
            this.panelGraficoPizza.Name = "panelGraficoPizza";
            this.panelGraficoPizza.Size = new System.Drawing.Size(475, 290);
            this.panelGraficoPizza.TabIndex = 1;

            // lblGraficoPizza
            this.lblGraficoPizza.AutoSize = true;
            this.lblGraficoPizza.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGraficoPizza.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGraficoPizza.ForeColor = System.Drawing.Color.Black;
            this.lblGraficoPizza.Location = new System.Drawing.Point(0, 0);
            this.lblGraficoPizza.Name = "lblGraficoPizza";
            this.lblGraficoPizza.Size = new System.Drawing.Size(242, 20);
            this.lblGraficoPizza.TabIndex = 0;
            this.lblGraficoPizza.Text = "Formas de Pagamento";

            // chartPizza
            this.chartPizza.BackColor = System.Drawing.Color.White;
            this.chartPizza.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartPizza.Location = new System.Drawing.Point(0, 0);
            this.chartPizza.Name = "chartPizza";
            this.chartPizza.Size = new System.Drawing.Size(475, 290);
            this.chartPizza.TabIndex = 1;
            this.chartPizza.Text = "pieChart1";

            // panelGraficoBarras
            this.panelGraficoBarras.Controls.Add(this.lblGraficoBarras);
            this.panelGraficoBarras.Controls.Add(this.chartBarras);
            this.panelGraficoBarras.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelGraficoBarras.Location = new System.Drawing.Point(15, 15);
            this.panelGraficoBarras.Name = "panelGraficoBarras";
            this.panelGraficoBarras.Size = new System.Drawing.Size(490, 290);
            this.panelGraficoBarras.TabIndex = 0;

            // lblGraficoBarras
            this.lblGraficoBarras.AutoSize = true;
            this.lblGraficoBarras.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGraficoBarras.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGraficoBarras.ForeColor = System.Drawing.Color.Black;
            this.lblGraficoBarras.Location = new System.Drawing.Point(0, 0);
            this.lblGraficoBarras.Name = "lblGraficoBarras";
            this.lblGraficoBarras.Size = new System.Drawing.Size(156, 20);
            this.lblGraficoBarras.TabIndex = 0;
            this.lblGraficoBarras.Text = "Vendas por Caixa";

            // chartBarras
            this.chartBarras.BackColor = System.Drawing.Color.White;
            this.chartBarras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBarras.Location = new System.Drawing.Point(0, 0);
            this.chartBarras.Name = "chartBarras";
            this.chartBarras.Size = new System.Drawing.Size(490, 290);
            this.chartBarras.TabIndex = 1;
            this.chartBarras.Text = "cartesianChart1";

            // panelResumo
            this.panelResumo.BackColor = System.Drawing.Color.White;
            this.panelResumo.Controls.Add(this.dgvResumoCaixas);
            this.panelResumo.Controls.Add(this.lblResumo);
            this.panelResumo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelResumo.Location = new System.Drawing.Point(0, 460);
            this.panelResumo.Name = "panelResumo";
            this.panelResumo.Padding = new System.Windows.Forms.Padding(15);
            this.panelResumo.Size = new System.Drawing.Size(992, 220);
            this.panelResumo.TabIndex = 4;

            // dgvResumoCaixas
            this.dgvResumoCaixas.BackgroundColor = System.Drawing.Color.White;
            this.dgvResumoCaixas.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvResumoCaixas.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvResumoCaixas.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvResumoCaixas.Location = new System.Drawing.Point(15, 35);
            this.dgvResumoCaixas.Name = "dgvResumoCaixas";
            this.dgvResumoCaixas.Size = new System.Drawing.Size(962, 0);
            this.dgvResumoCaixas.TabIndex = 1;

            // lblResumo
            this.lblResumo.AutoSize = true;
            this.lblResumo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblResumo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblResumo.ForeColor = System.Drawing.Color.Black;
            this.lblResumo.Location = new System.Drawing.Point(15, 15);
            this.lblResumo.Name = "lblResumo";
            this.lblResumo.Size = new System.Drawing.Size(160, 20);
            this.lblResumo.TabIndex = 0;
            this.lblResumo.Text = "Resumo por Caixa";

            // RelatorioCaixaUserControl
            this.AutoScroll = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelResumo);
            this.Controls.Add(this.panelGraficos);
            this.Controls.Add(this.panelCards);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "RelatorioCaixaUserControl";
            this.Size = new System.Drawing.Size(992, 402);

            this.panelCards.ResumeLayout(false);
            this.pnlCardTicket.ResumeLayout(false);
            this.pnlCardTicket.PerformLayout();
            this.pnlCardTroco.ResumeLayout(false);
            this.pnlCardTroco.PerformLayout();
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

        private System.Windows.Forms.Panel panelCards;
        private ModernCard pnlCardCaixas;
        private System.Windows.Forms.Label lblCaixasValor;
        private System.Windows.Forms.Label lblCaixasLabel;
        private ModernCard pnlCardValor;
        private System.Windows.Forms.Label lblValorVendidoValor;
        private System.Windows.Forms.Label lblValorVendidoLabel;
        private ModernCard pnlCardTroco;
        private System.Windows.Forms.Label lblTrocoTotalValor;
        private System.Windows.Forms.Label lblTrocoTotalLabel;
        private ModernCard pnlCardTicket;
        private System.Windows.Forms.Label lblTicketMedioValor;
        private System.Windows.Forms.Label lblTicketMedioLabel;
        private System.Windows.Forms.Panel panelGraficos;
        private System.Windows.Forms.Panel panelGraficoBarras;
        private LiveCharts.WinForms.CartesianChart chartBarras;
        private System.Windows.Forms.Label lblGraficoBarras;
        private System.Windows.Forms.Panel panelGraficoPizza;
        private LiveCharts.WinForms.PieChart chartPizza;
        private System.Windows.Forms.Label lblGraficoPizza;
        private System.Windows.Forms.Panel panelResumo;
        private System.Windows.Forms.DataGridView dgvResumoCaixas;
        private System.Windows.Forms.Label lblResumo;
    }
}
