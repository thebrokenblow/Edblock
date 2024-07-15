using System;
using System.Collections.Generic;
using System.Windows;
using EdblockView.Components.TopSettingsMenu;
using EdblockViewModel.Clients;
using EdblockViewModel.Components;
using EdblockViewModel.Components.CanvasSymbols;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.ListSymbols;
using EdblockViewModel.Components.ListSymbols.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;
using EdblockViewModel.Components.Subjects;
using EdblockViewModel.Components.Subjects.Interfaces;
using EdblockViewModel.Core;
using EdblockViewModel.Pages;
using EdblockViewModel.Project;
using EdblockViewModel.Project.Interfaces;
using EdblockViewModel.Services;
using EdblockViewModel.Services.Factories;
using EdblockViewModel.Services.Factories.Interfaces;
using EdblockViewModel.Services.Interfaces;
using EdblockViewModel.Symbols;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.ComponentsCommentSymbolVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EdblockView;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    public static IServiceProvider? serviceProvider;

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
        services.AddScoped<IProjectVM, ProjectVM>();
        services.AddScoped<ICanvasSymbolsComponentVM, CanvasSymbolsComponentVM>();
        services.AddScoped<IPopupBoxMenuComponentVM, PopupBoxMenuComponentVM>();
        services.AddScoped<IFactoryBlockSymbolVM, FactoryBlockSymbolVM>();

        services.AddScoped<IScaleAllSymbolComponentVM, ScaleAllSymbolComponentVM>();
        services.AddScoped<ILineStateStandardComponentVM, LineStateStandardComponentVM>();
        services.AddScoped<IListSymbolsComponentVM, ListSymbolsComponentVM>();
        services.AddScoped<ITopSettingsMenuComponentVM, TopSettingsMenuVM>();
        services.AddScoped<IListCanvasSymbolsComponentVM, ListCanvasSymbolsComponentVM>();
        services.AddScoped<IFormatTextSubject, FormatTextSubject>();

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
        services.AddTransient<IBuilderScaleRectangles, BuilderScaleRectangles>();

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

        services.AddScoped<IFontSizeSubject<int>>(
            serviceProvider => new FontSizeSubject<int>(2, 300, 2, 12));

        services.AddScoped<ITextAlignmentSubject>(
            serviceProvider => new TextAlignmentSubject(1));

        services.AddScoped<IColorSubject, ColorSubject>();

        services.AddScoped<IFontFamilySubject>(
           serviceProvider => new FontFamilySubject(
           [
               "Times New Roman"
           ], 
           "Times New Roman"));

        serviceProvider = services.BuildServiceProvider();
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        if (serviceProvider is null)
        {
            return;
        }

        var mainWindow = serviceProvider.GetRequiredService<MainWindow>();
        var mainViewModel = serviceProvider.GetRequiredService<MainWindowVM>();

        mainViewModel.NavigationService.NavigateTo<EditorVM>();
        mainWindow.Show();

        base.OnStartup(e);
    }
}