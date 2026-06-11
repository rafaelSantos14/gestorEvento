using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Text;
using System.Threading;
using System.Configuration;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    /// <summary>
    /// Serviço para impressão via Sistema Windows com suporte a ESC/POS
    /// Implementa IPrinterService para impressoras USB/Windows (TOMATE MDK-080, HP, Zebra, etc)
    /// Envia comandos ESC/POS através da impressora configurada no Windows
    /// SEM timings (Win32 API é síncrono, Print Spooler gerencia fila)
    /// </summary>
    public class WindowsPrinterService : IPrinterService
    {
        private readonly string _printerName;

        public WindowsPrinterService(string printerName = null)
        {
            // Se não informar impressora, usa a padrão do Windows ou a do App.config
            if (string.IsNullOrWhiteSpace(printerName))
            {
                _printerName = ConfigurationManager.AppSettings["WindowsPrinterName"] ?? "MDK-080";
            }
            else
            {
                _printerName = printerName;
            }
        }

        /// <summary>
        /// Imprime 2 cupons de teste com corte de papel entre eles
        /// Inclui layout completo: produto, valor, data, hora, redes sociais
        /// </summary>
        public bool ImprimirTesteCom2Cupons()
        {
            try
            {
                // Construir os dados ESC/POS para 2 cupons
                byte[] dadosImpressao = ConstruirDados2Cupons();

                // Enviar para impressora
                if (EnviarParaImpressora(dadosImpressao))
                {
                    UiHelper.ExibirSucesso("Sucesso", "Teste de impressão enviado com sucesso!\n\n" +
                        "✓ 2 Cupons impressos\n" +
                        "✓ Nome do Produto: Cerveja Premium - Long Neck\n" +
                        "✓ Valor: R$ 15.50\n" +
                        "✓ Informações de Caixa\n" +
                        "✓ Data e Hora\n" +
                        "✓ Redes Sociais\n" +
                        "✓ Corte de Papel Automático");
                    return true;
                }
                else
                {
                    UiHelper.ExibirErro("Erro", "Falha ao enviar dados para a impressora.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir teste: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Constrói os dados ESC/POS para 2 cupons completos com corte
        /// </summary>
        private byte[] ConstruirDados2Cupons()
        {
            // Usar StringBuilder para construir os comandos
            var sb = new StringBuilder();

            // ===== CUPOM 1 =====
            sb.Append(ConstrutorCupom("CUPOM #001"));

            // Corte de papel após cupom 1
            sb.Append(ComandoCorte());

            // ===== CUPOM 2 =====
            sb.Append(ConstrutorCupom("CUPOM #002"));

            // Corte de papel após cupom 2
            sb.Append(ComandoCorte());

            // ⏳ ESPAÇO FINAL CRÍTICO - Impressoras térmicas precisam de papel após o corte
            // Sem este espaço, o último corte fica pendente sem executar
            sb.Append("\n"); // quebra de linha adicional para garantir processamento do corte final

            // Converter string (com bytes especiais) para byte array
            return ConvertStringToByteArray(sb.ToString());
        }

        /// <summary>
        /// Constrói um cupom individual com formatação ESC/POS completa
        /// Segue o mesmo layout do EpsonTM20Service
        /// </summary>
        private string ConstrutorCupom(string titulo)
        {
            var sb = new StringBuilder();

            // ============ FASE 1: RESET E INICIALIZAÇÃO ============
            // Reset da impressora
            sb.Append("\x1B\x40"); // ESC @

            // Configurar code page para Windows-1252
            sb.Append("\x1B\x74\x10"); // ESC t 16 (Windows-1252 com acentuação)

            // ============ FASE 2: CONFIGURAÇÕES INICIAIS ============
            // Bold ON
            sb.Append("\x1B\x45\x01"); // ESC E 1
            
            // Font 2x (maior)
            sb.Append("\x1D\x21\x11"); // GS ! 11

            // ============ FASE 3: TÍTULO DO CUPOM ============
            // Align center para título
            sb.Append("\x1B\x61\x01"); // ESC a 1
            sb.Append(titulo);
            sb.Append("\n");

            // ============ FASE 3.5: NOME DO PRODUTO COM PREÇO ============
            // Align center para produto
            sb.Append("\x1B\x61\x01"); // ESC a 1
            // Bold OFF direto após font 2x
            sb.Append("\x1B\x45\x00"); // ESC E 0

            // Produto mockado
            string nomeProduto = "Cerveja Premium - Long Neck Ãã";
            decimal preco = 15.50m;
            sb.Append(nomeProduto);
            sb.Append("\n");
            sb.Append($"R$ {preco:N2}");
            sb.Append("\n");
            // ============ FASE 3.6: INFORMAÇÕES DE CAIXA ============
            sb.Append("\x1D\x21\x00"); // Font normal
            sb.Append("\x1B\x61\x01"); // ESC a 1
            int numeroCaixa = 1;
            string descricaoCaixa = "Caixa Principal";
            string infoCaixa = $"Caixa #{numeroCaixa} - {descricaoCaixa}";
            sb.Append(infoCaixa);
            sb.Append("\n");

            // ============ FASE 3.7: DATA E HORA ============
            // 3 linhas vazias antes da data/hora
            sb.Append("\n\n\n");
            
            // Align right para data/hora
            sb.Append("\x1B\x61\x02"); // ESC a 2
            
            // Fonte pequena para data
            sb.Append("\x1D\x21\x00"); // GS ! 00
            
            // Data e hora
            string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            sb.Append(dataHora);
            sb.Append("\n");

            // ============ FASE 3.8: REDES SOCIAIS (RODAPÉ) ============
            // 2 linhas vazias antes das redes sociais
            sb.Append("\n\n");
            
            // Align center
            sb.Append("\x1B\x61\x01"); // ESC a 1
            
            // Fonte pequena
            sb.Append("\x1D\x21\x00"); // GS ! 00
            
            // Imprimir redes sociais em uma linha
            string redesSociais = "@AliancaDeMisericordia.salto @CidadeRahamim";
            sb.Append(redesSociais);
            sb.Append("\n");

            // ============ FASE 4: RESET DE FORMATAÇÃO ============
            // Bold OFF
            sb.Append("\x1B\x45\x00"); // ESC E 0

            // Align left
            sb.Append("\x1B\x61\x00"); // ESC a 0

            // ============ FASE 5: AVANÇAR PAPEL ============
            // 1 quebra de linha
            sb.Append("\n");

            // ============ FASE 6: ESPAÇO ANTES DO CORTE ============
            // 3 linhas vazias antes do corte
            sb.Append("\n\n\n");

            return sb.ToString();
        }

        /// <summary>
        /// Constrói um cupom de venda com dados específicos
        /// </summary>
        private string ConstrutorCupomVenda(string nomeProduto, decimal preco, int numeroCaixa, string descricaoCaixa, int vendaId, int sequencia, int totalItens)
        {
            var sb = new StringBuilder();

            // ============ FASE 1: CONFIGURAÇÕES INICIAIS ============
            // Font 2x e Bold são suficientes para novo cupom
            sb.Append("\x1B\x45\x01"); // Bold ON
            sb.Append("\x1D\x21\x11"); // Font 2x

            // ============ FASE 2: PRODUTO COM PREÇO ============
            // Align center para produto
            sb.Append("\x1B\x61\x01"); // Align center
            string textoImpressao = nomeProduto;
            if (preco > 0)
            {
                textoImpressao = $"{nomeProduto} - R$ {preco.ToString("F2")}";
            }
            sb.Append(textoImpressao);
            sb.Append("\n");

            // ============ FASE 4: INFORMAÇÕES DE CAIXA ============
            sb.Append("\x1D\x21\x00"); // Font normal
            sb.Append("\x1B\x45\x00"); // Bold OFF
            sb.Append("\x1B\x61\x01"); // Align center
            
            if (numeroCaixa > 0)
            {
                string infoCaixa = $"Caixa #{numeroCaixa}";
                if (!string.IsNullOrWhiteSpace(descricaoCaixa))
                {
                    infoCaixa += $" - {descricaoCaixa}";
                }
                sb.Append(infoCaixa);
            }
            sb.Append("\n");

            // ============ FASE 5: ESPAÇO ANTES DE DATA/HORA ============
            sb.Append("\n\n\n"); // 3 linhas vazias

            // ============ FASE 6: DATA E HORA ============
            sb.Append("\x1B\x61\x02"); // Align right
            sb.Append("\x1D\x21\x00"); // Font pequena
            
            string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            sb.Append(dataHora);
            sb.Append("\n");

            // ============ FASE 7: ESPAÇO ANTES DE REDES SOCIAIS ============
            sb.Append("\n\n"); // 2 linhas vazias

            // ============ FASE 8: REDES SOCIAIS ============
            sb.Append("\x1B\x61\x01"); // Align center
            sb.Append("\x1D\x21\x00"); // Font pequena
            
            string redesSociais = "@AliancaDeMisericordia.salto @CidadeRahamim";
            sb.Append(redesSociais);
            sb.Append("\n");

            // ============ FASE 9: RESET DE FORMATAÇÃO ============
            sb.Append("\x1B\x45\x00"); // Bold OFF

            // ============ FASE 10: ESPAÇO FINAL ============
            sb.Append("\n\n\n\n");

            return sb.ToString();
        }

        /// <summary>
        /// Constrói um cupom de reimpressão com dados específicos
        /// </summary>
        private string ConstrutorCupomReimpressao(string nomeProduto, decimal preco, int numeroCaixa, string descricaoCaixa, int reimpressaoId, int sequencia, int totalItens)
        {
            var sb = new StringBuilder();

            // ============ FASE 1: CONFIGURAÇÕES INICIAIS ============
            // Font 2x e Bold são suficientes para novo cupom
            sb.Append("\x1B\x45\x01"); // Bold ON
            sb.Append("\x1D\x21\x11"); // Font 2x

            // ============ FASE 3: PRODUTO COM PREÇO ============
            // Align center para produto
            sb.Append("\x1B\x61\x01"); // Align center
            string textoImpressao = nomeProduto;
            if (preco > 0)
            {
                textoImpressao = $"{nomeProduto} - R$ {preco.ToString("F2")}";
            }
            sb.Append(textoImpressao);
            sb.Append("\n");

            // ============ FASE 4: MARCADOR DE REIMPRESSÃO ============
            sb.Append("\x1D\x21\x00"); // Font normal
            sb.Append("\x1B\x45\x00"); // Bold OFF
            sb.Append("*** REIMPRESSÃO ***");
            sb.Append("\n");

            // ============ FASE 5: INFORMAÇÕES DE CAIXA ============
            sb.Append("\x1B\x61\x01"); // Align center
            
            if (numeroCaixa > 0)
            {
                string infoCaixa = $"Caixa #{numeroCaixa}";
                if (!string.IsNullOrWhiteSpace(descricaoCaixa))
                {
                    infoCaixa += $" - {descricaoCaixa}";
                }
                sb.Append(infoCaixa);
            }
            sb.Append("\n");

            // ============ FASE 6: ESPAÇO ANTES DE DATA/HORA ============
            sb.Append("\n\n\n"); // 3 linhas vazias

            // ============ FASE 7: DATA E HORA ============
            sb.Append("\x1B\x61\x02"); // Align right
            sb.Append("\x1D\x21\x00"); // Font pequena
            
            string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            sb.Append(dataHora);
            sb.Append("\n");

            // ============ FASE 8: ESPAÇO ANTES DE REDES SOCIAIS ============
            sb.Append("\n\n"); // 2 linhas vazias

            // ============ FASE 9: REDES SOCIAIS ============
            sb.Append("\x1B\x61\x01"); // Align center
            sb.Append("\x1D\x21\x00"); // Font pequena
            
            string redesSociais = "@AliancaDeMisericordia.salto @CidadeRahamim";
            sb.Append(redesSociais);
            sb.Append("\n");

            // ============ FASE 10: RESET DE FORMATAÇÃO ============
            sb.Append("\x1B\x45\x00"); // Bold OFF

            // ============ FASE 11: ESPAÇO FINAL ============
            sb.Append("\n\n\n\n");

            return sb.ToString();
        }

        /// <summary>
        /// Retorna o comando ESC/POS para corte de papel
        /// </summary>
        private string ComandoCorte()
        {
            // GS V A = Corte completo (Full Cut)
            // GS V 0 = Corte parcial (Partial Cut)
            return "\x1D\x56\x41"; // Corte completo
        }

        /// <summary>
        /// Converte string com caracteres de controle para byte array
        /// Usa encoding Windows-1252 (CP1252) para acentuação
        /// </summary>
        private byte[] ConvertStringToByteArray(string input)
        {
            return Encoding.GetEncoding(1252).GetBytes(input);
        }

        /// <summary>
        /// Imprime uma venda completa (múltiplos itens)
        /// Constrói todos os cupons em um único documento para garantir cortes corretos
        /// </summary>
        public bool ImprimirVenda(int vendaId, List<ItemImpressao> itens, 
                                 int numeroCaixa = 0, string descricaoCaixa = "")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] Iniciando impressão de venda #{vendaId} com {itens.Count} item(ns)");

                // Construir todos os cupons em um único documento
                var sb = new StringBuilder();

                // ============ RESET ÚNICO NO INÍCIO ============
                sb.Append("\x1B\x40"); // ESC @ - Reset apenas UMA VEZ
                sb.Append("\x1B\x74\x10"); // ESC t 16 - Windows-1252 (uma vez após reset)
                Thread.Sleep(50); // Aguarda processamento do reset

                for (int i = 0; i < itens.Count; i++)
                {
                    var item = itens[i];
                    
                    // Construir cupom com dados da venda
                    sb.Append(ConstrutorCupomVenda(item.Nome, item.Preco, numeroCaixa, descricaoCaixa, vendaId, i + 1, itens.Count));

                    // Adicionar corte após cada cupom
                    sb.Append(ComandoCorte());
                }

                // ⏳ ESPAÇO FINAL CRÍTICO - Impressoras térmicas precisam de papel após o corte
                // Sem este espaço, o último corte fica pendente sem executar
                sb.Append("\n"); // quebra de linha adicional para garantir processamento do corte final

                // Converter para byte array e enviar TUDO de uma vez
                byte[] dadosImpressao = ConvertStringToByteArray(sb.ToString());
                
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] Venda #{vendaId} construída com {dadosImpressao.Length} bytes");

                bool resultado = EnviarParaImpressora(dadosImpressao);
                
                if (resultado)
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Venda #{vendaId} impressa com sucesso");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Falha ao imprimir venda #{vendaId}");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Erro ao imprimir venda: {ex.Message}");
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir venda: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Imprime uma reimpressão (cupom sem debitar estoque)
        /// Constrói todos os cupons em um único documento para garantir cortes corretos
        /// </summary>
        public bool ImprimirReimpressao(int reimpressaoId, List<ItemImpressao> itens, 
                                       int numeroCaixa = 0, string descricaoCaixa = "")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] Iniciando impressão de reimpressão #{reimpressaoId} com {itens.Count} item(ns)");

                // Construir todos os cupons em um único documento
                var sb = new StringBuilder();
                // ============ RESET ÚNICO NO INÍCIO ============
                sb.Append("\x1B\x40"); // ESC @ - Reset apenas UMA VEZ
                sb.Append("\x1B\x74\x10"); // ESC t 16 - Windows-1252 (uma vez após reset)
                Thread.Sleep(50); // Aguarda processamento do reset
                for (int i = 0; i < itens.Count; i++)
                {
                    var item = itens[i];
                    
                    // Construir cupom com dados da reimpressão
                    sb.Append(ConstrutorCupomReimpressao(item.Nome, item.Preco, numeroCaixa, descricaoCaixa, reimpressaoId, i + 1, itens.Count));

                    // Adicionar corte após cada cupom
                    sb.Append(ComandoCorte());
                }

                // ⏳ ESPAÇO FINAL CRÍTICO - Impressoras térmicas precisam de papel após o corte
                // Sem este espaço, o último corte fica pendente sem executar
                sb.Append("\n"); // quebra de linha adicional para garantir processamento do corte final

                // Converter para byte array e enviar TUDO de uma vez
                byte[] dadosImpressao = ConvertStringToByteArray(sb.ToString());
                
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] Reimpressão #{reimpressaoId} construída com {dadosImpressao.Length} bytes");

                bool resultado = EnviarParaImpressora(dadosImpressao);
                
                if (resultado)
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Reimpressão #{reimpressaoId} impressa com sucesso");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Falha ao imprimir reimpressão #{reimpressaoId}");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Erro ao imprimir reimpressão: {ex.Message}");
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir reimpressão: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Imprime um cupom individual com dados ESC/POS
        /// Implementação compatível com interface IPrinterService
        /// </summary>
        public bool ImprimirCupom(string nomeProduto, int numeroCaixa = 0, string descricaoCaixa = "", decimal preco = 0)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] Iniciando impressão de cupom: {nomeProduto}");

                // Construir os dados ESC/POS para um cupom
                var sb = new StringBuilder();

                // ============ FASE 1: RESET E INICIALIZAÇÃO ============
                // Reset da impressora
                sb.Append("\x1B\x40"); // ESC @

                // Configurar code page para Windows-1252
                sb.Append("\x1B\x74\x10"); // ESC t 16 (Windows-1252 com acentuação)

                // ============ FASE 2: CONFIGURAÇÕES INICIAIS ============
                // Align center
                sb.Append("\x1B\x61\x01"); // ESC a 1
                
                // Bold ON
                sb.Append("\x1B\x45\x01"); // ESC E 1
                
                // Font 2x (maior)
                sb.Append("\x1D\x21\x11"); // GS ! 11

                // ============ FASE 3: NOME DO PRODUTO COM PREÇO ============
                string textoImpressao = nomeProduto;
                if (preco > 0)
                {
                    textoImpressao = $"{nomeProduto} - R$ {preco.ToString("F2")}";
                }
                sb.Append(textoImpressao);
                sb.Append("\n");

                // Bold OFF
                sb.Append("\x1B\x45\x00"); // ESC E 0

                // ============ FASE 3.5: INFORMAÇÕES DE CAIXA ============             

                // Font normal
                sb.Append("\x1D\x21\x00"); // GS ! 00
                
                // Align center
                sb.Append("\x1B\x61\x01"); // ESC a 1
                
                if (numeroCaixa > 0)
                {
                    string infoCaixa = $"Caixa #{numeroCaixa}";
                    if (!string.IsNullOrWhiteSpace(descricaoCaixa))
                    {
                        infoCaixa += $" - {descricaoCaixa}";
                    }
                    sb.Append(infoCaixa);
                }
                sb.Append("\n\n");

                // ============ FASE 3.6: DATA E HORA ============                
                sb.Append("\n");
                
                // Align right para data/hora
                sb.Append("\x1B\x61\x02"); // ESC a 2
                
                // Fonte pequena para data
                sb.Append("\x1D\x21\x00"); // GS ! 00
                
                // Data e hora
                string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                sb.Append(dataHora);
                sb.Append("\n");

                // ============ FASE 3.7: REDES SOCIAIS (RODAPÉ) ============
                // linhas vazias antes das redes sociais
                sb.Append("\n");
                
                // Align center
                sb.Append("\x1B\x61\x01"); // ESC a 1
                
                // Fonte pequena
                sb.Append("\x1D\x21\x00"); // GS ! 00
                
                // Imprimir redes sociais em uma linha
                string redesSociais = "@AliancaDeMisericordia.salto @CidadeRahamim";
                sb.Append(redesSociais);
                sb.Append("\n");

                // ============ FASE 4: RESET DE FORMATAÇÃO ============
                // Bold OFF
                sb.Append("\x1B\x45\x00"); // ESC E 0

                // Font normal
                sb.Append("\x1D\x21\x00"); // GS ! 00

                // Align left
                sb.Append("\x1B\x61\x00"); // ESC a 0

                // ============ FASE 5: AVANÇAR PAPEL ============
                // 1 quebra de linha
                sb.Append("\n");

                // ============ FASE 6: ESPAÇO ANTES DO CORTE ============
                // 3 linhas vazias antes do corte
                sb.Append("\n");

                // ============ FASE 7: CORTE ============
                // Corte completo
                sb.Append("\x1D\x56\x41"); // GS V A (Full Cut)

                // ⏳ ESPAÇO FINAL CRÍTICO - Impressoras térmicas precisam de papel após o corte
                // Sem este espaço, o corte fica pendente sem executar
                sb.Append("\n"); // 5 quebras de linha adicionais para garantir processamento

                // Converter para byte array e enviar
                byte[] dadosImpressao = ConvertStringToByteArray(sb.ToString());
                
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] Cupom construído com {dadosImpressao.Length} bytes");

                bool resultado = EnviarParaImpressora(dadosImpressao);
                
                if (resultado)
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Cupom impresso com sucesso");
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Falha ao imprimir cupom");
                }

                return resultado;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Erro ao imprimir cupom: {ex.Message}");
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir cupom: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Envia dados ESC/POS para a impressora via Windows Print Service
        /// Envia direto sem usar PrintDocument (evita páginas em branco)
        /// </summary>
        private bool EnviarParaImpressora(byte[] dados)
        {
            try
            {
                // Tentar encontrar a impressora pelo nome
                string impressoraParaUsar = _printerName;
                bool printerFound = false;

                foreach (string printer in PrinterSettings.InstalledPrinters)
                {
                    if (printer.Equals(_printerName, StringComparison.OrdinalIgnoreCase))
                    {
                        impressoraParaUsar = printer;
                        printerFound = true;
                        break;
                    }
                }

                if (!printerFound)
                {
                    // Se não encontrar, usar impressora padrão
                    System.Diagnostics.Debug.WriteLine($"⚠ Impressora '{_printerName}' não encontrada. Usando padrão.");
                    if (PrinterSettings.InstalledPrinters.Count > 0)
                    {
                        impressoraParaUsar = PrinterSettings.InstalledPrinters[0];
                    }
                    else
                    {
                        throw new Exception("Nenhuma impressora instalada no sistema.");
                    }
                }

                // Enviar dados brutos diretamente via Win32 API
                return EnviarDadosBrutos(dados, impressoraParaUsar);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao enviar para impressora: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Envia dados brutos para impressora usando Win32 API
        /// </summary>
        private bool EnviarDadosBrutos(byte[] dados, string printerName)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] Enviando {dados.Length} bytes para '{printerName}'");

                IntPtr printerHandle = IntPtr.Zero;

                DOCINFO docInfo = new DOCINFO
                {
                    pDocName = "GestorEvento Print Job",
                    pDataType = "RAW",
                    pOutputFile = null
                };

                // Abrir impressora
                if (!Win32PrinterApi.OpenPrinter(printerName, out printerHandle, IntPtr.Zero))
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Falha ao abrir impressora '{printerName}'");
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Impressora aberta");

                // Iniciar documento
                if (!Win32PrinterApi.StartDocPrinter(printerHandle, 1, ref docInfo))
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Falha ao iniciar documento");
                    Win32PrinterApi.ClosePrinter(printerHandle);
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Documento iniciado");

                // Iniciar página
                if (!Win32PrinterApi.StartPagePrinter(printerHandle))
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Falha ao iniciar página");
                    Win32PrinterApi.EndDocPrinter(printerHandle);
                    Win32PrinterApi.ClosePrinter(printerHandle);
                    return false;
                }

                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Página iniciada");

                // Enviar dados brutos
                uint bytesWritten = 0;
                bool result = Win32PrinterApi.WritePrinter(printerHandle, dados, (uint)dados.Length, out bytesWritten);
                
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] Bytes escritos: {bytesWritten} de {dados.Length}");

                // Finalizar página
                Win32PrinterApi.EndPagePrinter(printerHandle);
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Página finalizada");
                
                // Finalizar documento
                Win32PrinterApi.EndDocPrinter(printerHandle);
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Documento finalizado");

                // ⏳ AGUARDAR PROCESSAMENTO APÓS FINALIZAR DOCUMENTO
                // A impressora precisa de tempo para processar ESC/POS commands (especialmente corte)
                // Este delay DEVE estar APÓS EndDocPrinter() para garantir que o Print Spooler processe tudo
                Thread.Sleep(1000); // 1 segundo para garantir que o corte seja processado
                
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Processamento concluído");
                
                // Fechar impressora
                Win32PrinterApi.ClosePrinter(printerHandle);
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Impressora fechada");

                if (result && bytesWritten > 0)
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✓ Dados enviados com sucesso");
                    return true;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Falha ao escrever dados");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[WindowsPrinterService] ✗ Erro: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Desconecta da impressora (sem-op para Windows Printer Service)
        /// </summary>
        public void Desconectar()
        {
            // Classe WindowsPrinterService não mantém conexão persistente
            // Cada impressão abre e fecha a conexão automaticamente
            // Nada a fazer aqui
        }

        /// <summary>
        /// Libera recursos da impressora
        /// </summary>
        public void Dispose()
        {
            // Classe WindowsPrinterService não mantém estado de conexão
            // A impressora é apenas referenciada pelo nome
            // Nada a limpar neste momento
        }
    }

    /// <summary>
    /// Estrutura para informações do documento
    /// </summary>
    [System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Sequential, CharSet = System.Runtime.InteropServices.CharSet.Ansi)]
    public struct DOCINFO
    {
        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string pDocName;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string pOutputFile;

        [System.Runtime.InteropServices.MarshalAs(System.Runtime.InteropServices.UnmanagedType.LPStr)]
        public string pDataType;
    }

    /// <summary>
    /// Wrapper para Win32 Printer API
    /// </summary>
    public class Win32PrinterApi
    {
        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        public static extern bool OpenPrinter(
            string szPrinter,
            out IntPtr hPrinter,
            IntPtr pd);

        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        public static extern bool StartDocPrinter(
            IntPtr hPrinter,
            int level,
            ref DOCINFO pDocInfo);

        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        public static extern bool StartPagePrinter(IntPtr hPrinter);

        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        public static extern bool WritePrinter(
            IntPtr hPrinter,
            byte[] pBuf,
            uint cbBuf,
            out uint pcWritten);

        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        public static extern bool EndPagePrinter(IntPtr hPrinter);

        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        public static extern bool EndDocPrinter(IntPtr hPrinter);

        [System.Runtime.InteropServices.DllImport("winspool.drv", CharSet = System.Runtime.InteropServices.CharSet.Ansi, SetLastError = true)]
        public static extern bool ClosePrinter(IntPtr hPrinter);
    }
}
