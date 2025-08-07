using Oqtacore.Rrm.Domain.Models;

namespace Oqtacore.Rrm.Application.Queries.Recruiters
{
    public class GetRecruiterStatisticQuery : ListQuery<GetRecruiterStatisticQueryResult>
    {
        public int RecruiterId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
    public class GetRecruiterStatisticQueryResult : ListQueryResult<GetRecruiterStatisticQueryResultItem>
    {
        public List<VacancyStatusType> StatusList { get; set; }
        public RecruiterSummary Summary { get; set; }
    }
    public class RecruiterSummary
    {
        public int TotalCount { get; set; }
        public int SuccessCount { get; set; }
        public int FailCount { get; set; }
    }
    public class GetRecruiterStatisticQueryResultItem
    {
        public int Id { get; set; }
        public int CandidateId { get; set; }
        public int VacancyId { get; set; }
        public int VacancyStatusId { get; set; }
        public string StatusName { get; set; }
        public DateTime DateAdded { get; set; }
        public int? CreatedBy { get; set; }
        public bool? Success { get; set; }
        public bool? Fail { get; set; }
        public string AdminName { get; set; }
    }
}