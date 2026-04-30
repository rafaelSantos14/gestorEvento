# CONTEXTO_PRINTER_SERVER.md

## 1. Título e Descrição

**Nome do Projeto:** EpsonTM20Service - Serviço de Impressora Térmica EPSON TM-20

**Breve Resumo:** Serviço especializado de comunicação com impressora térmica EPSON TM-20 via protocolo ESC/POS, otimizado para emissão de cupons de ponto de venda.

---

## 2. Objetivo / Propósito

**Problema Resolvido:**
- Comunição confiável com impressora térmica via porta serial (COM)
- Emissão de cupons formatados corretamente em tempo real
- Garantia de que papel é impresso ANTES de ser cortado
- Suporte a caracteres especiais português (acentuação, cedilha)
- Otimização de performance para não deixar cliente esperando
- Tratamento de erros e reconexão automática

**Público-Alvo / Cenário de Uso:**
- Serviço é para ser usado em um cenário onde existe vários caixas e somente uma impressora. Com o serviço é possível configurar o sistema para utilizar esse serviço para orquestrar as impressões em apenas uma impressora.

---

## 3. Funcionalidades

### 3.1 Principais Features

#### **Inicialização e Conexão**
- Conectar a porta COM configurada em App.config (propriedade: PrinterPortName)
- Velocidade serial configurada em App.config (propriedade: PrinterBaudRate)
- Verificação de disponibilidade da impressora
- Tentativa de reconexão em caso de desconexão

#### **Impressão de Cupom**
Impressão simplificada de um produto:
1. **Nome do Produto** - Descrição do item
2. **Dados do Caixa** - Número/identificação do caixa ou PDV
3. **Data e Hora** - Timestamp da impressão
4. **Corte de Papel** - Corta papel para próxima impressão

**Nota:** Método `ImprimirCupom()` é para impressão individual de produtos. Para impressão completa de venda com detalhes, formas de pagamento e troco, usar `ImprimirVenda()` (via PrinterServiceFactory)

#### **Formatação de Texto**
- Centralização automática
- Negrito / Normal
- Diferentes tamanhos de fonte (normal, pequeno, grande)
- Espaçamento e linhas

#### **Tratamento de Caracteres**
- Código de página: Windows-1252
- Suporte a acentuação (á, é, í, ó, ú, ã, õ)
- Suporte a cedilha (ç)
- Caracteres especiais (€, £, §, etc)

#### **Diagnostóstico**
- Status da impressora (conectada, desconectada)
- Teste de impressão
- Log de erros

---

## 4. Arquitetura / Estrutura do Serviço

### 4.1 Arquitetura de Camadas

```
EpsonTM20Service (Classe Principal)
  ├── SerialPort (Comunicação com hardware)
  ├── Encoding (Conversão de caracteres)
  ├── Buffer de Comandos (Otimização de performance)
  └── Error Handling (Tratamento de falhas)
```

### 4.2 Estrutura de Código

#### **EpsonTM20Service.cs** (Principal)

```csharp
public class EpsonTM20Service
{
    // Configurações (sempre lidas de App.config)
    private string _portName;
    private int _baudRate;
    
    // Componentes
    private SerialPort _serialPort;
    private Encoding _encoding;
    
    // Métodos Públicos
    public bool TestarConexao()
    public bool ImprimirCupom(int idVenda)
    public void Desconectar()
    
    // Métodos Privados (Orquestração)
    private void SetarPaginaISO()
    private void CentralizarTexto()
    private void ImprimirCabecalho()
    private void ImprimirDetalhes(List<ItemVenda> items)
    private void ImprimirFormasPagamento(List<Recebimento> recebimentos)
    private void ImprimirTroco(decimal vlTroco)
    private void CortarPapel()
    
    // Métodos Privados (Utilidades)
    private void EscreverComando(byte[] comando)
    private void EscreverTexto(string texto)
    private void Flush()
}
```

### 4.3 Fluxo de Execução

```
ImprimirCupom(nomeProduto, numeroCaixa = 0, descricaoCaixa = "")
  │
  ├─ Validar conexão
  │
  ├─ SetarPaginaISO() [ESC @ para resetar]
  │
  ├─ SetarEncodingPortugues() [ESC t 0x10 para Windows-1252]
  │
  ├─ Imprimir Nome do Produto
  │   └─ nomeProduto (ex: "REFRIGERANTE #1")
  │
  ├─ Imprimir Dados do Caixa
  │   ├─ Número do caixa (numeroCaixa)
  │   └─ Descrição do caixa (descricaoCaixa)
  │
  ├─ Imprimir Data e Hora
  │   └─ DateTime.Now (ex: "30/04/2026 14:30:45")
  │
  ├─ CortarPapel() [GS V 0x42 0x00 para corte completo]
  │   └─ Espera 500ms para cortar antes de liberar
  │
  └─ return true (sucesso) ou false (erro)
```

