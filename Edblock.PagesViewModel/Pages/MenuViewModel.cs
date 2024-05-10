using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;

namespace Edblock.PagesViewModel.Pages;

public class MenuViewModel : BaseViewModel
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

    public RelayCommand NavigateToHome { get; }
    public RelayCommand NavigateToProjects { get; }
    public RelayCommand NavigateToSettings { get; }
    public RelayCommand NavigateToAuthentication { get; }

    public MenuViewModel(INavigationService navigateServiceWindow, Func<Type, BaseViewModel> viewModelFactory)
    {
        _navigateServiceWindow = navigateServiceWindow;

        navigationServiceMenu = new NavigateService(viewModelFactory);

        NavigateToHome =
            FactoryNavigateService<HomeViewModel>.Create(navigationServiceMenu, true);

        NavigateToSettings =
            FactoryNavigateService<SettingsViewModel>.Create(navigationServiceMenu, true);

        NavigateToProjects =
            FactoryNavigateService<ProjectsViewModel>.Create(navigationServiceMenu, true);

        NavigateToAuthentication =
            FactoryNavigateService<AuthenticationViewModel>.Create(_navigateServiceWindow, true);

        NavigateToSettings.Execute();
    }
}