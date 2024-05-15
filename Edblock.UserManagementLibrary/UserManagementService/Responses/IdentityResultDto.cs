using Microsoft.AspNetCore.Identity;
using System.Text.Json.Serialization;

namespace Edblock.UserManagementLibrary.UserManagementService.Responses;

public class IdentityResultDto
{
    [JsonPropertyName("succeeded")]
    public required bool Succeeded { get; set; }

    [JsonPropertyName("errors")]
    public required IEnumerable<IdentityError> Errors { get; set; }
}