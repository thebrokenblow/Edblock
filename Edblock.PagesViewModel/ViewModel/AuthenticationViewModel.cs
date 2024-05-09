using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;

namespace Edblock.PagesViewModel.ViewModel;

public class AuthenticationViewModel(INavigationService navigationService) : BaseViewModel
{
    private string? login;
    public string? Login
    {
        get => login;
        set
        {
            login = value;
            OnPropertyChanged();
        }
    }

    public RelayCommand NavigateToRegistration { get; } =
        FactoryNavigateService<RegistrationViewModel>.Create(navigationService, true);

    public RelayCommand NavigateToMenu { get; } =
        FactoryNavigateService<MenuViewModel>.Create(navigationService, true);
}