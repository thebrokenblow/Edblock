using Prism.Commands;
using EdblockModel;
using EdblockViewModel.CoreVM;
using EdblockViewModel.Service;
using System.Windows.Input;

namespace EdblockViewModel.PagesVM;

public class RegistrationVM : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }
    public ICommand NavigateToAuthentication { get; }
    public ICommand NavigateToMenu { get; }

    public RegistrationVM(INavigationService navigationService)
    {
        NavigateToAuthentication =
           FactoryNavigateService<AuthenticationVM>.Create(navigationService, true);

        NavigateToMenu =
            FactoryNavigateService<MenuVM>.Create(navigationService, true);

        Signin = new(RegistrationAccount);
    }

    public async void RegistrationAccount()
    {
        var registrationModel = new RegistrationModel();
        var tokenResponse = await registrationModel.Registration(Login, Password);
    }
}