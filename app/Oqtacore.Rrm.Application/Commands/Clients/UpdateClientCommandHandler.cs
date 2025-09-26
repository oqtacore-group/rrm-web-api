using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class UpdateClientCommandHandler : RequestHandler<UpdateClientCommand, UpdateClientCommandResult>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAdminRepository _adminRepository;
        public UpdateClientCommandHandler(IClientRepository clientRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
        }
        protected async override Task<UpdateClientCommandResult> Execute(UpdateClientCommand request, CancellationToken cancellationToken)
        {
            var result = new UpdateClientCommandResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var client = await _clientRepository.Get(request.Id);
            if(client == null)
            {
                result.Message = "The client not found.";
                return result;
            }

            #region 1. Update Client

            client.Name = request.Name;
            client.Email = request.Email;
            client.PhoneNumber = request.PhoneNumber;
            client.SiteUrl = request.SiteUrl;

            await Repository.Update(client);

            #endregion

            #region 2. Add Audit Log

            var clientArchive = new ClientArchive
            {
                ActionBy = admin.id,
                ActionDate = DateTime.UtcNow,
                ActionType = "edit",
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

            result.Success = true;
            result.Message = "The client has been updated successfully.";

            return result;
        }
    }
}
