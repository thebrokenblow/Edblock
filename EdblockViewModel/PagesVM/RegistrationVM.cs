using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;

namespace EdblockViewModel.PagesVM;

public class RegistrationVM(NavigationStore navigationStoreMainWindow) : BaseVM
{
    public ICommand NavigateToAuthentication { get; } = 
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new AuthenticationVM(navigationStoreMainWindow));

    public ICommand NavigateToMenu { get; } =
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new MenuVM(navigationStoreMainWindow));
}