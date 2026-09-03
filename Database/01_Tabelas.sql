DROP TABLE IF EXISTS ITEM_VENDA;  
DROP TABLE IF EXISTS DOACAO_VENDA;            
DROP TABLE IF EXISTS RECEBIMENTO_VENDA;       
DROP TABLE IF EXISTS REIMPRESSAO_ITENS;       
DROP TABLE IF EXISTS MOVIMENTACAO_PONTO_VENDA;
DROP TABLE IF EXISTS REIMPRESSAO;             
DROP TABLE IF EXISTS VENDA;
DROP TABLE IF EXISTS INSCRICAO_EVENTO;
DROP TABLE IF EXISTS PONTO_VENDA;
DROP TABLE IF EXISTS PRODUTO_EVENTO_MOVIMENTACAO;
DROP TABLE IF EXISTS PRODUTO_EVENTO;
DROP TABLE IF EXISTS FORMA_PAGAMENTO;
DROP TABLE IF EXISTS MOTIVOS_REIMPRESSAO;
DROP TABLE IF EXISTS EVENTO;
DROP TABLE IF EXISTS PRODUTO;

-- Tabela PRODUTO
CREATE TABLE PRODUTO (
    id_produto INT AUTO_INCREMENT PRIMARY KEY,
    nm_produto VARCHAR(255) NOT NULL,
    CONSTRAINT UQ_PRODUTO_NM_PRODUTO UNIQUE (nm_produto)
);

-- Tabela EVENTO
CREATE TABLE EVENTO (
    id_evento INT AUTO_INCREMENT PRIMARY KEY,
    nm_evento VARCHAR(255) NOT NULL,
    dt_evento DATE NOT NULL,
    cd_status VARCHAR(20) NOT NULL DEFAULT 'Ativo',
    dt_encerramento DATETIME NULL,
    CONSTRAINT UQ_EVENTO_NM_EVENTO_DT_EVENTO UNIQUE (nm_evento, dt_evento)
);

-- Tabela PRODUTO_EVENTO (Relacionamento entre produtos e eventos)
CREATE TABLE PRODUTO_EVENTO (
    id_produto_evento INT AUTO_INCREMENT PRIMARY KEY,
    id_produto INT NOT NULL,
    id_evento INT NOT NULL,
    vl_produto DECIMAL(10, 2) NOT NULL,
    qtde_produto INT NOT NULL,
	qtde_vendida INT NOT NULL,
    fl_ativo VARCHAR(3) DEFAULT 'SIM',
    fl_permite_vl_zerado VARCHAR(3) DEFAULT 'NAO',
    CONSTRAINT UQ_PRODUTO_EVENTO_ID_PRODUTO_ID_EVENTO UNIQUE (id_produto, id_evento),
    FOREIGN KEY (id_produto) REFERENCES PRODUTO(id_produto) ,
    FOREIGN KEY (id_evento) REFERENCES EVENTO(id_evento)
);

ALTER TABLE PRODUTO_EVENTO
ADD COLUMN fl_antecipado VARCHAR(3) DEFAULT 'NAO';

ALTER TABLE PRODUTO_EVENTO ADD COLUMN fl_permite_vl_zerado VARCHAR(3) DEFAULT 'NAO';

-- Tabela INSCRICAO_EVENTO
-- Descricao: Inscrições importadas de planilha (almoço/produto comprado antecipadamente fora do sistema),
-- vinculadas a um evento e retiradas automaticamente no PDV via pesquisa por nome/CPF/e-mail
CREATE TABLE IF NOT EXISTS INSCRICAO_EVENTO (
    id_inscricao_evento INT AUTO_INCREMENT PRIMARY KEY,
    id_evento INT NOT NULL,
    nm_participante VARCHAR(255) NOT NULL,
    ds_email VARCHAR(255) NULL,
    nr_cpf_cnpj VARCHAR(20) NOT NULL,          -- normalizado: somente dígitos
    nr_celular VARCHAR(15) NULL,               -- normalizado: somente dígitos
    qtde_antecipada INT NOT NULL,
    cd_status VARCHAR(20) NOT NULL DEFAULT 'Pendente',   -- Pendente | Retirado
    dt_criacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    dt_retirada DATETIME NULL,
    CONSTRAINT UQ_INSCRICAO_EVENTO_ID_EVENTO_CPF UNIQUE (id_evento, nr_cpf_cnpj),
    CONSTRAINT fk_inscricao_evento_evento FOREIGN KEY (id_evento) REFERENCES EVENTO(id_evento),
    INDEX idx_inscricao_evento_status (id_evento, cd_status)
);


