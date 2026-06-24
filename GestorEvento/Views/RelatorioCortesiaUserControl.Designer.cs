using GestorEvento.Components;

namespace GestorEvento.Views
{
    partial class RelatorioCortesiaUserControl
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
            this.lblTicketCortesiaValor = new System.Windows.Forms.Label();
            this.lblTicketCortesiaLabel = new System.Windows.Forms.Label();
            this.pnlCardValor = new GestorEvento.Components.ModernCard();
            this.lblValorCortesiaValor = new System.Windows.Forms.Label();
            this.lblValorCortesiaLabel = new System.Windows.Forms.Label();
            this.pnlCardQtde = new GestorEvento.Components.ModernCard();
            this.lblQtdCortesiaValor = new System.Windows.Forms.Label();
            this.lblQtdCortesiaLabel = new System.Windows.Forms.Label();
            this.panelGrafico = new System.Windows.Forms.Panel();
            this.lblGraficoBarras = new System.Windows.Forms.Label();
            this.chartBarras = new LiveCharts.WinForms.CartesianChart();
            this.panelProdutos = new System.Windows.Forms.Panel();
            this.dgvProdutosCortesia = new System.Windows.Forms.DataGridView();
            this.lblProdutosCortesia = new System.Windows.Forms.Label();
            this.guna2HtmlToolTip1 = new Guna.UI2.WinForms.Guna2HtmlToolTip();
            this.panelCards.SuspendLayout();
            this.pnlCardTicket.SuspendLayout();
            this.pnlCardValor.SuspendLayout();
            this.pnlCardQtde.SuspendLayout();
            this.panelGrafico.SuspendLayout();
            this.panelProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosCortesia)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.pnlCardTicket);
            this.panelCards.Controls.Add(this.pnlCardValor);
            this.panelCards.Controls.Add(this.pnlCardQtde);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 0);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.Size = new System.Drawing.Size(941, 140);
            this.panelCards.TabIndex = 2;
            // 
            // pnlCardTicket
            // 
            this.pnlCardTicket.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlCardTicket.BorderRadius = 12;
            this.pnlCardTicket.Controls.Add(this.lblTicketCortesiaValor);
            this.pnlCardTicket.Controls.Add(this.lblTicketCortesiaLabel);
            this.pnlCardTicket.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTicket.Location = new System.Drawing.Point(682, 20);
            this.pnlCardTicket.Name = "pnlCardTicket";
            this.pnlCardTicket.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTicket.ShadowSize = 4;
            this.pnlCardTicket.Size = new System.Drawing.Size(290, 100);
            this.pnlCardTicket.TabIndex = 0;
            // 
            // lblTicketCortesiaValor
            // 
            this.lblTicketCortesiaValor.AutoSize = true;
            this.lblTicketCortesiaValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTicketCortesiaValor.ForeColor = System.Drawing.Color.DimGray;
            this.lblTicketCortesiaValor.Location = new System.Drawing.Point(10, 45);
            this.lblTicketCortesiaValor.Name = "lblTicketCortesiaValor";
            this.lblTicketCortesiaValor.Size = new System.Drawing.Size(69, 37);
            this.lblTicketCortesiaValor.TabIndex = 1;
            this.lblTicketCortesiaValor.Text = "R$ -";
            // 
            // lblTicketCortesiaLabel
            // 
            this.lblTicketCortesiaLabel.AutoSize = true;
            this.lblTicketCortesiaLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTicketCortesiaLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTicketCortesiaLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTicketCortesiaLabel.Name = "lblTicketCortesiaLabel";
            this.lblTicketCortesiaLabel.Size = new System.Drawing.Size(98, 19);
            this.lblTicketCortesiaLabel.TabIndex = 0;
            this.lblTicketCortesiaLabel.Text = "Ticket Cortesia";
            // 
            // pnlCardValor
            // 
            this.pnlCardValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.pnlCardValor.BorderRadius = 12;
            this.pnlCardValor.Controls.Add(this.lblValorCortesiaValor);
            this.pnlCardValor.Controls.Add(this.lblValorCortesiaLabel);
            this.pnlCardValor.ForeColor = System.Drawing.Color.Black;
            this.pnlCardValor.Location = new System.Drawing.Point(347, 20);
            this.pnlCardValor.Name = "pnlCardValor";
            this.pnlCardValor.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardValor.ShadowSize = 4;
            this.pnlCardValor.Size = new System.Drawing.Size(290, 100);
            this.pnlCardValor.TabIndex = 0;
            // 
            // lblValorCortesiaValor
            // 
            this.lblValorCortesiaValor.AutoSize = true;
            this.lblValorCortesiaValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValorCortesiaValor.ForeColor = System.Drawing.Color.Green;
            this.lblValorCortesiaValor.Location = new System.Drawing.Point(10, 45);
            this.lblValorCortesiaValor.Name = "lblValorCortesiaValor";
            this.lblValorCortesiaValor.Size = new System.Drawing.Size(69, 37);
            this.lblValorCortesiaValor.TabIndex = 1;
            this.lblValorCortesiaValor.Text = "R$ -";
            // 
            // lblValorCortesiaLabel
            // 
            this.lblValorCortesiaLabel.AutoSize = true;
            this.lblValorCortesiaLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblValorCortesiaLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblValorCortesiaLabel.Location = new System.Drawing.Point(10, 10);
            this.lblValorCortesiaLabel.Name = "lblValorCortesiaLabel";
            this.lblValorCortesiaLabel.Size = new System.Drawing.Size(94, 19);
            this.lblValorCortesiaLabel.TabIndex = 0;
            this.lblValorCortesiaLabel.Text = "Valor Cortesia";
            // 
            // pnlCardQtde
            // 
            this.pnlCardQtde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlCardQtde.BorderRadius = 12;
            this.pnlCardQtde.Controls.Add(this.lblQtdCortesiaValor);
            this.pnlCardQtde.Controls.Add(this.lblQtdCortesiaLabel);
            this.pnlCardQtde.ForeColor = System.Drawing.Color.Black;
            this.pnlCardQtde.Location = new System.Drawing.Point(12, 20);
            this.pnlCardQtde.Name = "pnlCardQtde";
            this.pnlCardQtde.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardQtde.ShadowSize = 4;
            this.pnlCardQtde.Size = new System.Drawing.Size(290, 100);
            this.pnlCardQtde.TabIndex = 0;
            // 
            // lblQtdCortesiaValor
            // 
            this.lblQtdCortesiaValor.AutoSize = true;
            this.lblQtdCortesiaValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblQtdCortesiaValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblQtdCortesiaValor.Location = new System.Drawing.Point(10, 40);
            this.lblQtdCortesiaValor.Name = "lblQtdCortesiaValor";
            this.lblQtdCortesiaValor.Size = new System.Drawing.Size(37, 51);
            this.lblQtdCortesiaValor.TabIndex = 1;
            this.lblQtdCortesiaValor.Text = "-";
            // 
            // lblQtdCortesiaLabel
            // 
            this.lblQtdCortesiaLabel.AutoSize = true;
            this.lblQtdCortesiaLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQtdCortesiaLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblQtdCortesiaLabel.Location = new System.Drawing.Point(10, 10);
            this.lblQtdCortesiaLabel.Name = "lblQtdCortesiaLabel";
            this.lblQtdCortesiaLabel.Size = new System.Drawing.Size(135, 19);
            this.lblQtdCortesiaLabel.TabIndex = 0;
            this.lblQtdCortesiaLabel.Text = "Quantidade Cortesia";
            // 
            // panelGrafico
            // 
            this.panelGrafico.BackColor = System.Drawing.Color.White;
            this.panelGrafico.Controls.Add(this.lblGraficoBarras);
            this.panelGrafico.Controls.Add(this.chartBarras);
            this.panelGrafico.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGrafico.Location = new System.Drawing.Point(0, 140);
            this.panelGrafico.Name = "panelGrafico";
            this.panelGrafico.Padding = new System.Windows.Forms.Padding(15);
            this.panelGrafico.Size = new System.Drawing.Size(941, 250);
            this.panelGrafico.TabIndex = 3;
            // 
            // lblGraficoBarras
            // 
            this.lblGraficoBarras.AutoSize = true;
            this.lblGraficoBarras.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGraficoBarras.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGraficoBarras.ForeColor = System.Drawing.Color.Black;
            this.lblGraficoBarras.Location = new System.Drawing.Point(15, 15);
            this.lblGraficoBarras.Name = "lblGraficoBarras";
            this.lblGraficoBarras.Size = new System.Drawing.Size(142, 20);
            this.lblGraficoBarras.TabIndex = 0;
            this.lblGraficoBarras.Text = "Cortesias por Caixa";
            // 
            // chartBarras
            // 
            this.chartBarras.BackColor = System.Drawing.Color.White;
            this.chartBarras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBarras.Location = new System.Drawing.Point(15, 15);
            this.chartBarras.Name = "chartBarras";
            this.chartBarras.Size = new System.Drawing.Size(911, 220);
            this.chartBarras.TabIndex = 1;
            this.chartBarras.Text = "cartesianChart1";
            // 
            // panelProdutos
            // 
            this.panelProdutos.BackColor = System.Drawing.Color.White;
            this.panelProdutos.Controls.Add(this.dgvProdutosCortesia);
            this.panelProdutos.Controls.Add(this.lblProdutosCortesia);
            this.panelProdutos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProdutos.Location = new System.Drawing.Point(0, 390);
            this.panelProdutos.Name = "panelProdutos";
            this.panelProdutos.Padding = new System.Windows.Forms.Padding(15);
            this.panelProdutos.Size = new System.Drawing.Size(941, 360);
            this.panelProdutos.TabIndex = 4;
            // 
            // dgvProdutosCortesia
            // 
            this.dgvProdutosCortesia.BackgroundColor = System.Drawing.Color.White;
            this.dgvProdutosCortesia.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProdutosCortesia.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProdutosCortesia.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProdutosCortesia.Location = new System.Drawing.Point(15, 35);
            this.dgvProdutosCortesia.Name = "dgvProdutosCortesia";
            this.dgvProdutosCortesia.Size = new System.Drawing.Size(911, 310);
            this.dgvProdutosCortesia.TabIndex = 1;
            // 
            // lblProdutosCortesia
            // 
            this.lblProdutosCortesia.AutoSize = true;
            this.lblProdutosCortesia.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProdutosCortesia.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblProdutosCortesia.ForeColor = System.Drawing.Color.Black;
            this.lblProdutosCortesia.Location = new System.Drawing.Point(15, 15);
            this.lblProdutosCortesia.Name = "lblProdutosCortesia";
            this.lblProdutosCortesia.Size = new System.Drawing.Size(81, 20);
            this.lblProdutosCortesia.TabIndex = 0;
            this.lblProdutosCortesia.Text = "Detalhado";
            // 
            // guna2HtmlToolTip1
            // 
            this.guna2HtmlToolTip1.AllowLinksHandling = true;
            this.guna2HtmlToolTip1.MaximumSize = new System.Drawing.Size(0, 0);
            // 
            // RelatorioCortesiaUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelProdutos);
            this.Controls.Add(this.panelGrafico);
            this.Controls.Add(this.panelCards);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "RelatorioCortesiaUserControl";
            this.Size = new System.Drawing.Size(941, 402);
            this.panelCards.ResumeLayout(false);
            this.pnlCardTicket.ResumeLayout(false);
            this.pnlCardTicket.PerformLayout();
            this.pnlCardValor.ResumeLayout(false);
            this.pnlCardValor.PerformLayout();
            this.pnlCardQtde.ResumeLayout(false);
            this.pnlCardQtde.PerformLayout();
            this.panelGrafico.ResumeLayout(false);
            this.panelGrafico.PerformLayout();
            this.panelProdutos.ResumeLayout(false);
            this.panelProdutos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosCortesia)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelCards;
        private ModernCard pnlCardQtde;
        private System.Windows.Forms.Label lblQtdCortesiaValor;
        private System.Windows.Forms.Label lblQtdCortesiaLabel;
        private ModernCard pnlCardValor;
        private System.Windows.Forms.Label lblValorCortesiaValor;
        private System.Windows.Forms.Label lblValorCortesiaLabel;
        private ModernCard pnlCardTicket;
        private System.Windows.Forms.Label lblTicketCortesiaValor;
        private System.Windows.Forms.Label lblTicketCortesiaLabel;
        private System.Windows.Forms.Panel panelGrafico;
        private System.Windows.Forms.Label lblGraficoBarras;
        private LiveCharts.WinForms.CartesianChart chartBarras;
        private System.Windows.Forms.Panel panelProdutos;
        private System.Windows.Forms.DataGridView dgvProdutosCortesia;
        private System.Windows.Forms.Label lblProdutosCortesia;
        private Guna.UI2.WinForms.Guna2HtmlToolTip guna2HtmlToolTip1;
    }
}
