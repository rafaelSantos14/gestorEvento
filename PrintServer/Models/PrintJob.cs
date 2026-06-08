using System;

namespace PrintServer.Models
{
    public class PrintJob
    {
        public Guid JobId { get; set; }
        public string ProductName { get; set; }
        public decimal Preco { get; set; }
        public int NumeroCaixa { get; set; }
        public string DescricaoCaixa { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Attempts { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public PrintJob(string productName, int numeroCaixa = 0, string descricaoCaixa = "", decimal preco = 0)
        {
            JobId = Guid.NewGuid();
            ProductName = productName;
            Preco = preco;
            NumeroCaixa = numeroCaixa;
            DescricaoCaixa = descricaoCaixa ?? "";
            CreatedAt = DateTime.Now;
            Attempts = 0;
            Success = false;
        }

        public override string ToString()
        {
            return $"[{JobId:N}] {ProductName} - R$ {Preco:F2} - Caixa #{NumeroCaixa} - Tentativas: {Attempts}";
        }
    }
}
