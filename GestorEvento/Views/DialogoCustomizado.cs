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

namespace GestorEvento.Views
{
    public partial class DialogoCustomizado : Form
    {
        public TipoButton TipoBotao { get; set; }

        public DialogoCustomizado(string titulo, string mensagem, TipoDialogo tipo = TipoDialogo.Informacao, TipoButton tipoButton = TipoButton.Ok)
        {
            InitializeComponent();            
            
            this.Text = titulo;          
            lblMensagem.Text = mensagem;
            this.TipoBotao = tipoButton;
            
            // Alterar ícone e cor conforme tipo
            AlterarPorTipo(tipo);
            ConfigurarBotoes(tipoButton);
        }

        private void AlterarPorTipo(TipoDialogo tipo)
        {
            switch (tipo)
            {
                case TipoDialogo.Sucesso:
                    lblIcone.Text = "✓";
                    lblIcone.ForeColor = Color.Green;
                    break;
                case TipoDialogo.Erro:
                    lblIcone.Text = "✗";
                    lblIcone.ForeColor = Color.Red;
                    break;
                case TipoDialogo.Aviso:
                    lblIcone.Text = "⚠";
                    lblIcone.ForeColor = Color.Orange;
                    break;
                case TipoDialogo.Informacao:
                default:
                    lblIcone.Text = "ℹ";
                    lblIcone.ForeColor = Color.FromArgb(25, 118, 210); // Azul Material Design
                    break;
            }
        }

        private void ConfigurarBotoes(TipoButton tipoButton)
        {
            switch (tipoButton)
            {
                case TipoButton.Ok:
                    btnOk.Text = "OK";
                    btnOk.Visible = true;
                    btnNao.Visible = false;
                    // Posicionar botão OK no centro
                    btnOk.Location = new Point(125, 130);
                    break;
                    
                case TipoButton.SimNao:
                    btnOk.Text = "SIM";
                    btnOk.Visible = true;
                    btnNao.Text = "NÃO";
                    btnNao.Visible = true;
                    // Posicionar botões lado a lado
                    btnOk.Location = new Point(55, 130);
                    btnNao.Location = new Point(195, 130);
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNao_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }
    }

    public enum TipoDialogo
    {
        Informacao,
        Sucesso,
        Erro,
        Aviso
    }

    public enum TipoButton
    {
        Ok,
        SimNao
    }
}
