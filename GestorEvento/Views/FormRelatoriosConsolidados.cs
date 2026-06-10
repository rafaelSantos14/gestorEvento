using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestorEvento.Models;
using GestorEvento.Services;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormRelatoriosConsolidados : Form
    {
        private readonly EventoService _eventoService;
        private List<Evento> _eventosCompletos;
        private string _statusFiltro = "Todos";
        
        // Flags para lazy loading - rastrear quais abas já foram carregadas
        private bool _vendaCarregada = false;
        private bool _caixaCarregada = false;
        private bool _cortesiaCarregada = false;
        private bool _reimpressaoCarregada = false;

        // Referências aos UserControls (serão criados e adicionados nas TabPages)
        private RelatorioVendaUserControl ucVendas;
        private RelatorioCaixaUserControl ucCaixa;
        private RelatorioCortesiaUserControl ucCortesia;
        private RelatorioReimpressaoUserControl ucReimpressao;

        public FormRelatoriosConsolidados()
        {
            InitializeComponent();

            _eventoService = new EventoService();
            _eventosCompletos = new List<Evento>();

            DoubleBuffered = true;
            
            // Inicializar todos os UserControls ANTES de qualquer coisa
            InicializarUserControls();
            
            // Carregar eventos ao inicializar
            CarregarEventos();
        }

        private void InicializarUserControls()
        {
            try
            {
                // Criar e adicionar UserControls nas abas com antecedência
                // Isso garante que eles estejam completamente renderizados quando precisar carregar dados
                
                ucVendas = new RelatorioVendaUserControl();
                tabPageVendas.Controls.Clear();
                ucVendas.Dock = DockStyle.Fill;
                ucVendas.BackColor = Color.White;
                tabPageVendas.Controls.Add(ucVendas);
                _ = ucVendas.Handle;
                
                ucCaixa = new RelatorioCaixaUserControl();
                tabPageCaixa.Controls.Clear();
                ucCaixa.Dock = DockStyle.Fill;
                ucCaixa.BackColor = Color.White;
                tabPageCaixa.Controls.Add(ucCaixa);
                _ = ucCaixa.Handle;
                
                ucCortesia = new RelatorioCortesiaUserControl();
                tabPageCortesias.Controls.Clear();
                ucCortesia.Dock = DockStyle.Fill;
                ucCortesia.BackColor = Color.White;
                tabPageCortesias.Controls.Add(ucCortesia);
                _ = ucCortesia.Handle;
                
                ucReimpressao = new RelatorioReimpressaoUserControl();
                tabPageReimpressoes.Controls.Clear();
                ucReimpressao.Dock = DockStyle.Fill;
                ucReimpressao.BackColor = Color.White;
                tabPageReimpressoes.Controls.Add(ucReimpressao);
                _ = ucReimpressao.Handle;
                
                // Processar eventos para garantir que tudo foi renderizado
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao inicializar controles: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CarregarEventos()
        {
            try
            {
                _eventosCompletos = _eventoService.GetAllEventos();
                
                // ComboBox inicia vazio - será preenchido conforme digita no TextBox
                cmbEventoResultados.DataSource = null;
                
                // Inicializar ComboBox de status com "Ativo" selecionado por padrão
                if (cmbStatusFiltro != null && cmbStatusFiltro.Items.Count == 0)
                {
                    cmbStatusFiltro.Items.AddRange(new[] { "Todos", "Ativo", "Encerrado" });
                    cmbStatusFiltro.SelectedItem = "Ativo";  // Selecionar "Ativo" por padrão
                    _statusFiltro = "Ativo";
                    cmbStatusFiltro.SelectedIndexChanged += CmbStatusFiltro_SelectedIndexChanged;
                }
                
                // Carregar eventos ativos ao iniciar
                AtualizarComboEventos();
                
                txtBuscaEvento.Focus();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar eventos: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void TxtBuscaEvento_TextChanged(object sender, EventArgs e)
        {
            AtualizarComboEventos();
        }

        private void CmbEventoResultados_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Quando um evento é selecionado, resetar flags e carregar aba ativa
            if (cmbEventoResultados.SelectedValue != null && cmbEventoResultados.SelectedValue is int idEvento && idEvento > 0)
            {
                // Reset flags para forçar recarregamento
                _vendaCarregada = false;
                _caixaCarregada = false;
                _cortesiaCarregada = false;
                _reimpressaoCarregada = false;
                
                // Carregar a aba ativa (que é a aba atual selecionada)
                CarregarAbaSelecionada(idEvento);
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

            // Se textoBusca está vazio, considerar como "%" (mostrar todos do status selecionado)
            if (string.IsNullOrWhiteSpace(textoBusca))
            {
                textoBusca = "%";
            }

            var eventosFiltrados = FiltrarEventosPorNomeEStatus(textoBusca);

            cmbEventoResultados.DataSource = eventosFiltrados;
            cmbEventoResultados.DisplayMember = "DisplayText";
            cmbEventoResultados.ValueMember = "Id";
            cmbEventoResultados.Refresh();
            cmbEventoResultados.Invalidate();
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
                else if (_statusFiltro == "Ativo" && string.Equals(evento.CdStatus, "Ativo", StringComparison.OrdinalIgnoreCase))
                {
                    passouFiltroStatus = true;
                }
                else if (_statusFiltro == "Encerrado" && string.Equals(evento.CdStatus, "Encerrado", StringComparison.OrdinalIgnoreCase))
                {
                    passouFiltroStatus = true;
                }

                if (!passouFiltroStatus)
                    continue;

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

        private void TabControlRelatorios_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Quando muda de aba, carregar dados da aba se evento estiver selecionado
            if (cmbEventoResultados.SelectedValue != null && cmbEventoResultados.SelectedValue is int idEvento && idEvento > 0)
            {
                CarregarAbaSelecionada(idEvento);
            }
        }

        private void CarregarAbaSelecionada(int idEvento)
        {
            try
            {
                int abaAtiva = tabControlRelatorios.SelectedIndex;

                switch (abaAtiva)
                {
                    case 0: // Aba Vendas
                        if (!_vendaCarregada)
                        {
                            ucVendas.CarregarDados(idEvento);
                            _vendaCarregada = true;
                        }
                        break;

                    case 1: // Aba Caixa
                        if (!_caixaCarregada)
                        {
                            ucCaixa.CarregarDados(idEvento);
                            _caixaCarregada = true;
                        }
                        break;

                    case 2: // Aba Cortesias
                        if (!_cortesiaCarregada)
                        {
                            ucCortesia.CarregarDados(idEvento);
                            _cortesiaCarregada = true;
                        }
                        break;

                    case 3: // Aba Reimpressões
                        if (!_reimpressaoCarregada)
                        {
                            ucReimpressao.CarregarDados(idEvento);
                            _reimpressaoCarregada = true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar aba: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
