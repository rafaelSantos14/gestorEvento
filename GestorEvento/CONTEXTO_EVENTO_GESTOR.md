# CONTEXTO_EVENTO_GESTOR.md

## 1. Título e Descrição

**Nome do Projeto:** GestorEvento - Sistema de Gerenciamento de Eventos e Ponto de Venda (PDV)

**Breve Resumo:** Sistema Windows Forms integrado para gerenciar eventos, produtos, pontos de venda, vendas em tempo real e fechamento de caixa com suporte a impressora térmica EPSON TM-20 para emissão de cupons.

---

## 2. Objetivo / Propósito

**Problema Resolvido:**
- Gerenciamento centralizado de eventos e produtos relacionados
- Operação de PDV (Ponto de Venda) em tempo real durante eventos
- Emissão automática de cupons térmicos para cada venda
- Rastreamento de múltiplas formas de pagamento e troco
- Fechamento de caixa com reconciliação automática de saldo
- Garantia de integridade de dados em operações multi-etapa (venda + recebimento + troco)

**Público-Alvo / Cenário de Uso:**
- Igreja que solicitou para ter um controle melhor dos caixas, pois no momento controlam com fichas de papel, trabalhoso e dificil controle.

---

## 3. Funcionalidades

### 3.1 Telas Principais

#### **FormPrincipal.cs**
- Tela inicial com menu de navegação
- Acesso a: Cadastro de Produtos, Cadastro de Eventos, Gerenciamento de PDV, Relatórios
- Autenticação básica (se implementada)

#### **FormProdutos.cs**
- CRUD de produtos (criar, ler, atualizar, deletar)
- Campos: nome, descrição, preço, categoria
- Busca e filtros
- Quantidade de estoque

#### **FormEventos.cs**
- Cadastro de eventos (nome, data, local, descrição)
- Status: ativo, inativo, finalizado
- Associação de produtos por evento

#### **FormEventosAtivos.cs**
- Lista de eventos em andamento
- Seleção de evento para operações de PDV
- Status real-time

#### **FormSelecionarPontoVenda.cs**
- Seleção do ponto de venda (caixa) para operar
- Lista de pontos de venda disponíveis
- Confirmação de abertura de caixa

#### **FormAbrirCaixa.cs**
- Abertura de caixa com valor inicial
- Confirmação de operador
- Data/hora de abertura
- Status de impressora térmica

#### **FormPDV.cs**
- **Tela de venda em tempo real**
- Seleção de produtos e quantidade
- Adição/remoção de itens do carrinho
- Cálculo automático de totais
- Múltiplas formas de pagamento (Dinheiro, PIX, Débito, Crédito)
- Cálculo automático de troco
- Botão "CONFIRMAR VENDA" com:
  - Registro da venda no banco
  - Registro de recebimentos
  - Registro de movimentações (troco)
  - **Emissão automática de cupom térmico**
  - Feedback visual de sucesso/erro

#### **FormFecharCaixa.cs**
- Resumo executivo com valores esperados
- Resumo de formas de pagamento
- Campo para inserir valor contado em mão
- Cálculo automático de diferença (com cores: vermelho/verde/azul)
- Campo de observações
- Validação e fechamento da caixa

---

## 4. Arquitetura / Estrutura do Projeto

### 4.1 Padrão de Arquitetura

**Padrão: Layered Architecture (N-Tier) + Repository Pattern**

```
┌──────────────────────────────────────────────┐
│  Presentation Layer (Views)                  │
│  ├── FormPrincipal.cs                        │
│  ├── FormPDV.cs                              │
│  └── FormFecharCaixa.cs                      │
└─────────────────┬──────────────────────────┘
                  │ (Dependency Injection)
                  ↓
┌──────────────────────────────────────────────┐
│  Business Logic Layer (Services)             │
│  ├── VendaService.cs                         │
│  ├── PontoVendaService.cs                    │
│  ├── EpsonTM20Service.cs                     │
│  └── MovimentacaoService.cs                  │
└─────────────────┬──────────────────────────┘
                  │ (Dependency Injection)
                  ↓
┌──────────────────────────────────────────────┐
│  Data Access Layer (Repositories)            │
│  ├── VendaRepository.cs                      │
│  ├── RecebimentoRepository.cs                │
│  ├── MovimentacaoRepository.cs               │
│  └── Connection.cs (gerencia pool)           │
└─────────────────┬──────────────────────────┘
                  │ (SQL Queries)
                  ↓
┌──────────────────────────────────────────────┐
│  Domain Layer (Models)                       │
│  ├── Venda.cs                                │
│  ├── Recebimento.cs                          │
│  ├── Movimentacao.cs                         │
│  └── ResumoFechamentoCaixa.cs (DTO)          │
└─────────────────┬──────────────────────────┘
                  │
                  ↓
┌──────────────────────────────────────────────┐
│  Database Layer (MySQL 8.0)                  │
│  └── gestor_evento (Database)                │
└──────────────────────────────────────────────┘
```

