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
    public partial class RelatorioCaixaUserControl : UserControl
    {
        private readonly RelatorioVendaService _relatorioService;

        public RelatorioCaixaUserControl()
        {
            InitializeComponent();
            _relatorioService = new RelatorioVendaService();
            DoubleBuffered = true;
            ConfigurarGridResumo();
            tooltip.SetToolTip(pnlCardValor, "Valor referente ao total vendido, já descontando o valor do troco.");
            tooltip.SetToolTip(lblValorVendidoValor, "Valor referente ao total vendido, já descontando o valor do troco.");
        }

        /// <summary>
        /// Método público para carregar dados do relatório de caixa
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

                LimparCards();
                LimparGraficos();
                dgvResumoCaixas.Rows.Clear();

                var dadosRelatorio = _relatorioService.ObterDadosRelatorio(idEvento);

                AtualizarCards(dadosRelatorio);
                AtualizarGraficos(dadosRelatorio);
                AtualizarGridResumo(dadosRelatorio);

                // Mostrar novamente com fundo correto
                this.Visible = true;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.Visible = true;
                MessageBox.Show($"Erro ao carregar relatório de caixas: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void AtualizarCards(RelatorioVendaData dados)
        {
            decimal ticketMedio = dados.TotalQuantidadeVendas > 0
                ? dados.ValorTotalVendido / dados.TotalQuantidadeVendas
                : 0;

            lblCaixasValor.Text = dados.TotalQuantidadeVendas.ToString();
            lblValorVendidoValor.Text = $"R$ {dados.ValorTotalVendido:N2}";
            lblTrocoTotalValor.Text = $"R$ {dados.ValorTotalTroco:N2}";
            lblTicketMedioValor.Text = $"R$ {ticketMedio:N2}";
            
            // Log de cortesias para auditoria (se houver)
            if (dados.TotalQuantidadeCortesia > 0)
            {
                System.Diagnostics.Debug.WriteLine(
                    $"[RELATÓRIO CAIXA] Cortesias neste período: Qtd={dados.TotalQuantidadeCortesia}, Valor=R${dados.ValorTotalCortesia:N2}"
                );
            }
        }

        private void LimparCards()
        {
            lblCaixasValor.Text = "-";
            lblValorVendidoValor.Text = "-";
            lblTrocoTotalValor.Text = "-";
            lblTicketMedioValor.Text = "-";
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
                {
                    return;
                }

                // Limpar séries anteriores
                chartBarras.Series.Clear();

                chartBarras.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Valor Vendido (R$)",
                        Values = new ChartValues<decimal>(dados.DadosPorCaixa.Select(c => c.ValorTotal))
                    }
                };

                chartBarras.AxisX = new AxesCollection
                {
                    new Axis
                    {
                        Title = "Caixas (PDV)",
                        Labels = dados.DadosPorCaixa.Select(c => $"Caixa #{c.NumeroCaixa} - {c.NomeCaixa}").ToList(),
                        Separator = new Separator { Step = 1 }
                    }
                };

                chartBarras.AxisY = new AxesCollection
                {
                    new Axis
                    {
                        Title = "Valor (R$)",
                        LabelFormatter = value => value.ToString("C0")
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
                {
                    return;
                }

                // Limpar séries anteriores
                chartPizza.Series.Clear();

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

        private void ConfigurarGridResumo()
        {
            dgvResumoCaixas.AutoGenerateColumns = false;
            dgvResumoCaixas.AllowUserToAddRows = false;
            dgvResumoCaixas.AllowUserToDeleteRows = false;
            dgvResumoCaixas.ReadOnly = true;
            dgvResumoCaixas.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvResumoCaixas.MultiSelect = false;
            dgvResumoCaixas.RowHeadersVisible = false;

            dgvResumoCaixas.Columns.Clear();
            dgvResumoCaixas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Numero", HeaderText = "Caixa", Width = 80 });
            dgvResumoCaixas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Ponto de Venda", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvResumoCaixas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Qtde", HeaderText = "Vendas", Width = 80 });
            dgvResumoCaixas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Valor", HeaderText = "Valor Total", Width = 120 });
            dgvResumoCaixas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Troco", HeaderText = "Troco", Width = 110 });
            dgvResumoCaixas.Columns.Add(new DataGridViewTextBoxColumn { Name = "Percentual", HeaderText = "% do Evento", Width = 110 });

            dgvResumoCaixas.DefaultCellStyle.ForeColor = Color.Black;
            dgvResumoCaixas.DefaultCellStyle.BackColor = Color.White;
            dgvResumoCaixas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResumoCaixas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvResumoCaixas.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvResumoCaixas.EnableHeadersVisualStyles = false;
        }

        private void AtualizarGridResumo(RelatorioVendaData dados)
        {
            dgvResumoCaixas.Rows.Clear();

            decimal totalEvento = dados.ValorTotalVendido;
            foreach (var caixa in dados.DadosPorCaixa.OrderBy(c => c.NumeroCaixa))
            {
                decimal percentual = totalEvento > 0 ? (caixa.ValorTotal / totalEvento) * 100 : 0;
                dgvResumoCaixas.Rows.Add(
                    caixa.NumeroCaixa,
                    caixa.NomeCaixa,
                    caixa.QuantidadeVendas,
                    $"R$ {caixa.ValorTotal:N2}",
                    $"R$ {caixa.ValorTroco:N2}",
                    $"{percentual:N1}%"
                );
            }
        }
    }
}
