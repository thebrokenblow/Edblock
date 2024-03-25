using EdblockViewModel.Core;
using EdblockViewModel.Interfaces;

namespace EdblockViewModel.PagesVM;

public class AuthenticationVM(INavigationServices navigationServices) : ViewModel
{
    public RelayCommand NavigateToRegistrationViewCommand { get; } =
            new RelayCommand(_ => navigationServices.NatigateTo<RegistrationVM>(), _ => true);

    public RelayCommand NavigateToMenuViewCommand { get; } =
        new RelayCommand(_ => navigationServices.NatigateTo<MenuVM>(), _ => true);
}