using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class AddCandidateCommandHandler: RequestHandler<AddCandidateCommand, AddCandidateResult>
    {
        public AddCandidateCommandHandler(IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
        }
        protected override async Task<AddCandidateResult> Execute(AddCandidateCommand request, CancellationToken cancellationToken)
        {
            var result = new AddCandidateResult();

            var newCandidate = new Candidate
            {
                Name = request.Name,
                Sex = request.Sex,
                DateOfBirth = request.DateOfBirth,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                HHurl = request.HHurl,
                lastjob_position = request.lastjob_position,
                lastjob_company = request.lastjob_company,
                salary = request.salary,
                resume_text = request.resume_text,
                favorite = request.favorite,
            };

            await Repository.Add(newCandidate);

            result.Id = newCandidate.id;
            result.Success = true;
            result.Message = "The candidate added successfully.";

            return result;
        }
    }
}