**Notas Importantes:**
- Este fluxo é para `ImprimirCupom()` - impressão simples de um único produto
- Para impressão completa de venda (múltiplos itens, formas de pagamento, troco), usar `ImprimirVenda()` via `PrinterServiceFactory`
- Método `ImprimirCupom()` é chamado iterativamente por `ImprimirVendaLocal()` para cada item da venda

---

## 5. Tecnologias Utilizadas

### 5.1 Linguagem e Framework
- **Linguagem:** C# 7.3
- **Framework:** .NET Framework 4.7.2
- **Namespace:** System.IO.Ports (SerialPort)

### 5.2 Protocolo de Comunicação
- **Protocolo:** ESC/POS (EPSON Standard Code for Point Of Sale)
- **Tipo de Conexão:** Serial (COM)
- **Velocidade:** 9600 baud
- **Bits de Dados:** 8
- **Stop Bits:** 1
- **Paridade:** None
- **Handshake:** None

### 5.3 Encoding de Caracteres
- **Encoding Principal:** Windows-1252 (cp1252)
- **Command:** ESC t 0x10 (selecionar página 16 = Windows-1252)
- **Fallback:** UTF-8 → Windows-1252 (conversão automática)

### 5.4 Hardware
- **Impressora:** EPSON TM-20 (M249A)
- **Interface:** USB (virtual COM2 no Windows)
- **Resolução:** 8 dots/mm (203 dpi)
- **Largura de Papel:** 80mm
- **Velocidade de Impressão:** até 250mm/segundo

### 5.5 Bibliotecas do Sistema
- `System.IO.Ports` - Comunicação serial
- `System.Text` - Encoding
- `System.Threading` - Sleep/delays
- `System.Diagnostics` - Debug output

---

## 6. Instalação / Configuração

### 6.1 Pré-requisitos

1. **Driver EPSON TM-20** instalado
2. **Impressora EPSON TM-20** conectada via USB
3. **Windows (qualquer versão)** com suporte a COM
4. **Porta COM virtual** criada (COM1-COM9 típicamente)

### 6.2 Passos para Configuração

#### Passo 1: Instalar Driver EPSON

1. Baixar driver em https://www.epson.com.br/
2. Procurar por "EPSON TM-20 Driver"
3. Instalar executável
4. Aceitar licenças e configurações padrão

#### Passo 2: Conectar Impressora via USB

1. Conectar cabo USB da impressora ao computador
2. Windows detectará automaticamente
3. Abrir "Gerenciador de Dispositivos" (Win+R → devmgmt.msc)
4. Expandir "Portas (COM e LPT)"
5. Identificar porta da impressora (ex: COM2, COM3)
6. **Anotar número da porta** (usaremos em configuração)

#### Passo 3: Configurar Porta no App.config

Editar `App.config`:

```xml
<configuration>
  <appSettings>
    <!-- Configuração de Porta Serial -->
    <add key="PrinterPortName" value="COM2" />
    <add key="PrinterBaudRate" value="9600" />
  </appSettings>
</configuration>
```

**Nota:** Nunca hardcode a porta no código C#. Sempre leia do App.config para permitir reconfiguração sem recompilar.

#### Passo 4: Integrar no Projeto

Em `FormAbrirCaixa.cs` ou `FormPDV.cs`:

```csharp
// Instanciar serviço (sem parâmetros - lê de App.config automaticamente)
EpsonTM20Service servicoImpressora = new EpsonTM20Service();

// Testar conexão
if (!servicoImpressora.Conectar()) {
    MessageBox.Show("Impressora não conectada!");
    return;
}

// Testar disponibilidade
if (!servicoImpressora.TestarConexao()) {
    MessageBox.Show("Impressora não responde!");
    return;
}

// Após confirmar venda
servicoImpressora.ImprimirCupom(nomeProduto, numeroCaixa, descricaoCaixa);

// Sempre desconectar
servicoImpressora.Desconectar();
```

#### Passo 5: Verificar Configuração

1. Abrir `App.config` e verificar:
   ```xml
   <add key="PrinterPortName" value="COM2" />
   <add key="PrinterBaudRate" value="9600" />
   ```

