using System;
using System.IO.Ports;
using System.Text;
using System.Threading;
using System.Drawing;
using System.IO;
using System.Configuration;
using GestorEvento.Utilities;

namespace GestorEvento.Services
{
    /// <summary>
    /// Serviço de impressora térmica EPSON TM-20 com ESC/POS
    /// Versão para PrintServer (Console)
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

        public EpsonTM20Service(string portName = null, int baudRate = -1)
        {
            // Ler do App.config se não for passado valor
            if (string.IsNullOrWhiteSpace(portName))
            {
                portName = ConfigurationManager.AppSettings["PrinterPortName"] ?? "COM2";
            }
            
            if (baudRate <= 0)
            {
                if (!int.TryParse(ConfigurationManager.AppSettings["PrinterBaudRate"] ?? "9600", out baudRate))
                {
                    baudRate = 9600;
                }
            }

            _portName = portName;
            _baudRate = baudRate;
            _serialPort = new SerialPort(portName, baudRate, Parity.None, 8, StopBits.One)
            {
                Handshake = Handshake.None,
                WriteTimeout = 5000,
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
                return true;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao conectar na porta {_portName}: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Imprime uma imagem (PNG, BMP) no cupom
        /// </summary>
        private bool ImprimirImagem(string caminhoImagem)
        {
            try
            {
                if (!System.IO.File.Exists(caminhoImagem))
                {
                    UiHelper.ExibirAviso("Aviso", $"Imagem não encontrada: {caminhoImagem}");
                    return false;
                }

                using (Bitmap originalBitmap = new Bitmap(caminhoImagem))
                {
                    int maxWidth = 200;
                    int newWidth = originalBitmap.Width > maxWidth ? maxWidth : originalBitmap.Width;
                    int newHeight = (originalBitmap.Height * newWidth) / originalBitmap.Width;

                    Bitmap resizedBitmap = new Bitmap(originalBitmap, newWidth, newHeight);

                    Bitmap monoBitmap = new Bitmap(resizedBitmap.Width, resizedBitmap.Height);
                    for (int y = 0; y < resizedBitmap.Height; y++)
                    {
                        for (int x = 0; x < resizedBitmap.Width; x++)
                        {
                            Color pixel = resizedBitmap.GetPixel(x, y);
                            
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

                    byte[] imgData = BitmapToEscPosImage(monoBitmap);
                    _serialPort.Write(imgData, 0, imgData.Length);
                    _serialPort.BaseStream.Flush();
                    Thread.Sleep(500);

                    monoBitmap.Dispose();
                    resizedBitmap.Dispose();
                    return true;
                }
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao imprimir imagem: {ex.Message}");
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

            int widthBytes = (width + 7) / 8;
            byte[] imageData = new byte[widthBytes * height];

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    int luminance = (pixel.R + pixel.G + pixel.B) / 3;
                    bool isBlack = luminance < 128;

                    int byteIndex = (y * widthBytes) + (x / 8);
                    int bitIndex = 7 - (x % 8);

                    if (isBlack)
                    {
                        imageData[byteIndex] |= (byte)(1 << bitIndex);
                    }
                }
            }

            byte[] header = new byte[8];
            header[0] = 0x1D;
            header[1] = 0x76;
            header[2] = 0x30;
            header[3] = 0x00;
            header[4] = (byte)(widthBytes % 256);
            header[5] = (byte)(widthBytes / 256);
            header[6] = (byte)(height % 256);
            header[7] = (byte)(height / 256);

            byte[] result = new byte[header.Length + imageData.Length];
            System.Buffer.BlockCopy(header, 0, result, 0, header.Length);
            System.Buffer.BlockCopy(imageData, 0, result, header.Length, imageData.Length);

            return result;
        }

        /// <summary>
        /// Imprime cupom na impressora
        /// REFATORADO COM TRATATIVA COMPLETA DE ACENTUAÇÃO (v2.0)
        /// </summary>
        public bool ImprimirCupom(string nomeProduto, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            try
            {
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
                byte[] reset = { 0x1B, 0x40 }; // ESC @
                _serialPort.Write(reset, 0, reset.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(100);

                // ============ FASE 1.1: CONFIGURAR CODE PAGE PARA ACENTUAÇÃO ============
                byte[] setCodePage = { 0x1B, 0x74, 0x10 }; // ESC t 16 (Windows-1252 com acentuação)
                _serialPort.Write(setCodePage, 0, setCodePage.Length);
                _serialPort.BaseStream.Flush();

                byte[] lineFeed = { 0x0A };

                // ============ FASE 2: CONFIGURAÇÕES ============
                byte[] alignCenter = { 0x1B, 0x61, 0x01 };
                _serialPort.Write(alignCenter, 0, alignCenter.Length);
                
                byte[] boldOn = { 0x1B, 0x45, 0x01 };
                _serialPort.Write(boldOn, 0, boldOn.Length);
                
                byte[] font2x = { 0x1D, 0x21, 0x11 };
                _serialPort.Write(font2x, 0, font2x.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(30);

                // ============ FASE 3: IMPRIMIR TEXTO COM ACENTUAÇÃO ============
                byte[] productBytes = Encoding.GetEncoding(1252).GetBytes(nomeProduto);
                _serialPort.Write(productBytes, 0, productBytes.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(50);

                // ============ FASE 3.5: IMPRIMIR INFORMAÇÕES DE CAIXA ============
                _serialPort.Write(lineFeed, 0, lineFeed.Length);
                
                byte[] fontNormalCaixa = { 0x1D, 0x21, 0x00 };
                _serialPort.Write(fontNormalCaixa, 0, fontNormalCaixa.Length);
                
                byte[] alignCenterCaixa = { 0x1B, 0x61, 0x01 };
                _serialPort.Write(alignCenterCaixa, 0, alignCenterCaixa.Length);
                _serialPort.BaseStream.Flush();
                
                if (numeroCaixa > 0)
                {
                    string infoCaixa = $"Caixa #{numeroCaixa}";
                    if (!string.IsNullOrWhiteSpace(descricaoCaixa))
                    {
                        infoCaixa += $" - {descricaoCaixa}";
                    }
                    
                    byte[] caixaBytes = Encoding.GetEncoding(1252).GetBytes(infoCaixa);
                    _serialPort.Write(caixaBytes, 0, caixaBytes.Length);
                    _serialPort.BaseStream.Flush();
                    Thread.Sleep(30);
                }

                // ============ FASE 3.6: IMPRIMIR DATA E HORA COM ACENTUAÇÃO ============
                // 3 linhas vazias antes da data/hora
                for (int i = 0; i < 3; i++)
                {
                    _serialPort.Write(lineFeed, 0, lineFeed.Length);
                }
                
                byte[] alignRight = { 0x1B, 0x61, 0x02 };
                _serialPort.Write(alignRight, 0, alignRight.Length);
                
                byte[] fontSmall = { 0x1D, 0x21, 0x00 };
                _serialPort.Write(fontSmall, 0, fontSmall.Length);
                
                string dataHora = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
                byte[] dataHoraBytes = Encoding.GetEncoding(1252).GetBytes(dataHora);
                _serialPort.Write(dataHoraBytes, 0, dataHoraBytes.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(50);
                
                byte[] fontNormalSmall = { 0x1D, 0x21, 0x00 };
                _serialPort.Write(fontNormalSmall, 0, fontNormalSmall.Length);
                _serialPort.BaseStream.Flush();

                // ============ FASE 3.7: REDES SOCIAIS (RODAPÉ) ============
                // 2 linhas vazias antes das redes sociais
                for (int i = 0; i < 2; i++)
                {
                    _serialPort.Write(lineFeed, 0, lineFeed.Length);
                }
                
                // Align center
                byte[] alignCenterSocial = { 0x1B, 0x61, 0x01 };
                _serialPort.Write(alignCenterSocial, 0, alignCenterSocial.Length);
                
                // Fonte pequena
                byte[] fontSmallSocial = { 0x1D, 0x21, 0x00 };
                _serialPort.Write(fontSmallSocial, 0, fontSmallSocial.Length);
                _serialPort.BaseStream.Flush();
                
                // Imprimir redes sociais em uma linha
                string redesSociais = "@AliancaDeMisericordia.salto @CidadeRahamim";
                byte[] redesSociaisBytes = Encoding.GetEncoding(1252).GetBytes(redesSociais);
                _serialPort.Write(redesSociaisBytes, 0, redesSociaisBytes.Length);
                _serialPort.BaseStream.Flush();
                Thread.Sleep(20);

                // ============ FASE 4: RESET DE FORMATAÇÃO ============
                byte[] boldOff = { 0x1B, 0x45, 0x00 };
                _serialPort.Write(boldOff, 0, boldOff.Length);

                byte[] fontNormal = { 0x1D, 0x21, 0x00 };
                _serialPort.Write(fontNormal, 0, fontNormal.Length);

                byte[] alignLeft = { 0x1B, 0x61, 0x00 };
                _serialPort.Write(alignLeft, 0, alignLeft.Length);
                _serialPort.BaseStream.Flush();

                // ============ FASE 5: AVANÇAR PAPEL ============
                // Enviar 1 quebra de linha apenas
                _serialPort.Write(lineFeed, 0, lineFeed.Length);
                _serialPort.BaseStream.Flush();

                // ============ FASE 6: ESPERAR IMPRESSÃO TERMINAR ============
                Thread.Sleep(300);

                // ============ FASE 7: FEEDS EXTRAS ANTES DO CORTE ============
                // Removido: não adicionar mais linhas aqui
                _serialPort.BaseStream.Flush();
                // Agora apenas 1 linha de feed antes do corte (da FASE 5)

                // ============ FASE 8: CORTE FINAL ============
                byte[] paperCutFull = { 0x1D, 0x56, 0x41 }; // GS V A (Full Cut)
                _serialPort.Write(paperCutFull, 0, paperCutFull.Length);
                _serialPort.BaseStream.Flush();
                
                Thread.Sleep(150);
                
                byte[] paperCutPartial = { 0x1D, 0x56, 0x00 }; // GS V 0 (Partial Cut)
                _serialPort.Write(paperCutPartial, 0, paperCutPartial.Length);
                _serialPort.BaseStream.Flush();
                
                Thread.Sleep(500);
                
                return true;
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro de Impressão", $"Erro ao imprimir cupom: {ex.Message}");
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
                }
            }
            catch (Exception ex)
            {
                UiHelper.ExibirErro("Erro", $"Erro ao desconectar da impressora: {ex.Message}");
            }
        }

        public void Dispose()
        {
            Desconectar();
            _serialPort?.Dispose();
        }
    }
}
