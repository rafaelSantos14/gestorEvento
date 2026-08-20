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
            this.lblTitulo = new System.Windows.Forms.Label();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.btnFechar = new System.Windows.Forms.Button();
            this.gbResumo = new System.Windows.Forms.GroupBox();
            this.lblTotalDoacoes = new System.Windows.Forms.Label();
            this.lblValorTotalCortesias = new System.Windows.Forms.Label();
            this.lblTotalCortesias = new System.Windows.Forms.Label();
            this.lblTotalVendas = new System.Windows.Forms.Label();
            this.lblTotalEsperado = new System.Windows.Forms.Label();
            this.lblTotalSangria = new System.Windows.Forms.Label();
            this.lblTotalEntradaTroco = new System.Windows.Forms.Label();
            this.lblTotalTroco = new System.Windows.Forms.Label();
            this.lblTotalDinheiro = new System.Windows.Forms.Label();
            this.lblAbertura = new System.Windows.Forms.Label();
            this.lblTituloResumo = new System.Windows.Forms.Label();
            this.gbFormas = new System.Windows.Forms.GroupBox();
            this.tabControlFormas = new System.Windows.Forms.TabControl();
            this.tabPagamento = new System.Windows.Forms.TabPage();
            this.dgvFormasPagamento = new System.Windows.Forms.DataGridView();
            this.colFormaVisual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalVisual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.tabDoacoes = new System.Windows.Forms.TabPage();
            this.dgvDoacoes = new System.Windows.Forms.DataGridView();
            this.gbContagem = new System.Windows.Forms.GroupBox();
            this.txtObservacoes = new System.Windows.Forms.TextBox();
            this.lblObservacoes = new System.Windows.Forms.Label();
            this.lblDiferenca = new System.Windows.Forms.Label();
            this.txtValorContado = new System.Windows.Forms.TextBox();
            this.lblValorContadoLabel = new System.Windows.Forms.Label();
            this.btnFecharCaixa = new System.Windows.Forms.Button();
            this.btnCancelar = new System.Windows.Forms.Button();
            this.colFormaDoacaoVisual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colTotalDoacaoVisual = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.panelTitulo.SuspendLayout();
            this.gbResumo.SuspendLayout();
            this.gbFormas.SuspendLayout();
            this.tabControlFormas.SuspendLayout();
            this.tabPagamento.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormasPagamento)).BeginInit();
            this.tabDoacoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoacoes)).BeginInit();
            this.gbContagem.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Controls.Add(this.btnMinimizar);
            this.panelTitulo.Controls.Add(this.btnFechar);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(850, 40);
            this.panelTitulo.TabIndex = 0;
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(10, 9);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(173, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Fechamento de Caixa";
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(760, 0);
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
            this.btnFechar.Location = new System.Drawing.Point(805, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(45, 40);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "✕";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.BtnFecharTitulo_Click);
            // 
            // gbResumo
            // 
            this.gbResumo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbResumo.Controls.Add(this.lblTotalDoacoes);
            this.gbResumo.Controls.Add(this.lblValorTotalCortesias);
            this.gbResumo.Controls.Add(this.lblTotalCortesias);
            this.gbResumo.Controls.Add(this.lblTotalVendas);
            this.gbResumo.Controls.Add(this.lblTotalEsperado);
            this.gbResumo.Controls.Add(this.lblTotalSangria);
            this.gbResumo.Controls.Add(this.lblTotalEntradaTroco);
            this.gbResumo.Controls.Add(this.lblTotalTroco);
            this.gbResumo.Controls.Add(this.lblTotalDinheiro);
            this.gbResumo.Controls.Add(this.lblAbertura);
            this.gbResumo.Controls.Add(this.lblTituloResumo);
            this.gbResumo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gbResumo.Location = new System.Drawing.Point(20, 60);
            this.gbResumo.Name = "gbResumo";
            this.gbResumo.Size = new System.Drawing.Size(810, 255);
            this.gbResumo.TabIndex = 1;
            this.gbResumo.TabStop = false;
            this.gbResumo.Text = "RESUMO EXECUTIVO";
            // 
            // lblTotalDoacoes
            // 
            this.lblTotalDoacoes.AutoSize = true;
            this.lblTotalDoacoes.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalDoacoes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.lblTotalDoacoes.Location = new System.Drawing.Point(10, 150);
            this.lblTotalDoacoes.Name = "lblTotalDoacoes";
            this.lblTotalDoacoes.Size = new System.Drawing.Size(16, 17);
            this.lblTotalDoacoes.TabIndex = 10;
            this.lblTotalDoacoes.Text = "-";
            // 
            // lblValorTotalCortesias
            // 
            this.lblValorTotalCortesias.AutoSize = true;
            this.lblValorTotalCortesias.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblValorTotalCortesias.Location = new System.Drawing.Point(10, 222);
            this.lblValorTotalCortesias.Name = "lblValorTotalCortesias";
            this.lblValorTotalCortesias.Size = new System.Drawing.Size(16, 17);
            this.lblValorTotalCortesias.TabIndex = 9;
            this.lblValorTotalCortesias.Text = "-";
            // 
            // lblTotalCortesias
            // 
            this.lblTotalCortesias.AutoSize = true;
            this.lblTotalCortesias.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalCortesias.Location = new System.Drawing.Point(10, 204);
            this.lblTotalCortesias.Name = "lblTotalCortesias";
            this.lblTotalCortesias.Size = new System.Drawing.Size(16, 17);
            this.lblTotalCortesias.TabIndex = 8;
            this.lblTotalCortesias.Text = "-";
            // 
            // lblTotalVendas
            // 
            this.lblTotalVendas.AutoSize = true;
            this.lblTotalVendas.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalVendas.Location = new System.Drawing.Point(10, 186);
            this.lblTotalVendas.Name = "lblTotalVendas";
            this.lblTotalVendas.Size = new System.Drawing.Size(16, 17);
            this.lblTotalVendas.TabIndex = 7;
            this.lblTotalVendas.Text = "-";
            // 
            // lblTotalEsperado
            // 
            this.lblTotalEsperado.AutoSize = true;
            this.lblTotalEsperado.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalEsperado.Location = new System.Drawing.Point(10, 168);
            this.lblTotalEsperado.Name = "lblTotalEsperado";
            this.lblTotalEsperado.Size = new System.Drawing.Size(16, 17);
            this.lblTotalEsperado.TabIndex = 6;
            this.lblTotalEsperado.Text = "-";
            // 
            // lblTotalSangria
            // 
            this.lblTotalSangria.AutoSize = true;
            this.lblTotalSangria.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblTotalSangria.Location = new System.Drawing.Point(10, 132);
            this.lblTotalSangria.Name = "lblTotalSangria";
            this.lblTotalSangria.Size = new System.Drawing.Size(16, 17);
            this.lblTotalSangria.TabIndex = 5;
            this.lblTotalSangria.Text = "-";
            // 
            // lblTotalEntradaTroco
            // 
            this.lblTotalEntradaTroco.AutoSize = true;
            this.lblTotalEntradaTroco.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblTotalEntradaTroco.Location = new System.Drawing.Point(10, 114);
            this.lblTotalEntradaTroco.Name = "lblTotalEntradaTroco";
            this.lblTotalEntradaTroco.Size = new System.Drawing.Size(16, 17);
            this.lblTotalEntradaTroco.TabIndex = 4;
            this.lblTotalEntradaTroco.Text = "-";
            // 
            // lblTotalTroco
            // 
            this.lblTotalTroco.AutoSize = true;
            this.lblTotalTroco.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblTotalTroco.Location = new System.Drawing.Point(10, 96);
            this.lblTotalTroco.Name = "lblTotalTroco";
            this.lblTotalTroco.Size = new System.Drawing.Size(16, 17);
            this.lblTotalTroco.TabIndex = 3;
            this.lblTotalTroco.Text = "-";
            // 
            // lblTotalDinheiro
            // 
            this.lblTotalDinheiro.AutoSize = true;
            this.lblTotalDinheiro.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblTotalDinheiro.Location = new System.Drawing.Point(10, 78);
            this.lblTotalDinheiro.Name = "lblTotalDinheiro";
            this.lblTotalDinheiro.Size = new System.Drawing.Size(16, 17);
            this.lblTotalDinheiro.TabIndex = 2;
            this.lblTotalDinheiro.Text = "-";
            // 
            // lblAbertura
            // 
            this.lblAbertura.AutoSize = true;
            this.lblAbertura.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblAbertura.Location = new System.Drawing.Point(10, 60);
            this.lblAbertura.Name = "lblAbertura";
            this.lblAbertura.Size = new System.Drawing.Size(16, 17);
            this.lblAbertura.TabIndex = 1;
            this.lblAbertura.Text = "-";
            // 
            // lblTituloResumo
            // 
            this.lblTituloResumo.AutoSize = true;
            this.lblTituloResumo.Location = new System.Drawing.Point(10, 30);
            this.lblTituloResumo.Name = "lblTituloResumo";
            this.lblTituloResumo.Size = new System.Drawing.Size(15, 19);
            this.lblTituloResumo.TabIndex = 0;
            this.lblTituloResumo.Text = "-";
            // 
            // gbFormas
            // 
            this.gbFormas.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFormas.Controls.Add(this.tabControlFormas);
            this.gbFormas.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gbFormas.Location = new System.Drawing.Point(20, 325);
            this.gbFormas.Name = "gbFormas";
            this.gbFormas.Size = new System.Drawing.Size(810, 180);
            this.gbFormas.TabIndex = 2;
            this.gbFormas.TabStop = false;
            this.gbFormas.Text = "RESUMO POR FORMA DE PAGAMENTO";
            // 
            // tabControlFormas
            // 
            this.tabControlFormas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControlFormas.Controls.Add(this.tabPagamento);
            this.tabControlFormas.Controls.Add(this.tabDoacoes);
            this.tabControlFormas.Location = new System.Drawing.Point(10, 25);
            this.tabControlFormas.Name = "tabControlFormas";
            this.tabControlFormas.SelectedIndex = 0;
            this.tabControlFormas.Size = new System.Drawing.Size(790, 145);
            this.tabControlFormas.TabIndex = 0;
            // 
            // tabPagamento
            // 
            this.tabPagamento.Controls.Add(this.dgvFormasPagamento);
            this.tabPagamento.Location = new System.Drawing.Point(4, 26);
            this.tabPagamento.Name = "tabPagamento";
            this.tabPagamento.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagamento.Size = new System.Drawing.Size(782, 115);
            this.tabPagamento.TabIndex = 0;
            this.tabPagamento.Text = "Formas de Pagamento";
            this.tabPagamento.UseVisualStyleBackColor = true;
            // 
            // dgvFormasPagamento
            // 
            this.dgvFormasPagamento.AllowUserToAddRows = false;
            this.dgvFormasPagamento.AllowUserToDeleteRows = false;
            this.dgvFormasPagamento.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvFormasPagamento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvFormasPagamento.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFormaVisual,
            this.colTotalVisual});
            this.dgvFormasPagamento.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvFormasPagamento.Location = new System.Drawing.Point(3, 3);
            this.dgvFormasPagamento.Name = "dgvFormasPagamento";
            this.dgvFormasPagamento.ReadOnly = true;
            this.dgvFormasPagamento.Size = new System.Drawing.Size(776, 109);
            this.dgvFormasPagamento.TabIndex = 0;
            // 
            // colFormaVisual
            // 
            this.colFormaVisual.HeaderText = "Forma de Pagamento";
            this.colFormaVisual.Name = "colFormaVisual";
            this.colFormaVisual.ReadOnly = true;
            // 
            // colTotalVisual
            // 
            this.colTotalVisual.HeaderText = "Valor Total";
            this.colTotalVisual.Name = "colTotalVisual";
            this.colTotalVisual.ReadOnly = true;
            // 
            // tabDoacoes
            // 
            this.tabDoacoes.Controls.Add(this.dgvDoacoes);
            this.tabDoacoes.Location = new System.Drawing.Point(4, 26);
            this.tabDoacoes.Name = "tabDoacoes";
            this.tabDoacoes.Padding = new System.Windows.Forms.Padding(3);
            this.tabDoacoes.Size = new System.Drawing.Size(782, 115);
            this.tabDoacoes.TabIndex = 1;
            this.tabDoacoes.Text = "Doações";
            this.tabDoacoes.UseVisualStyleBackColor = true;
            // 
            // dgvDoacoes
            // 
            this.dgvDoacoes.AllowUserToAddRows = false;
            this.dgvDoacoes.AllowUserToDeleteRows = false;
            this.dgvDoacoes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDoacoes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvDoacoes.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFormaDoacaoVisual,
            this.colTotalDoacaoVisual});
            this.dgvDoacoes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDoacoes.Location = new System.Drawing.Point(3, 3);
            this.dgvDoacoes.Name = "dgvDoacoes";
            this.dgvDoacoes.ReadOnly = true;
            this.dgvDoacoes.Size = new System.Drawing.Size(776, 109);
            this.dgvDoacoes.TabIndex = 0;
            // 
            // gbContagem
            // 
            this.gbContagem.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbContagem.Controls.Add(this.txtObservacoes);
            this.gbContagem.Controls.Add(this.lblObservacoes);
            this.gbContagem.Controls.Add(this.lblDiferenca);
            this.gbContagem.Controls.Add(this.txtValorContado);
            this.gbContagem.Controls.Add(this.lblValorContadoLabel);
            this.gbContagem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.gbContagem.Location = new System.Drawing.Point(20, 515);
            this.gbContagem.Name = "gbContagem";
            this.gbContagem.Size = new System.Drawing.Size(810, 160);
            this.gbContagem.TabIndex = 3;
            this.gbContagem.TabStop = false;
            this.gbContagem.Text = "FECHAMENTO";
            // 
            // txtObservacoes
            // 
            this.txtObservacoes.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtObservacoes.Location = new System.Drawing.Point(10, 105);
            this.txtObservacoes.Multiline = true;
            this.txtObservacoes.Name = "txtObservacoes";
            this.txtObservacoes.Size = new System.Drawing.Size(790, 45);
            this.txtObservacoes.TabIndex = 4;
            // 
            // lblObservacoes
            // 
            this.lblObservacoes.AutoSize = true;
            this.lblObservacoes.Location = new System.Drawing.Point(10, 83);
            this.lblObservacoes.Name = "lblObservacoes";
            this.lblObservacoes.Size = new System.Drawing.Size(90, 19);
            this.lblObservacoes.TabIndex = 3;
            this.lblObservacoes.Text = "Observações:";
            // 
            // lblDiferenca
            // 
            this.lblDiferenca.AutoSize = true;
            this.lblDiferenca.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblDiferenca.ForeColor = System.Drawing.Color.Green;
            this.lblDiferenca.Location = new System.Drawing.Point(220, 50);
            this.lblDiferenca.Name = "lblDiferenca";
            this.lblDiferenca.Size = new System.Drawing.Size(190, 25);
            this.lblDiferenca.TabIndex = 2;
            this.lblDiferenca.Text = "DIFERENÇA: R$ 0,00";
            // 
            // txtValorContado
            // 
            this.txtValorContado.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.txtValorContado.Location = new System.Drawing.Point(10, 46);
            this.txtValorContado.Name = "txtValorContado";
            this.txtValorContado.Size = new System.Drawing.Size(200, 32);
            this.txtValorContado.TabIndex = 1;
            this.txtValorContado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtValorContado.TextChanged += new System.EventHandler(this.TxtValorContado_TextChanged);
            // 
            // lblValorContadoLabel
            // 
            this.lblValorContadoLabel.AutoSize = true;
            this.lblValorContadoLabel.Location = new System.Drawing.Point(10, 24);
            this.lblValorContadoLabel.Name = "lblValorContadoLabel";
            this.lblValorContadoLabel.Size = new System.Drawing.Size(155, 19);
            this.lblValorContadoLabel.TabIndex = 0;
            this.lblValorContadoLabel.Text = "Valor Contado em Mão:";
            // 
            // btnFecharCaixa
            // 
            this.btnFecharCaixa.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnFecharCaixa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(175)))), ((int)(((byte)(80)))));
            this.btnFecharCaixa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFecharCaixa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnFecharCaixa.ForeColor = System.Drawing.Color.White;
            this.btnFecharCaixa.Location = new System.Drawing.Point(20, 703);
            this.btnFecharCaixa.Name = "btnFecharCaixa";
            this.btnFecharCaixa.Size = new System.Drawing.Size(150, 35);
            this.btnFecharCaixa.TabIndex = 4;
            this.btnFecharCaixa.Text = "FECHAR CAIXA";
            this.btnFecharCaixa.UseVisualStyleBackColor = false;
            this.btnFecharCaixa.Click += new System.EventHandler(this.BtnFecharCaixa_Click);
            // 
            // btnCancelar
            // 
            this.btnCancelar.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location = new System.Drawing.Point(176, 703);
            this.btnCancelar.Name = "btnCancelar";
            this.btnCancelar.Size = new System.Drawing.Size(150, 35);
            this.btnCancelar.TabIndex = 5;
            this.btnCancelar.Text = "CANCELAR";
            this.btnCancelar.UseVisualStyleBackColor = false;
            this.btnCancelar.Click += new System.EventHandler(this.BtnCancelar_Click);
            // 
            // colFormaDoacaoVisual
            // 
            this.colFormaDoacaoVisual.HeaderText = "Forma de Doação";
            this.colFormaDoacaoVisual.Name = "colFormaDoacaoVisual";
            this.colFormaDoacaoVisual.ReadOnly = true;
            // 
            // colTotalDoacaoVisual
            // 
            this.colTotalDoacaoVisual.HeaderText = "Valor Total";
            this.colTotalDoacaoVisual.Name = "colTotalDoacaoVisual";
            this.colTotalDoacaoVisual.ReadOnly = true;
            // 
            // FormFecharCaixa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.ClientSize = new System.Drawing.Size(850, 740);
            this.Controls.Add(this.btnCancelar);
            this.Controls.Add(this.btnFecharCaixa);
            this.Controls.Add(this.gbContagem);
            this.Controls.Add(this.gbFormas);
            this.Controls.Add(this.gbResumo);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormFecharCaixa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Fechamento de Caixa";
            this.Load += new System.EventHandler(this.FormFecharCaixa_Load);
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.gbResumo.ResumeLayout(false);
            this.gbResumo.PerformLayout();
            this.gbFormas.ResumeLayout(false);
            this.tabControlFormas.ResumeLayout(false);
            this.tabPagamento.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvFormasPagamento)).EndInit();
            this.tabDoacoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvDoacoes)).EndInit();
            this.gbContagem.ResumeLayout(false);
            this.gbContagem.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.GroupBox gbResumo;
        private System.Windows.Forms.Label lblTotalDoacoes;
        private System.Windows.Forms.Label lblValorTotalCortesias;
        private System.Windows.Forms.Label lblTotalCortesias;
        private System.Windows.Forms.Label lblTotalVendas;
        private System.Windows.Forms.Label lblTotalEsperado;
        private System.Windows.Forms.Label lblTotalSangria;
        private System.Windows.Forms.Label lblTotalEntradaTroco;
        private System.Windows.Forms.Label lblTotalTroco;
        private System.Windows.Forms.Label lblTotalDinheiro;
        private System.Windows.Forms.Label lblAbertura;
        private System.Windows.Forms.Label lblTituloResumo;
        private System.Windows.Forms.GroupBox gbFormas;
        private System.Windows.Forms.TabControl tabControlFormas;
        private System.Windows.Forms.TabPage tabPagamento;
        private System.Windows.Forms.DataGridView dgvFormasPagamento;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormaVisual;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalVisual;
        private System.Windows.Forms.TabPage tabDoacoes;
        private System.Windows.Forms.DataGridView dgvDoacoes;
        private System.Windows.Forms.GroupBox gbContagem;
        private System.Windows.Forms.TextBox txtObservacoes;
        private System.Windows.Forms.Label lblObservacoes;
        private System.Windows.Forms.Label lblDiferenca;
        private System.Windows.Forms.TextBox txtValorContado;
        private System.Windows.Forms.Label lblValorContadoLabel;
        private System.Windows.Forms.Button btnFecharCaixa;
        private System.Windows.Forms.Button btnCancelar;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFormaDoacaoVisual;
        private System.Windows.Forms.DataGridViewTextBoxColumn colTotalDoacaoVisual;
    }
}

