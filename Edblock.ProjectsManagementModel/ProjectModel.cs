using System.Text.Json.Serialization;

namespace Edblock.ProjectsManagementModel;

public class ProjectModel
{
    [JsonPropertyName("name")]
    public required string Name { get; set; }

    [JsonPropertyName("description")]
    public required string? Description { get; set; }

    [JsonPropertyName("content")]
    public required string Content { get; set; }
}