using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Recruiters
{
    public class GetRecruiterStatisticQueryHandler : IRequestHandler<GetRecruiterStatisticQuery, GetRecruiterStatisticQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetRecruiterStatisticQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetRecruiterStatisticQueryResult> Handle(GetRecruiterStatisticQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetRecruiterStatisticQueryResult();

            var queryable = _dataContext.RecruiterStatisticViewModel.Where(x => x.DateAdded >= request.StartDate && x.DateAdded <= request.EndDate);

            if (request.RecruiterId > 0)
                queryable = queryable.Where(t => t.CreatedBy == request.RecruiterId);

            var totalRecords = await queryable.CountAsync();

            var result =
                await queryable
                .Select(x => new GetRecruiterStatisticQueryResultItem
                {
                    Id = x.id,
                    CandidateId = x.CandidateId,
                    VacancyId = x.VacancyId,
                    VacancyStatusId = x.VacancyStatusId,
                    StatusName = x.StatusName,
                    DateAdded = x.DateAdded,
                    CreatedBy = x.CreatedBy,
                    Success = x.Success,
                    Fail = x.Fail,
                    AdminName = x.AdminName,
                })
                .ToListAsync(cancellationToken);

            var totalSummary = new RecruiterSummary
            {
                TotalCount = await queryable.Select(t => new { t.VacancyId, t.CandidateId }).Distinct().CountAsync(),
                FailCount = await queryable.Where(t => t.Fail == true).Select(t => new { t.VacancyId, t.CandidateId }).Distinct().CountAsync(),
                SuccessCount = await queryable.Where(t => t.Success == true).Select(t => new { t.VacancyId, t.CandidateId }).Distinct().CountAsync(),
            };

            var statusList = _dataContext.VacancyStatusType.OrderBy(t => t.OrderId).ToList();

            queryResult.Data = result;
            queryResult.TotalCount = totalRecords;
            queryResult.StatusList = statusList;
            queryResult.Summary = totalSummary;

            queryResult.Success = true;

            return queryResult;
        }
    }
}