using EdblockViewModel.CoreVM;

namespace EdblockViewModel.Service;

public interface INavigationService
{
    BaseViewModel? CurrentViewModel { get; }
    void NavigateTo<T>() where T : BaseViewModel;
}