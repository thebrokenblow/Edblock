using EdblockViewModel.CoreVM;

namespace EdblockViewModel.Service;

public class FactoryNavigateService<T> where T : BaseViewModel
{
    public static RelayCommand Create(INavigationService navigationService, bool isCanExecute) =>
        new(obj => navigationService.NavigateTo<T>(), obj => isCanExecute);
}