-- Tabela PRODUTO_EVENTO_MOVIMENTACAO (histórico de alterações de preço/quantidade)
CREATE TABLE PRODUTO_EVENTO_MOVIMENTACAO (
    id_produto_evento_movimentacao INT AUTO_INCREMENT PRIMARY KEY,
    id_produto_evento INT NOT NULL,
    vl_produto_anterior DECIMAL(10, 2) NOT NULL,
    vl_produto_novo DECIMAL(10, 2) NOT NULL,
    qtde_produto_anterior INT NOT NULL,
    qtde_produto_novo INT NOT NULL,
    dt_movimentacao DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_produto_evento) REFERENCES PRODUTO_EVENTO(id_produto_evento),
    INDEX idx_produto_evento_movimentacao (id_produto_evento),
    INDEX idx_produto_evento_movimentacao_dt (dt_movimentacao)
);

CREATE TABLE FORMA_PAGAMENTO (
    id_forma_pagamento INT AUTO_INCREMENT PRIMARY KEY,
    nm_forma_pagamento VARCHAR(50) NOT NULL UNIQUE,
    cd_forma_pagamento VARCHAR(20) NOT NULL UNIQUE,
    fl_ativo VARCHAR(3) DEFAULT 'SIM'
);

CREATE TABLE PONTO_VENDA (
    id_ponto_venda INT AUTO_INCREMENT PRIMARY KEY,
    id_evento INT NOT NULL, 
	no_ponto_venda INT NOT NULL,
	ds_ponto_venda VARCHAR(50),
    cd_status VARCHAR(50),
    dt_abertura DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    vl_inicial DECIMAL(10,2) NOT NULL,
    dt_fechamento DATETIME NULL,
    vl_final DECIMAL(10,2) NULL,
    obs_caixa TEXT,
    FOREIGN KEY (id_evento) REFERENCES EVENTO(id_evento)
);

-- Transação de venda (pode ter múltiplos produtos)
-- id_inscricao_evento: venda que retirou uma inscrição antecipada (se houver). FK fica em VENDA
-- (não em INSCRICAO_EVENTO) para já suportar, sem migração futura, uma mesma inscrição gerando mais de uma venda
CREATE TABLE VENDA (
    id_venda INT AUTO_INCREMENT PRIMARY KEY,
    id_ponto_venda INT NOT NULL,
	cd_status VARCHAR(50) NOT NULL,
    vl_total DECIMAL(10,2) NOT NULL,
    tp_operacao ENUM('VENDA', 'CORTESIA') NOT NULL DEFAULT 'VENDA',
    dt_venda DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_ponto_venda) REFERENCES PONTO_VENDA(id_ponto_venda),
    INDEX idx_venda_tp_operacao (tp_operacao)
);

ALTER TABLE VENDA
ADD COLUMN id_inscricao_evento INT NULL,
ADD CONSTRAINT fk_venda_inscricao_evento
    FOREIGN KEY (id_inscricao_evento)
    REFERENCES INSCRICAO_EVENTO(id_inscricao_evento),
ADD INDEX idx_venda_id_inscricao_evento (id_inscricao_evento);

-- Cada produto vendido naquela transação
CREATE TABLE ITEM_VENDA (
    id_item_venda INT AUTO_INCREMENT PRIMARY KEY,
    id_venda INT NOT NULL,
    id_produto_evento INT NOT NULL,
    qtde_vendida INT NOT NULL,
    vl_unitario DECIMAL(10,2) NOT NULL,
    vl_subtotal DECIMAL(10,2) NOT NULL,
    FOREIGN KEY (id_venda) REFERENCES VENDA(id_venda),
    FOREIGN KEY (id_produto_evento) REFERENCES PRODUTO_EVENTO(id_produto_evento)
);

-- Formas de pagamento da transação
CREATE TABLE RECEBIMENTO_VENDA (
    id_recebimento_venda INT AUTO_INCREMENT PRIMARY KEY,
    id_venda INT NOT NULL,
    id_forma_pagamento INT NOT NULL,
    vl_recebimento_venda DECIMAL(10,2) NOT NULL,
    dt_recebimento_venda DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_venda) REFERENCES VENDA(id_venda),
    FOREIGN KEY (id_forma_pagamento) REFERENCES FORMA_PAGAMENTO(id_forma_pagamento)
);

-- Doações registradas na venda (ex.: cliente deixa o troco, ou parte dele, como doação)
-- Independente do cálculo de troco/recebimento: apenas um registro adicional vinculado à venda
CREATE TABLE DOACAO_VENDA (
    id_doacao_venda INT AUTO_INCREMENT PRIMARY KEY,
    id_venda INT NOT NULL,
    id_forma_pagamento INT NOT NULL,
    vl_doacao_venda DECIMAL(10,2) NOT NULL,
    dt_doacao_venda DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_venda) REFERENCES VENDA(id_venda),
    FOREIGN KEY (id_forma_pagamento) REFERENCES FORMA_PAGAMENTO(id_forma_pagamento)
);

