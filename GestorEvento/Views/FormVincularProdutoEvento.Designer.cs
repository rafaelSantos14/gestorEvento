namespace GestorEvento.Views
{
    partial class FormVincularProdutoEvento
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
            this.lblProduto = new System.Windows.Forms.Label();
            this.lblPreco = new System.Windows.Forms.Label();
            this.lblQuantidade = new System.Windows.Forms.Label();
            this.txtQuantidade = new System.Windows.Forms.TextBox();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.txtPreco = new System.Windows.Forms.TextBox();
            this.chkPermiteValorZerado = new System.Windows.Forms.CheckBox();
            this.lblHistorico = new System.Windows.Forms.Label();
            this.dgvHistorico = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).BeginInit();
            this.SuspendLayout();
            // 
            // lblProduto
            // 
            this.lblProduto.AutoSize = true;
            this.lblProduto.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblProduto.Location = new System.Drawing.Point(8, 9);
            this.lblProduto.Name = "lblProduto";
            this.lblProduto.Size = new System.Drawing.Size(70, 20);
            this.lblProduto.TabIndex = 0;
            this.lblProduto.Text = "Produto:";
            // 
            // lblPreco
            // 
            this.lblPreco.AutoSize = true;
            this.lblPreco.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPreco.Location = new System.Drawing.Point(67, 59);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(46, 19);
            this.lblPreco.TabIndex = 1;
            this.lblPreco.Text = "Preço:";
            // 
            // lblQuantidade
            // 
            this.lblQuantidade.AutoSize = true;
            this.lblQuantidade.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblQuantidade.Location = new System.Drawing.Point(273, 59);
            this.lblQuantidade.Name = "lblQuantidade";
            this.lblQuantidade.Size = new System.Drawing.Size(84, 19);
            this.lblQuantidade.TabIndex = 3;
            this.lblQuantidade.Text = "Quantidade:";
            // 
            // txtQuantidade
            // 
            this.txtQuantidade.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtQuantidade.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtQuantidade.Location = new System.Drawing.Point(273, 84);
            this.txtQuantidade.Name = "txtQuantidade";
            this.txtQuantidade.Size = new System.Drawing.Size(200, 25);
            this.txtQuantidade.TabIndex = 4;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(172, 150);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(95, 35);
            this.btnSalvar.TabIndex = 5;
            this.btnSalvar.Text = "SALVAR";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.btnCancelar.FlatAppearance.BorderSize = 0;
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(277, 150);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(95, 35);
            this.btnCancelar.TabIndex = 6;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // txtPreco
            // 
            this.txtPreco.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPreco.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPreco.Location = new System.Drawing.Point(67, 84);
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(200, 25);
            this.txtPreco.TabIndex = 2;
            this.txtPreco.TextChanged += new System.EventHandler(this.TxtPreco_TextChanged);
            this.txtPreco.Leave += new System.EventHandler(this.TxtPreco_Leave);
            // 
            // chkPermiteValorZerado
            // 
            this.chkPermiteValorZerado.AutoSize = true;
            this.chkPermiteValorZerado.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.chkPermiteValorZerado.Location = new System.Drawing.Point(67, 116);
            this.chkPermiteValorZerado.Name = "chkPermiteValorZerado";
            this.chkPermiteValorZerado.Size = new System.Drawing.Size(150, 21);
            this.chkPermiteValorZerado.TabIndex = 5;
            this.chkPermiteValorZerado.Text = "Permitir valor zerado";
            this.chkPermiteValorZerado.UseVisualStyleBackColor = true;
            // 
            // lblHistorico
            // 
            this.lblHistorico.AutoSize = true;
            this.lblHistorico.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHistorico.Location = new System.Drawing.Point(12, 212);
            this.lblHistorico.Name = "lblHistorico";
            this.lblHistorico.Size = new System.Drawing.Size(149, 19);
            this.lblHistorico.TabIndex = 8;
            this.lblHistorico.Text = "Histórico de Alterações";
            // 
            // dgvHistorico
            // 
            this.dgvHistorico.AllowUserToAddRows = false;
            this.dgvHistorico.AllowUserToDeleteRows = false;
            this.dgvHistorico.BackgroundColor = System.Drawing.Color.White;
            this.dgvHistorico.Location = new System.Drawing.Point(16, 234);
            this.dgvHistorico.Name = "dgvHistorico";
            this.dgvHistorico.ReadOnly = true;
            this.dgvHistorico.RowHeadersVisible = false;
            this.dgvHistorico.Size = new System.Drawing.Size(511, 209);
            this.dgvHistorico.TabIndex = 9;
            // 
            // FormVincularProdutoEvento
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(550, 455);
            this.Controls.Add(this.dgvHistorico);
            this.Controls.Add(this.lblHistorico);
            this.Controls.Add(this.chkPermiteValorZerado);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnSalvar);
            this.Controls.Add(this.txtQuantidade);
            this.Controls.Add(this.lblQuantidade);
            this.Controls.Add(this.txtPreco);
            this.Controls.Add(this.lblPreco);
            this.Controls.Add(this.lblProduto);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormVincularProdutoEvento";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Vincular Produto";
            this.Load += new System.EventHandler(this.FormVinculacaoProduto_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvHistorico)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblProduto;
        private System.Windows.Forms.Label lblPreco;
        private System.Windows.Forms.Label lblQuantidade;
        private System.Windows.Forms.TextBox txtQuantidade;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.TextBox txtPreco;
        private System.Windows.Forms.CheckBox chkPermiteValorZerado;
        private System.Windows.Forms.Label lblHistorico;
        private System.Windows.Forms.DataGridView dgvHistorico;
    }
}
