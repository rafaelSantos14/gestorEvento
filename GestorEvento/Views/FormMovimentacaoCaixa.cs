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
    public partial class FormMovimentacaoCaixa : Form
    {
        private int _caixaIdSelecionado = 0;
        private MovimentacaoService _movimentacaoService;

        public FormMovimentacaoCaixa(int caixaId)
        {
            InitializeComponent();
            _caixaIdSelecionado = caixaId;
            _movimentacaoService = new MovimentacaoService();
            
            // Carregar opções de tipo de movimentação
            CarregarTiposMovimento();
        }

        private void CarregarTiposMovimento()
        {
            cmbTipoMovimento.Items.Clear();
            cmbTipoMovimento.Items.Add("Selecione...");
            cmbTipoMovimento.Items.Add("Entrada de Troco");
            cmbTipoMovimento.Items.Add("Sangria");
            cmbTipoMovimento.SelectedIndex = 0;
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            // Validar seleção do tipo de movimentação (índice 0 é o placeholder "Selecione...")
            if (cmbTipoMovimento.SelectedIndex <= 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, selecione um tipo de movimentação",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            // Validar valor
            string valorStr = txtValor.Text.Trim();
            if (string.IsNullOrEmpty(valorStr) || !decimal.TryParse(valorStr, out decimal valor) || valor <= 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Por favor, insira um valor maior que zero",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            try
            {
                string tipoSelecionado = cmbTipoMovimento.SelectedItem.ToString();
                int novoIdMovimento = 0;

                if (tipoSelecionado == "Entrada de Troco")
                {
                    novoIdMovimento = _movimentacaoService.RegistrarEntradaTroco(
                        _caixaIdSelecionado,
                        valor
                    );
                }
                else if (tipoSelecionado == "Sangria")
                {
                    novoIdMovimento = _movimentacaoService.RegistrarSangria(
                        _caixaIdSelecionado,
                        valor
                    );
                }

                if (novoIdMovimento > 0)
                {
                    DialogoCustomizado sucesso = new DialogoCustomizado(
                        "Sucesso",
                        $"Movimentação registrada com sucesso!\nValor: R$ {valor:F2}",
                        TipoDialogo.Sucesso,
                        TipoButton.Ok
                    );
                    sucesso.ShowDialog();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    DialogoCustomizado erro = new DialogoCustomizado(
                        "Erro",
                        "Erro ao registrar movimentação. Tente novamente.",
                        TipoDialogo.Erro,
                        TipoButton.Ok
                    );
                    erro.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao registrar movimentação: {ex.Message}",
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

        private void TxtValor_TextChanged(object sender, EventArgs e)
        {
            // Remove caracteres não numéricos
            string texto = new string(txtValor.Text.Where(c => char.IsDigit(c)).ToArray());

            // Se vazio, mostra "0"
            if (string.IsNullOrEmpty(texto))
            {
                texto = "0";
            }

            // Formata com 2 casas decimais
            decimal valor = decimal.Parse(texto) / 100;
            txtValor.Text = valor.ToString("F2");
            txtValor.SelectionStart = txtValor.Text.Length; // Coloca cursor no final
        }
    }
}
