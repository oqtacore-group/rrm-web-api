using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Candidates
{
    public class GetCandidateListQueryHandler : IRequestHandler<GetCandidateListQuery, GetCandidateListQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetCandidateListQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetCandidateListQueryResult> Handle(GetCandidateListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetCandidateListQueryResult();

            var queryable = _dataContext.Candidate.AsQueryable();
            var currentVacancyStatusQuery = _dataContext.CurrentVacancyStatusListViewModel.AsQueryable();

            if(request.VacancyId.HasValue && request.VacancyId > 0)
            {
                currentVacancyStatusQuery = currentVacancyStatusQuery.Where(x => x.VacancyId == request.VacancyId);
            }
            if (request.VacancyStatusId.HasValue && request.VacancyStatusId > 0) 
            {
                currentVacancyStatusQuery = currentVacancyStatusQuery.Where(x => x.VacancyStatusId == request.VacancyStatusId);
            }

            if((request.VacancyId.HasValue && request.VacancyId > 0) || (request.VacancyStatusId.HasValue || request.VacancyStatusId > 0))
            {
                var candidateIds = currentVacancyStatusQuery.Select(x => x.CandidateId).ToArray();
                queryable = queryable.Where(x => candidateIds.Contains(x.id));
            }

            var totalCount = await queryable.CountAsync();

            switch (request.Sort)
            {
                case "name":
                    queryable = request.Descending ? queryable.OrderByDescending(x => x.Name) : queryable.OrderBy(x => x.Name);
                    break;
                default:
                    queryable = queryable.OrderByDescending(x => x.id);
                    break;
            }

            queryable = queryable.Skip(request.Page * request.PageSize).Take(request.PageSize);

            var result = await queryable.Select(x => new GetCandidateListQueryResultItem
            {
                Id = x.id,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Sex = x.Sex,
                DateOfBirth = x.DateOfBirth,
            }).ToListAsync();

            queryResult.TotalCount = totalCount;
            queryResult.Data = result;
            queryResult.Success = true;

            return queryResult;
        }
    }
}
