using Edblock.Library.IdentityServer;
using Edblock.Library.Options;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace Edblock.Library.Clients.IdentityServer;

public class IdentityServerClient : IIdentityServerClient
{
    public IdentityServerClient(HttpClient client, IOptions<ServiceAdressOptions> options)
    {
        HttpClient = client;
        HttpClient.BaseAddress = new Uri(options.Value.IdentityServer);
    }

    public HttpClient HttpClient { get; init; }

    public async Task<Token> GetApiToken(IdentityServerApiOptions options)
    {
        var keyValues = new List<KeyValuePair<string, string>>
            {
                new("scope", options.Scope),
                new("client_secret", options.ClientSecret),
                new("grant_type", options.GrantType),
                new("client_id", options.ClientId)
            };

        var content = new FormUrlEncodedContent(keyValues);
        var response = await HttpClient.PostAsync("/connect/token", content);
        var responseContent = await response.Content.ReadAsStringAsync();

        var token = JsonConvert.DeserializeObject<Token>(responseContent);
        return token;
    }
}