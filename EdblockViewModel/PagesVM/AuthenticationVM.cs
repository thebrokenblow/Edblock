using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;

namespace EdblockViewModel.PagesVM;

public class AuthenticationVM(NavigationStore navigationStore) : BaseVM
{
    public ICommand NavigateToRegistration { get; } = 
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStore, 
            () => new RegistrationVM(navigationStore));

    public ICommand NavigateToMenu { get; } = 
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStore, 
            () => new MenuVM(navigationStore));
}