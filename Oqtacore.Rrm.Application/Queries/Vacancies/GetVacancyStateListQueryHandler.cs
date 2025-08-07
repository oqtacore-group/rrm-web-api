using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Vacancies
{
    public class GetVacancyStateListQueryHandler : IRequestHandler<GetVacancyStateListQuery, GetVacancyStateListQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetVacancyStateListQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetVacancyStateListQueryResult> Handle(GetVacancyStateListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetVacancyStateListQueryResult();

            var queryable = _dataContext.VacancyStateListViewModel.AsQueryable();

            if (request.ClientId == 0 || !_dataContext.Client.Any(t => t.id == request.ClientId))
            {
            }
            else
            {
                queryable = queryable.Where(t => t.ClientId == request.ClientId);
            }

            var totalCount = await queryable.CountAsync();

            var result =
                await queryable.Select(x => new GetVacancyStateListQueryResultItem
                {
                    VacancyId = x.VacancyId,
                    Name = x.Name,
                    ClientName = x.ClientName,
                    SuccessCount = x.SuccessCount,
                    WorkplaceNumber = x.WorkplaceNumber,
                    CandidateCount = x.CandidateCount,
                    ClientId = x.ClientId,
                    CreatedBy = x.CreatedBy,
                })
                .ToListAsync();

            queryResult.Data = result;
            queryResult.TotalCount = totalCount;
            queryResult.Success = true;

            return queryResult;
        }
    }
}
