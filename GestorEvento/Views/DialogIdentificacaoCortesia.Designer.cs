namespace GestorEvento.Views
{
    partial class DialogIdentificacaoCortesia
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
            this.panelConteudo = new System.Windows.Forms.Panel();
            this.txtObservacao = new System.Windows.Forms.TextBox();
            this.lblObservacao = new System.Windows.Forms.Label();
            this.cmbSetor = new System.Windows.Forms.ComboBox();
            this.lblSetor = new System.Windows.Forms.Label();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.btnConfirmar = new System.Windows.Forms.Button();
            this.panelTitulo.SuspendLayout();
            this.panelConteudo.SuspendLayout();
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
            this.panelTitulo.Size = new System.Drawing.Size(320, 35);
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
            this.btnMinimizar.Location = new System.Drawing.Point(242, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(39, 35);
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
            this.btnFechar.Location = new System.Drawing.Point(281, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(39, 35);
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
            this.lblTitulo.Location = new System.Drawing.Point(9, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(199, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Identificação da Cortesia";
            // 
            // panelConteudo
            // 
            this.panelConteudo.Controls.Add(this.txtObservacao);
            this.panelConteudo.Controls.Add(this.lblObservacao);
            this.panelConteudo.Controls.Add(this.cmbSetor);
            this.panelConteudo.Controls.Add(this.lblSetor);
            this.panelConteudo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelConteudo.Location = new System.Drawing.Point(0, 35);
            this.panelConteudo.Name = "panelConteudo";
            this.panelConteudo.Padding = new System.Windows.Forms.Padding(17);
            this.panelConteudo.Size = new System.Drawing.Size(320, 198);
            this.panelConteudo.TabIndex = 1;
            // 
            // txtObservacao
            // 
            this.txtObservacao.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtObservacao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtObservacao.Location = new System.Drawing.Point(17, 105);
            this.txtObservacao.Multiline = true;
            this.txtObservacao.Name = "txtObservacao";
            this.txtObservacao.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtObservacao.Size = new System.Drawing.Size(270, 70);
            this.txtObservacao.TabIndex = 3;
            // 
            // lblObservacao
            // 
            this.lblObservacao.AutoSize = true;
            this.lblObservacao.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblObservacao.ForeColor = System.Drawing.Color.Black;
            this.lblObservacao.Location = new System.Drawing.Point(17, 82);
            this.lblObservacao.Name = "lblObservacao";
            this.lblObservacao.Size = new System.Drawing.Size(159, 20);
            this.lblObservacao.TabIndex = 2;
            this.lblObservacao.Text = "Observação (opcional)";
            // 
            // cmbSetor
            // 
            this.cmbSetor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSetor.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cmbSetor.FormattingEnabled = true;
            this.cmbSetor.Location = new System.Drawing.Point(17, 40);
            this.cmbSetor.Name = "cmbSetor";
            this.cmbSetor.Size = new System.Drawing.Size(270, 28);
            this.cmbSetor.TabIndex = 1;
            // 
            // lblSetor
            // 
            this.lblSetor.AutoSize = true;
            this.lblSetor.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblSetor.ForeColor = System.Drawing.Color.Black;
            this.lblSetor.Location = new System.Drawing.Point(17, 17);
            this.lblSetor.Name = "lblSetor";
            this.lblSetor.Size = new System.Drawing.Size(54, 20);
            this.lblSetor.TabIndex = 0;
            this.lblSetor.Text = "Setor *";
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBotoes.Controls.Add(this.btnCancelar);
            this.panelBotoes.Controls.Add(this.btnConfirmar);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Location = new System.Drawing.Point(0, 233);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Padding = new System.Windows.Forms.Padding(9);
            this.panelBotoes.Size = new System.Drawing.Size(320, 43);
            this.panelBotoes.TabIndex = 2;
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(170, 9);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(141, 26);
            this.btnCancelar.TabIndex = 1;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // btnConfirmar
            // 
            this.btnConfirmar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnConfirmar.FlatAppearance.BorderSize = 0;
            this.btnConfirmar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnConfirmar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirmar.ForeColor = System.Drawing.Color.White;
            this.btnConfirmar.Location = new System.Drawing.Point(9, 9);
            this.btnConfirmar.Name = "btnConfirmar";
            this.btnConfirmar.Size = new System.Drawing.Size(153, 26);
            this.btnConfirmar.TabIndex = 0;
            this.btnConfirmar.Text = "✓ CONFIRMAR";
            this.btnConfirmar.UseVisualStyleBackColor = false;
            this.btnConfirmar.Click += new System.EventHandler(this.BtnConfirmar_Click);
            // 
            // DialogIdentificacaoCortesia
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(320, 276);
            this.Controls.Add(this.panelConteudo);
            this.Controls.Add(this.panelBotoes);
            this.Controls.Add(this.panelTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DialogIdentificacaoCortesia";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Identificação da Cortesia";
            this.Load += new System.EventHandler(this.DialogIdentificacaoCortesia_Load);
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
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Panel panelConteudo;
        private System.Windows.Forms.Label lblSetor;
        private System.Windows.Forms.ComboBox cmbSetor;
        private System.Windows.Forms.Label lblObservacao;
        private System.Windows.Forms.TextBox txtObservacao;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.Button btnConfirmar;
    }
}
