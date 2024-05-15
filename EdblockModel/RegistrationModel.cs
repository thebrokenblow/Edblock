using Edblock.UserManagementLibrary.Clients;
using Edblock.UserManagementLibrary.Configurations;
using IdentityModel.Client;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.DependencyInjection;

namespace EdblockModel;

public class RegistrationModel
{
    public async Task<TokenResponse> Registration(string login, string password)
    {
        var serviceProvider = ConfigurationUser.GetServiceProvider();

        var registration = serviceProvider.GetRequiredService<Registration>();
        var tokenResponse = await registration.RegistrationAccount(login, password);

        return tokenResponse;
    }
}