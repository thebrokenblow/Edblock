using Edblock.IdentityServer.Models;

namespace Edblock.Library.UserManagementService.Requests;

public class CreateUserRequest
{
    public required ApplicationUser User { get; set; }
    public required string Password { get; set; }
}