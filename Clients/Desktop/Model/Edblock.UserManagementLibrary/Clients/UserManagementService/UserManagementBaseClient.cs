using Edblock.Library.UserManagementService.Models;
using Edblock.UserManagementLibrary.Options;
using Edblock.UserManagementLibrary.UserManagementService.Responses;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.Text;
using System.Text.Json;

namespace Edblock.UserManagementLibrary.Clients.UserManagementService;

public abstract class UserManagementBaseClient
{
    public UserManagementBaseClient(HttpClient client, IOptions<ServiceAdressOptions> options)
    {
        HttpClient = client;
        HttpClient.BaseAddress = new Uri(options.Value.UserManagementService);
    }

    public HttpClient HttpClient { get; init; }

    protected async Task<ApplicationUser> SendGetRequest(string path)
    {
        //https://localhost:7121/Users/getByName?name=alice
        var requestResult = await HttpClient.GetAsync(path);

        IdentityResult result;

        if (requestResult.IsSuccessStatusCode)
        {
            var responseJson = await requestResult.Content.ReadAsStringAsync();
            var applicationUser = JsonSerializer.Deserialize<ApplicationUser>(responseJson) ?? throw new NullReferenceException("Response is null");

            return applicationUser;
        }

        throw new Exception("Не удалось получить пользователя");
    }

    protected async Task<IdentityResult> SendPostRequest<TRequest>(TRequest request, string path)
    {
        var jsonContent = JsonSerializer.Serialize(request);
        var httpContent = new StringContent(jsonContent, Encoding.UTF8, "application/json");
        var requestResult = await HttpClient.PostAsync(path, httpContent);

        IdentityResult result;

        if (requestResult.IsSuccessStatusCode)
        {
            var responseJson = await requestResult.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<IdentityResultDto>(responseJson) ?? throw new NullReferenceException("Response is null");
            result = HandleResponse(response);
        }
        else
        {
            result = IdentityResult.Failed(
                new IdentityError()
                {
                    Code = requestResult.StatusCode.ToString(),
                    Description = requestResult.ReasonPhrase
                }
            );
        }

        return result;
    }

    protected async Task<UserManagementServiceResponse<TResult>> SendGetRequest<TResult>(string request)
    {
        var requestResult = await HttpClient.GetAsync(request);

        UserManagementServiceResponse<TResult> result;

        if (requestResult.IsSuccessStatusCode)
        {
            var response = await requestResult.Content.ReadAsStringAsync();
            if (string.IsNullOrEmpty(response))
            {
                result = new UserManagementServiceResponse<TResult>()
                {
                    Code = requestResult.StatusCode.ToString(),
                    Description = requestResult.ReasonPhrase
                };
            }
            else
            {
                var payload = JsonSerializer.Deserialize<TResult>(response);
                result = new UserManagementServiceResponse<TResult>()
                {
                    Code = requestResult.StatusCode.ToString(),
                    Description = requestResult.ReasonPhrase,
                    Payload = payload
                };
            }
        }
        else
        {
            result = new UserManagementServiceResponse<TResult>()
            {
                Code = requestResult.StatusCode.ToString(),
                Description = requestResult.ReasonPhrase
            };
        }

        return result;
    }

    private static IdentityResult HandleResponse(IdentityResultDto response)
    {
        if (response.Succeeded)
        {
            return IdentityResult.Success;
        }
        else
        {
            return IdentityResult.Failed(response.Errors.ToArray());
        }
    }
}