# Print Server - Epson TM-T20

Um serviço HTTP que gerencia impressões em fila para a impressora térmica Epson TM-T20 via API REST, permitindo múltiplas máquinas na mesma rede imprimirem simultaneamente.

## 📋 O Problema

Quando múltiplas máquinas em uma rede tentam acessar a mesma impressora serial via COM (Epson TM-20), há riscos de:
- ❌ Cupons misturados
- ❌ Conflitos de porta
- ❌ Falhas de impressão

## ✅ A Solução

O **Print Server** centraliza o acesso à impressora em uma única máquina, expondo uma **API REST HTTP** que outras máquinas podem chamar.

```
Máquina A (COM2 conectada)    Máquina B           Máquina C
        │                          │                   │
        │                          │                   │
        └──────────────────────────┴───────────────────┘
                        ↓
                  Print Server API
                  http://192.168.1.50:5000
                        ↓
                    [Fila de Impressão]
                        ↓
                  Epson TM-T20 (COM2)
```

## 🚀 Como Usar

### 1. Iniciar o Print Server

Na máquina com a impressora conectada:

```bash
PrintServer.exe
```

Ou especificar porta customizada:

```bash
PrintServer.exe COM3 6000
```

**Saída esperada:**
```
═══════════════════════════════════════════════════════════════
         PRINT SERVER - Epson TM-T20 Network Print Service
═══════════════════════════════════════════════════════════════

[CONFIG] Porta Serial: COM2
[CONFIG] Taxa Baud: 9600
[CONFIG] Porta HTTP: 5000

✓ API iniciada em http://localhost:5000/
✓ Print Server está online e aguardando requisições
```

### 2. Chamar de Outra Máquina na Rede

A partir de qualquer máquina na rede:

#### **Opção A: Via Postman**

1. Abrir Postman
2. **POST** para `http://192.168.1.50:5000/imprimir?produto=Cerveja%20Premium`
3. Cupom será adicionado à fila

#### **Opção B: Via C# (seu GestorEvento)**

```csharp
using (HttpClient client = new HttpClient())
{
    try
    {
        string ipPrintServer = "192.168.1.50"; // IP da máquina com impressora
        string produtoNome = "Cerveja Premium";
        
        var response = await client.PostAsync(
            $"http://{ipPrintServer}:5000/imprimir?produto={Uri.EscapeDataString(produtoNome)}",
            null
        );
        
        if (response.IsSuccessStatusCode)
        {
            string result = await response.Content.ReadAsStringAsync();
            MessageBox.Show("Cupom enviado à fila com sucesso!");
        }
    }
    catch (Exception ex)
    {
        MessageBox.Show($"Erro ao imprimir: {ex.Message}");
    }
}
```

#### **Opção C: Via PowerShell**

```powershell
Invoke-WebRequest -Uri "http://192.168.1.50:5000/imprimir?produto=Cerveja" -Method POST
```

## 📡 API REST Endpoints

### 1. **POST /imprimir** - Adiciona cupom à fila

**Requisição:**
```
POST http://localhost:5000/imprimir?produto=Cerveja%20Premium
```

**Resposta (Sucesso):**
```json
{
  "success": true,
  "message": "Cupom adicionado à fila",
  "jobId": "a1b2c3d4-e5f6-g7h8-i9j0-k1l2m3n4o5p6"
}
```

**Resposta (Erro):**
```json
{
  "erro": "Parâmetro 'produto' é obrigatório"
}
```

---

### 2. **GET /status** - Verifica status da fila

**Requisição:**
```
GET http://localhost:5000/status
```

**Resposta:**
```json
{
  "fila_size": 2,
  "is_processing": true,
  "pending_jobs": [
    {
      "jobId": "a1b2c3d4...",
      "produto": "Cerveja Premium",
      "tentativas": 0
    },
    {
      "jobId": "x9y8z7w6...",
      "produto": "Água Gelada",
      "tentativas": 0
    }
  ]
}
```

---

### 3. **POST /limpar** - Limpa a fila

**Requisição:**
```
POST http://localhost:5000/limpar
```

