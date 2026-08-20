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
    public partial class FormFecharCaixa : Form
    {
        private int _caixaIdSelecionado = 0;
        private PontoVendaService _pontoVendaService;
        private ResumoFechamentoCaixa _resumoFechamento;

        public FormFecharCaixa(int caixaId)
        {
            InitializeComponent();
            _caixaIdSelecionado = caixaId;
            _pontoVendaService = new PontoVendaService();
        }

        private void FormFecharCaixa_Load(object sender, EventArgs e)
        {
            try
            {
                // Carregar dados do resumo
                _resumoFechamento = _pontoVendaService.GetResumoFechamento(_caixaIdSelecionado);

                if (_resumoFechamento == null)
                {
                    throw new Exception("Resumo de fechamento não encontrado para o caixa selecionado.");
                }

                // Configurar título da janela
                this.Text = $"Fechamento de Caixa #{_resumoFechamento.NoPontoVenda} - {_resumoFechamento.NomePontoVenda}";
                lblTitulo.Text = this.Text;

                // Preencher dados
                PreencherResumoExecutivo();
                PreencherTabelaFormasPagamento();
                PreencherTabelaDoacoes();

                // Ajustar tamanho da janela responsivamente
                AdjustarTamanhoDaJanela();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar dados de fechamento: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
                this.Close();
            }
        }

        private void AdjustarTamanhoDaJanela()
        {
            // Obter resolução da tela
            Screen telaAtiva = Screen.FromPoint(this.Location);
            int alturaDisponivel = telaAtiva.WorkingArea.Height;
            int larguraDisponivel = telaAtiva.WorkingArea.Width;

            // Definir tamanho com margem de segurança (100px para taskbar do Windows)
            int alturaPadrao = 740;
            int larguraPadrao = 850;
            int margemSeguranca = 100;

            // Ajustar altura
            if (alturaDisponivel < alturaPadrao)
            {
                this.ClientSize = new Size(
                    Math.Min(larguraPadrao, larguraDisponivel - margemSeguranca),
                    Math.Max(600, alturaDisponivel - margemSeguranca) // Mínimo 600px de altura
                );
                this.StartPosition = FormStartPosition.CenterScreen;
            }
            else
            {
                // Tamanho normal
                this.ClientSize = new Size(larguraPadrao, alturaPadrao);
                this.StartPosition = FormStartPosition.CenterScreen;
            }

            // Se for tela pequena, ativar AutoScroll
            if (this.ClientSize.Height < 650)
            {
                this.AutoScroll = true;
            }
        }

        private void PreencherResumoExecutivo()
        {
            lblTituloResumo.Text = $"Caixa #{_resumoFechamento.NoPontoVenda} - {_resumoFechamento.NomePontoVenda} | Aberto em {_resumoFechamento.DtAbertura:dd/MM/yyyy HH:mm:ss}";
            
            int comprimentoBase = 35; // Comprimento para alinhamento
            
            lblAbertura.Text = AlinharComPontos("Valor de Abertura", $"R$ {_resumoFechamento.VlInicial:F2}", comprimentoBase);
            lblTotalDinheiro.Text = AlinharComPontos("Total Vendido (Dinheiro)", $"R$ {_resumoFechamento.TotalVendasDinheiro:F2}", comprimentoBase);
            
            // Calcular total de troco
            decimal totalTroco = _resumoFechamento.Movimentacoes
                .Where(m => m.TipoMovimento == "TROCO")
                .Sum(m => m.VlMovimento);
            lblTotalTroco.Text = AlinharComPontos("Total Troco", $"R$ -{totalTroco:F2}", comprimentoBase);
            
            // Calcular total de entrada de troco
            decimal totalEntradaTroco = _resumoFechamento.Movimentacoes
                .Where(m => m.TipoMovimento == "ENTRADA_TROCO")
                .Sum(m => m.VlMovimento);
            lblTotalEntradaTroco.Text = AlinharComPontos("Total Entrada de Troco", $"R$ {totalEntradaTroco:F2}", comprimentoBase);
            
            // Calcular total de sangria
            decimal totalSangria = _resumoFechamento.Movimentacoes
                .Where(m => m.TipoMovimento == "SANGRIA")
                .Sum(m => m.VlMovimento);
            lblTotalSangria.Text = AlinharComPontos("Total Sangria", $"R$ -{totalSangria:F2}", comprimentoBase);
            
            lblTotalEsperado.Text = AlinharComPontos("TOTAL ESPERADO", $"R$ {_resumoFechamento.TotalEsperado:F2}", comprimentoBase);

            int totalVendas = _resumoFechamento.Vendas.Count(v => !string.Equals(v.TipoOperacao, "CORTESIA", StringComparison.OrdinalIgnoreCase));
            int totalCortesias = _resumoFechamento.Vendas.Count(v => string.Equals(v.TipoOperacao, "CORTESIA", StringComparison.OrdinalIgnoreCase));
            decimal valorTotalCortesias = _resumoFechamento.Vendas
                .Where(v => string.Equals(v.TipoOperacao, "CORTESIA", StringComparison.OrdinalIgnoreCase))
                .Sum(v => v.VlTotal);

            lblTotalVendas.Text = AlinharComPontos("Total de Vendas", $"{totalVendas}", comprimentoBase);
            lblTotalCortesias.Text = AlinharComPontos("Total de Cortesias", $"{totalCortesias}", comprimentoBase);
            lblValorTotalCortesias.Text = AlinharComPontos("Valor Total Cortesias", $"R$ {valorTotalCortesias:F2}", comprimentoBase);

            // Só a parte em Dinheiro (é a que efetivamente afeta a conferência do caixa físico);
            // o detalhamento com todas as formas fica na aba "Doações"
            lblTotalDoacoes.Text = AlinharComPontos("Total Doações (Dinheiro)", $"R$ {_resumoFechamento.TotalDoacoesDinheiro:F2}", comprimentoBase);
        }

        private string AlinharComPontos(string descricao, string valor, int comprimentoTotal)
        {
            int espacoDisponivel = comprimentoTotal - descricao.Length;
            if (espacoDisponivel < 3) espacoDisponivel = 3;
            return descricao + new string('.', espacoDisponivel) + " " + valor;
        }

        private void PreencherTabelaFormasPagamento()
        {
            dgvFormasPagamento.Rows.Clear();

            foreach (var resumo in _resumoFechamento.RecebimentosPorForma)
            {
                dgvFormasPagamento.Rows.Add(
                    resumo.NomeFormaPagamento,
                    $"R$ {resumo.TotalRecebimento:F2}"
                );
            }

            // Adicionar linha de total
            decimal totalGeral = _resumoFechamento.RecebimentosPorForma.Sum(r => r.TotalRecebimento);
            dgvFormasPagamento.Rows.Add(
                "TOTAL",
                $"R$ {totalGeral:F2}"
            );

            // Colorir última linha (TOTAL)
            int lastRow = dgvFormasPagamento.Rows.Count - 1;
            dgvFormasPagamento.Rows[lastRow].DefaultCellStyle.BackColor = Color.LightGray;
            dgvFormasPagamento.Rows[lastRow].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void PreencherTabelaDoacoes()
        {
            dgvDoacoes.Rows.Clear();

            foreach (var resumo in _resumoFechamento.DoacoesPorForma)
            {
                dgvDoacoes.Rows.Add(
                    resumo.NomeFormaPagamento,
                    $"R$ {resumo.TotalDoacao:F2}"
                );
            }

            // Adicionar linha de total
            decimal totalGeral = _resumoFechamento.DoacoesPorForma.Sum(d => d.TotalDoacao);
            dgvDoacoes.Rows.Add(
                "TOTAL",
                $"R$ {totalGeral:F2}"
            );

            // Colorir última linha (TOTAL)
            int lastRow = dgvDoacoes.Rows.Count - 1;
            dgvDoacoes.Rows[lastRow].DefaultCellStyle.BackColor = Color.LightGray;
            dgvDoacoes.Rows[lastRow].DefaultCellStyle.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }

        private void TxtValorContado_TextChanged(object sender, EventArgs e)
        {
            try
            {
                // Remove caracteres não numéricos
                string texto = new string(txtValorContado.Text.Where(c => char.IsDigit(c)).ToArray());

                if (string.IsNullOrEmpty(texto))
                {
                    texto = "0";
                }

                // Formata com 2 casas decimais
                decimal valor = decimal.Parse(texto) / 100;
                txtValorContado.Text = valor.ToString("F2");
                txtValorContado.SelectionStart = txtValorContado.Text.Length;

                // Calcular diferença
                decimal diferenca = valor - _resumoFechamento.TotalEsperado;
                lblDiferenca.Text = $"DIFERENÇA: R$ {diferenca:F2}";

                // Colorir
                if (diferenca < 0)
                {
                    lblDiferenca.ForeColor = Color.Red;
                }
                else if (diferenca == 0)
                {
                    lblDiferenca.ForeColor = Color.Green;
                }
                else
                {
                    lblDiferenca.ForeColor = Color.Blue;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao processar valor contado: {ex.Message}");
            }
        }

        private void BtnFecharCaixa_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar valor contado
                if (!decimal.TryParse(txtValorContado.Text, out decimal valorContado) || valorContado < 0)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        "Por favor, insira um valor contado válido (maior ou igual a 0)",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                // Fechar caixa
                bool sucesso = _pontoVendaService.FecharPontoVenda(
                    _caixaIdSelecionado,
                    valorContado,
                    txtObservacoes.Text.Trim()
                );

                if (sucesso)
                {
                    DialogoCustomizado mensagem = new DialogoCustomizado(
                        "Sucesso",
                        $"Caixa #{_resumoFechamento.NoPontoVenda} - {_resumoFechamento.NomePontoVenda} fechado com sucesso!\n\n" +
                        $"Total Esperado: R$ {_resumoFechamento.TotalEsperado:F2}\n" +
                        $"Valor Contado: R$ {valorContado:F2}\n" +
                        $"Diferença: R$ {(valorContado - _resumoFechamento.TotalEsperado):F2}",
                        TipoDialogo.Sucesso,
                        TipoButton.Ok
                    );
                    mensagem.ShowDialog();

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    DialogoCustomizado erro = new DialogoCustomizado(
                        "Erro",
                        "Erro ao fechar caixa. Tente novamente.",
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

        private void BtnFecharTitulo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
