using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MetroFramework;
using GestorEvento.Utilities;
using GestorEvento.Services;
using GestorEvento.Models;

namespace GestorEvento.Views
{
    public partial class FormProdutos : Form
    {
        private ProdutoService _service;

        public FormProdutos()
        {
            InitializeComponent();
            
            // Inicializar serviço
            _service = new ProdutoService();
            
            // Aplicar estilos padrão
            EstiloManager.AplicarEstiloSalvar(btnSalvar);           
            EstiloManager.AplicarEstiloDeletar(btnDeletar);
            EstiloManager.AplicarEstiloInfo(btnPesquisar);

            // Configurar DataGridView
            ConfigurarDataGridView();
            
            // Carregar dados iniciais do banco
            CarregarProdutosDoDb();
        }

        private void ConfigurarDataGridView()
        {
            // Limpar colunas existentes
            dgvProdutos.Columns.Clear();

            // Criar coluna ID
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                Width = 50,
                ReadOnly = true
            };
            dgvProdutos.Columns.Add(colId);

            // Criar coluna Nome
            var colNome = new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Nome",
                Width = 300,
                AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
            };
            dgvProdutos.Columns.Add(colNome);

            // Configurar cores do texto
            dgvProdutos.DefaultCellStyle.ForeColor = Color.Black;
            dgvProdutos.DefaultCellStyle.BackColor = Color.White;
            dgvProdutos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProdutos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210); // Azul Material Design
            dgvProdutos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Cinza claro

            // Handler para duplo-clique (editar)
            dgvProdutos.CellDoubleClick += DataGridViewProdutos_CellDoubleClick;

            // Handler para seleção de linha
            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CarregarProdutosDoDb()
        {
            dgvProdutos.Rows.Clear();
            
            try
            {
                var produtos = _service.GetAllProducts();
                
                if (produtos.Count == 0)
                {
                    // Se não houver produtos, mostrar mensagem informativa
                    return;
                }

                foreach (var produto in produtos)
                {
                    dgvProdutos.Rows.Add(produto.Id, produto.Nome);
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar produtos: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
            }
        }

        private void DataGridViewProdutos_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            // Validar se há linhas no grid
            if (e.RowIndex < 0 || dgvProdutos.Rows.Count == 0) 
                return;

            // Validar se as células têm valores
            if (dgvProdutos.Rows[e.RowIndex].Cells["ID"].Value == null || 
                dgvProdutos.Rows[e.RowIndex].Cells["Nome"].Value == null)
            {
                return;
            }

            int produtoId = Convert.ToInt32(dgvProdutos.Rows[e.RowIndex].Cells["ID"].Value);
            string nomeProduto = dgvProdutos.Rows[e.RowIndex].Cells["Nome"].Value.ToString();

            // Abrir FormEditarProduto como dialog modal
            var formEditar = new FormEditarProduto(produtoId);
            if (formEditar.ShowDialog(this) == DialogResult.OK)
            {
                // Se salvou com sucesso, recarregar dados do grid
                CarregarProdutosDoDb();
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validar campo
            if (string.IsNullOrWhiteSpace(txtNomeProduto.Text))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, preencha o nome do produto",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            // Criar novo produto
            var produto = new Produto
            {
                Nome = txtNomeProduto.Text.Trim()
            };

            // Tentar salvar no banco
            if (_service.CreateProduct(produto))
            {
                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    "Produto salvo com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();
                
                // Limpar campo e recarregar grid
                txtNomeProduto.Clear();
                CarregarProdutosDoDb();
            }           
        }

        private void btnLimpar_Click(object sender, EventArgs e)
        {
            txtNomeProduto.Clear();
        }

        private void btnDeletar_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Selecione um produto na lista para deletar",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            string nomeProduto = dgvProdutos.SelectedRows[0].Cells["Nome"].Value.ToString();
            DialogoCustomizado confirmacao = new DialogoCustomizado(
                "Confirmação",
                $"Deseja realmente deletar o produto '{nomeProduto}'?",
                TipoDialogo.Aviso,
                TipoButton.SimNao
            );
            
            if (confirmacao.ShowDialog() == DialogResult.Yes)
            {
                // Obter ID do produto selecionado
                if (dgvProdutos.SelectedRows[0].Cells["ID"].Value == null)
                    return;

                int produtoId = Convert.ToInt32(dgvProdutos.SelectedRows[0].Cells["ID"].Value);
                int rowIndex = dgvProdutos.SelectedRows[0].Index;
                
                // Tentar deletar do banco
                if (_service.DeleteProduct(produtoId))
                {
                    // Remover linha do grid
                    dgvProdutos.Rows.RemoveAt(rowIndex);

                    DialogoCustomizado sucesso = new DialogoCustomizado(
                        "Sucesso",
                        "Produto deletado com sucesso!",
                        TipoDialogo.Sucesso,
                        TipoButton.Ok
                    );
                    sucesso.ShowDialog();
                }
                else
                {
                    DialogoCustomizado erro = new DialogoCustomizado(
                        "Erro",
                        "Erro ao deletar produto. Tente novamente.",
                        TipoDialogo.Erro,
                        TipoButton.Ok
                    );
                    erro.ShowDialog();
                }
            }
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvProdutos.SelectedRows.Count == 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Informação",
                    "Selecione um produto na lista para editar",
                    TipoDialogo.Informacao,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            // Validar se as células têm valores
            if (dgvProdutos.SelectedRows[0].Cells["ID"].Value == null || 
                dgvProdutos.SelectedRows[0].Cells["Nome"].Value == null)
            {
                return;
            }

            // Abrir FormEditarProduto como dialog modal
            int produtoId = Convert.ToInt32(dgvProdutos.SelectedRows[0].Cells["ID"].Value);
            var formEditar = new FormEditarProduto(produtoId);
            if (formEditar.ShowDialog(this) == DialogResult.OK)
            {
                // Se salvou com sucesso, recarregar dados do grid
                CarregarProdutosDoDb();
            }
        }

        private void btnPesquisar_Click(object sender, EventArgs e)
        {
            string filtro = txtPesquisar.Text.Trim();
            dgvProdutos.Rows.Clear();

            try
            {
                var produtos = _service.SearchProducts(filtro);
                
                if (produtos.Count == 0)
                {
                    DialogoCustomizado info = new DialogoCustomizado(
                        "Informação",
                        $"Nenhum produto encontrado com o filtro: '{filtro}'",
                        TipoDialogo.Informacao,
                        TipoButton.Ok
                    );
                    info.ShowDialog();
                    CarregarProdutosDoDb();
                    return;
                }

                foreach (var produto in produtos)
                {
                    dgvProdutos.Rows.Add(produto.Id, produto.Nome);
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao buscar produtos: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
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
    }
}
