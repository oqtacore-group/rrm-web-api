
namespace Oqtacore.Rrm.Application.Queries.Vacancies
{
    public class GetVacancyStatusListQuery : ListQuery<GetVacancyStatusListQueryResult>
    {
        public int VacancyId { get; set; }
    }
    public class GetVacancyStatusListQueryResult : ListQueryResult<GetVacancyStatusListQueryResultItem>
    {
    }
    public class GetVacancyStatusListQueryResultItem
    {
        public int VacancyId { get; set; }
        public int StatusId { get; set; }
        public string Name { get; set; }
        public bool? CountSuccess { get; set; }
        public int OrderId { get; set; }
        public int CandidateCount { get; set; }
    }
}