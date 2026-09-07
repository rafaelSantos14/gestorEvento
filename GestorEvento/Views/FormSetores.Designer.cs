namespace GestorEvento.Views
{
    partial class FormSetores
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

        #region Código gerado pelo Windows Form Designer

        private void InitializeComponent()
        {
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabCadastro = new System.Windows.Forms.TabPage();
            this.txtNomeSetor = new System.Windows.Forms.TextBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.panelBotoesCadastro = new System.Windows.Forms.Panel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.tabLista = new System.Windows.Forms.TabPage();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnEditar = new System.Windows.Forms.Button();
            this.dgvSetores = new System.Windows.Forms.DataGridView();
            this.panelPesquisa = new System.Windows.Forms.Panel();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.txtPesquisar = new System.Windows.Forms.TextBox();
            this.panelTitulo.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabCadastro.SuspendLayout();
            this.panelBotoesCadastro.SuspendLayout();
            this.tabLista.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetores)).BeginInit();
            this.panelPesquisa.SuspendLayout();
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
            this.panelTitulo.Size = new System.Drawing.Size(900, 40);
            this.panelTitulo.TabIndex = 10;
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
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(810, 0);
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
            this.btnFechar.Location = new System.Drawing.Point(855, 0);
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
            this.lblTitulo.Size = new System.Drawing.Size(160, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Cadastro de Setores";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabCadastro);
            this.tabControl1.Controls.Add(this.tabLista);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl1.Location = new System.Drawing.Point(0, 40);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(900, 502);
            this.tabControl1.TabIndex = 0;
            // 
            // tabCadastro
            // 
            this.tabCadastro.BackColor = System.Drawing.Color.White;
            this.tabCadastro.Controls.Add(this.txtNomeSetor);
            this.tabCadastro.Controls.Add(this.lblNome);
            this.tabCadastro.Controls.Add(this.panelBotoesCadastro);
            this.tabCadastro.Location = new System.Drawing.Point(4, 26);
            this.tabCadastro.Name = "tabCadastro";
            this.tabCadastro.Padding = new System.Windows.Forms.Padding(15);
            this.tabCadastro.Size = new System.Drawing.Size(892, 472);
            this.tabCadastro.TabIndex = 0;
            this.tabCadastro.Text = "CADASTRO";
            // 
            // txtNomeSetor
            // 
            this.txtNomeSetor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNomeSetor.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtNomeSetor.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNomeSetor.Location = new System.Drawing.Point(77, 43);
            this.txtNomeSetor.Name = "txtNomeSetor";
            this.txtNomeSetor.Size = new System.Drawing.Size(400, 27);
            this.txtNomeSetor.TabIndex = 2;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNome.ForeColor = System.Drawing.Color.Black;
            this.lblNome.Location = new System.Drawing.Point(18, 45);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(53, 20);
            this.lblNome.TabIndex = 1;
            this.lblNome.Text = "Nome:";
            // 
            // panelBotoesCadastro
            // 
            this.panelBotoesCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBotoesCadastro.Controls.Add(this.btnSalvar);
            this.panelBotoesCadastro.Controls.Add(this.btnLimpar);
            this.panelBotoesCadastro.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoesCadastro.Location = new System.Drawing.Point(15, 407);
            this.panelBotoesCadastro.Name = "panelBotoesCadastro";
            this.panelBotoesCadastro.Padding = new System.Windows.Forms.Padding(10);
            this.panelBotoesCadastro.Size = new System.Drawing.Size(862, 50);
            this.panelBotoesCadastro.TabIndex = 3;
            // 
            // btnSalvar
            // 
            this.btnSalvar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnSalvar.FlatAppearance.BorderSize = 0;
            this.btnSalvar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalvar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSalvar.ForeColor = System.Drawing.Color.White;
            this.btnSalvar.Location = new System.Drawing.Point(10, 11);
            this.btnSalvar.Name = "btnSalvar";
            this.btnSalvar.Size = new System.Drawing.Size(100, 30);
            this.btnSalvar.TabIndex = 7;
            this.btnSalvar.Text = "SALVAR";
            this.btnSalvar.UseVisualStyleBackColor = false;
            this.btnSalvar.Click += new System.EventHandler(this.btnSalvar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnLimpar.FlatAppearance.BorderSize = 0;
            this.btnLimpar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimpar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLimpar.ForeColor = System.Drawing.Color.White;
            this.btnLimpar.Location = new System.Drawing.Point(120, 11);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(100, 30);
            this.btnLimpar.TabIndex = 8;
            this.btnLimpar.Text = "LIMPAR";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // tabLista
            // 
            this.tabLista.BackColor = System.Drawing.Color.White;
            this.tabLista.Controls.Add(this.panelBotoes);
            this.tabLista.Controls.Add(this.dgvSetores);
            this.tabLista.Controls.Add(this.panelPesquisa);
            this.tabLista.Location = new System.Drawing.Point(4, 26);
            this.tabLista.Name = "tabLista";
            this.tabLista.Padding = new System.Windows.Forms.Padding(10);
            this.tabLista.Size = new System.Drawing.Size(892, 472);
            this.tabLista.TabIndex = 1;
            this.tabLista.Text = "LISTA DE SETORES";
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBotoes.Controls.Add(this.btnEditar);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Location = new System.Drawing.Point(10, 412);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Padding = new System.Windows.Forms.Padding(10);
            this.panelBotoes.Size = new System.Drawing.Size(872, 50);
            this.panelBotoes.TabIndex = 2;
            // 
            // btnEditar
            // 
            this.btnEditar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnEditar.FlatAppearance.BorderSize = 0;
            this.btnEditar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEditar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEditar.ForeColor = System.Drawing.Color.White;
            this.btnEditar.Location = new System.Drawing.Point(10, 11);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(100, 30);
            this.btnEditar.TabIndex = 4;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            //
            // dgvSetores
            // 
            this.dgvSetores.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dgvSetores.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvSetores.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSetores.Location = new System.Drawing.Point(10, 60);
            this.dgvSetores.Name = "dgvSetores";
            this.dgvSetores.ReadOnly = true;
            this.dgvSetores.Size = new System.Drawing.Size(872, 352);
            this.dgvSetores.TabIndex = 1;
            // 
            // panelPesquisa
            // 
            this.panelPesquisa.BackColor = System.Drawing.Color.White;
            this.panelPesquisa.Controls.Add(this.btnPesquisar);
            this.panelPesquisa.Controls.Add(this.txtPesquisar);
            this.panelPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPesquisa.Location = new System.Drawing.Point(10, 10);
            this.panelPesquisa.Name = "panelPesquisa";
            this.panelPesquisa.Padding = new System.Windows.Forms.Padding(10);
            this.panelPesquisa.Size = new System.Drawing.Size(872, 50);
            this.panelPesquisa.TabIndex = 0;
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.Location = new System.Drawing.Point(366, 10);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(100, 27);
            this.btnPesquisar.TabIndex = 2;
            this.btnPesquisar.Text = "PESQUISAR";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // txtPesquisar
            // 
            this.txtPesquisar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPesquisar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtPesquisar.Location = new System.Drawing.Point(10, 10);
            this.txtPesquisar.Name = "txtPesquisar";
            this.txtPesquisar.Size = new System.Drawing.Size(350, 27);
            this.txtPesquisar.TabIndex = 1;
            // 
            // FormSetores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 542);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.panelTitulo);
            this.ForeColor = System.Drawing.SystemColors.Control;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormSetores";
            this.Text = "Cadastro de Setores";
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabCadastro.ResumeLayout(false);
            this.tabCadastro.PerformLayout();
            this.panelBotoesCadastro.ResumeLayout(false);
            this.tabLista.ResumeLayout(false);
            this.panelBotoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSetores)).EndInit();
            this.panelPesquisa.ResumeLayout(false);
            this.panelPesquisa.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabCadastro;
        private System.Windows.Forms.TabPage tabLista;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.TextBox txtNomeSetor;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Panel panelPesquisa;
        private System.Windows.Forms.TextBox txtPesquisar;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridView dgvSetores;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Panel panelBotoesCadastro;
        private System.Windows.Forms.Button btnEditar;
    }
}
