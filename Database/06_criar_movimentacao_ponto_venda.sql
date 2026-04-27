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

-- Verificar se tabela foi criada
SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES 
WHERE TABLE_SCHEMA = 'gestor_evento' 
AND TABLE_NAME = 'MOVIMENTACAO_PONTO_VENDA';
