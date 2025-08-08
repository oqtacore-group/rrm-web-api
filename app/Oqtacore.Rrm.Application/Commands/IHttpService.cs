
namespace Oqtacore.Rrm.Application.Commands
{
    public interface IHttpService
    {
        int LogonUserId { get; }
        string LogonUserEmail { get; }
        string ClientId { get; }
        string Method { get; }
        string IPAddress { get; }
        string UserAgent { get; }
        List<int> LogonUserRoles { get; }
    }
}
