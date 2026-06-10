namespace GestorEvento.Views
{
    partial class FormRelatoriosConsolidados
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
            this.panelTitulo = new System.Windows.Forms.Panel();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.panelFiltro = new System.Windows.Forms.Panel();
            this.txtBuscaEvento = new System.Windows.Forms.TextBox();
            this.cmbStatusFiltro = new System.Windows.Forms.ComboBox();
            this.cmbEventoResultados = new System.Windows.Forms.ComboBox();
            this.lblEvento = new System.Windows.Forms.Label();
            this.lblResultados = new System.Windows.Forms.Label();
            this.lblStatus = new System.Windows.Forms.Label();
            this.tabControlRelatorios = new System.Windows.Forms.TabControl();
            this.tabPageVendas = new System.Windows.Forms.TabPage();
            this.tabPageCaixa = new System.Windows.Forms.TabPage();
            this.tabPageCortesias = new System.Windows.Forms.TabPage();
            this.tabPageReimpressoes = new System.Windows.Forms.TabPage();
            
            this.panelTitulo.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            this.tabControlRelatorios.SuspendLayout();
            this.SuspendLayout();

            // panelTitulo
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTitulo.Controls.Add(this.btnMinimizar);
            this.panelTitulo.Controls.Add(this.btnFechar);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(1000, 50);
            this.panelTitulo.TabIndex = 0;

            // btnMinimizar
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(910, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 50);
            this.btnMinimizar.TabIndex = 3;
            this.btnMinimizar.Text = "−";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.BtnMinimizar_Click);

            // btnFechar
            this.btnFechar.BackColor = System.Drawing.Color.Transparent;
            this.btnFechar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnFechar.FlatAppearance.BorderSize = 0;
            this.btnFechar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFechar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnFechar.ForeColor = System.Drawing.Color.White;
            this.btnFechar.Location = new System.Drawing.Point(955, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(45, 50);
            this.btnFechar.TabIndex = 4;
            this.btnFechar.Text = "✕";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFechar_Click);

            // lblTitulo
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(15, 12);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(289, 25);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "RELATÓRIOS CONSOLIDADOS";

            // panelFiltro
            this.panelFiltro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelFiltro.Controls.Add(this.txtBuscaEvento);
            this.panelFiltro.Controls.Add(this.cmbStatusFiltro);
            this.panelFiltro.Controls.Add(this.cmbEventoResultados);
            this.panelFiltro.Controls.Add(this.lblEvento);
            this.panelFiltro.Controls.Add(this.lblResultados);
            this.panelFiltro.Controls.Add(this.lblStatus);
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltro.Location = new System.Drawing.Point(0, 50);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Padding = new System.Windows.Forms.Padding(15);
            this.panelFiltro.Size = new System.Drawing.Size(1000, 100);
            this.panelFiltro.TabIndex = 1;

            // txtBuscaEvento
            this.txtBuscaEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBuscaEvento.Location = new System.Drawing.Point(147, 20);
            this.txtBuscaEvento.Name = "txtBuscaEvento";
            this.txtBuscaEvento.Size = new System.Drawing.Size(320, 25);
            this.txtBuscaEvento.TabIndex = 1;
            this.txtBuscaEvento.TextChanged += new System.EventHandler(this.TxtBuscaEvento_TextChanged);

            // cmbStatusFiltro
            this.cmbStatusFiltro.DropDownHeight = 100;
            this.cmbStatusFiltro.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbStatusFiltro.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbStatusFiltro.FormattingEnabled = true;
            this.cmbStatusFiltro.IntegralHeight = false;
            this.cmbStatusFiltro.Location = new System.Drawing.Point(526, 20);
            this.cmbStatusFiltro.Name = "cmbStatusFiltro";
            this.cmbStatusFiltro.Size = new System.Drawing.Size(150, 25);
            this.cmbStatusFiltro.TabIndex = 3;
            this.cmbStatusFiltro.SelectedIndexChanged += new System.EventHandler(this.CmbStatusFiltro_SelectedIndexChanged);

            // cmbEventoResultados
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

            // lblEvento
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEvento.Location = new System.Drawing.Point(15, 23);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(115, 19);
            this.lblEvento.TabIndex = 0;
            this.lblEvento.Text = "Pesquisar evento:";

            // lblResultados
            this.lblResultados.AutoSize = true;
            this.lblResultados.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblResultados.Location = new System.Drawing.Point(15, 53);
            this.lblResultados.Name = "lblResultados";
            this.lblResultados.Size = new System.Drawing.Size(60, 19);
            this.lblResultados.TabIndex = 0;
            this.lblResultados.Text = "Eventos:";

            // lblStatus
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatus.Location = new System.Drawing.Point(479, 23);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(50, 19);
            this.lblStatus.TabIndex = 5;
            this.lblStatus.Text = "Status:";

            // tabControlRelatorios
            this.tabControlRelatorios.Controls.Add(this.tabPageVendas);
            this.tabControlRelatorios.Controls.Add(this.tabPageCaixa);
            this.tabControlRelatorios.Controls.Add(this.tabPageCortesias);
            this.tabControlRelatorios.Controls.Add(this.tabPageReimpressoes);
            this.tabControlRelatorios.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlRelatorios.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tabControlRelatorios.Location = new System.Drawing.Point(0, 150);
            this.tabControlRelatorios.Name = "tabControlRelatorios";
            this.tabControlRelatorios.SelectedIndex = 0;
            this.tabControlRelatorios.Size = new System.Drawing.Size(1000, 430);
            this.tabControlRelatorios.TabIndex = 2;
            this.tabControlRelatorios.SelectedIndexChanged += new System.EventHandler(this.TabControlRelatorios_SelectedIndexChanged);

            // tabPageVendas
            this.tabPageVendas.BackColor = System.Drawing.Color.White;
            this.tabPageVendas.Location = new System.Drawing.Point(4, 24);
            this.tabPageVendas.Name = "tabPageVendas";
            this.tabPageVendas.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageVendas.Size = new System.Drawing.Size(992, 402);
            this.tabPageVendas.TabIndex = 0;
            this.tabPageVendas.Text = "Vendas";

            // tabPageCaixa
            this.tabPageCaixa.BackColor = System.Drawing.Color.White;
            this.tabPageCaixa.Location = new System.Drawing.Point(4, 24);
            this.tabPageCaixa.Name = "tabPageCaixa";
            this.tabPageCaixa.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCaixa.Size = new System.Drawing.Size(992, 402);
            this.tabPageCaixa.TabIndex = 1;
            this.tabPageCaixa.Text = "Caixa";

            // tabPageCortesias
            this.tabPageCortesias.BackColor = System.Drawing.Color.White;
            this.tabPageCortesias.Location = new System.Drawing.Point(4, 24);
            this.tabPageCortesias.Name = "tabPageCortesias";
            this.tabPageCortesias.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCortesias.Size = new System.Drawing.Size(992, 402);
            this.tabPageCortesias.TabIndex = 2;
            this.tabPageCortesias.Text = "Cortesias";

            // tabPageReimpressoes
            this.tabPageReimpressoes.BackColor = System.Drawing.Color.White;
            this.tabPageReimpressoes.Location = new System.Drawing.Point(4, 24);
            this.tabPageReimpressoes.Name = "tabPageReimpressoes";
            this.tabPageReimpressoes.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageReimpressoes.Size = new System.Drawing.Size(992, 402);
            this.tabPageReimpressoes.TabIndex = 3;
            this.tabPageReimpressoes.Text = "Reimpressões";

            // FormRelatoriosConsolidados
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 580);
            this.Controls.Add(this.tabControlRelatorios);
            this.Controls.Add(this.panelFiltro);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormRelatoriosConsolidados";
            this.Text = "Relatórios Consolidados";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.tabControlRelatorios.ResumeLayout(false);
            this.ResumeLayout(false);
        }


        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Panel panelFiltro;
        private System.Windows.Forms.TextBox txtBuscaEvento;
        private System.Windows.Forms.ComboBox cmbEventoResultados;
        private System.Windows.Forms.ComboBox cmbStatusFiltro;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.Label lblResultados;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.TabControl tabControlRelatorios;
        private System.Windows.Forms.TabPage tabPageVendas;
        private System.Windows.Forms.TabPage tabPageCaixa;
        private System.Windows.Forms.TabPage tabPageCortesias;
        private System.Windows.Forms.TabPage tabPageReimpressoes;
    }
}
