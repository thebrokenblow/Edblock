using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;

namespace Edblock.PagesViewModel.Pages;

public class AuthenticationViewModel(INavigationService navigationService) : BaseViewModel
{
    public string Login { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;

    public RelayCommand NavigateToRegistration { get; } =
        FactoryNavigateService<RegistrationViewModel>.Create(navigationService, true);

    public RelayCommand NavigateToMenu { get; } =
        FactoryNavigateService<MenuViewModel>.Create(navigationService, true);

    public RelayCommand NavigateToEditor { get; } =
        FactoryNavigateService<EditorViewModel>.Create(navigationService, true);
}