2. Se as chaves não existirem, adicionar manualmente (valores acima são os padrões)

3. Ligar impressora

4. Colocar papel

5. Executar teste:
```csharp
EpsonTM20Service teste = new EpsonTM20Service();

if (teste.Conectar()) {
    if (teste.TestarConexao()) {
        Console.WriteLine("✓ Impressora conectada e respondendo!");
    } else {
        Console.WriteLine("✗ Impressora não responde!");
    }
    teste.Desconectar();
} else {
    Console.WriteLine("✗ Não foi possível abrir a porta COM!");
}
```

**Troubleshooting:**
- Se disser "Porta COM não encontrada": Verificar em App.config se `PrinterPortName` está correto
- Se disser "Porta ocupada": Outra aplicação está usando a porta, ou desconectar anterior falhou

---

## 7. Padrões de Código do Serviço

### 7.1 Padrão de Escrita (CRÍTICO)

```csharp
// ❌ ERRADO - Sem Flush (comando fica em buffer)
_serialPort.Write(comando, 0, comando.Length);
_serialPort.Write(outroComando, 0, outroComando.Length);

// ✅ CORRETO - Com Flush (garante execução)
_serialPort.Write(comando, 0, comando.Length);
_serialPort.BaseStream.Flush();
_serialPort.Write(outroComando, 0, outroComando.Length);
_serialPort.BaseStream.Flush();

// ✅ OTIMIZADO - Consolidar antes de Flush (mais rápido)
byte[] buffer = Concatenar(comando1, comando2, comando3);
_serialPort.Write(buffer, 0, buffer.Length);
_serialPort.BaseStream.Flush();
```

### 7.2 Padrão de Encoding

```csharp
// Configurar encoding Windows-1252
private Encoding _encoding = Encoding.GetEncoding(1252);

// Converter string para bytes
string texto = "São Paulo";
byte[] bytes = _encoding.GetBytes(texto);
_serialPort.Write(bytes, 0, bytes.Length);
_serialPort.BaseStream.Flush();
```

### 7.3 Padrão de Comando ESC/POS

```csharp
// Comando = ESC (0x1B) + código + parâmetro
// Exemplo: Centralizar = ESC a 1

private byte ESC = 0x1B;
private byte GS = 0x1D;

// Comando: Resetar
byte[] resetar = new byte[] { ESC, 0x40 };

// Comando: Centralizar
byte[] centralizar = new byte[] { ESC, 0x61, 0x01 };

// Comando: Cortar papel
byte[] cortar = new byte[] { GS, 0x56, 0x42, 0x00 };
```

---

## 8. Fluxo de Dados / Diagrama de Componentes

### 8.1 Fluxo de Impressão Completo (ImprimirCupom)

```
                  ┌─────────────────────────────────┐
                  │  PrinterServiceFactory.         │
                  │  ImprimirVendaLocal()           │
                  └────────────┬────────────────────┘
                               │
                   Para cada item da venda:
                               │
                               ▼
                  ┌─────────────────────────────────┐
                  │  EpsonTM20Service.              │
                  │  ImprimirCupom(nomeProduto)     │
                  └────────────┬────────────────────┘
                               │
        ┌──────────────────────┼──────────────────────┐
        │                      │                      │
        ▼                      ▼                      ▼
┌──────────────┐        ┌──────────────┐      ┌──────────────┐
│ Validar      │        │ Resetar      │      │ Setar        │
│ Conexão      │        │ Impressora   │      │ Encoding     │
└──────┬───────┘        └──────┬───────┘      │ Windows-1252 │
       │                       │               └──────┬───────┘
       └───────────┬───────────┴────────────────────┬─┘
                   │
                   ▼
        ┌──────────────────────────────────┐
        │ Imprimir Nome do Produto         │
        │ (ex: "REFRIGERANTE #1")          │
        └──────┬───────────────────────────┘
               │
               ▼
        ┌──────────────────────────────────┐
        │ Imprimir Dados do Caixa          │
        │ (número e descrição do PDV)      │
        └──────┬───────────────────────────┘
               │
               ▼
        ┌──────────────────────────────────┐
        │ Imprimir Data e Hora             │
        │ (DateTime.Now)                   │
        └──────┬───────────────────────────┘
               │
               ▼
        ┌──────────────────────────────────┐
        │ Cortar Papel (GS V)              │
        │ (pronto para próximo cupom)      │
        └──────┬───────────────────────────┘
               │
               ▼
        ┌──────────────────────┐
        │ return true/false    │
        └──────────────────────┘
```

