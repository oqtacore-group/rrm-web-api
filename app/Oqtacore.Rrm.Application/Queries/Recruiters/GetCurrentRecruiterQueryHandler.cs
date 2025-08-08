using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Application.Commands;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Recruiters
{
    public class GetCurrentRecruiterQueryHandler : IRequestHandler<GetCurrentRecruiterQuery, GetCurrentRecruiterQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        private readonly IHttpService _httpService;
        public GetCurrentRecruiterQueryHandler(ApplicationContext dataContext, IHttpService httpService)
        {
            _dataContext = dataContext;
            _httpService = httpService;
        }
        public async Task<GetCurrentRecruiterQueryResult> Handle(GetCurrentRecruiterQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetCurrentRecruiterQueryResult();

            var queryable = _dataContext.Admin.Where(x => x.Email == _httpService.LogonUserEmail);

            var reuslt =
                await queryable
                .Select(x => new GetCurrentRecruiterQueryResultItem
                {
                    Id = x.id,
                    Name = x.Name,
                    Description = x.Description,
                    Email = x.Email
                })
                .FirstOrDefaultAsync();

            queryResult.Data = reuslt;
            queryResult.Success = true;

            return queryResult;
        }
    }
}
