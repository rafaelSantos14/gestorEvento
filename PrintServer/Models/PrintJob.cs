using System;

namespace PrintServer.Models
{
    public class PrintJob
    {
        public Guid JobId { get; set; }
        public string ProductName { get; set; }
        public int NumeroCaixa { get; set; }
        public string DescricaoCaixa { get; set; }
        public DateTime CreatedAt { get; set; }
        public int Attempts { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public PrintJob(string productName, int numeroCaixa = 0, string descricaoCaixa = "")
        {
            JobId = Guid.NewGuid();
            ProductName = productName;
            NumeroCaixa = numeroCaixa;
            DescricaoCaixa = descricaoCaixa ?? "";
            CreatedAt = DateTime.Now;
            Attempts = 0;
            Success = false;
        }

        public override string ToString()
        {
            return $"[{JobId:N}] {ProductName} - Caixa #{NumeroCaixa} - Tentativas: {Attempts}";
        }
    }
}
