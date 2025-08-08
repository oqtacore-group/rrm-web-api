
namespace Oqtacore.Rrm.Application.Commands.Clients
{
    public class DeleteClientCommand : ICommand<DeleteClientCommandResult>
    {
        public int Id { get; set; }
    }
    public class DeleteClientCommandResult : Result
    {
    }
}