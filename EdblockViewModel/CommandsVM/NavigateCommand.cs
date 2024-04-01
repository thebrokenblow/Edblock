using EdblockViewModel.CoreVM;
using EdblockViewModel.ServicesVM;

namespace EdblockViewModel.CommandsVM;

public class NavigateCommand<TViewModel>(INavigationService<TViewModel> navigationService) : CommandBase where TViewModel : BaseVM
{
    public override void Execute(object? parameter = null)
    {
        navigationService.Navigate();
    }
}