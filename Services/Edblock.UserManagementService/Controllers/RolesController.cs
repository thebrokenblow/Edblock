using Edblock.Library.Constants;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Edblock.UserManagementService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class RolesController(RoleManager<IdentityRole> roleManager) : ControllerBase
{
    [HttpPost(RepoActions.Add)]
    public Task<IdentityResult> Add(IdentityRole identityRole) =>
        roleManager.CreateAsync(identityRole);

    [HttpPost(RepoActions.Update)]
    public async Task<IdentityResult> Update(IdentityRole identityRole)
    {
        var roleToBeUpdated = await roleManager.FindByIdAsync(identityRole.Id);

        if (roleToBeUpdated is null)
        {
            return IdentityResult.Failed(
                new IdentityError()
                {
                    Description = $"Role {identityRole.Name} was not found."
                });
        }

        roleToBeUpdated.Name = identityRole.Name;

        var result = await roleManager.UpdateAsync(roleToBeUpdated);

        return result;
    }

    [HttpPost(RepoActions.Remove)]
    public Task<IdentityResult> Remove(IdentityRole identityRole) =>
        roleManager.DeleteAsync(identityRole);

    [HttpGet]
    public Task<IdentityRole?> Get(string name) =>
        roleManager.FindByNameAsync(name);

    [HttpGet(RepoActions.GetAll)]
    public IEnumerable<IdentityRole> Get() =>
        roleManager.Roles.AsEnumerable();
}