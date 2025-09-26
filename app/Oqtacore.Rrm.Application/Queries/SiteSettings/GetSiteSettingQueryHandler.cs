using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.SiteSettings
{
    public class GetSiteSettingQueryHandler : IRequestHandler<GetSiteSettingQuery, GetSiteSettingQueryResult>
    {
        private readonly ApplicationContext dataContext;
        public GetSiteSettingQueryHandler(ApplicationContext dbContext)
        {
            dataContext = dbContext;
        }
        public async Task<GetSiteSettingQueryResult> Handle(GetSiteSettingQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetSiteSettingQueryResult();

            var queryable = dataContext.SiteSetting.Where(x => x.Name == request.Name);

            var result =
                await queryable
                .Select(x => new GetSiteSettingQueryResultItem
                {
                    Name = x.Name,
                    Value = x.Value,
                })
                .FirstOrDefaultAsync();

            if (result == null)
                throw new NotFoundException("Site setting not found.");

            queryResult.Data = result;
            queryResult.Success = true;

            return queryResult;
        }
    }
}
