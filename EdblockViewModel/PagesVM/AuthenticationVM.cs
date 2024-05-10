using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;
using Prism.Commands;
using System.Net.Http;
using System;
using IdentityModel.Client;
using EdblockModel;

namespace EdblockViewModel.PagesVM;

public class AuthenticationVM : BaseVM
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }

    public ICommand NavigateToMenu { get; }
    public ICommand NavigateToRegistration { get; }

    private Authentication? authenticationModel;

    public AuthenticationVM(NavigationStore navigationStoreMainWindow)
    {
        NavigateToRegistration = FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new RegistrationVM(navigationStoreMainWindow));

        NavigateToMenu = FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new MenuVM(navigationStoreMainWindow));

        Signin = new(SignInAccount);
    }

    public async void SignInAccount()
    {
        authenticationModel = new Authentication();
        var tokenResponse = await authenticationModel.Authenticate(Login, Password);
    }
}