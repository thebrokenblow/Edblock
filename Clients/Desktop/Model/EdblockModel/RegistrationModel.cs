using Edblock.UserManagementLibrary.Clients;
using Edblock.UserManagementLibrary.Configurations;
using Edblock.UserManagementModel.Clients;
using Microsoft.Extensions.DependencyInjection;

namespace EdblockModel;

public class RegistrationModel
{
    public async Task<UserModel> Registration(string login, string password)
    {
        var serviceProvider = ConfigurationUser.GetServiceProvider();

        var registration = serviceProvider.GetRequiredService<Registration>();
        var userModel = await registration.RegistrationAccount(login, password);

        return userModel;
    }
}