using Edblock.Library.UserManagementService.Models;
using Edblock.UserManagementLibrary.Constants;
using Edblock.UserManagementLibrary.Options;
using Edblock.UserManagementLibrary.UserManagementService.Requests;
using Edblock.UserManagementLibrary.UserManagementService.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Edblock.UserManagementLibrary.Clients.UserManagementService;

public class UsersClient(HttpClient httpClient, IOptions<ServiceAdressOptions> options) : UserManagementBaseClient(httpClient, options), IUsersClient
{
    public async Task<IdentityResult> Add(CreateUserRequest request) => 
        await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{RepoActions.Add}");

    public async Task<ApplicationUser> GetUserByName(string name) =>
       await SendGetRequest($"/{UsersControllerRoutes.ControllerName}/{RepoActions.GetByName}?name={name}");

    public async Task<IdentityResult> ChangePassword(UserPasswordChangeRequest request) => 
        await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.ChangePassword}");

    public async Task<IdentityResult> AddToRole(AddRemoveRoleRequest request) => 
        await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.AddToRole}");

    public async Task<IdentityResult> AddToRoles(AddRemoveRolesRequest request) => 
        await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.AddToRoles}");

    public async Task<IdentityResult> RemoveFromRole(AddRemoveRoleRequest request) => 
        await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.RemoveFromRole}");

    public async Task<IdentityResult> RemoveFromRoles(AddRemoveRolesRequest request) => 
        await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.RemoveFromRoles}");

    public async Task<UserManagementServiceResponse<ApplicationUser>> Get(string name) => 
        await SendGetRequest<ApplicationUser>($"{UsersControllerRoutes.ControllerName}?name={name}");

    public async Task<UserManagementServiceResponse<IEnumerable<ApplicationUser>>> GetAll() => 
        await SendGetRequest<IEnumerable<ApplicationUser>>($"/{UsersControllerRoutes.ControllerName}/{RepoActions.GetAll}");

    public async Task<IdentityResult> Remove(ApplicationUser user) => 
        await SendPostRequest(user, $"/{UsersControllerRoutes.ControllerName}/{RepoActions.Remove}");

    public async Task<IdentityResult> Update(ApplicationUser user) => 
        await SendPostRequest(user, $"/{UsersControllerRoutes.ControllerName}/{RepoActions.Update}");
}