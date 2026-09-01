namespace GestorEvento.Views
{
    partial class FormImportarInscricoes
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
            this.lblArquivo = new System.Windows.Forms.Label();
            this.txtCaminhoArquivo = new System.Windows.Forms.TextBox();
            this.btnSelecionarArquivo = new System.Windows.Forms.Button();
            this.btnImportar = new System.Windows.Forms.Button();
            this.btnConfirmarImportacao = new System.Windows.Forms.Button();
            this.lblAvisoSumidos = new System.Windows.Forms.Label();
            this.btnMarcarTodosSumidos = new System.Windows.Forms.Button();
            this.btnDesmarcarTodosSumidos = new System.Windows.Forms.Button();
            this.dgvRegistrosSumidos = new System.Windows.Forms.DataGridView();
            this.lblResultado = new System.Windows.Forms.Label();
            this.txtResultado = new System.Windows.Forms.TextBox();
            this.btnFechar = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistrosSumidos)).BeginInit();
            this.SuspendLayout();
            //
            // lblArquivo
            //
            this.lblArquivo.AutoSize = true;
            this.lblArquivo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblArquivo.Location = new System.Drawing.Point(12, 15);
            this.lblArquivo.Name = "lblArquivo";
            this.lblArquivo.Size = new System.Drawing.Size(68, 19);
            this.lblArquivo.TabIndex = 0;
            this.lblArquivo.Text = "Arquivo:";
            //
            // txtCaminhoArquivo
            //
            this.txtCaminhoArquivo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtCaminhoArquivo.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtCaminhoArquivo.Location = new System.Drawing.Point(12, 37);
            this.txtCaminhoArquivo.Name = "txtCaminhoArquivo";
            this.txtCaminhoArquivo.ReadOnly = true;
            this.txtCaminhoArquivo.Size = new System.Drawing.Size(500, 25);
            this.txtCaminhoArquivo.TabIndex = 1;
            //
            // btnSelecionarArquivo
            //
            this.btnSelecionarArquivo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSelecionarArquivo.Location = new System.Drawing.Point(518, 35);
            this.btnSelecionarArquivo.Name = "btnSelecionarArquivo";
            this.btnSelecionarArquivo.Size = new System.Drawing.Size(90, 29);
            this.btnSelecionarArquivo.TabIndex = 2;
            this.btnSelecionarArquivo.Text = "...";
            this.btnSelecionarArquivo.UseVisualStyleBackColor = true;
            this.btnSelecionarArquivo.Click += new System.EventHandler(this.btnSelecionarArquivo_Click);
            //
            // btnImportar
            //
            this.btnImportar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnImportar.Location = new System.Drawing.Point(12, 78);
            this.btnImportar.Name = "btnImportar";
            this.btnImportar.Size = new System.Drawing.Size(180, 36);
            this.btnImportar.TabIndex = 3;
            this.btnImportar.Text = "📥 IMPORTAR";
            this.btnImportar.UseVisualStyleBackColor = true;
            this.btnImportar.Click += new System.EventHandler(this.btnImportar_Click);
            //
            // btnConfirmarImportacao
            //
            this.btnConfirmarImportacao.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnConfirmarImportacao.Location = new System.Drawing.Point(200, 78);
            this.btnConfirmarImportacao.Name = "btnConfirmarImportacao";
            this.btnConfirmarImportacao.Size = new System.Drawing.Size(260, 36);
            this.btnConfirmarImportacao.TabIndex = 4;
            this.btnConfirmarImportacao.Text = "✅ CONFIRMAR IMPORTAÇÃO";
            this.btnConfirmarImportacao.UseVisualStyleBackColor = true;
            this.btnConfirmarImportacao.Visible = false;
            this.btnConfirmarImportacao.Click += new System.EventHandler(this.btnConfirmarImportacao_Click);
            //
            // lblAvisoSumidos
            //
            this.lblAvisoSumidos.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAvisoSumidos.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(95)))), ((int)(((byte)(0)))));
            this.lblAvisoSumidos.Location = new System.Drawing.Point(12, 126);
            this.lblAvisoSumidos.Name = "lblAvisoSumidos";
            this.lblAvisoSumidos.Size = new System.Drawing.Size(596, 40);
            this.lblAvisoSumidos.TabIndex = 5;
            this.lblAvisoSumidos.Text = "As inscrições abaixo estão Pendentes neste evento mas não vieram nesta planil" +
    "ha. Marque quais deseja EXCLUIR - as demais serão mantidas como estão.";
            this.lblAvisoSumidos.Visible = false;
            //
            // btnMarcarTodosSumidos
            //
            this.btnMarcarTodosSumidos.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnMarcarTodosSumidos.Location = new System.Drawing.Point(12, 170);
            this.btnMarcarTodosSumidos.Name = "btnMarcarTodosSumidos";
            this.btnMarcarTodosSumidos.Size = new System.Drawing.Size(190, 28);
            this.btnMarcarTodosSumidos.TabIndex = 6;
            this.btnMarcarTodosSumidos.Text = "Marcar todos p/ excluir";
            this.btnMarcarTodosSumidos.UseVisualStyleBackColor = true;
            this.btnMarcarTodosSumidos.Enabled = false;
            this.btnMarcarTodosSumidos.Click += new System.EventHandler(this.btnMarcarTodosSumidos_Click);
            //
            // btnDesmarcarTodosSumidos
            //
            this.btnDesmarcarTodosSumidos.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnDesmarcarTodosSumidos.Location = new System.Drawing.Point(210, 170);
            this.btnDesmarcarTodosSumidos.Name = "btnDesmarcarTodosSumidos";
            this.btnDesmarcarTodosSumidos.Size = new System.Drawing.Size(190, 28);
            this.btnDesmarcarTodosSumidos.TabIndex = 7;
            this.btnDesmarcarTodosSumidos.Text = "Desmarcar todos";
            this.btnDesmarcarTodosSumidos.UseVisualStyleBackColor = true;
            this.btnDesmarcarTodosSumidos.Enabled = false;
            this.btnDesmarcarTodosSumidos.Click += new System.EventHandler(this.btnDesmarcarTodosSumidos_Click);
            //
            // dgvRegistrosSumidos
            //
            this.dgvRegistrosSumidos.AllowUserToAddRows = false;
            this.dgvRegistrosSumidos.AllowUserToDeleteRows = false;
            this.dgvRegistrosSumidos.BackgroundColor = System.Drawing.Color.White;
            this.dgvRegistrosSumidos.Location = new System.Drawing.Point(12, 206);
            this.dgvRegistrosSumidos.Name = "dgvRegistrosSumidos";
            this.dgvRegistrosSumidos.RowHeadersVisible = false;
            this.dgvRegistrosSumidos.Size = new System.Drawing.Size(596, 260);
            this.dgvRegistrosSumidos.TabIndex = 8;
            this.dgvRegistrosSumidos.Enabled = false;
            //
            // lblResultado
            //
            this.lblResultado.AutoSize = true;
            this.lblResultado.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblResultado.Location = new System.Drawing.Point(12, 478);
            this.lblResultado.Name = "lblResultado";
            this.lblResultado.Size = new System.Drawing.Size(84, 19);
            this.lblResultado.TabIndex = 9;
            this.lblResultado.Text = "Resultado:";
            //
            // txtResultado
            //
            this.txtResultado.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtResultado.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.txtResultado.Location = new System.Drawing.Point(12, 500);
            this.txtResultado.Multiline = true;
            this.txtResultado.Name = "txtResultado";
            this.txtResultado.ReadOnly = true;
            this.txtResultado.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtResultado.Size = new System.Drawing.Size(596, 220);
            this.txtResultado.TabIndex = 10;
            //
            // btnFechar
            //
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnFechar.Location = new System.Drawing.Point(508, 730);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(100, 32);
            this.btnFechar.TabIndex = 11;
            this.btnFechar.Text = "FECHAR";
            this.btnFechar.UseVisualStyleBackColor = true;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click);
            //
            // FormImportarInscricoes
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(620, 780);
            this.Controls.Add(this.btnFechar);
            this.Controls.Add(this.txtResultado);
            this.Controls.Add(this.lblResultado);
            this.Controls.Add(this.dgvRegistrosSumidos);
            this.Controls.Add(this.btnDesmarcarTodosSumidos);
            this.Controls.Add(this.btnMarcarTodosSumidos);
            this.Controls.Add(this.lblAvisoSumidos);
            this.Controls.Add(this.btnConfirmarImportacao);
            this.Controls.Add(this.btnImportar);
            this.Controls.Add(this.btnSelecionarArquivo);
            this.Controls.Add(this.txtCaminhoArquivo);
            this.Controls.Add(this.lblArquivo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormImportarInscricoes";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Importar Inscrições Antecipadas";
            ((System.ComponentModel.ISupportInitialize)(this.dgvRegistrosSumidos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblArquivo;
        private System.Windows.Forms.TextBox txtCaminhoArquivo;
        private System.Windows.Forms.Button btnSelecionarArquivo;
        private System.Windows.Forms.Button btnImportar;
        private System.Windows.Forms.Button btnConfirmarImportacao;
        private System.Windows.Forms.Label lblAvisoSumidos;
        private System.Windows.Forms.Button btnMarcarTodosSumidos;
        private System.Windows.Forms.Button btnDesmarcarTodosSumidos;
        private System.Windows.Forms.DataGridView dgvRegistrosSumidos;
        private System.Windows.Forms.Label lblResultado;
        private System.Windows.Forms.TextBox txtResultado;
        private System.Windows.Forms.Button btnFechar;
    }
}
