using System;
using System.Windows;
using EdblockViewModel.Clients;
using EdblockViewModel.Components.CanvasSymbols;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.ListSymbols;
using EdblockViewModel.Components.ListSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Core;
using EdblockViewModel.Pages;
using EdblockViewModel.Services;
using EdblockViewModel.Services.Factories;
using EdblockViewModel.Services.Factories.Interfaces;
using EdblockViewModel.Services.Interfaces;
using EdblockViewModel.Symbols;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsCommentSymbolVM;
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
        services.AddScoped<CanvasSymbolsComponentVM>();
        services.AddScoped<EditorVM>();
        services.AddScoped<ICanvasSymbolsComponentVM, CanvasSymbolsComponentVM>();
        services.AddScoped<IPopupBoxMenuComponentVM, PopupBoxMenuComponentVM>();

        services.AddScoped<IScaleAllSymbolComponentVM, ScaleAllSymbolComponentVM>();
        services.AddScoped<ILineStateStandardComponentVM, LineStateStandardComponentVM>();
        services.AddScoped<IListSymbolsComponentVM, ListSymbolsComponentVM>();
        services.AddScoped<ITopSettingsMenuComponentVM, TopSettingsMenuComponentVM>();
        services.AddScoped<IListCanvasSymbolsComponentVM, ListCanvasSymbolsComponentVM>();
        services.AddScoped<IFontFamilyComponentVM, FontFamilyComponentVM>();
        services.AddScoped<IFontSizeComponentVM, FontSizeComponentVM>();
        services.AddScoped<IFormatTextComponentVM, FormatTextComponentVM>();
        services.AddScoped<ITextAlignmentComponentVM, TextAlignmentComponentVM>();
        services.AddScoped<IFormatVerticalAlignComponentVM, FormatVerticalAlignComponentVM>();
        services.AddScoped<IColorSymbolComponentVM, ColorSymbolComponentVM>();

        services.AddTransient<ActionSymbolVM>();
        services.AddTransient<ConditionSymbolVM>();
        services.AddTransient<CycleForSymbolVM>();
        services.AddTransient<CycleWhileBeginSymbolVM>();
        services.AddTransient<CycleWhileEndSymbolVM>();
        services.AddTransient<InputOutputSymbolVM>();
        services.AddTransient<LinkSymbolVM>();
        services.AddTransient<StartEndSymbolVM>();
        services.AddTransient<SubroutineSymbolVM>();
        services.AddTransient<CommentSymbolVM>();

        services.AddScoped<INavigationService, NavigateService>();

        services.AddScoped(typeof(IFactoryNavigationService), typeof(FactoryNavigationService));

        services.AddScoped<Func<Type, BaseViewModel>>(
            serviceProvider =>
            pageTypeViewModel =>
            (BaseViewModel)serviceProvider.GetRequiredService(pageTypeViewModel));

        services.AddScoped<Func<Type, BlockSymbolVM>>(
           serviceProvider =>
           blockSymbolTypeVM =>
           (BlockSymbolVM)serviceProvider.GetRequiredService(blockSymbolTypeVM));

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