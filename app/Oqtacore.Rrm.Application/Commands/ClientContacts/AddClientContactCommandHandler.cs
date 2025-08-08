using Oqtacore.Rrm.Domain.Entity;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class AddClientContactCommandHandler : RequestHandler<AddClientContactCommand, AddClientContactResult>
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAdminRepository _adminRepository;
        public AddClientContactCommandHandler(IClientRepository clientRepository, IAdminRepository adminRepository, IRepository repository, IHttpService httpService) : base(repository, httpService)
        {
            _clientRepository = clientRepository;
            _adminRepository = adminRepository;
        }
        protected override async Task<AddClientContactResult> Execute(AddClientContactCommand request, CancellationToken cancellationToken)
        {
            var result = new AddClientContactResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var contactData = new ContactData
            {
                CreatedBy = admin.id,
            };

            await Repository.Add(contactData);

            var newClientContact = new ClientContact
            {
                ClientId = request.ClientId,
                ContactDataId = contactData.Id,
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber
            };

            await Repository.Add(newClientContact);

            result.Success = true;

            return result;
        }
    }
}
