using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class DeleteClientContactCommandHandler : RequestHandler<DeleteClientContactCommand, DeleteClientContactResult>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAdminRepository _adminRepository;
        public DeleteClientContactCommandHandler(IClientRepository clientRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
        }
        protected override async Task<DeleteClientContactResult> Execute(DeleteClientContactCommand request, CancellationToken cancellationToken)
        {
            var result = new DeleteClientContactResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var clientContact = await _clientRepository.GetContact(request.Id);
            if(clientContact == null)
            {
                result.Message = "Client contact not found.";
                return result;
            }

            await Repository.Delete(clientContact);

            result.Success = true;

            return result;
        }
    }
}