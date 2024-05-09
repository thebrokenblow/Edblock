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
    public Task<IdentityResult> Add(IdentityRole role)
    {
        var result = roleManager.CreateAsync(role);

        return result;
    }

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
    public Task<IdentityResult> Remove(IdentityRole role)
    {
        var result = roleManager.DeleteAsync(role);

        return result;
    }

    [HttpGet]
    public Task<IdentityRole?> Get(string name)
    {
        var result = roleManager.FindByNameAsync(name);

        return result;
    }

    [HttpGet(RepoActions.GetAll)]
    public IEnumerable<IdentityRole> Get()
    {
        var result = roleManager.Roles.AsEnumerable();

        return result;
    }
}