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
    public partial class FormSelecionarPDV : Form
    {
        private EventoService _eventoService;
        private PontoVendaService _pontoVendaService;
        private List<Evento> _eventos;
        private static FormPDV _formPDVGlobal = null;

        public FormSelecionarPDV()
        {
            InitializeComponent();
            _eventoService = new EventoService();
            _pontoVendaService = new PontoVendaService();
            _eventos = new List<Evento>();
        }

        private void FormSelecionarPDV_Load(object sender, EventArgs e)
        {
            try
            {
                // Inicializar combobox de caixas com item "Selecione"
                cmbCaixa.Items.Add("Selecione uma caixa");
                
                CarregarEventos();
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar eventos: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
            }
        }

        private void CarregarEventos()
        {
            cmbEvento.Items.Clear();
            _eventos.Clear();

            try
            {
                // Adicionar item de instrução
                cmbEvento.Items.Add("Selecione um evento");

                // Carregar todos os eventos
                var eventos = _eventoService.GetAllEventos();

                if (eventos.Count == 0)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        "Nenhum evento disponível",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                // Adicionar eventos ao combobox
                foreach (var evento in eventos)
                {
                    _eventos.Add(evento);
                    cmbEvento.Items.Add(evento.Nome);
                }

                // NÃO selecionar nenhum evento (deixar com índice -1)
            }
            catch (Exception ex)
            {
                throw new Exception($"Erro ao carregar eventos: {ex.Message}");
            }
        }

        private void CmbEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                cmbCaixa.Items.Clear();

                // Se o índice é 0, significa que selecionou "Selecione um evento"
                if (cmbEvento.SelectedIndex <= 0)
                {
                    cmbCaixa.Items.Add("Selecione uma caixa");
                    return;
                }

                // O índice real é SelectedIndex - 1 (porque há um item de instrução no início)
                int indiceEvento = cmbEvento.SelectedIndex - 1;
                int eventoSelecionado = _eventos[indiceEvento].Id;

                // Carregar caixas (pontos de venda) do evento selecionado
                var caixas = _pontoVendaService.GetCaixasAbertas(eventoSelecionado);

                // Adicionar item de instrução
                cmbCaixa.Items.Add("Selecione uma caixa");

                if (caixas.Count == 0)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        "Nenhuma caixa disponível para este evento",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                // Adicionar caixas ao combobox
                foreach (var caixa in caixas)
                {
                    string descricaoCaixa = $"Caixa #{caixa.NoPontoVenda}";
                    if (!string.IsNullOrWhiteSpace(caixa.DsPontoVenda))
                    {
                        descricaoCaixa += $" - {caixa.DsPontoVenda}";
                    }
                    cmbCaixa.Items.Add(descricaoCaixa);
                }

                // NÃO selecionar nenhuma caixa automaticamente
            }
            catch (Exception ex)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar caixas: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
            }
        }

        private void BtnAbrir_Click(object sender, EventArgs e)
        {
            try
            {
                // Validar seleções
                if (cmbEvento.SelectedIndex <= 0)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        "Por favor, selecione um evento",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                if (cmbCaixa.SelectedIndex <= 0)
                {
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Aviso",
                        "Por favor, selecione uma caixa",
                        TipoDialogo.Aviso,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    return;
                }

                // Obter índices reais (subtraindo 1 por causa do item "Selecione")
                int indiceEvento = cmbEvento.SelectedIndex - 1;
                int indiceCaixa = cmbCaixa.SelectedIndex - 1;

                // Recarregar caixas para obter o objeto correto
                int eventoId = _eventos[indiceEvento].Id;
                var caixasDisp = _pontoVendaService.GetCaixasAbertas(eventoId);
                var pontoVenda = caixasDisp[indiceCaixa];

                // Verificar se FormPDV já está aberta
                if (_formPDVGlobal != null && !_formPDVGlobal.IsDisposed)
                {
                    // Exibir mensagem de aviso
                    DialogoCustomizado dialogo = new DialogoCustomizado(
                        "Informação",
                        "Já existe uma janela de PDV aberta!",
                        TipoDialogo.Informacao,
                        TipoButton.Ok
                    );
                    dialogo.ShowDialog();
                    
                    // Ativar a janela existente
                    _formPDVGlobal.BringToFront();
                    _formPDVGlobal.Activate();
                    
                    // Fechar FormSelecionarPDV (que é modal)
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                else
                {
                    // Abrir FormPDV
                    _formPDVGlobal = new FormPDV(pontoVenda.IdPontoVenda);
                    
                    // Limpar referência quando fechada
                    _formPDVGlobal.FormClosed += (s, args) =>
                    {
                        _formPDVGlobal = null;
                    };
                    
                    _formPDVGlobal.Show();
                    
                    // Trazer FormPDV para frente (acima do menu principal)
                    _formPDVGlobal.BringToFront();
                    _formPDVGlobal.Activate();

                    // Fechar este form
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao abrir PDV: {ex.Message}",
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

        private void BtnFechar_Click(object sender, EventArgs e)
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
