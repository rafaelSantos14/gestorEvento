namespace GestorEvento.Views
{
    partial class FormRelatorioReimpressao
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
            this.cmbStatusFiltro = new System.Windows.Forms.ComboBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.cmbEventoResultados = new System.Windows.Forms.ComboBox();
            this.lblEvento = new System.Windows.Forms.Label();
            this.txtBuscaEvento = new System.Windows.Forms.TextBox();
            this.panelCards = new System.Windows.Forms.Panel();
            this.chartMotivos = new LiveCharts.WinForms.CartesianChart();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabItens = new System.Windows.Forms.TabPage();
            this.dgvItens = new System.Windows.Forms.DataGridView();
            this.tabPorProduto = new System.Windows.Forms.TabPage();
            this.dgvPorProduto = new System.Windows.Forms.DataGridView();
            this.cardTotal = new GestorEvento.Components.ModernCard();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.lblTotalReimpressoes = new System.Windows.Forms.Label();
            this.cardValor = new GestorEvento.Components.ModernCard();
            this.lblValorLabel = new System.Windows.Forms.Label();
            this.lblValorTotal = new System.Windows.Forms.Label();
            this.panelTitulo.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            this.panelCards.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabItens.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).BeginInit();
            this.tabPorProduto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorProduto)).BeginInit();
            this.cardTotal.SuspendLayout();
            this.cardValor.SuspendLayout();
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
            this.panelTitulo.Size = new System.Drawing.Size(1000, 50);
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
            this.btnMinimizar.Location = new System.Drawing.Point(910, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 50);
            this.btnMinimizar.TabIndex = 2;
            this.btnMinimizar.Text = "−";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.BtnMinimizar_Click);
            // 
            // btnFechar
            // 
            this.btnFechar.BackColor = System.Drawing.Color.Transparent;
            this.btnFechar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(955, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(45, 50);
            this.btnFechar.TabIndex = 1;
            this.btnFechar.Text = "✕";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(10, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(114, 21);
            this.lblTitulo.TabIndex = 3;
            this.lblTitulo.Text = "Reimpressões";
            // 
            // panelFiltro
            // 
            this.panelFiltro.BackColor = System.Drawing.Color.WhiteSmoke;
            this.panelFiltro.Controls.Add(this.cmbStatusFiltro);
            this.panelFiltro.Controls.Add(this.lblStatus);
            this.panelFiltro.Controls.Add(this.cmbEventoResultados);
            this.panelFiltro.Controls.Add(this.lblEvento);
            this.panelFiltro.Controls.Add(this.txtBuscaEvento);
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltro.Location = new System.Drawing.Point(0, 50);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Padding = new System.Windows.Forms.Padding(10);
            this.panelFiltro.Size = new System.Drawing.Size(1000, 80);
            this.panelFiltro.TabIndex = 0;
            // 
            // cmbStatusFiltro
            // 
            this.cmbStatusFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatusFiltro.FormattingEnabled = true;
            this.cmbStatusFiltro.Location = new System.Drawing.Point(370, 23);
            this.cmbStatusFiltro.Name = "cmbStatusFiltro";
            this.cmbStatusFiltro.Size = new System.Drawing.Size(150, 25);
            this.cmbStatusFiltro.TabIndex = 4;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatus.Location = new System.Drawing.Point(370, 18);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(53, 19);
            this.lblStatus.TabIndex = 3;
            this.lblStatus.Text = "Status:";
            // 
            // cmbEventoResultados
            // 
            this.cmbEventoResultados.BackColor = System.Drawing.Color.White;
            this.cmbEventoResultados.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEventoResultados.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbEventoResultados.FormattingEnabled = true;
            this.cmbEventoResultados.Location = new System.Drawing.Point(10, 53);
            this.cmbEventoResultados.Name = "cmbEventoResultados";
            this.cmbEventoResultados.Size = new System.Drawing.Size(350, 25);
            this.cmbEventoResultados.TabIndex = 2;
            this.cmbEventoResultados.SelectedIndexChanged += new System.EventHandler(this.CmbEventoResultados_SelectedIndexChanged);
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblEvento.Location = new System.Drawing.Point(10, 3);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(58, 19);
            this.lblEvento.TabIndex = 0;
            this.lblEvento.Text = "Evento:";
            // 
            // txtBuscaEvento
            // 
            this.txtBuscaEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscaEvento.Location = new System.Drawing.Point(10, 23);
            this.txtBuscaEvento.Name = "txtBuscaEvento";
            this.txtBuscaEvento.Size = new System.Drawing.Size(350, 25);
            this.txtBuscaEvento.TabIndex = 1;
            this.txtBuscaEvento.TextChanged += new System.EventHandler(this.TxtBuscaEvento_TextChanged);
            // 
            // panelCards
            // 
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.cardTotal);
            this.panelCards.Controls.Add(this.cardValor);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 130);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(10);
            this.panelCards.Size = new System.Drawing.Size(1000, 110);
            this.panelCards.TabIndex = 2;
            // 
            // chartMotivos
            // 
            this.chartMotivos.BackColor = System.Drawing.Color.White;
            this.chartMotivos.Dock = System.Windows.Forms.DockStyle.Top;
            this.chartMotivos.Location = new System.Drawing.Point(0, 240);
            this.chartMotivos.Name = "chartMotivos";
            this.chartMotivos.Size = new System.Drawing.Size(1000, 200);
            this.chartMotivos.TabIndex = 3;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabItens);
            this.tabControl.Controls.Add(this.tabPorProduto);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 440);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1000, 260);
            this.tabControl.TabIndex = 4;
            // 
            // tabItens
            // 
            this.tabItens.BackColor = System.Drawing.Color.White;
            this.tabItens.Controls.Add(this.dgvItens);
            this.tabItens.Location = new System.Drawing.Point(4, 26);
            this.tabItens.Name = "tabItens";
            this.tabItens.Padding = new System.Windows.Forms.Padding(3);
            this.tabItens.Size = new System.Drawing.Size(992, 230);
            this.tabItens.TabIndex = 0;
            this.tabItens.Text = "Itens";
            // 
            // dgvItens
            // 
            this.dgvItens.BackgroundColor = System.Drawing.Color.White;
            this.dgvItens.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvItens.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvItens.Location = new System.Drawing.Point(3, 3);
            this.dgvItens.Name = "dgvItens";
            this.dgvItens.ReadOnly = true;
            this.dgvItens.RowHeadersVisible = false;
            this.dgvItens.Size = new System.Drawing.Size(986, 224);
            this.dgvItens.TabIndex = 0;
            // 
            // tabPorProduto
            // 
            this.tabPorProduto.BackColor = System.Drawing.Color.White;
            this.tabPorProduto.Controls.Add(this.dgvPorProduto);
            this.tabPorProduto.Location = new System.Drawing.Point(4, 26);
            this.tabPorProduto.Name = "tabPorProduto";
            this.tabPorProduto.Padding = new System.Windows.Forms.Padding(3);
            this.tabPorProduto.Size = new System.Drawing.Size(992, 230);
            this.tabPorProduto.TabIndex = 1;
            this.tabPorProduto.Text = "Por Produto";
            // 
            // dgvPorProduto
            // 
            this.dgvPorProduto.BackgroundColor = System.Drawing.Color.White;
            this.dgvPorProduto.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvPorProduto.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPorProduto.Location = new System.Drawing.Point(3, 3);
            this.dgvPorProduto.Name = "dgvPorProduto";
            this.dgvPorProduto.ReadOnly = true;
            this.dgvPorProduto.RowHeadersVisible = false;
            this.dgvPorProduto.Size = new System.Drawing.Size(986, 228);
            this.dgvPorProduto.TabIndex = 0;
            // 
            // cardTotal
            // 
            this.cardTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.cardTotal.BorderRadius = 10;
            this.cardTotal.Controls.Add(this.lblTotalLabel);
            this.cardTotal.Controls.Add(this.lblTotalReimpressoes);
            this.cardTotal.ForeColor = System.Drawing.Color.Black;
            this.cardTotal.Location = new System.Drawing.Point(15, 15);
            this.cardTotal.Name = "cardTotal";
            this.cardTotal.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cardTotal.ShadowSize = 3;
            this.cardTotal.Size = new System.Drawing.Size(172, 80);
            this.cardTotal.TabIndex = 0;
            // 
            // lblTotalLabel
            // 
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalLabel.Location = new System.Drawing.Point(15, 15);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(144, 19);
            this.lblTotalLabel.TabIndex = 0;
            this.lblTotalLabel.Text = "Total de Reimpressões";
            // 
            // lblTotalReimpressoes
            // 
            this.lblTotalReimpressoes.AutoSize = true;
            this.lblTotalReimpressoes.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTotalReimpressoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblTotalReimpressoes.Location = new System.Drawing.Point(15, 35);
            this.lblTotalReimpressoes.Name = "lblTotalReimpressoes";
            this.lblTotalReimpressoes.Size = new System.Drawing.Size(29, 32);
            this.lblTotalReimpressoes.TabIndex = 1;
            this.lblTotalReimpressoes.Text = "0";
            // 
            // cardValor
            // 
            this.cardValor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(255)))), ((int)(((byte)(240)))));
            this.cardValor.BorderRadius = 10;
            this.cardValor.Controls.Add(this.lblValorLabel);
            this.cardValor.Controls.Add(this.lblValorTotal);
            this.cardValor.ForeColor = System.Drawing.Color.Black;
            this.cardValor.Location = new System.Drawing.Point(203, 17);
            this.cardValor.Name = "cardValor";
            this.cardValor.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.cardValor.ShadowSize = 3;
            this.cardValor.Size = new System.Drawing.Size(243, 80);
            this.cardValor.TabIndex = 1;
            // 
            // lblValorLabel
            // 
            this.lblValorLabel.AutoSize = true;
            this.lblValorLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblValorLabel.Location = new System.Drawing.Point(15, 15);
            this.lblValorLabel.Name = "lblValorLabel";
            this.lblValorLabel.Size = new System.Drawing.Size(73, 19);
            this.lblValorLabel.TabIndex = 0;
            this.lblValorLabel.Text = "Valor Total";
            // 
            // lblValorTotal
            // 
            this.lblValorTotal.AutoSize = true;
            this.lblValorTotal.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblValorTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblValorTotal.Location = new System.Drawing.Point(15, 35);
            this.lblValorTotal.Name = "lblValorTotal";
            this.lblValorTotal.Size = new System.Drawing.Size(101, 32);
            this.lblValorTotal.TabIndex = 1;
            this.lblValorTotal.Text = "R$ 0,00";
            // 
            // FormRelatorioReimpressao
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(1000, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.chartMotivos);
            this.Controls.Add(this.panelCards);
            this.Controls.Add(this.panelFiltro);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatorioReimpressao";
            this.Text = "Relatório de Reimpressões";
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.panelCards.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.tabItens.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvItens)).EndInit();
            this.tabPorProduto.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPorProduto)).EndInit();
            this.cardTotal.ResumeLayout(false);
            this.cardTotal.PerformLayout();
            this.cardValor.ResumeLayout(false);
            this.cardValor.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelFiltro;
        private System.Windows.Forms.ComboBox cmbStatusFiltro;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.ComboBox cmbEventoResultados;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.TextBox txtBuscaEvento;
        private System.Windows.Forms.Panel panelCards;
        private GestorEvento.Components.ModernCard cardTotal;
        private GestorEvento.Components.ModernCard cardValor;
        private System.Windows.Forms.Label lblTotalReimpressoes;
        private System.Windows.Forms.Label lblTotalLabel;
        private System.Windows.Forms.Label lblValorTotal;
        private System.Windows.Forms.Label lblValorLabel;
        private LiveCharts.WinForms.CartesianChart chartMotivos;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabItens;
        private System.Windows.Forms.DataGridView dgvItens;
        private System.Windows.Forms.TabPage tabPorProduto;
        private System.Windows.Forms.DataGridView dgvPorProduto;
    }
}
