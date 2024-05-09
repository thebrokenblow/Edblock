using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;

namespace Edblock.PagesViewModel.ViewModel;

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

    public RelayCommand NavigateToSettings { get; }
    public RelayCommand NavigateToProjects { get; }
    public RelayCommand NavigateToAuthentication { get; }

    public MenuViewModel(INavigationService navigateServiceWindow, Func<Type, BaseViewModel> viewModelFactory)
    {
        _navigateServiceWindow = navigateServiceWindow;

        navigationServiceMenu = new NavigateService(viewModelFactory);

        NavigateToSettings =
            FactoryNavigateService<SettingsViewModel>.Create(navigationServiceMenu, true);

        NavigateToProjects =
            FactoryNavigateService<SettingsViewModel>.Create(navigationServiceMenu, true);

        NavigateToAuthentication =
            FactoryNavigateService<SettingsViewModel>.Create(_navigateServiceWindow, true);

        NavigateToSettings.Execute();
    }
}