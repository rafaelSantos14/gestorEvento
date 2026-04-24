using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Drawing;
using System.IO;

namespace GestorEvento.Services
{
    /// <summary>
    /// Serviço de impressora térmica EPSON TM-20 com ESC/POS
    /// 
    /// ANÁLISE DO PROBLEMA:
    /// - Desconectar/Conectar entre impressões causa problemas de fila
    /// - SerialPort precisa de Flush() para garantir envio de dados
    /// - Precisa esperar a impressora estar pronta antes de cortar
    /// - Não usar Thread.Sleep genérico, sincronizar com status
    /// </summary>
    public class EpsonTM20Service
    {
        private SerialPort _serialPort;
        private readonly string _portName;
        private readonly int _baudRate;
        private bool _isConnected = false;

        public EpsonTM20Service(string portName = "COM2", int baudRate = 9600)
        {
            _portName = portName;
            _baudRate = baudRate;
            _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.None,
                WriteTimeout = 5000,  // AUMENTADO para 5 segundos
                ReadTimeout = 5000,
                DtrEnable = true,
                RtsEnable = true
            };
        }

        public bool Conectar()
        {
            try
            {
                if (_isConnected && _serialPort.IsOpen)
                    return true;

                if (_serialPort.IsOpen)
                    _serialPort.Close();

                Thread.Sleep(500);
                _serialPort.Open();
                Thread.Sleep(500);
                
                _isConnected = true;
                System.Diagnostics.Debug.WriteLine($"✓ Conectado em {_portName}");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"✗ Erro ao conectar: {ex.Message}");
                return false;
            }
        }

        // DESATIVADO PARA TESTES DE VELOCIDADE
        /*
        /// <summary>
        /// Imprime dois logos lado a lado (Aliança e Rahamim)
        /// </summary>
        private bool ImprimirDoisLogos()
        {
            try
            {
                string caminhoAlianca = System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "img", "logo_impressao_alianca.png"
                );
                
                string caminhoRahamim = System.IO.Path.Combine(
                    System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location),
                    "img", "logo_impressao_rahamim.png"
                );

                if (!System.IO.File.Exists(caminhoAlianca))
                {
                    System.Diagnostics.Debug.WriteLine($"Logo Aliança não encontrada: {caminhoAlianca}");
                    return false;
                }

                if (!System.IO.File.Exists(caminhoRahamim))
                {
                    System.Diagnostics.Debug.WriteLine($"Logo Rahamim não encontrada: {caminhoRahamim}");
                    return false;
                }

                // Carregar ambas as imagens
                Bitmap bmpAlianca = new Bitmap(caminhoAlianca);
                Bitmap bmpRahamim = new Bitmap(caminhoRahamim);

                // Redimensionar para altura igual (máximo 90px cada)
                int maxHeight = 90;
                
                // Redimensionar Aliança
                int newHeightAlianca = bmpAlianca.Height > maxHeight ? maxHeight : bmpAlianca.Height;
                int newWidthAlianca = (bmpAlianca.Width * newHeightAlianca) / bmpAlianca.Height;
                Bitmap resizedAlianca = new Bitmap(bmpAlianca, newWidthAlianca, newHeightAlianca);

                // Redimensionar Rahamim
                int newHeightRahamim = bmpRahamim.Height > maxHeight ? maxHeight : bmpRahamim.Height;
                int newWidthRahamim = (bmpRahamim.Width * newHeightRahamim) / bmpRahamim.Height;
                Bitmap resizedRahamim = new Bitmap(bmpRahamim, newWidthRahamim, newHeightRahamim);

                // Ajustar para mesma altura
                int finalHeight = Math.Max(resizedAlianca.Height, resizedRahamim.Height);
                int totalWidth = resizedAlianca.Width + resizedRahamim.Width + 10; // 10px de espaço entre

                // Criar imagem combinada
                Bitmap combinedBitmap = new Bitmap(totalWidth, finalHeight);
                using (Graphics g = Graphics.FromImage(combinedBitmap))
                {
                    g.Clear(Color.White);
                    g.DrawImage(resizedAlianca, 0, (finalHeight - resizedAlianca.Height) / 2);
                    g.DrawImage(resizedRahamim, resizedAlianca.Width + 10, (finalHeight - resizedRahamim.Height) / 2);
                }

                // Converter para monocromático
                Bitmap monoBitmap = new Bitmap(combinedBitmap.Width, combinedBitmap.Height);
                for (int y = 0; y < combinedBitmap.Height; y++)
                {
                    for (int x = 0; x < combinedBitmap.Width; x++)
                    {
                        Color pixel = combinedBitmap.GetPixel(x, y);
                        
                        if (pixel.A < 128)
                        {
                            monoBitmap.SetPixel(x, y, Color.White);
                        }
                        else
                        {
                            int luminance = (pixel.R + pixel.G + pixel.B) / 3;
                            Color bwPixel = luminance < 128 ? Color.Black : Color.White;
                            monoBitmap.SetPixel(x, y, bwPixel);
                        }
                    }
                }

                // Enviar imagem via ESC/POS
                byte[] imgData = BitmapToEscPosImage(monoBitmap);
                _serialPort.Write(imgData, 0, imgData.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(500);

                // Limpar recursos
                bmpAlianca.Dispose();
                bmpRahamim.Dispose();
                resizedAlianca.Dispose();
                resizedRahamim.Dispose();
                combinedBitmap.Dispose();
                monoBitmap.Dispose();

                System.Diagnostics.Debug.WriteLine("✓ Dois logos impressos com sucesso");
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao imprimir dois logos: {ex.Message}");
                return false;
            }
        }
        */

        /// <summary>
        /// Imprime uma imagem (PNG, BMP) no cupom
        /// </summary>
        private bool ImprimirImagem(string caminhoImagem)
        {
            try
            {
                if (!System.IO.File.Exists(caminhoImagem))
                {
                    System.Diagnostics.Debug.WriteLine($"Imagem não encontrada: {caminhoImagem}");
                    return false;
                }

                using (Bitmap originalBitmap = new Bitmap(caminhoImagem))
                {
                    // Redimensionar para caber no papel (máximo 200px de largura - REDUZIDO)
                    int maxWidth = 200;
                    int newWidth = originalBitmap.Width > maxWidth ? maxWidth : originalBitmap.Width;
                    int newHeight = (originalBitmap.Height * newWidth) / originalBitmap.Width;

                    Bitmap resizedBitmap = new Bitmap(originalBitmap, newWidth, newHeight);

                    // Converter para monocromático (preto e branco) - inverter fundo transparente
                    Bitmap monoBitmap = new Bitmap(resizedBitmap.Width, resizedBitmap.Height);
                    for (int y = 0; y < resizedBitmap.Height; y++)
                    {
                        for (int x = 0; x < resizedBitmap.Width; x++)
                        {
                            Color pixel = resizedBitmap.GetPixel(x, y);
                            
                            // Se tem transparência, tratar como branco
                            if (pixel.A < 128)
                            {
                                monoBitmap.SetPixel(x, y, Color.White);
                            }
                            else
                            {
                                // Calcular luminância considerando alfa
                                int luminance = (pixel.R + pixel.G + pixel.B) / 3;
                                // Luminância baixa = preto, luminância alta = branco
                                Color bwPixel = luminance < 128 ? Color.Black : Color.White;
                                monoBitmap.SetPixel(x, y, bwPixel);
                            }
                        }
                    }

                    // Enviar imagem via ESC/POS
                    byte[] imgData = BitmapToEscPosImage(monoBitmap);
                    _serialPort.Write(imgData, 0, imgData.Length);
                    _serialPort.BaseStream.Flush();
                    Thread.Sleep(500);

                    monoBitmap.Dispose();
                    resizedBitmap.Dispose();
                    System.Diagnostics.Debug.WriteLine("✓ Imagem impressa com sucesso");
                    return true;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao imprimir imagem: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Converte Bitmap para dados ESC/POS
        /// </summary>
        private byte[] BitmapToEscPosImage(Bitmap bitmap)
        {
            int width = bitmap.Width;
            int height = bitmap.Height;

            // Alinhar largura para múltiplos de 8
            int widthBytes = (width + 7) / 8;
            byte[] imageData = new byte[widthBytes * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    // Calcular luminância do pixel
                    int luminance = (pixel.R + pixel.G + pixel.B) / 3;
                    // Se luminância baixa = preto = deve imprimir = bit 1
                    // Se luminância alta = branco = não deve imprimir = bit 0
                    bool isBlack = luminance < 128;

                    int byteIndex = (y * widthBytes) + (x / 8);
                    int bitIndex = 7 - (x % 8);

                    if (isBlack)
                    {
                        imageData[byteIndex] |= (byte)(1 << bitIndex);
                    }
                }
            }

            // Construir comando ESC/POS para imagem
            // GS v 0 (bit image normal)
            byte[] header = new byte[8];
            header[0] = 0x1D;           // GS
            header[1] = 0x76;           // v
            header[2] = 0x30;           // 0 (normal mode)
            header[3] = 0x00;           // mode

            // Largura em bytes (little endian)
            header[4] = (byte)(widthBytes % 256);
            header[5] = (byte)(widthBytes / 256);

            // Altura em linhas (little endian)
            header[6] = (byte)(height % 256);
            header[7] = (byte)(height / 256);

            byte[] result = new byte[header.Length + imageData.Length];
            System.Buffer.BlockCopy(header, 0, result, 0, header.Length);
            System.Buffer.BlockCopy(imageData, 0, result, header.Length, imageData.Length);

            return result;
        }

        /// <summary>
        /// REFATORADO COMPLETAMENTE
        /// Estratégia: Imprimir primeiro, DEPOIS cortar
        /// </summary>
        public bool ImprimirCupom(string nomeProduto)
        {
            try
            {
                System.Diagnostics.Debug.WriteLine("═══════════════════════════════════════");
                System.Diagnostics.Debug.WriteLine($"INICIANDO CUPOM: {nomeProduto}");
                System.Diagnostics.Debug.WriteLine("═══════════════════════════════════════");

                // Garantir conexão aberta
                if (!_isConnected)
                {
                    if (!Conectar())
                        return false;
                }

                if (!_serialPort.IsOpen)
                {
                    if (!Conectar())
                        return false;
                }

                // ============ FASE 1: RESET E INICIALIZAÇÃO ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 1] Reset da impressora");
                byte[] reset = { 0x1B, 0x40 }; // ESC @
                _serialPort.Write(reset, 0, reset.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(100); // Reduzido de 200ms
                System.Diagnostics.Debug.WriteLine("✓ Reset enviado");

                // ============ FASE 1.1: CONFIGURAR CODE PAGE ============
                System.Diagnostics.Debug.WriteLine("[FASE 1.1] Configurando code page para Windows-1252");
                byte[] setCodePage = { 0x1B, 0x74, 0x10 }; // ESC t 16 (Windows-1252 com acentuação)
                _serialPort.Write(setCodePage, 0, setCodePage.Length);
                _serialPort.BaseStream.Flush();
                // Sem delay aqui - consolidado
                System.Diagnostics.Debug.WriteLine("✓ Code page configurado");

                // Declarar variáveis de uso frequente
                byte[] lineFeed = { 0x0A };

                // ============ FASE 2: CONFIGURAÇÕES ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 2] Configurando formatação");

                // Align center
                byte[] alignCenter = { 0x1B, 0x61, 0x01 };
                _serialPort.Write(alignCenter, 0, alignCenter.Length);
                
                // Bold ON
                byte[] boldOn = { 0x1B, 0x45, 0x01 };
                _serialPort.Write(boldOn, 0, boldOn.Length);
                
                // Font 2x
                byte[] font2x = { 0x1D, 0x21, 0x11 };
                _serialPort.Write(font2x, 0, font2x.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(30); // Reduzido de 50ms
                System.Diagnostics.Debug.WriteLine("✓ Configurações enviadas");

                // ============ FASE 3: IMPRIMIR TEXTO ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 3] Imprimindo texto");
                byte[] productBytes = Encoding.GetEncoding(1252).GetBytes(nomeProduto);
                _serialPort.Write(productBytes, 0, productBytes.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(50); // Reduzido de 100ms
                System.Diagnostics.Debug.WriteLine($"✓ Texto enviado: {nomeProduto}");

                // ============ FASE 3.5: IMPRIMIR DATA E HORA ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 3.5] Imprimindo data e hora");
                
                // 3 linhas vazias entre produto e data
                for (int i = 0; i < 3; i++)
                {
                    _serialPort.Write(lineFeed, 0, lineFeed.Length);
                }
                // Sem delay aqui - consolidado
                
                // Reduzir tamanho da fonte
                byte[] fontSmall = { 0x1D, 0x21, 0x00 }; // Fonte pequena
                _serialPort.Write(fontSmall, 0, fontSmall.Length);
                
                // Data e hora
                string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                byte[] dataHoraBytes = Encoding.GetEncoding(1252).GetBytes(dataHora);
                _serialPort.Write(dataHoraBytes, 0, dataHoraBytes.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(50); // Reduzido de 100ms
                System.Diagnostics.Debug.WriteLine($"✓ Data/Hora enviada: {dataHora}");
                
                // Voltar para fonte normal
                byte[] fontNormalSmall = { 0x1D, 0x21, 0x00 };
                _serialPort.Write(fontNormalSmall, 0, fontNormalSmall.Length);
                _serialPort.BaseStream.Flush();
                System.Diagnostics.Debug.WriteLine($"✓ Data/Hora enviada: {dataHora}");

                // ============ FASE 3.6: LOGO (IMAGEM) - DESATIVADO PARA TESTES ============
                //System.Diagnostics.Debug.WriteLine("\n[FASE 3.6] Imprimindo logos");
                //
                //// 2 quebras de linha
                //for (int i = 0; i < 2; i++)
                //{
                //    _serialPort.Write(lineFeed, 0, lineFeed.Length);
                //}
                //_serialPort.BaseStream.Flush();
                //Thread.Sleep(50);
                //
                //// Align center
                //byte[] alignCenterLogo = { 0x1B, 0x61, 0x01 };
                //_serialPort.Write(alignCenterLogo, 0, alignCenterLogo.Length);
                //_serialPort.BaseStream.Flush();
                //
                //// Imprimir dois logos lado a lado
                //bool logosImpressos = ImprimirDoisLogos();
                //if (!logosImpressos)
                //{
                //    System.Diagnostics.Debug.WriteLine("⚠ Logos não encontrados, continuando sem logos");
                //}
                //
                //// Quebra de linha
                //_serialPort.Write(lineFeed, 0, lineFeed.Length);
                //_serialPort.BaseStream.Flush();
                //Thread.Sleep(100);
                //System.Diagnostics.Debug.WriteLine("✓ Logos processados");

                // ============ FASE 4: RESET DE FORMATAÇÃO ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 4] Resetando formatação");

                // Bold OFF
                byte[] boldOff = { 0x1B, 0x45, 0x00 };
                _serialPort.Write(boldOff, 0, boldOff.Length);

                // Font normal
                byte[] fontNormal = { 0x1D, 0x21, 0x00 };
                _serialPort.Write(fontNormal, 0, fontNormal.Length);

                // Align left
                byte[] alignLeft = { 0x1B, 0x61, 0x00 };
                _serialPort.Write(alignLeft, 0, alignLeft.Length);
                _serialPort.BaseStream.Flush();
                // Sem delay aqui - consolidado
                System.Diagnostics.Debug.WriteLine("✓ Formatação resetada");

                // ============ FASE 5: AVANÇAR PAPEL ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 5] Avançando papel");
                
                // Enviar 2 quebras de linha (reduzido de 5)
                for (int i = 0; i < 2; i++)
                {
                    _serialPort.Write(lineFeed, 0, lineFeed.Length);
                }
                _serialPort.BaseStream.Flush();
                System.Diagnostics.Debug.WriteLine("✓ 2 quebras de linha enviadas");

                // ============ FASE 6: ESPERAR IMPRESSÃO TERMINAR ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 6] Aguardando impressão");
                System.Diagnostics.Debug.WriteLine("Esperando 300ms...");
                Thread.Sleep(300); // Reduzido de 500ms
                System.Diagnostics.Debug.WriteLine("✓ Tempo de impressão decorrido");

                // ============ FASE 7: MAIS FEEDS ANTES DE CORTAR ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 7] Enviando feeds extras antes do corte");
                _serialPort.Write(lineFeed, 0, lineFeed.Length); // 1 quebra de linha apenas
                _serialPort.BaseStream.Flush();
                Thread.Sleep(100); // Reduzido de 200ms
                System.Diagnostics.Debug.WriteLine("✓ 1 feed extra enviado");
                System.Diagnostics.Debug.WriteLine("✓ 3 feeds extras enviados");

                // ============ FASE 8: CORTE (FINAL) ============
                System.Diagnostics.Debug.WriteLine("\n[FASE 8] ENVIANDO CORTE");
                
                // Tentar comando full cut
                byte[] paperCutFull = { 0x1D, 0x56, 0x41 }; // GS V A
                System.Diagnostics.Debug.WriteLine("Enviando: 0x1D 0x56 0x41 (Full Cut)");
                _serialPort.Write(paperCutFull, 0, paperCutFull.Length);
                _serialPort.BaseStream.Flush();
                
                System.Diagnostics.Debug.WriteLine("Aguardando 150ms para execução...");
                Thread.Sleep(150); // Reduzido de 300ms
                
                // Se não funcionou, tentar partial cut
                byte[] paperCutPartial = { 0x1D, 0x56, 0x00 }; // GS V 0
                System.Diagnostics.Debug.WriteLine("Enviando fallback: 0x1D 0x56 0x00 (Partial Cut)");
                _serialPort.Write(paperCutPartial, 0, paperCutPartial.Length);
                _serialPort.BaseStream.Flush();
                
                Thread.Sleep(500); // Reduzido de 800ms
                System.Diagnostics.Debug.WriteLine("✓ Comandos de corte enviados");

                System.Diagnostics.Debug.WriteLine("\n═══════════════════════════════════════");
                System.Diagnostics.Debug.WriteLine($"✓✓✓ CUPOM COMPLETO: {nomeProduto}");
                System.Diagnostics.Debug.WriteLine("═══════════════════════════════════════\n");
                
                return true;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"\n✗✗✗ ERRO: {ex.Message}\n");
                return false;
            }
        }

        public void Desconectar()
        {
            try
            {
                if (_serialPort?.IsOpen == true)
                {
                    _serialPort.Close();
                    _isConnected = false;
                    System.Diagnostics.Debug.WriteLine("Desconectado da impressora");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Erro ao desconectar: {ex.Message}");
            }
        }

        public void Dispose()
        {
            Desconectar();
            _serialPort?.Dispose();
        }
    }
}
