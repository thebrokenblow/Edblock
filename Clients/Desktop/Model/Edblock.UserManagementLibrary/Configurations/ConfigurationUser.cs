
using Edblock.UserManagementLibrary.Clients;
using Edblock.UserManagementLibrary.Clients.IdentityServer;
using Edblock.UserManagementLibrary.Clients.UserManagementService;
using Edblock.UserManagementLibrary.Options;
using Edblock.UserManagementModel.Clients;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Edblock.UserManagementLibrary.Configurations;

public class ConfigurationUser
{
    private static readonly IHost host;

    static ConfigurationUser()
    {
        var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHttpClient<IdentityServerClient>();
                   services.AddHttpClient<UsersClient>();
                   services.AddTransient<Registration>();
                   services.AddTransient<Authentication>();

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

        host = builder.Build();
    }

    public static IServiceProvider GetServiceProvider()
    {
        return host.Services;
    }
}