**Contexto Importante:**
- Este diagrama mostra o fluxo do método `ImprimirCupom()` para **UM ÚNICO PRODUTO**
- O método é chamado iterativamente por `PrinterServiceFactory.ImprimirVendaLocal()` para cada item da venda
- Cada chamada imprime um cupom simples e corta o papel
- Para visualizar como múltiplos cupons são impressos em sequência, ver seção 8.2 "Buffer de Comunicação"

**Fluxo Completo de Venda (múltiplos itens):**
```
FormPDV.BtnConfirmarVenda()
    ↓
VendaService.RegistrarVendaComTrocoComTransacao()  [ACID]
    ↓
PrinterServiceFactory.ImprimirVenda(vendaId, itensPorImprimir)
    ↓
PrinterServiceFactory.ImprimirVendaLocal()
    ├─ Para cada item em itensPorImprimir:
    │   └─ EpsonTM20Service.ImprimirCupom(item)  ✓ Cupom 1
    │   └─ EpsonTM20Service.ImprimirCupom(item)  ✓ Cupom 2
    │   └─ EpsonTM20Service.ImprimirCupom(item)  ✓ Cupom N
    └─ return true
    ↓
✓ Venda completa impressa
```

### 8.2 Fluxo de Impressão Completo (Venda Completa)

Quando uma venda é confirmada, **todos os itens são impressos** chamando `PrinterServiceFactory.ImprimirVenda()`:

```
                  ┌──────────────────────────────────┐
                  │  FormPDV.BtnConfirmarVenda()     │
                  │  (Venda com múltiplos itens)     │
                  └────────────┬─────────────────────┘
                               │
                               ▼
                  ┌──────────────────────────────────┐
                  │  VendaService.                   │
                  │  RegistrarVendaComTroco...()     │
                  │  (ACID Transaction)              │
                  └────────────┬─────────────────────┘
                               │
                    ✓ Venda inserida no banco
                    ✓ Recebimentos registrados
                    ✓ Troco registrado (se houver)
                               │
                               ▼
                  ┌──────────────────────────────────┐
                  │  PrinterServiceFactory.          │
                  │  ImprimirVenda(                  │
                  │    vendaId,                      │
                  │    itensPorImprimir,             │
                  │    numeroCaixa,                  │
                  │    descricaoCaixa                │
                  │  )                               │
                  └────────────┬─────────────────────┘
                               │
                ┌──────────────┴──────────────┐
                │                             │
                ▼                             ▼
        ┌──────────────────┐      ┌──────────────────┐
        │ PrintMode=LOCAL  │      │ PrintMode=REMOTE │
        └────────┬─────────┘      └────────┬─────────┘
                 │                         │
                 ▼                         ▼
        ┌──────────────────┐      ┌──────────────────┐
        │ ImprimirVenda    │      │ ImprimirVenda    │
        │ Local()          │      │ ViaAPI()         │
        └────────┬─────────┘      └────────┬─────────┘
                 │                         │
                 │                   POST HTTP/JSON
                 │                 para Print Server
                 │                         │
        ┌────────▼──────────────────────┐ │
        │ Para cada item em itensPor... │ │
        │ Imprimir:                     │ │
        └────────┬──────────────────────┘ │
                 │                         │
    ┌────────────┼────────────┐            │
    │            │            │            │
    ▼            ▼            ▼            │
┌─────────┐  ┌─────────┐  ┌─────────┐    │
│ Item 1  │  │ Item 2  │  │ Item N  │    │
│ Cupom 1 │  │ Cupom 2 │  │ Cupom N │    │
│  CUT    │  │  CUT    │  │  CUT    │    │
└────┬────┘  └────┬────┘  └────┬────┘    │
     │            │            │          │
     └────────────┼────────────┘          │
                  │                       │
                  ▼                       │
        ┌──────────────────┐              │
        │ ✓ Todos itens    │              │
        │   impressos      │              │
        └──────────────────┘              │
                                          │
                  ┌───────────────────────┘
                  │
                  ▼ (se Print Server processa com sucesso)
        ┌──────────────────┐
        │ ✓ Venda Completa │
        │   Impressa!      │
        └──────────────────┘
```

---

## 9. Decisões Técnicas / Considerações Importantes

### 9.1 Por que Windows-1252 e não UTF-8?

**Problema:** Impressora espera single-byte, UTF-8 é multi-byte
- UTF-8: "ã" = 0xC3 0xA3 (2 bytes)
- Windows-1252: "ã" = 0xE3 (1 byte)

