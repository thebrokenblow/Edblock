using Edblock.Library.Constants;
using Edblock.Library.Options;
using Edblock.Library.UserManagementService.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Edblock.Library.Clients.UserManagementService;

public class RolesClient : UserManagementBaseClient, IRolesClient
{
    public RolesClient(HttpClient httpClient, IOptions<ServiceAdressOptions> options) : base(httpClient, options) { }

    public async Task<IdentityResult> Add(IdentityRole role)
        => await SendPostRequest(role, $"/{RolesControllerRoutes.ControllerName}/{RepoActions.Add}");

    public async Task<UserManagementServiceResponse<IdentityRole>> Get(string name)
        => await SendGetRequest<IdentityRole>($"{RolesControllerRoutes.ControllerName}?name={name}");

    public async Task<UserManagementServiceResponse<IEnumerable<IdentityRole>>> GetAll()
        => await SendGetRequest<IEnumerable<IdentityRole>>($"/{RolesControllerRoutes.ControllerName}/{RepoActions.GetAll}");

    public async Task<IdentityResult> Remove(IdentityRole role)
        => await SendPostRequest(role, $"/{RolesControllerRoutes.ControllerName}/{RepoActions.Remove}");

    public async Task<IdentityResult> Update(IdentityRole role)
        => await SendPostRequest(role, $"/{RolesControllerRoutes.ControllerName}/{RepoActions.Update}");
}