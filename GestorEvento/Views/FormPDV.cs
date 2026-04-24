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
    public partial class FormPDV : Form
    {
        private int _caixaIdSelecionado = 0;
        private int _eventoIdSelecionado = 0;
        private decimal _totalVenda = 0m;
        private List<VendaItem> _itensVenda = new List<VendaItem>();
        private List<ProdutoLinhaVenda> _produtosLinhas = new List<ProdutoLinhaVenda>();
        private List<FormaPagamentoInput> _formasPagamento = new List<FormaPagamentoInput>();
        private bool _isDragging = false;
        private Point _dragPoint;
        private VendaService _vendaService;
        private ProdutoEventoService _produtoEventoService;
        private ProdutoService _produtoService;
        private PontoVendaService _pontoVendaService;
        private FormaPagamentoService _formaPagamentoService;
        private RecebimentoService _recebimentoService;

        public FormPDV(int caixaId)
        {
            InitializeComponent();
            _caixaIdSelecionado = caixaId;
            _vendaService = new VendaService();
            _produtoEventoService = new ProdutoEventoService();
            _produtoService = new ProdutoService();
            _pontoVendaService = new PontoVendaService();
            _formaPagamentoService = new FormaPagamentoService();
            _recebimentoService = new RecebimentoService();
        }

        private void FormPDV_Load(object sender, EventArgs e)
        {
            try
            {
                // Buscar número do caixa e exibir
                var pontoVenda = _pontoVendaService.GetPontoVendaById(_caixaIdSelecionado);
                if (pontoVenda != null)
                {
                    int noCaixa = pontoVenda.NoPontoVenda;
                    _eventoIdSelecionado = pontoVenda.IdEvento;
                    
                    // Concatenar número com descrição se houver
                    string textoDescricao = string.IsNullOrWhiteSpace(pontoVenda.DsPontoVenda) ? 
                        "" : $" - {pontoVenda.DsPontoVenda}";
                    lblInfoCaixa.Text = $"Caixa: {noCaixa}{textoDescricao}";
                    
                    // Ajustar layout responsivo para maximized
                    AjustarLayoutPaineis();
                    
                    // Carregar produtos e formas de pagamento
                    CarregarProdutos();
                    CarregarFormasPagamento();
                }
                else
                {
                    MessageBox.Show("Caixa não encontrado!");
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar forma: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                this.Close();
            }
        }

        // Ajustar largura dos painéis para layout responsivo quando maximizado
        private void AjustarLayoutPaineis()
        {
            // Calcular largura disponível (panelConteudo.Width)
            int larguraDisponivel = panelConteudo.Width;
            
            // Distribuir: Produtos (60%) | Pagamento (20%) | Totalizacao (20%)
            int larguraProdutos = (int)(larguraDisponivel * 0.60);
            int larguraPagamento = (int)(larguraDisponivel * 0.20);
            
            // Definir largura dos painéis de esquerda
            panelProdutos.Width = larguraProdutos;
            panelPagamento.Width = larguraPagamento;
            
            // panelTotalizacao preencherá o resto automaticamente (Dock=Fill)
        }

        private void CarregarProdutos()
        {
            panelProdutos.Controls.Clear();
            _produtosLinhas.Clear();
            
            try
            {
                // Obter produtos vinculados ao evento
                var produtosEvento = _produtoEventoService.GetProdutosVinculados(_eventoIdSelecionado);
                
                if (produtosEvento.Count == 0)
                {
                    Label lblNenhum = new Label
                    {
                        Text = "Nenhum produto disponível",
                        AutoSize = true,
                        ForeColor = Color.Gray,
                        Font = new Font("Segoe UI", 10F)
                    };
                    panelProdutos.Controls.Add(lblNenhum);
                    return;
                }

                // Calcular layout dinâmico baseado na altura do painel
                int alturaDisponivel = panelProdutos.Height - 20; // -20 para padding
                int alturaItem = 85; // Altura de cada produto
                int produtosPorColuna = Math.Max(1, alturaDisponivel / alturaItem);
                
                int xPosition = 10;
                int yPosition = 10;
                int produtoAtual = 0;
                
                foreach (var produtoEvento in produtosEvento)
                {
                    // Buscar produto para pegar valor padrão
                    var produto = _produtoService.GetProductById(produtoEvento.IdProduto);
                    if (produto != null)
                    {
                        // Se atingiu o limite de produtos na coluna, passar para próxima coluna
                        if (produtoAtual > 0 && produtoAtual % produtosPorColuna == 0)
                        {
                            xPosition += 400; // Largura de uma coluna (aumentado para mais espaço entre colunas)
                            yPosition = 10;
                        }
                        
                        // Criar linha de produto com Label + TextBox Qtde + TextBox Valor
                        var produtoLinha = new ProdutoLinhaVenda(
                            produtoEvento.Id,  // IdProdutoEvento (necessário para reduzir estoque)
                            produtoEvento.IdProduto,
                            produto.Nome,
                            produtoEvento.Preco,
                            produtoEvento.QuantidadeDisponivel,  // Quantidade disponível para venda
                            xPosition,
                            yPosition,
                            this
                        );
                        
                        produtoLinha.AddToPanel(panelProdutos);
                        _produtosLinhas.Add(produtoLinha);
                        
                        // Incrementar posição vertical
                        yPosition += 85; // Mesmo valor de alturaItem
                        produtoAtual++;
                    }
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar produtos: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
            }
        }

        private void CarregarFormasPagamento()
        {
            panelPagamento.Controls.Clear();
            _formasPagamento.Clear();
            
            try
            {
                var formas = _formaPagamentoService.GetAllFormasPagamento();
                
                if (formas.Count == 0)
                {
                    Label lblNenhum = new Label
                    {
                        Text = "Nenhuma forma de pagamento",
                        AutoSize = true,
                        ForeColor = Color.Gray,
                        Font = new Font("Segoe UI", 10F)
                    };
                    panelPagamento.Controls.Add(lblNenhum);
                    return;
                }

                // Criar linha para cada forma de pagamento (vertical, em lista)
                int yPosition = 10;
                
                foreach (var forma in formas)
                {
                    var formaPagamento = new FormaPagamentoInput(
                        forma.Id,
                        forma.NmFormaPagamento,
                        yPosition,
                        this
                    );
                    
                    formaPagamento.AddToPanel(panelPagamento);
                    _formasPagamento.Add(formaPagamento);
                    
                    yPosition += 130;  // Aumentado de 100 para acomodar tamanho maior
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar formas de pagamento: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
            }
        }

        // MÉTODO PÚBLICO CHAMADO PELOS EVENTOS LEAVE DAS LINHAS DE PRODUTOS
        public void AtualizarTotalVenda()
        {
            _totalVenda = 0m;
            
            // Somar subtotais de todos os produtos com quantidade > 0
            foreach (var linha in _produtosLinhas)
            {
                int qtde = linha.GetQuantidade();
                decimal valor = linha.GetValor();
                
                if (qtde > 0)
                {
                    _totalVenda += qtde * valor;
                }
            }
            
            lblTotalValor.Text = $"R$ {_totalVenda.ToString("F2")}";
            
            // Atualizar troco se tiver dinheiro informado
            AtualizarTroco();
        }

        // MÉTODO PÚBLICO CHAMADO PELOS EVENTOS LEAVE DAS FORMAS DE PAGAMENTO
        public void AtualizarTroco()
        {
            try
            {
                // Somar TODAS as formas de pagamento (não apenas dinheiro)
                decimal somaTodasFormas = _formasPagamento.Sum(f => f.GetValor());
                
                // Se não houver nenhum pagamento informado, mostrar 0
                if (somaTodasFormas == 0)
                {
                    lblTrocoValor.Text = "R$ 0,00";
                    lblTrocoValor.ForeColor = Color.FromArgb(76, 175, 80); // Verde
                    return;
                }
                
                // Calcular troco: diferença entre pagamento total e valor da venda
                decimal troco = somaTodasFormas - _totalVenda;
                
                lblTrocoValor.Text = $"R$ {troco.ToString("F2")}";
                
                // Colorir conforme o valor do troco
                if (troco < 0)
                {
                    // Vermelho: falta pagar
                    lblTrocoValor.ForeColor = Color.FromArgb(244, 67, 54); // Vermelho
                }
                else
                {
                    // Verde: pagamento suficiente ou excesso
                    lblTrocoValor.ForeColor = Color.FromArgb(76, 175, 80); // Verde
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao atualizar troco: {ex.Message}");
            }
        }

        private void BtnConfirmarVenda_Click(object sender, EventArgs e)
        {
            try
            {
                // Validação 1: Existe pelo menos um produto com qtde > 0?
                var produtosComQtde = _produtosLinhas.Where(p => p.GetQuantidade() > 0).ToList();
                if (produtosComQtde.Count == 0)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        "Por favor, adicione pelo menos um produto com quantidade maior que zero",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                // Validação 2: A soma dos pagamentos é >= total da venda?
                decimal somaPagementos = _formasPagamento.Sum(f => f.GetValor());
                if (somaPagementos < _totalVenda)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        $"Soma dos pagamentos (R$ {somaPagementos.ToString("F2")}) é menor que o total (R$ {_totalVenda.ToString("F2")})",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                // Criar venda com itens que têm quantidade > 0
                var venda = new Venda(_caixaIdSelecionado);
                
                foreach (var linha in _produtosLinhas)
                {
                    int qtde = linha.GetQuantidade();
                    if (qtde > 0)
                    {
                        decimal valor = linha.GetValor();
                        
                        venda.AdicionarItem(new ItemVenda(
                            linha.IdProduto,
                            linha.NomeProduto,
                            qtde,
                            valor
                        ));
                    }
                }

                // Registrar venda no banco de dados
                int idVenda = _vendaService.RegistrarVenda(venda);

                // Registrar quantidade vendida no PRODUTO_EVENTO
                // (reduz a quantidade disponível para futuras vendas)
                foreach (var linha in _produtosLinhas)
                {
                    int qtde = linha.GetQuantidade();
                    if (qtde > 0)
                    {
                        try
                        {
                            _produtoEventoService.RegistrarVendaProduto(linha.IdProdutoEvento, qtde);
                        }
                        catch (Exception exEstoque)
                        {
                            System.Diagnostics.Debug.WriteLine($"Aviso ao registrar estoque: {exEstoque.Message}");
                        }
                    }
                }

                // Registrar recebimentos (formas de pagamento com valor > 0)
                foreach (var forma in _formasPagamento)
                {
                    decimal valor = forma.GetValor();
                    if (valor > 0)
                    {
                        try
                        {
                            _recebimentoService.RegistrarRecebimento(idVenda, forma.IdFormaPagamento, valor);
                        }
                        catch (Exception exRecebimento)
                        {
                            System.Diagnostics.Debug.WriteLine($"Aviso ao registrar recebimento: {exRecebimento.Message}");
                        }
                    }
                }

                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    $"Venda #{idVenda} confirmada!\nTotal: R$ {_totalVenda.ToString("F2")}",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();

                // Limpar tela para próxima venda
                LimparTudo();
                
                // Recarregar produtos com quantidades atualizadas do banco
                CarregarProdutos();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao confirmar venda: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            // Limpar tudo sem fechar o form
            LimparTudo();
        }

        private void LimparTudo()
        {
            // Limpar itens da venda
            _itensVenda.Clear();
            
            // Desmarcar e limpar todos os produtos
            foreach (var linha in _produtosLinhas)
            {
                linha.Limpar();
            }
            
            // Limpar todas as formas de pagamento
            foreach (var forma in _formasPagamento)
            {
                forma.Limpar();
            }
            
            // Atualizar totalizações
            AtualizarTotalVenda();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void PanelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.WindowState != FormWindowState.Maximized)
            {
                _isDragging = true;
                _dragPoint = e.Location;
            }
        }

        private void PanelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point novaLocacao = this.Location;
                novaLocacao.X += e.X - _dragPoint.X;
                novaLocacao.Y += e.Y - _dragPoint.Y;
                this.Location = novaLocacao;
            }
        }

        private void PanelTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        // Override para recalcular layout quando janela for redimensionada
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            AjustarLayoutPaineis();
        }

        // ==================== CLASSES INTERNAS ====================

        // Classe que representa uma linha de produto (Label + TextBox Qtde + TextBox Valor)
        private class ProdutoLinhaVenda
        {
            public int IdProdutoEvento { get; set; }  // ID da vinculação produto-evento
            public int IdProduto { get; set; }
            public string NomeProduto { get; set; }
            private decimal _valorPadrao;
            private int _quantidadeDisponivel;  // Quantidade que pode ser vendida
            private Label _lblProduto;
            private TextBox _txtQuantidade;
            private FormPDV _formParent;

            public ProdutoLinhaVenda(int idProdutoEvento, int idProduto, string nomeProduto, decimal valorPadrao, int quantidadeDisponivel, int xPosition, int yPosition, FormPDV formParent)
            {
                IdProdutoEvento = idProdutoEvento;
                IdProduto = idProduto;
                NomeProduto = nomeProduto;
                _valorPadrao = valorPadrao;
                _quantidadeDisponivel = quantidadeDisponivel;
                _formParent = formParent;
                
                // Criar label com nome do produto, valor e quantidade disponível - NO TOPO
                _lblProduto = new Label
                {
                    Text = $"{nomeProduto} - R$ {valorPadrao.ToString("F2")} - Disp. ({quantidadeDisponivel})",
                    Location = new Point(xPosition, yPosition),
                    Size = new Size(340, 30),
                    Font = new Font("Segoe UI", 12F),
                    AutoSize = false,
                    TextAlign = ContentAlignment.TopLeft
                };

                // Criar TextBox Quantidade (vazio, habilitado) - EMBAIXO
                _txtQuantidade = new TextBox
                {
                    Location = new Point(xPosition, yPosition + 35),
                    Size = new Size(340, 35),
                    Enabled = true,
                    Font = new Font("Segoe UI", 12F),
                    Text = "",
                };
                _txtQuantidade.Leave += TxtQuantidade_Leave;
                _txtQuantidade.TextChanged += TxtQuantidade_TextChanged;
            }

            private void TxtQuantidade_Leave(object sender, EventArgs e)
            {
                // Validar se é um número válido
                if (int.TryParse(_txtQuantidade.Text, out int qtde))
                {
                    if (qtde < 0)
                    {
                        _txtQuantidade.Text = "0";
                    }
                    // Validar contra estoque disponível
                    else if (qtde > _quantidadeDisponivel)
                    {
                        DialogoCustomizado dialogo = new DialogoCustomizado(
                            "Aviso",
                            $"Quantidade indisponível!\nDisponível: {_quantidadeDisponivel}\nSolicitado: {qtde}",
                            TipoDialogo.Aviso,
                            TipoButton.Ok
                        );
                        dialogo.ShowDialog();
                        _txtQuantidade.Text = "";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(_txtQuantidade.Text))
                {
                    _txtQuantidade.Text = "0";
                }
                
                // Chamar método do form parent para atualizar total
                _formParent.AtualizarTotalVenda();
            }

            private void TxtQuantidade_TextChanged(object sender, EventArgs e)
            {
                // Remove caracteres não numéricos - apenas dígitos
                string texto = new string(_txtQuantidade.Text.Where(c => char.IsDigit(c)).ToArray());

                // Se vazio, deixa vazio (sem mostrar "0")
                if (string.IsNullOrEmpty(texto))
                {
                    texto = "";
                }

                // Atualiza o texto apenas com números
                _txtQuantidade.Text = texto;
                _txtQuantidade.SelectionStart = _txtQuantidade.Text.Length;
            }

            public void AddToPanel(Panel panel)
            {
                panel.Controls.Add(_lblProduto);
                panel.Controls.Add(_txtQuantidade);
            }

            public int GetQuantidade()
            {
                if (int.TryParse(_txtQuantidade.Text, out int qtd))
                    return qtd;
                return 0;
            }

            public decimal GetValor()
            {
                return _valorPadrao;
            }

            public void Limpar()
            {
                _txtQuantidade.Text = "";
            }
        }

        // Classe que representa uma forma de pagamento (Label + TextBox Valor)
        private class FormaPagamentoInput
        {
            public int IdFormaPagamento { get; set; }
            public string NomeFormaPagamento { get; set; }
            private Label _lblForma;
            private TextBox _txtValor;
            private int _yPosition;
            private FormPDV _formParent;

            public FormaPagamentoInput(int idFormaPagamento, string nomeFormaPagamento, int yPosition, FormPDV formParent)
            {
                IdFormaPagamento = idFormaPagamento;
                NomeFormaPagamento = nomeFormaPagamento;
                _yPosition = yPosition;
                _formParent = formParent;
                
                // Criar label com nome da forma de pagamento
                _lblForma = new Label
                {
                    Text = nomeFormaPagamento,
                    Location = new Point(10, yPosition),
                    Size = new Size(280, 45),
                    Font = new Font("Segoe UI", 20F),
                    AutoSize = false
                };

                // Criar TextBox Valor (vazio, habilitado)
                _txtValor = new TextBox
                {
                    Location = new Point(10, yPosition + 50),
                    Size = new Size(180, 50),
                    Enabled = true,
                    Font = new Font("Segoe UI", 20F),
                    Text = ""
                };
                _txtValor.Leave += TxtValor_Leave;
                _txtValor.TextChanged += TxtValor_TextChanged;
            }

            private void TxtValor_Leave(object sender, EventArgs e)
            {
                // Validar se é um número válido
                if (decimal.TryParse(_txtValor.Text, out decimal valor))
                {
                    if (valor < 0)
                    {
                        _txtValor.Text = "0";
                    }
                }
                else if (!string.IsNullOrWhiteSpace(_txtValor.Text))
                {
                    _txtValor.Text = "0";
                }
                
                // Chamar método do form parent para atualizar troco
                _formParent.AtualizarTroco();
            }

            private void TxtValor_TextChanged(object sender, EventArgs e)
            {
                // Remove caracteres não numéricos
                string texto = new string(_txtValor.Text.Where(c => char.IsDigit(c)).ToArray());

                // Se vazio, mostra "0"
                if (string.IsNullOrEmpty(texto))
                {
                    texto = "0";
                }

                // Formata com 2 casas decimais
                decimal valor = decimal.Parse(texto) / 100;
                
                // Guarda o index do cursor
                int cursorPos = _txtValor.SelectionStart;
                
                // Atualiza o texto formatado
                _txtValor.Text = valor.ToString("F2");
                
                // Reposiciona o cursor no final
                _txtValor.SelectionStart = _txtValor.Text.Length;
            }

            public bool IsDinheiro()
            {
                return NomeFormaPagamento.ToUpper().Contains("DINHEIRO");
            }

            public void AddToPanel(Panel panel)
            {
                panel.Controls.Add(_lblForma);
                panel.Controls.Add(_txtValor);
            }

            public decimal GetValor()
            {
                if (decimal.TryParse(_txtValor.Text, out decimal valor))
                    return valor;
                return 0m;
            }

            public void Limpar()
            {
                _txtValor.Text = "";
            }
        }

        // Classe auxiliar para itens de venda
        private class VendaItem
        {
            public int IdProduto { get; set; }
            public string NomeProduto { get; set; }
            public int Quantidade { get; set; }
            public decimal ValorUnitario { get; set; }
            public decimal Subtotal { get; set; }
        }
    }
}