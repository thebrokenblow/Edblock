using Edblock.Library.Clients.IdentityServer;
using Edblock.Library.Clients.UserManagementService;
using Edblock.Library.IdentityServer;
using Edblock.Library.Options;
using IdentityModel.Client;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Edblock.UserManagementService.ConsoleAppTest.Tests;

public class RoleUserTest(
    IdentityServerClient identityServerClient,
    RolesClient rolesClient,
    IOptions<IdentityServerApiOptions> options)
{

    private readonly IdentityServerApiOptions identityServerOptions = options.Value;

    public async Task<List<string>> RunTests(string[] args)
    {
        var nameRole = "ShopClient";
        var newNameRole = "Client";
        var erros = new List<string>();

        await SetToken();

        erros.AddRange(await AddRole(nameRole));

        Thread.Sleep(1000);

        var role = await GetRole(nameRole);

        Thread.Sleep(1000);

        erros.AddRange(await UpdateRole(newNameRole, role));

        Thread.Sleep(1000);

        await GetAllRoles();

        Thread.Sleep(1000);

        erros.AddRange(await RemoveRole(role.Name));

        return erros;
    }

    public async Task SetToken()
    {
        var token = await GetToken();
        rolesClient.HttpClient.SetBearerToken(token.AccessToken);
    }

    public async Task<Token> GetToken() =>
       await identityServerClient.GetApiToken(identityServerOptions);


    public async Task<List<string>> AddRole(string nameRole)
    {
        var addRoleResult = await rolesClient.Add(new IdentityRole(nameRole));

        Console.WriteLine($"ADD ROLE : {addRoleResult.Succeeded}");

        var errorRequest = new ErrorRequest(addRoleResult);

        return errorRequest.GetErrorRequest();
    }

    public async Task<IdentityRole> GetRole(string nameRole)
    {
        var role = await rolesClient.Get(nameRole) ?? throw new($"{nameRole} role is not found");

        Console.WriteLine($"GET ROLE BY NAME: {role.Code}");

        return role.Payload;
    }

    public async Task GetAllRoles()
    {
        var getAllRoles = await rolesClient.GetAll();

        Console.WriteLine($"GET ALL ROLES: {getAllRoles.Code}");
    }

    public async Task<List<string>> UpdateRole(string newNameRole, IdentityRole identityRole)
    {
        identityRole.Name = newNameRole;
        var updateRoleResult = await rolesClient.Update(identityRole);

        Console.WriteLine($"UPDATE ROLE: {updateRoleResult.Succeeded}");

        var errorRequest = new ErrorRequest(updateRoleResult);

        return errorRequest.GetErrorRequest();
    }

    public async Task<List<string>> RemoveRole(string nameRole)
    {
        var role = await rolesClient.Get(nameRole);
        var resultRemove = await rolesClient.Remove(role.Payload);

        Console.WriteLine($"DELETE ROLE: {resultRemove.Succeeded}");

        var errorRequest = new ErrorRequest(resultRemove);

        return errorRequest.GetErrorRequest();
    }
}