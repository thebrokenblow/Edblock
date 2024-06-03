using System;
using EdblockViewModel.Core;
using EdblockViewModel.Services.Interfaces;

namespace EdblockViewModel.Services;

public class NavigateService(Func<Type, BaseViewModel> pageViewModelFactory) : ObservableObject, INavigationService
{
    private BaseViewModel? currentViewModel;
    public BaseViewModel? CurrentViewModel
    {
        get => currentViewModel;
        private set
        {
            currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public void NavigateTo<T>() where T : BaseViewModel
    {
        if (CurrentViewModel is not T)
        {
            CurrentViewModel = pageViewModelFactory.Invoke(typeof(T));
        }
    }
}