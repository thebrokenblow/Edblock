using EdblockViewModel.Core;
using EdblockViewModel.Interfaces;

namespace EdblockViewModel.PagesVM;

public class MainWindowVM : ViewModel
{
    private INavigationServices? _navigationServices;
    public INavigationServices? NavigationServices
    {
        get => _navigationServices;
        set
        {
            _navigationServices = value;
            OnPropertyChanged();

            NavigateToHomeCommand.Execute(null);
        }
    }

    public RelayCommand NavigateToHomeCommand { get; set; }
    public RelayCommand NavigateToAccountViewCommand { get; set; }

    public MainWindowVM(INavigationServices navigationServices)
    {
        _navigationServices = navigationServices;

        NavigateToHomeCommand = new RelayCommand(_ => _navigationServices.NatigateTo<RegistrationVM>(), _ => true);
        NavigateToAccountViewCommand = new RelayCommand(_ => _navigationServices.NatigateTo<AuthenticationVM>(), _ => true);

        NavigateToHomeCommand.Execute(null);
    }
}