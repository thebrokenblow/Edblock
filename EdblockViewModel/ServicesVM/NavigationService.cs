using System;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;

namespace EdblockViewModel.ServicesVM;

public class NavigationService<TViewModel>(NavigationStore navigationStore, Func<TViewModel> createViewModel) : INavigationService<TViewModel> where TViewModel : BaseVM
{
    public void Navigate()
    {
        if (navigationStore.CurrentViewModel is not TViewModel)
        {
            navigationStore.CurrentViewModel = createViewModel.Invoke();
        }
    }
}
