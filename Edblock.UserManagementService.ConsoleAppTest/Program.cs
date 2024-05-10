using Edblock.Library.Clients.IdentityServer;
using Edblock.Library.Clients.UserManagementService;
using Edblock.Library.Options;
using Edblock.UserManagementService.ConsoleAppTest.Tests;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHttpClient<IdentityServerClient>();
                    services.AddHttpClient<UsersClient>();
                    services.AddHttpClient<RolesClient>();

                    services.AddTransient<UserTest>();
                    services.AddTransient<RoleUserTest>();


                    var configurationBuilder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: false);

                    IConfiguration configuration = configurationBuilder.Build();

                    services.Configure<IdentityServerApiOptions>(configuration.GetSection(IdentityServerApiOptions.SectionName));
                    services.Configure<ServiceAdressOptions>(configuration.GetSection(ServiceAdressOptions.SectionName));
                })
                .ConfigureLogging(logging =>
                {
                    logging.AddConsole();
                    logging.SetMinimumLevel(LogLevel.Information);
                })
                .UseConsoleLifetime();

var host = builder.Build();

using (var serviceScope = host.Services.CreateScope())
{
    var services = serviceScope.ServiceProvider;

    try
    {
        var roleUserTest = services.GetRequiredService<RoleUserTest>();
        var userTest = services.GetRequiredService<UserTest>();

        var errorsRoleUserTest = await roleUserTest.RunTests(args);
        var errorsUserTest = await userTest.RunTests(args);

        foreach (var errorRoleUserTest in errorsRoleUserTest)
        {
            Console.WriteLine(errorRoleUserTest);
        }

        foreach (var errorUserTest in errorsUserTest)
        {
            Console.WriteLine(errorUserTest);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error occured: {ex.Message}");
    }
}

Console.ReadKey();