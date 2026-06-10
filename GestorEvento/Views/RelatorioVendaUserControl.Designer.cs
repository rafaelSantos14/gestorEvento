using GestorEvento.Components;

namespace GestorEvento.Views
{
    partial class RelatorioVendaUserControl
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
            this.pnlCardTroco = new GestorEvento.Components.ModernCard();
            this.lblTrocoValor = new System.Windows.Forms.Label();
            this.lblTrocoLabel = new System.Windows.Forms.Label();
            this.pnlCardValor = new GestorEvento.Components.ModernCard();
            this.lblValorVendidoValor = new System.Windows.Forms.Label();
            this.lblValorVendidoLabel = new System.Windows.Forms.Label();
            this.pnlCardQtde = new GestorEvento.Components.ModernCard();
            this.lblQtdeValor = new System.Windows.Forms.Label();
            this.lblQtdeLabel = new System.Windows.Forms.Label();
            this.panelGraficos = new System.Windows.Forms.Panel();
            this.panelGraficoPizza = new System.Windows.Forms.Panel();
            this.lblGraficoPizza = new System.Windows.Forms.Label();
            this.chartPizza = new LiveCharts.WinForms.PieChart();
            this.panelGraficoBarras = new System.Windows.Forms.Panel();
            this.lblGraficoBarras = new System.Windows.Forms.Label();
            this.chartBarras = new LiveCharts.WinForms.CartesianChart();
            this.panelProdutos = new System.Windows.Forms.Panel();
            this.dgvProdutosVendidos = new System.Windows.Forms.DataGridView();
            this.lblProdutosVendidos = new System.Windows.Forms.Label();
            
