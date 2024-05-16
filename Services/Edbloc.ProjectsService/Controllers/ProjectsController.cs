using System.Text.Json;
using Edblock.ProjectsServiceLibrary.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Edblock.ProjectsService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ProjectsController : ControllerBase
{
    [HttpGet(RepoAction.GetAll)]
    public async Task<List<Project>> Get(string key)
    {
        var database = RedisHelper.RedisDatabase;
        var projects = new List<Project>();
        var prejectsRedisValue = await database.ListRangeAsync(key);
        
        foreach (var prejectRedisValue in prejectsRedisValue) 
        {
            var project = JsonSerializer.Deserialize<Project>(prejectRedisValue) ?? throw new Exception("Не удалось десериализовать проект");
            projects.Add(project);
        }

        return projects;
    }

    [HttpPost(RepoAction.Add)]
    public async Task Add(string key, Project project)
    {
        var database = RedisHelper.RedisDatabase;
        var projectRedis = JsonSerializer.Serialize(project);
        await database.ListRightPushAsync(key, projectRedis);
    }

    [HttpPut(RepoAction.Update)]
    public async Task Update(string key, int index, Project project)
    {
        var database = RedisHelper.RedisDatabase;

        var projectForChangesRedis = await database.ListGetByIndexAsync(key, index);

        var projectForChanges = JsonSerializer.Deserialize<Project>(projectForChangesRedis) ?? throw new Exception("Не удалось десериализовать проект");

        projectForChanges.Name = project.Name;
        projectForChanges.Description = project.Description;
        projectForChanges.Content = project.Content;

        var resultProjectRedis = JsonSerializer.Serialize(projectForChanges);
        await database.ListSetByIndexAsync(key, index, resultProjectRedis);
    }

    [HttpDelete(RepoAction.Remove)]
    public async Task Delete(string key, int index)
    {
        var database = RedisHelper.RedisDatabase;
        var project = await database.ListGetByIndexAsync(key, index);
        await database.ListRemoveAsync(key, project, 1);
    }
}