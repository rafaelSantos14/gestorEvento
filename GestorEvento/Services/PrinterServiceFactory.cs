using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    /// <summary>
    /// Factory para decidir qual serviço de impressão usar (Local ou Remote via API)
    /// Baseado na configuração em App.config
    /// Usa IPrinterService para abstrair implementações específicas
    /// </summary>
    public static class PrinterServiceFactory
    {
        /// <summary>
        /// Cria instância do serviço de impressão apropriado
        /// Baseado em PrintMode (Local/Remote) e PrinterType (COM/USB)
        /// </summary>
        public static IPrinterService CreatePrinterService()
        {
            string printMode = ConfigurationManager.AppSettings["PrintMode"] ?? "Local";
            
            if (printMode.Equals("Remote", StringComparison.OrdinalIgnoreCase))
            {
                // Modo Remote usa métodos estáticos (ImprimirViaAPI, ImprimirVendaViaAPI)
                // Para usar interface IPrinterService, use os métodos públicos estáticos da factory
                throw new NotImplementedException("Modo Remote com IPrinterService não implementado. Use PrinterServiceFactory.ImprimirCupom/Venda() diretamente.");
            }
            
            // Modo Local - decidir por PrinterType
            string printerType = ConfigurationManager.AppSettings["PrinterType"] ?? "COM";
            
            switch (printerType.ToUpper())
            {
                case "USB":
                    // Impressora USB/Windows (TOMATE MDK-080, HP, Zebra, etc)
                    string windowsPrinterName = ConfigurationManager.AppSettings["WindowsPrinterName"] ?? "MDK-080";
                    System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] Usando impressora USB: {windowsPrinterName}");
                    return new WindowsPrinterService(windowsPrinterName);
                
                case "COM":
                default:
                    // Impressora Serial (Epson TM-20, etc)
                    string portName = ConfigurationManager.AppSettings["PrinterPortName"] ?? "COM2";
                    int baudRate = int.TryParse(
                        ConfigurationManager.AppSettings["PrinterBaudRate"] ?? "9600", 
                        out int br) ? br : 9600;
                    System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] Usando impressora COM: {portName} @ {baudRate} baud");
                    return new EpsonTM20Service(portName, baudRate);
            }
        }

        /// <summary>
        /// Imprime um cupom individual no modo configurado (Local ou Remote)
        /// </summary>
        public static bool ImprimirCupom(string nomeProduto)
        {
            string printMode = ConfigurationManager.AppSettings["PrintMode"] ?? "Local";

            System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] Modo: {printMode}");

            if (printMode == "Remote")
            {
                return ImprimirViaAPI(nomeProduto);
            }
            else
            {
                return ImprimirLocal(nomeProduto);
            }
        }

        /// <summary>
        /// Imprime uma venda completa (todos os itens de uma vez)
        /// Evita race condition entre múltiplas máquinas
        /// </summary>
        public static bool ImprimirVenda(int vendaId, List<ItemImpressao> itens, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            string printMode = ConfigurationManager.AppSettings["PrintMode"] ?? "Local";

            System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] Venda #{vendaId} - Caixa #{numeroCaixa} - Modo: {printMode} - Itens: {itens.Count}");

            if (printMode == "Remote")
            {
                return ImprimirVendaViaAPI(vendaId, itens, numeroCaixa, descricaoCaixa);
            }
            else
            {
                return ImprimirVendaLocal(vendaId, itens, numeroCaixa, descricaoCaixa);
            }
        }

        /// <summary>
        /// Imprime uma reimpressão (cupom sem debitar estoque)
        /// Segue o mesmo padrão de ImprimirVenda
        /// </summary>
        public static bool ImprimirReimpressao(int reimpressaoId, List<ItemImpressao> itens, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            string printMode = ConfigurationManager.AppSettings["PrintMode"] ?? "Local";

            System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] Reimpressão #{reimpressaoId} - Caixa #{numeroCaixa} - Modo: {printMode} - Itens: {itens.Count}");

            if (printMode == "Remote")
            {
                return ImprimirVendaViaAPI(reimpressaoId, itens, numeroCaixa, descricaoCaixa);
            }
            else
            {
                return ImprimirVendaLocal(reimpressaoId, itens, numeroCaixa, descricaoCaixa);
            }
        }

        /// <summary>
        /// Imprime uma venda localmente (todos os itens sequencialmente)
        /// Respeita a configuração PrinterType (COM ou USB)
        /// </summary>
        private static bool ImprimirVendaLocal(int vendaId, List<ItemImpressao> itens, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] Imprimindo venda #{vendaId} localmente ({itens.Count} itens)");

                // Usar CreatePrinterService() para respeitar configuração PrinterType (COM ou USB)
                var printer = CreatePrinterService();

                // Imprimir cada item como um cupom separado (método antigo que funcionava)
                foreach (var item in itens)
                {
                    bool resultado = printer.ImprimirCupom(item.Nome, numeroCaixa, descricaoCaixa, item.Preco);
                    
                    if (resultado)
                    {
                        System.Diagnostics.Debug.WriteLine($"  ✓ Cupom impresso: {item.Nome} - R$ {item.Preco.ToString("F2")}");
                    }
                    else
                    {
                        System.Diagnostics.Debug.WriteLine($"  ✗ Falha ao imprimir cupom: {item.Nome}");
                    }
                }

                printer.Desconectar();
                printer.Dispose();

                System.Diagnostics.Debug.WriteLine($"✓ Venda #{vendaId} finalizada");
                return true;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir venda localmente: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Imprime uma venda via API HTTP do Print Server (Remote)
        /// Toda a venda é enviada em UMA requisição (atômico)
        /// </summary>
        private static bool ImprimirVendaViaAPI(int vendaId, List<ItemImpressao> itens, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            try
            {
                System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] Imprimindo venda #{vendaId} via API ({itens.Count} itens)");

                string printServerIp = ConfigurationManager.AppSettings["PrintServerIp"];
                if (string.IsNullOrWhiteSpace(printServerIp))
                    throw new ConfigurationErrorsException("PrintServerIp não configurado em App.config");

                if (!int.TryParse(
                    ConfigurationManager.AppSettings["PrintServerPort"] ?? "5000",
                    out int printServerPort))
                {
                    printServerPort = 5000;
                }

                string url = $"http://{printServerIp}:{printServerPort}/imprimir-venda";

                // Construir JSON manualmente (sem dependências externas)
                string json = JsonSerializeVenda(vendaId, itens, numeroCaixa, descricaoCaixa);

                System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] URL: {url}");

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    try
                    {
                        var content = new StringContent(json, Encoding.UTF8, "application/json");
                        var response = client.PostAsync(url, content).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] ✓ Venda #{vendaId} enviada com sucesso");
                            return true;
                        }
                        else
                        {
                            string errorContent = response.Content.ReadAsStringAsync().Result;
                            System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] ✗ Erro: {response.StatusCode} - {errorContent}");
                            UiHelper.ExibirErro("Erro", $"Erro ao enviar venda: {response.StatusCode}");
                            return false;
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] ✗ Erro de conexão: {ex.Message}");
                        UiHelper.ExibirErro("Erro de Conexão",
                            $"Não foi possível conectar ao Print Server em {printServerIp}:{printServerPort}\n\n" +
                            "Verifique:\n" +
                            "1. Se o Print Server está rodando\n" +
                            "2. Se o IP está correto\n" +
                            "3. Se há conectividade de rede");
                        return false;
                    }
                    catch (TaskCanceledException)
                    {
                        System.Diagnostics.Debug.WriteLine("[PrinterServiceFactory] ✗ Timeout na requisição");
                        UiHelper.ExibirErro("Timeout", "Requisição ao Print Server expirou (30 segundos)");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] ✗ Erro geral: {ex.Message}");
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir venda via API: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Imprime localmente usando a impressora configurada
        /// Respeita a configuração PrinterType (COM ou USB)
        /// </summary>
        private static bool ImprimirLocal(string nomeProduto)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[PrinterServiceFactory] Usando modo LOCAL");

                // Usar CreatePrinterService() para respeitar configuração PrinterType (COM ou USB)
                var printer = CreatePrinterService();
                
                bool resultado = printer.ImprimirCupom(nomeProduto);
                printer.Desconectar();
                printer.Dispose();

                return resultado;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir localmente: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Imprime via API HTTP do Print Server (Remote) - Item único
        /// </summary>
        private static bool ImprimirViaAPI(string nomeProduto)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("[PrinterServiceFactory] Usando modo REMOTE (API)");

                string printServerIp = ConfigurationManager.AppSettings["PrintServerIp"];
                if (string.IsNullOrWhiteSpace(printServerIp))
                    throw new ConfigurationErrorsException("PrintServerIp não configurado em App.config");

                if (!int.TryParse(
                    ConfigurationManager.AppSettings["PrintServerPort"] ?? "5000",
                    out int printServerPort))
                {
                    printServerPort = 5000;
                }

                string url = $"http://{printServerIp}:{printServerPort}/imprimir?produto={Uri.EscapeDataString(nomeProduto)}";

                System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] URL: {url}");

                using (HttpClient client = new HttpClient())
                {
                    client.Timeout = TimeSpan.FromSeconds(30);

                    try
                    {
                        var response = client.PostAsync(url, null).Result;

                        if (response.IsSuccessStatusCode)
                        {
                            System.Diagnostics.Debug.WriteLine("[PrinterServiceFactory] ✓ Cupom enviado com sucesso");
                            return true;
                        }
                        else
                        {
                            string errorContent = response.Content.ReadAsStringAsync().Result;
                            System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] ✗ Erro: {response.StatusCode} - {errorContent}");
                            UiHelper.ExibirErro("Erro", $"Erro ao enviar cupom: {response.StatusCode}");
                            return false;
                        }
                    }
                    catch (HttpRequestException ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] ✗ Erro de conexão: {ex.Message}");
                        UiHelper.ExibirErro("Erro de Conexão", 
                            $"Não foi possível conectar ao Print Server em {printServerIp}:{printServerPort}\n\n" +
                            "Verifique:\n" +
                            "1. Se o Print Server está rodando\n" +
                            "2. Se o IP está correto\n" +
                            "3. Se há conectividade de rede");
                        return false;
                    }
                    catch (TaskCanceledException)
                    {
                        System.Diagnostics.Debug.WriteLine("[PrinterServiceFactory] ✗ Timeout na requisição");
                        UiHelper.ExibirErro("Timeout", "Requisição ao Print Server expirou (30 segundos)");
                        return false;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"[PrinterServiceFactory] ✗ Erro geral: {ex.Message}");
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir via API: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Serializa venda para JSON sem dependências externas
        /// </summary>
        private static string JsonSerializeVenda(int vendaId, List<ItemImpressao> itens, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            // Serializar cada item como JSON com nome e preço
            var itensJson = itens.Select(i => $"{{\"nome\":\"{EscapeJson(i.Nome)}\",\"preco\":{i.Preco.ToString("F2").Replace(",", ".")}}}" );
            string caixaJson = $",\"numeroCaixa\":{numeroCaixa},\"descricaoCaixa\":\"{EscapeJson(descricaoCaixa)}\"";
            return $"{{\"vendaId\":{vendaId},\"itens\":[{string.Join(",", itensJson)}]{caixaJson}}}";
        }

        /// <summary>
        /// Escapa caracteres especiais para JSON
        /// </summary>
        private static string EscapeJson(string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t");
        }

        /// <summary>
        /// Retorna informações sobre o modo atual de impressão
        /// </summary>
        public static string GetModoImpressao()
        {
            string printMode = ConfigurationManager.AppSettings["PrintMode"] ?? "Local";

            if (printMode == "Remote")
            {
                string ip = ConfigurationManager.AppSettings["PrintServerIp"];
                string port = ConfigurationManager.AppSettings["PrintServerPort"] ?? "5000";
                if (string.IsNullOrWhiteSpace(ip))
                    ip = "[não configurado]";
                return $"REMOTE - {ip}:{port}";
            }
            else
            {
                string port = ConfigurationManager.AppSettings["PrinterPortName"] ?? "COM2";
                return $"LOCAL - {port}";
            }
        }
    }
}