**Resposta:**
```json
{
  "sucesso": true,
  "mensagem": "Fila limpa"
}
```

---

### 4. **GET /** - Informações da API

**Requisição:**
```
GET http://localhost:5000/
```

**Resposta:**
```json
{
  "servico": "Print Server Epson TM-T20",
  "versao": "1.0",
  "endpoints": {
    "imprimir": {
      "metodo": "POST",
      "url": "/imprimir?produto=NomeProduto",
      "descricao": "Adiciona cupom à fila"
    },
    "status": {
      "metodo": "GET",
      "url": "/status",
      "descricao": "Status atual da fila"
    },
    "limpar": {
      "metodo": "POST",
      "url": "/limpar",
      "descricao": "Limpa a fila de impressão"
    }
  }
}
```

## ⚙️ Configuração

### Encontrar IP da Máquina com Impressora

No Windows (máquina com a impressora):

```powershell
ipconfig
```

Procure por "IPv4 Address", exemplo: `192.168.1.50`

### Encontrar Porta Serial da Impressora

```powershell
Get-WmiObject Win32_SerialPort | Select-Object Name, Description
```

Ou abra Gerenciador de Dispositivos e procure por "Portas (COM e LPT)"

### Testar Conexão

```bash
ping 192.168.1.50
```

## 🔄 Fluxo de Impressão

```
1. Requisição HTTP recebida
   ↓
2. Validação (produto é obrigatório?)
   ↓
3. Adicionar à fila
   ↓
4. PrintQueueManager processa (um por vez)
   ↓
5. Conecta à SerialPort (COM2)
   ↓
6. Envia comandos ESC/POS à impressora
   ↓
7. Aguarda impressão terminar
   ↓
8. Próximo cupom na fila (ou aguarda novo)
```

## 🔧 Retry Automático

Se a impressão falhar:

- **Tentativa 1**: Imediato
- **Tentativa 2**: Aguarda 2 segundos + tenta novamente
- **Tentativa 3**: Aguarda 2 segundos + tenta novamente
- **Falha final**: Registra erro e move para próximo

Máximo: **3 tentativas** por cupom

## 📝 Logs (Console)

Exemplo de saída:

```
[REQUEST] POST /imprimir?produto=Cerveja
  Produto: Cerveja
✓ Trabalho adicionado à fila: [a1b2c3d4...] Cerveja - Tentativas: 0
  Fila atual: 1 trabalho(s)

[IMPRESSÃO] Iniciando: Cerveja (Tentativa 1/3)
✓ [SUCESSO] Cupom impresso: Cerveja
✓✓✓ Trabalho finalizado com sucesso: Cerveja
```

## ⚠️ Troubleshooting

### "Erro ao conectar na porta COM2"

**Causas:**
- Impressora desligada
- Cabo USB desconectado
- Porta serial errada

**Solução:**
```powershell
# Verificar portas disponíveis
Get-WmiObject Win32_SerialPort | Select-Object Name
```

### "Acesso negado" ao iniciar API

**Causa:** Outra aplicação usando a porta 5000

**Solução:**
```powershell
# Ver o que está usando porta 5000
netstat -ano | findstr :5000

# Matar processo se necessário (cuidado!)
taskkill /PID <PID> /F
```

### Múltiplas máquinas não conseguem conectar

**Causas:**
- IP errado
- Firewall bloqueando porta 5000
- Máquinas em redes diferentes

**Solução:**
```powershell
# Habilitar porta 5000 no firewall (Windows)
netsh advfirewall firewall add rule `
  name="Print Server 5000" `
  dir=in action=allow protocol=tcp localport=5000
```

## 📌 Próximas Melhorias

- [ ] Converter para Windows Service (inicia automaticamente)
- [ ] Dashboard web para monitorar fila
- [ ] Persistência de logs
- [ ] Autenticação opcional
- [ ] Suporte para múltiplas impressoras
- [ ] Interface gráfica

## 📄 Licença

Interno - Projeto GestorEvento

---

**Desenvolvido em:** April 29, 2026  
**Framework:** .NET Framework 4.7.2  
**Impressora:** Epson TM-T20
