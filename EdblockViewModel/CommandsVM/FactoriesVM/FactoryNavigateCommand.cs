using System;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.ServicesVM.FactoryVM;

namespace EdblockViewModel.CommandsVM.FactoriesVM;

public class FactoryNavigateCommand
{
    public static NavigateCommand<TViewModel> CreateNavigateCommand<TViewModel>(
        NavigationStore navigationStore,
        Func<TViewModel> creatorViewModel) 
        where TViewModel : BaseVM
    {
        return new NavigateCommand<TViewModel>(FactoryNavigationService.CreateNavigationService(navigationStore, creatorViewModel));
    }

    public static NavigateCommandParameter<TParameter, TViewModel> CreateNavigateCommandParameter<TParameter, TViewModel>(
        NavigationStore navigationStore, 
        Func<TParameter, TViewModel> creatorViewModel) 
        where TViewModel : BaseVM
    {
        return new NavigateCommandParameter<TParameter, TViewModel>(FactoryNavigationService.CreateParameterNavigationService(navigationStore, creatorViewModel));
    }
}
