using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.WinForms;
using LiveCharts.Wpf;
using GestorEvento.Models;
using GestorEvento.Services;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormRelatorioVenda : Form
    {
        private RelatorioVendaService _relatorioService;
        private EventoService _eventoService;
        private List<Evento> _eventosCompletos;

        public FormRelatorioVenda()
        {
            InitializeComponent();

            _relatorioService = new RelatorioVendaService();
            _eventoService = new EventoService();
            _eventosCompletos = new List<Evento>();

            // Configurar estilos
            EstiloManager.AplicarEstiloInfo(btnAtualizar);
            
            // Reduzir flickering
            this.DoubleBuffered = true;

            // Carregar eventos
            CarregarEventos();
        }

        private void CarregarEventos()
        {
            try
            {
                _eventosCompletos = _eventoService.GetAllEventos();
                
                // ComboBox inicia vazio - será preenchido conforme digita no TextBox
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
            // Filtrar eventos conforme digita no TextBox
            string textoBusca = txtBuscaEvento.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(textoBusca))
            {
                // Se vazio, limpar ComboBox
                cmbEventoResultados.DataSource = null;
                cmbEventoResultados.Refresh();
            }
            else
            {
                // Filtrar eventos que correspondem ao texto digitado
                var eventosFiltrados = _eventosCompletos
                    .Where(evento => evento.Nome.ToLower().Contains(textoBusca) || 
                                evento.DataEvento.ToString("dd/MM/yyyy").Contains(textoBusca))
                    .Select(evento => new
                    {
                        Id = evento.Id,
                        DisplayText = $"{evento.Nome} - {evento.DataEvento:dd/MM/yyyy}"
                    })
                    .ToList();

                // Atualizar ComboBox com resultados
                cmbEventoResultados.DataSource = eventosFiltrados;
                cmbEventoResultados.DisplayMember = "DisplayText";
                cmbEventoResultados.ValueMember = "Id";
                cmbEventoResultados.Refresh();
                cmbEventoResultados.Invalidate();
            }
        }

        private void CmbEventoResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Quando um evento é selecionado, carregar dados
            if (cmbEventoResultados.SelectedValue != null && cmbEventoResultados.SelectedValue is int idEvento && idEvento > 0)
            {
                CarregarDadosRelatorio(idEvento);
            }
        }

        private void CarregarDadosRelatorio(int idEvento)
        {
            try
            {
                // Limpar dados anteriores
                LimparCards();
                LimparGraficos();

                // Buscar dados
                var dadosRelatorio = _relatorioService.ObterDadosRelatorio(idEvento);

                // Atualizar cards
                AtualizarCards(dadosRelatorio);

                // Atualizar gráficos
                AtualizarGraficos(dadosRelatorio);
            }
            catch (Exception ex)
            {
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

                // Criar série de dados para o gráfico de colunas (duas séries lado a lado)
                var seriesCollection = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Valor Total (R$)",
                        Values = new ChartValues<decimal>(dados.DadosPorCaixa.Select(c => c.ValorTotal))
                    },
                    new ColumnSeries
                    {
                        Title = "Quantidade de Vendas",
                        Values = new ChartValues<int>(dados.DadosPorCaixa.Select(c => c.QuantidadeVendas))
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

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            // Recarregar eventos e limpar filtros
            CarregarEventos();
            txtBuscaEvento.Clear();
            cmbEventoResultados.DataSource = null;
            LimparCards();
            LimparGraficos();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}
