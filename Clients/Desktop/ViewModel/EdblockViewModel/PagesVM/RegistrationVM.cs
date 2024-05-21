using Prism.Commands;
using EdblockModel;
using EdblockViewModel.CoreVM;
using EdblockViewModel.Clients;
using EdblockViewModel.Services.Interfaces;
using EdblockViewModel.Services.Factories.Interfaces;

namespace EdblockViewModel.PagesVM;

public class RegistrationVM : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }
    public RelayCommand NavigateToAuthentication { get; }
    public RelayCommand NavigateToMenu { get; }
    public RelayCommand NavigateToEditor { get; }

    private readonly UserViewModel _userViewModel;

    public RegistrationVM(
        IFactoryNavigationService factoryNavigationService,
        INavigationService navigationService,
        UserViewModel userViewModel)
    {
        NavigateToAuthentication =
           factoryNavigationService.Create<AuthenticationVM>(navigationService, true);

        NavigateToMenu =
            factoryNavigationService.Create<MenuVM>(navigationService, true);

        NavigateToEditor =
            factoryNavigationService.Create<EditorVM>(navigationService, true);

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