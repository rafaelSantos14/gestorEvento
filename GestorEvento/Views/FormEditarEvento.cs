using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestorEvento.Services;
using GestorEvento.Models;
using GestorEvento.Utilities;

namespace GestorEvento.Views
{
    public partial class FormEditarEvento : Form
    {
        private EventoService _service;
        private int _eventoId;

        public FormEditarEvento(int eventoId)
        {
            InitializeComponent();
            _eventoId = eventoId;
            _service = new EventoService();

            // Aplicar estilos dos botões
            EstiloManager.AplicarEstiloSalvar(btnSalvar);
            EstiloManager.AplicarEstiloLimpar(btnCancelar);

            // Configurar DateTimePicker (inicialmente vazio)
            dtpData.Format = DateTimePickerFormat.Custom;
            dtpData.CustomFormat = " ";
            dtpData.Value = DateTime.Now;

            // Configurar MaskedTextBox
            mtbData.Mask = "00/00/0000";
            mtbData.ValidatingType = typeof(DateTime);

            // Eventos de sincronização
            dtpData.ValueChanged += DtpData_ValueChanged;
            mtbData.TextChanged += MtbData_TextChanged;

            // Carregar dados do evento
            CarregarEvento();
        }

        private void CarregarEvento()
        {
            try
            {
                var evento = _service.GetEventoById(_eventoId);

                if (evento == null)
                {
                    DialogoCustomizado erro = new DialogoCustomizado(
                        "Erro",
                        "Evento não encontrado.",
                        TipoDialogo.Erro,
                        TipoButton.Ok
                    );
                    erro.ShowDialog();
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                    return;
                }

                // Preencher campos com dados atuais
                txtNome.Text = evento.Nome;
                dtpData.Value = evento.DataEvento;
                mtbData.Text = evento.DataEvento.ToString("dd/MM/yyyy");
                dtpData.Format = DateTimePickerFormat.Short;

                // Focar no campo Nome para edição imediata
                txtNome.Focus();
                txtNome.SelectAll();
            }
            catch (Exception ex)
            {
                DialogoCustomizado erro = new DialogoCustomizado(
                    "Erro",
                    $"Erro ao carregar evento: {ex.Message}",
                    TipoDialogo.Erro,
                    TipoButton.Ok
                );
                erro.ShowDialog();
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void btnSalvar_Click(object sender, EventArgs e)
        {
            // Validar nome
            if (string.IsNullOrWhiteSpace(txtNome.Text))
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Nome do evento não pode ser vazio",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            if (txtNome.Text.Length > 255)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Nome do evento não pode ter mais de 255 caracteres",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            // Validar data
            DateTime? dataEvento = null;
            if (!string.IsNullOrWhiteSpace(mtbData.Text.Trim('/')))
            {
                if (DateTime.TryParse(mtbData.Text, out DateTime dt))
                {
                    dataEvento = dt;
                }
            }

            if (!dataEvento.HasValue)
            {
                DialogoCustomizado dialogo = new DialogoCustomizado(
                    "Aviso",
                    "Data é obrigatória e deve ser válida",
                    TipoDialogo.Aviso,
                    TipoButton.Ok
                );
                dialogo.ShowDialog();
                return;
            }

            // Criar objeto evento com dados atualizados
            var evento = new Evento
            {
                Id = _eventoId,
                Nome = txtNome.Text.Trim(),
                DataEvento = dataEvento.Value
            };

            // Tentar atualizar no banco
            if (_service.UpdateEvento(evento))
            {
                DialogoCustomizado sucesso = new DialogoCustomizado(
                    "Sucesso",
                    "Evento atualizado com sucesso!",
                    TipoDialogo.Sucesso,
                    TipoButton.Ok
                );
                sucesso.ShowDialog();
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void DtpData_ValueChanged(object sender, EventArgs e)
        {
            // Quando seleciona uma data no calendário, atualiza o MaskedTextBox
            dtpData.Format = DateTimePickerFormat.Short;
            mtbData.Text = dtpData.Value.ToString("dd/MM/yyyy");
        }

        private void MtbData_TextChanged(object sender, EventArgs e)
        {
            // Quando digita no MaskedTextBox
            string texto = mtbData.Text;

            if (string.IsNullOrWhiteSpace(texto.Trim('/')))
            {
                // Se apagou, deixa o DateTimePicker vazio
                dtpData.Format = DateTimePickerFormat.Custom;
                dtpData.CustomFormat = " ";
            }
            else if (texto.Length == 10 && texto[2] == '/' && texto[5] == '/') // Máscara completa: DD/MM/YYYY
            {
                // Apenas tenta converter quando a máscara está completa
                try
                {
                    if (DateTime.TryParse(texto, out DateTime dt) && dt >= dtpData.MinDate && dt <= dtpData.MaxDate)
                    {
                        dtpData.Value = dt;
                        dtpData.Format = DateTimePickerFormat.Short;
                    }
                }
                catch
                {
                    // Ignora erros de conversão de datas inválidas
                }
            }
        }
    }
}
