using Edblock.Library.Constants;
using Edblock.Library.Options;
using Edblock.Library.UserManagementService.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Edblock.Library.Clients.UserManagementService;

public class RolesClient(HttpClient httpClient, IOptions<ServiceAdressOptions> options) : UserManagementBaseClient(httpClient, options), IRolesClient
{
    public async Task<IdentityResult> Add(IdentityRole role)
        => await SendPostRequest(role, $"/{RolesControllerRoutes.ControllerName}/{RepositoryActions.Add}");

    public async Task<UserManagementServiceResponse<IdentityRole>> Get(string name)
        => await SendGetRequest<IdentityRole>($"{RolesControllerRoutes.ControllerName}?name={name}");

    public async Task<UserManagementServiceResponse<IEnumerable<IdentityRole>>> GetAll()
        => await SendGetRequest<IEnumerable<IdentityRole>>($"/{RolesControllerRoutes.ControllerName}/{RepositoryActions.GetAll}");

    public async Task<IdentityResult> Remove(IdentityRole role)
        => await SendPostRequest(role, $"/{RolesControllerRoutes.ControllerName}/{RepositoryActions.Remove}");

    public async Task<IdentityResult> Update(IdentityRole role)
        => await SendPostRequest(role, $"/{RolesControllerRoutes.ControllerName}/{RepositoryActions.Update}");
}