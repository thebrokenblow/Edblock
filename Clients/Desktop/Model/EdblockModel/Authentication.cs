using IdentityModel.Client;

namespace EdblockModel;

public class Authentication : IAuthentication
{
    private const string clientId = "external";

    public async Task<TokenResponse> Authenticate(string login, string password)
    {
        var client = new HttpClient();
        var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync("https://localhost:5001");
        
        if (discoveryDocumentResponse.IsError)
        {
            throw new Exception(discoveryDocumentResponse.Error);
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = discoveryDocumentResponse.TokenEndpoint,
            ClientId = clientId,
            UserName = login,
            Password = password
        });

        if (tokenResponse.IsError)
        {
            throw new Exception(tokenResponse.Error);
        }

        return tokenResponse;
    }
}
