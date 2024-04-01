using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;

namespace EdblockViewModel.PagesVM;

public class RegistrationVM(NavigationStore navigationStore) : BaseVM
{
    public ICommand NavigateToAuthentication { get; } = 
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStore,
            () => new AuthenticationVM(navigationStore));

    public ICommand NavigateToMenu { get; } =
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStore,
            () => new MenuVM(navigationStore));
}