using Edblock.ProjectsManagementModel.Options;
using Edblock.ProjectsManagementModel.ProjectManagementService;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Edblock.ProjectsManagementModel.Configurations;

public class ConfigurationProjectManager
{
    private static readonly IHost host;

    static ConfigurationProjectManager()
    {
        var builder = new HostBuilder()
               .ConfigureServices((hostContext, services) =>
               {
                   services.AddHttpClient<ProjectManager>();
                   services.AddTransient<IProjectManager, ProjectManager>();

                   var configurationBuilder = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json", optional: false);

                   IConfiguration configuration = configurationBuilder.Build();

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
