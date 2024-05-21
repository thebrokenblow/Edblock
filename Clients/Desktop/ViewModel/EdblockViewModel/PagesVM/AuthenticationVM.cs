using EdblockModel;
using EdblockViewModel.CoreVM;
using System.Windows.Input;
using Prism.Commands;
using EdblockViewModel.Clients;
using EdblockViewModel.Services.Interfaces;
using EdblockViewModel.Services;
using EdblockViewModel.Services.Factories.Interfaces;
using System.Reflection;
using EdblockViewModel.Services.Factories;
using System;

namespace EdblockViewModel.PagesVM;

public class AuthenticationVM : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public DelegateCommand Signin { get; set; }

    public RelayCommand NavigateToMenu { get; }
    public RelayCommand NavigateToRegistration { get; }
    public RelayCommand NavigateToEditor { get; }


    private readonly AuthenticationModel authenticationModel = new();

    private readonly UserViewModel _userViewModel;
    public AuthenticationVM(
        IFactoryNavigationService factoryNavigationService,
        INavigationService navigationService,
        UserViewModel userViewModel)
    {
        NavigateToRegistration =
            factoryNavigationService.Create<RegistrationVM>(navigationService, true);

        NavigateToMenu =
            factoryNavigationService.Create<MenuVM>(navigationService, true);

        NavigateToEditor =
            factoryNavigationService.Create<EditorVM>(navigationService, true);

        _userViewModel = userViewModel;
        Signin = new(SignInAccount);
    }

    public void SignInAccount()
    {
       // var userModel = await authenticationModel.AuthenticateAccount(Login, Password);
       // _userViewModel.Id = userModel.Id;

        NavigateToMenu.Execute();
    }
}