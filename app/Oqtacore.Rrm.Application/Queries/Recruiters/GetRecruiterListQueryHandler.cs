using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Recruiters
{
    public class GetRecruiterListQueryHandler : IRequestHandler<GetRecruiterListQuery, GetRecruiterListQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetRecruiterListQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetRecruiterListQueryResult> Handle(GetRecruiterListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetRecruiterListQueryResult();

            var queryable = _dataContext.Admin.AsQueryable();

            var totalCount = await queryable.CountAsync();

            var reuslt =
                await queryable
                .Select(x => new GetRecruiterListQueryResultItem
                {
                    Id = x.id,
                    Name = x.Name,
                    Description = x.Description,
                    Email = x.Email
                })
                .ToListAsync();

            queryResult.TotalCount = totalCount;
            queryResult.Data = reuslt;
            queryResult.Success = true;

            return queryResult;
        }
    }
}