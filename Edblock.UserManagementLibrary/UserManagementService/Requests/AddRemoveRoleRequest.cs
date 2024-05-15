namespace Edblock.UserManagementLibrary.UserManagementService.Requests;

public class AddRemoveRoleRequest
{
    public required string UserName { get; set; }
    public required string RoleName { get; set; }
}