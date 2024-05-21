using EdblockViewModel.CoreVM;
using EdblockViewModel.Services.Interfaces;

namespace EdblockViewModel.Services.Factories.Interfaces;

public interface IFactoryNavigationService
{
    RelayCommand Create<T>(INavigationService navigationService, bool isCanExecute) where T : BaseViewModel;
}