namespace GestorEvento.Views
{
    partial class FormPrincipal
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.lblTituloTela = new System.Windows.Forms.Label();
            this.panelMenu = new System.Windows.Forms.Panel();
            this.btnSair = new System.Windows.Forms.Button();
            this.btnRelatorios = new System.Windows.Forms.Button();
            this.btnCaixa = new System.Windows.Forms.Button();
            this.btnVincular = new System.Windows.Forms.Button();
            this.btnEventos = new System.Windows.Forms.Button();
            this.btnProdutos = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelTitulo.SuspendLayout();
            this.panelMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTitulo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panelTitulo.Controls.Add(this.btnMinimizar);
            this.panelTitulo.Controls.Add(this.lblTituloTela);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(1200, 40);
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
            this.btnMinimizar.Location = new System.Drawing.Point(1153, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 38);
            this.btnMinimizar.TabIndex = 1;
            this.btnMinimizar.Text = "−";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.BtnMinimizar_Click);
            // 
            // lblTituloTela
            // 
            this.lblTituloTela.AutoSize = true;
            this.lblTituloTela.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTituloTela.ForeColor = System.Drawing.Color.White;
            this.lblTituloTela.Location = new System.Drawing.Point(10, 10);
            this.lblTituloTela.Name = "lblTituloTela";
            this.lblTituloTela.Size = new System.Drawing.Size(135, 21);
            this.lblTituloTela.TabIndex = 0;
            this.lblTituloTela.Text = "GESTOR EVENTO";
            // 
            // panelMenu
            // 
            this.panelMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelMenu.Controls.Add(this.btnSair);
            this.panelMenu.Controls.Add(this.btnRelatorios);
            this.panelMenu.Controls.Add(this.btnCaixa);
            this.panelMenu.Controls.Add(this.btnVincular);
            this.panelMenu.Controls.Add(this.btnEventos);
            this.panelMenu.Controls.Add(this.btnProdutos);
            this.panelMenu.Controls.Add(this.lblTitulo);
            this.panelMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.panelMenu.Location = new System.Drawing.Point(0, 40);
            this.panelMenu.Name = "panelMenu";
            this.panelMenu.Size = new System.Drawing.Size(202, 660);
            this.panelMenu.TabIndex = 0;
            // 
            // btnSair
            // 
            this.btnSair.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.btnSair.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnSair.FlatAppearance.BorderSize = 0;
            this.btnSair.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSair.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSair.ForeColor = System.Drawing.Color.White;
            this.btnSair.Location = new System.Drawing.Point(0, 614);
            this.btnSair.Name = "btnSair";
            this.btnSair.Size = new System.Drawing.Size(202, 46);
            this.btnSair.TabIndex = 6;
            this.btnSair.Text = "🚪 SAIR";
            this.btnSair.UseVisualStyleBackColor = false;
            this.btnSair.Click += new System.EventHandler(this.btnSair_Click);
            // 
            // btnRelatorios
            // 
            this.btnRelatorios.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnRelatorios.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRelatorios.FlatAppearance.BorderSize = 0;
            this.btnRelatorios.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRelatorios.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRelatorios.ForeColor = System.Drawing.Color.White;
            this.btnRelatorios.Location = new System.Drawing.Point(0, 191);
            this.btnRelatorios.Name = "btnRelatorios";
            this.btnRelatorios.Size = new System.Drawing.Size(202, 46);
            this.btnRelatorios.TabIndex = 5;
            this.btnRelatorios.Text = "📊 RELATÓRIOS";
            this.btnRelatorios.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRelatorios.UseVisualStyleBackColor = false;
            this.btnRelatorios.Click += new System.EventHandler(this.btnRelatorios_Click);
            // 
            // btnCaixa
            // 
            this.btnCaixa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnCaixa.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnCaixa.FlatAppearance.BorderSize = 0;
            this.btnCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCaixa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCaixa.ForeColor = System.Drawing.Color.White;
            this.btnCaixa.Location = new System.Drawing.Point(0, 145);
            this.btnCaixa.Name = "btnCaixa";
            this.btnCaixa.Size = new System.Drawing.Size(202, 46);
            this.btnCaixa.TabIndex = 4;
            this.btnCaixa.Text = "💰 FRENTE DE CAIXA";
            this.btnCaixa.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnCaixa.UseVisualStyleBackColor = false;
            this.btnCaixa.Click += new System.EventHandler(this.btnCaixa_Click);
            // 
            // btnVincular
            // 
            this.btnVincular.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnVincular.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnVincular.FlatAppearance.BorderSize = 0;
            this.btnVincular.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnVincular.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnVincular.ForeColor = System.Drawing.Color.White;
            this.btnVincular.Location = new System.Drawing.Point(0, 99);
            this.btnVincular.Name = "btnVincular";
            this.btnVincular.Size = new System.Drawing.Size(202, 46);
            this.btnVincular.TabIndex = 3;
            this.btnVincular.Text = "🔗 VINCULAR PRODUTOS";
            this.btnVincular.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnVincular.UseVisualStyleBackColor = false;
            this.btnVincular.Click += new System.EventHandler(this.btnVincular_Click);
            // 
            // btnEventos
            // 
            this.btnEventos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnEventos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnEventos.FlatAppearance.BorderSize = 0;
            this.btnEventos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEventos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnEventos.ForeColor = System.Drawing.Color.White;
            this.btnEventos.Location = new System.Drawing.Point(0, 57);
            this.btnEventos.Name = "btnEventos";
            this.btnEventos.Size = new System.Drawing.Size(202, 42);
            this.btnEventos.TabIndex = 2;
            this.btnEventos.Text = "🎪 EVENTOS";
            this.btnEventos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnEventos.UseVisualStyleBackColor = false;
            this.btnEventos.Click += new System.EventHandler(this.btnEventos_Click);
            // 
            // btnProdutos
            // 
            this.btnProdutos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnProdutos.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnProdutos.FlatAppearance.BorderSize = 0;
            this.btnProdutos.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProdutos.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnProdutos.ForeColor = System.Drawing.Color.White;
            this.btnProdutos.Location = new System.Drawing.Point(0, 0);
            this.btnProdutos.Name = "btnProdutos";
            this.btnProdutos.Size = new System.Drawing.Size(202, 57);
            this.btnProdutos.TabIndex = 1;
            this.btnProdutos.Text = "📦 PRODUTOS";
            this.btnProdutos.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnProdutos.UseVisualStyleBackColor = false;
            this.btnProdutos.Click += new System.EventHandler(this.btnProdutos_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(10, 7);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(59, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "MENU";
            // 
            // FormPrincipal
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.panelMenu);
            this.Controls.Add(this.panelTitulo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormPrincipal";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelMenu.ResumeLayout(false);
            this.panelMenu.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblTituloTela;
        private System.Windows.Forms.Panel panelMenu;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnProdutos;
        private System.Windows.Forms.Button btnEventos;
        private System.Windows.Forms.Button btnVincular;
        private System.Windows.Forms.Button btnCaixa;
        private System.Windows.Forms.Button btnRelatorios;
        private System.Windows.Forms.Button btnSair;
    }
}
