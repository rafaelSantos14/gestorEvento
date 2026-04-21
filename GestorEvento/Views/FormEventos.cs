using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestorEvento.Utilities;
using GestorEvento.Services;
using GestorEvento.Models;

namespace GestorEvento.Views
{
    public partial class FormEventos : Form
    {
        private EventoService _service;
        private ProdutoService _produtoService;
        private ProdutoEventoService _produtoEventoService;
        private bool _dataPesquisaSelecionada = false;
        
        // Vincular Produtos
        private List<int> _produtosVinculados = new List<int>();
        private int _eventoIdSelecionado = 0;
        private DataGridViewRow _draggingRow = null;
        private bool _isDraggingFromLeft = false;

        public FormEventos()
        {
            InitializeComponent();
            
            // Inicializar serviços
            _service = new EventoService();
            _produtoService = new ProdutoService();
            _produtoEventoService = new ProdutoEventoService();
            
            // Aplicar estilos padrão
            EstiloManager.AplicarEstiloSalvar(btnSalvar);           
            EstiloManager.AplicarEstiloDeletar(btnDeletar);
            EstiloManager.AplicarEstiloInfo(btnPesquisar);

            // Configurar DateTimePicker para busca (inicialmente vazio)
            dtpDataPesquisa.Format = DateTimePickerFormat.Custom;
            dtpDataPesquisa.CustomFormat = " ";
            dtpDataPesquisa.Value = DateTime.Now;
            
            // Configurar MaskedTextBox com máscara de data
            mtbDataPesquisa.Mask = "00/00/0000";
            mtbDataPesquisa.ValidatingType = typeof(DateTime);
            
            // Configurar DateTimePicker para cadastro (inicialmente vazio)
            dtpDataEvento.Format = DateTimePickerFormat.Custom;
            dtpDataEvento.CustomFormat = " ";
            dtpDataEvento.Value = DateTime.Now;
            
            // Configurar MaskedTextBox para cadastro
            mtbDataEvento.Mask = "00/00/0000";
            mtbDataEvento.ValidatingType = typeof(DateTime);
            
            // Eventos - Busca
            dtpDataPesquisa.ValueChanged += DtpDataPesquisa_ValueChanged;
            mtbDataPesquisa.TextChanged += MtbDataPesquisa_TextChanged;
            
            // Eventos - Cadastro
            dtpDataEvento.ValueChanged += DtpDataEvento_ValueChanged;
            mtbDataEvento.TextChanged += MtbDataEvento_TextChanged;

            // Configurar DataGridView
            ConfigurarDataGridView();
            ConfigurarDataGridViewDisponivel();
            ConfigurarDataGridViewVinculado();
            
            // Carregar eventos para a aba VINCULAR
            CarregarEventosDropdown();
        }

        private void DtpDataPesquisa_ValueChanged(object sender, EventArgs e)
        {
            // Quando seleciona uma data no calendário, atualiza o MaskedTextBox
            dtpDataPesquisa.Format = DateTimePickerFormat.Short;
            mtbDataPesquisa.Text = dtpDataPesquisa.Value.ToString("dd/MM/yyyy");
            _dataPesquisaSelecionada = true;
        }

        private void MtbDataPesquisa_TextChanged(object sender, EventArgs e)
        {
            // Quando digita no MaskedTextBox
            string texto = mtbDataPesquisa.Text;

            if (string.IsNullOrWhiteSpace(texto.Trim('/')))
            {
                // Se apagou, deixa o DateTimePicker vazio
                dtpDataPesquisa.Format = DateTimePickerFormat.Custom;
                dtpDataPesquisa.CustomFormat = " ";
                _dataPesquisaSelecionada = false;
            }
            else if (texto.Length == 10 && texto[2] == '/' && texto[5] == '/') // Máscara completa: DD/MM/YYYY
            {
                // Apenas tenta converter quando a máscara está completa
                try
                {
                    if (DateTime.TryParse(texto, out DateTime dt) && dt >= dtpDataPesquisa.MinDate && dt <= dtpDataPesquisa.MaxDate)
                    {
                        dtpDataPesquisa.Value = dt;
                        dtpDataPesquisa.Format = DateTimePickerFormat.Short;
                        _dataPesquisaSelecionada = true;
                    }
                }
                catch
                {
                    // Ignora erros de conversão de datas inválidas
                }
            }
        }

