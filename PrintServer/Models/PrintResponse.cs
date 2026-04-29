namespace PrintServer.Models
{
    public class PrintResponse
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string JobId { get; set; }

        public PrintResponse(bool success, string message, string jobId = null)
        {
            Success = success;
            Message = message;
            JobId = jobId;
        }
    }
}
