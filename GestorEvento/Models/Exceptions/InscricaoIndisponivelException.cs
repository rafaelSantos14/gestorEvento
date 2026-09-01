using System;

namespace GestorEvento.Models.Exceptions
{
    /// <summary>
    /// Exception lançada quando uma inscrição antecipada não existe ou já não está mais Pendente
    /// no momento da retirada (tentativa de reuso, ou uso simultâneo por outro terminal/operador)
    /// </summary>
    public class InscricaoIndisponivelException : Exception
    {
        public int IdInscricaoEvento { get; set; }
        public string NomeParticipante { get; set; }
        public string CdStatusAtual { get; set; }

        public InscricaoIndisponivelException(int idInscricaoEvento, string nomeParticipante, string cdStatusAtual)
            : base($"A inscrição de {nomeParticipante} já foi retirada (status atual: {cdStatusAtual}) e não pode ser usada novamente.")
        {
            IdInscricaoEvento = idInscricaoEvento;
            NomeParticipante = nomeParticipante;
            CdStatusAtual = cdStatusAtual;
        }
    }
}