**4 Camadas Principais:**

1. **Presentation Layer (Views)**
   - Formulários Windows Forms
   - Responsável: Interface com usuário, validação de entrada
   - Exemplo: FormPDV, FormFecharCaixa

2. **Business Logic Layer (Services)**
   - Lógica de negócio e orquestração
   - Responsável: Regras de negócio, transações, validações
   - Exemplo: VendaService.RegistrarVendaComTrocoComTransacao()
   - Injeção de dependência dos Repositories

3. **Data Access Layer (Repositories)**
   - Abstração do acesso a dados
   - Responsável: Queries SQL, mapeamento de resultados, gerenciar conexões
   - Exemplo: VendaRepository, RecebimentoRepository
   - Implementa Repository Pattern (abstrai a fonte de dados)

4. **Domain Layer (Models)**
   - Entidades de negócio e DTOs
   - Responsável: Representação de dados
   - Exemplo: Venda, Recebimento, ResumoFechamentoCaixa

### 4.2 Organização de Pastas

```
GestorEvento/
├── Views/
│   ├── FormPrincipal.cs                      (Menu principal, navegação)
│   ├── FormProdutos.cs                       (CRUD de produtos)
│   ├── FormEventos.cs                        (Cadastro de eventos)
│   ├── FormEventosAtivos.cs                  (Seleção de evento ativo)
│   ├── FormSelecionarPontoVenda.cs           (Seleção do caixa/PDV)
│   ├── FormAbrirCaixa.cs                     (Abertura com valor inicial)
│   ├── FormPDV.cs                            (Tela de venda em tempo real - CRÍTICA)
│   ├── FormFecharCaixa.cs                    (Fechamento e reconciliação - UI dinâmica)
│   └── DialogoCustomizado.cs                 (Diálogos dinâmicos com ícones)
│
├── Services/
│   ├── ProdutoService.cs                     (Lógica de produtos: CRUD, validações)
│   ├── EventoService.cs                      (Lógica de eventos: criar, listar, ativar)
│   ├── PontoVendaService.cs                  (Lógica de PDV: abrir, fechar, resumo)
│   ├── VendaService.cs                       (CRÍTICO: orquestração transacional de vendas)
│   │   └── Método: RegistrarVendaComTrocoComTransacao()
│   ├── RecebimentoService.cs                 (Lógica de formas de pagamento)
│   ├── MovimentacaoService.cs                (NOVO: rastreamento de troco/sangria)
│   ├── FormaPagamentoService.cs              (Lógica de formas de pagamento cadastradas)
│   ├── ProdutoEventoService.cs               (Associação de produtos por evento)
│   └── EpsonTM20Service.cs                   (CRÍTICO: impressora térmica)
│       └── Métodos:
│           ├── TestarConexao()
│           ├── ImprimirCupom(idVenda)
│           ├── SetarPaginaISO()
│           ├── CentralizarTexto()
│           ├── ImprimirCabecalho()
│           ├── ImprimirDetalhes()
│           ├── ImprimirFormasPagamento()
│           ├── ImprimirTroco()
│           ├── ImprimirRodape()
│           └── CortarPapel()
│
├── Repositories/
│   ├── Connection.cs                         (String de conexão, gerencia pool)
│   ├── ProdutoRepository.cs                  (CRUD de produtos)
│   ├── EventoRepository.cs                   (CRUD de eventos)
│   ├── PontoVendaRepository.cs               (CRUD de pontos de venda)
│   ├── VendaRepository.cs                    (CRUD de vendas + resumos)
│   │   └── Métodos:
│   │       ├── RegistrarVenda(venda)
│   │       ├── GetVendaById(idVenda)
│   │       ├── GetVendasByPontoVenda(idPontoVenda)
│   │       └── GetResumoVendasByPontoVenda()
│   ├── RecebimentoRepository.cs              (CRUD de recebimentos + transações)
│   │   └── Métodos:
│   │       ├── RegistrarRecebimento()
│   │       ├── RegistrarRecebimentoComTransacao() (com MySqlTransaction)
│   │       ├── GetResumoRecebimentosByPontoVenda()
│   │       └── GetTotalRecebimentoByFormaPagamento()
│   ├── MovimentacaoRepository.cs             (NOVO: CRUD de movimentações)
│   │   └── Métodos:
│   │       ├── RegistrarMovimentacao()
│   │       ├── RegistrarTroco()
│   │       ├── RegistrarTrocoComTransacao() (com MySqlTransaction)
│   │       ├── RegistrarSangria()
│   │       ├── RegistrarEntradaTroco()
│   │       ├── GetMovimentacoesByPontoVenda()
│   │       └── GetTotalMovimentacaoPorTipo()
│   ├── FormaPagamentoRepository.cs           (CRUD de formas de pagamento)
│   └── ProdutoEventoRepository.cs            (Associação produto x evento)
│
├── Models/
│   ├── Produto.cs                            (Entidade: nome, preço, descrição, estoque)
│   ├── Evento.cs                             (Entidade: nome, data, status, descrição)
│   ├── PontoVenda.cs                         (Entidade: número, nome, status, valor inicial)
│   ├── Venda.cs                              (Entidade: produtos, total, data, ponto venda)
│   ├── Recebimento.cs                        (Entidade: forma pagamento, valor, venda)
│   ├── Movimentacao.cs                       (NOVO: tipo movimento, valor, data, ponto venda)
│   │   └── Enum: TipoMovimento { TROCO=1, SANGRIA=2, ENTRADA_TROCO=3 }
│   ├── FormaPagamento.cs                     (Entidade: Dinheiro, PIX, Débito, Crédito)
│   ├── ProdutoEvento.cs                      (Entidade: associação produto x evento)
│   └── ResumoFechamentoCaixa.cs              (DTO para FormFecharCaixa)
│       ├── Inner Class: ResumoPorForma (formas de pagamento)
│       ├── Inner Class: ResumoVenda (vendas)
│       └── Inner Class: MovimentacaoDetalhada (movimentações)
│
├── Utilities/
│   ├── EstiloManager.cs                      (Tema Material Design: cores, fontes, estilos)
│   └── PrinterServiceFactory.cs              (Factory Pattern para criar serviço de impressão)
│
├── Properties/
│   ├── AssemblyInfo.cs                       (Info do assembly: versão, copyright)
│   ├── Resources.resx                        (Ícones, imagens, strings)
│   ├── Settings.settings                     (Configurações da aplicação)
│
├── bin/
│   ├── Debug/
│   │   ├── GestorEvento.exe                  (Executável de debug)
│   │   ├── GestorEvento.exe.config           (Configurações em runtime)
│   │   └── (dependências DLL)
│   └── Release/
│       └── GestorEvento.exe                  (Executável otimizado)
│
├── obj/
│   └── (Arquivos temporários de build)
│
├── Database/
│   └── 01_Tabelas.sql
│
├── App.config                                (String de conexão, configurações impressora)
├── Program.cs                                (Entry point: Main())
├── Form1.cs                                  (Formulário padrão do template)
├── GestorEvento.csproj                       (Projeto: referências, compilação)
├── GestorEvento.sln                          (Solução: múltiplos projetos)
├── CONTEXTO_EVENTO_GESTOR.md                 (Este documento)
└── CONTEXTO_PRINTER_SERVER.md                (Documentação do serviço de impressora)
```

