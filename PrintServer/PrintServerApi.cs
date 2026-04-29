using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using PrintServer.Models;

namespace PrintServer
{
    public class PrintServerApi
    {
        private readonly HttpListener _httpListener;
        private readonly PrintQueueManager _queueManager;
        private readonly string _baseUrl;
        private bool _isRunning = false;

        public PrintServerApi(PrintQueueManager queueManager, string ipAddress = null, int port = -1)
        {
            // Ler IP do App.config (obrigatório)
            if (string.IsNullOrWhiteSpace(ipAddress))
            {
                ipAddress = ConfigurationManager.AppSettings["PrintServerIp"];
                if (string.IsNullOrWhiteSpace(ipAddress))
                {
                    throw new ConfigurationErrorsException("PrintServerIp não configurado em App.config");
                }
            }

            // Ler Porta do App.config (obrigatório)
            if (port <= 0)
            {
                string portConfig = ConfigurationManager.AppSettings["PrintServerPort"];
                if (string.IsNullOrWhiteSpace(portConfig) || !int.TryParse(portConfig, out port))
                {
                    throw new ConfigurationErrorsException("PrintServerPort não configurado ou inválido em App.config");
                }
            }

            _queueManager = queueManager;
            _baseUrl = $"http://{ipAddress}:{port}/";
            
            _httpListener = new HttpListener();
            
            // Adicionar prefixos para aceitar requisições via:
            // - IP específico (ex: 192.168.1.1)
            // - localhost
            // - 127.0.0.1
            
            try
            {
                _httpListener.Prefixes.Add($"http://{ipAddress}:{port}/");
                Console.WriteLine($"✓ Prefixo adicionado: http://{ipAddress}:{port}/");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"⚠️  Erro ao adicionar IP {ipAddress}: {ex.Message}");
            }
            
            try
            {
                _httpListener.Prefixes.Add($"http://localhost:{port}/");
                Console.WriteLine($"✓ Prefixo adicionado: http://localhost:{port}/");
            }
            catch { }
            
            try
            {
                _httpListener.Prefixes.Add($"http://127.0.0.1:{port}/");
                Console.WriteLine($"✓ Prefixo adicionado: http://127.0.0.1:{port}/");
            }
            catch { }
        }

        /// <summary>
        /// Inicia o servidor HTTP
        /// </summary>
        public void Start()
        {
            try
            {
                Console.WriteLine("\n[START] Iniciando HttpListener...");
                _httpListener.Start();
                _isRunning = true;
                Console.WriteLine($"✓ HttpListener iniciado");
                Console.WriteLine($"✓ API iniciada em {_baseUrl}");
                Console.WriteLine($"  localhost:5000 também está disponível");
                
                // Iniciar loop de requisições
                Console.WriteLine("\n[START] Criando Task para ListenForRequests()...");
                Task listenerTask = Task.Run(() => ListenForRequests());
                Console.WriteLine("[START] Task criada com sucesso");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Erro ao iniciar API: {ex.Message}");
                Console.WriteLine($"  StackTrace: {ex.StackTrace}");
            }
        }

        /// <summary>
        /// Loop de escuta por requisições HTTP
        /// </summary>
        private void ListenForRequests()
        {
            Console.WriteLine("\n[LISTENER] Iniciando loop de escuta de requisições...");
            int requestCount = 0;
            
            while (_isRunning)
            {
                try
                {
                    Console.WriteLine($"[LISTENER] Aguardando requisição #{requestCount + 1}...");
                    HttpListenerContext context = _httpListener.GetContext();
                    requestCount++;
                    Console.WriteLine($"[LISTENER] Requisição recebida! Total: {requestCount}");
                    Task.Run(() => HandleRequest(context));
                }
                catch (HttpListenerException ex) when (ex.ErrorCode == 995)
                {
                    // Listener foi encerrado
                    Console.WriteLine("[LISTENER] Listener encerrado (erro 995)");
                    break;
                }
                catch (ObjectDisposedException)
                {
                    Console.WriteLine("[LISTENER] HttpListener foi descartado");
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"✗ [LISTENER] Erro ao processar requisição: {ex.Message}");
                    Console.WriteLine($"              StackTrace: {ex.StackTrace}");
                }
            }
            
