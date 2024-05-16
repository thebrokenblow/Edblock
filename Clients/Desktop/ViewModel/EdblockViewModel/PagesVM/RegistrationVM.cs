using Prism.Commands;
using EdblockModel;
using EdblockViewModel.CoreVM;
using EdblockViewModel.Service;
using EdblockViewModel.Clients;

namespace EdblockViewModel.PagesVM;

public class RegistrationVM : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }
    public RelayCommand NavigateToAuthentication { get; }
    public RelayCommand NavigateToMenu { get; }

    private readonly UserViewModel _userViewModel;

    public RegistrationVM(INavigationService navigationService, UserViewModel userViewModel)
    {
        NavigateToAuthentication =
           FactoryNavigateService<AuthenticationVM>.Create(navigationService, true);

        NavigateToMenu =
            FactoryNavigateService<MenuVM>.Create(navigationService, true);

        Signin = new(RegistrationAccount);

        _userViewModel = userViewModel;
    }

    public async void RegistrationAccount()
    {
        var registrationModel = new RegistrationModel();
        var userModel = await registrationModel.Registration(Login, Password);
        _userViewModel.Id = userModel.Id;
        NavigateToMenu.Execute();
    }
}