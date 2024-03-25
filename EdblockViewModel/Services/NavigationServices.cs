using System;
using EdblockViewModel.Core;
using EdblockViewModel.Interfaces;

namespace EdblockViewModel.Services;

public class NavigationServices(Func<Type, ViewModel> viewModelFactory) : ObservabelObject, INavigationServices
{
    private ViewModel? currentView;
    public ViewModel? CurrentView
    {
        get => currentView;
        private set
        {
            currentView = value;
            OnPropertyChanged();
        }
    }

    private readonly Func<Type, ViewModel> viewModelFactory = viewModelFactory;

    public void NatigateTo<TViewModel>() where TViewModel : ViewModel
    {
        ViewModel viewModel = viewModelFactory.Invoke(typeof(TViewModel));
        CurrentView = viewModel;
    }
}