using EdblockViewModel.CoreVM;

namespace EdblockViewModel.ServicesVM;

public interface INavigationService<TViewModel> where TViewModel : BaseVM
{
    void Navigate();
}