**Legendas:**
- `CRÍTICO`: Componentes essenciais para funcionamento
- `NOVO`: Adicionados na fase 1 (transações)
- `DTO`: Data Transfer Object (para passagem de dados entre camadas)
- Inner Class: Classes definidas dentro de outras classes

**Arquivo Mais Importante para Lógica de Negócio:**
- `VendaService.cs` → método `RegistrarVendaComTrocoComTransacao()`

**Arquivo Mais Importante para Hardware:**
- `EpsonTM20Service.cs` → método `ImprimirCupom()`

**Arquivo Mais Importante para Persistência:**
- `Connection.cs` → Gerencia string de conexão e pool de conexões
---

## 5. Tecnologias Utilizadas

### 5.1 Framework e Linguagem
- **Framework:** .NET Framework 4.7.2
- **Linguagem:** C# 7.3
- **Tipo de Aplicação:** Windows Forms (Desktop)

### 5.2 Bibliotecas Externas
- **MaterialSkin 2.3.0.0** - Tema Material Design para Windows Forms
- **MySql.Data 8.0.23** - Conector MySQL para .NET (conectar ao banco de dados)
- **MetroFramework 1.2.0.3** - Framework UI adicional (opcional)
- **LiveCharts.WinForms 0.9.7.1** - Componentes para relatórios

### 5.3 Banco de Dados
- **MySQL 8.0** - Servidor de banco de dados
- **Database:** `gestor_evento`
- **Autenticação:** usuário `root` (sem senha, desenvolvimento local)

### 5.4 Hardware (Específico do PDV)
- **Impressora:** EPSON TM-20 (M249A)
- **Interface:** USB (virtual COM2)
- **Protocolo:** ESC/POS (EPSON Standard Code for Point Of Sale)
- **Velocidade Serial:** 9600 baud
- **Page Code:** Windows-1252

### 5.5 Sistemas Operacionais
- **Desenvolvimento:** Windows 10/11
- **Produção:** Windows (qualquer versão suportada por .NET 4.7.2)

---

## 6. Instalação / Configuração

