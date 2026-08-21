namespace GestorEvento.Views
{
    partial class FormConsultaVenda
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
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.lblIdVenda = new System.Windows.Forms.Label();
            this.txtIdVenda = new System.Windows.Forms.TextBox();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.gbDadosVenda = new System.Windows.Forms.GroupBox();
            this.lblTrocoValor = new System.Windows.Forms.Label();
            this.lblTrocoCap = new System.Windows.Forms.Label();
            this.lblValorTotalValor = new System.Windows.Forms.Label();
            this.lblValorTotalCap = new System.Windows.Forms.Label();
            this.lblTipoValor = new System.Windows.Forms.Label();
            this.lblTipoCap = new System.Windows.Forms.Label();
            this.lblStatusValor = new System.Windows.Forms.Label();
            this.lblStatusCap = new System.Windows.Forms.Label();
            this.lblDataValor = new System.Windows.Forms.Label();
            this.lblDataCap = new System.Windows.Forms.Label();
            this.lblCaixaValor = new System.Windows.Forms.Label();
            this.lblCaixaCap = new System.Windows.Forms.Label();
            this.lblIdVendaValor = new System.Windows.Forms.Label();
            this.lblIdVendaCap = new System.Windows.Forms.Label();
            this.gbProdutos = new System.Windows.Forms.GroupBox();
            this.dgvProdutos = new System.Windows.Forms.DataGridView();
            this.gbPagamentos = new System.Windows.Forms.GroupBox();
            this.dgvPagamentos = new System.Windows.Forms.DataGridView();
            this.gbDoacoes = new System.Windows.Forms.GroupBox();
            this.dgvDoacoes = new System.Windows.Forms.DataGridView();
            this.panelTitulo.SuspendLayout();
            this.gbDadosVenda.SuspendLayout();
            this.gbProdutos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).BeginInit();
            this.gbPagamentos.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagamentos)).BeginInit();
            this.gbDoacoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoacoes)).BeginInit();
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
            this.panelTitulo.Size = new System.Drawing.Size(950, 40);
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
            this.btnMinimizar.Location = new System.Drawing.Point(860, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 40);
            this.btnMinimizar.TabIndex = 1;
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
            this.btnFechar.Location = new System.Drawing.Point(905, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(45, 40);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "✕";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(10, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(152, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Consulta de Venda";
            // 
            // lblIdVenda
            // 
            this.lblIdVenda.AutoSize = true;
            this.lblIdVenda.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblIdVenda.Location = new System.Drawing.Point(20, 58);
            this.lblIdVenda.Name = "lblIdVenda";
            this.lblIdVenda.Size = new System.Drawing.Size(93, 20);
            this.lblIdVenda.TabIndex = 1;
            this.lblIdVenda.Text = "ID da Venda:";
            // 
            // txtIdVenda
            // 
            this.txtIdVenda.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtIdVenda.Location = new System.Drawing.Point(130, 54);
            this.txtIdVenda.Name = "txtIdVenda";
            this.txtIdVenda.Size = new System.Drawing.Size(150, 29);
            this.txtIdVenda.TabIndex = 2;
            this.txtIdVenda.TextChanged += new System.EventHandler(this.TxtIdVenda_TextChanged);
            this.txtIdVenda.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtIdVenda_KeyDown);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnPesquisar.FlatAppearance.BorderSize = 0;
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.Location = new System.Drawing.Point(300, 52);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(160, 34);
            this.btnPesquisar.TabIndex = 3;
            this.btnPesquisar.Text = "🔎 PESQUISAR";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.BtnPesquisar_Click);
            // 
            // gbDadosVenda
            // 
            this.gbDadosVenda.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDadosVenda.Controls.Add(this.lblTrocoValor);
            this.gbDadosVenda.Controls.Add(this.lblTrocoCap);
            this.gbDadosVenda.Controls.Add(this.lblValorTotalValor);
            this.gbDadosVenda.Controls.Add(this.lblValorTotalCap);
            this.gbDadosVenda.Controls.Add(this.lblTipoValor);
            this.gbDadosVenda.Controls.Add(this.lblTipoCap);
            this.gbDadosVenda.Controls.Add(this.lblStatusValor);
            this.gbDadosVenda.Controls.Add(this.lblStatusCap);
            this.gbDadosVenda.Controls.Add(this.lblDataValor);
            this.gbDadosVenda.Controls.Add(this.lblDataCap);
            this.gbDadosVenda.Controls.Add(this.lblCaixaValor);
            this.gbDadosVenda.Controls.Add(this.lblCaixaCap);
            this.gbDadosVenda.Controls.Add(this.lblIdVendaValor);
            this.gbDadosVenda.Controls.Add(this.lblIdVendaCap);
            this.gbDadosVenda.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gbDadosVenda.Location = new System.Drawing.Point(20, 100);
            this.gbDadosVenda.Name = "gbDadosVenda";
            this.gbDadosVenda.Size = new System.Drawing.Size(910, 150);
            this.gbDadosVenda.TabIndex = 4;
            this.gbDadosVenda.TabStop = false;
            this.gbDadosVenda.Text = "DADOS DA VENDA";
            // 
            // lblTrocoValor
            // 
            this.lblTrocoValor.AutoSize = true;
            this.lblTrocoValor.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTrocoValor.Location = new System.Drawing.Point(476, 100);
            this.lblTrocoValor.Name = "lblTrocoValor";
            this.lblTrocoValor.Size = new System.Drawing.Size(16, 17);
            this.lblTrocoValor.TabIndex = 13;
            this.lblTrocoValor.Text = "-";
            // 
            // lblTrocoCap
            // 
            this.lblTrocoCap.AutoSize = true;
            this.lblTrocoCap.Location = new System.Drawing.Point(390, 98);
            this.lblTrocoCap.Name = "lblTrocoCap";
            this.lblTrocoCap.Size = new System.Drawing.Size(80, 19);
            this.lblTrocoCap.TabIndex = 12;
            this.lblTrocoCap.Text = "Valor Troco:";
            // 
            // lblValorTotalValor
            // 
            this.lblValorTotalValor.AutoSize = true;
            this.lblValorTotalValor.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold);
            this.lblValorTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblValorTotalValor.Location = new System.Drawing.Point(475, 71);
            this.lblValorTotalValor.Name = "lblValorTotalValor";
            this.lblValorTotalValor.Size = new System.Drawing.Size(18, 19);
            this.lblValorTotalValor.TabIndex = 11;
            this.lblValorTotalValor.Text = "-";
            // 
            // lblValorTotalCap
            // 
            this.lblValorTotalCap.AutoSize = true;
            this.lblValorTotalCap.Location = new System.Drawing.Point(390, 71);
            this.lblValorTotalCap.Name = "lblValorTotalCap";
            this.lblValorTotalCap.Size = new System.Drawing.Size(76, 19);
            this.lblValorTotalCap.TabIndex = 10;
            this.lblValorTotalCap.Text = "Valor Total:";
            // 
            // lblTipoValor
            // 
            this.lblTipoValor.AutoSize = true;
            this.lblTipoValor.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTipoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblTipoValor.Location = new System.Drawing.Point(101, 96);
            this.lblTipoValor.Name = "lblTipoValor";
            this.lblTipoValor.Size = new System.Drawing.Size(15, 19);
            this.lblTipoValor.TabIndex = 9;
            this.lblTipoValor.Text = "-";
            // 
            // lblTipoCap
            // 
            this.lblTipoCap.AutoSize = true;
            this.lblTipoCap.Location = new System.Drawing.Point(6, 96);
            this.lblTipoCap.Name = "lblTipoCap";
            this.lblTipoCap.Size = new System.Drawing.Size(38, 19);
            this.lblTipoCap.TabIndex = 8;
            this.lblTipoCap.Text = "Tipo:";
            // 
            // lblStatusValor
            // 
            this.lblStatusValor.AutoSize = true;
            this.lblStatusValor.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblStatusValor.Location = new System.Drawing.Point(476, 46);
            this.lblStatusValor.Name = "lblStatusValor";
            this.lblStatusValor.Size = new System.Drawing.Size(16, 17);
            this.lblStatusValor.TabIndex = 7;
            this.lblStatusValor.Text = "-";
            // 
            // lblStatusCap
            // 
            this.lblStatusCap.AutoSize = true;
            this.lblStatusCap.Location = new System.Drawing.Point(390, 46);
            this.lblStatusCap.Name = "lblStatusCap";
            this.lblStatusCap.Size = new System.Drawing.Size(50, 19);
            this.lblStatusCap.TabIndex = 6;
            this.lblStatusCap.Text = "Status:";
            // 
            // lblDataValor
            // 
            this.lblDataValor.AutoSize = true;
            this.lblDataValor.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblDataValor.Location = new System.Drawing.Point(101, 71);
            this.lblDataValor.Name = "lblDataValor";
            this.lblDataValor.Size = new System.Drawing.Size(16, 17);
            this.lblDataValor.TabIndex = 5;
            this.lblDataValor.Text = "-";
            // 
            // lblDataCap
            // 
            this.lblDataCap.AutoSize = true;
            this.lblDataCap.Location = new System.Drawing.Point(6, 71);
            this.lblDataCap.Name = "lblDataCap";
            this.lblDataCap.Size = new System.Drawing.Size(76, 19);
            this.lblDataCap.TabIndex = 4;
            this.lblDataCap.Text = "Data/Hora:";
            // 
            // lblCaixaValor
            // 
            this.lblCaixaValor.AutoSize = true;
            this.lblCaixaValor.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaixaValor.Location = new System.Drawing.Point(100, 23);
            this.lblCaixaValor.Name = "lblCaixaValor";
            this.lblCaixaValor.Size = new System.Drawing.Size(16, 17);
            this.lblCaixaValor.TabIndex = 3;
            this.lblCaixaValor.Text = "-";
            // 
            // lblCaixaCap
            // 
            this.lblCaixaCap.AutoSize = true;
            this.lblCaixaCap.Location = new System.Drawing.Point(6, 21);
            this.lblCaixaCap.Name = "lblCaixaCap";
            this.lblCaixaCap.Size = new System.Drawing.Size(44, 19);
            this.lblCaixaCap.TabIndex = 2;
            this.lblCaixaCap.Text = "Caixa:";
            // 
            // lblIdVendaValor
            // 
            this.lblIdVendaValor.AutoSize = true;
            this.lblIdVendaValor.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblIdVendaValor.Location = new System.Drawing.Point(101, 46);
            this.lblIdVendaValor.Name = "lblIdVendaValor";
            this.lblIdVendaValor.Size = new System.Drawing.Size(16, 17);
            this.lblIdVendaValor.TabIndex = 1;
            this.lblIdVendaValor.Text = "-";
            // 
            // lblIdVendaCap
            // 
            this.lblIdVendaCap.AutoSize = true;
            this.lblIdVendaCap.Location = new System.Drawing.Point(6, 46);
            this.lblIdVendaCap.Name = "lblIdVendaCap";
            this.lblIdVendaCap.Size = new System.Drawing.Size(68, 19);
            this.lblIdVendaCap.TabIndex = 0;
            this.lblIdVendaCap.Text = "ID Venda:";
            // 
            // gbProdutos
            // 
            this.gbProdutos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbProdutos.Controls.Add(this.dgvProdutos);
            this.gbProdutos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gbProdutos.Location = new System.Drawing.Point(20, 260);
            this.gbProdutos.Name = "gbProdutos";
            this.gbProdutos.Size = new System.Drawing.Size(910, 230);
            this.gbProdutos.TabIndex = 5;
            this.gbProdutos.TabStop = false;
            this.gbProdutos.Text = "PRODUTOS DA VENDA";
            // 
            // dgvProdutos
            // 
            this.dgvProdutos.AllowUserToAddRows = false;
            this.dgvProdutos.AllowUserToDeleteRows = false;
            this.dgvProdutos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProdutos.Location = new System.Drawing.Point(3, 21);
            this.dgvProdutos.Name = "dgvProdutos";
            this.dgvProdutos.ReadOnly = true;
            this.dgvProdutos.RowHeadersVisible = false;
            this.dgvProdutos.Size = new System.Drawing.Size(904, 206);
            this.dgvProdutos.TabIndex = 0;
            // 
            // gbPagamentos
            // 
            this.gbPagamentos.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbPagamentos.Controls.Add(this.dgvPagamentos);
            this.gbPagamentos.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gbPagamentos.Location = new System.Drawing.Point(20, 500);
            this.gbPagamentos.Name = "gbPagamentos";
            this.gbPagamentos.Size = new System.Drawing.Size(445, 160);
            this.gbPagamentos.TabIndex = 6;
            this.gbPagamentos.TabStop = false;
            this.gbPagamentos.Text = "FORMAS DE RECEBIMENTO";
            // 
            // dgvPagamentos
            // 
            this.dgvPagamentos.AllowUserToAddRows = false;
            this.dgvPagamentos.AllowUserToDeleteRows = false;
            this.dgvPagamentos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvPagamentos.Location = new System.Drawing.Point(3, 21);
            this.dgvPagamentos.Name = "dgvPagamentos";
            this.dgvPagamentos.ReadOnly = true;
            this.dgvPagamentos.RowHeadersVisible = false;
            this.dgvPagamentos.Size = new System.Drawing.Size(439, 136);
            this.dgvPagamentos.TabIndex = 0;
            // 
            // gbDoacoes
            // 
            this.gbDoacoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbDoacoes.Controls.Add(this.dgvDoacoes);
            this.gbDoacoes.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gbDoacoes.Location = new System.Drawing.Point(485, 500);
            this.gbDoacoes.Name = "gbDoacoes";
            this.gbDoacoes.Size = new System.Drawing.Size(445, 160);
            this.gbDoacoes.TabIndex = 7;
            this.gbDoacoes.TabStop = false;
            this.gbDoacoes.Text = "DOAÇÕES";
            // 
            // dgvDoacoes
            // 
            this.dgvDoacoes.AllowUserToAddRows = false;
            this.dgvDoacoes.AllowUserToDeleteRows = false;
            this.dgvDoacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoacoes.Location = new System.Drawing.Point(3, 21);
            this.dgvDoacoes.Name = "dgvDoacoes";
            this.dgvDoacoes.ReadOnly = true;
            this.dgvDoacoes.RowHeadersVisible = false;
            this.dgvDoacoes.Size = new System.Drawing.Size(439, 136);
            this.dgvDoacoes.TabIndex = 0;
            // 
            // FormConsultaVenda
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(950, 690);
            this.Controls.Add(this.gbDoacoes);
            this.Controls.Add(this.gbPagamentos);
            this.Controls.Add(this.gbProdutos);
            this.Controls.Add(this.gbDadosVenda);
            this.Controls.Add(this.btnPesquisar);
            this.Controls.Add(this.txtIdVenda);
            this.Controls.Add(this.lblIdVenda);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormConsultaVenda";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Consulta de Venda";
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.gbDadosVenda.ResumeLayout(false);
            this.gbDadosVenda.PerformLayout();
            this.gbProdutos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutos)).EndInit();
            this.gbPagamentos.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPagamentos)).EndInit();
            this.gbDoacoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoacoes)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Label lblIdVenda;
        private System.Windows.Forms.TextBox txtIdVenda;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.GroupBox gbDadosVenda;
        private System.Windows.Forms.Label lblIdVendaCap;
        private System.Windows.Forms.Label lblIdVendaValor;
        private System.Windows.Forms.Label lblCaixaCap;
        private System.Windows.Forms.Label lblCaixaValor;
        private System.Windows.Forms.Label lblDataCap;
        private System.Windows.Forms.Label lblDataValor;
        private System.Windows.Forms.Label lblStatusCap;
        private System.Windows.Forms.Label lblStatusValor;
        private System.Windows.Forms.Label lblTipoCap;
        private System.Windows.Forms.Label lblTipoValor;
        private System.Windows.Forms.Label lblValorTotalCap;
        private System.Windows.Forms.Label lblValorTotalValor;
        private System.Windows.Forms.Label lblTrocoCap;
        private System.Windows.Forms.Label lblTrocoValor;
        private System.Windows.Forms.GroupBox gbProdutos;
        private System.Windows.Forms.DataGridView dgvProdutos;
        private System.Windows.Forms.GroupBox gbPagamentos;
        private System.Windows.Forms.DataGridView dgvPagamentos;
        private System.Windows.Forms.GroupBox gbDoacoes;
        private System.Windows.Forms.DataGridView dgvDoacoes;
    }
}
