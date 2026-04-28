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

        // Componentes UI - Panel Título
        private Panel panelTitulo;
        private Button btnFechar;
        private Button btnMinimizar;
        private Label lblTitulo;

        // Componentes UI - Conteúdo
        private Label lblTituloResumo;
        private Label lblAbertura;
        private Label lblTotalDinheiro;
        private Label lblTotalEsperado;
        private Label lblTotalVendas;
        private Label lblDiferenca;
        
        private DataGridView dgvFormasPagamento;
        
        private Label lblObservacoes;
        private TextBox txtObservacoes;
        private TextBox txtValorContado;
        private Label lblTotalTroco;

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

                // Configurar título da janela
                this.Text = $"Fechamento de Caixa #{_resumoFechamento.NoPontoVenda} - {_resumoFechamento.NomePontoVenda}";

                // Carregar UI dinâmico (criar componentes)
                CriarComponentes();

                // Preencher dados
                PreencherResumoExecutivo();
                PreencherTabelaFormasPagamento();

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
            int alturaPadrao = 700;
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

        private void CriarComponentes()
        {
            // Panel Título
            panelTitulo = new Panel
            {
                BackColor = Color.FromArgb(25, 118, 210),
                Dock = DockStyle.Top,
                Height = 40
            };

            lblTitulo = new Label
            {
                Text = $"Fechamento de Caixa #{_resumoFechamento.NoPontoVenda} - {_resumoFechamento.NomePontoVenda}",
                Location = new Point(10, 10),
                AutoSize = true,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold),
                ForeColor = Color.White
            };

            btnMinimizar = new Button
            {
                Text = "−",
                Dock = DockStyle.Right,
                Width = 45,
                Height = 40,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnMinimizar.FlatAppearance.BorderSize = 0;
            btnMinimizar.Click += BtnMinimizar_Click;

            btnFechar = new Button
            {
                Text = "✕",
                Dock = DockStyle.Right,
                Width = 45,
                Height = 40,
                BackColor = Color.Transparent,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 12F, FontStyle.Bold)
            };
            btnFechar.FlatAppearance.BorderSize = 0;
            btnFechar.Click += BtnFecharTitulo_Click;

            panelTitulo.Controls.AddRange(new Control[] { lblTitulo, btnMinimizar, btnFechar });
            this.Controls.Add(panelTitulo);

            // Panel para Resumo Executivo
            GroupBox gbResumo = new GroupBox
            {
                Text = "RESUMO EXECUTIVO",
                Location = new Point(20, 60),
                Size = new Size(this.ClientSize.Width - 40, 190),
                Font = new Font("Segoe UI", 10F)
            };

            lblTituloResumo = new Label { Location = new Point(10, 30), AutoSize = true };
            lblAbertura = new Label { Location = new Point(10, 60), AutoSize = true, Font = new Font("Consolas", 10F) };
            lblTotalDinheiro = new Label { Location = new Point(10, 78), AutoSize = true, Font = new Font("Consolas", 10F) };
            lblTotalTroco = new Label { Location = new Point(10, 96), AutoSize = true, Font = new Font("Consolas", 10F) };
            lblTotalEsperado = new Label { Location = new Point(10, 114), AutoSize = true, Font = new Font("Consolas", 10F) };
            lblTotalVendas = new Label { Location = new Point(10, 132), AutoSize = true, Font = new Font("Consolas", 10F) };

            gbResumo.Controls.AddRange(new Control[] { lblTituloResumo, lblAbertura, lblTotalDinheiro, lblTotalTroco, lblTotalEsperado, lblTotalVendas });
            this.Controls.Add(gbResumo);

            // Panel para Formas de Pagamento
            GroupBox gbFormas = new GroupBox
            {
                Text = "RESUMO POR FORMA DE PAGAMENTO",
                Location = new Point(20, 260),
                Size = new Size(this.ClientSize.Width - 40, 180),
                Font = new Font("Segoe UI", 10F)
            };

            dgvFormasPagamento = new DataGridView
            {
                Location = new Point(10, 25),
                Size = new Size(gbFormas.ClientSize.Width - 20, 140),
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false
            };
            dgvFormasPagamento.Columns.Add("FormaVisual", "Forma de Pagamento");
            dgvFormasPagamento.Columns.Add("TotalVisual", "Valor Total");

            gbFormas.Controls.Add(dgvFormasPagamento);
            this.Controls.Add(gbFormas);

            // Panel para entrada de Valor Contado
            GroupBox gbContagem = new GroupBox
            {
                Text = "FECHAMENTO",
                Location = new Point(20, 450),
                Size = new Size(this.ClientSize.Width - 40, 200),
                Font = new Font("Segoe UI", 10F)
            };

            Label lblValorContadoLabel = new Label
            {
                Text = "Valor Contado em Mão:",
                Location = new Point(10, 30),
                AutoSize = true
            };

            txtValorContado = new TextBox
            {
                Location = new Point(10, 55),
                Size = new Size(200, 40),
                Font = new Font("Segoe UI", 14F),
                TextAlign = HorizontalAlignment.Right
            };
            txtValorContado.TextChanged += TxtValorContado_TextChanged;

            lblDiferenca = new Label
            {
                Text = "DIFERENÇA: R$ 0,00",
                Location = new Point(220, 55),
                Size = new Size(300, 40),
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                ForeColor = Color.Green
            };

            lblObservacoes = new Label
            {
                Text = "Observações:",
                Location = new Point(10, 105),
                AutoSize = true
            };

            txtObservacoes = new TextBox
            {
                Location = new Point(10, 125),
                Size = new Size(gbContagem.ClientSize.Width - 20, 50),
                Multiline = true
            };

            gbContagem.Controls.AddRange(new Control[] { lblValorContadoLabel, txtValorContado, lblDiferenca, lblObservacoes, txtObservacoes });
            this.Controls.Add(gbContagem);

            // Resize form antes de criar botões (para calcular posição correta)
            this.ClientSize = new Size(Math.Max(850, this.ClientSize.Width), 700);

            // Botões
            Button btnFecharCaixa = new Button
            {
                Text = "FECHAR CAIXA",
                Location = new Point(20, this.ClientSize.Height - 45),
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 11F, FontStyle.Bold),
                BackColor = Color.FromArgb(76, 175, 80),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnFecharCaixa.Click += BtnFecharCaixa_Click;

            Button btnCancelar = new Button
            {
                Text = "CANCELAR",
                Location = new Point(180, this.ClientSize.Height - 45),
                Size = new Size(150, 35),
                Font = new Font("Segoe UI", 11F),
                BackColor = Color.FromArgb(244, 67, 54),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat
            };
            btnCancelar.Click += BtnCancelar_Click;

            this.Controls.AddRange(new Control[] { btnFecharCaixa, btnCancelar });
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
            lblTotalTroco.Text = AlinharComPontos("Total Troco", $"R$ {totalTroco:F2}", comprimentoBase);
            
            lblTotalEsperado.Text = AlinharComPontos("TOTAL ESPERADO", $"R$ {_resumoFechamento.TotalEsperado:F2}", comprimentoBase);
            lblTotalVendas.Text = AlinharComPontos("Total de Vendas", $"{_resumoFechamento.Vendas.Count}", comprimentoBase);
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
