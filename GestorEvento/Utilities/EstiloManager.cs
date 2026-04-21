using System;
using System.Drawing;

namespace GestorEvento.Utilities
{
    public static class EstiloManager
    {
        // Cores Botões
        public static Color CorBotaoSalvar => Color.FromArgb(56, 142, 60);       // Verde escuro
        public static Color CorBotaoLimpar => Color.FromArgb(158, 158, 158);     // Cinza
        public static Color CorBotaoDeletar => Color.FromArgb(244, 67, 54);      // Vermelho
        public static Color CorBotaoInfo => Color.FromArgb(25, 118, 210);        // Azul
        public static Color CorBotaoAviso => Color.FromArgb(255, 152, 0);        // Laranja
        
        // Cor texto padrão
        public static Color CorTexto => Color.White;
        
        // Cores Header/Painel
        public static Color CorHeaderAzul => Color.FromArgb(25, 118, 210);
        public static Color CorHeaderVerde => Color.Green;
        public static Color CorHeaderVermelho => Color.Red;
        public static Color CorHeaderLaranja => Color.Orange;
        
        // Aplicar estilos aos botões
        public static void AplicarEstiloSalvar(System.Windows.Forms.Button btn)
        {
            AplicarEstilo(btn, CorBotaoSalvar);
        }
        
        public static void AplicarEstiloLimpar(System.Windows.Forms.Button btn)
        {
            AplicarEstilo(btn, CorBotaoLimpar);
        }
        
        public static void AplicarEstiloDeletar(System.Windows.Forms.Button btn)
        {
            AplicarEstilo(btn, CorBotaoDeletar);
        }
        
        public static void AplicarEstiloInfo(System.Windows.Forms.Button btn)
        {
            AplicarEstilo(btn, CorBotaoInfo);
        }
        
        public static void AplicarEstiloAviso(System.Windows.Forms.Button btn)
        {
            AplicarEstilo(btn, CorBotaoAviso);
        }
        
        // Método genérico
        private static void AplicarEstilo(System.Windows.Forms.Button btn, Color cor)
        {
            btn.BackColor = cor;
            btn.ForeColor = CorTexto;
            btn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
        }
    }
}
