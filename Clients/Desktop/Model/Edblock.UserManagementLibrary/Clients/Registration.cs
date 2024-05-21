using Edblock.Library.UserManagementService.Models;
using Edblock.UserManagementLibrary.Clients.IdentityServer;
using Edblock.UserManagementLibrary.Clients.UserManagementService;
using Edblock.UserManagementLibrary.IdentityServer;
using Edblock.UserManagementLibrary.Options;
using Edblock.UserManagementLibrary.UserManagementService.Requests;
using Edblock.UserManagementModel.Clients;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Edblock.UserManagementLibrary.Clients;

public class Registration(IdentityServerClient identityServerClient, UsersClient usersClient, IOptions<IdentityServerApiOptions> options)
{
    private readonly IdentityServerApiOptions identityServerOptions = options.Value;

    public async Task<Token> GetToken() =>
        await identityServerClient.GetApiToken(identityServerOptions);

    public async Task SetToken()
    {
        var token = await GetToken();
        usersClient.HttpClient.SetBearerToken(token.AccessToken);
    }

    public async Task<UserModel> RegistrationAccount(string userName, string passwordUser)
    {
        await SetToken();

        var addUserResult = await usersClient.Add(
             new CreateUserRequest()
             {
                 User = new ApplicationUser()
                 {
                     UserName = userName
                 },
                 Password = passwordUser
             });


        if (!addUserResult.Succeeded)
        {
            throw new Exception("Не удалось создать пользователя");
        }

        var aplicationUser = await usersClient.GetUserByName(userName);
        var user = new UserModel(aplicationUser.Id);
        return user;

    }
}