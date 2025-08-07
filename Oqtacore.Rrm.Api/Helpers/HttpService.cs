using Oqtacore.Rrm.Application.Commands;

namespace Oqtacore.Rrm.Api.Helpers;

public class HttpService : IHttpService
{
    public HttpService(IHttpContextAccessor httpContextAccessor)
    {
        if (httpContextAccessor.HttpContext.User.Identity.IsAuthenticated) 
        {
            var emailClaim = httpContextAccessor.HttpContext.User.Claims.FirstOrDefault();
            var userName = httpContextAccessor.HttpContext.User.Identity.Name;

            LogonUserEmail = emailClaim.Value;
            //LogonUserId = userIdClaim != null ? int.Parse(userIdClaim.Value) : 0;
            //ClientId = clientClaim?.Value;

            Method = httpContextAccessor.HttpContext.Request.Method;
            IPAddress = httpContextAccessor.HttpContext.Request.Headers["Origin"].ToString();
            UserAgent = httpContextAccessor.HttpContext.Request.Headers["User-Agent"].ToString();
        }
    }
    public List<int> LogonUserRoles { get; private set; } = new();
    public int LogonUserId { get; private set; }
    public string LogonUserEmail { get; private set; }
    public string ClientId { get; private set; }
    public string Method { get; private set; }
    public string IPAddress { get; private set; }
    public string UserAgent { get; private set; }
}
