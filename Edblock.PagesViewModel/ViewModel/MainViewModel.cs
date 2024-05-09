using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;

namespace Edblock.PagesViewModel.ViewModel;

public class MainViewModel(INavigationService navigationService) : BaseViewModel
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