            Console.WriteLine("[LISTENER] Loop de escuta encerrado");
        }

        /// <summary>
        /// Processa uma requisição HTTP individual
        /// </summary>
        private void HandleRequest(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            Console.WriteLine($"\n[REQUEST] {request.HttpMethod} {request.Url.PathAndQuery}");
            Console.WriteLine($"[REQUEST] RemoteEndPoint: {request.RemoteEndPoint}");

            try
            {
                response.ContentType = "application/json; charset=utf-8";

                // Rotear requisições
                if (request.Url.AbsolutePath == "/imprimir" && request.HttpMethod == "POST")
                {
                    Console.WriteLine("[ROUTE] Roteando para /imprimir");
                    HandlePrintRequest(request, response);
                }
                else if (request.Url.AbsolutePath == "/status" && request.HttpMethod == "GET")
                {
                    Console.WriteLine("[ROUTE] Roteando para /status");
                    HandleStatusRequest(response);
                }
                else if (request.Url.AbsolutePath == "/limpar" && request.HttpMethod == "POST")
                {
                    Console.WriteLine("[ROUTE] Roteando para /limpar");
                    HandleClearQueueRequest(response);
                }
                else if (request.Url.AbsolutePath == "/imprimir-venda" && request.HttpMethod == "POST")
                {
                    Console.WriteLine("[ROUTE] Roteando para /imprimir-venda");
                    HandlePrintVendaRequest(request, response);
                }
                else if (request.Url.AbsolutePath == "/" && request.HttpMethod == "GET")
                {
                    Console.WriteLine("[ROUTE] Roteando para / (info)");
                    HandleInfoRequest(response);
                }
                else
                {
                    Console.WriteLine($"[ROUTE] Path desconhecido: {request.Url.AbsolutePath}");
                    SendResponse(response, 404, JsonSerialize(new { erro = "Endpoint não encontrado" }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ [HANDLER] Erro ao processar requisição: {ex.Message}");
                Console.WriteLine($"            StackTrace: {ex.StackTrace}");
                SendResponse(response, 500, JsonSerialize(new { erro = ex.Message }));
            }
            finally
            {
                try
                {
                    response.Close();
                    Console.WriteLine("[RESPONSE] Resposta fechada");
                }
                catch { }
            }
        }

        /// <summary>
        /// Handler para POST /imprimir
        /// </summary>
        private void HandlePrintRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                // Ler produto do query string
                string produtoEncoded = request.QueryString["produto"];
                
                // CORREÇÃO: HttpListener decodifica como Latin-1, mas os bytes são UTF-8
                // Precisamos reinterpretar como UTF-8 e depois converter para Windows-1252
                byte[] utf8Bytes = Encoding.GetEncoding(1252).GetBytes(produtoEncoded ?? "");
                string produtoUTF8 = Encoding.UTF8.GetString(utf8Bytes);

                if (string.IsNullOrWhiteSpace(produtoUTF8))
                {
                    SendResponse(response, 400, JsonSerialize(
                        new { erro = "Parâmetro 'produto' é obrigatório. Use: /imprimir?produto=NomeProduto" }
                    ));
                    return;
                }

                // Adicionar à fila (usar string UTF-8 corrigida)
                PrintJob job = _queueManager.EnqueuePrintJob(produtoUTF8);

                PrintResponse printResponse = new PrintResponse(true, "Cupom adicionado à fila", job.JobId.ToString());
                SendResponse(response, 200, JsonSerialize(printResponse));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ [ERRO] Falha na requisição /imprimir: {ex.Message}");
                SendResponse(response, 500, JsonSerialize(new { erro = ex.Message }));
            }
        }

        /// <summary>
        /// Handler para GET /status
        /// </summary>
        private void HandleStatusRequest(HttpListenerResponse response)
        {
            try
            {
                var status = _queueManager.GetQueueStatus();
                SendResponse(response, 200, JsonSerialize(status));
                Console.WriteLine($"  Status: Fila com {status["fila_size"]} trabalho(s)");
            }
            catch (Exception ex)
            {
                SendResponse(response, 500, JsonSerialize(new { erro = ex.Message }));
            }
        }

        /// <summary>
        /// Handler para POST /limpar
        /// </summary>
        private void HandleClearQueueRequest(HttpListenerResponse response)
        {
            try
            {
                _queueManager.ClearQueue();
                SendResponse(response, 200, JsonSerialize(new { sucesso = true, mensagem = "Fila limpa" }));
                Console.WriteLine($"  Ação: Fila limpa via API");
            }
            catch (Exception ex)
            {
                SendResponse(response, 500, JsonSerialize(new { erro = ex.Message }));
            }
        }

        /// <summary>
        /// Handler para POST /imprimir-venda (venda completa com múltiplos itens)
        /// </summary>
        private void HandlePrintVendaRequest(HttpListenerRequest request, HttpListenerResponse response)
        {
            try
            {
                using (var reader = new StreamReader(request.InputStream, Encoding.UTF8))
                {
                    string json = reader.ReadToEnd();
                    Console.WriteLine($"  JSON recebido: {json}");
                    
                    // Extrair vendaId, itens, numeroCaixa e descricaoCaixa do JSON
                    int vendaId = ExtractJsonInt(json, "vendaId");
                    var itens = ExtractJsonArray(json, "itens");
                    int numeroCaixa = ExtractJsonInt(json, "numeroCaixa");
                    string descricaoCaixa = ExtractJsonString(json, "descricaoCaixa");
                    
                    Console.WriteLine($"✓ Venda #{vendaId} recebida - Caixa #{numeroCaixa} - {itens.Count} item(ns)");
                    
                    // Enfileirar cada item da venda com dados do caixa
                    foreach (var item in itens)
                    {
                        _queueManager.EnqueuePrintJob(item, numeroCaixa, descricaoCaixa);
                    }
                    
                    SendResponse(response, 200, JsonSerialize(new { sucesso = true, vendaId = vendaId, numeroCaixa = numeroCaixa, itens = itens.Count }));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"✗ Erro ao processar /imprimir-venda: {ex.Message}");
                SendResponse(response, 500, JsonSerialize(new { erro = ex.Message }));
            }
        }

        /// <summary>
        /// Handler para GET /
        /// </summary>
        private void HandleInfoRequest(HttpListenerResponse response)
        {
            var info = new
            {
                servico = "Print Server Epson TM-T20",
                versao = "1.0",
                endpoints = new
                {
                    imprimir = new { metodo = "POST", url = "/imprimir?produto=NomeProduto", descricao = "Adiciona cupom à fila" },
                    imprimir_venda = new { metodo = "POST", url = "/imprimir-venda", descricao = "Imprime venda completa (múltiplos itens)" },
                    status = new { metodo = "GET", url = "/status", descricao = "Status atual da fila" },
                    limpar = new { metodo = "POST", url = "/limpar", descricao = "Limpa a fila de impressão" }
                }
            };
            SendResponse(response, 200, JsonSerialize(info));
            Console.WriteLine($"  Ação: Info solicitado");
        }

        /// <summary>
        /// Envia resposta HTTP
        /// </summary>
        private void SendResponse(HttpListenerResponse response, int statusCode, string content)
        {
            response.StatusCode = statusCode;
            byte[] buffer = Encoding.UTF8.GetBytes(content);
            response.ContentLength64 = buffer.Length;
            response.OutputStream.Write(buffer, 0, buffer.Length);
        }

        /// <summary>
        /// Serializa objeto para JSON (simples)
        /// </summary>
        private string JsonSerialize(object obj)
        {
            if (obj is string str)
                return str;

            // Serialização JSON manual para evitar dependências
            return SimpleJsonSerialize(obj);
        }

        /// <summary>
        /// Serialização JSON simples sem dependências
        /// </summary>
        private string SimpleJsonSerialize(object obj)
        {
            if (obj == null)
                return "null";

            var type = obj.GetType();
            
            // Tipos primitivos
            if (obj is bool b)
                return b ? "true" : "false";
            if (obj is int || obj is float || obj is double || obj is decimal)
                return obj.ToString();
            if (obj is string s)
                return "\"" + EscapeJsonString(s) + "\"";
            
            // Dicionários
            if (obj is System.Collections.Generic.Dictionary<string, object> dict)
            {
                var sb = new StringBuilder("{");
                bool first = true;
                foreach (var kvp in dict)
                {
                    if (!first) sb.Append(",");
                    sb.Append("\"").Append(kvp.Key).Append("\":");
                    sb.Append(SimpleJsonSerialize(kvp.Value));
                    first = false;
                }
                sb.Append("}");
                return sb.ToString();
            }

            // Listas
            if (obj is System.Collections.IEnumerable enumerable && !(obj is string))
            {
                var sb = new StringBuilder("[");
                bool first = true;
                foreach (var item in enumerable)
                {
                    if (!first) sb.Append(",");
                    sb.Append(SimpleJsonSerialize(item));
                    first = false;
                }
                sb.Append("]");
                return sb.ToString();
            }

            // Objetos anônimos
            var sb2 = new StringBuilder("{");
            var properties = type.GetProperties();
            bool isFirst = true;
            foreach (var prop in properties)
            {
                if (!isFirst) sb2.Append(",");
                sb2.Append("\"").Append(prop.Name).Append("\":");
                var value = prop.GetValue(obj);
                sb2.Append(SimpleJsonSerialize(value));
                isFirst = false;
            }
            sb2.Append("}");
            return sb2.ToString();
        }

        /// <summary>
        /// Escapa caracteres especiais para JSON
        /// </summary>
        private string EscapeJsonString(string str)
        {
            return str
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r")
                .Replace("\t", "\\t");
        }

        /// <summary>
        /// Extrai um inteiro do JSON
        /// </summary>
        private int ExtractJsonInt(string json, string key)
        {
            try
            {
                // Regex mais robusta: aceita espaços antes e depois dos dois-pontos
                string pattern = $"\"{key}\"\\s*:\\s*([0-9]+)";
                var match = System.Text.RegularExpressions.Regex.Match(json, pattern);
                if (match.Success && int.TryParse(match.Groups[1].Value, out int value))
                {
                    Console.WriteLine($"  → Extraído {key}: {value}");
                    return value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ Erro ao extrair {key}: {ex.Message}");
            }
            return 0;
        }

        /// <summary>
        /// Extrai string do JSON
        /// </summary>
        private string ExtractJsonString(string json, string key)
        {
            try
            {
                // Procura por "chave":"valor" com suporte a espaços
                string pattern = $"\"{key}\"\\s*:\\s*\\\"([^\\\"]*)\\\"";
                var match = System.Text.RegularExpressions.Regex.Match(json, pattern);
                if (match.Success)
                {
                    string value = match.Groups[1].Value;
                    Console.WriteLine($"  → Extraído {key}: {value}");
                    return value;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ Erro ao extrair {key}: {ex.Message}");
            }
            return "";
        }

        /// <summary>
        /// Extrai array de strings do JSON
        /// </summary>
        private List<string> ExtractJsonArray(string json, string key)
        {
            var result = new List<string>();
            try
            {
                // Regex mais robusta: [\s\S] captura incluindo newlines
                // Procura por "itens":[...] com espaçamento variável
                string pattern = $"\"{key}\"\\s*:\\s*\\[([\\s\\S]*?)\\]";
                var match = System.Text.RegularExpressions.Regex.Match(json, pattern);
                
                if (match.Success)
                {
                    string arrayContent = match.Groups[1].Value;
                    Console.WriteLine($"  → Conteúdo array: {arrayContent}");
                    
                    // Procurar por strings entre aspas (com suporte a acentuação)
                    var stringMatches = System.Text.RegularExpressions.Regex.Matches(arrayContent, "\"([^\"]*)\"");
                    foreach (System.Text.RegularExpressions.Match stringMatch in stringMatches)
                    {
                        string item = stringMatch.Groups[1].Value;
                        result.Add(item);
                        Console.WriteLine($"  → Item: {item}");
                    }
                }
                else
                {
                    Console.WriteLine($"  ✗ Array '{key}' não encontrado no JSON");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"  ✗ Erro ao extrair array: {ex.Message}");
            }
            return result;
        }

        /// <summary>
        /// Para o servidor
        /// </summary>
        public void Stop()
        {
            _isRunning = false;
            try
            {
                _httpListener?.Stop();
                _httpListener?.Close();
                Console.WriteLine("✓ API encerrada");
            }
            catch { }
        }
    }
}
