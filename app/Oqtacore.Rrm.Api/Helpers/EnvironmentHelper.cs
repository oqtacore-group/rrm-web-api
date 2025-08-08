using System.Text.Json;

namespace Oqtacore.Rrm.Api.Helpers
{
    public static class EnvironmentHelper
    {
        public static void LoadEnvironmentVariables()
        {
            var envFile = Path.Combine(Directory.GetCurrentDirectory(), ".env");
            if (File.Exists(envFile))
            {
                var lines = File.ReadAllLines(envFile);
                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                        continue;

                    var parts = line.Split('=', 2);
                    if (parts.Length == 2)
                    {
                        var key = parts[0].Trim();
                        var value = parts[1].Trim().Trim('"');
                        
                        if (!Environment.GetEnvironmentVariables().Contains(key))
                        {
                            Environment.SetEnvironmentVariable(key, value);
                        }
                    }
                }
            }
        }

        public static string GetEnvironmentVariable(string key, string defaultValue = "")
        {
            return Environment.GetEnvironmentVariable(key) ?? defaultValue;
        }

        public static T GetEnvironmentVariable<T>(string key, T defaultValue = default(T))
        {
            var value = Environment.GetEnvironmentVariable(key);
            if (string.IsNullOrEmpty(value))
                return defaultValue;

            try
            {
                if (typeof(T) == typeof(bool))
                    return (T)(object)bool.Parse(value);
                if (typeof(T) == typeof(int))
                    return (T)(object)int.Parse(value);
                if (typeof(T) == typeof(double))
                    return (T)(object)double.Parse(value);
                
                return (T)(object)value;
            }
            catch
            {
                return defaultValue;
            }
        }
    }
}
