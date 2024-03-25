using System;
using System.Windows;
using EdblockViewModel.Core;
using EdblockViewModel.PagesVM;
using EdblockViewModel.Services;
using EdblockViewModel.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EdblockView;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider serviceProvider;
    public App()
    {
        var service = new ServiceCollection();

        service.AddSingleton(provider => new MainWindow()
        {
            DataContext = provider.GetRequiredService<MainWindowVM>()
        });

        service.AddSingleton<MainWindowVM>();
        service.AddSingleton<MenuVM>();
        service.AddSingleton<RegistrationVM>();
        service.AddSingleton<AuthenticationVM>();
        service.AddSingleton<INavigationServices, NavigationServices>();

        service.AddSingleton<Func<Type, ViewModel>>(
            serviceProvider =>
            viewModelType =>
            (ViewModel)serviceProvider.GetRequiredService(viewModelType));

        serviceProvider = service.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainViewModel = serviceProvider.GetRequiredService<MainWindow>();
        mainViewModel.Show();
        base.OnStartup(e);
    }
}