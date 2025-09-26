namespace Oqtacore.Rrm.Api.Middleware;

public class UserLoggingMiddleware
{
    private readonly RequestDelegate _next;

    public UserLoggingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var user = context.User;
        if (user.Identity != null && user.Identity.IsAuthenticated)
        {
            var emailClaim = user.Claims.FirstOrDefault();

            var logonUserEmail = emailClaim.Value;

            NLog.MappedDiagnosticsLogicalContext.Set("logonUserEmail", logonUserEmail);
        }

        try
        {
            await _next(context);
        }
        finally
        {
            // Clear the context after the request is complete
            NLog.MappedDiagnosticsLogicalContext.Clear();
        }
    }
}