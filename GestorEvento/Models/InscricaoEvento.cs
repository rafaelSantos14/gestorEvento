using System;

namespace GestorEvento.Models
{
    public class InscricaoEvento
    {
        public const string StatusPendente = "Pendente";
        public const string StatusRetirado = "Retirado";

        public int Id { get; set; }
        public int IdEvento { get; set; }
        public string NomeParticipante { get; set; }
        public string Email { get; set; }
        public string CpfCnpj { get; set; }
        public string Celular { get; set; }
        public int QtdeAntecipada { get; set; }
        public string CdStatus { get; set; }
        public DateTime DtCriacao { get; set; }
        public DateTime? DtRetirada { get; set; }

        public bool IsPendente => string.Equals(CdStatus, StatusPendente, StringComparison.OrdinalIgnoreCase);

        public InscricaoEvento()
        {
            CdStatus = StatusPendente;
        }
    }
}
