namespace Edblock.Library.UserManagementService.Requests;

public class AddRemoveRolesRequest
{
    public required string UserName { get; set; }
    public required string[] RoleNames { get; set; }
}