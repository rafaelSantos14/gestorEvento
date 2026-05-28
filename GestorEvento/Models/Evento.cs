using System;

namespace GestorEvento.Models
{
    public class Evento
    {
        public const string StatusAtivo = "Ativo";
        public const string StatusEncerrado = "Encerrado";

        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataEvento { get; set; }
        public string CdStatus { get; set; }
        public DateTime? DtEncerramento { get; set; }

        public bool IsEncerrado => string.Equals(CdStatus, StatusEncerrado, StringComparison.OrdinalIgnoreCase);

        // Construtores
        public Evento()
        {
            CdStatus = StatusAtivo;
        }

        public Evento(int id, string nome, DateTime dataEvento, string cdStatus = StatusAtivo, DateTime? dtEncerramento = null)
        {
            Id = id;
            Nome = nome;
            DataEvento = dataEvento;
            CdStatus = string.IsNullOrWhiteSpace(cdStatus) ? StatusAtivo : cdStatus;
            DtEncerramento = dtEncerramento;
        }
    }
}
