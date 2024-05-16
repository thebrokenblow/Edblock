using EdblockModel;
using EdblockViewModel.CoreVM;
using EdblockViewModel.Service;
using System.Windows.Input;
using Prism.Commands;
using EdblockViewModel.Clients;

namespace EdblockViewModel.PagesVM;

public class AuthenticationVM : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }

    public ICommand NavigateToMenu { get; }
    public ICommand NavigateToRegistration { get; }

    private readonly AuthenticationModel authenticationModel = new();

    private readonly UserViewModel _userViewModel;
    public AuthenticationVM(INavigationService navigationService, UserViewModel userViewModel)
    {
        NavigateToRegistration = 
            FactoryNavigateService<RegistrationVM>.Create(navigationService, true);

        NavigateToMenu = 
            FactoryNavigateService<MenuVM>.Create(navigationService, true);

        _userViewModel = userViewModel;
        Signin = new(SignInAccount);
    }

    public async void SignInAccount()
    {
        var userModel = await authenticationModel.AuthenticateAccount(Login, Password);
        _userViewModel.Id = userModel.Id;

        NavigateToMenu.Execute(null);
    }
}