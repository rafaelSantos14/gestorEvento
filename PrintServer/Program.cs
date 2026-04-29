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

            // Configuração
            string portName = "COM2";           // Porta serial da impressora
            int baudRate = 9600;                // Taxa de transmissão
            string ipAddress = ConfigurationManager.AppSettings["PrintServerIp"];     // IP do App.config
            string portString = ConfigurationManager.AppSettings["PrintServerPort"];  // Porta do App.config
            
            // Validar configurações obrigatórias
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

            PrintQueueManager queueManager = new PrintQueueManager(portName, baudRate);
            PrintServerApi api = new PrintServerApi(queueManager, ipAddress, httpPort);

            try
            {
                // Iniciar API
                api.Start();

                Console.WriteLine("\n═══════════════════════════════════════════════════════════════");
                Console.WriteLine("✓ Print Server está online e aguardando requisições");
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
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ ERRO CRÍTICO: {ex.Message}");
                Console.WriteLine($"  {ex.StackTrace}");
            }
            finally
            {
                // Limpar recursos
                api?.Stop();
                queueManager?.Shutdown();
            }
        }
    }
}
