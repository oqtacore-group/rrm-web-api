using MediatR;
using Microsoft.EntityFrameworkCore;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Infrastructure.Data;

namespace Oqtacore.Rrm.Application.Queries.Candidates
{
    public class GetCandidateQueryHandler : IRequestHandler<GetCandidateQuery, GetCandidateQueryResult>
    {
        private readonly ApplicationContext _dataContext;
        public GetCandidateQueryHandler(ApplicationContext dataContext)
        {
            _dataContext = dataContext;
        }
        public async Task<GetCandidateQueryResult> Handle(GetCandidateQuery request, CancellationToken cancellationToken)
        {
            var queryResult = new GetCandidateQueryResult();

            var queryable = _dataContext.Candidate.Where(x => x.id == request.Id);

            var result = await queryable.Select(x => new GetCandidateQueryResultItem
            {
                Id = x.id,
                Name = x.Name,
                Email = x.Email,
                PhoneNumber = x.PhoneNumber,
                Sex = x.Sex,
                DateOfBirth = x.DateOfBirth,
                HHurl = x.HHurl,
                lastjob_position = x.lastjob_position,
                lastjob_company = x.lastjob_company,
                salary = x.salary,
                resume_text = x.resume_text,
                favorite = x.favorite,

            }).FirstOrDefaultAsync();

            if(result == null)
                throw new NotFoundException("Candidate not found"); 

            queryResult.Data = result;
            queryResult.Success = true;

            return queryResult;
        }
    }
}