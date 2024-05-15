using Microsoft.AspNetCore.Identity;

namespace Edblock.Library.UserManagementService.Responses;

public class IdentityResultDto
{
    public required bool Succeeded { get; set; }
    public required IEnumerable<IdentityError> Errors { get; set; }
}