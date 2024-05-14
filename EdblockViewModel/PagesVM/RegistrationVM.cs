using EdblockViewModel.CoreVM;
using Prism.Commands;
using EdblockViewModel.Service;

namespace EdblockViewModel.PagesVM;

public class RegistrationVM : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }

    public RelayCommand NavigateToAuthentication { get; }
    public RelayCommand NavigateToMenu { get; } 

    public RegistrationVM(INavigationService navigationService)
    {
        NavigateToAuthentication =
           FactoryNavigateService<AuthenticationVM>.Create(navigationService, true);

        NavigateToMenu =
            FactoryNavigateService<MenuVM>.Create(navigationService, true);

        Signin = new(RegistrationAccount);
    }

    public void RegistrationAccount()
    {
        NavigateToMenu.Execute();
        //var registration = new Registration();
        //registration.RegistrationAccount(Login, Password);
    }
}