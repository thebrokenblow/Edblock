using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;

namespace EdblockViewModel.PagesVM;

public class AuthenticationVM(NavigationStore navigationStoreMainWindow) : BaseVM
{
    public ICommand NavigateToRegistration { get; } = 
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow, 
            () => new RegistrationVM(navigationStoreMainWindow));

    public ICommand NavigateToMenu { get; } = 
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow, 
            () => new MenuVM(navigationStoreMainWindow));
}