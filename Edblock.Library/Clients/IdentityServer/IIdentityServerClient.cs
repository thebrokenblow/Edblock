using Edblock.Library.IdentityServer;
using Edblock.Library.Options;

namespace Edblock.Library.Clients.IdentityServer;

public interface IIdentityServerClient
{
    Task<Token> GetApiToken(IdentityServerApiOptions options);
}