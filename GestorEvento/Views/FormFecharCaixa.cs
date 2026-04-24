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
    public partial class FormFecharCaixa : Form
    {
        private int _caixaIdSelecionado = 0;
        private PontoVendaService _pontoVendaService;

        public FormFecharCaixa(int caixaId)
        {
            InitializeComponent();
            _caixaIdSelecionado = caixaId;
            _pontoVendaService = new PontoVendaService();
            
            // Buscar número do caixa e exibir
            var pontoVenda = _pontoVendaService.GetPontoVendaById(caixaId);
            int noCaixa = pontoVenda?.NoPontoVenda ?? caixaId;
            txtNomeCaixa.Text = noCaixa.ToString();
        }

        private void BtnFecharCaixaBtn_Click(object sender, EventArgs e)
        {
            // Validar valor final
            string valorFinalStr = txtValorFinal.Text.Trim();
            if (string.IsNullOrEmpty(valorFinalStr) || !decimal.TryParse(valorFinalStr, out decimal valorFinal) || valorFinal < 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, insira um valor final válido (maior ou igual a 0)",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            try
            {
                // Fechar caixa no banco de dados
                _pontoVendaService.FecharPontoVenda(
                    _caixaIdSelecionado,
                    valorFinal,
                    txtObservacoes.Text.Trim()
                );
                
                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    $"Caixa '{txtNomeCaixa.Text}' fechada com sucesso!",
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
                    $"Erro ao fechar caixa: {ex.Message}",
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

        private void TxtValorFinal_TextChanged(object sender, EventArgs e)
        {
            // Remove caracteres não numéricos
            string texto = new string(txtValorFinal.Text.Where(c => char.IsDigit(c)).ToArray());

            // Se vazio, mostra "0"
            if (string.IsNullOrEmpty(texto))
            {
                texto = "0";
            }

            // Formata com 2 casas decimais
            decimal valor = decimal.Parse(texto) / 100;
            txtValorFinal.Text = valor.ToString("F2");
            txtValorFinal.SelectionStart = txtValorFinal.Text.Length; // Coloca cursor no final
        }
    }
}
