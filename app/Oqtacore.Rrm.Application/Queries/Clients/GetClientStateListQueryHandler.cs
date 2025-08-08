using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Clients
{
    public class GetClientStateListQueryHandler : IRequestHandler<GetClientStateListQuery, GetClientStateListQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetClientStateListQueryHandler(ApplicationContext dbContext)
        {
            _dataContext = dbContext;
        }
        public async Task<GetClientStateListQueryResult> Handle(GetClientStateListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetClientStateListQueryResult();

            var allClientsCount = await _dataContext.Client.CountAsync(cancellationToken);
            var newClientsCount = await _dataContext.Client
                .CountAsync(t => !_dataContext.Vacancy.Any(x => x.ClientId == t.id), cancellationToken);
            var workingClientsCount = await _dataContext.Client
                .CountAsync(t => _dataContext.Vacancy.Any(x => x.ClientId == t.id && x.Opened), cancellationToken);
            var closedClientsCount = await _dataContext.Client
                .CountAsync(t => !_dataContext.Vacancy.Any(x => x.ClientId == t.id && x.Opened) &&
                                 _dataContext.Vacancy.Any(x => x.ClientId == t.id && !x.Opened), cancellationToken);

            var result = new List<GetClientStateListQueryResultItem>
            {
                new GetClientStateListQueryResultItem { Name = "all", ClientCount = allClientsCount },
                new GetClientStateListQueryResultItem { Name = "new", ClientCount = newClientsCount },
                new GetClientStateListQueryResultItem { Name = "working", ClientCount = workingClientsCount },
                new GetClientStateListQueryResultItem { Name = "closed", ClientCount = closedClientsCount }
            };

            queryResult.Data = result;
            queryResult.TotalCount = result.Count;
            queryResult.Success = true;

            return queryResult;
        }
    }
}