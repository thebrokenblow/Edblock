using Edblock.Library.UserManagementService.Models;
using Edblock.UserManagementLibrary.Clients.IdentityServer;
using Edblock.UserManagementLibrary.Clients.UserManagementService;
using Edblock.UserManagementLibrary.IdentityServer;
using Edblock.UserManagementLibrary.Options;
using Edblock.UserManagementLibrary.UserManagementService.Requests;
using IdentityModel.Client;
using Microsoft.Extensions.Options;

namespace Edblock.UserManagementModel.Clients;

public class Authentication(IdentityServerClient identityServerClient, UsersClient usersClient, IOptions<IdentityServerApiOptions> options)
{
    private const string clientId = "external";
    private readonly IdentityServerApiOptions identityServerOptions = options.Value;

    public async Task<Token> GetToken() =>
        await identityServerClient.GetApiToken(identityServerOptions);

    public async Task SetToken()
    {
        var token = await GetToken();
        usersClient.HttpClient.SetBearerToken(token.AccessToken);
    }

    public async Task<UserModel> AuthenticateAccount(string login, string password)
    {
        var client = new HttpClient();
        var discoveryDocumentResponse = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

        if (discoveryDocumentResponse.IsError)
        {
            throw new Exception(discoveryDocumentResponse.Error);
        }

        var tokenResponse = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
        {
            Address = discoveryDocumentResponse.TokenEndpoint,
            ClientId = clientId,
            UserName = login,
            Password = password
        });

        if (tokenResponse.IsError)
        {
            throw new Exception(tokenResponse.Error);
        }

        await SetToken();

        var aplicationUser = await usersClient.GetUserByName(login);
        var user = new UserModel(aplicationUser.Id);
        return user;

    }
}