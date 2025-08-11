using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Vacancies
{
    public class UpdateVacancyCommandHandler : RequestHandler<UpdateVacancyCommand, UpdateVacancyResult>
    {
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IAdminRepository _adminRepository;
        public UpdateVacancyCommandHandler(IVacancyRepository vacancyRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _vacancyRepository = vacancyRepository;
            _adminRepository = adminRepository;
        }
        protected override async Task<UpdateVacancyResult> Execute(UpdateVacancyCommand request, CancellationToken cancellationToken)
        {
            var result = new UpdateVacancyResult();

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

            var vacancyArchive = new VacancyArchive(vacancy, admin, "edit");
            
            await Repository.Add(vacancyArchive);

            #endregion

            vacancy.Name = request.Name;
            vacancy.ClientId = request.ClientId;
            vacancy.WorkplaceNumber = request.WorkplaceNumber;
            //Status = request.Status,
            vacancy.SalaryLowerEnd = request.SalaryLowerEnd;
            vacancy.SalaryHighEnd = request.SalaryHighEnd;
            vacancy.SalaryCurrency = request.SalaryCurrency;
            vacancy.Experience = request.Experience;
            vacancy.Location = request.Location;
            vacancy.LocalTime = request.LocalTime;
            vacancy.RelocationHelp = request.RelocationHelp;
            vacancy.RemoteWorkPlace = request.RemoteWorkPlace;
                //Opened = request.Opened,
            vacancy.Responsibility = request.Responsibility;
            vacancy.Skills = request.Skills;
            vacancy.PersonalQuality = request.PersonalQuality;
            vacancy.Languages = request.Languages;
            vacancy.Notes = request.Notes;

            await Repository.Update(vacancy);

            result.Success = true;
            result.Message = "The vacancy updated successfully.";

            return result;
        }
    }
}
