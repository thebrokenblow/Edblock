using System.Text.Json;
using Edblock.ProjectsServiceLibrary.Constants;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace Edblock.ProjectsService.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class ProjectsController(IDatabase database) : ControllerBase
{
    [HttpGet(RepoActionProjectsService.GetAll)]
    public async Task<ActionResult<List<Project>>> Get(string key)
    {
        try
        {
            var projects = new List<Project>();
            var prejectsRedisValue = await database.ListRangeAsync(key);

            foreach (var prejectRedisValue in prejectsRedisValue)
            {
                var project = JsonSerializer.Deserialize<Project>(prejectRedisValue.ToString());
                if (project != null)
                {
                    projects.Add(project);
                }
                else
                {
                    return BadRequest("Не удалось десериализовать проект");
                }
            }

            return projects;
        }
        catch (Exception ex)
        {
            return StatusCode(500, ex.Message);
        }
    }

    [HttpPost(RepoActionProjectsService.Add)]
    public async Task Add(string key, Project project)
    {
        var projectRedis = JsonSerializer.Serialize(project);
        await database.ListRightPushAsync(key, projectRedis);
    }

    [HttpPut(RepoActionProjectsService.Update)]
    public async Task Update(string key, int index, Project project)
    {
        if (!await database.KeyExistsAsync(key))
        {
            throw new KeyNotFoundException("Ключ не найден");
        }

        var projectForChangesRedis = await database.ListGetByIndexAsync(key, index);

        var projectForChanges = JsonSerializer.Deserialize<Project>(projectForChangesRedis.ToString()) ?? throw new Exception("Не удалось десериализовать проект");

        projectForChanges.Name = project.Name;
        projectForChanges.Description = project.Description;
        projectForChanges.Content = project.Content;

        var resultProjectRedis = JsonSerializer.Serialize(projectForChanges);
        await database.ListSetByIndexAsync(key, index, resultProjectRedis);
    }

    [HttpDelete(RepoActionProjectsService.Remove)]
    public async Task Delete(string key, int index)
    {
        if (!(await database.ListLengthAsync(key) > index) || index < 0)
        {
            throw new IndexOutOfRangeException("Индекс вышел за границы спика проектов");
        }

        var project = await database.ListGetByIndexAsync(key, index);
        await database.ListRemoveAsync(key, project, 1);
    }
}