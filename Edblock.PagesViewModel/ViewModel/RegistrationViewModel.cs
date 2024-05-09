using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;

namespace Edblock.PagesViewModel.ViewModel;

public class RegistrationViewModel(INavigationService navigationService) : BaseViewModel
{
    public RelayCommand NavigateToAuthentication { get; } =
        FactoryNavigateService<AuthenticationViewModel>.Create(navigationService, true);

    public RelayCommand NavigateToMenu { get; } =
        FactoryNavigateService<MenuViewModel>.Create(navigationService, true);
}