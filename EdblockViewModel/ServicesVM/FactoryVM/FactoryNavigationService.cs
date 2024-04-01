using System;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;

namespace EdblockViewModel.ServicesVM.FactoryVM;

public class FactoryNavigationService
{
    public static INavigationService<TViewModel> CreateNavigationService<TViewModel>(
        NavigationStore navigationStore, 
        Func<TViewModel> creatorViewModel) 
        where TViewModel : BaseVM
    {
        return new NavigationService<TViewModel>(navigationStore, creatorViewModel);
    }

    public static IParameterNavigationService<TParametr, TViewModel> CreateParameterNavigationService<TViewModel, TParametr>(
        NavigationStore navigationStore, 
        Func<TParametr, TViewModel> creatorViewModel) 
        where TViewModel : BaseVM
    {
        return new ParameterNavigationService<TParametr, TViewModel>(navigationStore, creatorViewModel);
    }
}