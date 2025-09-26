
namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class UpdateClientContactCommand : ICommand<UpdateClientContactResult>
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
    }
    public class UpdateClientContactResult : Result
    {
        public int Id { get; set; }
    }
}