**Resultado:**
- UTF-8 → Impressora recebe 0xC3 e pula 0xA3 → caractere quebrado
- Windows-1252 → Impressora recebe 0xE3 → caractere correto

**Solução:**
```csharp
Encoding.GetEncoding(1252)  // Força single-byte
+ ESC t 0x10                 // Command para Windows-1252
```

### 9.2 Por que BaseStream.Flush() é crítico?

**Problema:** SerialPort acumula dados em buffer (otimização)
- Write() apenas enfileira dados
- Dados ficam em buffer aguardando mais dados
- Pode não enviar se buffer não encher

**Resultado:**
- Comando enviado lentamente
- Papel começa a cortar ANTES de imprimir

**Solução:**
```csharp
_serialPort.Write(comando);
_serialPort.BaseStream.Flush();  // FORÇA envio imediato
```

### 9.3 Por que 1.3 segundos é o mínimo?

**Limitações físicas:**
- Impressora térmica: ~250mm/segundo
- Papel 80mm + margens = ~120mm real
- Mínimo físico: ~120/250 = 0.48 segundos
- Overhead software: ~0.8 segundos
- **Total: ~1.3 segundos** (não pode ser mais rápido sem mudar hardware)

**Otimizações já aplicadas:**
- ✓ Consolidar comandos antes de Flush()
- ✓ Reduzir Thread.Sleep() (200→100, 500→300, 800→500)
- ✓ Retirar delays desnecessários
- ✓ Pré-compilar comandos

### 9.4 Por que não usar port.ReadLine()?

**Problema:** Impressora não envia feedback significativo em ESC/POS básico
- ESC/POS é one-way (aplicação → impressora)
- Impressora só envia dados em modo específico (status)

**Solução:** Usar apenas Write(), não Read()
- Testar conexão com comando soft-reset (não espera resposta)
- Confiar em exceções se porta desconectar

---

## 13. API / Métodos Principais

### PrinterServiceFactory (Factory Pattern)

```csharp
// MÉTODO ESTÁTICO PÚBLICO
public static bool ImprimirCupom(string nomeProduto)
    // Parâmetro: nomeProduto - Nome/descrição do produto a imprimir
    // Retorna: true se impresso com sucesso, false se erro
    // Efeito: Impressão simples de UM PRODUTO (nome + caixa + data + corte)
    // Uso: Teste individual de impressão
    // Performance: ~1.3 segundos

// MÉTODO ESTÁTICO PÚBLICO (PRINCIPAL)
public static bool ImprimirVenda(int vendaId, List<string> itens, 
                                 int numeroCaixa = 0, string descricaoCaixa = "")
    // Parâmetros:
    //   - vendaId: ID da venda (para log/auditoria)
    //   - itens: Lista de produtos a imprimir
    //   - numeroCaixa: Número do PDV/caixa (padrão: 0)
    //   - descricaoCaixa: Descrição/nome do caixa (padrão: "")
    // Retorna: true se todos itens impressos com sucesso, false se erro
    // Efeito: Orquestra impressão de todos os itens (LOCAL ou REMOTE)
    // Decisão: Lê App.config PrintMode e chama ImprimirVendaLocal() ou ImprimirVendaViaAPI()
    // Performance: ~1.3s × quantidade_de_itens
    // Exemplo: 3 itens = ~3.9 segundos

// MÉTODOS PRIVADOS (INTERNOS)
private static bool ImprimirVendaLocal(int vendaId, List<string> itens, 
                                       int numeroCaixa = 0, string descricaoCaixa = "")
    // Impressão LOCAL via SerialPort
    // Itera cada item e chama EpsonTM20Service.ImprimirCupom()

private static bool ImprimirVendaViaAPI(int vendaId, List<string> itens, 
                                        int numeroCaixa = 0, string descricaoCaixa = "")
    // Impressão REMOTA via HTTP POST
    // Envia JSON com todos os itens para Print Server
```

### EpsonTM20Service (Classe Principal)

