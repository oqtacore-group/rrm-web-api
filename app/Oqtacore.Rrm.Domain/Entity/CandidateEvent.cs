
namespace Oqtacore.Rrm.Domain.Entity
{
    public class CandidateEvent
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CandidateId { get; set; }
        public string? Caption { get; set; }
        public int TypeId { get; set; }
        public bool? Completed { get; set; }
        public int CreatedBy { get; set; }
        public string? ZoomLink { get; set; }
        public bool? ReminderSent { get; set; }
        public bool? ReminderEarlySent { get; set; }
        public string HashCode { get; set; }
    }
}