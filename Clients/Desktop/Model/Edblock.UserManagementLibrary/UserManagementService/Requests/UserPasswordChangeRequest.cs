namespace Edblock.UserManagementLibrary.UserManagementService.Requests;

public class UserPasswordChangeRequest
{
    public required string UserName { get; set; }
    public required string CurrentPassword { get; set; }
    public required string NewPassword { get; set; }
}