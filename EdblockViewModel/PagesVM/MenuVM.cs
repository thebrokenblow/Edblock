using System;
using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.Service;

namespace EdblockViewModel.PagesVM;

public class MenuVM : BaseViewModel
{
    private INavigationService navigationServiceMenu;
    public INavigationService NavigationServiceMenu
    {
        get => navigationServiceMenu;
        set
        {
            navigationServiceMenu = value;
            OnPropertyChange();
        }
    }

    private readonly INavigationService _navigateServiceWindow;

    public ICommand NavigateToHome { get; set; }
    public ICommand NavigateToSettings { get; set; }
    public ICommand NavigateToProjects { get; set; }
    public ICommand NavigateToAuthentication { get; set; }

    public MenuVM(INavigationService navigateServiceWindow, Func<Type, BaseViewModel> viewModelFactory)
    {
        _navigateServiceWindow = navigateServiceWindow;

        navigationServiceMenu = new NavigateService(viewModelFactory);

        NavigateToHome =
            FactoryNavigateService<HomeVM>.Create(navigationServiceMenu, true);

        NavigateToSettings =
            FactoryNavigateService<SettingsVM>.Create(navigationServiceMenu, true);

        NavigateToProjects =
            FactoryNavigateService<ProjectsVM>.Create(navigationServiceMenu, true);

        NavigateToAuthentication =
            FactoryNavigateService<AuthenticationVM>.Create(_navigateServiceWindow, true);

        NavigateToSettings.Execute(null);
    }
}