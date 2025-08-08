using Oqtacore.Rrm.Domain.Entity;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;
using Oqtacore.Rrm.Infrastructure.Repository;

namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class DeleteClientCommandHandler : RequestHandler<DeleteClientCommand, DeleteClientCommandResult>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IVacancyRepository _vacancyRepository;
        private readonly IAdminRepository _adminRepository;
        public DeleteClientCommandHandler(IClientRepository clientRepository, IAdminRepository adminRepository, IVacancyRepository vacancyRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
            _vacancyRepository = vacancyRepository;
        }
        protected async override Task<DeleteClientCommandResult> Execute(DeleteClientCommand request, CancellationToken cancellationToken)
        {
            var result = new DeleteClientCommandResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            #region 1. get client and validate existance

            var client = await _clientRepository.Get(request.Id);
            if (client == null)
            {
                result.Message = "The client not found.";
                return result;
            }

            #endregion

            #region 2. Remove vacancies

            var vacancies = await _vacancyRepository.GetByClientId(client.id);

            await _vacancyRepository.DeleteRange(vacancies);

            #endregion

            #region 3. Remove Client contacts

            await _clientRepository.DeleteContacts(client.id);

            #endregion

            #region 4. Record archive client 

            var clientArchive = new ClientArchive
            {
                ActionBy = admin.id,
                ActionDate = DateTime.UtcNow,
                ActionType = "delete",
                Created = client.Created,
                CreatedBy = client.CreatedBy,
                Email = client.Email,
                id = client.id,
                Name = client.Name,
                PhoneNumber = client.PhoneNumber,
                SiteUrl = client.SiteUrl
            };

            await _clientRepository.AddClientArchive(clientArchive);

            #endregion

            #region 5. Delete client

            await _clientRepository.Delete(client);

            #endregion

            result.Success = true;
            result.Message = "The client has been deleted successfully.";

            return result;
        }
    }
}