using Edblock.PagesViewModel.Core;

namespace Edblock.PagesViewModel.Service;

public class FactoryNavigateService<T> where T : BaseViewModel
{
    public static RelayCommand Create(INavigationService navigationService, bool isCanExecute) =>
        new(obj => navigationService.NavigateTo<T>(), obj => isCanExecute);
}