# Instalação da Impressora Epson TM-T20

## Guia de Instalação

### 1. Instalação dos Drivers
- Instalar o setup conforme o sistema operacional
- Instalar o utilitário TM-T20 (02-Utilitários\Utility TM-T20 Versão 1.50.exe)

### 2. Configuração da Impressora

#### Via EPSON TM-T20 Utility
Executar o EPSON TM-T20 Utility.exe e seguir os passos abaixo:

1. **Conectar a impressora**: Conectar via USB no computador
2. **Adicionar Porta**: 
   - Clicar em "Adic. Porta"
   - Selecionar USB → Selecionar a Porta
   - Clicar em "Teste de comunicação" → OK
3. **Conectar à Impressora**:
   - Exibirá o modelo TM-T20
   - Dar um duplo clique na impressora
   - Irá conectar com a impressora e abrirá a janela "TM-T20 Utility"
4. **Configurar Interface de Comunicação**:
   - Menu "Comunicação I/F" → Selecionar a opção "Vender Class"
   - Clicar em "Definir"

### 3. Instalação da Porta Virtual
- Instalar o programa TMVirtualPortDriver870c.exe (03-Porta Virtual\TMVirtualPortDriver870c.exe)

#### Executar o TMVirtualPortDriver870c.exe
1. Selecionar uma porta (ex: COM2)
2. Clicar em "Assing Port"
3. Selecionar opção USB → Selecionar a impressora → OK
4. Clicar em "Exit"

---

## Configuração da Impressora na Aplicação

A configuração da impressora é feita através do arquivo **App.config** da aplicação GestorEvento. Não é necessário acessar menus da aplicação.

### Modo LOCAL (Impressora conectada via USB)

Editar `App.config` no diretório da aplicação:

```xml
<configuration>
  <appSettings>
    <!-- Modo de impressão: LOCAL ou REMOTE -->
    <add key="PrintMode" value="Local" />
    
    <!-- Configuração LOCAL -->
    <add key="PrinterPortName" value="COM2" />
    <add key="PrinterBaudRate" value="9600" />
  </appSettings>
  
  <connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=localhost;Database=gestor_evento;Uid=root;Pwd=;" />
  </connectionStrings>
</configuration>
```

**Campos:**
- `PrintMode`: Definir como `Local` para impressora física conectada via USB
- `PrinterPortName`: Porta COM da impressora (ex: COM2, COM3)
- `PrinterBaudRate`: Velocidade serial (9600 para EPSON TM-20)

**Como descobrir a porta COM:**
1. Conectar impressora via USB
2. Abrir "Gerenciador de Dispositivos" (Win+R → `devmgmt.msc`)
3. Expandir "Portas (COM e LPT)"
4. Procurar pela impressora EPSON (ex: "EPSON TM-20 (COM2)")
5. Usar esse número no `PrinterPortName`

---

### Modo REMOTE (Impressora via PrintServer)

Quando múltiplos PDVs compartilham uma única impressora através de um serviço centralizado:

```xml
<configuration>
  <appSettings>
    <!-- Modo de impressão: LOCAL ou REMOTE -->
    <add key="PrintMode" value="Remote" />
    
    <!-- Configuração REMOTE -->
    <add key="PrintServerIp" value="192.168.1.100" />
    <add key="PrintServerPort" value="5000" />
  </appSettings>
  
  <connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=localhost;Database=gestor_evento;Uid=root;Pwd=;" />
  </connectionStrings>
</configuration>
```

**Campos:**
- `PrintMode`: Definir como `Remote` para usar PrintServer remoto
- `PrintServerIp`: IP ou hostname do servidor PrintServer (ex: 192.168.1.100)
- `PrintServerPort`: Porta do serviço PrintServer (padrão: 5000)

**Quando usar REMOTE:**
- Múltiplos PDVs compartilhando uma única impressora
- Impressora centralizada em servidor dedicado
- Melhor disponibilidade e fila centralizada

---

## Configuração para Modo REMOTE

Se usar modo REMOTE, o **PrintServer** deve estar rodando na máquina onde a impressora física está conectada. Siga os passos abaixo:

### Passo 1: Liberar Porta no Firewall

Executar o comando abaixo no **PowerShell ou CMD** (como administrador):

```powershell
netsh advfirewall firewall add rule name="Print Server Port 5000" dir=in action=allow protocol=tcp localport=5000
```

Este comando:
- Adiciona uma regra no firewall do Windows
- Permite conexões de entrada na porta 5000
- Nome da regra: "Print Server Port 5000" (para identificação no Firewall)

**Verificar se funcionou:**
Opção 1: 
  - Exibe "OK" no cmd.

Opção 2:
  - Abrir "Windows Defender Firewall com Segurança Avançada"
  - Ir em "Regras de Entrada"
  - Procurar por "Print Server Port 5000"
  - Status deve ser "Habilitado"

---

### Passo 2: Executar o PrintServer como Administrador

Abrir **PowerShell ou CMD como administrador** e executar:

```powershell
GestorEvento\PrintServer\bin\Debug\PrintServer.exe
```

**Importante:**
- ⚠️ **SEMPRE executar como ADMINISTRADOR** (necessário para acesso à porta serial COM)
- Deixar a janela aberta enquanto estiver usando o sistema
- Será exibido "Server running on http://0.0.0.0:5000" se tudo estiver OK

