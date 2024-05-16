using EdblockViewModel.CoreVM;
using EdblockViewModel.Service;

namespace EdblockViewModel.PagesVM;

public class MainWindowVM(INavigationService navigationService) : BaseViewModel
{
    public INavigationService NavigationService
    {
        get => navigationService;
        set
        {
            navigationService = value;
            OnPropertyChange();
        }
    }
}