```csharp
// CONSTRUTOR
public EpsonTM20Service()
    // Efeito: Lê configurações de App.config
    //   - PrinterPortName (padrão se não existir: COM2)
    //   - PrinterBaudRate (padrão se não existir: 9600)
    // Inicializa SerialPort e Encoding Windows-1252
    // Código:
    //   _portName = ConfigurationManager.AppSettings["PrinterPortName"] ?? "COM2";
    //   _baudRate = int.Parse(ConfigurationManager.AppSettings["PrinterBaudRate"] ?? "9600");

// MÉTODO PÚBLICO - TESTAR CONEXÃO
public bool Conectar()
    // Retorna: true se porta abriu com sucesso, false se erro
    // Efeito: Abre SerialPort usando _portName (de App.config)
    // Exceção: Lança exception se porta já está aberta ou não existe

public bool TestarConexao()
    // Retorna: true se impressora responde, false se erro
    // Efeito: Envia comando soft-reset (ESC @) e aguarda resposta
    // Performance: ~200ms

// MÉTODO PÚBLICO - IMPRIMIR CUPOM (PRINCIPAL)
public bool ImprimirCupom(string nomeProduto, int numeroCaixa = 0, string descricaoCaixa = "")
    // Parâmetros:
    //   - nomeProduto: Nome/descrição do produto (ex: "REFRIGERANTE 2L")
    //   - numeroCaixa: Número do caixa/PDV (padrão: 0)
    //   - descricaoCaixa: Descrição do caixa (padrão: "")
    // Retorna: true se impresso com sucesso, false se erro
    // O que imprime (simplificado):
    //   1. Nome do produto (ex: "REFRIGERANTE 2L")
    //   2. Dados do caixa (ex: "Caixa 1 - PDV LOJA A")
    //   3. Data e hora (DateTime.Now formatada)
    //   4. Corte de papel (GS V 0x42 0x00)
    // Performance: ~1.3 segundos por cupom
    // Nota: NÃO imprime detalhes, formas de pagamento ou troco
    //       Esses são impressos iterativamente para múltiplos itens

// MÉTODO PÚBLICO - DESCONECTAR
public void Desconectar()
    // Efeito: Fecha SerialPort e libera recursos
    // Seguro chamar mesmo se já está desconectado

public void Dispose()
    // Efeito: Implementa IDisposable, chama Desconectar()

// MÉTODOS PRIVADOS (USO INTERNO)
private void EscreverTexto(string texto)
    // Converte string para Windows-1252 e envia para porta
    // SEMPRE seguido de BaseStream.Flush()

private void EscreverComando(byte[] comando)
    // Envia bytes brutos (comandos ESC/POS)
    // SEMPRE seguido de BaseStream.Flush()

private void Flush()
    // Força envio imediato de dados do buffer SerialPort
    // CRÍTICO: Sem isso, dados ficam acumulados
```

### Resumo de Métodos Realmente Implementados

**PrinterServiceFactory.cs (2 métodos públicos):**
1. `ImprimirCupom(nomeProduto)` - Simples, para teste individual
2. `ImprimirVenda(vendaId, itens, numeroCaixa, descricaoCaixa)` - Principal, para venda completa

**EpsonTM20Service.cs (4 métodos públicos):**
1. `Conectar()` - Abre porta COM
2. `TestarConexao()` - Verifica disponibilidade
3. `ImprimirCupom(nomeProduto, numeroCaixa, descricaoCaixa)` - Imprime cupom simples
4. `Desconectar()` / `Dispose()` - Fecha porta e libera recursos


---

---

## 14. Endpoints da API REST

### Visão Geral

O Print Server expõe uma API HTTP para receber requisições de impressão. Todos os endpoints retornam **JSON** como resposta.

**Base URL:** `http://{PrintServerIp}:{PrintServerPort}`
- Exemplo local: `http://localhost:5000`
- Exemplo remoto: `http://192.168.1.100:5000`

---

### 14.1 POST /imprimir
**Impressão de Cupom Simples (Um Produto)**

Imprime um **único produto** na impressora.

**HTTP Method:** `POST`

**Query Parameters:**
| Parâmetro | Tipo | Obrigatório | Descrição |
|-----------|------|-------------|-----------|
| `produto` | string | ✓ SIM | Nome/descrição do produto a imprimir (ex: "REFRIGERANTE 2L") |
| `numeroCaixa` | int | ✗ NÃO | Número do caixa/PDV (padrão: 0) |
| `descricaoCaixa` | string | ✗ NÃO | Descrição do caixa (padrão: "") |

**Resposta (200 OK):**
```json
{
  "sucesso": true,
  "mensagem": "Cupom adicionado à fila",
  "jobId": "a1b2c3d4-e5f6-47g8-h9i0-j1k2l3m4n5o6"
}
```

**Resposta (400 Bad Request):**
```json
{
  "erro": "Parâmetro 'produto' é obrigatório. Use: /imprimir?produto=NomeProduto"
}
```

**Exemplos de Uso:**

