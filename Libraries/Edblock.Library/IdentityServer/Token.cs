using Newtonsoft.Json;

namespace Edblock.Library.IdentityServer;

public class Token
{
    [JsonProperty("access_token")]
    public required string AccessToken { get; set; }

    [JsonProperty("expires_in")]
    public required int ExpiresIn { get; set; }

    [JsonProperty("token_type")]
    public required string TokenType { get; set; }

    [JsonProperty("scope")]
    public required string Scope { get; set; }
}