namespace GestorEvento.Views
{
    partial class FormPDV
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
            this.panelInfoCaixa = new System.Windows.Forms.Panel();
            this.lblInfoCaixa = new System.Windows.Forms.Label();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.panelTotalizacao = new System.Windows.Forms.Panel();
            this.lblTotal = new System.Windows.Forms.Label();
            this.lblTotalValor = new System.Windows.Forms.Label();
            this.groupBoxTipoOperacao = new System.Windows.Forms.GroupBox();
            this._rbReimprimir = new System.Windows.Forms.RadioButton();
            this._rbCortesia = new System.Windows.Forms.RadioButton();
            this._rbVenda = new System.Windows.Forms.RadioButton();
            this.lblTrocoValor = new System.Windows.Forms.Label();
            this.btnConfirmarVenda = new System.Windows.Forms.Button();
            this.lblTroco = new System.Windows.Forms.Label();
            this.panelPagamento = new System.Windows.Forms.Panel();
            this.panelProdutos = new System.Windows.Forms.Panel();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnAtualizarPDV = new System.Windows.Forms.Button();
            this.panelTitulo.SuspendLayout();
            this.panelInfoCaixa.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelTotalizacao.SuspendLayout();
            this.groupBoxTipoOperacao.SuspendLayout();
            this.panelBotoes.SuspendLayout();
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
            this.panelTitulo.Size = new System.Drawing.Size(1000, 40);
            this.panelTitulo.TabIndex = 0;
            this.panelTitulo.MouseDown += new System.Windows.Forms.MouseEventHandler(this.PanelTitulo_MouseDown);
            this.panelTitulo.MouseMove += new System.Windows.Forms.MouseEventHandler(this.PanelTitulo_MouseMove);
            this.panelTitulo.MouseUp += new System.Windows.Forms.MouseEventHandler(this.PanelTitulo_MouseUp);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(910, 0);
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
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(955, 0);
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
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(104, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "PDV Caixa";
            // 
            // panelInfoCaixa
            // 
            this.panelInfoCaixa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelInfoCaixa.Controls.Add(this.lblInfoCaixa);
            this.panelInfoCaixa.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelInfoCaixa.Location = new System.Drawing.Point(0, 40);
            this.panelInfoCaixa.Name = "panelInfoCaixa";
            this.panelInfoCaixa.Padding = new System.Windows.Forms.Padding(10);
            this.panelInfoCaixa.Size = new System.Drawing.Size(1000, 35);
            this.panelInfoCaixa.TabIndex = 1;
            // 
            // lblInfoCaixa
            // 
            this.lblInfoCaixa.AutoSize = true;
            this.lblInfoCaixa.Font = new System.Drawing.Font("Segoe UI", 13F);
            this.lblInfoCaixa.ForeColor = System.Drawing.Color.Black;
            this.lblInfoCaixa.Location = new System.Drawing.Point(10, 8);
            this.lblInfoCaixa.Name = "lblInfoCaixa";
            this.lblInfoCaixa.Size = new System.Drawing.Size(194, 25);
            this.lblInfoCaixa.TabIndex = 0;
            this.lblInfoCaixa.Text = "Caixa: Não selecionado";
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.panelTotalizacao);
            this.panelConteudo.Controls.Add(this.panelPagamento);
            this.panelConteudo.Controls.Add(this.panelProdutos);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 75);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Size = new System.Drawing.Size(1000, 455);
            this.panelConteudo.TabIndex = 2;
            // 
            // panelTotalizacao
            // 
            this.panelTotalizacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelTotalizacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTotalizacao.Controls.Add(this.lblTotal);
            this.panelTotalizacao.Controls.Add(this.lblTotalValor);
            this.panelTotalizacao.Controls.Add(this.groupBoxTipoOperacao);
            this.panelTotalizacao.Controls.Add(this.lblTrocoValor);
            this.panelTotalizacao.Controls.Add(this.btnConfirmarVenda);
            this.panelTotalizacao.Controls.Add(this.lblTroco);
            this.panelTotalizacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelTotalizacao.Location = new System.Drawing.Point(666, 0);
            this.panelTotalizacao.Name = "panelTotalizacao";
            this.panelTotalizacao.Padding = new System.Windows.Forms.Padding(15);
            this.panelTotalizacao.Size = new System.Drawing.Size(334, 455);
            this.panelTotalizacao.TabIndex = 3;
            // 
            // lblTotal
            // 
            this.lblTotal.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTotal.ForeColor = System.Drawing.Color.Black;
            this.lblTotal.Location = new System.Drawing.Point(5, 2);
            this.lblTotal.Name = "lblTotal";
            this.lblTotal.Size = new System.Drawing.Size(200, 49);
            this.lblTotal.TabIndex = 0;
            this.lblTotal.Text = "TOTAL:";
            this.lblTotal.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTotalValor
            // 
            this.lblTotalValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblTotalValor.Location = new System.Drawing.Point(5, 51);
            this.lblTotalValor.Name = "lblTotalValor";
            this.lblTotalValor.Size = new System.Drawing.Size(200, 49);
            this.lblTotalValor.TabIndex = 1;
            this.lblTotalValor.Text = "R$ 0";
            this.lblTotalValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // groupBoxTipoOperacao
            // 
            this.groupBoxTipoOperacao.Controls.Add(this._rbReimprimir);
            this.groupBoxTipoOperacao.Controls.Add(this._rbCortesia);
            this.groupBoxTipoOperacao.Controls.Add(this._rbVenda);
            this.groupBoxTipoOperacao.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.groupBoxTipoOperacao.Location = new System.Drawing.Point(5, 285);
            this.groupBoxTipoOperacao.Name = "groupBoxTipoOperacao";
            this.groupBoxTipoOperacao.Size = new System.Drawing.Size(276, 101);
            this.groupBoxTipoOperacao.TabIndex = 10;
            this.groupBoxTipoOperacao.TabStop = false;
            this.groupBoxTipoOperacao.Text = "Tipo de Operação";
            // 
            // _rbReimprimir
            // 
            this._rbReimprimir.AutoSize = true;
            this._rbReimprimir.Location = new System.Drawing.Point(10, 70);
            this._rbReimprimir.Name = "_rbReimprimir";
            this._rbReimprimir.Size = new System.Drawing.Size(90, 19);
            this._rbReimprimir.TabIndex = 2;
            this._rbReimprimir.Text = "REIMPRIMIR";
            this._rbReimprimir.UseVisualStyleBackColor = true;
            this._rbReimprimir.CheckedChanged += new System.EventHandler(this.RbTipoOperacao_CheckedChanged);
            // 
            // _rbCortesia
            // 
            this._rbCortesia.AutoSize = true;
            this._rbCortesia.Location = new System.Drawing.Point(10, 45);
            this._rbCortesia.Name = "_rbCortesia";
            this._rbCortesia.Size = new System.Drawing.Size(142, 19);
            this._rbCortesia.TabIndex = 1;
            this._rbCortesia.Text = "AMIGOS DA ALIANÇA";
            this._rbCortesia.UseVisualStyleBackColor = true;
            this._rbCortesia.CheckedChanged += new System.EventHandler(this.RbTipoOperacao_CheckedChanged);
            // 
            // _rbVenda
            // 
            this._rbVenda.AutoSize = true;
            this._rbVenda.Checked = true;
            this._rbVenda.Location = new System.Drawing.Point(10, 20);
            this._rbVenda.Name = "_rbVenda";
            this._rbVenda.Size = new System.Drawing.Size(63, 19);
            this._rbVenda.TabIndex = 0;
            this._rbVenda.TabStop = true;
            this._rbVenda.Text = "VENDA";
            this._rbVenda.UseVisualStyleBackColor = true;
            this._rbVenda.CheckedChanged += new System.EventHandler(this.RbTipoOperacao_CheckedChanged);
            // 
            // lblTrocoValor
            // 
            this.lblTrocoValor.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTrocoValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.lblTrocoValor.Location = new System.Drawing.Point(5, 147);
            this.lblTrocoValor.Name = "lblTrocoValor";
            this.lblTrocoValor.Size = new System.Drawing.Size(200, 38);
            this.lblTrocoValor.TabIndex = 3;
            this.lblTrocoValor.Text = "R$ 0";
            this.lblTrocoValor.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnConfirmarVenda
            // 
            this.btnConfirmarVenda.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConfirmarVenda.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnConfirmarVenda.FlatAppearance.BorderSize = 0;
            this.btnConfirmarVenda.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmarVenda.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnConfirmarVenda.ForeColor = System.Drawing.Color.White;
            this.btnConfirmarVenda.Location = new System.Drawing.Point(5, 195);
            this.btnConfirmarVenda.Name = "btnConfirmarVenda";
            this.btnConfirmarVenda.Size = new System.Drawing.Size(319, 84);
            this.btnConfirmarVenda.TabIndex = 0;
            this.btnConfirmarVenda.Text = "✅ CONFIRMAR";
            this.btnConfirmarVenda.UseVisualStyleBackColor = false;
            this.btnConfirmarVenda.Click += new System.EventHandler(this.BtnConfirmarVenda_Click);
            // 
            // lblTroco
            // 
            this.lblTroco.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTroco.ForeColor = System.Drawing.Color.Black;
            this.lblTroco.Location = new System.Drawing.Point(5, 98);
            this.lblTroco.Name = "lblTroco";
            this.lblTroco.Size = new System.Drawing.Size(200, 49);
            this.lblTroco.TabIndex = 2;
            this.lblTroco.Text = "TROCO:";
            this.lblTroco.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panelPagamento
            // 
            this.panelPagamento.AutoScroll = true;
            this.panelPagamento.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.panelPagamento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelPagamento.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelPagamento.Location = new System.Drawing.Point(333, 0);
            this.panelPagamento.Name = "panelPagamento";
            this.panelPagamento.Padding = new System.Windows.Forms.Padding(10);
            this.panelPagamento.Size = new System.Drawing.Size(333, 455);
            this.panelPagamento.TabIndex = 4;
            // 
            // panelProdutos
            // 
            this.panelProdutos.AutoScroll = true;
            this.panelProdutos.BackColor = System.Drawing.Color.White;
            this.panelProdutos.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelProdutos.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelProdutos.Location = new System.Drawing.Point(0, 0);
            this.panelProdutos.Name = "panelProdutos";
            this.panelProdutos.Padding = new System.Windows.Forms.Padding(10);
            this.panelProdutos.Size = new System.Drawing.Size(333, 455);
            this.panelProdutos.TabIndex = 0;
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBotoes.Controls.Add(this.btnAtualizarPDV);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Location = new System.Drawing.Point(0, 530);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Padding = new System.Windows.Forms.Padding(10);
            this.panelBotoes.Size = new System.Drawing.Size(1000, 50);
            this.panelBotoes.TabIndex = 4;
            // 
            // btnAtualizarPDV
            // 
            this.btnAtualizarPDV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnAtualizarPDV.FlatAppearance.BorderSize = 0;
            this.btnAtualizarPDV.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAtualizarPDV.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnAtualizarPDV.ForeColor = System.Drawing.Color.White;
            this.btnAtualizarPDV.Location = new System.Drawing.Point(15, 8);
            this.btnAtualizarPDV.Name = "btnAtualizarPDV";
            this.btnAtualizarPDV.Size = new System.Drawing.Size(174, 30);
            this.btnAtualizarPDV.TabIndex = 2;
            this.btnAtualizarPDV.Text = "🔄 ATUALIZAR/LIMPAR";
            this.btnAtualizarPDV.UseVisualStyleBackColor = false;
            this.btnAtualizarPDV.Click += new System.EventHandler(this.btnAtualizarPDV_Click);
            // 
            // FormPDV
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 580);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelBotoes);
            this.Controls.Add(this.panelInfoCaixa);
            this.Controls.Add(this.panelTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPDV";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDV Caixa";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormPDV_Load);
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelInfoCaixa.ResumeLayout(false);
            this.panelInfoCaixa.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelTotalizacao.ResumeLayout(false);
            this.groupBoxTipoOperacao.ResumeLayout(false);
            this.groupBoxTipoOperacao.PerformLayout();
            this.panelBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelInfoCaixa;
        private System.Windows.Forms.Label lblInfoCaixa;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Panel panelProdutos;
        private System.Windows.Forms.Panel panelTotalizacao;
        private System.Windows.Forms.Label lblTotal;
        private System.Windows.Forms.Label lblTotalValor;
        private System.Windows.Forms.Label lblTroco;
        private System.Windows.Forms.Label lblTrocoValor;
        private System.Windows.Forms.Panel panelPagamento;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnConfirmarVenda;
        private System.Windows.Forms.GroupBox groupBoxTipoOperacao;
        private System.Windows.Forms.RadioButton _rbCortesia;
        private System.Windows.Forms.RadioButton _rbVenda;
        private System.Windows.Forms.RadioButton _rbReimprimir;
        private System.Windows.Forms.Button btnAtualizarPDV;
    }
}