using Edblock.PagesViewModel.Core;

namespace Edblock.PagesViewModel.Service;

internal class NavigateService(Func<Type, BaseViewModel> viewModelFactory) : ObservableObject, INavigationService
{
    private BaseViewModel? currentViewModel;
    public BaseViewModel? CurrentViewModel
    {
        get => currentViewModel;
        private set
        {
            currentViewModel = value;
            OnPropertyChange();
        }
    }

    public void NavigateTo<T>() where T : BaseViewModel
    {
        if (CurrentViewModel is not T)
        {
            CurrentViewModel = viewModelFactory.Invoke(typeof(T));
        }
    }
}