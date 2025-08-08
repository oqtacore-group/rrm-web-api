using MediatR;
using Oqtacore.Rrm.Application.Commands;

namespace Oqtacore.Rrm.Application.Queries
{
    public class SearchQuery : IRequest<SearchQueryResult>
    {
        public string SearchText { get; set; }
    }
    public class SearchQueryResult : Result
    {
        public List<SearchQueryResultItem> CandidateOutput { get; set; }
        public List<SearchQueryResultItem> ClientOutput { get; set; }
        public List<SearchQueryResultItem> VacancyOutput { get; set; }
    }
    public class SearchQueryResultItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
