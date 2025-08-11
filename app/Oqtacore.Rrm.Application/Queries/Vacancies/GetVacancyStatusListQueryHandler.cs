using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Vacancies
{
    public class GetVacancyStatusListQueryHandler : IRequestHandler<GetVacancyStatusListQuery, GetVacancyStatusListQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetVacancyStatusListQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetVacancyStatusListQueryResult> Handle(GetVacancyStatusListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetVacancyStatusListQueryResult();

            var queryable = _dataContext.VacancyStatusListingViewModel.AsQueryable();

            var totalCount = await queryable.CountAsync();

            queryable = queryable.OrderBy(x => x.OrderId);

            var result =
                await queryable.Select(x => new GetVacancyStatusListQueryResultItem
                {
                    VacancyId = x.VacancyId,
                    StatusId = x.StatusId,
                    Name = x.Name,
                    CountSuccess = x.CountSuccess,
                    OrderId = x.OrderId,
                    CandidateCount = x.CandidateCount,
                })
                .ToListAsync();

            queryResult.Data = result;
            queryResult.TotalCount = totalCount;
            queryResult.Success = true;

            return queryResult;
        }
    }
}
