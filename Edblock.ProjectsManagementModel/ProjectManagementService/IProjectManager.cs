namespace Edblock.ProjectsManagementModel.ProjectManagementService;

public interface IProjectManager
{
    Task Add(string key, ProjectModel project);
    Task Update(string key, int index, ProjectModel project);
    Task Remove(string key, int index);
    Task<IEnumerable<ProjectModel>> GetAll(string key);
}