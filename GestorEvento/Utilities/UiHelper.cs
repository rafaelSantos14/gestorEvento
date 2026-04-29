using System.Windows.Forms;
using GestorEvento.Views;

namespace GestorEvento.Utilities
{
    /// <summary>
    /// Classe helper para exibir mensagens de UI de forma centralizada
    /// Encapsula a lógica de tentativa com DialogoCustomizado e fallback para MessageBox
    /// </summary>
    public static class UiHelper
    {
        /// <summary>
        /// Exibe uma mensagem de erro ao usuário
        /// </summary>
        public static void ExibirErro(string titulo, string mensagem)
        {
            try
            {
                var dialogo = new DialogoCustomizado(titulo, mensagem, TipoDialogo.Erro, TipoButton.Ok);
                dialogo.ShowDialog();
            }
            catch
            {
                MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// Exibe uma mensagem de aviso ao usuário
        /// </summary>
        public static void ExibirAviso(string titulo, string mensagem)
        {
            try
            {
                var dialogo = new DialogoCustomizado(titulo, mensagem, TipoDialogo.Aviso, TipoButton.Ok);
                dialogo.ShowDialog();
            }
            catch
            {
                MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Exibe uma mensagem de sucesso ao usuário
        /// </summary>
        public static void ExibirSucesso(string titulo, string mensagem)
        {
            try
            {
                var dialogo = new DialogoCustomizado(titulo, mensagem, TipoDialogo.Sucesso, TipoButton.Ok);
                dialogo.ShowDialog();
            }
            catch
            {
                MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        /// <summary>
        /// Exibe uma mensagem de informação ao usuário
        /// </summary>
        public static void ExibirInfo(string titulo, string mensagem)
        {
            try
            {
                var dialogo = new DialogoCustomizado(titulo, mensagem, TipoDialogo.Informacao, TipoButton.Ok);
                dialogo.ShowDialog();
            }
            catch
            {
                MessageBox.Show(mensagem, titulo, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
