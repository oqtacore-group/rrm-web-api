using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Vacancies
{
    public class GetVacancyQueryHandler : IRequestHandler<GetVacancyQuery, GetVacancyQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetVacancyQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetVacancyQueryResult> Handle(GetVacancyQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetVacancyQueryResult();

            var queryable = _dataContext.VacancyInfo.Where(x => x.Id == request.Id);

            var result = 
                await
                queryable
                .Select(x => new GetVacancyQueryResultItem
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
                .FirstOrDefaultAsync();

            if (result == null)
                throw new NotFoundException("Vacancy not found.");

            queryResult.Data = result;
            queryResult.Success = true;

            return queryResult;
        }
    }
}