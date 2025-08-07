using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class UpdateCandidateCommandHandler : RequestHandler<UpdateCandidateCommand, UpdateCandidateResult>
    {
        private readonly ICandidateRepository _candidateRepository;
        public UpdateCandidateCommandHandler(ICandidateRepository candidateRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _candidateRepository = candidateRepository;
        }
        protected override async Task<UpdateCandidateResult> Execute(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var result = new UpdateCandidateResult();

            var candidate = await _candidateRepository.Get(request.Id);
            if (candidate == null)
            {
                result.Message = "Candidate not found.";
                return result;
            }

            candidate.Name = request.Name;
            candidate.Sex = request.Sex;
            candidate.DateOfBirth = request.DateOfBirth;
            candidate.PhoneNumber = request.PhoneNumber;
            candidate.Email = request.Email;
            candidate.HHurl = request.HHurl;
            candidate.lastjob_position = request.lastjob_position;
            candidate.lastjob_company = request.lastjob_company;
            candidate.salary = request.salary;
            candidate.resume_text = request.resume_text;
            candidate.favorite = request.favorite;

            await Repository.Update(candidate);

            result.Success = true;
            result.Message = "The candidate updated successfully.";

            return result;
        }
    }
}