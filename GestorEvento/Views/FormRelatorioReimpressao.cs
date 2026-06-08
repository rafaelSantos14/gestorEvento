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
using GestorEvento.Components;

namespace GestorEvento.Views
{
    public partial class FormRelatorioReimpressao : Form
    {
        private ReimpressaoService _reimpressaoService;
        private EventoService _eventoService;
        private ProdutoService _produtoService;
        private MotivoReimpressaoService _motivoService;
        private List<Evento> _eventosCompletos;
        private List<MotivoReimpressao> _motivosReimpressao;
        private string _statusFiltro = "Todos";

        public FormRelatorioReimpressao()
        {
            InitializeComponent();

            _reimpressaoService = new ReimpressaoService();
            _eventoService = new EventoService();
            _produtoService = new ProdutoService();
            _motivoService = new MotivoReimpressaoService();
            _eventosCompletos = new List<Evento>();
            _motivosReimpressao = new List<MotivoReimpressao>();

            DoubleBuffered = true;
            ConfigurarGrids();
            CarregarDados();
        }

        private void CarregarDados()
        {
            try
            {
                _eventosCompletos = _eventoService.GetAllEventos();
                _motivosReimpressao = _motivoService.GetMotivosAtivos();

                // ComboBox inicia vazio
                cmbEventoResultados.DataSource = null;

                // Inicializar ComboBox de status
                if (cmbStatusFiltro != null && cmbStatusFiltro.Items.Count == 0)
                {
                    cmbStatusFiltro.Items.AddRange(new[] { "Todos", "Ativo", "Encerrado" });
                    cmbStatusFiltro.SelectedItem = "Todos";
                    cmbStatusFiltro.SelectedIndexChanged += CmbStatusFiltro_SelectedIndexChanged;
                }

                txtBuscaEvento.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar dados: {ex.Message}", "Erro");
            }
        }

        private void TxtBuscaEvento_TextChanged(object sender, EventArgs e)
        {
            AtualizarComboEventos();
        }

        private void CmbEventoResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbEventoResultados.SelectedValue != null && cmbEventoResultados.SelectedValue is int idEvento && idEvento > 0)
            {
                CarregarDadosRelatorio(idEvento);
            }
        }

        private void CmbStatusFiltro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbStatusFiltro != null && cmbStatusFiltro.SelectedItem != null)
            {
                _statusFiltro = cmbStatusFiltro.SelectedItem.ToString();
                AtualizarComboEventos();
            }
        }

        private void AtualizarComboEventos()
        {
            string textoBusca = txtBuscaEvento.Text.ToLower().Trim();

            if (string.IsNullOrWhiteSpace(textoBusca))
            {
                cmbEventoResultados.DataSource = null;
                cmbEventoResultados.Refresh();
                return;
            }

            var eventosFiltrados = FiltrarEventosPorNomeEStatus(textoBusca);
            cmbEventoResultados.DataSource = eventosFiltrados;
            cmbEventoResultados.DisplayMember = "DisplayText";
            cmbEventoResultados.ValueMember = "Id";
        }

        private List<object> FiltrarEventosPorNomeEStatus(string textoBusca)
        {
            var eventosFiltrados = new List<object>();
            foreach (var evento in _eventosCompletos)
            {
                // Filtrar por status
                bool passouFiltroStatus = false;
                if (_statusFiltro == "Todos")
                {
                    passouFiltroStatus = true;
                }
                else if (_statusFiltro == "Ativo" && evento.CdStatus == "Ativo")
                {
                    passouFiltroStatus = true;
                }
                else if (_statusFiltro == "Encerrado" && evento.CdStatus == "Encerrado")
                {
                    passouFiltroStatus = true;
                }

                if (!passouFiltroStatus) continue;

                // Filtrar por nome/data
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
                        DisplayText = $"{evento.Nome} - {evento.DataEvento:dd/MM/yyyy} [{evento.CdStatus}]"
                    });
                }
            }

            return eventosFiltrados;
        }

        private void CarregarDadosRelatorio(int idEvento)
        {
            try
            {
                LimparCards();
                LimparGraficos();
                dgvItens.Rows.Clear();
                dgvPorProduto.Rows.Clear();

                var reimpressoes = _reimpressaoService.GetReimpressoesPorEvento(idEvento);

                AtualizarCards(reimpressoes);
                AtualizarGraficoMotivosPorQuantidade(reimpressoes);
                AtualizarGridItens(reimpressoes);
                AtualizarGridPorProduto(reimpressoes);
            }
            catch (Exception ex)
            {
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
                lblTotalReimpressoes.Text = totalReimpressoes.ToString();

                // Card 2: Valor Total
                decimal valorTotal = reimpressoes.Sum(r => r.VlTotal);
                lblValorTotal.Text = $"R$ {valorTotal:N2}";

                // Card 3: Motivo Mais Comum
                if (reimpressoes.Count > 0)
                {
                    var motivoMaisComum = reimpressoes
                        .GroupBy(r => r.IdMotivo)
                        .OrderByDescending(g => g.SelectMany(r => r.Itens).Sum(i => i.QtdeReimpressao))
                        .FirstOrDefault();

                    if (motivoMaisComum != null)
                    {
                        var descricaoMotivo = ObterDescricaoMotivo(motivoMaisComum.Key);
                        var quantidadeTotal = motivoMaisComum.SelectMany(r => r.Itens).Sum(i => i.QtdeReimpressao);
                        //lblMotivoMaisComum.Text = $"{quantidadeTotal}x - {descricaoMotivo}";
                    }
                }
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
            lblTotalReimpressoes.Text = "0";
            lblValorTotal.Text = "R$ 0,00";
            //lblMotivoMaisComum.Text = "-";
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
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdReimpressao", HeaderText = "ID Rei.", Width = 80 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "IdReimpressaoItem", HeaderText = "ID Item Rei.", Width = 90 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "DtReimpressao", HeaderText = "Data/Hora", Width = 130 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "DsMotivo", HeaderText = "Descrição Motivo", Width = 250 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "Quantidade", HeaderText = "Qt", Width = 60 });
            dgvItens.Columns.Add(new DataGridViewTextBoxColumn { Name = "Valor", HeaderText = "Valor", Width = 100 });

            EstiloGridPadrao(dgvItens);
        }

        private void ConfigurarGridPorProduto()
        {
            dgvPorProduto.AutoGenerateColumns = false;
            dgvPorProduto.AllowUserToAddRows = false;
            dgvPorProduto.ReadOnly = true;
            dgvPorProduto.RowHeadersVisible = false;
            dgvPorProduto.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            dgvPorProduto.Columns.Clear();
            dgvPorProduto.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome do Produto", Width = 300 });
            dgvPorProduto.Columns.Add(new DataGridViewTextBoxColumn { Name = "QtdeTotal", HeaderText = "Qtde Total", Width = 100 });
            dgvPorProduto.Columns.Add(new DataGridViewTextBoxColumn { Name = "ValorTotal", HeaderText = "Valor Total", Width = 120 });
            dgvPorProduto.Columns.Add(new DataGridViewTextBoxColumn { Name = "Percentual", HeaderText = "% do Total", Width = 100 });

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

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }
    }
}

