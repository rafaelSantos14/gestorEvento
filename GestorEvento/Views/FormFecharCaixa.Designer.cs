namespace GestorEvento.Views
{
    partial class FormFecharCaixa
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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.lblObservacoes = new System.Windows.Forms.Label();
            this.txtObservacoes = new System.Windows.Forms.TextBox();
            this.lblValorFinal = new System.Windows.Forms.Label();
            this.txtValorFinal = new System.Windows.Forms.TextBox();
            this.lblNomeCaixa = new System.Windows.Forms.Label();
            this.txtNomeCaixa = new System.Windows.Forms.TextBox();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnFecharCaixaBtn = new System.Windows.Forms.Button();
            this.panelTitulo.SuspendLayout();
            this.panelConteudo.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTitulo.Controls.Add(this.btnFechar);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(450, 40);
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
            this.btnFechar.Location = new System.Drawing.Point(405, 0);
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
            this.lblTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(106, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Fechar Caixa";
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.lblObservacoes);
            this.panelConteudo.Controls.Add(this.txtObservacoes);
            this.panelConteudo.Controls.Add(this.lblValorFinal);
            this.panelConteudo.Controls.Add(this.txtValorFinal);
            this.panelConteudo.Controls.Add(this.lblNomeCaixa);
            this.panelConteudo.Controls.Add(this.txtNomeCaixa);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 40);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Padding = new System.Windows.Forms.Padding(20);
            this.panelConteudo.Size = new System.Drawing.Size(450, 270);
            this.panelConteudo.TabIndex = 1;
            // 
            // lblObservacoes
            // 
            this.lblObservacoes.AutoSize = true;
            this.lblObservacoes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblObservacoes.ForeColor = System.Drawing.Color.Black;
            this.lblObservacoes.Location = new System.Drawing.Point(20, 130);
            this.lblObservacoes.Name = "lblObservacoes";
            this.lblObservacoes.Size = new System.Drawing.Size(96, 20);
            this.lblObservacoes.TabIndex = 5;
            this.lblObservacoes.Text = "Observações:";
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtObservacoes.Location = new System.Drawing.Point(20, 153);
            this.txtObservacoes.MaxLength = 300;
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Size = new System.Drawing.Size(410, 80);
            this.txtObservacoes.TabIndex = 4;
            // 
            // lblValorFinal
            // 
            this.lblValorFinal.AutoSize = true;
            this.lblValorFinal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblValorFinal.ForeColor = System.Drawing.Color.Black;
            this.lblValorFinal.Location = new System.Drawing.Point(20, 75);
            this.lblValorFinal.Name = "lblValorFinal";
            this.lblValorFinal.Size = new System.Drawing.Size(112, 20);
            this.lblValorFinal.TabIndex = 3;
            this.lblValorFinal.Text = "Valor Final (R$):";
            // 
            // txtValorFinal
            // 
            this.txtValorFinal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtValorFinal.Location = new System.Drawing.Point(20, 98);
            this.txtValorFinal.Name = "txtValorFinal";
            this.txtValorFinal.Size = new System.Drawing.Size(150, 27);
            this.txtValorFinal.TabIndex = 2;
            this.txtValorFinal.Text = "0";
            this.txtValorFinal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorFinal.TextChanged += new System.EventHandler(this.TxtValorFinal_TextChanged);
            // 
            // lblNomeCaixa
            // 
            this.lblNomeCaixa.AutoSize = true;
            this.lblNomeCaixa.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNomeCaixa.ForeColor = System.Drawing.Color.Black;
            this.lblNomeCaixa.Location = new System.Drawing.Point(20, 20);
            this.lblNomeCaixa.Name = "lblNomeCaixa";
            this.lblNomeCaixa.Size = new System.Drawing.Size(128, 20);
            this.lblNomeCaixa.TabIndex = 1;
            this.lblNomeCaixa.Text = "Número do Caixa:";
            // 
            // txtNomeCaixa
            // 
            this.txtNomeCaixa.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNomeCaixa.Location = new System.Drawing.Point(20, 43);
            this.txtNomeCaixa.Name = "txtNomeCaixa";
            this.txtNomeCaixa.ReadOnly = true;
            this.txtNomeCaixa.Size = new System.Drawing.Size(410, 27);
            this.txtNomeCaixa.TabIndex = 0;
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBotoes.Controls.Add(this.btnCancelar);
            this.panelBotoes.Controls.Add(this.btnFecharCaixaBtn);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Location = new System.Drawing.Point(0, 310);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Padding = new System.Windows.Forms.Padding(10);
            this.panelBotoes.Size = new System.Drawing.Size(450, 50);
            this.panelBotoes.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(120, 10);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(100, 30);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // btnFecharCaixaBtn
            // 
            this.btnFecharCaixaBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnFecharCaixaBtn.FlatAppearance.BorderSize = 0;
            this.btnFecharCaixaBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFecharCaixaBtn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFecharCaixaBtn.ForeColor = System.Drawing.Color.White;
            this.btnFecharCaixaBtn.Location = new System.Drawing.Point(10, 10);
            this.btnFecharCaixaBtn.Name = "btnFecharCaixaBtn";
            this.btnFecharCaixaBtn.Size = new System.Drawing.Size(100, 30);
            this.btnFecharCaixaBtn.TabIndex = 0;
            this.btnFecharCaixaBtn.Text = "🔒 FECHAR";
            this.btnFecharCaixaBtn.UseVisualStyleBackColor = false;
            this.btnFecharCaixaBtn.Click += new System.EventHandler(this.BtnFecharCaixaBtn_Click);
            // 
            // FormFecharCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(450, 360);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelBotoes);
            this.Controls.Add(this.panelTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFecharCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fechar Caixa";
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelConteudo.ResumeLayout(false);
            this.panelConteudo.PerformLayout();
            this.panelBotoes.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Label lblObservacoes;
        private System.Windows.Forms.TextBox txtObservacoes;
        private System.Windows.Forms.Label lblValorFinal;
        private System.Windows.Forms.TextBox txtValorFinal;
        private System.Windows.Forms.Label lblNomeCaixa;
        private System.Windows.Forms.TextBox txtNomeCaixa;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnFecharCaixaBtn;
    }
}
