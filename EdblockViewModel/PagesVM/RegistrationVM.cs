﻿using EdblockViewModel.CoreVM;
using Prism.Commands;
using EdblockViewModel.Service;
using EdblockModel;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

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

    public async void RegistrationAccount()
    {
        var registrationModel = new RegistrationModel();
        var tokenResponse = await registrationModel.Registration(Login, Password);
    }
}