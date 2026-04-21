using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using GestorEvento.Repositories;
using GestorEvento.Views;
using MaterialSkin;
using MaterialSkin.Controls;

namespace GestorEvento
{
    public partial class Form1 : MaterialForm
    {
        public Form1()
        {
            InitializeComponent();
            
            // Configurar Material Skin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue500, Accent.Amber200, TextShade.BLACK);

            // Configurar DataGridView
            ConfigurarDataGridView();
            
            // Carregar dados iniciais de exemplo
            CarregarDadosExemplo();
        }

        private void ConfigurarDataGridView()
        {
            dgvProdutos.ColumnCount = 4;
            dgvProdutos.Columns[0].Name = "ID";
            dgvProdutos.Columns[1].Name = "Nome";
            dgvProdutos.Columns[2].Name = "Preço";
            dgvProdutos.Columns[3].Name = "Descrição";
            
            dgvProdutos.Columns[0].Width = 50;
            dgvProdutos.Columns[1].Width = 200;
            dgvProdutos.Columns[2].Width = 100;
            dgvProdutos.Columns[3].Width = 400;

            // Handler para duplo-clique (editar)
            dgvProdutos.CellDoubleClick += DataGridView1_CellDoubleClick;

            // Handler para seleção de linha
            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CarregarDadosExemplo()
        {
            dgvProdutos.Rows.Clear();
            
            // Dados de exemplo para demonstração
            dgvProdutos.Rows.Add(1, "Cerveja Corona", "R$ 8,50", "Cerveja importada 350ml");
            dgvProdutos.Rows.Add(2, "Refrigerante Coca-Cola", "R$ 5,00", "Refrigerante 2L");
            dgvProdutos.Rows.Add(3, "Suco Natural", "R$ 12,00", "Suco de laranja natural fresco");
            dgvProdutos.Rows.Add(4, "Água Mineral", "R$ 2,50", "Água mineral 500ml");
            dgvProdutos.Rows.Add(5, "Cerveja Heineken", "R$ 9,00", "Cerveja importada 350ml");
        }

        private void DataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            int produtoId = Convert.ToInt32(dgvProdutos.Rows[e.RowIndex].Cells[0].Value);
            string nomeProduto = dgvProdutos.Rows[e.RowIndex].Cells[1].Value.ToString();

            // TODO: Abrir FormEditarProduto(produtoId)
            DialogoCustomizado dialogo = new DialogoCustomizado(
                "Editar",
                $"Abrindo editor para o produto ID {produtoId}: {nomeProduto}\n\n(FormEditarProduto será criado em breve)",
                TipoDialogo.Informacao,
                TipoButton.Ok
            );
            dialogo.ShowDialog();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        // ===== HANDLERS DA ABA CADASTRO =====

        private void btnSalvarForm_Click(object sender, EventArgs e)
        {
            // TODO: Validar campos
            // TODO: Salvar no banco de dados
            DialogoCustomizado dialogo = new DialogoCustomizado(
                "Sucesso",
                "Produto salvo com sucesso!",
                TipoDialogo.Sucesso,
                TipoButton.Ok
            );
            dialogo.ShowDialog();
        }

        private void btnLimparForm_Click(object sender, EventArgs e)
        {
            // Limpar campos
            // TODO: Implementar limpeza de controles
        }

        private void btnEditarForm_Click(object sender, EventArgs e)
        {
            // TODO: Se houver seleção no grid, preencher campos para editar
            DialogoCustomizado dialogo = new DialogoCustomizado(
                "Informação",
                "Selecione um produto na lista para editar",
                TipoDialogo.Informacao,
                TipoButton.Ok
            );
            dialogo.ShowDialog();
        }

        private void btnDeletarForm_Click(object sender, EventArgs e)
        {
            // TODO: Validar seleção no grid
            DialogoCustomizado confirmacao = new DialogoCustomizado(
                "Confirmação",
                "Deseja realmente deletar este produto?",
                TipoDialogo.Aviso,
                TipoButton.SimNao
            );

            if (confirmacao.ShowDialog() == DialogResult.Yes)
            {
                // TODO: Deletar do banco de dados
                // TODO: Remover linha do grid
                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    "Produto deletado com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();

                // Recarregar grid
                CarregarDadosExemplo();
            }
        }

        // ===== HANDLERS DA ABA LISTA =====

        private void btnPesquisarForm_Click(object sender, EventArgs e)
        {
            // TODO: Implementar pesquisa no banco de dados
            DialogoCustomizado dialogo = new DialogoCustomizado(
                "Pesquisa",
                "Buscando produtos com filtro...\n\n(Integração com banco em breve)",
                TipoDialogo.Informacao,
                TipoButton.Ok
            );
            dialogo.ShowDialog();

            // Por enquanto, apenas recarrega dados
            CarregarDadosExemplo();
        }
    }
}
    