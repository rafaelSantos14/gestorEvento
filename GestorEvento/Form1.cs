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
using GestorEvento.Services;
using MaterialSkin;
using MaterialSkin.Controls;

namespace GestorEvento
{
    public partial class Form1 : MaterialForm
    {
        private FormaPagamentoService _formaPagamentoService;

        public Form1()
        {
            InitializeComponent();
            
            // Configurar Material Skin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue500, Accent.Amber200, TextShade.BLACK);

            // Inicializar serviço
            _formaPagamentoService = new FormaPagamentoService();

            // Configurar DataGridView
            ConfigurarDataGridView();
            
            // Carregar formas de pagamento
            CarregarFormasPagamento();
        }

        private void ConfigurarDataGridView()
        {
            dgvProdutos.ColumnCount = 3;
            dgvProdutos.Columns[0].Name = "ID";
            dgvProdutos.Columns[1].Name = "Nome Forma";
            dgvProdutos.Columns[2].Name = "Código";
            
            dgvProdutos.Columns[0].Width = 50;
            dgvProdutos.Columns[1].Width = 200;
            dgvProdutos.Columns[2].Width = 150;

            dgvProdutos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
        }

        private void CarregarFormasPagamento()
        {
            dgvProdutos.Rows.Clear();
            
            try
            {
                var formas = _formaPagamentoService.GetAllFormasPagamento();
                foreach (var forma in formas)
                {
                    dgvProdutos.Rows.Add(forma.Id, forma.NmFormaPagamento, forma.CdFormaPagamento);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar formas de pagamento: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
    