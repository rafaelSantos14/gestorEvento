using System;

namespace GestorEvento.Models
{
    public class Evento
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataEvento { get; set; }

        // Construtores
        public Evento()
        {
        }

        public Evento(int id, string nome, DateTime dataEvento)
        {
            Id = id;
            Nome = nome;
            DataEvento = dataEvento;
        }
    }
}