        private void DtpDataEvento_ValueChanged(object sender, EventArgs e)
        {
            // Quando seleciona uma data no calendário, atualiza o MaskedTextBox
            dtpDataEvento.Format = DateTimePickerFormat.Short;
            mtbDataEvento.Text = dtpDataEvento.Value.ToString("dd/MM/yyyy");
        }

        private void MtbDataEvento_TextChanged(object sender, EventArgs e)
        {
            // Quando digita no MaskedTextBox
            string texto = mtbDataEvento.Text;

            if (string.IsNullOrWhiteSpace(texto.Trim('/')))
            {
                // Se apagou, deixa o DateTimePicker vazio
                dtpDataEvento.Format = DateTimePickerFormat.Custom;
                dtpDataEvento.CustomFormat = " ";
            }
            else if (texto.Length == 10 && texto[2] == '/' && texto[5] == '/') // Máscara completa: DD/MM/YYYY
            {
                // Apenas tenta converter quando a máscara está completa
                try
                {
                    if (DateTime.TryParse(texto, out DateTime dt) && dt >= dtpDataEvento.MinDate && dt <= dtpDataEvento.MaxDate)
                    {
                        dtpDataEvento.Value = dt;
                        dtpDataEvento.Format = DateTimePickerFormat.Short;
                    }
                }
                catch
                {
                    // Ignora erros de conversão de datas inválidas
                }
            }
        }

        private void ConfigurarDataGridView()
        {
            // Limpar colunas existentes
            dgvEventos.Columns.Clear();

            // Criar coluna ID
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            };
            dgvEventos.Columns.Add(colId);

            // Criar coluna Nome
            var colNome = new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Evento",
                Width = 200,
                ReadOnly = true
            };
            dgvEventos.Columns.Add(colNome);

            // Criar coluna Data
            var colData = new DataGridViewTextBoxColumn
            {
                Name = "Data",
                HeaderText = "Data",
                Width = 100,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill,
                ReadOnly = true
            };
            dgvEventos.Columns.Add(colData);

