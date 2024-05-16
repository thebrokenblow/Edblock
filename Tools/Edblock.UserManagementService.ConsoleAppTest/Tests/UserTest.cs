using Edblock.Library.Clients.IdentityServer;
using Edblock.Library.Clients.UserManagementService;
using Edblock.Library.IdentityServer;
using Edblock.Library.Options;
using Edblock.Library.UserManagementService.Models;
using Edblock.Library.UserManagementService.Requests;
using IdentityModel.Client;
using Microsoft.Extensions.Options;
using System.Net;

namespace Edblock.UserManagementService.ConsoleAppTest.Tests;

public class UserTest(
    IdentityServerClient identityServerClient,
    UsersClient usersClient,
    RoleUserTest roleUserTest,
    IOptions<IdentityServerApiOptions> options)
{
    private readonly IdentityServerApiOptions identityServerOptions = options.Value;

    public async Task<List<string>> RunTests(string[] args)
    {
        var userName = "the_brokenblow";
        var passwordUser = "pop_ZZZkek_123";
        var newPasswordUser = "pop_ZZZkek_12334";
        var userRole = "ShopClient";

        var errorsTests = new List<string>();

        await SetToken();

        await roleUserTest.SetToken();

        errorsTests.AddRange(await AddUser(userName, passwordUser));

        Thread.Sleep(1000);

        errorsTests.AddRange(await ChangePassword(userName, passwordUser, newPasswordUser));

        Thread.Sleep(1000);

        await GetUserByName(userName);

        Thread.Sleep(1000);

        await GetAllUser();

        Thread.Sleep(1000);

        errorsTests.AddRange(await AddRoleUser(userName, userRole));

        Thread.Sleep(1000);

        errorsTests.AddRange(await RemoveRoleUser(userName, userRole));

        Thread.Sleep(1000);

        errorsTests.AddRange(await roleUserTest.RemoveRole(userRole));

        Thread.Sleep(1000);

        errorsTests.AddRange(await RemoveUser(userName));

        return errorsTests;
    }

    public async Task<Token> GetToken() =>
        await identityServerClient.GetApiToken(identityServerOptions);

    public async Task SetToken()
    {
        var token = await GetToken();
        usersClient.HttpClient.SetBearerToken(token.AccessToken);
    }

    public async Task<List<string>> RemoveUser(string userName)
    {
        var user = await GetUserByName(userName);
        var deleteResult = await usersClient.Remove(user);

        Console.WriteLine($"DELETE: {deleteResult.Succeeded}");

        var errorRequest = new ErrorRequest(deleteResult);
        return errorRequest.GetErrorRequest();
    }

    public async Task<List<string>> AddUser(string userName, string passwordUser)
    {
        var addUserResult = await usersClient.Add(
            new CreateUserRequest()
            {
                User = new ApplicationUser()
                {
                    UserName = userName
                },
                Password = passwordUser
            });

        Console.WriteLine($"ADD USER {addUserResult.Succeeded}");

        var errorRequest = new ErrorRequest(addUserResult);

        return errorRequest.GetErrorRequest();
    }

    public async Task<List<string>> ChangePassword(string userName, string passwordUser, string newPasswordUser)
    {
        var changePasswordResult = await usersClient.ChangePassword(
            new UserPasswordChangeRequest()
            {
                UserName = userName,
                CurrentPassword = passwordUser,
                NewPassword = newPasswordUser
            });

        Console.WriteLine($"CHANGE PASSWORD USER {changePasswordResult.Succeeded}");

        var errorRequest = new ErrorRequest(changePasswordResult);

        return errorRequest.GetErrorRequest();
    }

    public async Task<ApplicationUser> GetUserByName(string userName)
    {
        var user = await usersClient.Get(userName);

        Console.WriteLine($"GET USER BY NAME: {user.Code}");

        return user.Payload;
    }

    public async Task GetAllUser()
    {
        var getAllResult = await usersClient.GetAll();

        Console.WriteLine($"GET ALL: {getAllResult.Code}");
    }

    public async Task<List<string>> AddRoleUser(string userName, string roleName)
    {
        await roleUserTest.AddRole(roleName);

        var addToRoleRequest = await usersClient.AddToRole(
            new AddRemoveRoleRequest()
            {
                UserName = userName,
                RoleName = roleName
            });

        Console.WriteLine($"ADD ROLE: {addToRoleRequest.Succeeded}");

        var errorRequest = new ErrorRequest(addToRoleRequest);

        return errorRequest.GetErrorRequest();
    }

    public async Task<List<string>> RemoveRoleUser(string userName, string userRole)
    {
        var addToRoleRequest = await usersClient.RemoveFromRole(
            new AddRemoveRoleRequest()
            {
                UserName = userName,
                RoleName = userRole
            });

        Console.WriteLine($"REMOVE ROLE: {addToRoleRequest.Succeeded}");

        var errorRequest = new ErrorRequest(addToRoleRequest);

        return errorRequest.GetErrorRequest();
    }
}