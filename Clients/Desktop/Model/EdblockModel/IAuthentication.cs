using IdentityModel.Client;

namespace EdblockModel;

public interface IAuthentication
{
    Task<TokenResponse> Authenticate(string login, string password);
}
