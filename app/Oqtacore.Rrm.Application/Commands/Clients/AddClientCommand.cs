using MediatR;

namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class AddClientCommand : IRequest<AddClientResult>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SiteUrl { get; set; }
    }
    public class AddClientResult : Result
    {
        public int Id { get; set; }
    }
}