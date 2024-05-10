using Edblock.Library.UserManagementService.Models;
using Edblock.Library.UserManagementService.Requests;
using Edblock.Library.UserManagementService.Responses;
using Microsoft.AspNetCore.Identity;

namespace Edblock.Library.Clients.UserManagementService;

public interface IUsersClient
{
    Task<IdentityResult> Add(CreateUserRequest request);

    Task<IdentityResult> Update(ApplicationUser user);

    Task<IdentityResult> Remove(ApplicationUser user);

    Task<UserManagementServiceResponse<ApplicationUser>> Get(string name);

    Task<UserManagementServiceResponse<IEnumerable<ApplicationUser>>> GetAll();

    Task<IdentityResult> ChangePassword(UserPasswordChangeRequest request);

    Task<IdentityResult> AddToRole(AddRemoveRoleRequest request);

    Task<IdentityResult> AddToRoles(AddRemoveRolesRequest request);

    Task<IdentityResult> RemoveFromRole(AddRemoveRoleRequest request);

    Task<IdentityResult> RemoveFromRoles(AddRemoveRolesRequest request);
}