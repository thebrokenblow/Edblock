using EdblockModel;
using EdblockViewModel.CoreVM;
using EdblockViewModel.Service;
using System.Windows.Input;
using Prism.Commands;

namespace EdblockViewModel.PagesVM;

public class AuthenticationVM : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }

    public ICommand NavigateToMenu { get; }
    public ICommand NavigateToRegistration { get; }

    private Authentication? authenticationModel;

    public AuthenticationVM(INavigationService navigationService)
    {
        NavigateToRegistration = 
            FactoryNavigateService<RegistrationVM>.Create(navigationService, true);

        NavigateToMenu = 
            FactoryNavigateService<MenuVM>.Create(navigationService, true);

        Signin = new(SignInAccount);
    }

    public void SignInAccount()
    {
        NavigateToMenu.Execute(null);
        //authenticationModel = new Authentication();
        //var tokenResponse = await authenticationModel.Authenticate(Login, Password);
    }
}