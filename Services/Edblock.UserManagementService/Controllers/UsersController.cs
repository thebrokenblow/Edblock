using Edblock.Library.Constants;
using Edblock.Library.UserManagementService.Models;
using Edblock.Library.UserManagementService.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Edblock.UserManagementService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class UsersController(UserManager<ApplicationUser> userManager) : ControllerBase
{
    [HttpPost(RepoActions.Add)]
    public async Task<IdentityResult> Add(CreateUserRequest request) =>
        await userManager.CreateAsync(request.User, request.Password);


    [HttpPost(RepoActions.Update)]
    public async Task<IdentityResult> Update(ApplicationUser user)
    {
        if (user.UserName is null)
        {
            return IdentityResult.Failed(
                new IdentityError()
                {
                    Description = $"User is null"
                });
        }

        var userToBeUpdated = await userManager.FindByNameAsync(user.UserName);

        if (userToBeUpdated is null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Description = $"User {user.UserName} was not found."
            });
        }

        userToBeUpdated.PhoneNumber = user.PhoneNumber;
        userToBeUpdated.Email = user.Email;

        var result = await userManager.UpdateAsync(userToBeUpdated);

        return result;
    }

    [HttpPost(RepoActions.Remove)]
    public async Task<IdentityResult> Remove(ApplicationUser user) =>
        await userManager.DeleteAsync(user);

    [HttpGet]
    public async Task<ApplicationUser?> Get(string name) =>
        await userManager.FindByNameAsync(name);

    [HttpGet(RepoActions.GetAll)]
    public IEnumerable<ApplicationUser> Get() =>
        userManager.Users.AsEnumerable();

    [HttpPost(UsersControllerRoutes.ChangePassword)]
    public async Task<IdentityResult> ChangePassword(UserPasswordChangeRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Description = $"User {request.UserName} was not found."
            });
        }

        var result = await userManager.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword);

        return result;
    }

    [HttpPost(UsersControllerRoutes.AddToRole)]
    public async Task<IdentityResult> AddToRole(AddRemoveRoleRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Description = $"User {request.UserName} was not found."
            });
        }

        var result = await userManager.AddToRoleAsync(user, request.RoleName);

        return result;
    }

    [HttpPost(UsersControllerRoutes.AddToRoles)]
    public async Task<IdentityResult> AddToRoles(AddRemoveRolesRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Description = $"User {request.UserName} was not found."
            });
        }

        var result = await userManager.AddToRolesAsync(user, request.RoleNames);

        return result;
    }

    [HttpPost(UsersControllerRoutes.RemoveFromRole)]
    public async Task<IdentityResult> RemoveFromRole(AddRemoveRoleRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user == null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Description = $"User {request.UserName} was not found."
            });
        }

        var result = await userManager.RemoveFromRoleAsync(user, request.RoleName);

        return result;
    }

    [HttpPost(UsersControllerRoutes.RemoveFromRoles)]
    public async Task<IdentityResult> RemoveFromRoles(AddRemoveRolesRequest request)
    {
        var user = await userManager.FindByNameAsync(request.UserName);

        if (user is null)
        {
            return IdentityResult.Failed(new IdentityError()
            {
                Description = $"User {request.UserName} was not found."
            });
        }

        var result = await userManager.RemoveFromRolesAsync(user, request.RoleNames);

        return result;
    }
}