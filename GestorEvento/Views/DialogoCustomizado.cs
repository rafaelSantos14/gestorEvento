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
            
            // Adaptar tamanho do formulário à mensagem
            AdaptarTamanho();
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

        private void AdaptarTamanho()
        {
            // Dimensões mínimas e máximas
            int larguraMinima = 300;
            int larguraMaxima = 600;
            int alturaMinima = 150;
            
            // Medir o tamanho da mensagem (considerando menos espaço para o ícone)
            Size tamanhoMensagem = TextRenderer.MeasureText(
                lblMensagem.Text,
                lblMensagem.Font,
                new Size(larguraMaxima - 120, int.MaxValue),
                TextFormatFlags.WordBreak
            );

            // Calcular nova largura (com padding)
            int novaLargura = Math.Max(
                Math.Min(tamanhoMensagem.Width + 130, larguraMaxima),
                larguraMinima
            );

            // Calcular nova altura baseada no conteúdo
            int novaAltura = Math.Max(
                tamanhoMensagem.Height + 160, // +160 para ícone, padding extra e botões
                alturaMinima
            );

            // Posicionar o ícone à esquerda (espaço dedicado)
            lblIcone.Location = new Point(15, 25);
            lblIcone.AutoSize = true;

            // Ajustar o label de mensagem com AutoSize, com mais espaço para o ícone
            lblMensagem.AutoSize = false;
            lblMensagem.Size = new Size(novaLargura - 110, tamanhoMensagem.Height);
            lblMensagem.Location = new Point(80, 25);

            // Ajustar o formulário
            this.ClientSize = new Size(novaLargura, novaAltura);
            this.MaximumSize = new Size(larguraMaxima + 30, novaAltura + 40);

            // Reposicionar botões
            int posYBotoes = novaAltura - 50;
            
            if (this.TipoBotao == TipoButton.Ok)
            {
                btnOk.Location = new Point((novaLargura - btnOk.Width) / 2, posYBotoes);
            }
            else if (this.TipoBotao == TipoButton.SimNao)
            {
                int espacoTotal = novaLargura - 40;
                int espacoBotao = 80;
                int espacoEntre = (espacoTotal - (espacoBotao * 2)) / 3;
                
                btnOk.Location = new Point(espacoEntre, posYBotoes);
                btnNao.Location = new Point(espacoEntre + espacoBotao + espacoEntre, posYBotoes);
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
