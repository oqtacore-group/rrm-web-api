
namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class DeleteClientContactCommand : ICommand<DeleteClientContactResult>
    {
        public int Id { get; set; }
    }
    public class DeleteClientContactResult : Result
    {
    }
}