using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Application.Queries
{
    public class GetCandidateEventQuery : SingleQuery<GetCandidateEventQueryResult>
    {
        public int Id { get; set; }
    }
    public class GetCandidateEventQueryResult : SingleQueryResult<GetCandidateEventQueryResultItem>
    {
        public List<Candidate> CandidateList { get; set; }
        public List<CandidateEventType> EventTypeList { get; set; }
    }
    public class GetCandidateEventQueryResultItem
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int CandidateId { get; set; }
        public string Caption { get; set; }
        public int TypeId { get; set; }
        public bool? Completed { get; set; }
        public int CreatedBy { get; set; }
        public string ZoomLink { get; set; }
        public bool? ReminderSent { get; set; }
        public bool? ReminderEarlySent { get; set; }
        public string HashCode { get; set; }
    }
}