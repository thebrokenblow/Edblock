namespace Edblock.ProjectsService;

public class Project
{
    public required string Name { get; set; }
    public string? Description { get; set; }
    public required string Content { get; set; }
}