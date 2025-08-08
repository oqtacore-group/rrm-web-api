using Amazon;
using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;

namespace Oqtacore.Rrm.Api.Helpers;

public class SecretsManagerHelper
{
    private readonly IAmazonSecretsManager _secretsManager;

    public SecretsManagerHelper()
    {
        _secretsManager = new AmazonSecretsManagerClient(RegionEndpoint.EUCentral1);
    }

    public async Task<string> GetSecretValueAsync(string secretName)
    {
        try
        {
            var request = new GetSecretValueRequest
            {
                SecretId = secretName
            };
            var response = await _secretsManager.GetSecretValueAsync(request);
            return response.SecretString;
        }
        catch (Exception ex)
        {
            throw new Exception($"Error retrieving secret {secretName}: {ex.Message}", ex);
        }
    }
}