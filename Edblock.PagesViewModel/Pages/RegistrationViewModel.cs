using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;

namespace Edblock.PagesViewModel.Pages;

public class RegistrationViewModel(INavigationService navigationService) : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmedPassword { get; set; } = string.Empty;


    public RelayCommand NavigateToAuthentication { get; } =
        FactoryNavigateService<AuthenticationViewModel>.Create(navigationService, true);

    public RelayCommand NavigateToMenu { get; } =
        FactoryNavigateService<MenuViewModel>.Create(navigationService, true);

    public RelayCommand NavigateToEditor { get; } =
        FactoryNavigateService<EditorViewModel>.Create(navigationService, true);
}