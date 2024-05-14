using EdblockViewModel.CoreVM;
using System;

namespace EdblockViewModel.Service;

public class NavigateService(Func<Type, BaseViewModel> viewModelFactory) : ObservableObject, INavigationService
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