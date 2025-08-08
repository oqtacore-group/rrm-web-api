using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class UpdateClientContactCommandHandler : RequestHandler<UpdateClientContactCommand, UpdateClientContactResult>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAdminRepository _adminRepository;
        public UpdateClientContactCommandHandler(IClientRepository clientRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
        }
        protected override async Task<UpdateClientContactResult> Execute(UpdateClientContactCommand request, CancellationToken cancellationToken)
        {
            var result = new UpdateClientContactResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var clientContact = await _clientRepository.GetContact(request.Id);
            if(clientContact == null)
            {
                result.Message = "Client contact not found.";
                return result;
            }
            clientContact.ClientId = request.ClientId;
            clientContact.Name = request.Name;
            clientContact.Email = request.Email;
            clientContact.PhoneNumber = request.PhoneNumber;

            await Repository.Update(clientContact);

            result.Success = true;

            return result;
        }
    }
}