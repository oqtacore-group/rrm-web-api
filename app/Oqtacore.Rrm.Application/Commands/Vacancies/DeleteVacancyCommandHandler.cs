using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Vacancies
{
    public class DeleteVacancyCommandHandler : RequestHandler<DeleteVacancyCommand, DeleteVacancyResult>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IAdminRepository _adminRepository;
        public DeleteVacancyCommandHandler(IVacancyRepository vacancyRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _vacancyRepository = vacancyRepository;
            _adminRepository = adminRepository;
        }
        protected override async Task<DeleteVacancyResult> Execute(DeleteVacancyCommand request, CancellationToken cancellationToken)
        {
            var result = new DeleteVacancyResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var vacancy = await _vacancyRepository.Get(request.Id);
            if (vacancy == null)
            {
                result.Message = "The vacancy not found.";
                return result;
            }


            #region Add vacancy archive

            var vacancyArchive = new VacancyArchive(vacancy, admin, "delete");

            await _vacancyRepository.AddVacancyArchive(vacancyArchive);

            #endregion


            #region Delete Vacancy

            await _vacancyRepository.Delete(vacancy);

            #endregion

            result.Success = true;
            result.Message = "The vacancy deleted successfully.";

            return result;
        }
    }
}