using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries
{
    public class SearchQueryHandler : IRequestHandler<SearchQuery, SearchQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public SearchQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<SearchQueryResult> Handle(SearchQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new SearchQueryResult();

            #region Search clients

            var clientQuery = _dataContext.Client.Where(x => x.Name.Contains(request.SearchText));
            var clientResult = 
                await clientQuery
                .Select(x => new SearchQueryResultItem 
                { 
                    Id = x.id, 
                    Name = x.Name 
                })
                .Distinct()
                .OrderByDescending(x => x.Id).Take(5)
                .ToListAsync();

            #endregion

            #region Search vacancies

            var vacancyQuery = _dataContext.Vacancy.Where(t => t.Name.Contains(request.SearchText));
            var vacancieResult = 
                await vacancyQuery
                .Select(x => new SearchQueryResultItem 
                { 
                    Id = x.id, 
                    Name = x.Name 
                })
                .Distinct()
                .OrderByDescending(x => x.Id)
                .Take(5)
                .ToListAsync();

            #endregion

            #region Search candidates

            var candidateQuery = _dataContext.Candidate.Where(x => x.Name.Contains(request.SearchText));
            var candidateResult = 
                await candidateQuery
                .Select(x => new SearchQueryResultItem 
                { 
                    Id = x.id, 
                    Name = x.Name 
                })
                .Distinct()
                .OrderByDescending(x => x.Id)
                .Take(5)
                .ToListAsync();

            #endregion

            queryResult.CandidateOutput = candidateResult;
            queryResult.ClientOutput = clientResult;
            queryResult.VacancyOutput = vacancieResult;
            queryResult.Success = true;

            return queryResult;
        }
    }
}
