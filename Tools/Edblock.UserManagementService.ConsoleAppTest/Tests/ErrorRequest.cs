using Microsoft.AspNetCore.Identity;

namespace Edblock.UserManagementService.ConsoleAppTest.Tests;

public class ErrorRequest(IdentityResult identityResult)
{
    private readonly List<string> errorsTests = [];

    public List<string> GetErrorRequest()
    {
        if (!identityResult.Succeeded)
        {
            foreach (var error in identityResult.Errors)
            {
                errorsTests.Add($"{error.Code} : {error.Description}");

                Console.WriteLine(error.Code);
                Console.WriteLine(error.Description);
            }
        }

        return errorsTests;
    }
}