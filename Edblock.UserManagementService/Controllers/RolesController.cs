using Edblock.Library.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Edblock.UserManagementService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class RolesController(RoleManager<IdentityRole> roleManager) : ControllerBase
{
    [HttpPost(RepoActions.Add)]
    public Task<IdentityResult> Add(IdentityRole role) =>
        roleManager.CreateAsync(role);

    [HttpPost(RepoActions.Update)]
    public async Task<IdentityResult> Update(IdentityRole role)
    {
        var roleToBeUpdated = await roleManager.FindByIdAsync(role.Id);

        if (roleToBeUpdated is null)
        {
            return IdentityResult.Failed(
                new IdentityError()
                {
                    Description = $"Role {role.Name} was not found."
                });
        }

        roleToBeUpdated.Name = role.Name;

        var result = await roleManager.UpdateAsync(roleToBeUpdated);

        return result;
    }

    [HttpPost(RepoActions.Remove)]
    public Task<IdentityResult> Remove(IdentityRole role) =>
        roleManager.DeleteAsync(role);

    [HttpGet]
    public Task<IdentityRole?> Get(string name) =>
        roleManager.FindByNameAsync(name);

    [HttpGet(RepoActions.GetAll)]
    public IEnumerable<IdentityRole> Get() =>
        roleManager.Roles.AsEnumerable();
}