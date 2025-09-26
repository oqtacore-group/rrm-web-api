using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Clients
{
    public class GetClientContactListQueryHandler : IRequestHandler<GetClientContactListQuery, GetClientContactListQueryResult>
    {
        private readonly ApplicationContext dataContext;
        public GetClientContactListQueryHandler(ApplicationContext dbContext)
        {
            dataContext = dbContext;
        }
        public async Task<GetClientContactListQueryResult> Handle(GetClientContactListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetClientContactListQueryResult();

            var queryable = dataContext.ClientContact.Where(x => x.ClientId == request.ClientId);

            var result = 
                await queryable
                .Select(x => new GetClientContactListQueryResultItem
                { 
                    id = x.id,
                    ClientId = x.ClientId,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                })
                .ToListAsync();

            queryResult.Data = result;
            queryResult.Success = true;

            return queryResult;
        }
    }
}