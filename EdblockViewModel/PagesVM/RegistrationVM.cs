using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;
using EdblockModel;
using Prism.Commands;

namespace EdblockViewModel.PagesVM;

public class RegistrationVM : BaseVM
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }

    public ICommand NavigateToAuthentication { get; }
    public ICommand NavigateToMenu { get; } 

    public RegistrationVM(NavigationStore navigationStoreMainWindow)
    {
        NavigateToAuthentication = FactoryNavigateCommand.CreateNavigateCommand(
          navigationStoreMainWindow,
          () => new AuthenticationVM(navigationStoreMainWindow));

        NavigateToMenu =
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new MenuVM(navigationStoreMainWindow));

        Signin = new(RegistrationAccount);
    }

    public void RegistrationAccount()
    {
        var registration = new Registration();
        registration.RegistrationAccount(Login, Password);
    }
}