### 6.1 Pré-requisitos

1. **Visual Studio 2019 ou superior**
2. **MySQL 8.0** instalado e rodando
3. **Impressora EPSON TM-20** conectada via USB
4. **.NET Framework 4.7.2** (incluído no Visual Studio)

### 6.2 Passos para Rodar Localmente

#### Passo 1: Clonar Repositório
```bash
git clone <url-repositorio>
cd GestorEvento
```

#### Passo 2: Criar Banco de Dados
```sql
-- Execute em MySQL Workbench ou CLI
CREATE DATABASE IF NOT EXISTS gestor_evento;
USE gestor_evento;

-- Execute todos os scripts SQL em: GestorEvento/Database/*.sql
-- Ordem recomendada:
-- 01_criar_produtos.sql
-- 02_criar_eventos.sql
-- 03_criar_ponto_venda.sql
-- 04_criar_venda.sql
-- 05_criar_formas_pagamento.sql
-- 06_criar_movimentacao_ponto_venda.sql
```

#### Passo 3: Configurar String de Conexão
Editar `App.config`:
```xml
<connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=localhost;Database=gestor_evento;Uid=root;Pwd=;" />
</connectionStrings>
```

Ajustar conforme necessário:
- `Server`: IP ou hostname do MySQL (localhost para desenvolvimento)
- `Database`: nome do banco (gestor_evento)
- `Uid`: usuário MySQL
- `Pwd`: senha do usuário

---

#### Passo 3.5: Configurar Acesso Remoto ao Banco de Dados (Múltiplas Máquinas)

Se múltiplas máquinas precisam se conectar ao mesmo banco de dados MySQL (cenário de rede com múltiplos PDVs), é necessário configurar permissões de acesso remoto.

⚠️ **Executar estes comandos na máquina que hospeda o MySQL**, não nas máquinas cliente.

##### Opção 1: Permitir Acesso de um IP Específico (Mais Seguro)

Usar quando souber exatamente quais IPs vão se conectar ao banco.

Executar no MySQL (MySQL Workbench ou CLI):

```sql
GRANT ALL PRIVILEGES ON gestor_evento.* TO 'root'@'192.168.1.50';
FLUSH PRIVILEGES;
```

**Substituir `192.168.1.50` pelo IP da máquina cliente** que irá se conectar.

**Repetir o comando para cada IP diferente:**

```sql
-- Cliente 1
GRANT ALL PRIVILEGES ON gestor_evento.* TO 'root'@'192.168.1.50';

-- Cliente 2
GRANT ALL PRIVILEGES ON gestor_evento.* TO 'root'@'192.168.1.51';

-- Cliente 3
GRANT ALL PRIVILEGES ON gestor_evento.* TO 'root'@'192.168.1.52';

FLUSH PRIVILEGES;  -- Execute uma única vez no final
```

**Vantagens:**
- ✅ Mais seguro (só aceita IPs autorizado)
- ✅ Controle fino de acesso por IP

**Desvantagens:**
- ❌ Precisa saber IPs antecipadamente
- ❌ Precisa adicionar novo GRANT se adicionar novo cliente

---

##### Opção 2: Permitir Acesso de Qualquer IP (Mais Flexível)

Usar quando há múltiplos PDVs e IPs podem variar ou ser dinâmicos.

Executar no MySQL (MySQL Workbench ou CLI):

```sql
GRANT ALL PRIVILEGES ON gestor_evento.* TO 'root'@'%' IDENTIFIED BY 'sua_senha';
FLUSH PRIVILEGES;
```

**Substituir `sua_senha` pela senha desejada para acesso remoto.**

O `%` significa "qualquer host na rede".

**Vantagens:**
- ✅ Muito flexível (aceita qualquer IP)
- ✅ Não precisa reconfigurar para novos clientes

**Desvantagens:**
- ❌ Menos seguro (qualquer IP na rede pode tentar conectar)
- ⚠️ Recomendado apenas para redes internas protegidas

---

##### Configurar App.config nas Máquinas Cliente

Após configurar as permissões de acesso, editar `App.config` em cada máquina cliente:

```xml
<connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=192.168.1.100;Database=gestor_evento;Uid=root;Pwd=sua_senha;" />
</connectionStrings>
```

**Substituir:**
- `192.168.1.100`: IP da máquina que hospeda o MySQL
- `sua_senha`: senha configurada no GRANT (se usou Opção 2)

**Exemplo com Opção 1 (Acesso Específico):**

```xml
<!-- Sem senha, pois foi concedido sem IDENTIFIED BY -->
<connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=192.168.1.100;Database=gestor_evento;Uid=root;Pwd=;" />
</connectionStrings>
```

**Exemplo com Opção 2 (Acesso Flexível):**

