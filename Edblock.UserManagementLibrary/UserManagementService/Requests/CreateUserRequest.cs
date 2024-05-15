using Edblock.Library.UserManagementService.Models;

namespace Edblock.UserManagementLibrary.UserManagementService.Requests;

public class CreateUserRequest
{
    public required ApplicationUser User { get; set; }
    public required string Password { get; set; }
}