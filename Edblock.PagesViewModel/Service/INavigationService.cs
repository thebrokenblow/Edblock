using Edblock.PagesViewModel.Core;

namespace Edblock.PagesViewModel.Service;

public interface INavigationService
{
    BaseViewModel? CurrentViewModel { get; }
    void NavigateTo<T>() where T : BaseViewModel;
}