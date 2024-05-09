using System;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;

namespace EdblockViewModel.ServicesVM;

public class ParameterNavigationService<TParameter, TViewModel>(
    NavigationStore navigationStore, 
    Func<TParameter, TViewModel> createViewModel) : 
    IParameterNavigationService<TParameter, TViewModel> where TViewModel : BaseVM
{
    public void Navigate(TParameter parameter)
    {
        navigationStore.CurrentViewModel = createViewModel.Invoke(parameter);
    }
}