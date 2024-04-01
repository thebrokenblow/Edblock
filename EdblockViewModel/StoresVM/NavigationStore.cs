using System;
using EdblockViewModel.CoreVM;

namespace EdblockViewModel.StoresVM;

public class NavigationStore
{
    public event Action? CurrentViewModelChanged;

    private BaseVM? currentViewModel;
    public BaseVM? CurrentViewModel
    {
        get => currentViewModel;
        set
        {
            currentViewModel = value;
            OnCurrentViewModelChanged();
        }
    }

    private void OnCurrentViewModelChanged()
    {
        CurrentViewModelChanged?.Invoke();
    }
}