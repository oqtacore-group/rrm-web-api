
namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class UpdateClientCommand : ICommand<UpdateClientCommandResult>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? SiteUrl { get; set; }
    }
    public class UpdateClientCommandResult : Result
    {
    }
}
