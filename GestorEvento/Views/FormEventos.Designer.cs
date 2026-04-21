namespace GestorEvento.Views
{
    partial class FormEventos
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
            this.btnFechar = new System.Windows.Forms.Button();
            this.btnMinimizar = new System.Windows.Forms.Button();
            this.lblTitulo = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabCadastro = new System.Windows.Forms.TabPage();
            this.lblNomeEvento = new System.Windows.Forms.Label();
            this.txtNomeEvento = new System.Windows.Forms.TextBox();
            this.lblData = new System.Windows.Forms.Label();
            this.mtbDataEvento = new System.Windows.Forms.MaskedTextBox();
            this.dtpDataEvento = new System.Windows.Forms.DateTimePicker();
            this.panelBotoesCadastro = new System.Windows.Forms.Panel();
            this.btnSalvar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.tabLista = new System.Windows.Forms.TabPage();
            this.panelBotoes = new System.Windows.Forms.Panel();
            this.btnDeletar = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.dgvEventos = new System.Windows.Forms.DataGridView();
            this.panelPesquisa = new System.Windows.Forms.Panel();
            this.btnLimparFiltroPesquisa = new System.Windows.Forms.Button();
            this.btnPesquisar = new System.Windows.Forms.Button();
            this.mtbDataPesquisa = new System.Windows.Forms.MaskedTextBox();
            this.dtpDataPesquisa = new System.Windows.Forms.DateTimePicker();
            this.txtPesquisar = new System.Windows.Forms.TextBox();
            this.tabVincular = new System.Windows.Forms.TabPage();
            this.panelVinculacao = new System.Windows.Forms.Panel();
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.dgvProdutosDisponiveis = new System.Windows.Forms.DataGridView();
            this.lblProdutosDisponiveis = new System.Windows.Forms.Label();
            this.dgvProdutosVinculados = new System.Windows.Forms.DataGridView();
            this.lblProdutosVinculados = new System.Windows.Forms.Label();
            this.panelBotoesVinculacao = new System.Windows.Forms.Panel();
            this.panelEventoSel = new System.Windows.Forms.Panel();
            this.lblEvento = new System.Windows.Forms.Label();
            this.ddlEvento = new System.Windows.Forms.ComboBox();
            this.panelTitulo.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabCadastro.SuspendLayout();
            this.panelBotoesCadastro.SuspendLayout();
            this.tabLista.SuspendLayout();
            this.panelBotoes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).BeginInit();
            this.panelPesquisa.SuspendLayout();
            this.tabVincular.SuspendLayout();
            this.panelVinculacao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosDisponiveis)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosVinculados)).BeginInit();
            this.panelEventoSel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelTitulo
            // 
            this.panelTitulo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.panelTitulo.Controls.Add(this.btnFechar);
            this.panelTitulo.Controls.Add(this.btnMinimizar);
            this.panelTitulo.Controls.Add(this.lblTitulo);
            this.panelTitulo.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTitulo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.panelTitulo.Location = new System.Drawing.Point(0, 0);
            this.panelTitulo.Name = "panelTitulo";
            this.panelTitulo.Size = new System.Drawing.Size(1200, 40);
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
            this.btnFechar.Location = new System.Drawing.Point(1110, 0);
            this.btnFechar.Name = "btnFechar";
            this.btnFechar.Size = new System.Drawing.Size(45, 40);
            this.btnFechar.TabIndex = 2;
            this.btnFechar.Text = "✕";
            this.btnFechar.UseVisualStyleBackColor = false;
            this.btnFechar.Click += new System.EventHandler(this.btnFechar_Click_1);
            // 
            // btnMinimizar
            // 
            this.btnMinimizar.BackColor = System.Drawing.Color.Transparent;
            this.btnMinimizar.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnMinimizar.FlatAppearance.BorderSize = 0;
            this.btnMinimizar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnMinimizar.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnMinimizar.ForeColor = System.Drawing.Color.White;
            this.btnMinimizar.Location = new System.Drawing.Point(1155, 0);
            this.btnMinimizar.Name = "btnMinimizar";
            this.btnMinimizar.Size = new System.Drawing.Size(45, 40);
            this.btnMinimizar.TabIndex = 1;
            this.btnMinimizar.Text = "−";
            this.btnMinimizar.UseVisualStyleBackColor = false;
            this.btnMinimizar.Click += new System.EventHandler(this.BtnMinimizar_Click);
            // 
            // lblTitulo
            // 
            this.lblTitulo.AutoSize = true;
            this.lblTitulo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitulo.ForeColor = System.Drawing.Color.White;
            this.lblTitulo.Location = new System.Drawing.Point(10, 10);
            this.lblTitulo.Name = "lblTitulo";
            this.lblTitulo.Size = new System.Drawing.Size(164, 21);
            this.lblTitulo.TabIndex = 0;
            this.lblTitulo.Text = "Cadastro de Eventos";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabCadastro);
            this.tabControl.Controls.Add(this.tabVincular);
            this.tabControl.Controls.Add(this.tabLista);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.tabControl.Location = new System.Drawing.Point(0, 40);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1200, 660);
            this.tabControl.TabIndex = 1;
            // 
            // tabCadastro
            // 
            this.tabCadastro.BackColor = System.Drawing.Color.White;
            this.tabCadastro.Controls.Add(this.lblNomeEvento);
            this.tabCadastro.Controls.Add(this.txtNomeEvento);
            this.tabCadastro.Controls.Add(this.lblData);
            this.tabCadastro.Controls.Add(this.mtbDataEvento);
            this.tabCadastro.Controls.Add(this.dtpDataEvento);
            this.tabCadastro.Controls.Add(this.panelBotoesCadastro);
            this.tabCadastro.Location = new System.Drawing.Point(4, 26);
            this.tabCadastro.Name = "tabCadastro";
            this.tabCadastro.Padding = new System.Windows.Forms.Padding(15);
            this.tabCadastro.Size = new System.Drawing.Size(1192, 630);
            this.tabCadastro.TabIndex = 0;
            this.tabCadastro.Text = "CADASTRO";
            // 
            // lblNomeEvento
            // 
            this.lblNomeEvento.AutoSize = true;
            this.lblNomeEvento.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNomeEvento.ForeColor = System.Drawing.Color.Black;
            this.lblNomeEvento.Location = new System.Drawing.Point(18, 45);
            this.lblNomeEvento.Name = "lblNomeEvento";
            this.lblNomeEvento.Size = new System.Drawing.Size(124, 20);
            this.lblNomeEvento.TabIndex = 1;
            this.lblNomeEvento.Text = "Nome do Evento:";
            // 
            // txtNomeEvento
            // 
            this.txtNomeEvento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNomeEvento.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtNomeEvento.Location = new System.Drawing.Point(198, 42);
            this.txtNomeEvento.Name = "txtNomeEvento";
            this.txtNomeEvento.Size = new System.Drawing.Size(350, 27);
            this.txtNomeEvento.TabIndex = 2;
            // 
            // lblData
            // 
            this.lblData.AutoSize = true;
            this.lblData.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblData.ForeColor = System.Drawing.Color.Black;
            this.lblData.Location = new System.Drawing.Point(18, 85);
            this.lblData.Name = "lblData";
            this.lblData.Size = new System.Drawing.Size(115, 20);
            this.lblData.TabIndex = 3;
            this.lblData.Text = "Data do Evento:";
            // 
            // mtbDataEvento
            // 
            this.mtbDataEvento.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtbDataEvento.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.mtbDataEvento.Location = new System.Drawing.Point(198, 82);
            this.mtbDataEvento.Mask = "00/00/0000";
            this.mtbDataEvento.Name = "mtbDataEvento";
            this.mtbDataEvento.Size = new System.Drawing.Size(150, 27);
            this.mtbDataEvento.TabIndex = 4;
            this.mtbDataEvento.ValidatingType = typeof(System.DateTime);
            // 
            // dtpDataEvento
            // 
            this.dtpDataEvento.CustomFormat = " ";
            this.dtpDataEvento.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.dtpDataEvento.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataEvento.Location = new System.Drawing.Point(317, 82);
            this.dtpDataEvento.Name = "dtpDataEvento";
            this.dtpDataEvento.Size = new System.Drawing.Size(47, 27);
            this.dtpDataEvento.TabIndex = 5;
            // 
            // panelBotoesCadastro
            // 
            this.panelBotoesCadastro.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBotoesCadastro.Controls.Add(this.btnSalvar);
            this.panelBotoesCadastro.Controls.Add(this.btnLimpar);
            this.panelBotoesCadastro.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoesCadastro.Location = new System.Drawing.Point(15, 565);
            this.panelBotoesCadastro.Name = "panelBotoesCadastro";
            this.panelBotoesCadastro.Padding = new System.Windows.Forms.Padding(10);
            this.panelBotoesCadastro.Size = new System.Drawing.Size(1162, 50);
            this.panelBotoesCadastro.TabIndex = 7;
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
            this.btnSalvar.TabIndex = 6;
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
            this.btnLimpar.TabIndex = 7;
            this.btnLimpar.Text = "LIMPAR";
            this.btnLimpar.UseVisualStyleBackColor = false;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // tabLista
            // 
            this.tabLista.BackColor = System.Drawing.Color.White;
            this.tabLista.Controls.Add(this.panelBotoes);
            this.tabLista.Controls.Add(this.dgvEventos);
            this.tabLista.Controls.Add(this.panelPesquisa);
            this.tabLista.Location = new System.Drawing.Point(4, 26);
            this.tabLista.Name = "tabLista";
            this.tabLista.Padding = new System.Windows.Forms.Padding(10);
            this.tabLista.Size = new System.Drawing.Size(1192, 630);
            this.tabLista.TabIndex = 1;
            this.tabLista.Text = "LISTA DE EVENTOS";
            // 
            // panelBotoes
            // 
            this.panelBotoes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBotoes.Controls.Add(this.btnDeletar);
            this.panelBotoes.Controls.Add(this.btnEditar);
            this.panelBotoes.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoes.Location = new System.Drawing.Point(10, 570);
            this.panelBotoes.Name = "panelBotoes";
            this.panelBotoes.Padding = new System.Windows.Forms.Padding(10);
            this.panelBotoes.Size = new System.Drawing.Size(1172, 50);
            this.panelBotoes.TabIndex = 2;
            // 
            // btnDeletar
            // 
            this.btnDeletar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(244)))), ((int)(((byte)(67)))), ((int)(((byte)(54)))));
            this.btnDeletar.FlatAppearance.BorderSize = 0;
            this.btnDeletar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDeletar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDeletar.ForeColor = System.Drawing.Color.White;
            this.btnDeletar.Location = new System.Drawing.Point(120, 11);
            this.btnDeletar.Name = "btnDeletar";
            this.btnDeletar.Size = new System.Drawing.Size(100, 30);
            this.btnDeletar.TabIndex = 4;
            this.btnDeletar.Text = "DELETAR";
            this.btnDeletar.UseVisualStyleBackColor = false;
            this.btnDeletar.Click += new System.EventHandler(this.btnDeletar_Click);
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
            this.btnEditar.TabIndex = 3;
            this.btnEditar.Text = "EDITAR";
            this.btnEditar.UseVisualStyleBackColor = false;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // dgvEventos
            // 
            this.dgvEventos.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvEventos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEventos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvEventos.Location = new System.Drawing.Point(10, 60);
            this.dgvEventos.Name = "dgvEventos";
            this.dgvEventos.ReadOnly = true;
            this.dgvEventos.RowTemplate.Height = 25;
            this.dgvEventos.Size = new System.Drawing.Size(1172, 560);
            this.dgvEventos.TabIndex = 1;
            // 
            // panelPesquisa
            // 
            this.panelPesquisa.BackColor = System.Drawing.Color.White;
            this.panelPesquisa.Controls.Add(this.btnLimparFiltroPesquisa);
            this.panelPesquisa.Controls.Add(this.btnPesquisar);
            this.panelPesquisa.Controls.Add(this.mtbDataPesquisa);
            this.panelPesquisa.Controls.Add(this.dtpDataPesquisa);
            this.panelPesquisa.Controls.Add(this.txtPesquisar);
            this.panelPesquisa.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelPesquisa.Location = new System.Drawing.Point(10, 10);
            this.panelPesquisa.Name = "panelPesquisa";
            this.panelPesquisa.Padding = new System.Windows.Forms.Padding(10);
            this.panelPesquisa.Size = new System.Drawing.Size(1172, 50);
            this.panelPesquisa.TabIndex = 0;
            // 
            // btnLimparFiltroPesquisa
            // 
            this.btnLimparFiltroPesquisa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(117)))), ((int)(((byte)(117)))), ((int)(((byte)(117)))));
            this.btnLimparFiltroPesquisa.FlatAppearance.BorderSize = 0;
            this.btnLimparFiltroPesquisa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLimparFiltroPesquisa.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLimparFiltroPesquisa.ForeColor = System.Drawing.Color.White;
            this.btnLimparFiltroPesquisa.Location = new System.Drawing.Point(524, 12);
            this.btnLimparFiltroPesquisa.Name = "btnLimparFiltroPesquisa";
            this.btnLimparFiltroPesquisa.Size = new System.Drawing.Size(100, 25);
            this.btnLimparFiltroPesquisa.TabIndex = 7;
            this.btnLimparFiltroPesquisa.Text = "LIMPAR";
            this.btnLimparFiltroPesquisa.UseVisualStyleBackColor = false;
            this.btnLimparFiltroPesquisa.Click += new System.EventHandler(this.btnLimparFiltroPesquisa_Click);
            // 
            // btnPesquisar
            // 
            this.btnPesquisar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(118)))), ((int)(((byte)(210)))));
            this.btnPesquisar.FlatAppearance.BorderSize = 0;
            this.btnPesquisar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPesquisar.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnPesquisar.ForeColor = System.Drawing.Color.White;
            this.btnPesquisar.Location = new System.Drawing.Point(418, 12);
            this.btnPesquisar.Name = "btnPesquisar";
            this.btnPesquisar.Size = new System.Drawing.Size(100, 25);
            this.btnPesquisar.TabIndex = 4;
            this.btnPesquisar.Text = "PESQUISAR";
            this.btnPesquisar.UseVisualStyleBackColor = false;
            this.btnPesquisar.Click += new System.EventHandler(this.btnPesquisar_Click);
            // 
            // mtbDataPesquisa
            // 
            this.mtbDataPesquisa.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.mtbDataPesquisa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.mtbDataPesquisa.Location = new System.Drawing.Point(296, 12);
            this.mtbDataPesquisa.Mask = "00/00/0000";
            this.mtbDataPesquisa.Name = "mtbDataPesquisa";
            this.mtbDataPesquisa.Size = new System.Drawing.Size(100, 25);
            this.mtbDataPesquisa.TabIndex = 2;
            this.mtbDataPesquisa.ValidatingType = typeof(System.DateTime);
            // 
            // dtpDataPesquisa
            // 
            this.dtpDataPesquisa.CustomFormat = " ";
            this.dtpDataPesquisa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpDataPesquisa.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpDataPesquisa.Location = new System.Drawing.Point(365, 12);
            this.dtpDataPesquisa.Name = "dtpDataPesquisa";
            this.dtpDataPesquisa.Size = new System.Drawing.Size(47, 25);
            this.dtpDataPesquisa.TabIndex = 3;
            // 
            // txtPesquisar
            // 
            this.txtPesquisar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtPesquisar.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPesquisar.Location = new System.Drawing.Point(10, 12);
            this.txtPesquisar.Name = "txtPesquisar";
            this.txtPesquisar.Size = new System.Drawing.Size(280, 25);
            this.txtPesquisar.TabIndex = 1;
            // 
            // tabVincular
            // 
            this.tabVincular.BackColor = System.Drawing.Color.White;
            this.tabVincular.Controls.Add(this.panelVinculacao);
            this.tabVincular.Controls.Add(this.panelEventoSel);
            this.tabVincular.Location = new System.Drawing.Point(4, 26);
            this.tabVincular.Name = "tabVincular";
            this.tabVincular.Padding = new System.Windows.Forms.Padding(10);
            this.tabVincular.Size = new System.Drawing.Size(1192, 630);
            this.tabVincular.TabIndex = 2;
            this.tabVincular.Text = "VINCULAR PRODUTOS";
            // 
            // panelVinculacao
            // 
            this.panelVinculacao.BackColor = System.Drawing.Color.White;
            this.panelVinculacao.Controls.Add(this.splitContainer);
            this.panelVinculacao.Controls.Add(this.panelBotoesVinculacao);
            this.panelVinculacao.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelVinculacao.Location = new System.Drawing.Point(10, 60);
            this.panelVinculacao.Name = "panelVinculacao";
            this.panelVinculacao.Padding = new System.Windows.Forms.Padding(10);
            this.panelVinculacao.Size = new System.Drawing.Size(1172, 560);
            this.panelVinculacao.TabIndex = 1;
            // 
            // splitContainer
            // 
            this.splitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer.Location = new System.Drawing.Point(10, 10);
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            this.splitContainer.Panel1.Controls.Add(this.dgvProdutosDisponiveis);
            this.splitContainer.Panel1.Controls.Add(this.lblProdutosDisponiveis);
            // 
            // splitContainer.Panel2
            // 
            this.splitContainer.Panel2.Controls.Add(this.dgvProdutosVinculados);
            this.splitContainer.Panel2.Controls.Add(this.lblProdutosVinculados);
            this.splitContainer.Size = new System.Drawing.Size(1152, 495);
            this.splitContainer.SplitterDistance = 570;
            this.splitContainer.TabIndex = 0;
            // 
            // dgvProdutosDisponiveis
            // 
            this.dgvProdutosDisponiveis.AllowUserToAddRows = false;
            this.dgvProdutosDisponiveis.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProdutosDisponiveis.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProdutosDisponiveis.Location = new System.Drawing.Point(0, 25);
            this.dgvProdutosDisponiveis.Name = "dgvProdutosDisponiveis";
            this.dgvProdutosDisponiveis.ReadOnly = true;
            this.dgvProdutosDisponiveis.RowTemplate.Height = 25;
            this.dgvProdutosDisponiveis.Size = new System.Drawing.Size(570, 470);
            this.dgvProdutosDisponiveis.TabIndex = 1;
            // 
            // lblProdutosDisponiveis
            // 
            this.lblProdutosDisponiveis.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProdutosDisponiveis.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProdutosDisponiveis.Location = new System.Drawing.Point(0, 0);
            this.lblProdutosDisponiveis.Name = "lblProdutosDisponiveis";
            this.lblProdutosDisponiveis.Padding = new System.Windows.Forms.Padding(10, 3, 0, 0);
            this.lblProdutosDisponiveis.Size = new System.Drawing.Size(570, 25);
            this.lblProdutosDisponiveis.TabIndex = 2;
            this.lblProdutosDisponiveis.Text = "Produtos Disponíveis (0)";
            // 
            // dgvProdutosVinculados
            // 
            this.dgvProdutosVinculados.AllowUserToAddRows = false;
            this.dgvProdutosVinculados.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvProdutosVinculados.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvProdutosVinculados.Location = new System.Drawing.Point(0, 25);
            this.dgvProdutosVinculados.Name = "dgvProdutosVinculados";
            this.dgvProdutosVinculados.ReadOnly = true;
            this.dgvProdutosVinculados.RowTemplate.Height = 25;
            this.dgvProdutosVinculados.Size = new System.Drawing.Size(578, 470);
            this.dgvProdutosVinculados.TabIndex = 1;
            // 
            // lblProdutosVinculados
            // 
            this.lblProdutosVinculados.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblProdutosVinculados.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblProdutosVinculados.Location = new System.Drawing.Point(0, 0);
            this.lblProdutosVinculados.Name = "lblProdutosVinculados";
            this.lblProdutosVinculados.Padding = new System.Windows.Forms.Padding(10, 3, 0, 0);
            this.lblProdutosVinculados.Size = new System.Drawing.Size(578, 25);
            this.lblProdutosVinculados.TabIndex = 3;
            this.lblProdutosVinculados.Text = "Produtos Vinculados (0)";
            // 
            // panelBotoesVinculacao
            // 
            this.panelBotoesVinculacao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.panelBotoesVinculacao.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelBotoesVinculacao.Location = new System.Drawing.Point(10, 505);
            this.panelBotoesVinculacao.Name = "panelBotoesVinculacao";
            this.panelBotoesVinculacao.Size = new System.Drawing.Size(1152, 45);
            this.panelBotoesVinculacao.TabIndex = 1;
            // 
            // panelEventoSel
            // 
            this.panelEventoSel.BackColor = System.Drawing.Color.White;
            this.panelEventoSel.Controls.Add(this.lblEvento);
            this.panelEventoSel.Controls.Add(this.ddlEvento);
            this.panelEventoSel.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelEventoSel.Location = new System.Drawing.Point(10, 10);
            this.panelEventoSel.Name = "panelEventoSel";
            this.panelEventoSel.Padding = new System.Windows.Forms.Padding(10);
            this.panelEventoSel.Size = new System.Drawing.Size(1172, 50);
            this.panelEventoSel.TabIndex = 0;
            // 
            // lblEvento
            // 
            this.lblEvento.AutoSize = true;
            this.lblEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblEvento.Location = new System.Drawing.Point(10, 15);
            this.lblEvento.Name = "lblEvento";
            this.lblEvento.Size = new System.Drawing.Size(114, 19);
            this.lblEvento.TabIndex = 0;
            this.lblEvento.Text = "Selecione evento:";
            // 
            // ddlEvento
            // 
            this.ddlEvento.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlEvento.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.ddlEvento.FormattingEnabled = true;
            this.ddlEvento.Location = new System.Drawing.Point(124, 12);
            this.ddlEvento.Name = "ddlEvento";
            this.ddlEvento.Size = new System.Drawing.Size(350, 25);
            this.ddlEvento.TabIndex = 1;
            this.ddlEvento.SelectedIndexChanged += new System.EventHandler(this.ddlEvento_SelectedIndexChanged);
            // 
            // FormEventos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.panelTitulo);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FormEventos";
            this.ShowIcon = false;
            this.Text = "Cadastro de Eventos";
            this.panelTitulo.ResumeLayout(false);
            this.panelTitulo.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabCadastro.ResumeLayout(false);
            this.tabCadastro.PerformLayout();
            this.panelBotoesCadastro.ResumeLayout(false);
            this.tabLista.ResumeLayout(false);
            this.panelBotoes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvEventos)).EndInit();
            this.panelPesquisa.ResumeLayout(false);
            this.panelPesquisa.PerformLayout();
            this.tabVincular.ResumeLayout(false);
            this.panelVinculacao.ResumeLayout(false);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosDisponiveis)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvProdutosVinculados)).EndInit();
            this.panelEventoSel.ResumeLayout(false);
            this.panelEventoSel.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel panelTitulo;
        private System.Windows.Forms.Button btnFechar;
        private System.Windows.Forms.Button btnMinimizar;
        private System.Windows.Forms.Label lblTitulo;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabCadastro;
        private System.Windows.Forms.TabPage tabLista;
        private System.Windows.Forms.TabPage tabVincular;
        private System.Windows.Forms.Label lblNomeEvento;
        private System.Windows.Forms.TextBox txtNomeEvento;
        private System.Windows.Forms.Label lblData;
        private System.Windows.Forms.DateTimePicker dtpDataEvento;
        private System.Windows.Forms.MaskedTextBox mtbDataEvento;
        private System.Windows.Forms.Button btnSalvar;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Panel panelPesquisa;
        private System.Windows.Forms.TextBox txtPesquisar;
        private System.Windows.Forms.MaskedTextBox mtbDataPesquisa;
        private System.Windows.Forms.DateTimePicker dtpDataPesquisa;
        private System.Windows.Forms.Button btnPesquisar;
        private System.Windows.Forms.DataGridView dgvEventos;
        private System.Windows.Forms.Panel panelBotoes;
        private System.Windows.Forms.Panel panelBotoesCadastro;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnDeletar;
        private System.Windows.Forms.Button btnLimparFiltroPesquisa;
        private System.Windows.Forms.Panel panelVinculacao;
        private System.Windows.Forms.Panel panelBotoesVinculacao;
        private System.Windows.Forms.SplitContainer splitContainer;
        private System.Windows.Forms.DataGridView dgvProdutosDisponiveis;
        private System.Windows.Forms.Label lblProdutosVinculados;
        private System.Windows.Forms.DataGridView dgvProdutosVinculados;
        private System.Windows.Forms.Panel panelEventoSel;
        private System.Windows.Forms.Label lblEvento;
        private System.Windows.Forms.ComboBox ddlEvento;
        private System.Windows.Forms.Label lblProdutosDisponiveis;
    }
}