            // Configurar cores do texto
            dgvEventos.DefaultCellStyle.ForeColor = Color.Black;
            dgvEventos.DefaultCellStyle.BackColor = Color.White;
            dgvEventos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEventos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210); // Azul Material Design
            dgvEventos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Cinza claro

            // Handler para duplo-clique (editar)
            dgvEventos.CellDoubleClick += DataGridViewEventos_CellDoubleClick;

            // Handler para seleção de linha
            dgvEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CarregarEventosDoDb()
        {
            dgvEventos.Rows.Clear();
            
            try
            {
                var eventos = _service.GetAllEventos();
                
                if (eventos.Count == 0)
                {
                    // Se não houver eventos, mostrar mensagem informativa
                    return;
                }

                foreach (var evento in eventos)
                {
                    dgvEventos.Rows.Add(evento.Id, evento.Nome, evento.DataEvento.ToString("dd/MM/yyyy"));
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar eventos: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
            }
        }

        private void DataGridViewEventos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar se há linhas no grid
            if (e.RowIndex < 0 || dgvEventos.Rows.Count == 0) 
                return;

            // Validar se as células têm valores
            if (dgvEventos.Rows[e.RowIndex].Cells["ID"].Value == null || 
                dgvEventos.Rows[e.RowIndex].Cells["Nome"].Value == null)
            {
                return;
            }

            int eventoId = Convert.ToInt32(dgvEventos.Rows[e.RowIndex].Cells["ID"].Value);

            // Abrir FormEditarEvento como dialog modal
            var formEditar = new FormEditarEvento(eventoId);
            if (formEditar.ShowDialog(this) == DialogResult.OK)
            {
                // Se salvou com sucesso, recarregar dados do grid
                CarregarEventosDoDb();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validar campo Nome
            if (string.IsNullOrWhiteSpace(txtNomeEvento.Text))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, preencha o nome do evento",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            // Validar data
            DateTime? dataEvento = null;
            if (!string.IsNullOrWhiteSpace(mtbDataEvento.Text.Trim('/')))
            {
                if (DateTime.TryParse(mtbDataEvento.Text, out DateTime dt))
                {
                    dataEvento = dt;
                }
            }

            if (!dataEvento.HasValue)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, selecione uma data válida",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            // Criar novo evento
            var evento = new Evento
            {
                Nome = txtNomeEvento.Text.Trim(),
                DataEvento = dataEvento.Value
            };

            // Tentar salvar no banco
            if (_service.CreateEvento(evento))
            {
                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    "Evento salvo com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();
                
                // Limpar campos e recarregar grid
                txtNomeEvento.Clear();
                dtpDataEvento.Value = DateTime.Now;
                CarregarEventosDoDb();

                CarregarEventosDropdown();
            }           
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNomeEvento.Clear();
            mtbDataEvento.Clear();
            dtpDataEvento.Format = DateTimePickerFormat.Custom;
            dtpDataEvento.CustomFormat = " ";
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Selecione um evento na lista para deletar",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            string nomeEvento = dgvEventos.SelectedRows[0].Cells["Nome"].Value.ToString();
            DialogoCustomizado confirmacao = new DialogoCustomizado(
                "Confirmação",
                $"Deseja realmente deletar o evento '{nomeEvento}'?",
                TipoDialogo.Aviso,
                TipoButton.SimNao
            );
            
            if (confirmacao.ShowDialog() == DialogResult.Yes)
            {
                // Obter ID do evento selecionado
                if (dgvEventos.SelectedRows[0].Cells["ID"].Value == null)
                    return;

                int eventoId = Convert.ToInt32(dgvEventos.SelectedRows[0].Cells["ID"].Value);
                int rowIndex = dgvEventos.SelectedRows[0].Index;
                
                // Tentar deletar do banco
                if (_service.DeleteEvento(eventoId))
                {
                    // Remover linha do grid
                    dgvEventos.Rows.RemoveAt(rowIndex);

                    DialogoCustomizado sucesso = new DialogoCustomizado(
                        "Sucesso",
                        "Evento deletado com sucesso!",
                        TipoDialogo.Sucesso,
                        TipoButton.Ok
                    );
                    sucesso.ShowDialog();
                }
                else
                {
                    DialogoCustomizado erro = new DialogoCustomizado(
                        "Erro",
                        "Erro ao deletar evento. Tente novamente.",
                        TipoDialogo.Erro,
                        TipoButton.Ok
                    );
                    erro.ShowDialog();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Informação",
                    "Selecione um evento na lista para editar",
                    TipoDialogo.Informacao,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            // Validar se as células têm valores
            if (dgvEventos.SelectedRows[0].Cells["ID"].Value == null || 
                dgvEventos.SelectedRows[0].Cells["Nome"].Value == null)
            {
                return;
            }

            // Abrir FormEditarEvento como dialog modal
            int eventoId = Convert.ToInt32(dgvEventos.SelectedRows[0].Cells["ID"].Value);
            var formEditar = new FormEditarEvento(eventoId);
            if (formEditar.ShowDialog(this) == DialogResult.OK)
            {
                // Se salvou com sucesso, recarregar dados do grid
                CarregarEventosDoDb();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string nome = txtPesquisar.Text.Trim();
            
            // Extrair data do MaskedTextBox
            DateTime? data = null;
            if (_dataPesquisaSelecionada && DateTime.TryParse(mtbDataPesquisa.Text, out DateTime dt))
            {
                data = dt;
            }
            
            dgvEventos.Rows.Clear();

            try
            {
                var eventos = _service.SearchEventosByNameAndDate(nome, data);
                
                if (eventos.Count == 0)
                {
                    string filtroDesc = "";
                    if (!string.IsNullOrWhiteSpace(nome))
                        filtroDesc += $"nome: '{nome}'";
                    if (data.HasValue)
                    {
                        if (!string.IsNullOrWhiteSpace(filtroDesc))
                            filtroDesc += " e ";
                        filtroDesc += $"data: '{data.Value.ToString("dd/MM/yyyy")}'";
                    }

                    DialogoCustomizado info = new DialogoCustomizado(
                        "Informação",
                        $"Nenhum evento encontrado com os filtros: {filtroDesc}",
                        TipoDialogo.Informacao,
                        TipoButton.Ok
                    );
                    info.ShowDialog();
                    return;
                }

                foreach (var evento in eventos)
                {
                    dgvEventos.Rows.Add(evento.Id, evento.Nome, evento.DataEvento.ToString("dd/MM/yyyy"));
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao buscar eventos: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }

        private void btnLimparFiltroPesquisa_Click(object sender, EventArgs e)
        {
            // Limpar todos os filtros de pesquisa
            txtPesquisar.Clear();
            mtbDataPesquisa.Clear();
            dtpDataPesquisa.Format = DateTimePickerFormat.Custom;
            dtpDataPesquisa.CustomFormat = " ";
            _dataPesquisaSelecionada = false;
        }

        // Eventos da barra de título customizada
        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PanelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            // Não permite arrasto em forms MDI filhos
        }

        private void PanelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            // Não permite arrasto em forms MDI filhos
        }

        private void PanelTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            // Não permite arrasco em forms MDI filhos
        }

        // ==================== VINCULAR PRODUTOS ====================

        private void ConfigurarDataGridViewDisponivel()
        {
            dgvProdutosDisponiveis.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID", Width = 50, Visible = false };
            dgvProdutosDisponiveis.Columns.Add(colId);

            var colNome = new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Produto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill, ReadOnly = true };
            dgvProdutosDisponiveis.Columns.Add(colNome);

            var colAcao = new DataGridViewButtonColumn 
            { 
                Name = "Acao", 
                HeaderText = "", 
                Text = "➕", 
                UseColumnTextForButtonValue = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvProdutosDisponiveis.Columns.Add(colAcao);

            dgvProdutosDisponiveis.DefaultCellStyle.ForeColor = Color.Black;
            dgvProdutosDisponiveis.DefaultCellStyle.BackColor = Color.White;
            dgvProdutosDisponiveis.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvProdutosDisponiveis.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProdutosDisponiveis.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProdutosDisponiveis.AllowUserToAddRows = false;

            dgvProdutosDisponiveis.CellContentClick += DgvProdutosDisponiveis_CellContentClick;
            dgvProdutosDisponiveis.CellFormatting += DgvProdutosDisponiveis_CellFormatting;
        }

        private void ConfigurarDataGridViewVinculado()
        {
            dgvProdutosVinculados.Columns.Clear();

            var colId = new DataGridViewTextBoxColumn { Name = "ID", HeaderText = "ID", Width = 50, Visible = false };
            dgvProdutosVinculados.Columns.Add(colId);

            var colNome = new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Produto", Width = 150, ReadOnly = true };
            dgvProdutosVinculados.Columns.Add(colNome);

            var colPreco = new DataGridViewTextBoxColumn { Name = "Preco", HeaderText = "Preço", Width = 100, ReadOnly = true };
            dgvProdutosVinculados.Columns.Add(colPreco);

            var colQuantidade = new DataGridViewTextBoxColumn { Name = "Quantidade", HeaderText = "Qtde", Width = 80, ReadOnly = true };
            dgvProdutosVinculados.Columns.Add(colQuantidade);

            var colEditar = new DataGridViewButtonColumn 
            { 
                Name = "Editar", 
                HeaderText = "", 
                Text = "✏️", 
                UseColumnTextForButtonValue = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvProdutosVinculados.Columns.Add(colEditar);

            var colAcao = new DataGridViewButtonColumn 
            { 
                Name = "Acao", 
                HeaderText = "", 
                Text = "❌", 
                UseColumnTextForButtonValue = true,
                Width = 50,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.None
            };
            dgvProdutosVinculados.Columns.Add(colAcao);

            dgvProdutosVinculados.DefaultCellStyle.ForeColor = Color.Black;
            dgvProdutosVinculados.DefaultCellStyle.BackColor = Color.White;
            dgvProdutosVinculados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(56, 142, 60);
            dgvProdutosVinculados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProdutosVinculados.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProdutosVinculados.AllowUserToAddRows = false;

            dgvProdutosVinculados.CellContentClick += DgvProdutosVinculados_CellContentClick;
            dgvProdutosVinculados.CellFormatting += DgvProdutosVinculados_CellFormatting;
        }

        private void CarregarProdutosParaVinculacao()
        {
            dgvProdutosDisponiveis.Rows.Clear();
            dgvProdutosVinculados.Rows.Clear();
            _produtosVinculados.Clear();

            try
            {
                // Carregar vinculações existentes do banco de dados
                List<ProdutoEvento> produtosVinculados = _produtoEventoService.GetProdutosVinculados(_eventoIdSelecionado);
                var todosProdutos = _produtoService.GetAllProducts();

                // Adicionar ao grid de vinculados e manter referência
                foreach (var produtoVinculado in produtosVinculados)
                {
                    dgvProdutosVinculados.Rows.Add(
                        produtoVinculado.IdProduto,
                        todosProdutos.First(p => p.Id == produtoVinculado.IdProduto).Nome,
                        produtoVinculado.Preco.ToString("F2"),
                        produtoVinculado.Quantidade
                    );
                    _produtosVinculados.Add(produtoVinculado.IdProduto);
                }

                // Adicionar ao grid de disponíveis (produtos não vinculados)
                foreach (var produto in todosProdutos)
                {
                    if (!produtosVinculados.Any(pv => pv.IdProduto == produto.Id))
                    {
                        dgvProdutosDisponiveis.Rows.Add(produto.Id, produto.Nome);
                    }
                }

                AtualizarContadores();
            }
            catch (Exception ex)
            {
                MessageBox.Show(this, $"Erro ao carregar: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarContadores()
        {
            lblProdutosDisponiveis.Text = $"Produtos Disponíveis ({dgvProdutosDisponiveis.Rows.Count})";
            lblProdutosVinculados.Text = $"Produtos Vinculados ({dgvProdutosVinculados.Rows.Count})";
        }

        // ==================== DRAG & DROP ====================



        private void DgvProdutosDisponiveis_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProdutosDisponiveis.Columns["Acao"].Index && e.RowIndex >= 0)
            {
                var row = dgvProdutosDisponiveis.Rows[e.RowIndex];
                int produtoId = (int)row.Cells["ID"].Value;
                string nomeProduto = row.Cells["Nome"].Value.ToString();

                // Abrir modal para preencher preço e quantidade
                FormVincularProdutoEvento formModal = new FormVincularProdutoEvento(nomeProduto);
                if (formModal.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        // Vincular no banco de dados
                        _produtoEventoService.VincularProduto(produtoId, _eventoIdSelecionado, formModal.PrecoDigitado, formModal.QuantidadeDigitada);

                        // Remover de disponíveis e adicionar a vinculados
                        dgvProdutosDisponiveis.Rows.Remove(row);
                        dgvProdutosVinculados.Rows.Add(
                            produtoId,
                            nomeProduto,
                            formModal.PrecoDigitado.ToString("F2"),
                            formModal.QuantidadeDigitada
                        );
                        _produtosVinculados.Add(produtoId);

                        AtualizarContadores();

                        DialogoCustomizado dialogo = new DialogoCustomizado("Sucesso", $"Produto {nomeProduto} vinculado com sucesso!", TipoDialogo.Sucesso, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        DialogoCustomizado dialogo = new DialogoCustomizado("Erro", $"Erro ao vincular produto: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                }
            }
        }

        private void DgvProdutosDisponiveis_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvProdutosDisponiveis.Columns[e.ColumnIndex].Name == "Acao")
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.FromArgb(25, 118, 210);  // Azul
                e.CellStyle.SelectionBackColor = Color.White;
                e.CellStyle.SelectionForeColor = Color.FromArgb(25, 118, 210);
            }
        }

        private void DgvProdutosVinculados_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dgvProdutosVinculados.Columns["Editar"].Index && e.RowIndex >= 0)
            {
                var row = dgvProdutosVinculados.Rows[e.RowIndex];
                int produtoId = (int)row.Cells["ID"].Value;
                string nomeProduto = row.Cells["Nome"].Value.ToString();
                
                // Extrair valores atuais
                decimal precoAtual = decimal.Parse(row.Cells["Preco"].Value.ToString());
                int quantidadeAtual = int.Parse(row.Cells["Quantidade"].Value.ToString());

                // Abrir modal em modo de edição com valores carregados
                FormVincularProdutoEvento formModal = new FormVincularProdutoEvento(nomeProduto, precoAtual, quantidadeAtual);
                if (formModal.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        // Atualizar no banco de dados
                        _produtoEventoService.VincularProduto(produtoId, _eventoIdSelecionado, formModal.PrecoDigitado, formModal.QuantidadeDigitada);

                        // Atualizar a linha na grid com os novos valores
                        row.Cells["Preco"].Value = formModal.PrecoDigitado.ToString("F2");
                        row.Cells["Quantidade"].Value = formModal.QuantidadeDigitada;

                        DialogoCustomizado dialogo = new DialogoCustomizado("Sucesso", $"Produto {nomeProduto} atualizado com sucesso!", TipoDialogo.Sucesso, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        DialogoCustomizado dialogo = new DialogoCustomizado("Erro", $"Erro ao atualizar produto: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                }
            }
            else if (e.ColumnIndex == dgvProdutosVinculados.Columns["Acao"].Index && e.RowIndex >= 0)
            {
                var row = dgvProdutosVinculados.Rows[e.RowIndex];
                int produtoId = (int)row.Cells["ID"].Value;
                string nomeProduto = row.Cells["Nome"].Value.ToString();

                // Dialog de confirmação para remover
                DialogoCustomizado confirmacao = new DialogoCustomizado(
                    "Confirmar Remoção",
                    $"Remover o produto '{nomeProduto}' do evento?",
                    TipoDialogo.Aviso,
                    TipoButton.SimNao
                );

                if (confirmacao.ShowDialog() == DialogResult.Yes)
                {
                    try
                    {
                        // Remover do banco de dados
                        _produtoEventoService.RemoverProdutoDoEvento(produtoId, _eventoIdSelecionado);

                        // Remover de vinculados e adicionar a disponíveis
                        dgvProdutosVinculados.Rows.Remove(row);
                        dgvProdutosDisponiveis.Rows.Add(produtoId, nomeProduto);
                        _produtosVinculados.Remove(produtoId);

                        AtualizarContadores();

                        DialogoCustomizado dialogo = new DialogoCustomizado("Sucesso", $"Produto {nomeProduto} removido do evento com sucesso!", TipoDialogo.Sucesso, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                    catch (Exception ex)
                    {
                        DialogoCustomizado dialogo = new DialogoCustomizado("Erro", $"Erro ao remover produto: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                        dialogo.ShowDialog();
                    }
                }
            }
        }

        private void DgvProdutosVinculados_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.ColumnIndex >= 0 && dgvProdutosVinculados.Columns[e.ColumnIndex].Name == "Acao")
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.FromArgb(56, 142, 60);   // Verde
                e.CellStyle.SelectionBackColor = Color.White;
                e.CellStyle.SelectionForeColor = Color.FromArgb(56, 142, 60);
            }
            else if (e.ColumnIndex >= 0 && dgvProdutosVinculados.Columns[e.ColumnIndex].Name == "Editar")
            {
                e.CellStyle.BackColor = Color.White;
                e.CellStyle.ForeColor = Color.FromArgb(25, 118, 210);   // Azul
                e.CellStyle.SelectionBackColor = Color.White;
                e.CellStyle.SelectionForeColor = Color.FromArgb(25, 118, 210);
            }
        }

        private void CarregarEventosDropdown()
        {
            ddlEvento.Items.Clear();
            ddlEvento.DisplayMember = "Item2";
            ddlEvento.ValueMember = "Item1";

            try
            {
                ddlEvento.Items.Add(new Tuple<int, string>(0, "Selecione um evento"));

                var eventos = _service.GetAllEventos();
                foreach (var evento in eventos)
                {
                    string displayText = $"{evento.Nome} - {evento.DataEvento.ToString("dd/MM/yyyy")}";
                    ddlEvento.Items.Add(new Tuple<int, string>(evento.Id, displayText));
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado("Erro", $"Erro ao carregar eventos: {ex.Message}", TipoDialogo.Erro, TipoButton.Ok);
                dialogo.ShowDialog();
            }
        }

        private void ddlEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlEvento.SelectedIndex <= 0)
            {
                dgvProdutosDisponiveis.Rows.Clear();
                dgvProdutosVinculados.Rows.Clear();
                lblProdutosDisponiveis.Text = "Produtos Disponíveis (0)";
                lblProdutosVinculados.Text = "Produtos Vinculados (0)";
                _eventoIdSelecionado = 0;
                _produtosVinculados.Clear();
                return;
            }

            if (ddlEvento.SelectedItem is Tuple<int, string> tuple)
            {
                _eventoIdSelecionado = tuple.Item1;
                CarregarProdutosParaVinculacao();
            }
        }

        private void btnCarregar_Click(object sender, EventArgs e)
        {
            if (_eventoIdSelecionado > 0)
            {
                CarregarProdutosParaVinculacao();
            }
            else
            {
                DialogoCustomizado dialogo = new DialogoCustomizado("Aviso", "Selecione um evento primeiro", TipoDialogo.Aviso, TipoButton.Ok);
                dialogo.ShowDialog();
            }
        }

        private void btnSalvarVinculacao_Click(object sender, EventArgs e)
        {
            // Agora as vinculações são salvas imediatamente ao clicar duplo
            DialogoCustomizado dialogo = new DialogoCustomizado("Informação", "Todas as alterações foram salvas com sucesso!", TipoDialogo.Informacao, TipoButton.Ok);
            dialogo.ShowDialog();
        }

        private void btnLimparVinculacao_Click(object sender, EventArgs e)
        {
            CarregarProdutosParaVinculacao();
        }

        private void btnFechar_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
