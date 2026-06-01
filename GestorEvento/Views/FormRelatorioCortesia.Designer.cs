using GestorEvento.Components;

namespace GestorEvento.Views
{
    partial class FormRelatorioCortesia
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
            this.pnlCardTicketCortesia = new GestorEvento.Components.ModernCard();
            this.lblTicketCortesiaValor = new System.Windows.Forms.Label();
            this.lblTicketCortesiaLabel = new System.Windows.Forms.Label();
            this.pnlCardValorCortesia = new GestorEvento.Components.ModernCard();
            this.lblValorCortesiaValor = new System.Windows.Forms.Label();
            this.lblValorCortesiaLabel = new System.Windows.Forms.Label();
            this.pnlCardQtdCortesia = new GestorEvento.Components.ModernCard();
            this.lblQtdCortesiaValor = new System.Windows.Forms.Label();
            this.lblQtdCortesiaLabel = new System.Windows.Forms.Label();
            this.panelGraficos = new System.Windows.Forms.Panel();
            this.panelGraficoBarras = new System.Windows.Forms.Panel();
            this.chartBarras = new LiveCharts.WinForms.CartesianChart();
            this.lblGraficoBarras = new System.Windows.Forms.Label();
            this.panelProdutos = new System.Windows.Forms.Panel();
            this.dgvProdutosCortesia = new System.Windows.Forms.DataGridView();
            this.lblProdutos = new System.Windows.Forms.Label();
            this.panelTitulo.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.pnlCardTicketCortesia.SuspendLayout();
            this.pnlCardValorCortesia.SuspendLayout();
            this.pnlCardQtdCortesia.SuspendLayout();
            this.panelGraficos.SuspendLayout();
            this.panelGraficoBarras.SuspendLayout();
            this.panelProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosCortesia)).BeginInit();
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
            this.panelTitulo.Size = new System.Drawing.Size(983, 50);
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
            this.btnFechar.Location = new System.Drawing.Point(893, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(45, 50);
            this.btnFechar.TabIndex = 1;
            this.btnFechar.Text = "✕";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(938, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 50);
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.Text = "−";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.BtnMinimizar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(246, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "RELATÓRIO DE CORTESIAS";
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
            this.panelFiltro.Size = new System.Drawing.Size(983, 100);
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
            this.btnAtualizar.TabIndex = 3;
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
            this.lblResultados.TabIndex = 4;
            this.lblResultados.Text = "Eventos:";
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.pnlCardTicketCortesia);
            this.panelCards.Controls.Add(this.pnlCardValorCortesia);
            this.panelCards.Controls.Add(this.pnlCardQtdCortesia);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 150);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.Size = new System.Drawing.Size(983, 140);
            this.panelCards.TabIndex = 2;
            // 
            // pnlCardTicketCortesia
            // 
            this.pnlCardTicketCortesia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(250)))), ((int)(((byte)(235)))));
            this.pnlCardTicketCortesia.BorderRadius = 12;
            this.pnlCardTicketCortesia.Controls.Add(this.lblTicketCortesiaValor);
            this.pnlCardTicketCortesia.Controls.Add(this.lblTicketCortesiaLabel);
            this.pnlCardTicketCortesia.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTicketCortesia.Location = new System.Drawing.Point(670, 20);
            this.pnlCardTicketCortesia.Name = "pnlCardTicketCortesia";
            this.pnlCardTicketCortesia.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTicketCortesia.ShadowSize = 4;
            this.pnlCardTicketCortesia.Size = new System.Drawing.Size(300, 100);
            this.pnlCardTicketCortesia.TabIndex = 2;
            // 
            // lblTicketCortesiaValor
            // 
            this.lblTicketCortesiaValor.AutoSize = true;
            this.lblTicketCortesiaValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTicketCortesiaValor.ForeColor = System.Drawing.Color.Green;
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
            this.lblTicketCortesiaLabel.Size = new System.Drawing.Size(141, 19);
            this.lblTicketCortesiaLabel.TabIndex = 0;
            this.lblTicketCortesiaLabel.Text = "Ticket Médio Cortesia";
            // 
            // pnlCardValorCortesia
            // 
            this.pnlCardValorCortesia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.pnlCardValorCortesia.BorderRadius = 12;
            this.pnlCardValorCortesia.Controls.Add(this.lblValorCortesiaValor);
            this.pnlCardValorCortesia.Controls.Add(this.lblValorCortesiaLabel);
            this.pnlCardValorCortesia.ForeColor = System.Drawing.Color.Black;
            this.pnlCardValorCortesia.Location = new System.Drawing.Point(345, 20);
            this.pnlCardValorCortesia.Name = "pnlCardValorCortesia";
            this.pnlCardValorCortesia.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardValorCortesia.ShadowSize = 4;
            this.pnlCardValorCortesia.Size = new System.Drawing.Size(300, 100);
            this.pnlCardValorCortesia.TabIndex = 1;
            // 
            // lblValorCortesiaValor
            // 
            this.lblValorCortesiaValor.AutoSize = true;
            this.lblValorCortesiaValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblValorCortesiaValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(140)))), ((int)(((byte)(0)))));
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
            this.lblValorCortesiaLabel.Size = new System.Drawing.Size(133, 19);
            this.lblValorCortesiaLabel.TabIndex = 0;
            this.lblValorCortesiaLabel.Text = "Valor Total Cortesias";
            // 
            // pnlCardQtdCortesia
            // 
            this.pnlCardQtdCortesia.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlCardQtdCortesia.BorderRadius = 12;
            this.pnlCardQtdCortesia.Controls.Add(this.lblQtdCortesiaValor);
            this.pnlCardQtdCortesia.Controls.Add(this.lblQtdCortesiaLabel);
            this.pnlCardQtdCortesia.ForeColor = System.Drawing.Color.Black;
            this.pnlCardQtdCortesia.Location = new System.Drawing.Point(20, 20);
            this.pnlCardQtdCortesia.Name = "pnlCardQtdCortesia";
            this.pnlCardQtdCortesia.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardQtdCortesia.ShadowSize = 4;
            this.pnlCardQtdCortesia.Size = new System.Drawing.Size(300, 100);
            this.pnlCardQtdCortesia.TabIndex = 0;
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
            this.lblQtdCortesiaLabel.Size = new System.Drawing.Size(141, 19);
            this.lblQtdCortesiaLabel.TabIndex = 0;
            this.lblQtdCortesiaLabel.Text = "Quantidade Cortesias";
            // 
            // panelGraficos
            // 
            this.panelGraficos.BackColor = System.Drawing.Color.White;
            this.panelGraficos.Controls.Add(this.panelGraficoBarras);
            this.panelGraficos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelGraficos.Location = new System.Drawing.Point(0, 290);
            this.panelGraficos.Name = "panelGraficos";
            this.panelGraficos.Padding = new System.Windows.Forms.Padding(15);
            this.panelGraficos.Size = new System.Drawing.Size(983, 320);
            this.panelGraficos.TabIndex = 3;
            // 
            // panelGraficoBarras
            // 
            this.panelGraficoBarras.Controls.Add(this.chartBarras);
            this.panelGraficoBarras.Controls.Add(this.lblGraficoBarras);
            this.panelGraficoBarras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGraficoBarras.Location = new System.Drawing.Point(15, 15);
            this.panelGraficoBarras.Name = "panelGraficoBarras";
            this.panelGraficoBarras.Size = new System.Drawing.Size(953, 290);
            this.panelGraficoBarras.TabIndex = 0;
            // 
            // chartBarras
            // 
            this.chartBarras.BackColor = System.Drawing.Color.White;
            this.chartBarras.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chartBarras.Location = new System.Drawing.Point(0, 20);
            this.chartBarras.Name = "chartBarras";
            this.chartBarras.Size = new System.Drawing.Size(953, 270);
            this.chartBarras.TabIndex = 1;
            this.chartBarras.Text = "cartesianChart1";
            // 
            // lblGraficoBarras
            // 
            this.lblGraficoBarras.AutoSize = true;
            this.lblGraficoBarras.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGraficoBarras.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblGraficoBarras.ForeColor = System.Drawing.Color.Black;
            this.lblGraficoBarras.Location = new System.Drawing.Point(0, 0);
            this.lblGraficoBarras.Name = "lblGraficoBarras";
            this.lblGraficoBarras.Size = new System.Drawing.Size(196, 20);
            this.lblGraficoBarras.TabIndex = 0;
            this.lblGraficoBarras.Text = "Valor de Cortesia por Caixa";
            // 
            // panelProdutos
            // 
            this.panelProdutos.BackColor = System.Drawing.Color.White;
            this.panelProdutos.Controls.Add(this.dgvProdutosCortesia);
            this.panelProdutos.Controls.Add(this.lblProdutos);
            this.panelProdutos.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelProdutos.Location = new System.Drawing.Point(0, 610);
            this.panelProdutos.Name = "panelProdutos";
            this.panelProdutos.Padding = new System.Windows.Forms.Padding(15);
            this.panelProdutos.Size = new System.Drawing.Size(983, 260);
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
            this.dgvProdutosCortesia.Size = new System.Drawing.Size(953, 210);
            this.dgvProdutosCortesia.TabIndex = 1;
            // 
            // lblProdutos
            // 
            this.lblProdutos.AutoSize = true;
            this.lblProdutos.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProdutos.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblProdutos.ForeColor = System.Drawing.Color.Black;
            this.lblProdutos.Location = new System.Drawing.Point(15, 15);
            this.lblProdutos.Name = "lblProdutos";
            this.lblProdutos.Size = new System.Drawing.Size(234, 20);
            this.lblProdutos.TabIndex = 0;
            this.lblProdutos.Text = "Produtos em Cortesia no Evento";
            // 
            // FormRelatorioCortesia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.panelProdutos);
            this.Controls.Add(this.panelGraficos);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.panelFiltro);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatorioCortesia";
            this.Text = "Relatório de Cortesia";
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.panelCards.ResumeLayout(false);
            this.pnlCardTicketCortesia.ResumeLayout(false);
            this.pnlCardTicketCortesia.PerformLayout();
            this.pnlCardValorCortesia.ResumeLayout(false);
            this.pnlCardValorCortesia.PerformLayout();
            this.pnlCardQtdCortesia.ResumeLayout(false);
            this.pnlCardQtdCortesia.PerformLayout();
            this.panelGraficos.ResumeLayout(false);
            this.panelGraficoBarras.ResumeLayout(false);
            this.panelGraficoBarras.PerformLayout();
            this.panelProdutos.ResumeLayout(false);
            this.panelProdutos.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosCortesia)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFiltro;
        private System.Windows.Forms.Button btnAtualizar;
        private System.Windows.Forms.TextBox txtBuscaEvento;
        private System.Windows.Forms.ComboBox cmbEventoResultados;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblResultados;
        private System.Windows.Forms.Panel panelCards;
        private ModernCard pnlCardTicketCortesia;
        private System.Windows.Forms.Label lblTicketCortesiaValor;
        private System.Windows.Forms.Label lblTicketCortesiaLabel;
        private ModernCard pnlCardValorCortesia;
        private System.Windows.Forms.Label lblValorCortesiaValor;
        private System.Windows.Forms.Label lblValorCortesiaLabel;
        private ModernCard pnlCardQtdCortesia;
        private System.Windows.Forms.Label lblQtdCortesiaValor;
        private System.Windows.Forms.Label lblQtdCortesiaLabel;
        private System.Windows.Forms.Panel panelGraficos;
        private System.Windows.Forms.Panel panelGraficoBarras;
        private System.Windows.Forms.Label lblGraficoBarras;
        private LiveCharts.WinForms.CartesianChart chartBarras;
        private System.Windows.Forms.Panel panelProdutos;
        private System.Windows.Forms.DataGridView dgvProdutosCortesia;
        private System.Windows.Forms.Label lblProdutos;
    }
}
