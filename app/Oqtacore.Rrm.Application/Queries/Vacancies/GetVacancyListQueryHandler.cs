using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Vacancies
{
    public class GetVacancyListQueryHandler : IRequestHandler<GetVacancyListQuery, GetVacancyListQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetVacancyListQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetVacancyListQueryResult> Handle(GetVacancyListQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetVacancyListQueryResult();

            var queryable = _dataContext.VacancyInfo.AsQueryable();

            var totalCount = await queryable.CountAsync();

            var result = await queryable.Select(x => new GetVacancyListQueryResultItem
            {
                Id = x.Id,
                Name = x.Name,
                ClientId = x.ClientId,
                ClientName = x.ClientName,
                WorkplaceNumber = x.WorkplaceNumber,
                Status = x.Status,
                SalaryLowerEnd = x.SalaryLowerEnd,
                SalaryHighEnd = x.SalaryHighEnd,
                SalaryCurrency = x.SalaryCurrency,
                Experience = x.Experience,
                Location = x.Location,
                LocalTime = x.LocalTime,
                RelocationHelp = x.RelocationHelp,
                RemoteWorkPlace = x.RemoteWorkPlace,
                CreatedBy = x.CreatedBy,
                Opened = x.Opened,
                Responsibility = x.Responsibility,
                Skills = x.Skills,
                PersonalQuality = x.PersonalQuality,
                Languages = x.Languages,
                Notes = x.Notes
            })
                .ToListAsync();

            queryResult.Data = result;
            queryResult.TotalCount = totalCount;
            queryResult.Success = true;

            return queryResult;
        }
    }
}