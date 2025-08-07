
namespace Oqtacore.Rrm.Domain.Entity
{
    public class CandidateEventInfo
    {
        public int Id { get; set; }
        public DateTime? Date { get; set; }
        public int? CandidateId { get; set; }
        public string? Caption { get; set; }
        public int TypeId { get; set; }
        public string? Location { get; set; }
        public string EventType { get; set; }
        public bool? Completed { get; set; }
        public string? ZoomLink { get; set; }
        public string CandidateName { get; set; }
    }
}