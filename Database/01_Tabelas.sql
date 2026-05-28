-- Dropar tabelas na ordem correta (de acordo com as foreign keys)

DROP TABLE IF EXISTS ITEM_VENDA;
DROP TABLE IF EXISTS RECEBIMENTO_VENDA;
DROP TABLE IF EXISTS MOVIMENTACAO_PONTO_VENDA;
DROP TABLE IF EXISTS VENDA;
DROP TABLE IF EXISTS PONTO_VENDA;
DROP TABLE IF EXISTS PRODUTO_EVENTO;
DROP TABLE IF EXISTS FORMA_PAGAMENTO;
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
    CONSTRAINT UQ_PRODUTO_EVENTO_ID_PRODUTO_ID_EVENTO UNIQUE (id_produto, id_evento),
    FOREIGN KEY (id_produto) REFERENCES PRODUTO(id_produto) ,
    FOREIGN KEY (id_evento) REFERENCES EVENTO(id_evento) 
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
CREATE TABLE VENDA (
    id_venda INT AUTO_INCREMENT PRIMARY KEY,
    id_ponto_venda INT NOT NULL,
	cd_status VARCHAR(50) NOT NULL, 
    vl_total DECIMAL(10,2) NOT NULL,
    dt_venda DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (id_ponto_venda) REFERENCES PONTO_VENDA(id_ponto_venda)
);

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