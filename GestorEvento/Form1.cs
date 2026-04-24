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
        private EpsonTM20Service _epsonService;

        public Form1()
        {
            InitializeComponent();
            
            // Configurar Material Skin
            var materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.LIGHT;

            materialSkinManager.ColorScheme = new ColorScheme(Primary.Blue600, Primary.Blue700, Primary.Blue500, Accent.Amber200, TextShade.BLACK);

            // Inicializar serviços
            _formaPagamentoService = new FormaPagamentoService();
            _epsonService = new EpsonTM20Service("COM2", 9600);

            // Configurar DataGridView
            ConfigurarDataGridView();
            
            // Carregar formas de pagamento
            CarregarFormasPagamento();
        }

        private void ConfigurarDataGridView()
        {
           
        }

        private void CarregarFormasPagamento()
        {
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void BtnTestarImpressao_Click(object sender, EventArgs e)
        {
            int sucessos = 0;
            int falhas = 0;

            // Loop para imprimir 3 vezes
            for (int i = 1; i <= 1; i++)
            {
                bool sucesso = _epsonService.ImprimirCupom($"REFRIGERANTE #{i}");
                if (sucesso)
                {
                    sucessos++;
                }
                else
                {
                    falhas++;
                }
            }

            // Exibir resultado
            MessageBox.Show(
                $"Impressões concluídas!\n\nSucessos: {sucessos}\nFalhas: {falhas}",
                "Resultado",
                MessageBoxButtons.OK,
                sucessos == 3 ? MessageBoxIcon.Information : MessageBoxIcon.Warning
            );
        }
    }
}
    