**Passo a Passo no Windows:**
1. Pressionar `Win+R`
2. Digitar `powershell` (não feche, apenas digite)
3. Pressionar `Ctrl+Shift+Enter` (executa como administrador)
4. Aceitar a solicitação de UAC (Controle de Conta de Usuário)
5. Colar o comando acima
6. Pressionar `Enter`

---

### Passo 3: Testar Conectividade

**Na máquina onde está rodando a aplicação GestorEvento:**

1. Abrir PowerShell ou CMD
2. Executar:
```powershell
Test-NetConnection -ComputerName 192.168.1.100 -Port 5000
```

Substituir `192.168.1.100` pelo IP da máquina do PrintServer.

**Resposta esperada:**
```
ComputerName     : 192.168.1.100
RemoteAddress    : 192.168.1.100
RemotePort       : 5000
InterfaceAlias   : Ethernet
SourceAddress    : 192.168.1.X
TcpTestSucceeded : True
```

Se `TcpTestSucceeded` for `True` → Conectividade OK ✓

---

### Passo 4: Descobrir o IP da Máquina Centralizadora

A máquina que vai executar o PrintServer precisa ter um IP fixo ou identificável na rede. Para descobrir:

**Opção 1: Via PowerShell (Recomendado)**

Abrir PowerShell na máquina do PrintServer e executar:

```powershell
ipconfig
```

Procurar por `IPv4 Address` na seção `Ethernet` ou `Wireless`:

```
Ethernet adapter Ethernet:
   ...
   IPv4 Address. . . . . . . . . . . : 192.168.1.100
   Subnet Mask . . . . . . . . . . . : 255.255.255.0
   ...
```

**Anotar o IP:** `192.168.1.100` (este é o IP da máquina centralizadora)

---

### Passo 5: Configurar App.config nas Máquinas Cliente

Agora que conhece o IP da máquina centralizadora (PrintServer), configure o `App.config` de cada máquina cliente que vai usar o GestorEvento:

**Editar `App.config`:**

```xml
<configuration>
  <appSettings>
    <!-- Modo de impressão: LOCAL ou REMOTE -->
    <add key="PrintMode" value="Remote" />
    
    <!-- Configuração REMOTE - Máquina Centralizadora -->
    <add key="PrintServerIp" value="192.168.1.100" />
    <add key="PrintServerPort" value="5000" />
  </appSettings>
  
  <connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=localhost;Database=gestor_evento;Uid=root;Pwd=;" />
  </connectionStrings>
</configuration>
```

**Substituir `192.168.1.100` pelo IP real da máquina centralizadora** que você anotou no Passo 4.

**Exemplo com IP diferente:**

Se o IP da máquina centralizadora for `192.168.1.50`:

```xml
<add key="PrintServerIp" value="192.168.1.50" />
```

⚠️ **Importante:**
- Este `App.config` deve ser atualizado em **cada máquina cliente** que vai usar GestorEvento
- Certifique-se de que o IP está correto e acessível na mesma rede
- Sempre use `5000` como porta (ou a porta configurada do PrintServer)

---

---

### Passo 6: Configurar App.config do PrintServer (Máquina Centralizadora)

Na máquina que vai executar o PrintServer, também é necessário configurar seu próprio `App.config` com o IP e porta do serviço.

**Localização:** `\GestorEvento\PrintServer\App.config`

**Editar o arquivo com as informações da máquina centralizadora:**

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
    <appSettings>
        <!-- Configurações HTTP do Print Server API - CONFIGURAR COM O IP E PORTA DA MAQUINA QUE IRÁ EXECUTAR O SERVIÇO -->
        <add key="PrintServerIp" value="192.168.1.100" />
        <add key="PrintServerPort" value="5000" />
        
        <!-- Configurações para Impressora Serial -->
        <add key="PrinterPortName" value="COM2" />
        <add key="PrinterBaudRate" value="9600" />
    </appSettings>
    <startup> 
        <supportedRuntime version="v4.0" sku=".NETFramework,Version=v4.7.2" />
    </startup>
</configuration>
```

**Campos importantes:**

- `PrintServerIp`: IP da máquina centralizadora (mesmo IP que você descobriu no Passo 4)
  - Exemplo: `192.168.1.100`
  - Pode usar `localhost` ou `127.0.0.1` se testar localmente

- `PrintServerPort`: Porta do serviço HTTP (padrão: 5000)
  - Use sempre `5000` a menos que configure diferente

- `PrinterPortName`: Porta COM da impressora física (ex: COM2, COM3)
  - **Esta é a porta COM onde a impressora física está conectada**
  - Descobrir conforme seção "Como descobrir a porta COM" acima

- `PrinterBaudRate`: Velocidade serial (9600 para EPSON TM-20)
  - Não alterar para EPSON TM-20

**Exemplo Prático:**

Se a máquina centralizadora tem IP `192.168.1.100` e a impressora está em `COM3`:

```xml
<add key="PrintServerIp" value="192.168.1.100" />
<add key="PrintServerPort" value="5000" />
<add key="PrinterPortName" value="COM3" />
<add key="PrinterBaudRate" value="9600" />
```

⚠️ **Importante:**
- **PrintServerIp:** Deve ser o mesmo IP que você colocou nas máquinas cliente (Passo 5)
- **PrinterPortName:** Deve ser a porta COM onde a impressora FÍSICA está conectada
- Estes são valores interdependentes - certifique-se de consistência

---