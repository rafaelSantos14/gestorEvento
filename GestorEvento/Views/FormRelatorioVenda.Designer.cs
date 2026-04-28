using GestorEvento.Components;

namespace GestorEvento.Views
{
    partial class FormRelatorioVenda
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFiltro = new System.Windows.Forms.Panel();
            this.btnAtualizar = new System.Windows.Forms.Button();
            this.txtBuscaEvento = new System.Windows.Forms.TextBox();
            this.cmbEventoResultados = new System.Windows.Forms.ComboBox();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblResultados = new System.Windows.Forms.Label();
            this.panelCards = new System.Windows.Forms.Panel();
            this.panelGraficos = new System.Windows.Forms.Panel();
            this.panelGraficoPizza = new System.Windows.Forms.Panel();
            this.lblGraficoPizza = new System.Windows.Forms.Label();
            this.chartPizza = new LiveCharts.WinForms.PieChart();
            this.panelGraficoBarras = new System.Windows.Forms.Panel();
            this.lblGraficoBarras = new System.Windows.Forms.Label();
            this.chartBarras = new LiveCharts.WinForms.CartesianChart();
            this.pnlCardTroco = new ModernCard();
            this.lblTrocoValor = new System.Windows.Forms.Label();
            this.lblTrocoLabel = new System.Windows.Forms.Label();
            this.pnlCardValor = new ModernCard();
            this.lblValorVendidoValor = new System.Windows.Forms.Label();
            this.lblValorVendidoLabel = new System.Windows.Forms.Label();
            this.pnlCardQtde = new ModernCard();
            this.lblQtdeValor = new System.Windows.Forms.Label();
            this.lblQtdeLabel = new System.Windows.Forms.Label();
            this.panelTitulo.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.panelGraficos.SuspendLayout();
            this.panelGraficoPizza.SuspendLayout();
            this.panelGraficoBarras.SuspendLayout();
            this.pnlCardTroco.SuspendLayout();
            this.pnlCardValor.SuspendLayout();
            this.pnlCardQtde.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTitulo.Controls.Add(this.btnFechar);
            this.panelTitulo.Controls.Add(this.btnMinimizar);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(1000, 50);
            this.panelTitulo.TabIndex = 0;
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.Transparent;
            this.btnFechar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(910, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(45, 50);
            this.btnFechar.TabIndex = 4;
            this.btnFechar.Text = "✕";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(955, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 50);
            this.btnMinimizar.TabIndex = 3;
            this.btnMinimizar.Text = "−";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.btnMinimizar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(222, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "RELATÓRIO DE VENDAS";
            // 
            // panelFiltro
            // 
            this.panelFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelFiltro.Controls.Add(this.btnAtualizar);
            this.panelFiltro.Controls.Add(this.txtBuscaEvento);
            this.panelFiltro.Controls.Add(this.cmbEventoResultados);
            this.panelFiltro.Controls.Add(this.lblEvento);
            this.panelFiltro.Controls.Add(this.lblResultados);
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltro.Location = new System.Drawing.Point(0, 50);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Padding = new System.Windows.Forms.Padding(15);
            this.panelFiltro.Size = new System.Drawing.Size(1000, 100);
            this.panelFiltro.TabIndex = 1;
            // 
            // btnAtualizar
            // 
            this.btnAtualizar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnAtualizar.FlatAppearance.BorderSize = 0;
            this.btnAtualizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnAtualizar.ForeColor = System.Drawing.Color.White;
            this.btnAtualizar.Location = new System.Drawing.Point(473, 30);
            this.btnAtualizar.Name = "btnAtualizar";
            this.btnAtualizar.Size = new System.Drawing.Size(120, 35);
            this.btnAtualizar.TabIndex = 2;
            this.btnAtualizar.Text = "Atualizar";
            this.btnAtualizar.UseVisualStyleBackColor = false;
            this.btnAtualizar.Click += new System.EventHandler(this.BtnAtualizar_Click);
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
            this.lblResultados.TabIndex = 0;
            this.lblResultados.Text = "Eventos:";
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.pnlCardTroco);
            this.panelCards.Controls.Add(this.pnlCardValor);
            this.panelCards.Controls.Add(this.pnlCardQtde);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 150);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.Size = new System.Drawing.Size(1000, 140);
            this.panelCards.TabIndex = 2;
            // 
            // panelGraficos
            // 
            this.panelGraficos.BackColor = System.Drawing.Color.White;
            this.panelGraficos.Controls.Add(this.panelGraficoPizza);
            this.panelGraficos.Controls.Add(this.panelGraficoBarras);
            this.panelGraficos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraficos.Location = new System.Drawing.Point(0, 290);
            this.panelGraficos.Name = "panelGraficos";
            this.panelGraficos.Padding = new System.Windows.Forms.Padding(15);
            this.panelGraficos.Size = new System.Drawing.Size(1000, 410);
            this.panelGraficos.TabIndex = 3;
            // 
            // panelGraficoPizza
            // 
            this.panelGraficoPizza.Controls.Add(this.lblGraficoPizza);
            this.panelGraficoPizza.Controls.Add(this.chartPizza);
            this.panelGraficoPizza.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelGraficoPizza.Location = new System.Drawing.Point(510, 15);
            this.panelGraficoPizza.Name = "panelGraficoPizza";
            this.panelGraficoPizza.Size = new System.Drawing.Size(475, 380);
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
            this.chartPizza.Size = new System.Drawing.Size(475, 380);
            this.chartPizza.TabIndex = 1;
            this.chartPizza.Text = "pieChart1";
            // 
            // panelGraficoBarras
            // 
            this.panelGraficoBarras.Controls.Add(this.lblGraficoBarras);
            this.panelGraficoBarras.Controls.Add(this.chartBarras);
            this.panelGraficoBarras.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelGraficoBarras.Location = new System.Drawing.Point(15, 15);
            this.panelGraficoBarras.Name = "panelGraficoBarras";
            this.panelGraficoBarras.Size = new System.Drawing.Size(490, 380);
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
            this.lblGraficoBarras.Size = new System.Drawing.Size(214, 20);
            this.lblGraficoBarras.TabIndex = 0;
            this.lblGraficoBarras.Text = "Valor Total Vendido por Caixa";
            // 
            // chartBarras
            // 
            this.chartBarras.BackColor = System.Drawing.Color.White;
            this.chartBarras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBarras.Location = new System.Drawing.Point(0, 0);
            this.chartBarras.Name = "chartBarras";
            this.chartBarras.Size = new System.Drawing.Size(490, 380);
            this.chartBarras.TabIndex = 1;
            this.chartBarras.Text = "cartesianChart1";
            // 
            // pnlCardTroco
            // 
            this.pnlCardTroco.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(240)))), ((int)(((byte)(230)))));
            this.pnlCardTroco.BorderRadius = 12;
            this.pnlCardTroco.Controls.Add(this.lblTrocoValor);
            this.pnlCardTroco.Controls.Add(this.lblTrocoLabel);
            this.pnlCardTroco.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTroco.Location = new System.Drawing.Point(690, 20);
            this.pnlCardTroco.Name = "pnlCardTroco";
            this.pnlCardTroco.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTroco.ShadowSize = 4;
            this.pnlCardTroco.Size = new System.Drawing.Size(290, 100);
            this.pnlCardTroco.TabIndex = 0;
            // 
            // lblTrocoValor
            // 
            this.lblTrocoValor.AutoSize = true;
            this.lblTrocoValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTrocoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
            this.lblTrocoValor.Location = new System.Drawing.Point(10, 45);
            this.lblTrocoValor.Name = "lblTrocoValor";
            this.lblTrocoValor.Size = new System.Drawing.Size(69, 37);
            this.lblTrocoValor.TabIndex = 1;
            this.lblTrocoValor.Text = "R$ -";
            // 
            // lblTrocoLabel
            // 
            this.lblTrocoLabel.AutoSize = true;
            this.lblTrocoLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTrocoLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTrocoLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTrocoLabel.Name = "lblTrocoLabel";
            this.lblTrocoLabel.Size = new System.Drawing.Size(110, 19);
            this.lblTrocoLabel.TabIndex = 0;
            this.lblTrocoLabel.Text = "Valor Total Troco";
            // 
            // pnlCardValor
            // 
            this.pnlCardValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.pnlCardValor.BorderRadius = 12;
            this.pnlCardValor.Controls.Add(this.lblValorVendidoValor);
            this.pnlCardValor.Controls.Add(this.lblValorVendidoLabel);
            this.pnlCardValor.ForeColor = System.Drawing.Color.Black;
            this.pnlCardValor.Location = new System.Drawing.Point(355, 20);
            this.pnlCardValor.Name = "pnlCardValor";
            this.pnlCardValor.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardValor.ShadowSize = 4;
            this.pnlCardValor.Size = new System.Drawing.Size(290, 100);
            this.pnlCardValor.TabIndex = 0;
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
            // pnlCardQtde
            // 
            this.pnlCardQtde.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlCardQtde.BorderRadius = 12;
            this.pnlCardQtde.Controls.Add(this.lblQtdeValor);
            this.pnlCardQtde.Controls.Add(this.lblQtdeLabel);
            this.pnlCardQtde.ForeColor = System.Drawing.Color.Black;
            this.pnlCardQtde.Location = new System.Drawing.Point(20, 20);
            this.pnlCardQtde.Name = "pnlCardQtde";
            this.pnlCardQtde.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardQtde.ShadowSize = 4;
            this.pnlCardQtde.Size = new System.Drawing.Size(290, 100);
            this.pnlCardQtde.TabIndex = 0;
            // 
            // lblQtdeValor
            // 
            this.lblQtdeValor.AutoSize = true;
            this.lblQtdeValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblQtdeValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblQtdeValor.Location = new System.Drawing.Point(10, 40);
            this.lblQtdeValor.Name = "lblQtdeValor";
            this.lblQtdeValor.Size = new System.Drawing.Size(37, 51);
            this.lblQtdeValor.TabIndex = 1;
            this.lblQtdeValor.Text = "-";
            // 
            // lblQtdeLabel
            // 
            this.lblQtdeLabel.AutoSize = true;
            this.lblQtdeLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQtdeLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblQtdeLabel.Location = new System.Drawing.Point(10, 10);
            this.lblQtdeLabel.Name = "lblQtdeLabel";
            this.lblQtdeLabel.Size = new System.Drawing.Size(181, 19);
            this.lblQtdeLabel.TabIndex = 0;
            this.lblQtdeLabel.Text = "Quantidade Total de Vendas";
            // 
            // FormRelatorioVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelGraficos);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.panelFiltro);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatorioVenda";
            this.Text = "Relatório de Vendas";
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.panelCards.ResumeLayout(false);
            this.panelGraficos.ResumeLayout(false);
            this.panelGraficoPizza.ResumeLayout(false);
            this.panelGraficoPizza.PerformLayout();
            this.panelGraficoBarras.ResumeLayout(false);
            this.panelGraficoBarras.PerformLayout();
            this.pnlCardTroco.ResumeLayout(false);
            this.pnlCardTroco.PerformLayout();
            this.pnlCardValor.ResumeLayout(false);
            this.pnlCardValor.PerformLayout();
            this.pnlCardQtde.ResumeLayout(false);
            this.pnlCardQtde.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFiltro;
        private System.Windows.Forms.TextBox txtBuscaEvento;
        private System.Windows.Forms.ComboBox cmbEventoResultados;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Button btnAtualizar;
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
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblResultados;
    }
}
