using System;
using System.Windows;
using EdblockViewModel.Clients;
using EdblockViewModel.ComponentsVM.CanvasSymbols;
using EdblockViewModel.ComponentsVM.CanvasSymbols.Interfaces;
using EdblockViewModel.ComponentsVM.TopSettingsPanelComponent;
using EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;
using EdblockViewModel.CoreVM;
using EdblockViewModel.PagesVM;
using EdblockViewModel.Services;
using EdblockViewModel.Services.Factories;
using EdblockViewModel.Services.Factories.Interfaces;
using EdblockViewModel.Services.Interfaces;
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
        services.AddScoped<MenuVM>();
        services.AddScoped<ProjectsVM>();
        services.AddScoped<HomeVM>();
        services.AddScoped<SettingsVM>();
        services.AddScoped<UserViewModel>();
        services.AddScoped<CanvasSymbolsVM>();
        services.AddScoped<IListCanvasSymbolsVM, ListCanvasSymbolsVM>();
        services.AddScoped<IFontFamilyComponentVM, FontFamilyComponentVM>();
        services.AddScoped<IFontSizeComponentVM, FontSizeComponentVM>();
        services.AddScoped<IFormatTextComponentVM, FormatTextComponentVM>();
        services.AddScoped<ITextAlignmentComponentVM, TextAlignmentComponentVM>();

        services.AddScoped<INavigationService, NavigateService>();

        services.AddScoped(typeof(IFactoryNavigationService), typeof(FactoryNavigationService));

        services.AddScoped<Func<Type, BaseViewModel>>(
            serviceProvider =>
            viewModelType =>
            (BaseViewModel)serviceProvider.GetRequiredService(viewModelType));

        services.AddScoped<EditorVM>();

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