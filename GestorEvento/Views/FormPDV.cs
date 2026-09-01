using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestorEvento.Models;
using GestorEvento.Models.Exceptions;
using GestorEvento.Utilities;
using GestorEvento.Services;

namespace GestorEvento.Views
{
    public partial class FormPDV : Form
    {
        private int _caixaIdSelecionado = 0;
        private int _eventoIdSelecionado = 0;
        private int _numeroCaixa = 0;
        private string _descricaoCaixa = "";
        private decimal _totalVenda = 0m;
        private List<VendaItem> _itensVenda = new List<VendaItem>();
        private List<ProdutoLinhaVenda> _produtosLinhas = new List<ProdutoLinhaVenda>();
        private List<FormaPagamentoInput> _formasPagamento = new List<FormaPagamentoInput>();
        private List<DoacaoFormaInput> _formasDoacao = new List<DoacaoFormaInput>();
        private InscricaoEvento _inscricaoVinculada = null;
        private bool _isDragging = false;
        private Point _dragPoint;
        private VendaService _vendaService;
        private ProdutoEventoService _produtoEventoService;
        private InscricaoEventoService _inscricaoEventoService;
        private ProdutoService _produtoService;
        private PontoVendaService _pontoVendaService;
        private FormaPagamentoService _formaPagamentoService;
        private RecebimentoService _recebimentoService;
        private MovimentacaoService _movimentacaoService;
        private MotivoReimpressaoService _motivoReimpressaoService;
        private ReimpressaoService _reimpressaoService;

        public FormPDV(int caixaId)
        {
            InitializeComponent();
            _caixaIdSelecionado = caixaId;
            _vendaService = new VendaService();
            _produtoEventoService = new ProdutoEventoService();
            _inscricaoEventoService = new InscricaoEventoService();
            _produtoService = new ProdutoService();
            _pontoVendaService = new PontoVendaService();
            _formaPagamentoService = new FormaPagamentoService();
            _recebimentoService = new RecebimentoService();
            _movimentacaoService = new MovimentacaoService();
            _motivoReimpressaoService = new MotivoReimpressaoService();
            _reimpressaoService = new ReimpressaoService();
        }

