using EdblockViewModel.CoreVM;

namespace EdblockViewModel.ServicesVM;

public interface IParameterNavigationService<TParametr, TViewModel> where TViewModel : BaseVM
{
    void Navigate(TParametr parametr);
}