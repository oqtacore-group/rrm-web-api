
namespace Oqtacore.Rrm.Application.Queries.Candidates
{
    public class GetCandidateListQuery : ListQuery<GetCandidateListQueryResult>
    {
        public int? VacancyId { get; set; }
        public int? VacancyStatusId { get; set; }
    }
    public class GetCandidateListQueryResult : ListQueryResult<GetCandidateListQueryResultItem>
    {
    }
    public class GetCandidateListQueryResultItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public string? Sex { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
