using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Clients
{
    public class GetClientListQueryHandler : IRequestHandler<GetClientListQuery, GetClientListQueryResult>
    {
        private readonly ApplicationContext dataContext;
        public GetClientListQueryHandler(ApplicationContext dbContext)
        {
            dataContext = dbContext;
        }
        public async Task<GetClientListQueryResult> Handle(GetClientListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetClientListQueryResult();

            var queryable = dataContext.Client.AsQueryable();

            switch (request.StateName)
            {
                case "all":
                    //queryable = thql.Client.ToList();
                    break;
                case "new":
                    queryable = queryable.Where(t => !dataContext.Vacancy.Any(x => x.ClientId == t.id));
                    break;
                case "working":
                    queryable = queryable.Where(t => dataContext.Vacancy.Any(x => x.ClientId == t.id && x.Opened));
                    break;
                case "closed":
                    queryable = queryable.Where(t => !dataContext.Vacancy.Any(x => x.ClientId == t.id && x.Opened) && dataContext.Vacancy.Any(x => x.ClientId == t.id && !x.Opened));
                    break;
                default:
                    break;
            }

            var totalCount = await queryable.CountAsync();

            var result = 
                await queryable
                .Select(x => new GetClientListQueryResultItem
                { 
                    id = x.id,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    SiteUrl = x.SiteUrl,
                    CreatedOn = x.Created,
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