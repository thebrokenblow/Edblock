
using Edblock.Library.Clients.ProjectsManagementService;
using Edblock.Library.Constants;
using Edblock.ProjectsManagementModel.Options;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Edblock.ProjectsManagementModel.ProjectManagementService;

public class ProjectManager : IProjectManager
{
    public HttpClient HttpClient { get; set; }

    public ProjectManager(HttpClient client, IOptions<ServiceAdressOptions> options)
    {
        HttpClient = client;
        HttpClient.BaseAddress = new Uri(options.Value.ProjectsManagementService);
    }

    public async Task Add(string key, ProjectModel project)
    {
        var request = JsonSerializer.Serialize(project);
        var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
        var requestResult = await HttpClient.PostAsync($"{ProjectsControllerRoutes.ControllerName}/{RepositoryActions.Add}?key={key}", httpContent);

        if (!requestResult.IsSuccessStatusCode)
        {
            throw new Exception("");
        }
    }

    public async Task<IEnumerable<ProjectModel>> GetAll(string key)
    {
        var requestResult = await HttpClient.GetAsync($"{ProjectsControllerRoutes.ControllerName}/{RepositoryActions.GetAll}?key={key}");

        if (requestResult.IsSuccessStatusCode)
        {
            var responseJson = await requestResult.Content.ReadAsStringAsync();
            var applicationUser = JsonSerializer.Deserialize<IEnumerable<ProjectModel>>(responseJson) ??
                throw new NullReferenceException("Response is null");

            return applicationUser;
        }

        throw new Exception("Не удалось получить пользователя");
    }

    public async Task Remove(string key, int index)
    {        
        var requestResult = await HttpClient.DeleteAsync($"{ProjectsControllerRoutes.ControllerName}/{RepositoryActions.Remove}?key={key}&index={index}");

        if (!requestResult.IsSuccessStatusCode)
        {
            throw new Exception("");
        }
    }

    public async Task Update(string key, int index, ProjectModel project)
    {
        var request = JsonSerializer.Serialize(project);
        var httpContent = new StringContent(request, Encoding.UTF8, "application/json");
        var requestResult = await HttpClient.PutAsync($"{ProjectsControllerRoutes.ControllerName}/{RepositoryActions.Update}?key={key}&index={index}", httpContent);

        if (!requestResult.IsSuccessStatusCode)
        {
            throw new Exception("");
        }
    }
}