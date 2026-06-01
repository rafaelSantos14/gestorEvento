using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestorEvento.Components;
using GestorEvento.Models;
using GestorEvento.Services;
using GestorEvento.Utilities;
using LiveCharts;
using LiveCharts.Wpf;

namespace GestorEvento.Views
{
    public partial class FormRelatorioCortesia : Form
    {
        private readonly RelatorioVendaService _relatorioService;
        private readonly EventoService _eventoService;
        private List<Evento> _eventosCompletos;

        public FormRelatorioCortesia()
        {
            InitializeComponent();

            _relatorioService = new RelatorioVendaService();
            _eventoService = new EventoService();
            _eventosCompletos = new List<Evento>();

            EstiloManager.AplicarEstiloInfo(btnAtualizar);
            DoubleBuffered = true;
            ConfigurarGridProdutos();
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

            var eventosFiltrados = _eventosCompletos
                .Where(evento => textoBusca == "%"
                    || (evento.Nome ?? string.Empty).ToLower().Contains(textoBusca)
                    || evento.DataEvento.ToString("dd/MM/yyyy").Contains(textoBusca))
                .Select(evento => new
                {
                    Id = evento.Id,
                    DisplayText = $"{evento.Nome} - {evento.DataEvento:dd/MM/yyyy}"
                })
                .ToList();

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
                dgvProdutosCortesia.Rows.Clear();

                var dados = _relatorioService.ObterDadosRelatorioCortesia(idEvento);

                AtualizarCards(dados);
                AtualizarGraficoBarras(dados);
                AtualizarGridProdutos(dados);
            }
            catch (Exception ex)
            {
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
                    Values = new ChartValues<decimal>(dados.DadosPorCaixa.Select(c => c.ValorTotal))
                },
                new ColumnSeries
                {
                    Title = "Quantidade Cortesia",
                    Values = new ChartValues<int>(dados.DadosPorCaixa.Select(c => c.QuantidadeVendas))
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
            dgvProdutosCortesia.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantidade", HeaderText = "Qtd. Cortesia", Width = 95 });
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
                dgvProdutosCortesia.Rows.Add(
                    produto.NomeProduto,
                    produto.QuantidadeVendida,
                    produto.QuantidadeDisponivel,
                    $"R$ {produto.PrecoUnitario:N2}",
                    $"R$ {produto.ValorTotalVendido:N2}",
                    $"{produto.PercentualTotalVendas:N1}%"
                );
            }
        }

        private void BtnAtualizar_Click(object sender, EventArgs e)
        {
            CarregarEventos();
            txtBuscaEvento.Clear();
            cmbEventoResultados.DataSource = null;
            LimparCards();
            LimparGraficos();
            dgvProdutosCortesia.Rows.Clear();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }
    }
}
