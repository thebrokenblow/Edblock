using Edblock.Library.Constants;

namespace Edblock.Library.Options;

public class IdentityServerApiOptions
{
    public const string SectionName = nameof(IdentityServerApiOptions);
    public required string ClientId { get; set; }
    public required string ClientSecret { get; set; }
    public required string Scope { get; set; }
    public required string GrantType { get; set; }
}