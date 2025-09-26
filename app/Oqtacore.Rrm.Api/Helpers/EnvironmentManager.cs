namespace Oqtacore.Rrm.Api.Helpers;

public static class EnvironmentManager
{
    public static string GetEnvironmentName(IWebHostEnvironment environment)
    {
        var env = environment.EnvironmentName;
        switch (env)
        {
            case "Local":
                return "hes";
            case "Development":
                return "dev";
            case "Staging":
                return "staging";
            case "Production":
                return "prod";
            default:
                return "hes";
        }
    }
}