using System;
using System.Windows;
using EdblockViewModel.CoreVM;
using EdblockViewModel.PagesVM;
using EdblockViewModel.Service;
using Microsoft.Extensions.DependencyInjection;

namespace EdblockView;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly IServiceProvider serviceProvider;
    public App()
    {
        IServiceCollection services = new ServiceCollection();

        services.AddScoped(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainWindowVM>()
        });

        services.AddScoped<MainWindowVM>();
        services.AddScoped<AuthenticationVM>();
        services.AddScoped<RegistrationVM>();
        services.AddScoped<EditorVM>();
        services.AddScoped<MenuVM>();
        services.AddScoped<ProjectsVM>();
        services.AddScoped<HomeVM>();
        services.AddScoped<SettingsVM>();
        services.AddScoped<INavigationService, NavigateService>();
        services.AddScoped<Func<Type, BaseViewModel>>(
            serviceProvider =>
            viewModelType =>
            (BaseViewModel)serviceProvider.GetRequiredService(viewModelType));

        serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        var mainViewModel = serviceProvider.GetRequiredService<MainWindowVM>();

        mainViewModel.NavigationService.NavigateTo<AuthenticationVM>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}