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

namespace GestorEvento.Views
{
    public partial class FormAbrirCaixa : Form
    {
        private int _eventoIdSelecionado = 0;
        private PontoVendaService _pontoVendaService;

        public FormAbrirCaixa(int eventoId)
        {
            InitializeComponent();
            _eventoIdSelecionado = eventoId;
            _pontoVendaService = new PontoVendaService();
        }

        private void BtnAbrir_Click(object sender, EventArgs e)
        {
            // Validar valor inicial
            string valorInicialStr = txtValorInicial.Text.Trim();
            if (string.IsNullOrEmpty(valorInicialStr) || !decimal.TryParse(valorInicialStr, out decimal valorInicial) || valorInicial < 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, insira um valor inicial válido (maior ou igual a 0)",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            try
            {
                // Obter descrição do campo de texto (pode ser vazio/nulo)
                string descricao = txtDescCaixa.Text.Trim();
                if (string.IsNullOrEmpty(descricao))
                    descricao = null;

                // Abrir caixa no banco de dados
                int novoIdCaixa = _pontoVendaService.AbrirPontoVenda(
                    _eventoIdSelecionado,
                    valorInicial,
                    descricao
                );

                // Buscar o ponto de venda para obter o NoPontoVenda
                var pontoVenda = _pontoVendaService.GetPontoVendaById(novoIdCaixa);
                int noCaixa = pontoVenda?.NoPontoVenda ?? novoIdCaixa;

                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    $"Caixa #{noCaixa} aberta com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao abrir caixa: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void TxtValorInicial_TextChanged(object sender, EventArgs e)
        {
            // Remove caracteres não numéricos
            string texto = new string(txtValorInicial.Text.Where(c => char.IsDigit(c)).ToArray());

            // Se vazio, mostra "0"
            if (string.IsNullOrEmpty(texto))
            {
                texto = "0";
            }

            // Formata com 2 casas decimais
            decimal valor = decimal.Parse(texto) / 100;
            txtValorInicial.Text = valor.ToString("F2");
            txtValorInicial.SelectionStart = txtValorInicial.Text.Length; // Coloca cursor no final
        }
    }
}
