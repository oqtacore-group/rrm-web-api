using Oqtacore.Rrm.Domain.Models;
using Oqtacore.Rrm.Domain.Exceptions;
using Oqtacore.Rrm.Domain.Repository;

namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class AddClientCommandHandler : RequestHandler<AddClientCommand, AddClientResult>
    {
        private readonly IAdminRepository _adminRepository;
        public AddClientCommandHandler(IRepository repository, IAdminRepository adminRepository, IHttpService httpService) : base(repository, httpService)
        {
            _adminRepository = adminRepository;
        }
        protected override async Task<AddClientResult> Execute(AddClientCommand request, CancellationToken cancellationToken)
        {
            var result = new AddClientResult();

            var admin = await _adminRepository.GetByEmail(HttpService.LogonUserEmail);
            if (admin == null)
                throw new AuthorizationFailException("Authorization Failed.");

            var newClient = new Client
            {
                Name = request.Name,
                Email = request.Email,
                PhoneNumber = request.PhoneNumber,
                SiteUrl = request.SiteUrl,
                Created = DateTime.UtcNow,
                CreatedBy = admin.id,
            };

            await Repository.Add(newClient);

            result.Id = newClient.id;
            result.Success = true;
            result.Message = "The client has been created successfully.";

            return result;
        }
    }
}