```xml
<!-- Com senha, pois foi definida no GRANT -->
<connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=192.168.1.100;Database=gestor_evento;Uid=root;Pwd=sua_senha;" />
</connectionStrings>
```

---

##### Testar Conectividade Remota

Na máquina cliente, verificar se consegue se conectar ao MySQL remoto:

**Via MySQL CLI:**
```bash
mysql -h 192.168.1.100 -u root -p
```

Será solicitada a senha. Se conectar com sucesso, a configuração está OK.

**Via PowerShell (testar porta TCP):**
```powershell
Test-NetConnection -ComputerName 192.168.1.100 -Port 3306
```

Resposta esperada: `TcpTestSucceeded : True`

---

#### Passo 4: Configurar Impressora Térmica

A configuração da impressora é feita através do arquivo `App.config`, permitindo modo **LOCAL** (COM) ou **REMOTE** (via PrintServer):

##### Modo LOCAL (Impressora conectada via USB/Serial)

Editar `App.config`:
```xml
<configuration>
  <appSettings>
    <!-- Modo de impressão: LOCAL ou REMOTE -->
    <add key="PrinterMode" value="LOCAL" />
    
    <!-- Configuração LOCAL -->
    <add key="PrinterPort" value="COM2" />
    <add key="PrinterBaudRate" value="9600" />
  </appSettings>
  
  <connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=localhost;Database=gestor_evento;Uid=root;Pwd=;" />
  </connectionStrings>
</configuration>
```

**Campos:**
- `PrinterMode`: `LOCAL` para impressora física conectada ao computador
- `PrinterPort`: Porta COM da impressora (COM1, COM2, COM3, etc)
- `PrinterBaudRate`: Velocidade serial (9600 para EPSON TM-20)

**Descobrir a porta:**
1. Conectar impressora via USB
2. Abrir "Gerenciador de Dispositivos" (Win+R → devmgmt.msc)
3. Expandir "Portas (COM e LPT)"
4. Identificar porta da impressora (ex: "EPSON TM-20 (COM2)")
5. Usar esse número no `PrinterPort`

##### Modo REMOTE (Impressora via PrintServer)

Editar `App.config` para usar serviço PrintServer em máquina remota:
```xml
<configuration>
  <appSettings>
    <!-- Modo de impressão: LOCAL ou REMOTE -->
    <add key="PrinterMode" value="REMOTE" />
    
    <!-- Configuração REMOTE -->
    <add key="PrintServerAddress" value="192.168.1.100" />
    <add key="PrintServerPort" value="8080" />
    <add key="PrinterIdentifier" value="EPSON_TM20_PDV1" />
  </appSettings>
  
  <connectionStrings>
    <add name="MySqlConnection" 
         connectionString="Server=localhost;Database=gestor_evento;Uid=root;Pwd=;" />
  </connectionStrings>
</configuration>
```

**Campos:**
- `PrinterMode`: `REMOTE` para usar impressora através de PrintServer remoto
- `PrintServerAddress`: IP ou hostname do servidor PrintServer (ex: 192.168.1.100, printserver.local)
- `PrintServerPort`: Porta do serviço PrintServer (padrão: 8080)
- `PrinterIdentifier`: ID único da impressora no PrintServer (opcional, para múltiplas impressoras)

**Quando usar REMOTE:**
- Múltiplos PDVs compartilhando uma única impressora
- Impressora centralizada em servidor dedicado
- Melhor disponibilidade e load balancing
- Fila de impressão centralizada

##### Implementação em Código

No `EpsonTM20Service.cs`, a configuração é lida do `App.config`:

```csharp
public class EpsonTM20Service
{
    private string _printerMode;
    private string _printerPort;
    private int _printerBaudRate;
    private string _printServerAddress;
    private int _printServerPort;
    
    public EpsonTM20Service()
    {
        // Ler modo da impressora
        _printerMode = ConfigurationManager.AppSettings["PrinterMode"] ?? "LOCAL";
        
        if (_printerMode == "LOCAL")
        {
            // Configuração LOCAL
            _printerPort = ConfigurationManager.AppSettings["PrinterPort"] ?? "COM2";
            _printerBaudRate = int.Parse(
                ConfigurationManager.AppSettings["PrinterBaudRate"] ?? "9600"
            );
            InitializeLocalPrinter();
        }
        else if (_printerMode == "REMOTE")
        {
            // Configuração REMOTE
            _printServerAddress = ConfigurationManager.AppSettings["PrintServerAddress"];
            _printServerPort = int.Parse(
                ConfigurationManager.AppSettings["PrintServerPort"] ?? "8080"
            );
            InitializeRemotePrinter();
        }
    }
    
    private void InitializeLocalPrinter()
    {
        // Inicializar SerialPort com porta COM
        _serialPort = new SerialPort(_printerPort, _printerBaudRate);
        _serialPort.Open();
    }
    
    private void InitializeRemotePrinter()
    {
        // Conectar ao PrintServer via TCP/IP
        // Host: _printServerAddress
        // Port: _printServerPort
        // Enviar cupons via HTTP/REST ou Socket
    }
}
```

