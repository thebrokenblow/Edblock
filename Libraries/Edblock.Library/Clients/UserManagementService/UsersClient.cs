﻿using Edblock.Library.Constants;
using Edblock.Library.Options;
using Edblock.Library.UserManagementService.Models;
using Edblock.Library.UserManagementService.Requests;
using Edblock.Library.UserManagementService.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace Edblock.Library.Clients.UserManagementService;

public class UsersClient(HttpClient httpClient, IOptions<ServiceAdressOptions> options) : UserManagementBaseClient(httpClient, options), IUsersClient
{
    public async Task<IdentityResult> Add(CreateUserRequest request)
        => await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{RepositoryActions.Add}");

    public async Task<IdentityResult> ChangePassword(UserPasswordChangeRequest request)
        => await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.ChangePassword}");

    public async Task<IdentityResult> AddToRole(AddRemoveRoleRequest request)
        => await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.AddToRole}");

    public async Task<IdentityResult> AddToRoles(AddRemoveRolesRequest request)
        => await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.AddToRoles}");

    public async Task<IdentityResult> RemoveFromRole(AddRemoveRoleRequest request)
        => await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.RemoveFromRole}");

    public async Task<IdentityResult> RemoveFromRoles(AddRemoveRolesRequest request)
        => await SendPostRequest(request, $"/{UsersControllerRoutes.ControllerName}/{UsersControllerRoutes.RemoveFromRoles}");

    public async Task<UserManagementServiceResponse<ApplicationUser>> Get(string name)
        => await SendGetRequest<ApplicationUser>($"{UsersControllerRoutes.ControllerName}?name={name}");

    public async Task<UserManagementServiceResponse<IEnumerable<ApplicationUser>>> GetAll()
        => await SendGetRequest<IEnumerable<ApplicationUser>>($"/{UsersControllerRoutes.ControllerName}/{RepositoryActions.GetAll}");

    public async Task<IdentityResult> Remove(ApplicationUser user)
        => await SendPostRequest(user, $"/{UsersControllerRoutes.ControllerName}/{RepositoryActions.Remove}");

    public async Task<IdentityResult> Update(ApplicationUser user)
        => await SendPostRequest(user, $"/{UsersControllerRoutes.ControllerName}/{RepositoryActions.Update}");
}