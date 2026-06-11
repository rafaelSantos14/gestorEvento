# Instalação da Impressora Tomate MDK-080

## Guia de Instalação

### 1. Instalação dos Drivers
- Instalar driver

## Configuração da Impressora na Aplicação

A configuração da impressora é feita através do arquivo **App.config** da aplicação GestorEvento. Não é necessário acessar menus da aplicação.

### Modo LOCAL (Impressora conectada via USB)

Editar `App.config` no diretório da aplicação:

```xml
<configuration>
  <appSettings>
    <!-- Modo de impressão: LOCAL ou REMOTE -->
    <add key="PrintMode" value="Local" />
    
    <!-- INFORMAR USB -->
    <add key="PrinterType" value="USB" />

    <!-- INFORMAR NOME DA IMPRESSORA CONFORME APRESENTA NO WINDOWS -->
    <add key="WindowsPrinterName" value="NOME DA IMPRESSORA" />
  </appSettings>
```

**Campos:**
- `PrintMode`: Definir como `Local` para impressora física conectada via USB
- `PrinterType`: Informar fixo "USB", pois esse tipo de impressora fica em porta USB e não COM (SERIAL)
- `WindowsPrinterName`: Nome da impressora conforme está no windows
---