
namespace Oqtacore.Rrm.Domain.Entity
{
    public class AuditLog
    {
        public int Id { get; set; }
        public int? UserId { get; set; }
        public string? Action { get; set; }
        public string? Method { get; set; }
        public string? IPAddress { get; set; }
        public string? Client { get; set; }
        public string? UserAgent { get; set; }
        public string? RequestBody { get; set; }
        public string? ResponseBody { get; set; }
        public string? ErrorMessage { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool Success { get; set; }

        //public User User { get; set; }
    }
}