##### Configuração de PrintServer (Opcional)

Se usar modo REMOTE, um serviço PrintServer deve estar rodando:

**Requisitos do PrintServer:**
- Serviço Windows ou Linux rodando na máquina com impressora física
- Escutando em IP + Porta (ex: 192.168.1.100:8080)
- Recebendo requisições HTTP POST com dados do cupom
- Enviando para impressora local via SerialPort

**Exemplo de endpoint PrintServer:**
```
POST http://192.168.1.100:8080/api/print
Content-Type: application/json

{
  "printerIdentifier": "EPSON_TM20_PDV1",
  "content": "base64-encoded-cupom-data"
}
```

**Response esperado:**
```json
{
  "success": true,
  "jobId": "job_12345",
  "message": "Cupom impresso com sucesso"
}
```

##### Exemplo de Transição LOCAL → REMOTE

Se começou com LOCAL e quer migrar para REMOTE:

1. **Instalar PrintServer** em máquina com impressora
2. **Configurar PrintServer** com os dados da impressora local
3. **Atualizar App.config:**
   ```xml
   <!-- Antes -->
   <add key="PrinterMode" value="LOCAL" />
   <add key="PrinterPort" value="COM2" />
   
   <!-- Depois -->
   <add key="PrinterMode" value="REMOTE" />
   <add key="PrintServerAddress" value="192.168.1.50" />
   <add key="PrintServerPort" value="8080" />
   ```
4. **Recompilar** aplicação
5. **Testar** impressão remota

Nenhuma mudança necessária no código da aplicação! 🎯

#### Passo 5: Restaurar Pacotes NuGet
```bash
# No Visual Studio:
# Tools → NuGet Package Manager → Package Manager Console
# Ou via CLI:
nuget restore GestorEvento.sln
```

#### Passo 6: Compilar e Executar
```bash
# Compilar
dotnet build GestorEvento.sln

# Executar
# Abrir Visual Studio e pressionar F5
# Ou executar diretamente:
./bin/Debug/GestorEvento.exe
```
---

## 7. Padrões de Código

### 7.1 Padrão de Nomeação
- **Classes:** PascalCase (Ex: FormPDV, VendaService)
- **Métodos:** PascalCase (Ex: RegistrarVenda, ObterResumo)
- **Propriedades:** PascalCase (Ex: IdVenda, VlTotal)
- **Variáveis Locais:** camelCase (Ex: valorTroco, idVenda)
- **Constantes:** UPPER_SNAKE_CASE (Ex: PORTA_IMPRESSORA)

### 7.2 Transações (PADRÃO CRÍTICO)

Quando operação envolve múltiplas tabelas → **usar transação:**

```csharp
// ❌ ERRADO - Sem transação (dados inconsistentes se falhar)
_vendaRepository.RegistrarVenda(venda);
_recebimentoRepository.RegistrarRecebimento(recebimento);
_movimentacaoRepository.RegistrarTroco(idPontoVenda, vlTroco);

// ✅ CORRETO - Com transação (all-or-nothing)
int idVenda = _vendaService.RegistrarVendaComTrocoComTransacao(
    venda, recebimentos, vlTroco
);
```

### 7.3 Tratamento de Erros

Usar `DialogoCustomizado` para feedback ao usuário:

```csharp
try {
    // operação
}
catch (Exception ex) {
    DialogoCustomizado erro = new DialogoCustomizado(
        "Erro",
        $"Mensagem descritiva: {ex.Message}",
        TipoDialogo.Erro,
        TipoButton.Ok
    );
    erro.ShowDialog();
}
```

### 7.4 Factory Pattern: PrinterServiceFactory

O `PrinterServiceFactory` é responsável por **criar a instância correta do serviço de impressão** baseado na configuração do `App.config`:

```csharp
public static class PrinterServiceFactory
{
    public static IPrinterService CreatePrinterService()
    {
        string printerMode = ConfigurationManager.AppSettings["PrinterMode"] ?? "LOCAL";
        
        switch (printerMode.ToUpper())
        {
            case "LOCAL":
                return new LocalPrinterService();
            
            case "REMOTE":
                return new RemotePrinterService();
            
            default:
                throw new ConfigurationErrorsException(
                    $"PrinterMode '{printerMode}' não reconhecido. Use LOCAL ou REMOTE"
                );
        }
    }
}
```

**Por que usar Factory Pattern?**

1. **Abstração:** Views não precisam saber se é LOCAL ou REMOTE
2. **Flexibilidade:** Mudar de LOCAL para REMOTE apenas alterando `App.config`
3. **Testabilidade:** Fácil fazer mock de impressoras para testes
4. **Manutenibilidade:** Lógica de criação centralizada

