using EdblockViewModel.Core;
using EdblockViewModel.Interfaces;

namespace EdblockViewModel.PagesVM;

public class RegistrationVM(INavigationServices navigationServices) : ViewModel
{
    public RelayCommand NavigateToAuthenticationViewCommand { get; } =
            new RelayCommand(_ => navigationServices.NatigateTo<AuthenticationVM>(), _ => true);

    public RelayCommand NavigateToMenuViewCommand { get; } =
        new RelayCommand(_ => navigationServices.NatigateTo<MenuVM>(), _ => true);
}