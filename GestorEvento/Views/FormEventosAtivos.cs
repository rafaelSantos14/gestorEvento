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
    public partial class FormEventosAtivos : Form
    {
        private EventoService _eventoService;
        private int _eventoIdSelecionado = 0;
        private bool _isDragging = false;
        private Point _dragPoint;

        public FormEventosAtivos()
        {
            InitializeComponent();
            
            // Inicializar serviços
            _eventoService = new EventoService();
            
            // Configurar componentes
            ConfigurarDataGridView();
            
            // Carregar dados
            CarregarEventos();
        }

        private void ConfigurarDataGridView()
        {
            dgvEventos.Columns.Clear();

            // Coluna ID (hidden)
            var colId = new DataGridViewTextBoxColumn
            {
                Name = "ID",
                HeaderText = "ID",
                Width = 50,
                Visible = false,
                ReadOnly = true
            };
            dgvEventos.Columns.Add(colId);

            // Coluna Nome
            var colNome = new DataGridViewTextBoxColumn
            {
                Name = "Nome",
                HeaderText = "Evento",
                Width = 250,
                ReadOnly = true
            };
            dgvEventos.Columns.Add(colNome);

            // Coluna Data
            var colData = new DataGridViewTextBoxColumn
            {
                Name = "DataEvento",
                HeaderText = "Data",
                Width = 120,
                ReadOnly = true
            };
            dgvEventos.Columns.Add(colData);

            // Coluna Status
            var colStatus = new DataGridViewTextBoxColumn
            {
                Name = "Status",
                HeaderText = "Status",
                Width = 100,
                ReadOnly = true
            };
            dgvEventos.Columns.Add(colStatus);

            // Configurar cores
            dgvEventos.DefaultCellStyle.ForeColor = Color.Black;
            dgvEventos.DefaultCellStyle.BackColor = Color.White;
            dgvEventos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvEventos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210); // Azul Material
            dgvEventos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245); // Cinza claro
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
                    dgvEventos.Rows.Add(
                        evento.Id,
                        evento.Nome,
                        evento.DataEvento.ToString("dd/MM/yyyy"),
                        "Ativo" // TODO: substituir por campo de status quando houver
                    );
                }
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

        // ==================== EVENT HANDLERS ====================

        private void btnAbrirCaixa_Click(object sender, EventArgs e)
        {
            if (dgvEventos.SelectedRows.Count == 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Selecione um evento para abrir caixa",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            _eventoIdSelecionado = (int)dgvEventos.SelectedRows[0].Cells["ID"].Value;

            // Abrir FormAbrirCaixa como dialog
            FormAbrirCaixa formAbrirCaixa = new FormAbrirCaixa(_eventoIdSelecionado);
            if (formAbrirCaixa.ShowDialog() == DialogResult.OK)
            {
                // Caixa aberto com sucesso
                DialogoCustomizado sucesso = new DialogoCustomizado(
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
            if (dgvEventos.SelectedRows.Count == 0)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Selecione um evento para registrar venda",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            _eventoIdSelecionado = (int)dgvEventos.SelectedRows[0].Cells["ID"].Value;

            // Abrir FormSelecionarPontoVenda como dialog
            FormSelecionarPontoVenda formSelecionarPontoVenda = new FormSelecionarPontoVenda(_eventoIdSelecionado);
            if (formSelecionarPontoVenda.ShowDialog() == DialogResult.OK)
            {
                // Venda registrada com sucesso
                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    "Venda registrada com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();
            }
        }

        // ==================== TITLE BAR HANDLERS ====================

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
    }
}
