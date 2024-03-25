using EdblockViewModel.Core;
using EdblockViewModel.Interfaces;

namespace EdblockViewModel.PagesVM;

public class MenuVM(INavigationServices navigationServices) : ViewModel
{
    public RelayCommand NavigateToAuthenticationViewCommand { get; } =
            new RelayCommand(_ => navigationServices.NatigateTo<AuthenticationVM>(), _ => true);
}