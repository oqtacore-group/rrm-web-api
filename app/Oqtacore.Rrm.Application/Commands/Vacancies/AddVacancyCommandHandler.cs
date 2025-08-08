using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Vacancies
{
    public class AddVacancyCommandHandler : RequestHandler<AddVacancyCommand, AddVacancyResult>
    {
        private readonly IAdminRepository _adminRepository;
        public AddVacancyCommandHandler(IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _adminRepository = adminRepository;
        }
        protected override async Task<AddVacancyResult> Execute(AddVacancyCommand request, CancellationToken cancellationToken)
        {
            var result = new AddVacancyResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var newVacancy = new Vacancy
            {
                Name = request.Name,
                ClientId = request.ClientId,
                WorkplaceNumber = request.WorkplaceNumber,
                //Status = request.Status,
                SalaryLowerEnd = request.SalaryLowerEnd,
                SalaryHighEnd = request.SalaryHighEnd,
                SalaryCurrency = request.SalaryCurrency,
                Experience = request.Experience,
                Location = request.Location,
                LocalTime = request.LocalTime,
                RelocationHelp = request.RelocationHelp,
                RemoteWorkPlace = request.RemoteWorkPlace,
                CreatedBy = admin.id,
                //Opened = request.Opened,
                Responsibility = request.Responsibility,
                Skills = request.Skills,
                PersonalQuality = request.PersonalQuality,
                Languages = request.Languages,
                Notes = request.Notes
            };

            await Repository.Add(newVacancy);

            result.Id = newVacancy.id;
            result.Success = true;
            result.Message = "Vacancy added successfully.";

            return result;
        }
    }
}