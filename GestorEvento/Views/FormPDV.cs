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
        private int _numeroCaixa = 0;
        private string _descricaoCaixa = "";
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
        private MovimentacaoService _movimentacaoService;
        private MotivoReimpressaoService _motivoReimpressaoService;
        private ReimpressaoService _reimpressaoService;

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
                            xPosition += 240; // Largura de uma coluna (aumentado para mais espaço entre colunas)
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

                // ======== VALIDAÇÃO 3: VERIFICAR ESTOQUE ANTES DE REGISTRAR ========
                List<ProdutoEvento> produtosAtualizados = _produtoEventoService.GetProdutosVinculados(_eventoIdSelecionado);
                string erroEstoque = ValidarEstoqueDisponivel(produtosAtualizados);
                
                if (!string.IsNullOrEmpty(erroEstoque))
                {
                    // Mostrar qual produto acabou
                    DialogoCustomizado aviso = new DialogoCustomizado(
                        "Estoque Insuficiente",
                        $"⚠️ PRODUTO SEM ESTOQUE SUFICIENTE:\n\n{erroEstoque}\n\nAtualizando quantidades disponíveis...",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    aviso.ShowDialog();
                    
                    // Atualizar APENAS as quantidades na tela sem limpar dados
                    AtualizarQuantidadesNaTela(produtosAtualizados);
                    return; // Não registra venda
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

                // Registrar TUDO em uma transação (se algo falhar, rollback completo)
                int idVenda = _vendaService.RegistrarVendaComTrocoComTransacao(venda, recebimentos, vlTroco);

                System.Diagnostics.Debug.WriteLine($"[TRANSAÇÃO] Venda #{idVenda} registrada com sucesso (com troco R$ {vlTroco:F2})");

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
                            System.Diagnostics.Debug.WriteLine($"✗ Erro ao registrar estoque: {exEstoque.Message}");
                        }
                    }
                }

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

        // Validar se todos os produtos têm estoque suficiente
        private string ValidarEstoqueDisponivel(List<ProdutoEvento> produtosAtualizados)
        {
            foreach (var linha in _produtosLinhas)
            {
                int qtdeSolicitada = linha.GetQuantidade();
                if (qtdeSolicitada > 0)
                {
                    // Buscar produto atualizado do banco
                    var produtoAtualizado = produtosAtualizados.FirstOrDefault(p => p.Id == linha.IdProdutoEvento);
                    
                    if (produtoAtualizado == null)
                    {
                        return $"{linha.NomeProduto}: Produto não encontrado no banco de dados";
                    }

                    int quantidadeDisponivel = produtoAtualizado.QuantidadeDisponivel;

                    if (qtdeSolicitada > quantidadeDisponivel)
                    {
                        return $"{linha.NomeProduto}:\nDisponível: {quantidadeDisponivel}\nSolicitado: {qtdeSolicitada}";
                    }
                }
            }

            return null; // Tudo ok
        }

        // Atualizar apenas as quantidades na tela (sem limpar dados preenchidos)
        private void AtualizarQuantidadesNaTela(List<ProdutoEvento> produtosAtualizados)
        {
            foreach (var linha in _produtosLinhas)
            {
                var produtoAtualizado = produtosAtualizados.FirstOrDefault(p => p.Id == linha.IdProdutoEvento);
                
                if (produtoAtualizado != null)
                {
                    int novaQuantidade = produtoAtualizado.QuantidadeDisponivel;
                    
                    // Se a quantidade solicitada for maior que a disponível, reduzir a solicitação
                    int qtdeSolicitada = linha.GetQuantidade();
                    if (qtdeSolicitada > novaQuantidade)
                    {
                        // Ajustar para a máxima disponível (mantém o restante preenchido)
                        linha.AtualizarQuantidadeDisponivel(novaQuantidade);
                        linha.SetQuantidade(novaQuantidade); // Define a quantidade para a disponível
                        
                        System.Diagnostics.Debug.WriteLine($"⚠️ {linha.NomeProduto}: Qtde ajustada de {qtdeSolicitada} para {novaQuantidade}");
                    }
                    else
                    {
                        // Atualizar a quantidade disponível exibida
                        linha.AtualizarQuantidadeDisponivel(novaQuantidade);
                    }
                }
            }

            // Recalcular total com novas quantidades
            AtualizarTotalVenda();
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
                        decimal valor = linha.GetValor();
                        reimpressao.Itens.Add(new ReimpressaoItem
                        {
                            IdProdutoEvento = linha.IdProdutoEvento,
                            QtdeReimpressao = qtde,
                            VlUnitario = valor / qtde, // Preço unitário
                            VlSubtotal = valor,
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
        /// Handler para mudança no seletor de tipo de operação
        /// Quando CORTESIA ou REIMPRIMIR é selecionado, desabilita os campos de pagamento
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
            }
            else
            {
                // VENDA selecionado - habilitar formas de pagamento
                foreach (var formaPag in _formasPagamento)
                {
                    formaPag.SetEnabled(true);
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
            private decimal _valorPadrao;
            private int _quantidadeDisponivel;  // Quantidade que pode ser vendida
            private Label _lblProduto;
            private TextBox _txtQuantidade;
            private Button _btnMais;
            private Button _btnMenos;
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
                    Text = $"{nomeProduto} - R$ {valorPadrao.ToString("F2")} - ({quantidadeDisponivel})",
                    Location = new Point(xPosition, yPosition),
                    Size = new Size(240, 30),
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
            }

            private void BtnMais_Click(object sender, EventArgs e)
            {
                int qtdeAtual = GetQuantidade();
                if (qtdeAtual < _quantidadeDisponivel)
                {
                    _txtQuantidade.Text = (qtdeAtual + 1).ToString();
                }
                else
                {
                    // Exibir aviso se atingiu o limite
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
                _lblProduto.Text = $"{NomeProduto} - R$ {_valorPadrao.ToString("F2")} - Disp. ({novaQuantidade})";
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