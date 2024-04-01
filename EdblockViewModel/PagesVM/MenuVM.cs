using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;

namespace EdblockViewModel.PagesVM;

public class MenuVM : BaseVM
{
    public ICommand NavigateToHome { get; }
    public ICommand NavigateToProjects { get; }
    public ICommand NavigateToSettings { get; }
    public ICommand NavigateToLogout { get; }

    private readonly NavigationStore navigationStoreMenu = new();
    public BaseVM? CurrentViewModel => navigationStoreMenu.CurrentViewModel;
    public MenuVM(NavigationStore navigationStoreMainWindow)
    {
        NavigateToHome = FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMenu,
            () => new HomeVM());

        NavigateToProjects = FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMenu,
            () => new ProjectsVM(navigationStoreMainWindow, navigationStoreMenu));

        NavigateToSettings = FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMenu,
            () => new SettingsVM());

        NavigateToLogout = FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new AuthenticationVM(navigationStoreMainWindow));

        NavigateToHome.Execute(navigationStoreMenu);

        navigationStoreMenu.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}