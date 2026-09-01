using GestorEvento.Components;

namespace GestorEvento.Views
{
    partial class RelatorioInscricaoUserControl
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
            this.panelCards = new System.Windows.Forms.Panel();
            this.pnlCardRetirado = new GestorEvento.Components.ModernCard();
            this.lblRetiradoValor = new System.Windows.Forms.Label();
            this.lblRetiradoLabel = new System.Windows.Forms.Label();
            this.pnlCardPendente = new GestorEvento.Components.ModernCard();
            this.lblPendenteValor = new System.Windows.Forms.Label();
            this.lblPendenteLabel = new System.Windows.Forms.Label();
            this.pnlCardTotal = new GestorEvento.Components.ModernCard();
            this.lblTotalValor = new System.Windows.Forms.Label();
            this.lblTotalLabel = new System.Windows.Forms.Label();
            this.panelFiltro = new System.Windows.Forms.Panel();
            this.cmbFiltroStatus = new System.Windows.Forms.ComboBox();
            this.lblFiltroStatus = new System.Windows.Forms.Label();
            this.panelGrid = new System.Windows.Forms.Panel();
            this.dgvInscricoes = new System.Windows.Forms.DataGridView();
            this.panelCards.SuspendLayout();
            this.pnlCardRetirado.SuspendLayout();
            this.pnlCardPendente.SuspendLayout();
            this.pnlCardTotal.SuspendLayout();
            this.panelFiltro.SuspendLayout();
            this.panelGrid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscricoes)).BeginInit();
            this.SuspendLayout();
            //
            // panelCards
            //
            this.panelCards.BackColor = System.Drawing.Color.White;
            this.panelCards.Controls.Add(this.pnlCardRetirado);
            this.panelCards.Controls.Add(this.pnlCardPendente);
            this.panelCards.Controls.Add(this.pnlCardTotal);
            this.panelCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelCards.Location = new System.Drawing.Point(0, 0);
            this.panelCards.Name = "panelCards";
            this.panelCards.Padding = new System.Windows.Forms.Padding(15);
            this.panelCards.Size = new System.Drawing.Size(958, 140);
            this.panelCards.TabIndex = 0;
            //
            // pnlCardRetirado
            //
            this.pnlCardRetirado.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(245)))), ((int)(((byte)(230)))));
            this.pnlCardRetirado.BorderRadius = 12;
            this.pnlCardRetirado.Controls.Add(this.lblRetiradoValor);
            this.pnlCardRetirado.Controls.Add(this.lblRetiradoLabel);
            this.pnlCardRetirado.ForeColor = System.Drawing.Color.Black;
            this.pnlCardRetirado.Location = new System.Drawing.Point(682, 20);
            this.pnlCardRetirado.Name = "pnlCardRetirado";
            this.pnlCardRetirado.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardRetirado.ShadowSize = 4;
            this.pnlCardRetirado.Size = new System.Drawing.Size(290, 100);
            this.pnlCardRetirado.TabIndex = 2;
            //
            // lblRetiradoValor
            //
            this.lblRetiradoValor.AutoSize = true;
            this.lblRetiradoValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblRetiradoValor.ForeColor = System.Drawing.Color.Green;
            this.lblRetiradoValor.Location = new System.Drawing.Point(10, 40);
            this.lblRetiradoValor.Name = "lblRetiradoValor";
            this.lblRetiradoValor.Size = new System.Drawing.Size(21, 51);
            this.lblRetiradoValor.TabIndex = 1;
            this.lblRetiradoValor.Text = "-";
            //
            // lblRetiradoLabel
            //
            this.lblRetiradoLabel.AutoSize = true;
            this.lblRetiradoLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblRetiradoLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblRetiradoLabel.Location = new System.Drawing.Point(10, 10);
            this.lblRetiradoLabel.Name = "lblRetiradoLabel";
            this.lblRetiradoLabel.Size = new System.Drawing.Size(133, 19);
            this.lblRetiradoLabel.TabIndex = 0;
            this.lblRetiradoLabel.Text = "Inscrições Retiradas";
            //
            // pnlCardPendente
            //
            this.pnlCardPendente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(243)))), ((int)(((byte)(224)))));
            this.pnlCardPendente.BorderRadius = 12;
            this.pnlCardPendente.Controls.Add(this.lblPendenteValor);
            this.pnlCardPendente.Controls.Add(this.lblPendenteLabel);
            this.pnlCardPendente.ForeColor = System.Drawing.Color.Black;
            this.pnlCardPendente.Location = new System.Drawing.Point(347, 20);
            this.pnlCardPendente.Name = "pnlCardPendente";
            this.pnlCardPendente.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardPendente.ShadowSize = 4;
            this.pnlCardPendente.Size = new System.Drawing.Size(290, 100);
            this.pnlCardPendente.TabIndex = 1;
            //
            // lblPendenteValor
            //
            this.lblPendenteValor.AutoSize = true;
            this.lblPendenteValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblPendenteValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(152)))), ((int)(((byte)(0)))));
            this.lblPendenteValor.Location = new System.Drawing.Point(10, 40);
            this.lblPendenteValor.Name = "lblPendenteValor";
            this.lblPendenteValor.Size = new System.Drawing.Size(21, 51);
            this.lblPendenteValor.TabIndex = 1;
            this.lblPendenteValor.Text = "-";
            //
            // lblPendenteLabel
            //
            this.lblPendenteLabel.AutoSize = true;
            this.lblPendenteLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPendenteLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblPendenteLabel.Location = new System.Drawing.Point(10, 10);
            this.lblPendenteLabel.Name = "lblPendenteLabel";
            this.lblPendenteLabel.Size = new System.Drawing.Size(140, 19);
            this.lblPendenteLabel.TabIndex = 0;
            this.lblPendenteLabel.Text = "Inscrições Pendentes";
            //
            // pnlCardTotal
            //
            this.pnlCardTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(240)))), ((int)(((byte)(255)))));
            this.pnlCardTotal.BorderRadius = 12;
            this.pnlCardTotal.Controls.Add(this.lblTotalValor);
            this.pnlCardTotal.Controls.Add(this.lblTotalLabel);
            this.pnlCardTotal.ForeColor = System.Drawing.Color.Black;
            this.pnlCardTotal.Location = new System.Drawing.Point(12, 20);
            this.pnlCardTotal.Name = "pnlCardTotal";
            this.pnlCardTotal.ShadowColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.pnlCardTotal.ShadowSize = 4;
            this.pnlCardTotal.Size = new System.Drawing.Size(290, 100);
            this.pnlCardTotal.TabIndex = 0;
            //
            // lblTotalValor
            //
            this.lblTotalValor.AutoSize = true;
            this.lblTotalValor.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblTotalValor.Location = new System.Drawing.Point(10, 40);
            this.lblTotalValor.Name = "lblTotalValor";
            this.lblTotalValor.Size = new System.Drawing.Size(21, 51);
            this.lblTotalValor.TabIndex = 1;
            this.lblTotalValor.Text = "-";
            //
            // lblTotalLabel
            //
            this.lblTotalLabel.AutoSize = true;
            this.lblTotalLabel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTotalLabel.ForeColor = System.Drawing.Color.DarkGray;
            this.lblTotalLabel.Location = new System.Drawing.Point(10, 10);
            this.lblTotalLabel.Name = "lblTotalLabel";
            this.lblTotalLabel.Size = new System.Drawing.Size(120, 19);
            this.lblTotalLabel.TabIndex = 0;
            this.lblTotalLabel.Text = "Total de Inscrições";
            //
            // panelFiltro
            //
            this.panelFiltro.BackColor = System.Drawing.Color.White;
            this.panelFiltro.Controls.Add(this.cmbFiltroStatus);
            this.panelFiltro.Controls.Add(this.lblFiltroStatus);
            this.panelFiltro.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelFiltro.Location = new System.Drawing.Point(0, 140);
            this.panelFiltro.Name = "panelFiltro";
            this.panelFiltro.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.panelFiltro.Size = new System.Drawing.Size(958, 50);
            this.panelFiltro.TabIndex = 1;
            //
            // cmbFiltroStatus
            //
            this.cmbFiltroStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFiltroStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbFiltroStatus.FormattingEnabled = true;
            this.cmbFiltroStatus.Items.AddRange(new object[] {
            "Todos",
            "Pendente",
            "Retirado"});
            this.cmbFiltroStatus.Location = new System.Drawing.Point(78, 12);
            this.cmbFiltroStatus.Name = "cmbFiltroStatus";
            this.cmbFiltroStatus.Size = new System.Drawing.Size(160, 25);
            this.cmbFiltroStatus.TabIndex = 1;
            this.cmbFiltroStatus.SelectedIndexChanged += new System.EventHandler(this.CmbFiltroStatus_SelectedIndexChanged);
            //
            // lblFiltroStatus
            //
            this.lblFiltroStatus.AutoSize = true;
            this.lblFiltroStatus.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblFiltroStatus.Location = new System.Drawing.Point(15, 15);
            this.lblFiltroStatus.Name = "lblFiltroStatus";
            this.lblFiltroStatus.Size = new System.Drawing.Size(50, 19);
            this.lblFiltroStatus.TabIndex = 0;
            this.lblFiltroStatus.Text = "Status:";
            //
            // panelGrid
            //
            this.panelGrid.BackColor = System.Drawing.Color.White;
            this.panelGrid.Controls.Add(this.dgvInscricoes);
            this.panelGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelGrid.Location = new System.Drawing.Point(0, 190);
            this.panelGrid.Name = "panelGrid";
            this.panelGrid.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.panelGrid.Size = new System.Drawing.Size(958, 212);
            this.panelGrid.TabIndex = 2;
            //
            // dgvInscricoes
            //
            this.dgvInscricoes.BackgroundColor = System.Drawing.Color.White;
            this.dgvInscricoes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvInscricoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvInscricoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvInscricoes.Location = new System.Drawing.Point(15, 0);
            this.dgvInscricoes.Name = "dgvInscricoes";
            this.dgvInscricoes.ReadOnly = true;
            this.dgvInscricoes.RowHeadersVisible = false;
            this.dgvInscricoes.Size = new System.Drawing.Size(928, 197);
            this.dgvInscricoes.TabIndex = 0;
            //
            // RelatorioInscricaoUserControl
            //
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.panelGrid);
            this.Controls.Add(this.panelFiltro);
            this.Controls.Add(this.panelCards);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "RelatorioInscricaoUserControl";
            this.Size = new System.Drawing.Size(958, 402);
            this.panelCards.ResumeLayout(false);
            this.pnlCardRetirado.ResumeLayout(false);
            this.pnlCardRetirado.PerformLayout();
            this.pnlCardPendente.ResumeLayout(false);
            this.pnlCardPendente.PerformLayout();
            this.pnlCardTotal.ResumeLayout(false);
            this.pnlCardTotal.PerformLayout();
            this.panelFiltro.ResumeLayout(false);
            this.panelFiltro.PerformLayout();
            this.panelGrid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvInscricoes)).EndInit();
            this.ResumeLayout(false);

        }

        private System.Windows.Forms.Panel panelCards;
        private ModernCard pnlCardTotal;
        private System.Windows.Forms.Label lblTotalValor;
        private System.Windows.Forms.Label lblTotalLabel;
        private ModernCard pnlCardPendente;
        private System.Windows.Forms.Label lblPendenteValor;
        private System.Windows.Forms.Label lblPendenteLabel;
        private ModernCard pnlCardRetirado;
        private System.Windows.Forms.Label lblRetiradoValor;
        private System.Windows.Forms.Label lblRetiradoLabel;
        private System.Windows.Forms.Panel panelFiltro;
        private System.Windows.Forms.Label lblFiltroStatus;
        private System.Windows.Forms.ComboBox cmbFiltroStatus;
        private System.Windows.Forms.Panel panelGrid;
        private System.Windows.Forms.DataGridView dgvInscricoes;
    }
}