        private void FormPDV_Load(object sender, EventArgs e)
        {
            try
            {
                // Minimizar menu principal e trazer PDV para frente
                foreach (Form form in Application.OpenForms)
                {
                    if (form.GetType().Name == "FormPrincipal")
                    {
                        form.WindowState = FormWindowState.Minimized;
                        break;
                    }
                }
                
                // Trazer janela para frente
                this.BringToFront();
                this.Focus();
                
                // Registrar evento de fechamento para desconectar impressora
                this.FormClosing += FormPDV_FormClosing;
                
                // Buscar número do caixa e exibir
                var pontoVenda = _pontoVendaService.GetPontoVendaById(_caixaIdSelecionado);
                if (pontoVenda != null)
                {
                    _numeroCaixa = pontoVenda.NoPontoVenda;
                    _descricaoCaixa = pontoVenda.DsPontoVenda ?? "";
                    _eventoIdSelecionado = pontoVenda.IdEvento;

                    if (!string.Equals(pontoVenda.CdStatus, "Aberto", StringComparison.OrdinalIgnoreCase))
                    {
                        DialogoCustomizado caixaFechado = new DialogoCustomizado(
                            "Aviso",
                            "Caixa fechado. Não é possível registrar venda.",
                            TipoDialogo.Aviso,
                            TipoButton.Ok
                        );
                        caixaFechado.ShowDialog();
                        this.Close();
                        return;
                    }

                    var evento = new EventoService().GetEventoById(_eventoIdSelecionado);
                    if (evento != null && evento.IsEncerrado)
                    {
                        DialogoCustomizado bloqueado = new DialogoCustomizado(
                            "Aviso",
                            "Evento encerrado. Vendas não podem ser registradas.",
                            TipoDialogo.Aviso,
                            TipoButton.Ok
                        );
                        bloqueado.ShowDialog();
                        this.Close();
                        return;
                    }
                    
                    // Concatenar número com descrição se houver
                    string textoDescricao = string.IsNullOrWhiteSpace(_descricaoCaixa) ? 
                        "" : $" - {_descricaoCaixa}";
                    lblInfoCaixa.Text = $"Caixa: {_numeroCaixa}{textoDescricao}";
                    
                    // Ajustar layout responsivo para maximized
                    AjustarLayoutPaineis();
                    
                    // Posicionar botão próximo à label de total
                    PosicionarBotaoProximoAoTotal();
                    
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
            int larguraProdutos = (int)(larguraDisponivel * 0.70);
            int larguraPagamento = (int)(larguraDisponivel * 0.15);
            
            // Definir largura dos painéis de esquerda
            panelProdutos.Width = larguraProdutos;
            panelPagamento.Width = larguraPagamento;
            
            // panelTotalizacao preencherá o resto automaticamente (Dock=Fill)
        }

        // Posicionar botão Confirmar próximo à label de total
        private void PosicionarBotaoProximoAoTotal()
        {
            // Calcular posição Y baseado na label lblTotalValor
            int yLabel = lblTrocoValor.Location.Y;
            int alturaLabel = lblTrocoValor.Height;
            int espacamento = 8; // Pequeno espaçamento entre label e botão
            
            // Definir nova posição Y do botão
            btnConfirmarVenda.Location = new Point(btnConfirmarVenda.Location.X, yLabel + alturaLabel + espacamento);
        }

        private void CarregarProdutos()
        {
            panelProdutos.Controls.Clear();
            _produtosLinhas.Clear();
            
            try
            {
                // Obter produtos vinculados ao evento e ordenar alfabeticamente
                var produtosEvento = _produtoEventoService.GetProdutosVinculados(_eventoIdSelecionado)
                    .OrderBy(p => _produtoService.GetProductById(p.IdProduto)?.Nome ?? "")
                    .ToList();
                
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
                int larguraColuna = 240; // Largura padrão da coluna
                int larguraLabel = 240; // Largura padrão da label
                
                // AJUSTE PARA DPI 120: Detectar DPI real e ajustar apenas para 120 DPI
                float dpiAtual = 96f; // Padrão
                using (Graphics g = Graphics.FromHwnd(IntPtr.Zero))
                {
                    dpiAtual = g.DpiX;
                }

                if (dpiAtual >= 120)
                {
                    alturaItem = 110; // Aumentar altura para evitar truncamento no Slim 3
                    larguraColuna = 300; // Aumentar largura da coluna para acomodar label de 300px + espaçamento
                    larguraLabel = 300; // Aumentar largura da label para nomes longos
                }
                
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
                            xPosition += larguraColuna; // Usar variável (ajustada para DPI 120 se necessário)
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
                            larguraLabel,  // Passar largura ajustada para DPI 120
                            produtoEvento.Antecipado,  // Produto antecipado: quantidade sempre bloqueada para edição manual
                            this
                        );
                        
                        produtoLinha.AddToPanel(panelProdutos);
                        _produtosLinhas.Add(produtoLinha);
                        
                        // Incrementar posição vertical
                        yPosition += alturaItem; // Usar variável (ajustada para DPI 120 se necessário)
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

                    yPosition += 100;
                }

                // Seção de Doação, logo abaixo do último campo de pagamento
                // Largura calculada a partir do espaço real de panelPagamento (que é dinâmico, ver AjustarLayoutPaineis)
                int larguraSecaoDoacao = Math.Max(180, panelPagamento.ClientSize.Width - 20);
                panelDoacao.Width = larguraSecaoDoacao;
                panelCamposDoacao.Width = larguraSecaoDoacao;
                lblSetaDoacao.Location = new Point(larguraSecaoDoacao - 26, 2);

                panelDoacao.Location = new Point(10, yPosition + 5);
                panelPagamento.Controls.Add(panelDoacao);
                CarregarFormasDoacao();
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

        // Carrega os campos de valor da seção de Doação, um por forma de pagamento (mesma fonte de CarregarFormasPagamento)
        private void CarregarFormasDoacao()
        {
            panelCamposDoacao.Controls.Clear();
            _formasDoacao.Clear();

            try
            {
                var formas = _formaPagamentoService.GetAllFormasPagamento();

                int yPosition = 5;
                foreach (var forma in formas)
                {
                    var doacaoInput = new DoacaoFormaInput(forma.Id, forma.NmFormaPagamento, yPosition, panelCamposDoacao.Width);
                    doacaoInput.AddToPanel(panelCamposDoacao);
                    _formasDoacao.Add(doacaoInput);

                    yPosition += 32;
                }

                panelCamposDoacao.Height = yPosition + 5;
                AtualizarAlturaPanelDoacao();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao carregar formas de doação: {ex.Message}");
            }
        }

        // Ajusta a altura do container da seção de Doação conforme está recolhida ou expandida
        private void AtualizarAlturaPanelDoacao()
        {
            panelDoacao.Height = chkDoacao.Checked ? (30 + panelCamposDoacao.Height) : 30;
        }

        private void ChkDoacao_CheckedChanged(object sender, EventArgs e)
        {
            panelCamposDoacao.Visible = chkDoacao.Checked;
            lblSetaDoacao.Text = chkDoacao.Checked ? "▲" : "▼";
            AtualizarAlturaPanelDoacao();

            if (!chkDoacao.Checked)
            {
                foreach (var doacaoInput in _formasDoacao)
                {
                    doacaoInput.Limpar();
                }
            }
        }

        // Retorna as doações informadas (vazio se a seção estiver desmarcada) - já no formato usado pelo VendaService
        private List<(int idFormaPagamento, decimal valor)> GetDoacoes()
        {
            var doacoes = new List<(int idFormaPagamento, decimal valor)>();

            if (!chkDoacao.Checked)
            {
                return doacoes;
            }

            foreach (var doacaoInput in _formasDoacao)
            {
                decimal valor = doacaoInput.GetValor();
                if (valor > 0)
                {
                    doacoes.Add((doacaoInput.IdFormaPagamento, valor));
                }
            }

            return doacoes;
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
                var pontoVendaAtual = _pontoVendaService.GetPontoVendaById(_caixaIdSelecionado);
                if (pontoVendaAtual == null || !string.Equals(pontoVendaAtual.CdStatus, "Aberto", StringComparison.OrdinalIgnoreCase))
                {
                    DialogoCustomizado caixaFechado = new DialogoCustomizado(
                        "Aviso",
                        "Caixa fechado. Não é possível confirmar venda.",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    caixaFechado.ShowDialog();
                    return;
                }

                // Validação de status do evento no momento da confirmação
                var eventoAtual = new EventoService().GetEventoById(_eventoIdSelecionado);
                if (eventoAtual != null && eventoAtual.IsEncerrado)
                {
                    DialogoCustomizado eventoEncerrado = new DialogoCustomizado(
                        "Aviso",
                        "Evento encerrado. Vendas não podem ser registradas.",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    eventoEncerrado.ShowDialog();
                    return;
                }

                // ============ SE OPERAÇÃO = REIMPRIMIR, ROTA DIFERENTE ============
                if (_rbReimprimir.Checked)
                {
                    ExibirDialogReimpressao();
                    return;
                }

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

                // Validação 2: A soma dos pagamentos é >= total da venda? (apenas para VENDA)
                // CORTESIA não precisa de pagamento
                decimal somaPagementos = _formasPagamento.Sum(f => f.GetValor());
                
                if (!_rbCortesia.Checked)
                {
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
                }

                // ======== VALIDAÇÃO 3: VERIFICAR ESTOQUE ANTES DE REGISTRAR (camada adicional) ========
                // Essa validação é feita NOVAMENTE durante a transação com SELECT...FOR UPDATE
                // Mas essa aqui serve como fail-fast para evitar enviar para transação se souber que faltará
                // Recarrega quantidades atuais do banco antes de validar
                try
                {
                    List<ProdutoEvento> produtosAtualizados = _produtoEventoService.GetProdutosVinculados(_eventoIdSelecionado);
                    
                    foreach (var linha in _produtosLinhas)
                    {
                        int qtdeInformada = linha.GetQuantidade();
                        if (qtdeInformada > 0)
                        {
                            var produtoAtualizado = produtosAtualizados.FirstOrDefault(p => p.Id == linha.IdProdutoEvento);
                            if (produtoAtualizado != null)
                            {
                                // Atualizar a quantidade disponível na linha (pode ter mudado)
                                linha.AtualizarQuantidadeDisponivel(produtoAtualizado.QuantidadeDisponivel);
                                
                                // Validar se há quantidade suficiente
                                if (qtdeInformada > produtoAtualizado.QuantidadeDisponivel)
                                {
                                    DialogoCustomizado dialogo = new DialogoCustomizado(
                                        "Estoque Insuficiente",
                                        $"Produto: {linha.NomeProduto}\n" +
                                        $"Disponível: {produtoAtualizado.QuantidadeDisponivel}\n" +
                                        $"Solicitado: {qtdeInformada}\n\n" +
                                        $"Ajuste as quantidades e tente novamente.",
                                        TipoDialogo.Aviso,
                                        TipoButton.Ok
                                    );
                                    dialogo.ShowDialog();
                                    
                                    // Atualizar totalizações
                                    AtualizarTotalVenda();
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (Exception exValidacao)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Erro ao Validar Estoque",
                        $"Erro ao validar disponibilidade: {exValidacao.Message}",
                        TipoDialogo.Erro,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                // Criar venda com itens que têm quantidade > 0
                var venda = new Venda(_caixaIdSelecionado);
                
                // Definir tipo de operação (VENDA ou CORTESIA) baseado no seletor
                venda.TipoOperacao = _rbCortesia.Checked ? "CORTESIA" : "VENDA";
                
                foreach (var linha in _produtosLinhas)
                {
                    int qtde = linha.GetQuantidade();
                    if (qtde > 0)
                    {
                        decimal valor = linha.GetValor();
                        
                        venda.AdicionarItem(new ItemVenda(
                            linha.IdProdutoEvento,
                            linha.NomeProduto,
                            qtde,
                            valor
                        ));
                    }
                }

                // Registrar venda + recebimentos + troco em TRANSAÇÃO ÚNICA (seguro!)
                // CORTESIA não gera troco
                decimal vlTroco = _rbCortesia.Checked ? 0 : (somaPagementos - _totalVenda);
                
                // Preparar lista de recebimentos
                var recebimentos = new List<(int idFormaPagamento, decimal valor)>();
                foreach (var forma in _formasPagamento)
                {
                    decimal valor = forma.GetValor();
                    if (valor > 0)
                    {
                        recebimentos.Add((forma.IdFormaPagamento, valor));
                    }
                }

                // Preparar lista de doações (independente do troco/pagamento)
                var doacoes = GetDoacoes();

                // Registrar TUDO em uma transação atômica com validação de estoque
                // (Se algo falhar, rollback completo: VENDA + RECEBIMENTO + ESTOQUE + retirada da inscrição)
                int idVenda = _vendaService.RegistrarVendaComEstoqueComTransacao(venda, recebimentos, vlTroco, doacoes, _inscricaoVinculada?.Id);

                System.Diagnostics.Debug.WriteLine($"[TRANSAÇÃO] Venda #{idVenda} registrada com sucesso com estoque debitado");

                // ============ IMPRIMIR VENDA COMPLETA NA IMPRESSORA TÉRMICA ============
                System.Diagnostics.Debug.WriteLine($"\n[IMPRESSÃO] Enviando venda #{idVenda} para impressão");

                // Coletar TODOS os itens em uma lista com nome e preço
                List<ItemImpressao> itensPorImprimir = new List<ItemImpressao>();
                foreach (var linha in _produtosLinhas)
                {
                    int qtde = linha.GetQuantidade();
                    decimal preco = linha.GetValor();
                    for (int i = 0; i < qtde; i++)
                    {
                        itensPorImprimir.Add(new ItemImpressao(linha.NomeProduto, preco));
                    }
                }

                // Enviar TUDO em UMA requisição (evita race condition entre máquinas)
                bool sucessoVenda = PrinterServiceFactory.ImprimirVenda(idVenda, itensPorImprimir, _numeroCaixa, _descricaoCaixa);

                if (!sucessoVenda)
                {
                    DialogoCustomizado aviso = new DialogoCustomizado(
                        "Aviso - Impressão",
                        $"Erro ao enviar venda #{idVenda} para impressão.\n\nVerifique se a impressora está conectada e ligada.",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    aviso.ShowDialog();
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"✓ Venda #{idVenda} enviada com sucesso ({itensPorImprimir.Count} itens)");
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
            catch (EstoqueInsuficienteException exEstoque)
            {
                // ERRO ESPECÍFICO: Insuficiência de estoque
                // UX Híbrida (Opção 3): Atualizar visualmente APENAS o produto problemático
                
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Estoque Insuficiente",
                    $"⚠️ NÃO FOI POSSÍVEL REGISTRAR A VENDA\n\n" +
                    $"Produto: {exEstoque.NomeProduto}\n" +
                    $"Disponível: {exEstoque.QuantidadeDisponivel}\n" +
                    $"Solicitado: {exEstoque.QuantidadeSolicitada}\n\n" +
                    $"Ajuste as quantidades e tente novamente.",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();

                // Atualizar APENAS o produto problemático na tela (deixar outros intactos)
                try
                {
                    List<ProdutoEvento> produtosAtualizados = _produtoEventoService.GetProdutosVinculados(_eventoIdSelecionado);
                    var linhaProblema = _produtosLinhas.FirstOrDefault(p => p.IdProdutoEvento == exEstoque.IdProdutoEvento);
                    
                    if (linhaProblema != null)
                    {
                        var produtoAtualizado = produtosAtualizados.FirstOrDefault(p => p.Id == exEstoque.IdProdutoEvento);
                        if (produtoAtualizado != null)
                        {
                            // Atualizar quantidade disponível do produto problemático
                            linhaProblema.AtualizarQuantidadeDisponivel(produtoAtualizado.QuantidadeDisponivel);
                        }
                    }
                    
                    // Recalcular total (sem o produto que deu erro)
                    AtualizarTotalVenda();
                }
                catch (Exception exUpdate)
                {
                    System.Diagnostics.Debug.WriteLine($"Erro ao atualizar produto: {exUpdate.Message}");
                }
                
                // NÃO limpar a tela - usuário pode corrigir e tentar novamente
            }
            catch (InscricaoIndisponivelException exInscricao)
            {
                // ERRO ESPECÍFICO: a inscrição vinculada já não está mais Pendente (retirada por
                // outro terminal/operador entre a seleção e a confirmação). A transação já deu
                // rollback e nada foi debitado (a inscrição é travada antes do estoque).
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Inscrição Indisponível",
                    $"{exInscricao.Message}\n\nDesvincule esta inscrição e selecione outra, se necessário.",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();

                // NÃO limpar a tela - usuário pode clicar em CANCELAR INSCRIÇÃO e tentar com outra
            }
            catch (Exception ex)
            {
                // ERRO GENÉRICO: Qualquer outro erro durante a transação
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro ao Confirmar Venda",
                    $"Erro ao registrar venda (transação revertida):\n\n{ex.Message}\n\nTente novamente ou contacte suporte.",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
                
                // Não limpar - usuário pode revisar e reenviar
            }
        }

        /// <summary>
        /// Exibe dialog para seleção de motivo e registra a reimpressão
        /// </summary>
        private void ExibirDialogReimpressao()
        {
            try
            {
                // Validação: Existe pelo menos um produto com qtde > 0?
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

                // 1. Carregar motivos de reimpressão
                var motivos = _motivoReimpressaoService.GetMotivosAtivos();
                if (motivos == null || motivos.Count == 0)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Erro",
                        "Nenhum motivo de reimpressão disponível",
                        TipoDialogo.Erro,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                // 2. Mostrar dialog para seleção de motivo
                var dialogMotivo = new DialogMotivoReimpressao(motivos);
                if (dialogMotivo.ShowDialog() != DialogResult.OK)
                {
                    return; // Usuário cancelou
                }

                // 3. Criar objeto Reimpressao com os dados
                var reimpressao = new Reimpressao
                {
                    DtReimpressao = DateTime.Now,
                    IdMotivo = dialogMotivo.MotivoSelecionado.IdMotivo,
                    IdEvento = _eventoIdSelecionado,
                    IdPontoVenda = _caixaIdSelecionado,
                    Itens = new List<ReimpressaoItem>()
                };

                // 4. Adicionar itens com quantidade > 0
                foreach (var linha in _produtosLinhas)
                {
                    int qtde = linha.GetQuantidade();
                    if (qtde > 0)
                    {
                        decimal vlUnitario = linha.GetValor(); // GetValor() já retorna preço unitário
                        decimal vlSubtotal = vlUnitario * qtde; // Preço unitário × Quantidade
                        reimpressao.Itens.Add(new ReimpressaoItem
                        {
                            IdProdutoEvento = linha.IdProdutoEvento,
                            QtdeReimpressao = qtde,
                            VlUnitario = vlUnitario,
                            VlSubtotal = vlSubtotal,
                            DescricaoProduto = linha.NomeProduto
                        });
                    }
                }

                // 5. Registrar reimpressão (sem debitar estoque)
                int idReimpressao = _reimpressaoService.RegistrarReimpressao(reimpressao, _numeroCaixa, _descricaoCaixa);

                // 6. Mensagem de sucesso
                DialogoCustomizado dialogo_sucesso = new DialogoCustomizado(
                    "Sucesso",
                    $"Cupom reimpresso com sucesso!\n\nID Reimpressão: {idReimpressao}",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                dialogo_sucesso.ShowDialog();

                // 7. Limpar tela
                LimparTudo();
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo_erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao registrar reimpressão:\n\n{ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo_erro.ShowDialog();
            }
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

            // Desfazer qualquer vínculo de inscrição antecipada, para não vazar para a próxima venda
            DesvincularInscricao();
            
            // Limpar todas as formas de pagamento
            foreach (var forma in _formasPagamento)
            {
                forma.Limpar();
            }

            // Recolher e limpar a seção de Doação
            chkDoacao.Checked = false;
            foreach (var doacaoInput in _formasDoacao)
            {
                doacaoInput.Limpar();
            }

            // Resetar seletor de tipo de operação para VENDA
            if (_rbVenda != null)
            {
                _rbVenda.Checked = true;
                _rbCortesia.Checked = false;
                
                // Habilitar novamente os campos de pagamento
                foreach (var forma in _formasPagamento)
                {
                    forma.SetEnabled(true);
                }
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

        // Desconexão de impressora agora é gerenciada pelo PrinterServiceFactory
        private void FormPDV_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("✓ FormPDV fechado - PrinterServiceFactory gerencia conexão");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao fechar: {ex.Message}");
            }
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
            PosicionarBotaoProximoAoTotal();
        }

        /// <summary>
        /// Inicializa o seletor de tipo de operação (VENDA/CORTESIA)
        /// Adiciona RadioButtons no painel de totalização com VENDA pré-selecionado
        /// </summary>
        /// <summary>
        /// Verifica se a operação selecionada é REIMPRIMIR
        /// </summary>
        public bool IsOperacaoReimprimir()
        {
            return _rbReimprimir != null && _rbReimprimir.Checked;
        }

        /// <summary>
        /// Handler para mudança no seletor de tipo de operação
        /// Quando CORTESIA ou REIMPRIMIR é selecionado, desabilita os campos de pagamento
        /// Para REIMPRIMIR, também habilita todos os campos de quantidade
        /// </summary>
        private void RbTipoOperacao_CheckedChanged(object sender, EventArgs e)
        {
            if (_rbCortesia.Checked || _rbReimprimir.Checked)
            {
                // CORTESIA ou REIMPRIMIR selecionado - desabilitar formas de pagamento
                foreach (var formaPag in _formasPagamento)
                {
                    formaPag.SetEnabled(false);
                }

                // Doação não se aplica a CORTESIA/REIMPRIMIR - desmarca e desabilita (o desmarcar já recolhe e limpa)
                chkDoacao.Checked = false;
                chkDoacao.Enabled = false;
            }
            else
            {
                // VENDA selecionado - habilitar formas de pagamento
                foreach (var formaPag in _formasPagamento)
                {
                    formaPag.SetEnabled(true);
                }

                chkDoacao.Enabled = true;
            }

            // Se REIMPRIMIR foi selecionado, atualizar estado dos campos de quantidade
            // para permitir reimprimir qualquer quantidade mesmo sem estoque
            if (_rbReimprimir.Checked)
            {
                foreach (var linha in _produtosLinhas)
                {
                    linha.AtualizarEstadoParaReimprimir(true);
                }
            }
            else
            {
                // Restaurar validação normal de estoque para VENDA
                foreach (var linha in _produtosLinhas)
                {
                    linha.AtualizarEstadoParaReimprimir(false);
                }
            }
        }

        // ==================== CLASSES INTERNAS ====================

        // Classe que representa uma linha de produto (Label + TextBox Qtde + Botões +/-)
        private class ProdutoLinhaVenda
        {
            public int IdProdutoEvento { get; set; }  // ID da vinculação produto-evento
            public int IdProduto { get; set; }
            public string NomeProduto { get; set; }
            public bool Antecipado { get; private set; }  // fl_antecipado: quantidade só pode ser definida via SetQuantidade (rotina de inscrição), nunca manualmente
            private decimal _valorPadrao;
            private int _quantidadeDisponivel;  // Quantidade que pode ser vendida
            private Label _lblProduto;
            private TextBox _txtQuantidade;
            private Button _btnMais;
            private Button _btnMenos;
            private FormPDV _formParent;
            private bool _isReimprimindo = false;  // Flag para indicar modo REIMPRIMIR

            public ProdutoLinhaVenda(int idProdutoEvento, int idProduto, string nomeProduto, decimal valorPadrao, int quantidadeDisponivel, int xPosition, int yPosition, int larguraLabel, bool antecipado, FormPDV formParent)
            {
                IdProdutoEvento = idProdutoEvento;
                IdProduto = idProduto;
                NomeProduto = nomeProduto;
                Antecipado = antecipado;
                _valorPadrao = valorPadrao;
                _quantidadeDisponivel = quantidadeDisponivel;
                _formParent = formParent;

                // Criar label com nome do produto, valor e quantidade disponível - NO TOPO
                _lblProduto = new Label
                {
                    Text = $"{nomeProduto} R${valorPadrao.ToString("F2")} ({quantidadeDisponivel})" + (antecipado ? " [ANTECIPADO]" : ""),
                    Location = new Point(xPosition, yPosition),
                    Size = new Size(larguraLabel, 30),  // Usar parâmetro para ajustar largura
                    Font = new Font("Segoe UI", 10F),
                    AutoSize = false,
                    TextAlign = ContentAlignment.TopLeft
                };

                // Criar TextBox Quantidade (reduzido para 80px) - EMBAIXO
                _txtQuantidade = new TextBox
                {
                    Location = new Point(xPosition, yPosition + 35),
                    Size = new Size(80, 35),
                    Enabled = true,
                    Font = new Font("Segoe UI", 12F),
                    Text = "",
                    TextAlign = HorizontalAlignment.Center
                };
                _txtQuantidade.Leave += TxtQuantidade_Leave;
                _txtQuantidade.TextChanged += TxtQuantidade_TextChanged;

                // Criar Botão + (mais)
                _btnMais = new Button
                {
                    Location = new Point(xPosition + 90, yPosition + 32),
                    Size = new Size(40, 35),
                    Text = "+",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                _btnMais.Click += BtnMais_Click;

                // Criar Botão - (menos)
                _btnMenos = new Button
                {
                    Location = new Point(xPosition + 135, yPosition + 32),
                    Size = new Size(40, 35),
                    Text = "−",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold),
                    FlatStyle = FlatStyle.Flat,
                    TextAlign = ContentAlignment.MiddleCenter
                };
                _btnMenos.Click += BtnMenos_Click;

                // Atualizar estado de disponibilidade (label vermelha e textbox desabilitado se sem estoque)
                AtualizarEstadoDisponibilidade();
            }

            private void BtnMais_Click(object sender, EventArgs e)
            {
                int qtdeAtual = GetQuantidade();
                
                // Se for REIMPRIMIR, não validar contra estoque - permitir qualquer quantidade
                if (_isReimprimindo)
                {
                    _txtQuantidade.Text = (qtdeAtual + 1).ToString();
                }
                else if (qtdeAtual < _quantidadeDisponivel)
                {
                    _txtQuantidade.Text = (qtdeAtual + 1).ToString();
                }
                else
                {
                    // Exibir aviso se atingiu o limite (apenas para VENDA/CORTESIA)
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        $"Quantidade máxima disponível: {_quantidadeDisponivel}",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                }
                _formParent.AtualizarTotalVenda();
            }

            private void BtnMenos_Click(object sender, EventArgs e)
            {
                int qtdeAtual = GetQuantidade();
                if (qtdeAtual > 0)
                {
                    _txtQuantidade.Text = (qtdeAtual - 1).ToString();
                }
                _formParent.AtualizarTotalVenda();
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
                    // Validar contra estoque disponível APENAS se não for REIMPRIMIR
                    else if (!_isReimprimindo && qtde > _quantidadeDisponivel)
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
                panel.Controls.Add(_btnMais);
                panel.Controls.Add(_btnMenos);
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

            public void AtualizarQuantidadeDisponivel(int novaQuantidade)
            {
                _quantidadeDisponivel = novaQuantidade;
                // Atualizar o label para refletir a nova quantidade disponível
                _lblProduto.Text = $"{NomeProduto} R${_valorPadrao.ToString("F2")} ({novaQuantidade})" + (Antecipado ? " [ANTECIPADO]" : "");

                // Atualizar estado de disponibilidade (label vermelha e textbox desabilitado se sem estoque)
                AtualizarEstadoDisponibilidade();
            }

            private void AtualizarEstadoDisponibilidade()
            {
                // Se está em modo REIMPRIMIR, sempre habilitar campos (não validar estoque)
                if (_isReimprimindo)
                {
                    _lblProduto.ForeColor = Color.Black;
                    _txtQuantidade.Enabled = true;
                    _txtQuantidade.BackColor = Color.White;
                    _btnMais.Enabled = true;
                    _btnMenos.Enabled = true;
                    return;
                }

                // Produto antecipado: quantidade SEMPRE bloqueada para edição manual do operador,
                // independente de estoque ou de haver inscrição vinculada no momento - só a rotina
                // de vinculação de inscrição (SetQuantidade) pode alterar o valor.
                if (Antecipado)
                {
                    _lblProduto.ForeColor = _quantidadeDisponivel <= 0 ? Color.Red : Color.Black;
                    _txtQuantidade.Enabled = false;
                    _txtQuantidade.BackColor = Color.FromArgb(230, 230, 230);
                    _btnMais.Enabled = false;
                    _btnMenos.Enabled = false;
                    return;
                }

                // Para VENDA e CORTESIA: validar estoque
                if (_quantidadeDisponivel <= 0)
                {
                    // Sem estoque: label vermelha e textbox desabilitado
                    _lblProduto.ForeColor = Color.Red;
                    _txtQuantidade.Enabled = false;
                    _txtQuantidade.BackColor = Color.LightGray;
                    _txtQuantidade.Text = "";
                    _btnMais.Enabled = false;
                    _btnMenos.Enabled = false;
                }
                else
                {
                    // Com estoque: label preta e textbox habilitado
                    _lblProduto.ForeColor = Color.Black;
                    _txtQuantidade.Enabled = true;
                    _txtQuantidade.BackColor = Color.White;
                    _btnMais.Enabled = true;
                    _btnMenos.Enabled = true;
                }
            }

            /// <summary>
            /// Atualiza o estado dos campos para modo REIMPRIMIR
            /// Em REIMPRIMIR, todos os campos ficam habilitados e não há validação de estoque
            /// </summary>
            public void AtualizarEstadoParaReimprimir(bool isReimprimindo)
            {
                _isReimprimindo = isReimprimindo;
                AtualizarEstadoDisponibilidade();
            }

            public void SetQuantidade(int quantidade)
            {
                _txtQuantidade.Text = quantidade.ToString();
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
                    Font = new Font("Segoe UI", 15F),
                    AutoSize = false
                };

                // Criar TextBox Valor (vazio, habilitado)
                _txtValor = new TextBox
                {
                    Location = new Point(10, yPosition + 50),
                    Size = new Size(150, 50),
                    Enabled = true,
                    Font = new Font("Segoe UI", 16F),
                    Text = "0,00"
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

                // Chamar método do form parent para atualizar troco
                _formParent.AtualizarTroco();
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

            public void SetEnabled(bool enabled)
            {
                _lblForma.Enabled = enabled;
                _txtValor.Enabled = enabled;
                
                // Deixar campo de forma de pagamento desabilitado visualmente para CORTESIA
                if (!enabled)
                {
                    _txtValor.BackColor = System.Drawing.Color.LightGray;
                    _txtValor.ForeColor = System.Drawing.Color.Gray;
                }
                else
                {
                    _txtValor.BackColor = System.Drawing.Color.White;
                    _txtValor.ForeColor = System.Drawing.Color.Black;
                }
            }
        }

        // Classe que representa um campo de doação (Label + TextBox Valor, lado a lado)
        private class DoacaoFormaInput
        {
            public int IdFormaPagamento { get; set; }
            public string NomeFormaPagamento { get; set; }
            private Label _lblForma;
            private TextBox _txtValor;

            public DoacaoFormaInput(int idFormaPagamento, string nomeFormaPagamento, int yPosition, int larguraPanel)
            {
                IdFormaPagamento = idFormaPagamento;
                NomeFormaPagamento = nomeFormaPagamento;

                int larguraLabel = 78;
                int xValor = larguraLabel + 12;
                int larguraValor = Math.Max(55, larguraPanel - xValor - 10);

                _lblForma = new Label
                {
                    Text = nomeFormaPagamento,
                    Location = new Point(4, yPosition),
                    Size = new Size(larguraLabel, 24),
                    Font = new Font("Segoe UI", 8.5F),
                    TextAlign = ContentAlignment.MiddleLeft
                };

                _txtValor = new TextBox
                {
                    Location = new Point(xValor, yPosition),
                    Size = new Size(larguraValor, 24),
                    Font = new Font("Segoe UI", 9F),
                    Text = "0,00"
                };
                _txtValor.TextChanged += TxtValor_TextChanged;
            }

            private void TxtValor_TextChanged(object sender, EventArgs e)
            {
                // Remove caracteres não numéricos
                string texto = new string(_txtValor.Text.Where(c => char.IsDigit(c)).ToArray());

                if (string.IsNullOrEmpty(texto))
                {
                    texto = "0";
                }

                // Formata com 2 casas decimais
                decimal valor = decimal.Parse(texto) / 100;

                _txtValor.Text = valor.ToString("F2");
                _txtValor.SelectionStart = _txtValor.Text.Length;
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
                _txtValor.Text = "0,00";
            }

            public void SetEnabled(bool enabled)
            {
                _lblForma.Enabled = enabled;
                _txtValor.Enabled = enabled;
                _txtValor.BackColor = enabled ? Color.White : Color.LightGray;
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

        private void btnAtualizarPDV_Click(object sender, EventArgs e)
        {
            LimparTudo();
            CarregarProdutos();
        }

        private void btnConsultarVenda_Click(object sender, EventArgs e)
        {
            FormConsultaVenda formConsultaVenda = new FormConsultaVenda();
            formConsultaVenda.ShowDialog();
        }

        private void btnMovimentacaoCaixa_Click(object sender, EventArgs e)
        {
            try
            {
                var pontoVendaAtual = _pontoVendaService.GetPontoVendaById(_caixaIdSelecionado);
                if (pontoVendaAtual == null || !string.Equals(pontoVendaAtual.CdStatus, "Aberto", StringComparison.OrdinalIgnoreCase))
                {
                    DialogoCustomizado caixaFechado = new DialogoCustomizado(
                        "Aviso",
                        "Caixa fechado. Não é possível registrar movimentação.",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    caixaFechado.ShowDialog();
                    return;
                }

                if (new EventoService().EventoEstaEncerrado(_eventoIdSelecionado))
                {
                    DialogoCustomizado eventoEncerrado = new DialogoCustomizado(
                        "Aviso",
                        "Evento encerrado. Movimentações não podem ser registradas.",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    eventoEncerrado.ShowDialog();
                    return;
                }

                FormMovimentacaoCaixa formMovimentacao = new FormMovimentacaoCaixa(_caixaIdSelecionado);
                formMovimentacao.ShowDialog();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao abrir movimentação de caixa: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
        }

        private void btnVincularInscricao_Click(object sender, EventArgs e)
        {
            if (_inscricaoVinculada != null)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Já existe uma inscrição vinculada a esta venda. Clique em CANCELAR INSCRIÇÃO antes de vincular outra.",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            if (_rbReimprimir.Checked)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Não é possível vincular inscrição no modo REIMPRIMIR.",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            var formPesquisa = new FormPesquisarInscricaoEvento(_eventoIdSelecionado);
            if (formPesquisa.ShowDialog(this) == DialogResult.OK)
            {
                AplicarInscricaoVinculada(formPesquisa.InscricaoSelecionada);
            }
        }

        private void btnCancelarInscricao_Click(object sender, EventArgs e)
        {            
            DesvincularInscricao();            
            LimparTudo();
            CarregarProdutos();
        }
        
        private void AplicarInscricaoVinculada(InscricaoEvento inscricao)
        {
            _inscricaoVinculada = inscricao;

            bool existeProdutoAntecipado = false;
            foreach (var linha in _produtosLinhas)
            {
                if (linha.Antecipado)
                {
                    existeProdutoAntecipado = true;
                    linha.SetQuantidade(inscricao.QtdeAntecipada);
                }
            }

            lblInscricaoVinculada.Text = $"🎫 Inscrição vinculada: {inscricao.NomeParticipante} ({inscricao.QtdeAntecipada}x)";
            lblInscricaoVinculada.Visible = true;

            TravarPanelBotoes(true);
            AtualizarTotalVenda();

            if (!existeProdutoAntecipado)
            {
                DialogoCustomizado aviso = new DialogoCustomizado(
                    "Aviso",
                    "Este evento não possui nenhum produto marcado como antecipado (fl_antecipado). A inscrição foi vinculada, mas nenhum produto foi carregado automaticamente.",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                aviso.ShowDialog();
            }
        }
        
        private void DesvincularInscricao()
        {
            if (_inscricaoVinculada == null)
                return;

            foreach (var linha in _produtosLinhas)
            {
                if (linha.Antecipado)
                {
                    linha.SetQuantidade(0);
                }
            }

            _inscricaoVinculada = null;
            lblInscricaoVinculada.Visible = false;

            TravarPanelBotoes(false);
        }

        
        private void TravarPanelBotoes(bool travar)
        {
            btnAtualizarPDV.Enabled = !travar;
            btnMovimentacaoCaixa.Enabled = !travar;
            btnConsultarVenda.Enabled = !travar;
            btnVincularInscricao.Enabled = !travar;

            btnCancelarInscricao.Visible = travar;
            btnCancelarInscricao.Enabled = travar;

            if (travar)
            {
                _rbVenda.Checked = true;
            }
            _rbCortesia.Enabled = !travar;
            _rbReimprimir.Enabled = !travar;
        }
    }
}