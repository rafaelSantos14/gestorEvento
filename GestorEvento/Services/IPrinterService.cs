using System;
using System.Collections.Generic;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    /// <summary>
    /// Interface padrão para serviços de impressão
    /// Implementada por EpsonTM20Service (COM Serial) e WindowsPrinterService (USB/Windows)
    /// </summary>
    public interface IPrinterService
    {
        /// <summary>
        /// Imprime um cupom individual
        /// </summary>
        /// <param name="idImpressao">ID da venda (ou cortesia) ou da reimpressão, impresso acima da data/hora</param>
        /// <param name="isReimpressao">Quando true, rotula o idImpressao como "Reimpressão:" ao invés de "Venda:"</param>
        bool ImprimirCupom(string nomeProduto, int numeroCaixa = 0,
                          string descricaoCaixa = "", decimal preco = 0,
                          int idImpressao = 0, bool isReimpressao = false);

        /// <summary>
        /// Imprime uma venda completa com múltiplos itens
        /// </summary>
        bool ImprimirVenda(int vendaId, List<ItemImpressao> itens, 
                          int numeroCaixa = 0, string descricaoCaixa = "");

        /// <summary>
        /// Imprime uma reimpressão (cupom sem debitar estoque)
        /// </summary>
        bool ImprimirReimpressao(int reimpressaoId, List<ItemImpressao> itens, 
                                int numeroCaixa = 0, string descricaoCaixa = "");

        /// <summary>
        /// Desconecta da impressora
        /// </summary>
        void Desconectar();

        /// <summary>
        /// Libera recursos
        /// </summary>
        void Dispose();
    }
}