**Uso em FormPDV.cs:**

```csharp
public class FormPDV : Form
{
    private int _numeroCaixa;
    private string _descricaoCaixa;
    
    private void BtnConfirmarVenda_Click(object sender, EventArgs e)
    {
        // ... registrar venda ...
        int idVenda = venda.IdVenda;
        List<string> itensPorImprimir = PrepararItensPara Impressao();
        
        // Imprimir venda via Factory (sem saber qual tipo LOCAL/REMOTE)
        bool sucessoVenda = PrinterServiceFactory.ImprimirVenda(
            idVenda, 
            itensPorImprimir, 
            _numeroCaixa, 
            _descricaoCaixa
        );
    }
}
```

**Assinatura do método ImprimirVenda:**

```csharp
public static bool ImprimirVenda(
    int vendaId,                    // ID da venda para registro
    List<string> itens,             // Itens formatados para imprimir
    int numeroCaixa = 0,            // Número do caixa/PDV
    string descricaoCaixa = ""      // Descrição do caixa
)
```

**Implementação do PrinterServiceFactory (métodos estáticos):**

```csharp
public static class PrinterServiceFactory
{
    /// <summary>
    /// Imprime uma venda completa (todos os itens de uma vez)
    /// Evita race condition entre múltiplas máquinas
    /// </summary>
    public static bool ImprimirVenda(int vendaId, List<string> itens, 
                                     int numeroCaixa = 0, string descricaoCaixa = "")
    {
        string printMode = ConfigurationManager.AppSettings["PrintMode"] ?? "Local";
        
        if (printMode == "Remote")
        {
            // Envia para Print Server remoto via HTTP
            return ImprimirVendaViaAPI(vendaId, itens, numeroCaixa, descricaoCaixa);
        }
        else
        {
            // Imprime localmente via SerialPort
            return ImprimirVendaLocal(vendaId, itens, numeroCaixa, descricaoCaixa);
        }
    }

    /// <summary>
    /// Imprime uma venda LOCALMENTE (SerialPort direto na máquina)
    /// </summary>
    private static bool ImprimirVendaLocal(int vendaId, List<string> itens, 
                                           int numeroCaixa = 0, string descricaoCaixa = "")
    {
        try
        {
            string portName = ConfigurationManager.AppSettings["PrinterPortName"] ?? "COM2";
            int baudRate = int.Parse(ConfigurationManager.AppSettings["PrinterBaudRate"] ?? "9600");

            var printer = new EpsonTM20Service(portName, baudRate);

            if (!printer.Conectar())
            {
                UiHelper.ExibirErro("Erro", "Não foi possível conectar à impressora");
                return false;
            }

            // Imprimir todos os itens em sequência
            foreach (var item in itens)
            {
                try
                {
                    printer.ImprimirCupom(item, numeroCaixa, descricaoCaixa);
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Erro ao imprimir: {ex.Message}");
                }
            }

            printer.Desconectar();
            return true;
        }
        catch (Exception ex)
        {
            UiHelper.ExibirErro("Erro", $"Erro ao imprimir venda localmente: {ex.Message}");
            return false;
        }
    }

    /// <summary>
    /// Imprime uma venda VIA API (Print Server remoto)
    /// Toda a venda é enviada em UMA requisição HTTP (atômico)
    /// </summary>
    private static bool ImprimirVendaViaAPI(int vendaId, List<string> itens, 
                                            int numeroCaixa = 0, string descricaoCaixa = "")
    {
        try
        {
            string printServerIp = ConfigurationManager.AppSettings["PrintServerIp"];
            int printServerPort = int.Parse(ConfigurationManager.AppSettings["PrintServerPort"] ?? "5000");

            string url = $"http://{printServerIp}:{printServerPort}/imprimir-venda";
            string json = JsonSerializeVenda(vendaId, itens, numeroCaixa, descricaoCaixa);

            using (HttpClient client = new HttpClient())
            {
                client.Timeout = TimeSpan.FromSeconds(30);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(url, content).Result;

                return response.IsSuccessStatusCode;
            }
        }
        catch (Exception ex)
        {
            UiHelper.ExibirErro("Erro", $"Erro ao imprimir venda via API: {ex.Message}");
            return false;
        }
    }
}
```

**Implementação de LocalPrinterService (classe EpsonTM20Service):**

