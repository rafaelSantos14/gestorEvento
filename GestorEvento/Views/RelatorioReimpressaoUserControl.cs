using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using LiveCharts;
using LiveCharts.Wpf;
using GestorEvento.Models;
using GestorEvento.Services;

namespace GestorEvento.Views
{
    public partial class RelatorioReimpressaoUserControl : UserControl
    {
        private readonly ReimpressaoService _reimpressaoService;
        private readonly MotivoReimpressaoService _motivoService;
        private List<MotivoReimpressao> _motivosReimpressao;

        public RelatorioReimpressaoUserControl()
        {
            InitializeComponent();
            _reimpressaoService = new ReimpressaoService();
            _motivoService = new MotivoReimpressaoService();
            _motivosReimpressao = new List<MotivoReimpressao>();
            
            DoubleBuffered = true;
            CarregarMotivos();
            ConfigurarGrids();
        }

        private void CarregarMotivos()
        {
            try
            {
                _motivosReimpressao = _motivoService.GetMotivosAtivos();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar motivos: {ex.Message}");
            }
        }

        /// <summary>
        /// Método público para carregar dados do relatório de reimpressão
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
                chartMotivos.BackColor = Color.White;

                LimparCards();
                LimparGraficos();
                dgvItens.Rows.Clear();
                dgvPorProduto.Rows.Clear();

                var reimpressoes = _reimpressaoService.GetReimpressoesPorEvento(idEvento);

                AtualizarCards(reimpressoes);
                AtualizarGraficoMotivosPorQuantidade(reimpressoes);
                AtualizarGridItens(reimpressoes);
                AtualizarGridPorProduto(reimpressoes);

