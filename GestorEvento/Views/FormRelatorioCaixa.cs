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
    public partial class FormRelatorioCaixa : Form
    {
        private readonly RelatorioVendaService _relatorioService;
        private readonly EventoService _eventoService;
        private List<Evento> _eventosCompletos;

        public FormRelatorioCaixa()
        {
            InitializeComponent();

            _relatorioService = new RelatorioVendaService();
            _eventoService = new EventoService();
            _eventosCompletos = new List<Evento>();

            EstiloManager.AplicarEstiloInfo(btnAtualizar);
            DoubleBuffered = true;

            ConfigurarGridResumo();
            CarregarEventos();
        }

        private void CarregarEventos()
        {
            try
            {
                _eventosCompletos = _eventoService.GetAllEventos();
                cmbEventoResultados.DataSource = null;
                txtBuscaEvento.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar eventos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtBuscaEvento_TextChanged(object sender, EventArgs e)
        {
            string textoBusca = txtBuscaEvento.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(textoBusca))
            {
                cmbEventoResultados.DataSource = null;
                cmbEventoResultados.Refresh();
                return;
            }

            var eventosFiltrados = new List<object>();
            foreach (var evento in _eventosCompletos)
            {
                bool correspondeBusca = false;

                if (textoBusca == "%")
                {
                    correspondeBusca = true;
                }
                else
                {
                    string nomeEvento = evento.Nome ?? string.Empty;
                    string dataEvento = evento.DataEvento.ToString("dd/MM/yyyy");
                    correspondeBusca = nomeEvento.ToLower().Contains(textoBusca) || dataEvento.Contains(textoBusca);
                }

                if (correspondeBusca)
                {
                    eventosFiltrados.Add(new
                    {
                        Id = evento.Id,
                        DisplayText = $"{evento.Nome} - {evento.DataEvento:dd/MM/yyyy}"
                    });
                }
            }

            cmbEventoResultados.DataSource = eventosFiltrados;
            cmbEventoResultados.DisplayMember = "DisplayText";
            cmbEventoResultados.ValueMember = "Id";
            cmbEventoResultados.Refresh();
            cmbEventoResultados.Invalidate();
        }

        private void CmbEventoResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEventoResultados.SelectedValue != null && cmbEventoResultados.SelectedValue is int idEvento && idEvento > 0)
            {
                CarregarDadosRelatorio(idEvento);
            }
        }

        private void CarregarDadosRelatorio(int idEvento)
        {
            try
            {
                LimparCards();
                LimparGraficos();
                dgvResumoCaixas.Rows.Clear();

                var dadosRelatorio = _relatorioService.ObterDadosRelatorio(idEvento);

                AtualizarCards(dadosRelatorio);
                AtualizarGraficos(dadosRelatorio);
                AtualizarGridResumo(dadosRelatorio);
            }
            catch (Exception ex)
            {
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
                        Labels = dados.DadosPorCaixa.Select(c => $"Caixa #{c.NumeroCaixa}").ToList(),
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao atualizar gráfico de pizza: {ex.Message}", "Erro");
            }
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

        private void LimparGraficos()
        {
            chartBarras.Series.Clear();
            chartPizza.Series.Clear();
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarEventos();
            txtBuscaEvento.Clear();
            cmbEventoResultados.DataSource = null;
            LimparCards();
            LimparGraficos();
            dgvResumoCaixas.Rows.Clear();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
