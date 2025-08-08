using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Clients
{
    public class GetClientQueryHandler : IRequestHandler<GetClientQuery, GetClientQueryResult>
    {
        private readonly ApplicationContext dataContext;
        public GetClientQueryHandler(ApplicationContext dbContext)
        {
            dataContext = dbContext;
        }
        public async Task<GetClientQueryResult> Handle(GetClientQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetClientQueryResult();

            var queryable = dataContext.Client.Where(x => x.id == request.Id);

            var result = 
                await queryable
                .Select(x => new GetClientQueryResultItem 
                { 
                    id = x.id,
                    Name = x.Name,
                    Email = x.Email,
                    PhoneNumber = x.PhoneNumber,
                    SiteUrl = x.SiteUrl
                })
                .FirstOrDefaultAsync();

            if (result == null)
                throw new NotFoundException("Client not found.");

            queryResult.Data = result;
            queryResult.Success = true;

            return queryResult;
        }
    }
}