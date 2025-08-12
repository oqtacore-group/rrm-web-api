using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Application.Queries
{
    public class GetCandidateEventListQuery : ListQuery<GetCandidateEventListQueryResult>
    {
    }
    public class GetCandidateEventListQueryResult : ListQueryResult<GetCandidateEventListQueryResultItem>
    {
        public List<CandidateEventType> eventTypeList { get; set; }
    }
    public class GetCandidateEventListQueryResultItem
    {
        public int id { get; set; }
        public DateTime? Date { get; set; }
        public int? CandidateId { get; set; }
        public string? Caption { get; set; }
        public int TypeId { get; set; }
        public string? Location { get; set; }
        public string? EventType { get; set; }
        public bool? Completed { get; set; }
        public string? ZoomLink { get; set; }
        public string? CandidateName { get; set; }
    }
}
