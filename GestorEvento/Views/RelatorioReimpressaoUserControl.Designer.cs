using GestorEvento.Components;

namespace GestorEvento.Views
{
    partial class RelatorioReimpressaoUserControl
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
            this.pnlCardValor = new GestorEvento.Components.ModernCard();
            this.lblValorTotalValor = new System.Windows.Forms.Label();
            this.lblValorTotalLabel = new System.Windows.Forms.Label();
            this.pnlCardQtde = new GestorEvento.Components.ModernCard();
            this.lblTotalReimpressoesValor = new System.Windows.Forms.Label();
            this.lblTotalReimpressoesLabel = new System.Windows.Forms.Label();
            this.chartMotivos = new LiveCharts.WinForms.CartesianChart();
            this.lblPorProduto = new System.Windows.Forms.Label();
            this.dgvPorProduto = new System.Windows.Forms.DataGridView();
            this.lblDetalhado = new System.Windows.Forms.Label();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.panelCards.SuspendLayout();
            this.pnlCardValor.SuspendLayout();
            this.pnlCardQtde.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorProduto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.SuspendLayout();
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.pnlCardValor);
            this.panelCards.Controls.Add(this.pnlCardQtde);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 0);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.Size = new System.Drawing.Size(958, 140);
            this.panelCards.TabIndex = 0;
            // 
            // pnlCardValor
            // 
            this.pnlCardValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.pnlCardValor.BorderRadius = 12;
            this.pnlCardValor.Controls.Add(this.lblValorTotalValor);
            this.pnlCardValor.Controls.Add(this.lblValorTotalLabel);
            this.pnlCardValor.ForeColor = System.Drawing.Color.Black;
            this.pnlCardValor.Location = new System.Drawing.Point(347, 20);
            this.pnlCardValor.Name = "pnlCardValor";
            this.pnlCardValor.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardValor.ShadowSize = 4;
            this.pnlCardValor.Size = new System.Drawing.Size(290, 100);
            this.pnlCardValor.TabIndex = 0;
            // 
            // lblValorTotalValor
            // 
            this.lblValorTotalValor.AutoSize = true;
            this.lblValorTotalValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValorTotalValor.ForeColor = System.Drawing.Color.Green;
            this.lblValorTotalValor.Location = new System.Drawing.Point(10, 45);
            this.lblValorTotalValor.Name = "lblValorTotalValor";
            this.lblValorTotalValor.Size = new System.Drawing.Size(69, 37);
            this.lblValorTotalValor.TabIndex = 1;
            this.lblValorTotalValor.Text = "R$ -";
            // 
            // lblValorTotalLabel
            // 
            this.lblValorTotalLabel.AutoSize = true;
            this.lblValorTotalLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblValorTotalLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblValorTotalLabel.Location = new System.Drawing.Point(10, 10);
            this.lblValorTotalLabel.Name = "lblValorTotalLabel";
            this.lblValorTotalLabel.Size = new System.Drawing.Size(73, 19);
            this.lblValorTotalLabel.TabIndex = 0;
            this.lblValorTotalLabel.Text = "Valor Total";
            // 
            // pnlCardQtde
            // 
            this.pnlCardQtde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlCardQtde.BorderRadius = 12;
            this.pnlCardQtde.Controls.Add(this.lblTotalReimpressoesValor);
            this.pnlCardQtde.Controls.Add(this.lblTotalReimpressoesLabel);
            this.pnlCardQtde.ForeColor = System.Drawing.Color.Black;
            this.pnlCardQtde.Location = new System.Drawing.Point(12, 20);
            this.pnlCardQtde.Name = "pnlCardQtde";
            this.pnlCardQtde.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardQtde.ShadowSize = 4;
            this.pnlCardQtde.Size = new System.Drawing.Size(290, 100);
            this.pnlCardQtde.TabIndex = 0;
            // 
            // lblTotalReimpressoesValor
            // 
            this.lblTotalReimpressoesValor.AutoSize = true;
            this.lblTotalReimpressoesValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalReimpressoesValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblTotalReimpressoesValor.Location = new System.Drawing.Point(10, 40);
            this.lblTotalReimpressoesValor.Name = "lblTotalReimpressoesValor";
            this.lblTotalReimpressoesValor.Size = new System.Drawing.Size(37, 51);
            this.lblTotalReimpressoesValor.TabIndex = 1;
            this.lblTotalReimpressoesValor.Text = "-";
            // 
            // lblTotalReimpressoesLabel
            // 
            this.lblTotalReimpressoesLabel.AutoSize = true;
            this.lblTotalReimpressoesLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalReimpressoesLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTotalReimpressoesLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTotalReimpressoesLabel.Name = "lblTotalReimpressoesLabel";
            this.lblTotalReimpressoesLabel.Size = new System.Drawing.Size(144, 19);
            this.lblTotalReimpressoesLabel.TabIndex = 0;
            this.lblTotalReimpressoesLabel.Text = "Total de Reimpressões";
            // 
            // chartMotivos
            // 
            this.chartMotivos.BackColor = System.Drawing.Color.White;
            this.chartMotivos.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartMotivos.Location = new System.Drawing.Point(0, 140);
            this.chartMotivos.Name = "chartMotivos";
            this.chartMotivos.Size = new System.Drawing.Size(958, 150);
            this.chartMotivos.TabIndex = 1;
            this.chartMotivos.Text = "cartesianChart1";
            // 
            // lblPorProduto
            // 
            this.lblPorProduto.AutoSize = true;
            this.lblPorProduto.BackColor = System.Drawing.Color.White;
            this.lblPorProduto.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPorProduto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblPorProduto.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblPorProduto.Location = new System.Drawing.Point(0, 290);
            this.lblPorProduto.Name = "lblPorProduto";
            this.lblPorProduto.Padding = new System.Windows.Forms.Padding(15, 10, 15, 5);
            this.lblPorProduto.Size = new System.Drawing.Size(198, 35);
            this.lblPorProduto.TabIndex = 10;
            this.lblPorProduto.Text = "Agrupado por produto";
            // 
            // dgvPorProduto
            // 
            this.dgvPorProduto.AllowUserToAddRows = false;
            this.dgvPorProduto.BackgroundColor = System.Drawing.Color.White;
            this.dgvPorProduto.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvPorProduto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPorProduto.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvPorProduto.Location = new System.Drawing.Point(0, 325);
            this.dgvPorProduto.Name = "dgvPorProduto";
            this.dgvPorProduto.ReadOnly = true;
            this.dgvPorProduto.RowHeadersVisible = false;
            this.dgvPorProduto.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPorProduto.Size = new System.Drawing.Size(958, 270);
            this.dgvPorProduto.TabIndex = 2;
            // 
            // lblDetalhado
            // 
            this.lblDetalhado.AutoSize = true;
            this.lblDetalhado.BackColor = System.Drawing.Color.White;
            this.lblDetalhado.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetalhado.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblDetalhado.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.lblDetalhado.Location = new System.Drawing.Point(0, 595);
            this.lblDetalhado.Name = "lblDetalhado";
            this.lblDetalhado.Padding = new System.Windows.Forms.Padding(15, 10, 15, 5);
            this.lblDetalhado.Size = new System.Drawing.Size(175, 35);
            this.lblDetalhado.TabIndex = 11;
            this.lblDetalhado.Text = "Detalhado por item";
            // 
            // dgvItens
            // 
            this.dgvItens.AllowUserToAddRows = false;
            this.dgvItens.BackgroundColor = System.Drawing.Color.White;
            this.dgvItens.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItens.Dock = System.Windows.Forms.DockStyle.Top;
            this.dgvItens.Location = new System.Drawing.Point(0, 630);
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.ReadOnly = true;
            this.dgvItens.RowHeadersVisible = false;
            this.dgvItens.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvItens.Size = new System.Drawing.Size(958, 270);
            this.dgvItens.TabIndex = 3;
            // 
            // RelatorioReimpressaoUserControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.dgvItens);
            this.Controls.Add(this.lblDetalhado);
            this.Controls.Add(this.dgvPorProduto);
            this.Controls.Add(this.lblPorProduto);
            this.Controls.Add(this.chartMotivos);
            this.Controls.Add(this.panelCards);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "RelatorioReimpressaoUserControl";
            this.Size = new System.Drawing.Size(958, 620);
            this.panelCards.ResumeLayout(false);
            this.pnlCardValor.ResumeLayout(false);
            this.pnlCardValor.PerformLayout();
            this.pnlCardQtde.ResumeLayout(false);
            this.pnlCardQtde.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorProduto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Panel panelCards;
        private System.Windows.Forms.Label lblPorProduto;
        private System.Windows.Forms.Label lblDetalhado;
        private GestorEvento.Components.ModernCard pnlCardQtde;
        private System.Windows.Forms.Label lblTotalReimpressoesValor;
        private System.Windows.Forms.Label lblTotalReimpressoesLabel;
        private GestorEvento.Components.ModernCard pnlCardValor;
        private System.Windows.Forms.Label lblValorTotalValor;
        private System.Windows.Forms.Label lblValorTotalLabel;
        private LiveCharts.WinForms.CartesianChart chartMotivos;
        private System.Windows.Forms.DataGridView dgvPorProduto;
        private System.Windows.Forms.DataGridView dgvItens;
    }
}