**1. Postman - Chamada Simples:**
```
POST http://localhost:5000/imprimir?produto=CERVEJA%20PREMIUM
```
- Ou usar a aba "Params" no Postman:
  - Key: `produto`
  - Value: `CERVEJA PREMIUM`

**2. Postman - Com Dados do Caixa:**
```
POST http://localhost:5000/imprimir?produto=REFRIGERANTE%202L&numeroCaixa=1&descricaoCaixa=PDV%20LOJA%20A
```

**3. cURL:**
```bash
curl -X POST "http://localhost:5000/imprimir?produto=AGUA%20MINERAL"
```

**O que é impresso no papel:**
```
─────────────────────
CERVEJA PREMIUM
Caixa 1 - PDV LOJA A
30/04/2026 14:30:45
═════════════════════  (CUT)
```

---

### 14.2 POST /imprimir-venda
**Impressão de Venda Completa (Múltiplos Itens)**

Imprime **todos os itens de uma venda** em cupons separados.

**HTTP Method:** `POST`

**Content-Type:** `application/json`

**Request Body:**
```json
{
  "vendaId": 42,
  "itens": [
    "REFRIGERANTE 2L",
    "CERVEJA 350ML",
    "AGUA MINERAL 1.5L"
  ],
  "numeroCaixa": 1,
  "descricaoCaixa": "PDV LOJA A"
}
```

**Resposta (200 OK):**
```json
{
  "sucesso": true,
  "mensagem": "Venda adicionada à fila de impressão",
  "vendaId": 42,
  "itensAdicionados": 3,
  "tempoEstimado": "~3.9 segundos"
}
```

**Resposta (400 Bad Request):**
```json
{
  "erro": "Campo 'itens' deve conter pelo menos 1 item"
}
```

**Exemplos de Uso:**

**1. Postman:**
- Method: `POST`
- URL: `http://localhost:5000/imprimir-venda`
- Headers: `Content-Type: application/json`
- Body (raw/JSON):
```json
{
  "vendaId": 42,
  "itens": [
    "REFRIGERANTE 2L",
    "CERVEJA 350ML",
    "AGUA MINERAL 1.5L"
  ],
  "numeroCaixa": 1,
  "descricaoCaixa": "PDV LOJA A"
}
```

**2. cURL:**
```bash
curl -X POST http://localhost:5000/imprimir-venda \
  -H "Content-Type: application/json" \
  -d '{
    "vendaId": 42,
    "itens": ["REFRIGERANTE 2L", "CERVEJA 350ML"],
    "numeroCaixa": 1,
    "descricaoCaixa": "PDV LOJA A"
  }'
```

**O que é impresso no papel:**
```
─────────────────────
REFRIGERANTE 2L
Caixa 1 - PDV LOJA A
30/04/2026 14:30:45
═════════════════════  (CUT)

─────────────────────
CERVEJA 350ML
Caixa 1 - PDV LOJA A
30/04/2026 14:30:46
═════════════════════  (CUT)

─────────────────────
AGUA MINERAL 1.5L
Caixa 1 - PDV LOJA A
30/04/2026 14:30:47
═════════════════════  (CUT)
```

---

### 14.3 GET /status
**Verificar Status da Impressora e Fila**

Retorna informações sobre o status atual da impressora e da fila de impressão.

**HTTP Method:** `GET`

**Query Parameters:** Nenhum

**Resposta (200 OK):**
```json
{
  "impressora_conectada": true,
  "impressora_respondendo": true,
  "porta_com": "COM2",
  "baud_rate": 9600,
  "fila_size": 2,
  "fila": [
    {
      "jobId": "a1b2c3d4-e5f6-47g8-h9i0-j1k2l3m4n5o6",
      "productName": "CERVEJA 350ML",
      "status": "Processando",
      "adicionadoEm": "2026-04-30T14:30:45"
    },
    {
      "jobId": "b2c3d4e5-f6g7-48h9-i0j1-k2l3m4n5o6p7",
      "productName": "AGUA MINERAL 1.5L",
      "status": "Na Fila",
      "adicionadoEm": "2026-04-30T14:30:46"
    }
  ],
  "totalProcessado": 127,
  "erros": 0
}
```

**Exemplos de Uso:**

**1. Postman:**
- Method: `GET`
- URL: `http://localhost:5000/status`
- Clique em "Send"

**2. cURL:**
```bash
curl -X GET http://localhost:5000/status
```

