namespace Oqtacore.Rrm.Domain.Models
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public int StatusCode { get; set; }
        public string Path { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