INSERT INTO FORMA_PAGAMENTO (nm_forma_pagamento, cd_forma_pagamento, fl_ativo) VALUES 
('Dinheiro', 'DINHEIRO', 'SIM'),
('Pix', 'PIX', 'SIM'),
('Débito', 'DEBITO', 'SIM'),
('Crédito', 'CREDITO', 'SIM');


-- Script: Criar tabela MOVIMENTACAO_PONTO_VENDA
-- Descricao: Registra movimentações de entrada/saída de dinheiro no ponto de venda
-- Tipos: TROCO (automático), SANGRIA (saída manual), ENTRADA_TROCO (entrada manual)

CREATE TABLE IF NOT EXISTS MOVIMENTACAO_PONTO_VENDA (
    id_movimentacao INT PRIMARY KEY AUTO_INCREMENT,
    id_ponto_venda INT NOT NULL,
    tipo_movimento ENUM('TROCO', 'SANGRIA', 'ENTRADA_TROCO') NOT NULL,
    vl_movimento DECIMAL(10,2) NOT NULL,
    dt_movimento DATETIME DEFAULT CURRENT_TIMESTAMP,
    descricao VARCHAR(200),
    id_venda INT,
    
    -- Constraints
    CONSTRAINT fk_movimentacao_ponto_venda FOREIGN KEY (id_ponto_venda) 
        REFERENCES PONTO_VENDA(id_ponto_venda),
    CONSTRAINT fk_movimentacao_venda FOREIGN KEY (id_venda) 
        REFERENCES VENDA(id_venda),
    
    -- Índices para performance
    INDEX idx_id_ponto_venda (id_ponto_venda),
    INDEX idx_tipo_movimento (tipo_movimento),
    INDEX idx_dt_movimento (dt_movimento)
);

-- 1. Catálogo de motivos para reimpressão
CREATE TABLE MOTIVOS_REIMPRESSAO (
    id_motivo INT PRIMARY KEY AUTO_INCREMENT,
    cd_motivo VARCHAR(50) UNIQUE NOT NULL,
    ds_motivo VARCHAR(200) NOT NULL,
    fl_ativo BOOLEAN DEFAULT TRUE,
    INDEX idx_fl_ativo (fl_ativo)
);

-- Dados iniciais - Motivos padrão
INSERT INTO MOTIVOS_REIMPRESSAO (cd_motivo, ds_motivo) VALUES
('CUPOM_DANIFICADO', 'Cupom rasgado/amassado/ilegível'),
('ERRO_IMPRESSORA', 'Erro de impressão/corte/papel'),
('TESTE_SISTEMA', 'Teste de equipamentos/sistema'),
('OUTRO', 'Outro motivo');

-- 2. Header da reimpressão (registro da operação)
CREATE TABLE REIMPRESSAO (
    id_reimpressao INT PRIMARY KEY AUTO_INCREMENT,
    dt_reimpressao DATETIME DEFAULT NOW(),
    id_motivo INT NOT NULL,
    id_evento INT,
    id_ponto_venda INT,
    vl_total DECIMAL(10,2),
    FOREIGN KEY (id_motivo) REFERENCES MOTIVOS_REIMPRESSAO(id_motivo),
    FOREIGN KEY (id_evento) REFERENCES EVENTO(id_evento),
    FOREIGN KEY (id_ponto_venda) REFERENCES PONTO_VENDA(id_ponto_venda),
    INDEX idx_evento_data (id_evento, dt_reimpressao),
    INDEX idx_motivo (id_motivo),
    INDEX idx_ponto_venda (id_ponto_venda)
);

-- 3. Itens reimpressos (produtos que foram reimpressos)
CREATE TABLE REIMPRESSAO_ITENS (
    id_reimpressao_item INT PRIMARY KEY AUTO_INCREMENT,
    id_reimpressao INT NOT NULL,
    id_produto_evento INT,
    qtde_reimpressao INT,
    vl_unitario DECIMAL(10,2),
    vl_subtotal DECIMAL(10,2),
    FOREIGN KEY (id_reimpressao) REFERENCES REIMPRESSAO(id_reimpressao),
    FOREIGN KEY (id_produto_evento) REFERENCES PRODUTO_EVENTO(id_produto_evento),
    INDEX idx_reimpressao (id_reimpressao),
    INDEX idx_produto_evento (id_produto_evento)
);