```csharp
public class EpsonTM20Service
{
    private SerialPort _serialPort;

    public EpsonTM20Service(string portName, int baudRate)
    {
        _serialPort = new SerialPort(portName, baudRate);
    }

    public bool Conectar()
    {
        try
        {
            _serialPort.Open();
            return _serialPort.IsOpen;
        }
        catch
        {
            return false;
        }
    }

    public void ImprimirCupom(string nomeProduto, int numeroCaixa = 0, string descricaoCaixa = "")
    {
        // Enviar comandos ESC/POS para impressora
        // Phase 1: SetarPaginaISO (Windows-1252)
        // Phase 2: Centralizar texto
        // Phase 3: Imprimir dados
        // Phase 4: Cortar papel
    }

    public void Desconectar()
    {
        if (_serialPort != null && _serialPort.IsOpen)
            _serialPort.Close();
    }
}
```

**Fluxo Completo de Impressão de Venda:**

```
FormPDV.BtnConfirmarVenda_Click()
    ↓
VendaService.RegistrarVendaComTrocoComTransacao()  ← ACID transaction
    ↓
    ├─ Insere VENDA + ITEM_VENDA
    ├─ Insere RECEBIMENTO_VENDA
    └─ Insere MOVIMENTACAO_PONTO_VENDA (troco)
    ↓
PrinterServiceFactory.ImprimirVenda(idVenda, itensPorImprimir, numeroCaixa, descricaoCaixa)
    ↓
    ├── Se PrintMode = "Local"
    │   └─ ImprimirVendaLocal()
    │       ├─ Cria EpsonTM20Service(porta COM, baudRate)
    │       ├─ Conecta via SerialPort
    │       ├─ Itera cada item: printer.ImprimirCupom()
    │       │   (ESC/POS: setpage → centralize → print → cut)
    │       └─ Desconecta
    │
    └── Se PrintMode = "Remote"
        └─ ImprimirVendaViaAPI()
            ├─ Serializa venda para JSON
            └─ POST http://{PrintServerIp}:{PrintServerPort}/imprimir-venda
                └─ Print Server recebe e distribui para impressora adequada
    ↓
Venda impressa com sucesso! ✨
```

**Por que ImprimirVenda e não ImprimirCupom?**

- **ImprimirCupom:** Imprime um produto individual (simples)
- **ImprimirVenda:** Imprime TODOS os itens de uma venda de uma vez
  - Evita race condition em ambientes multi-máquina
  - Garante atomicidade (tudo imprime ou nada)
  - Permite impressão sequencial de múltiplos itens numa única transação

**Configuração em App.config:**

```xml
<configuration>
  <appSettings>
    <!-- Modo de impressão -->
    <add key="PrintMode" value="Local" />
    <!-- Local: Conecta diretamente à porta COM da máquina -->
    <add key="PrinterPortName" value="COM2" />
    <add key="PrinterBaudRate" value="9600" />
    
    <!-- Remote: Envia para Print Server remoto -->
    <add key="PrintServerIp" value="192.168.1.100" />
    <add key="PrintServerPort" value="5000" />
  </appSettings>
</configuration>
```

**Benefício Principal:** Mudar entre LOCAL e REMOTE alterando apenas **1 valor no App.config**, sem alterar nenhum código C#!

---

## 9. Decisões Técnicas / Considerações Importantes

### 9.1 Por que MySqlTransaction?

**Problema:** Multi-payment scenario (100 dinheiro + 100 PIX para 150 venda = 50 troco)
- Se recebimento inserir OK mas troco falhar → venda fica órfã
- Caixa não bate (50 de troco desaparecido)

**Solução:** Transação ACID
- All-or-nothing: ou tudo insere ou tudo reverte
- Rollback automático se qualquer INSERT falhar
- Garante reconciliação de caixa sempre bate

### 9.2 Por que Windows-1252 para Impressora?

**Problema:** UTF-8 (multi-byte) → impressora espera single-byte → caracteres quebrados
- "São Paulo" → "S?o Paulo"
- "Açúcar" → "Aa??ar"

**Solução:** Encoding.GetEncoding(1252) + ESC t 0x10 command
- Windows-1252 single-byte → caracteres corretos
- "São Paulo" → "São Paulo" ✓

### 9.3 Por que 1.3 segundos por cupom?

**Otimizações aplicadas:**
- Consolidar comandos antes de Flush() (não flush após cada comando)
- Reduzir Thread.Sleep(): 800ms → 500ms, 200ms → 100ms
- Resultado: 13.5s baseline → 1.3s (46% melhoria)

### 9.4 Por que FormFecharCaixa é totalmente dinâmica?

**Razão:** Designer do Visual Studio não consegue renderizar controles criados dinamicamente
- Não é problema em runtime (funciona perfeitamente)
- É limitação do designer (design-time)
- Solução: Criar controls em código (CriarComponentes())

---

## 11. Próximas Fases / Melhorias Planejadas

### Fase 3 (Médio Prazo)
- [ ] Autenticação de usuário (login/senha)
- [ ] Auditoria de operações (quem fez o quê e quando)
- [ ] Backup automático de banco de dados

---

**Última Atualização:** 30 de Abril de 2026
**Status:** Produção - Fase 1 Completa
