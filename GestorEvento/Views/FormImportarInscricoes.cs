using System;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using GestorEvento.Models;
using GestorEvento.Services;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormImportarInscricoes : Form
    {
        private readonly InscricaoEventoService _service;
        private readonly int _idEvento;
        private string _caminhoArquivo;
        private PreparacaoImportacaoInscricao _preparacaoPendente;

        public FormImportarInscricoes(int idEvento)
        {
            InitializeComponent();
            _idEvento = idEvento;
            _service = new InscricaoEventoService();

            EstiloManager.AplicarEstiloInfo(btnSelecionarArquivo);
            EstiloManager.AplicarEstiloSalvar(btnImportar);
            EstiloManager.AplicarEstiloSalvar(btnConfirmarImportacao);
            EstiloManager.AplicarEstiloLimpar(btnFechar);

            ConfigurarGridSumidos();
        }

        private void ConfigurarGridSumidos()
        {
            dgvRegistrosSumidos.AutoGenerateColumns = false;
            dgvRegistrosSumidos.Columns.Clear();
            dgvRegistrosSumidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Id", HeaderText = "ID", Visible = false });
            dgvRegistrosSumidos.Columns.Add(new DataGridViewCheckBoxColumn { Name = "Excluir", HeaderText = "Excluir?", Width = 70 });
            dgvRegistrosSumidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Nome", HeaderText = "Nome", Width = 180, ReadOnly = true });
            dgvRegistrosSumidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Cpf", HeaderText = "CPF/CNPJ", Width = 105, ReadOnly = true });
            dgvRegistrosSumidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Celular", HeaderText = "Celular", Width = 95, ReadOnly = true });
            dgvRegistrosSumidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Email", HeaderText = "E-mail", Width = 115, ReadOnly = true });
            dgvRegistrosSumidos.Columns.Add(new DataGridViewTextBoxColumn { Name = "Qtde", HeaderText = "Qtde.", Width = 55, ReadOnly = true });

            dgvRegistrosSumidos.DefaultCellStyle.ForeColor = Color.Black;
            dgvRegistrosSumidos.DefaultCellStyle.BackColor = Color.White;
            dgvRegistrosSumidos.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvRegistrosSumidos.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(25, 118, 210);
            dgvRegistrosSumidos.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(245, 245, 245);
            dgvRegistrosSumidos.EnableHeadersVisualStyles = false;
        }

        private void btnSelecionarArquivo_Click(object sender, EventArgs e)
        {
            using (var dialogo = new OpenFileDialog())
            {
                dialogo.Filter = "Planilhas Excel (*.xlsx;*.xls)|*.xlsx;*.xls";
                dialogo.Title = "Selecionar planilha de inscrições";

                if (dialogo.ShowDialog(this) == DialogResult.OK)
                {
                    _caminhoArquivo = dialogo.FileName;
                    txtCaminhoArquivo.Text = _caminhoArquivo;
                }
            }
        }

        private void btnImportar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(_caminhoArquivo))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Selecione um arquivo de planilha antes de importar",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            btnImportar.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                _preparacaoPendente = _service.PrepararImportacao(_idEvento, _caminhoArquivo);

                if (_preparacaoPendente.RegistrosSumidos.Count == 0)
                {
                    FinalizarImportacao(new System.Collections.Generic.List<int>());
                }
                else
                {
                    MostrarRegistrosSumidos(_preparacaoPendente.RegistrosSumidos);
                }
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao importar planilha: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
                btnImportar.Enabled = true;
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }
        }

        private void MostrarRegistrosSumidos(System.Collections.Generic.List<InscricaoEvento> sumidos)
        {
            dgvRegistrosSumidos.Rows.Clear();
            foreach (var registro in sumidos)
            {
                dgvRegistrosSumidos.Rows.Add(registro.Id, false, registro.NomeParticipante, registro.CpfCnpj, FormatarCelular(registro.Celular), registro.Email, registro.QtdeAntecipada);
            }

            lblAvisoSumidos.Visible = true;
            btnMarcarTodosSumidos.Enabled = true;
            btnDesmarcarTodosSumidos.Enabled = true;
            dgvRegistrosSumidos.Enabled = true;
            btnConfirmarImportacao.Visible = true;
            btnConfirmarImportacao.Enabled = true;
        }

        private void btnMarcarTodosSumidos_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvRegistrosSumidos.Rows)
                row.Cells["Excluir"].Value = true;
        }

        private void btnDesmarcarTodosSumidos_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvRegistrosSumidos.Rows)
                row.Cells["Excluir"].Value = false;
        }

        private void btnConfirmarImportacao_Click(object sender, EventArgs e)
        {
            dgvRegistrosSumidos.EndEdit();

            var idsParaExcluir = dgvRegistrosSumidos.Rows
                .Cast<DataGridViewRow>()
                .Where(row => row.Cells["Excluir"].Value != null && Convert.ToBoolean(row.Cells["Excluir"].Value))
                .Select(row => Convert.ToInt32(row.Cells["Id"].Value))
                .ToList();

            FinalizarImportacao(idsParaExcluir);
        }

        private void FinalizarImportacao(System.Collections.Generic.List<int> idsParaExcluir)
        {
            btnConfirmarImportacao.Enabled = false;
            this.Cursor = Cursors.WaitCursor;

            try
            {
                ImportacaoInscricaoResultado resultado = _service.ConfirmarImportacao(_idEvento, _preparacaoPendente.ItensParaGravar, idsParaExcluir);

                resultado.TotalLinhasLidas = _preparacaoPendente.TotalLinhasLidas;
                resultado.TotalIgnoradasInvalidas = _preparacaoPendente.TotalIgnoradasInvalidas;
                resultado.Avisos.InsertRange(0, _preparacaoPendente.Avisos);

                ExibirResultado(resultado);

                bool teveAvisos = resultado.Avisos.Count > 0;
                DialogoCustomizado dialogoFinal = new DialogoCustomizado(
                    teveAvisos ? "Importação concluída com avisos" : "Sucesso",
                    $"Inscrições novas: {resultado.TotalInseridas}\n" +
                    $"Inscrições atualizadas: {resultado.TotalAtualizadas}\n" +
                    $"Inscrições excluídas: {resultado.TotalExcluidas}\n" +
                    $"Linhas ignoradas (inválidas): {resultado.TotalIgnoradasInvalidas}\n" +
                    $"Linhas ignoradas (já retiradas): {resultado.TotalIgnoradasJaRetiradas}",
                    teveAvisos ? TipoDialogo.Aviso : TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                dialogoFinal.ShowDialog();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao importar planilha: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
            }
            finally
            {
                _preparacaoPendente = null;
                lblAvisoSumidos.Visible = false;
                btnMarcarTodosSumidos.Enabled = false;
                btnDesmarcarTodosSumidos.Enabled = false;
                dgvRegistrosSumidos.Rows.Clear();
                dgvRegistrosSumidos.Enabled = false;
                btnConfirmarImportacao.Visible = false;
                btnImportar.Enabled = true;
                this.Cursor = Cursors.Default;
            }
        }

        private void ExibirResultado(ImportacaoInscricaoResultado resultado)
        {
            var sb = new StringBuilder();
            sb.AppendLine($"Linhas lidas: {resultado.TotalLinhasLidas}");
            sb.AppendLine($"Inseridas: {resultado.TotalInseridas}");
            sb.AppendLine($"Atualizadas: {resultado.TotalAtualizadas}");
            sb.AppendLine($"Excluídas: {resultado.TotalExcluidas}");
            sb.AppendLine($"Ignoradas (inválidas): {resultado.TotalIgnoradasInvalidas}");
            sb.AppendLine($"Ignoradas (já retiradas): {resultado.TotalIgnoradasJaRetiradas}");

            if (resultado.Avisos.Count > 0)
            {
                sb.AppendLine();
                sb.AppendLine("Avisos:");
                foreach (var aviso in resultado.Avisos)
                {
                    sb.AppendLine($"- {aviso}");
                }
            }

            txtResultado.Text = sb.ToString();
        }

        private void btnFechar_Click(object sender, EventArgs e)
        {
            this.Close();
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
