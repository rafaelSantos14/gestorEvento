using System;
using System.Drawing;
using System.Windows.Forms;
using GestorEvento.Utilities;
using GestorEvento.Services;
using GestorEvento.Models;

namespace GestorEvento.Views
{
    public partial class FormEventosAtivos : Form
    {
        private EventoService _eventoService;
        private int _eventoIdSelecionado = 0;
        private bool _isDragging = false;
        private Point _dragPoint;

        public FormEventosAtivos()
        {
            InitializeComponent();

            _eventoService = new EventoService();
            ConfigurarDataGridView();
            dgvEventos.SelectionChanged += DgvEventos_SelectionChanged;
            CarregarEventos();
        }

        private void ConfigurarDataGridView()
        {
            dgvEventos.Columns.Clear();

            dgvEventos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                Width = 50,
                Visible = false,
                ReadOnly = true
            });

            dgvEventos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Evento",
                Width = 250,
                ReadOnly = true
            });

            dgvEventos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "DataEvento",
                HeaderText = "Data",
                Width = 120,
                ReadOnly = true
            });

            dgvEventos.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 120,
                ReadOnly = true
            });

            dgvEventos.DefaultCellStyle.ForeColor = Color.Black;
            dgvEventos.DefaultCellStyle.BackColor = Color.White;
            dgvEventos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEventos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvEventos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvEventos.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvEventos.AllowUserToAddRows = false;
            dgvEventos.ReadOnly = true;
        }

        private void CarregarEventos()
        {
            dgvEventos.Rows.Clear();

            try
            {
                var eventos = _eventoService.GetAllEventos();
                foreach (var evento in eventos)
                {
                    int rowIndex = dgvEventos.Rows.Add(
                        evento.Id,
                        evento.Nome,
                        evento.DataEvento.ToString("dd/MM/yyyy"),
                        string.IsNullOrWhiteSpace(evento.CdStatus) ? Evento.StatusAtivo : evento.CdStatus
                    );

                    if (evento.IsEncerrado)
                    {
                        dgvEventos.Rows[rowIndex].DefaultCellStyle.ForeColor = Color.FromArgb(183, 28, 28);
                    }
                }

                AtualizarEstadoBotoes();
            }
            catch (Exception ex)
            {
                var dialogo = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar eventos: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
            }
        }

        private void DgvEventos_SelectionChanged(object sender, EventArgs e)
        {
            AtualizarEstadoBotoes();
        }

        private Evento GetEventoSelecionado()
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                return null;
            }

            var row = dgvEventos.SelectedRows[0];
            return new Evento
            {
                Id = Convert.ToInt32(row.Cells["ID"].Value),
                Nome = row.Cells["Nome"].Value?.ToString(),
                DataEvento = DateTime.TryParse(row.Cells["DataEvento"].Value?.ToString(), out DateTime dt) ? dt : DateTime.MinValue,
                CdStatus = row.Cells["Status"].Value?.ToString()
            };
        }

        private bool EventoSelecionadoEstaEncerrado()
        {
            var evento = GetEventoSelecionado();
            return evento != null && evento.IsEncerrado;
        }

        private void AtualizarEstadoBotoes()
        {
            bool temSelecao = dgvEventos.SelectedRows.Count > 0;
            bool encerrado = EventoSelecionadoEstaEncerrado();

            btnAbrirCaixa.Enabled = temSelecao && !encerrado;
            btnCaixas.Enabled = temSelecao;
        }

        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            var evento = GetEventoSelecionado();
            if (evento == null)
            {
                var dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Selecione um evento para abrir caixa",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            if (evento.IsEncerrado)
            {
                var dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Evento encerrado. Não é possível abrir caixa.",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            _eventoIdSelecionado = evento.Id;

            var formAbrirCaixa = new FormAbrirCaixa(_eventoIdSelecionado);
            if (formAbrirCaixa.ShowDialog() == DialogResult.OK)
            {
                var sucesso = new DialogoCustomizado(
                    "Sucesso",
                    "Caixa aberto com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();
            }
        }

        private void btnRegistrarVenda_Click(object sender, EventArgs e)
        {
            var evento = GetEventoSelecionado();
            if (evento == null)
            {
                var dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Selecione um evento para acessar os caixas",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            _eventoIdSelecionado = evento.Id;
            var formSelecionarPontoVenda = new FormSelecionarPontoVenda(_eventoIdSelecionado);
            formSelecionarPontoVenda.ShowDialog();
        }

        private void BtnFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnMinimizar_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        private void PanelTitulo_MouseDown(object sender, MouseEventArgs e)
        {
            if (WindowState != FormWindowState.Maximized)
            {
                _isDragging = true;
                _dragPoint = e.Location;
            }
        }

        private void PanelTitulo_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging)
            {
                Point novaLocacao = Location;
                novaLocacao.X += e.X - _dragPoint.X;
                novaLocacao.Y += e.Y - _dragPoint.Y;
                Location = novaLocacao;
            }
        }

        private void PanelTitulo_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }
    }
}
