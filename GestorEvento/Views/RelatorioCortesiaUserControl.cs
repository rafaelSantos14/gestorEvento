using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using GestorEvento.Models;
using GestorEvento.Services;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class RelatorioCortesiaUserControl : UserControl
    {
        private readonly RelatorioVendaService _relatorioService;

        public RelatorioCortesiaUserControl()
        {
            InitializeComponent();
            _relatorioService = new RelatorioVendaService();
            DoubleBuffered = true;
            ConfigurarGridProdutos();
        }

        /// <summary>
        /// Método público para carregar dados do relatório de cortesia
        /// </summary>
        public void CarregarDados(int idEvento)
        {
            CarregarDadosRelatorio(idEvento);
        }

        private void CarregarDadosRelatorio(int idEvento)
        {
            try
            {
                // Ocultar temporariamente para evitar renderização incorreta
                this.Visible = false;

                // Configurar BackColor do gráfico ANTES de carregar dados
                chartBarras.BackColor = Color.White;

                LimparCards();
                LimparGraficos();
                dgvProdutosCortesia.Rows.Clear();

                var dados = _relatorioService.ObterDadosRelatorioCortesia(idEvento);

                AtualizarCards(dados);
                AtualizarGraficoBarras(dados);
                AtualizarGridProdutos(dados);

                // Mostrar novamente com fundo correto
                this.Visible = true;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.Visible = true;
                MessageBox.Show($"Erro ao carregar relatório de cortesia: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarCards(RelatorioVendaData dados)
        {
            decimal ticketMedioCortesia = dados.TotalQuantidadeCortesia > 0
                ? dados.ValorTotalCortesia / dados.TotalQuantidadeCortesia
                : 0m;

            lblQtdCortesiaValor.Text = dados.TotalQuantidadeCortesia.ToString();
            lblValorCortesiaValor.Text = $"R$ {dados.ValorTotalCortesia:N2}";
            lblTicketCortesiaValor.Text = $"R$ {ticketMedioCortesia:N2}";
        }

        private void LimparCards()
        {
            lblQtdCortesiaValor.Text = "-";
            lblValorCortesiaValor.Text = "R$ -";
            lblTicketCortesiaValor.Text = "R$ -";
        }

        private void AtualizarGraficoBarras(RelatorioVendaData dados)
        {
            if (dados.DadosPorCaixa.Count == 0)
            {
                return;
            }

            chartBarras.Series = new SeriesCollection
            {
                new ColumnSeries
                {
                    Title = "Valor Cortesia (R$)",
                    Values = new ChartValues<decimal>(dados.DadosPorCaixa.Select(c => c.ValorTotal)),
                    ScalesYAt = 0  // Usa o primeiro eixo Y (Valor)
                },
                new ColumnSeries
                {
                    Title = "Quantidade Itens Cortesia",
                    // QuantidadeVendas agora contém a quantidade TOTAL DE ITENS (não vendas-cortesia)
                    Values = new ChartValues<int>(dados.DadosPorCaixa.Select(c => c.QuantidadeVendas)),
                    ScalesYAt = 1  // Usa o segundo eixo Y (Quantidade)
                }
            };

            chartBarras.AxisX = new AxesCollection
            {
                new Axis
                {
                    Title = "Caixas",
                    Labels = dados.DadosPorCaixa.Select(c => $"Caixa #{c.NumeroCaixa}").ToList(),
                    Separator = new Separator { Step = 1 }
                }
            };

            chartBarras.AxisY = new AxesCollection
            {
                new Axis
                {
                    Title = "Valor (R$)",
                    Position = AxisPosition.LeftBottom,
                    LabelFormatter = value => value.ToString("C0")
                },
                new Axis
                {
                    Title = "Quantidade",
                    Position = AxisPosition.RightTop,
                    LabelFormatter = value => value.ToString("N0")
                }
            };

            chartBarras.LegendLocation = LegendLocation.Bottom;
            chartBarras.BackColor = Color.White;
            
            // Forçar redraw do gráfico
            Application.DoEvents();
            chartBarras.Invalidate();
            chartBarras.Refresh();
        }

        private void LimparGraficos()
        {
            chartBarras.Series.Clear();
        }

        private void ConfigurarGridProdutos()
        {
            dgvProdutosCortesia.AutoGenerateColumns = false;
            dgvProdutosCortesia.AllowUserToAddRows = false;
            dgvProdutosCortesia.AllowUserToDeleteRows = false;
            dgvProdutosCortesia.ReadOnly = true;
            dgvProdutosCortesia.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProdutosCortesia.MultiSelect = false;
            dgvProdutosCortesia.RowHeadersVisible = false;

            dgvProdutosCortesia.Columns.Clear();
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "NomeProduto", HeaderText = "Nome do Produto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantidadeInicial", HeaderText = "Qtd. Inicial", Width = 90 });
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantidadeVendida", HeaderText = "Qtd. Vendida", Width = 95 });
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantidade", HeaderText = "Qtde Cortesias", Width = 100 });
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantidadeDisponivel", HeaderText = "Qtd. Disponível", Width = 105 });
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecoUnitario", HeaderText = "Preço Un.", Width = 90 });
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "ValorTotal", HeaderText = "Valor Total Cortesia", Width = 140 });
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "Percentual", HeaderText = "% do Total", Width = 90 });

            dgvProdutosCortesia.DefaultCellStyle.ForeColor = Color.Black;
            dgvProdutosCortesia.DefaultCellStyle.BackColor = Color.White;
            dgvProdutosCortesia.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProdutosCortesia.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvProdutosCortesia.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvProdutosCortesia.EnableHeadersVisualStyles = false;
        }

        private void AtualizarGridProdutos(RelatorioVendaData dados)
        {
            dgvProdutosCortesia.Rows.Clear();

            foreach (var produto in dados.DadosProdutosVendidos)
            {
                // Mostrar apenas produtos que tiveram cortesia
                if (produto.QuantidadeCortesia > 0)
                {
                    dgvProdutosCortesia.Rows.Add(
                        produto.NomeProduto,
                        produto.QuantidadeInicial,
                        produto.QuantidadeVendida,
                        produto.QuantidadeCortesia,
                        produto.QuantidadeDisponivel,
                        $"R$ {produto.PrecoUnitario:N2}",
                        $"R$ {produto.ValorTotalVendido:N2}",
                        $"{produto.PercentualTotalVendas:N1}%"
                    );
                }
            }
        }
    }
}
