using System;

namespace GestorEvento.Utilities
{
    /// <summary>
    /// Helper simplificado para PrintServer (Console)
    /// Versão reduzida do UiHelper que funciona em ambiente console
    /// </summary>
    public static class UiHelper
    {
        public static void ExibirErro(string titulo, string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"✗ [{titulo}] {mensagem}");
            Console.ResetColor();
        }

        public static void ExibirAviso(string titulo, string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine($"⚠ [{titulo}] {mensagem}");
            Console.ResetColor();
        }

        public static void ExibirSucesso(string titulo, string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"✓ [{titulo}] {mensagem}");
            Console.ResetColor();
        }

        public static void ExibirInfo(string titulo, string mensagem)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine($"ℹ [{titulo}] {mensagem}");
            Console.ResetColor();
        }
    }
}
