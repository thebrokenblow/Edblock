using System;
using EdblockViewModel.Core;
using EdblockViewModel.Services;
using EdblockViewModel.Services.Factories.Interfaces;
using EdblockViewModel.Services.Interfaces;

namespace EdblockViewModel.Pages;

public class MenuVM : BaseViewModel
{
    private INavigationService navigationServiceMenu;
    public INavigationService NavigationServiceMenu
    {
        get => navigationServiceMenu;
        set
        {
            navigationServiceMenu = value;
            OnPropertyChanged();
        }
    }

    private readonly INavigationService _navigateServiceWindow;

    public RelayCommand NavigateToHome { get; set; }
    public RelayCommand NavigateToSettings { get; set; }
    public RelayCommand NavigateToProjects { get; set; }
    public RelayCommand NavigateToAuthentication { get; set; }

    public MenuVM(
        IFactoryNavigationService factoryNavigationService,
        INavigationService navigateServiceWindow,
        Func<Type, BaseViewModel> viewModelFactory)
    {
        _navigateServiceWindow = navigateServiceWindow;

        navigationServiceMenu = new NavigateService(viewModelFactory);

        NavigateToHome =
            factoryNavigationService.Create<HomeVM>(navigationServiceMenu, true);

        NavigateToSettings =
            factoryNavigationService.Create<SettingsVM>(navigationServiceMenu, true);

        NavigateToProjects =
            factoryNavigationService.Create<ProjectsVM>(navigationServiceMenu, true);

        NavigateToAuthentication =
            factoryNavigationService.Create<AuthenticationVM>(_navigateServiceWindow, true);

        NavigateToSettings.Execute();
    }
}