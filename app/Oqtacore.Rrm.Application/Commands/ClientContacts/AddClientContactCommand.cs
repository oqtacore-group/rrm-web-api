using MediatR;

namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class AddClientContactCommand : IRequest<AddClientContactResult>
    {
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class AddClientContactResult : Result
    {
        public int Id { get; set; }
    }
}