**3. JavaScript/Fetch:**
```javascript
fetch('http://localhost:5000/status')
  .then(res => res.json())
  .then(data => {
    console.log('Fila:', data.fila_size);
    console.log('Impressora OK:', data.impressora_conectada);
  })
  .catch(err => console.error(err));
```

**4. Browser (direto):**
```
http://localhost:5000/status
```

---

### 14.4 POST /limpar
**Limpar Fila de Impressão**

Remove **todos os trabalhos** da fila sem imprimir.

**HTTP Method:** `POST`

**Query Parameters:** Nenhum

**Resposta (200 OK):**
```json
{
  "sucesso": true,
  "mensagem": "Fila limpa",
  "itensRemovidos": 5
}
```

**Exemplos de Uso:**

**1. Postman:**
- Method: `POST`
- URL: `http://localhost:5000/limpar`
- Body: vazio ou nenhum
- Clique em "Send"

**2. cURL:**
```bash
curl -X POST http://localhost:5000/limpar
```

**3. JavaScript/Fetch:**
```javascript
fetch('http://localhost:5000/limpar', {
  method: 'POST'
})
.then(res => res.json())
.then(data => console.log('Fila limpa:', data.mensagem))
.catch(err => console.error(err));
```

---

### 14.5 Códigos HTTP de Resposta

| Código | Significado | Exemplo |
|--------|-------------|---------|
| **200** | OK - Requisição bem-sucedida | Cupom adicionado à fila |
| **400** | Bad Request - Parâmetro obrigatório faltando | Faltou parâmetro 'produto' |
| **404** | Not Found - Endpoint não existe | POST /invalido |
| **500** | Internal Server Error - Erro no servidor | Impressora desconectada inesperadamente |

---

### 14.6 Teste Completo no Postman

**Passo a Passo:**

1. **Verificar Status:**
   - GET `http://localhost:5000/status`
   - Verificar se `impressora_conectada: true`

2. **Imprimir Cupom Simples:**
   - POST `http://localhost:5000/imprimir?produto=TESTE`
   - Verificar se papel foi impresso

3. **Imprimir Venda Completa:**
   - POST `http://localhost:5000/imprimir-venda`
   - Body JSON com 3 itens
   - Verificar se todos os cupons foram impressos

4. **Verificar Fila:**
   - GET `http://localhost:5000/status`
   - Verificar se `fila_size: 0`

5. **Limpar Fila (se houver pendências):**
   - POST `http://localhost:5000/limpar`
   - Verificar se retornou `sucesso: true`

---

### 14.7 Configuração de Timeout

Se a requisição demorar muito (impressora lenta), configure timeout no Postman:
1. Abra Postman
2. File → Settings → General
3. Request timeout: `30000` (30 segundos)
4. Salve

---

## 15. Referências Técnicas

### Documentação de Referência

**ESC/POS Commands:**
- https://reference.epson-pos.com/ (Oficial)
- Command Set: https://www.epson.com/cgi-bin/Store/support/...

**EPSON TM-20 Specifications:**
- Product Manual: https://www.epson.com.br/products/printers
- Driver Download: https://www.epson.com.br/Support/Drivers

**Windows Serial Port Programming:**
- Microsoft Docs: https://docs.microsoft.com/dotnet/api/system.io.ports.serialport
- Example Code: https://github.com/dotnet/samples/...

**Character Encodings:**
- Windows-1252: https://en.wikipedia.org/wiki/Windows-1252
- Code Pages: https://docs.microsoft.com/globalization/encodings/encoding-overview

### Comandos ESC/POS Mais Usados

| Função | Comando | Descrição |
|--------|---------|-----------|
| Reset | ESC @ | Reseta todas as configurações |
| Centralizar | ESC a 1 | Centraliza texto seguinte |
| Negrito | ESC E 1 | Ativa negrito |
| Tamanho | ESC ! 0x21 | Define tamanho (1=normal, 2=2x, etc) |
| Corte | GS V B 00 | Corta papel completamente |
| Encoding | ESC t 0x10 | Seleciona Windows-1252 |
| Avanço | ESC d n | Avança n linhas |
| Linha | ESC - 1 | Sublinha (on) |

### Versões Críticas

- EPSON TM-20: Qualquer revisão (M249A-STABLE)
- Windows: XP SP3 ou superior (COM suportado)
- .NET Framework: 4.0 ou superior
- Driver EPSON: v3.0 ou superior (recomendado)

---

**Última Atualização:** 30 de Abril de 2026
**Status:** Production Ready - Otimizado e Testado
**Performance:** 1.3s por cupom (benchmarked)
