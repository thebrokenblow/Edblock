using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.ServicesVM.FactoryVM;

namespace EdblockViewModel.PagesVM;

public class MainWindowVM : BaseVM
{
    private readonly NavigationStore _navigationStore;

    public BaseVM? CurrentViewModel => _navigationStore.CurrentViewModel;

    public MainWindowVM(NavigationStore navigationStore)
    {
        _navigationStore = navigationStore;
       
        var navigationServiceRegistration = FactoryNavigationService.CreateNavigationService(
            navigationStore, 
            () => new RegistrationVM(navigationStore));

        navigationServiceRegistration.Navigate();

        navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
    }

    private void OnCurrentViewModelChanged()
    {
        OnPropertyChanged(nameof(CurrentViewModel));
    }
}