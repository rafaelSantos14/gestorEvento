using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using GestorEvento.Models;
using GestorEvento.Services;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class RelatorioVendaUserControl : UserControl
    {
        private readonly RelatorioVendaService _relatorioService;

        public RelatorioVendaUserControl()
        {
            InitializeComponent();
            _relatorioService = new RelatorioVendaService();
            DoubleBuffered = true;
            ConfigurarGridProdutosVendidos();
        }

        /// <summary>
        /// Método público para carregar dados do relatório de vendas
        /// Chamado pela tela consolidada
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

                // Configurar BackColor dos gráficos ANTES de carregar dados
                chartBarras.BackColor = Color.White;
                chartPizza.BackColor = Color.White;

                // Limpar dados anteriores
                LimparCards();
                LimparGraficos();
                dgvProdutosVendidos.Rows.Clear();

                // Buscar dados
                var dadosRelatorio = _relatorioService.ObterDadosRelatorio(idEvento);

                // Atualizar cards
                AtualizarCards(dadosRelatorio);

                // Atualizar gráficos
                AtualizarGraficos(dadosRelatorio);

                // Atualizar grid de produtos vendidos
                AtualizarGridProdutosVendidos(dadosRelatorio);

                // Mostrar novamente com fundo correto
                this.Visible = true;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.Visible = true;
                MessageBox.Show($"Erro ao carregar relatório: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarCards(RelatorioVendaData dados)
        {
            // Card 1: Quantidade Total de Vendas
            lblQtdeValor.Text = dados.TotalQuantidadeVendas.ToString();

            // Card 2: Valor Total Vendido
            lblValorVendidoValor.Text = $"R$ {dados.ValorTotalVendido:N2}";

            // Card 3: Valor Total Troco
            lblTrocoValor.Text = $"R$ {dados.ValorTotalTroco:N2}";
            
            // Log de cortesias para auditoria (se houver)
            if (dados.TotalQuantidadeCortesia > 0)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[RELATÓRIO VENDA] Cortesias neste evento: Qtd={dados.TotalQuantidadeCortesia}, Valor=R${dados.ValorTotalCortesia:N2}"
                );
            }
        }

        private void LimparCards()
        {
            lblQtdeValor.Text = "-";
            lblValorVendidoValor.Text = "-";
            lblTrocoValor.Text = "-";
        }

        private void AtualizarGraficos(RelatorioVendaData dados)
        {
            AtualizarGraficoBarras(dados);
            AtualizarGraficoPizza(dados);
        }

        private void AtualizarGraficoBarras(RelatorioVendaData dados)
        {
            try
            {
                if (dados.DadosPorCaixa.Count == 0)
                    return;

                // Limpar séries anteriores
                chartBarras.Series.Clear();

                // Criar série de dados para o gráfico de colunas (duas séries lado a lado)
                var seriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Valor Total (R$)",
                        Values = new ChartValues<decimal>(dados.DadosPorCaixa.Select(c => c.ValorTotal)),
                        ScalesYAt = 0  // Usa o primeiro eixo Y (Valor)
                    },
                    new ColumnSeries
                    {
                        Title = "Quantidade de Vendas",
                        Values = new ChartValues<int>(dados.DadosPorCaixa.Select(c => c.QuantidadeVendas)),
                        ScalesYAt = 1  // Usa o segundo eixo Y (Quantidade)
                    }
                };

                chartBarras.Series = seriesCollection;

                // Configurar eixo X com os nomes das caixas
                chartBarras.AxisX = new AxesCollection
                {
                    new Axis
                    {
                        Title = "Caixas",
                        Labels = dados.DadosPorCaixa.Select(c => $"Caixa #{c.NumeroCaixa} - {c.NomeCaixa}").ToList(),
                        Separator = new Separator() { Step = 1 }
                    }
                };

                // Configurar eixo Y com dois eixos (esquerda e direita)
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
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar gráfico de barras: {ex.Message}", "Erro");
            }
        }

        private void AtualizarGraficoPizza(RelatorioVendaData dados)
        {
            try
            {
                if (dados.DadosPorFormaPagamento.Count == 0)
                    return;

                // Limpar gráfico completamente
                chartPizza.Series.Clear();

                // Criar série de dados para o gráfico de pizza
                var seriesCollection = new SeriesCollection();

                foreach (var pagamento in dados.DadosPorFormaPagamento)
                {
                    seriesCollection.Add(new PieSeries
                    {
                        Title = pagamento.NomeFormaPagamento,
                        Values = new ChartValues<decimal> { pagamento.ValorTotal },
                        DataLabels = true
                    });
                }

                chartPizza.Series = seriesCollection;
                chartPizza.LegendLocation = LegendLocation.Bottom;
                chartPizza.BackColor = Color.White;
                
                // Forçar redraw do gráfico
                Application.DoEvents();
                chartPizza.Invalidate();
                chartPizza.Refresh();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar gráfico de pizza: {ex.Message}", "Erro");
            }
        }

        private void LimparGraficos()
        {
            chartBarras.Series.Clear();
            chartPizza.Series.Clear();
        }

        private void ConfigurarGridProdutosVendidos()
        {
            dgvProdutosVendidos.AutoGenerateColumns = false;
            dgvProdutosVendidos.AllowUserToAddRows = false;
            dgvProdutosVendidos.AllowUserToDeleteRows = false;
            dgvProdutosVendidos.ReadOnly = true;
            dgvProdutosVendidos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvProdutosVendidos.MultiSelect = false;
            dgvProdutosVendidos.RowHeadersVisible = false;

            dgvProdutosVendidos.Columns.Clear();
            dgvProdutosVendidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "NomeProduto", HeaderText = "Nome do Produto", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvProdutosVendidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantidadeVendida", HeaderText = "Qtd. Vendida", Width = 95 });
            dgvProdutosVendidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "QuantidadeDisponivel", HeaderText = "Qtd. Disponível", Width = 105 });
            dgvProdutosVendidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "PrecoUnitario", HeaderText = "Preço Un.", Width = 90 });
            dgvProdutosVendidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "ValorTotal", HeaderText = "Valor Total Vendido", Width = 140 });
            dgvProdutosVendidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Percentual", HeaderText = "% do Total", Width = 90 });

            dgvProdutosVendidos.DefaultCellStyle.ForeColor = Color.Black;
            dgvProdutosVendidos.DefaultCellStyle.BackColor = Color.White;
            dgvProdutosVendidos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvProdutosVendidos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvProdutosVendidos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvProdutosVendidos.EnableHeadersVisualStyles = false;
        }

        private void AtualizarGridProdutosVendidos(RelatorioVendaData dados)
        {
            dgvProdutosVendidos.Rows.Clear();

            foreach (var produto in dados.DadosProdutosVendidos)
            {
                dgvProdutosVendidos.Rows.Add(
                    produto.NomeProduto,
                    produto.QuantidadeVendida,
                    produto.QuantidadeDisponivel,
                    $"R$ {produto.PrecoUnitario:N2}",
                    $"R$ {produto.ValorTotalVendido:N2}",
                    $"{produto.PercentualTotalVendas:N1}%"
                );
            }
        }
    }
}
