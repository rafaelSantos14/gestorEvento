using System.Collections.Generic;

namespace GestorEvento.Models
{
    /// <summary>
    /// Resultado da etapa de "preparação" (parse + validação + detecção de sumidos) de uma
    /// importação de planilha de inscrições - nada é gravado no banco ainda. O operador decide
    /// (via ConfirmarImportacao) o que fazer com RegistrosSumidos antes de a gravação ocorrer.
    /// </summary>
    public class PreparacaoImportacaoInscricao
    {
        public List<(string nome, string email, string cpf, string celular, int qtde)> ItensParaGravar { get; set; } = new List<(string, string, string, string, int)>();

        // Inscrições Pendentes do evento que não vieram na planilha nova - candidatas a exclusão,
        // mas a decisão é sempre do operador (nunca excluídas automaticamente)
        public List<InscricaoEvento> RegistrosSumidos { get; set; } = new List<InscricaoEvento>();

        public int TotalLinhasLidas { get; set; }
        public int TotalIgnoradasInvalidas { get; set; }
        public List<string> Avisos { get; set; } = new List<string>();
    }
}
