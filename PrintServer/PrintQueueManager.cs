using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GestorEvento.Services;
using PrintServer.Models;

namespace PrintServer
{
    public class PrintQueueManager
    {
        private readonly Queue<PrintJob> _printQueue;
        private readonly EpsonTM20Service _printerService;
        private readonly object _lockObj = new object();
        private bool _isProcessing = false;
        private Task _processingTask;

        public PrintQueueManager(string portName = null, int baudRate = -1)
        {
            _printQueue = new Queue<PrintJob>();
            // EpsonTM20Service lê do App.config automaticamente se portName/baudRate forem null/-1
            _printerService = new EpsonTM20Service(portName, baudRate);
        }

        /// <summary>
        /// Adiciona um trabalho de impressão à fila
        /// </summary>
        public PrintJob EnqueuePrintJob(string productName, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            lock (_lockObj)
            {
                var job = new PrintJob(productName, numeroCaixa, descricaoCaixa);
                _printQueue.Enqueue(job);
                Console.WriteLine($"✓ Trabalho adicionado à fila: {job}");
                Console.WriteLine($"  Fila atual: {_printQueue.Count} trabalho(s)");
                
                // Iniciar processamento se não estiver rodando
                if (!_isProcessing)
                {
                    StartProcessing();
                }

                return job;
            }
        }

        /// <summary>
        /// Inicia o processamento da fila em background
        /// </summary>
        private void StartProcessing()
        {
            if (_isProcessing)
                return;

            _isProcessing = true;
            _processingTask = Task.Run(() => ProcessQueue());
        }

        /// <summary>
        /// Processa itens da fila continuamente
        /// </summary>
        private void ProcessQueue()
        {
            while (true)
            {
                PrintJob job = null;

                lock (_lockObj)
                {
                    if (_printQueue.Count == 0)
                    {
                        _isProcessing = false;
                        Console.WriteLine("ℹ Fila vazia. Aguardando próximos trabalhos...");
                        break;
                    }

                    job = _printQueue.Dequeue();
                }

                if (job != null)
                {
                    PrintJobInternal(job);
                }
            }
        }

        /// <summary>
        /// Imprime um trabalho com retry automático
        /// </summary>
        private void PrintJobInternal(PrintJob job)
        {
            const int maxAttempts = 3;
            const int delayBetweenAttempts = 2000; // 2 segundos

            while (job.Attempts < maxAttempts && !job.Success)
            {
                job.Attempts++;
                
                try
                {
                    // Conectar à impressora
                    if (!_printerService.Conectar())
                    {
                        throw new Exception("Não foi possível conectar à impressora");
                    }

                    // Imprimir cupom
                    if (_printerService.ImprimirCupom(job.ProductName, job.NumeroCaixa, job.DescricaoCaixa))
                    {
                        job.Success = true;
                        Console.WriteLine($"✓ Cupom impresso: {job.ProductName}");
                    }
                    else
                    {
                        throw new Exception("Falha ao imprimir cupom");
                    }
                }
                catch (Exception ex)
                {
                    job.ErrorMessage = ex.Message;
                    Console.WriteLine($"✗ Erro na impressão de '{job.ProductName}' (Tentativa {job.Attempts}/{maxAttempts}): {ex.Message}");

                    if (job.Attempts < maxAttempts)
                    {
                        Console.WriteLine($"  Retentando em {delayBetweenAttempts}ms...");
                        Thread.Sleep(delayBetweenAttempts);
                    }
                }
            }

            // Log final
            if (!job.Success)
            {
                Console.WriteLine($"✗ FALHA: Não foi possível imprimir '{job.ProductName}' após {job.Attempts} tentativas.");
            }
        }

        /// <summary>
        /// Retorna status da fila
        /// </summary>
        public Dictionary<string, object> GetQueueStatus()
        {
            lock (_lockObj)
            {
                return new Dictionary<string, object>
                {
                    { "fila_size", _printQueue.Count },
                    { "is_processing", _isProcessing },
                    { "pending_jobs", _printQueue.Select(j => new
                    {
                        jobId = j.JobId,
                        produto = j.ProductName,
                        tentativas = j.Attempts
                    }).ToList() }
                };
            }
        }

        /// <summary>
        /// Limpa a fila
        /// </summary>
        public void ClearQueue()
        {
            lock (_lockObj)
            {
                int count = _printQueue.Count;
                _printQueue.Clear();
                Console.WriteLine($"ℹ Fila limpa ({count} trabalho(s) removido(s))");
            }
        }

        /// <summary>
        /// Encerra o serviço
        /// </summary>
        public void Shutdown()
        {
            Console.WriteLine("\n═══════════════════════════════════════");
            Console.WriteLine("Encerrando Print Server...");
            Console.WriteLine("═══════════════════════════════════════");

            _printerService?.Desconectar();
            _printerService?.Dispose();

            if (_processingTask != null)
            {
                _processingTask.Wait(5000); // Espera até 5 segundos
            }

            Console.WriteLine("✓ Print Server encerrado");
        }
    }
}
