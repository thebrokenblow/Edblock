using Edblock.UserManagementLibrary.IdentityServer;
using Edblock.UserManagementLibrary.Options;

namespace Edblock.UserManagementLibrary.Clients.IdentityServer;

public interface IIdentityServerClient
{
    Task<Token> GetApiToken(IdentityServerApiOptions options);
}