            this.panelCards.SuspendLayout();
            this.pnlCardTroco.SuspendLayout();
            this.pnlCardValor.SuspendLayout();
            this.pnlCardQtde.SuspendLayout();
            this.panelGraficos.SuspendLayout();
            this.panelGraficoPizza.SuspendLayout();
            this.panelGraficoBarras.SuspendLayout();
            this.panelProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosVendidos)).BeginInit();
            this.SuspendLayout();

            // panelCards
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.pnlCardTroco);
            this.panelCards.Controls.Add(this.pnlCardValor);
            this.panelCards.Controls.Add(this.pnlCardQtde);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 0);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.Size = new System.Drawing.Size(992, 140);
            this.panelCards.TabIndex = 2;

            // pnlCardTroco
            this.pnlCardTroco.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(230)))));
            this.pnlCardTroco.BorderRadius = 12;
            this.pnlCardTroco.Controls.Add(this.lblTrocoValor);
            this.pnlCardTroco.Controls.Add(this.lblTrocoLabel);
            this.pnlCardTroco.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTroco.Location = new System.Drawing.Point(682, 20);
            this.pnlCardTroco.Name = "pnlCardTroco";
            this.pnlCardTroco.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTroco.ShadowSize = 4;
            this.pnlCardTroco.Size = new System.Drawing.Size(290, 100);
            this.pnlCardTroco.TabIndex = 0;

            // lblTrocoValor
            this.lblTrocoValor.AutoSize = true;
            this.lblTrocoValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTrocoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.lblTrocoValor.Location = new System.Drawing.Point(10, 45);
            this.lblTrocoValor.Name = "lblTrocoValor";
            this.lblTrocoValor.Size = new System.Drawing.Size(69, 37);
            this.lblTrocoValor.TabIndex = 1;
            this.lblTrocoValor.Text = "R$ -";

            // lblTrocoLabel
            this.lblTrocoLabel.AutoSize = true;
            this.lblTrocoLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrocoLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTrocoLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTrocoLabel.Name = "lblTrocoLabel";
            this.lblTrocoLabel.Size = new System.Drawing.Size(110, 19);
            this.lblTrocoLabel.TabIndex = 0;
            this.lblTrocoLabel.Text = "Valor Total Troco";

            // pnlCardValor
            this.pnlCardValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.pnlCardValor.BorderRadius = 12;
            this.pnlCardValor.Controls.Add(this.lblValorVendidoValor);
            this.pnlCardValor.Controls.Add(this.lblValorVendidoLabel);
            this.pnlCardValor.ForeColor = System.Drawing.Color.Black;
            this.pnlCardValor.Location = new System.Drawing.Point(347, 20);
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

            // pnlCardQtde
            this.pnlCardQtde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlCardQtde.BorderRadius = 12;
            this.pnlCardQtde.Controls.Add(this.lblQtdeValor);
            this.pnlCardQtde.Controls.Add(this.lblQtdeLabel);
            this.pnlCardQtde.ForeColor = System.Drawing.Color.Black;
            this.pnlCardQtde.Location = new System.Drawing.Point(12, 20);
            this.pnlCardQtde.Name = "pnlCardQtde";
            this.pnlCardQtde.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardQtde.ShadowSize = 4;
            this.pnlCardQtde.Size = new System.Drawing.Size(290, 100);
            this.pnlCardQtde.TabIndex = 0;

            // lblQtdeValor
            this.lblQtdeValor.AutoSize = true;
            this.lblQtdeValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblQtdeValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblQtdeValor.Location = new System.Drawing.Point(10, 40);
            this.lblQtdeValor.Name = "lblQtdeValor";
            this.lblQtdeValor.Size = new System.Drawing.Size(37, 51);
            this.lblQtdeValor.TabIndex = 1;
            this.lblQtdeValor.Text = "-";

            // lblQtdeLabel
            this.lblQtdeLabel.AutoSize = true;
            this.lblQtdeLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQtdeLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblQtdeLabel.Location = new System.Drawing.Point(10, 10);
            this.lblQtdeLabel.Name = "lblQtdeLabel";
            this.lblQtdeLabel.Size = new System.Drawing.Size(181, 19);
            this.lblQtdeLabel.TabIndex = 0;
            this.lblQtdeLabel.Text = "Quantidade Total de Vendas";

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
            this.lblGraficoPizza.Text = "Valores por Forma de Pagamento";

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
            this.lblGraficoBarras.Size = new System.Drawing.Size(214, 20);
            this.lblGraficoBarras.TabIndex = 0;
            this.lblGraficoBarras.Text = "Valor Total Vendido por Caixa";

            // chartBarras
            this.chartBarras.BackColor = System.Drawing.Color.White;
            this.chartBarras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBarras.Location = new System.Drawing.Point(0, 0);
            this.chartBarras.Name = "chartBarras";
            this.chartBarras.Size = new System.Drawing.Size(490, 290);
            this.chartBarras.TabIndex = 1;
            this.chartBarras.Text = "cartesianChart1";

            // panelProdutos
            this.panelProdutos.BackColor = System.Drawing.Color.White;
            this.panelProdutos.Controls.Add(this.dgvProdutosVendidos);
            this.panelProdutos.Controls.Add(this.lblProdutosVendidos);
            this.panelProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelProdutos.Location = new System.Drawing.Point(0, 460);
            this.panelProdutos.Name = "panelProdutos";
            this.panelProdutos.Padding = new System.Windows.Forms.Padding(15);
            this.panelProdutos.Size = new System.Drawing.Size(992, 0);
            this.panelProdutos.TabIndex = 4;

            // dgvProdutosVendidos
            this.dgvProdutosVendidos.BackgroundColor = System.Drawing.Color.White;
            this.dgvProdutosVendidos.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvProdutosVendidos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvProdutosVendidos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProdutosVendidos.Location = new System.Drawing.Point(15, 35);
            this.dgvProdutosVendidos.Name = "dgvProdutosVendidos";
            this.dgvProdutosVendidos.Size = new System.Drawing.Size(962, 0);
            this.dgvProdutosVendidos.TabIndex = 1;

            // lblProdutosVendidos
            this.lblProdutosVendidos.AutoSize = true;
            this.lblProdutosVendidos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProdutosVendidos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblProdutosVendidos.ForeColor = System.Drawing.Color.Black;
            this.lblProdutosVendidos.Location = new System.Drawing.Point(15, 15);
            this.lblProdutosVendidos.Name = "lblProdutosVendidos";
            this.lblProdutosVendidos.Size = new System.Drawing.Size(215, 20);
            this.lblProdutosVendidos.TabIndex = 0;
            this.lblProdutosVendidos.Text = "Produtos Vendidos no Evento";

            // RelatorioVendaUserControl
            this.AutoScroll = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelProdutos);
            this.Controls.Add(this.panelGraficos);
            this.Controls.Add(this.panelCards);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "RelatorioVendaUserControl";
            this.Size = new System.Drawing.Size(992, 402);

            this.panelCards.ResumeLayout(false);
            this.pnlCardTroco.ResumeLayout(false);
            this.pnlCardTroco.PerformLayout();
            this.pnlCardValor.ResumeLayout(false);
            this.pnlCardValor.PerformLayout();
            this.pnlCardQtde.ResumeLayout(false);
            this.pnlCardQtde.PerformLayout();
            this.panelGraficos.ResumeLayout(false);
            this.panelGraficoPizza.ResumeLayout(false);
            this.panelGraficoPizza.PerformLayout();
            this.panelGraficoBarras.ResumeLayout(false);
            this.panelGraficoBarras.PerformLayout();
            this.panelProdutos.ResumeLayout(false);
            this.panelProdutos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosVendidos)).EndInit();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel panelCards;
        private ModernCard pnlCardQtde;
        private System.Windows.Forms.Label lblQtdeValor;
        private System.Windows.Forms.Label lblQtdeLabel;
        private ModernCard pnlCardValor;
        private System.Windows.Forms.Label lblValorVendidoValor;
        private System.Windows.Forms.Label lblValorVendidoLabel;
        private ModernCard pnlCardTroco;
        private System.Windows.Forms.Label lblTrocoValor;
        private System.Windows.Forms.Label lblTrocoLabel;
        private System.Windows.Forms.Panel panelGraficos;
        private System.Windows.Forms.Panel panelGraficoBarras;
        private LiveCharts.WinForms.CartesianChart chartBarras;
        private System.Windows.Forms.Label lblGraficoBarras;
        private System.Windows.Forms.Panel panelGraficoPizza;
        private LiveCharts.WinForms.PieChart chartPizza;
        private System.Windows.Forms.Label lblGraficoPizza;
        private System.Windows.Forms.Panel panelProdutos;
        private System.Windows.Forms.DataGridView dgvProdutosVendidos;
        private System.Windows.Forms.Label lblProdutosVendidos;
    }
}
