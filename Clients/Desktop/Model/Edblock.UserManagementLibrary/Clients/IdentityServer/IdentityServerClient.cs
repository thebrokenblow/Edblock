using System.Text.Json;
using Microsoft.Extensions.Options;
using Edblock.UserManagementLibrary.Options;
using Edblock.UserManagementLibrary.IdentityServer;

namespace Edblock.UserManagementLibrary.Clients.IdentityServer;

public class IdentityServerClient : IIdentityServerClient
{
    public HttpClient HttpClient { get; }

    public IdentityServerClient(HttpClient client, IOptions<ServiceAdressOptions> options)
    {
        HttpClient = client;
        HttpClient.BaseAddress = new Uri(options.Value.IdentityServer);
    }

    public async Task<Token> GetApiToken(IdentityServerApiOptions options)
    {
        var formatEncodedContent = new Dictionary<string, string>
        {
            { "scope", options.Scope },
            { "client_secret", options.ClientSecret },
            { "grant_type", options.GrantType },
            { "client_id", options.ClientId }
        };

        var content = new FormUrlEncodedContent(formatEncodedContent);
        var response = await HttpClient.PostAsync("/connect/token", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        var token = JsonSerializer.Deserialize<Token>(responseContent);

        return token is null ? throw new NullReferenceException("Token is null") : token;
    }
}