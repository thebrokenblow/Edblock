using Edblock.UserManagementModel.Clients;
using Edblock.UserManagementLibrary.Configurations;
using Microsoft.Extensions.DependencyInjection;

namespace EdblockModel;

public class AuthenticationModel : IAuthentication
{
    public async Task<UserModel> AuthenticateAccount(string login, string password)
    {
        var serviceProvider = ConfigurationUser.GetServiceProvider();

        var registration = serviceProvider.GetRequiredService<Authentication>();
        var userModel = await registration.AuthenticateAccount(login, password);

        return userModel;
    }
}