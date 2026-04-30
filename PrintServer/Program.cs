using System;
using System.Configuration;
using System.Threading;

namespace PrintServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            
            Console.WriteLine("═══════════════════════════════════════════════════════════════");
            Console.WriteLine("         PRINT SERVER - Epson TM-T20 Network Print Service");
            Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

            // Configuração (sempre lír de App.config)
            string portName = ConfigurationManager.AppSettings["PrinterPortName"];
            string baudRateStr = ConfigurationManager.AppSettings["PrinterBaudRate"];
            
            // Validar configurações da impressora (obrigatórias)
            if (string.IsNullOrWhiteSpace(portName))
                throw new ConfigurationErrorsException("PrinterPortName não configurado em App.config. Exemplo: <add key=\"PrinterPortName\" value=\"COM2\" />");
            
            if (string.IsNullOrWhiteSpace(baudRateStr))
                throw new ConfigurationErrorsException("PrinterBaudRate não configurado em App.config. Exemplo: <add key=\"PrinterBaudRate\" value=\"9600\" />");
            
            int baudRate = 9600;
            if (!int.TryParse(baudRateStr, out baudRate))
                throw new ConfigurationErrorsException($"PrinterBaudRate inválido: '{baudRateStr}'. Deve ser um número inteiro (ex: 9600)");
            
            if (baudRate <= 0)
                throw new ConfigurationErrorsException($"PrinterBaudRate deve ser maior que 0. Valor informado: {baudRate}");
            
            string ipAddress = ConfigurationManager.AppSettings["PrintServerIp"];     // IP do App.config
            string portString = ConfigurationManager.AppSettings["PrintServerPort"];  // Porta do App.config
            
            // Validar configurações obrigatórias do Print Server
            if (string.IsNullOrWhiteSpace(ipAddress))
                throw new ConfigurationErrorsException("PrintServerIp não configurado em App.config");
            
            int httpPort = 5000;
            if (string.IsNullOrWhiteSpace(portString) || !int.TryParse(portString, out httpPort))
                throw new ConfigurationErrorsException("PrintServerPort não configurado ou inválido em App.config");

            // Permitir argumentos de linha de comando
            if (args.Length > 0)
            {
                portName = args[0];
            }
            if (args.Length > 1)
            {
                ipAddress = args[1];
            }
            if (args.Length > 2)
            {
                if (int.TryParse(args[2], out int port))
                    httpPort = port;
            }

            // Inicializar serviços
            Console.WriteLine($"[CONFIG] Porta Serial: {portName}");
            Console.WriteLine($"[CONFIG] Taxa Baud: {baudRate}");
            Console.WriteLine($"[CONFIG] IP da Máquina: {ipAddress}");
            Console.WriteLine($"[CONFIG] Porta HTTP: {httpPort}\n");

            // Verificar se está rodando como administrador
            bool isAdmin = new System.Security.Principal.WindowsPrincipal(
                System.Security.Principal.WindowsIdentity.GetCurrent()).IsInRole(
                System.Security.Principal.WindowsBuiltInRole.Administrator);
            
            if (!isAdmin)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("⚠️  AVISO: Não está executando como ADMINISTRADOR");
                Console.WriteLine("   Isso pode causar erro ao tentar usar a porta HTTP {httpPort}");
                Console.WriteLine("   Recomendação: Execute o prompt de comando como Administrador\n");
                Console.ResetColor();
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Executando com permissões de Administrador\n");
                Console.ResetColor();
            }

            PrintQueueManager queueManager = new PrintQueueManager(portName, baudRate);
            PrintServerApi api = new PrintServerApi(queueManager, ipAddress, httpPort);

            try
            {
                // Iniciar fila de impressão
                Console.WriteLine("[INIT] Inicializando PrintQueueManager...");
                // (já inicializado no construtor)
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓ Fila de impressão inicializada\n");
                Console.ResetColor();

                // Iniciar API
                api.Start();
                
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"✓ API HttpListener iniciada com sucesso\n");
                Console.ResetColor();

                Console.WriteLine("═══════════════════════════════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("✓✓✓ PRINT SERVER ONLINE E PRONTO PARA RECEBER REQUISIÇÕES ✓✓✓");
                Console.ResetColor();
                Console.WriteLine($"📍 Endereço: http://{ipAddress}:{httpPort}");
                Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

                Console.WriteLine("EXEMPLOS DE USO:\n");
                Console.WriteLine("  1. Imprimir cupom (POST):");
                Console.WriteLine($"     http://{ipAddress}:{httpPort}/imprimir?produto=Cerveja%20Premium");
                Console.WriteLine($"     http://localhost:{httpPort}/imprimir?produto=Cerveja%20Premium\n");
                Console.WriteLine("  2. Verificar status (GET):");
                Console.WriteLine($"     http://{ipAddress}:{httpPort}/status");
                Console.WriteLine($"     http://localhost:{httpPort}/status\n");
                Console.WriteLine("  3. Limpar fila (POST):");
                Console.WriteLine($"     http://{ipAddress}:{httpPort}/limpar");
                Console.WriteLine($"     http://localhost:{httpPort}/limpar\n");
                Console.WriteLine("  4. Informações (GET):");
                Console.WriteLine($"     http://{ipAddress}:{httpPort}/");
                Console.WriteLine($"     http://localhost:{httpPort}/\n");

                Console.WriteLine("═══════════════════════════════════════════════════════════════");
                Console.WriteLine("Pressione CTRL+C para encerrar o Print Server");
                Console.WriteLine("═══════════════════════════════════════════════════════════════\n");

                // Aguardar indefinidamente
                while (true)
                {
                    Thread.Sleep(1000);
                }
            }
            catch (ConfigurationErrorsException cfgEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ ERRO DE CONFIGURAÇÃO: {cfgEx.Message}");
                Console.ResetColor();
                Console.WriteLine("\nVerifique o arquivo App.config:");
                Console.WriteLine("  • PrintServerIp está preenchido?");
                Console.WriteLine("  • PrintServerPort está preenchido e é um número válido?");
                Console.WriteLine($"\n{cfgEx.StackTrace}");
            }
            catch (System.Net.HttpListenerException httpEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ ERRO NA PORTA HTTP: {httpEx.Message}");
                Console.ResetColor();
                
                if (httpEx.ErrorCode == 5)
                {
                    Console.WriteLine("\n🔒 Erro 5 = Acesso Negado");
                    Console.WriteLine("   SOLUÇÃO: Execute como ADMINISTRADOR");
                    Console.WriteLine("   • Clique direito no Prompt → 'Executar como administrador'");
                    Console.WriteLine("   • Ou configure o Firewall para liberar porta {httpPort}");
                }
                else if (httpEx.ErrorCode == 48)
                {
                    Console.WriteLine($"\n⚠️  Erro 48 = Porta {httpPort} já está em uso");
                    Console.WriteLine("   SOLUÇÃO: Mude a porta no App.config");
                    Console.WriteLine("   Ou finalize o processo que está usando a porta:");
                    Console.WriteLine($"   PowerShell: netstat -ano | findstr :{httpPort}");
                }
                else
                {
                    Console.WriteLine($"\n   Código do erro: {httpEx.ErrorCode}");
                    Console.WriteLine("   Verifique se:");
                    Console.WriteLine("   • Está executando como ADMINISTRADOR");
                    Console.WriteLine("   • O IP {ipAddress} é válido");
                    Console.WriteLine($"   • A porta {httpPort} não está em uso");
                }
                Console.WriteLine($"\n{httpEx.StackTrace}");
            }
            catch (System.IO.IOException ioEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ ERRO NA PORTA SERIAL: {ioEx.Message}");
                Console.ResetColor();
                Console.WriteLine($"\nNão conseguiu conectar à porta {portName}");
                Console.WriteLine("   SOLUÇÃO:");
                Console.WriteLine("   1. Verifique se a impressora está conectada");
                Console.WriteLine("   2. Abra Device Manager (devmgmt.msc)");
                Console.WriteLine("   3. Procure a porta correta em 'Ports (COM & LPT)'");
                Console.WriteLine("   4. Atualize App.config com a porta correta (ex: COM3)");
                Console.WriteLine($"\n{ioEx.StackTrace}");
            }
            catch (UnauthorizedAccessException uaEx)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n❌ ERRO DE PERMISSÃO: {uaEx.Message}");
                Console.ResetColor();
                Console.WriteLine("\n🔒 Você não tem permissão para usar este recurso");
                Console.WriteLine("   SOLUÇÃO: Execute o Prompt como ADMINISTRADOR");
                Console.WriteLine("   • Clique direito em 'Prompt de Comando'");
                Console.WriteLine("   • Selecione 'Executar como administrador'");
                Console.WriteLine($"\n{uaEx.StackTrace}");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n✗ ERRO CRÍTICO: {ex.Message}");
                Console.ResetColor();
                Console.WriteLine($"  Tipo: {ex.GetType().Name}");
                Console.WriteLine($"  {ex.StackTrace}");
            }
            finally
            {
                // Limpar recursos
                api?.Stop();
                queueManager?.Shutdown();
                
                Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Pressione qualquer tecla para encerrar...");
                Console.ResetColor();
                Console.ReadKey();
            }
        }
    }
}