                // Mostrar novamente com fundo correto
                this.Visible = true;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                this.Visible = true;
                MessageBox.Show($"Erro ao carregar relatório: {ex.Message}", "Erro");
            }
        }

        private void AtualizarCards(List<Reimpressao> reimpressoes)
        {
            try
            {
                // Card 1: Total de Impressões (soma da quantidade de itens)
                int totalReimpressoes = reimpressoes
                    .SelectMany(r => r.Itens)
                    .Sum(i => i.QtdeReimpressao);
                lblTotalReimpressoesValor.Text = totalReimpressoes.ToString();

                // Card 2: Valor Total
                decimal valorTotal = reimpressoes.Sum(r => r.VlTotal);
                lblValorTotalValor.Text = $"R$ {valorTotal:N2}";
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar cards: {ex.Message}");
            }
        }

        private void AtualizarGraficoMotivosPorQuantidade(List<Reimpressao> reimpressoes)
        {
            try
            {
                if (reimpressoes.Count == 0)
                {
                    LimparGraficos();
                    return;
                }

                var dadosPorMotivo = reimpressoes
                    .GroupBy(r => r.IdMotivo)
                    .Select(g => new
                    {
                        IdMotivo = g.Key,
                        DsMotivo = ObterDescricaoMotivo(g.Key),
                        Quantidade = g.SelectMany(r => r.Itens).Sum(i => i.QtdeReimpressao)
                    })
                    .OrderByDescending(x => x.Quantidade)
                    .ToList();

                chartMotivos.Series = new SeriesCollection
                {
                    new ColumnSeries
                    {
                        Title = "Quantidade de Reimpressões",
                        Values = new ChartValues<int>(dadosPorMotivo.Select(d => d.Quantidade))
                    }
                };

                chartMotivos.AxisX = new AxesCollection
                {
                    new Axis
                    {
                        Title = "Motivos",
                        Labels = dadosPorMotivo.Select(d => d.DsMotivo).ToList()
                    }
                };

                chartMotivos.AxisY = new AxesCollection
                {
                    new Axis
                    {
                        Title = "Quantidade"
                    }
                };

                chartMotivos.BackColor = Color.White;
                
                // Forçar redraw do gráfico
                Application.DoEvents();
                chartMotivos.Invalidate();
                chartMotivos.Refresh();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar gráfico: {ex.Message}");
            }
        }

        private void AtualizarGridItens(List<Reimpressao> reimpressoes)
        {
            try
            {
                dgvItens.Rows.Clear();

                // Mostrar cada ITEM separadamente
                foreach (var reimp in reimpressoes.OrderByDescending(r => r.DtReimpressao))
                {
                    foreach (var item in reimp.Itens)
                    {
                        dgvItens.Rows.Add(
                            reimp.IdReimpressao,
                            item.IdReimpressaoItem,
                            reimp.DtReimpressao.ToString("dd/MM/yyyy HH:mm"),
                            item.DescricaoProduto,
                            ObterDescricaoMotivo(reimp.IdMotivo),
                            item.QtdeReimpressao,
                            $"R$ {item.VlSubtotal:N2}"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar grid itens: {ex.Message}");
            }
        }

        private void AtualizarGridPorProduto(List<Reimpressao> reimpressoes)
        {
            try
            {
                dgvPorProduto.Rows.Clear();

                // Agrupar por ID_PRODUTO_EVENTO
                var produtoAgrupado = reimpressoes
                    .SelectMany(r => r.Itens)
                    .GroupBy(i => i.IdProdutoEvento)
                    .Select(g => new
                    {
                        IdProdutoEvento = g.Key,
                        NomeProduto = g.First().DescricaoProduto,
                        QtdeTotal = g.Sum(i => i.QtdeReimpressao),
                        ValorTotal = g.Sum(i => i.VlSubtotal)
                    })
                    .OrderByDescending(x => x.ValorTotal)
                    .ToList();

                // Calcular total geral
                decimal totalGeral = produtoAgrupado.Sum(p => p.ValorTotal);

                foreach (var prod in produtoAgrupado)
                {
                    decimal percentual = totalGeral > 0 ? (prod.ValorTotal / totalGeral) * 100 : 0;

                    dgvPorProduto.Rows.Add(
                        prod.NomeProduto,
                        prod.QtdeTotal,
                        $"R$ {prod.ValorTotal:N2}",
                        $"{percentual:F2}%"
                    );
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar grid por produto: {ex.Message}");
            }
        }

        private string ObterDescricaoMotivo(int idMotivo)
        {
            var motivo = _motivosReimpressao.FirstOrDefault(m => m.IdMotivo == idMotivo);
            return motivo?.DsMotivo ?? $"Motivo {idMotivo}";
        }

        private void LimparCards()
        {
            lblTotalReimpressoesValor.Text = "-";
            lblValorTotalValor.Text = "-";
        }

        private void LimparGraficos()
        {
            chartMotivos.Series = new SeriesCollection();
            chartMotivos.AxisX = new AxesCollection();
            chartMotivos.AxisY = new AxesCollection();
        }

        private void ConfigurarGrids()
        {
            ConfigurarGridItens();
            ConfigurarGridPorProduto();
        }

        private void ConfigurarGridItens()
        {
            dgvItens.AutoGenerateColumns = false;
            dgvItens.AllowUserToAddRows = false;
            dgvItens.ReadOnly = true;
            dgvItens.RowHeadersVisible = false;
            dgvItens.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvItens.Columns.Clear();
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdReimpressao", HeaderText = "ID Rei.", FillWeight = 10 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdReimpressaoItem", HeaderText = "ID Item", FillWeight = 10 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "DtReimpressao", HeaderText = "Data/Hora", FillWeight = 15 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "NomeProduto", HeaderText = "Produto", FillWeight = 30 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "DsMotivo", HeaderText = "Motivo", FillWeight = 20 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantidade", HeaderText = "Qt", FillWeight = 8 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "Valor", HeaderText = "Valor", FillWeight = 7 });

            dgvItens.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            EstiloGridPadrao(dgvItens);
        }

        private void ConfigurarGridPorProduto()
        {
            dgvPorProduto.AutoGenerateColumns = false;
            dgvPorProduto.AllowUserToAddRows = false;
            dgvPorProduto.ReadOnly = true;
            dgvPorProduto.RowHeadersVisible = false;
            dgvPorProduto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvPorProduto.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            dgvPorProduto.Columns.Clear();
            dgvPorProduto.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome" });
            dgvPorProduto.Columns.Add(new DataGridViewTextBoxColumn { Name = "QtdeTotal", HeaderText = "Qtd" });
            dgvPorProduto.Columns.Add(new DataGridViewTextBoxColumn { Name = "ValorTotal", HeaderText = "Valor Total" });
            dgvPorProduto.Columns.Add(new DataGridViewTextBoxColumn { Name = "Percentual", HeaderText = "%" });

            EstiloGridPadrao(dgvPorProduto);
        }

        private void EstiloGridPadrao(DataGridView dgv)
        {
            dgv.DefaultCellStyle.BackColor = Color.White;
            dgv.DefaultCellStyle.ForeColor = Color.Black;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgv.EnableHeadersVisualStyles = false;
        }
    }
}
