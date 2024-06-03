using EdblockViewModel.Services.Interfaces;
using EdblockViewModel.Services.Factories.Interfaces;
using EdblockViewModel.Core;

namespace EdblockViewModel.Services.Factories;

public class FactoryNavigationService : IFactoryNavigationService
{
    public RelayCommand Create<T>(INavigationService navigationService, bool isCanExecute) where T : BaseViewModel =>
        new(obj => navigationService.NavigateTo<T>(), obj => isCanExecute);
}