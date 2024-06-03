using EdblockViewModel.Core;
using EdblockViewModel.Services.Interfaces;

namespace EdblockViewModel.Pages;

public class MainWindowVM(INavigationService navigationService) : BaseViewModel
{
    public INavigationService NavigationService
    {
        get => navigationService;
        set
        {
            navigationService = value;
            OnPropertyChanged();
        }
    }
}