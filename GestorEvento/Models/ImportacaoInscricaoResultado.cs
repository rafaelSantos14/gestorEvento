using System.Collections.Generic;

namespace GestorEvento.Models
{
    /// <summary>
    /// Resumo do resultado de uma importação de planilha de inscrições, para exibição na UI.
    /// </summary>
    public class ImportacaoInscricaoResultado
    {
        public int TotalLinhasLidas { get; set; }
        public int TotalInseridas { get; set; }
        public int TotalAtualizadas { get; set; }
        public int TotalIgnoradasInvalidas { get; set; }
        public int TotalIgnoradasJaRetiradas { get; set; }
        public int TotalExcluidas { get; set; }
        public List<string> Avisos { get; set; } = new List<string>();
    }
}
