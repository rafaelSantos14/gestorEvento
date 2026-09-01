using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using GestorEvento.Models;
using GestorEvento.Services;

namespace GestorEvento.Views
{
    public partial class RelatorioInscricaoUserControl : UserControl
    {
        private readonly InscricaoEventoService _inscricaoEventoService;
        private List<InscricaoEvento> _todasInscricoes = new List<InscricaoEvento>();
        private bool _carregando;

        public RelatorioInscricaoUserControl()
        {
            InitializeComponent();
            _inscricaoEventoService = new InscricaoEventoService();
            DoubleBuffered = true;
            ConfigurarGrid();

            _carregando = true;
            cmbFiltroStatus.SelectedIndex = 0; // Todos
            _carregando = false;
        }

        public void CarregarDados(int idEvento)
        {
            try
            {
                LimparCards();
                dgvInscricoes.Rows.Clear();

                // Busca tudo de uma vez (cdStatus: null = sem filtro no repositório) - o filtro do
                // combo é aplicado em memória, sem ida ao banco a cada troca de status
                _todasInscricoes = _inscricaoEventoService.Buscar(idEvento, cdStatus: null);

                AtualizarCards();
                AplicarFiltroGrid();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao carregar inscrições: {ex.Message}", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ConfigurarGrid()
        {
            dgvInscricoes.AutoGenerateColumns = false;
            dgvInscricoes.AllowUserToAddRows = false;
            dgvInscricoes.AllowUserToDeleteRows = false;
            dgvInscricoes.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvInscricoes.MultiSelect = false;

            dgvInscricoes.Columns.Clear();
            dgvInscricoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            dgvInscricoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cpf", HeaderText = "CPF/CNPJ", Width = 140 });
            dgvInscricoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Celular", HeaderText = "Celular", Width = 130 });
            dgvInscricoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "E-mail", Width = 200 });
            dgvInscricoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Qtde", HeaderText = "Qtde. Antecipada", Width = 130 });
            dgvInscricoes.Columns.Add(new DataGridViewTextBoxColumn { Name = "Status", HeaderText = "Status", Width = 100 });

            dgvInscricoes.DefaultCellStyle.ForeColor = Color.Black;
            dgvInscricoes.DefaultCellStyle.BackColor = Color.White;
            dgvInscricoes.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInscricoes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvInscricoes.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvInscricoes.EnableHeadersVisualStyles = false;
        }

        private void AtualizarCards()
        {
            int total = _todasInscricoes.Count;
            int pendentes = _todasInscricoes.Count(i => i.IsPendente);
            int retiradas = total - pendentes;

            lblTotalValor.Text = total.ToString();
            lblPendenteValor.Text = pendentes.ToString();
            lblRetiradoValor.Text = retiradas.ToString();
        }

        private void LimparCards()
        {
            lblTotalValor.Text = "-";
            lblPendenteValor.Text = "-";
            lblRetiradoValor.Text = "-";
        }

        private void CmbFiltroStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_carregando)
                AplicarFiltroGrid();
        }

        private void AplicarFiltroGrid()
        {
            string filtro = cmbFiltroStatus.SelectedItem?.ToString() ?? "Todos";

            IEnumerable<InscricaoEvento> inscricoesFiltradas = _todasInscricoes;
            if (filtro == "Pendente")
                inscricoesFiltradas = _todasInscricoes.Where(i => i.IsPendente);
            else if (filtro == "Retirado")
                inscricoesFiltradas = _todasInscricoes.Where(i => !i.IsPendente);

            dgvInscricoes.Rows.Clear();
            foreach (var inscricao in inscricoesFiltradas.OrderBy(i => i.NomeParticipante))
            {
                dgvInscricoes.Rows.Add(
                    inscricao.NomeParticipante,
                    FormatarCpfCnpj(inscricao.CpfCnpj),
                    FormatarCelular(inscricao.Celular),
                    inscricao.Email,
                    inscricao.QtdeAntecipada,
                    inscricao.CdStatus
                );
            }
        }

        private string FormatarCpfCnpj(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            if (valor.Length == 11)
                return $"{valor.Substring(0, 3)}.{valor.Substring(3, 3)}.{valor.Substring(6, 3)}-{valor.Substring(9, 2)}";

            if (valor.Length == 14)
                return $"{valor.Substring(0, 2)}.{valor.Substring(2, 3)}.{valor.Substring(5, 3)}/{valor.Substring(8, 4)}-{valor.Substring(12, 2)}";

            return valor;
        }

        private string FormatarCelular(string valor)
        {
            if (string.IsNullOrEmpty(valor))
                return valor;

            if (valor.Length == 11)
                return $"({valor.Substring(0, 2)}) {valor.Substring(2, 5)}-{valor.Substring(7, 4)}";

            if (valor.Length == 10)
                return $"({valor.Substring(0, 2)}) {valor.Substring(2, 4)}-{valor.Substring(6, 4)}";

            return valor;
        }
    }
}
