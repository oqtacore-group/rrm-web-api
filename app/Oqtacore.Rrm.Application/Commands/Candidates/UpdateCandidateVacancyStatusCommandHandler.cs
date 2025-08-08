using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Candidates
{
    public class UpdateCandidateVacancyStatusCommandHandler : RequestHandler<UpdateCandidateVacancyStatusCommand, UpdateCandidateVacancyStatusResult>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly ICandidateRepository _candidateRepository;
        private readonly IAdminRepository _adminRepository;
        public UpdateCandidateVacancyStatusCommandHandler(IVacancyRepository vacancyRepository, ICandidateRepository candidateRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _vacancyRepository = vacancyRepository;
            _candidateRepository = candidateRepository;
            _adminRepository = adminRepository;
        }
        protected async override Task<UpdateCandidateVacancyStatusResult> Execute(UpdateCandidateVacancyStatusCommand request, CancellationToken cancellationToken)
        {
            var result = new UpdateCandidateVacancyStatusResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var vacancy = await _vacancyRepository.Get(request.VacancyId);
            if(vacancy == null)
            {
                result.Message = "The Vacancy not found.";
                return result;
            }
            var candidate = await _candidateRepository.Get(request.CandidateId);
            if (candidate == null)
            {
                result.Message = "The Candidate not found.";
                return result;
            }

            var newConnect = new CandidatesVacancyStatu()
            {
                CandidateId = request.CandidateId,
                DateAdded = DateTime.UtcNow,
                Note = request.Note,
                VacancyId = request.VacancyId,
                VacancyStatusId = request.StatusId,
                CreatedBy = admin.id
            };

            await _candidateRepository.ChangeVacancyStatus(newConnect);

            result.Success = true;
            result.Message = "The candidate status updated successfully.";

            return result;
        }
    }
}
