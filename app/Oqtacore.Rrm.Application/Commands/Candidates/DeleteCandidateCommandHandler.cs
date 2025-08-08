using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class DeleteCandidateCommandHandler : RequestHandler<DeleteCandidateCommand, DeleteCandidateResult>
    {
        private readonly ICandidateRepository _candidateRepository;
        private readonly IAdminRepository _adminRepository;
        public DeleteCandidateCommandHandler(ICandidateRepository candidateRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _candidateRepository = candidateRepository;
            _adminRepository = adminRepository;
        }
        protected override async Task<DeleteCandidateResult> Execute(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var result = new DeleteCandidateResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var candidate = await _candidateRepository.Get(request.Id);
            if (candidate == null)
            {
                result.Message = "The candidate not found.";
                return result;
            }

            var candidateArchive = new CandidateArchive(candidate, admin, "delete");
            await Repository.Add(candidateArchive);

            await _candidateRepository.Delete(candidate);

            result.Success = true;
            result.Message = "The candidate deleted successfully.";

            return result;
        }
    }
}