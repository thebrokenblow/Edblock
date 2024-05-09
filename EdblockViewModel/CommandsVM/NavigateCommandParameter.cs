using EdblockViewModel.CoreVM;
using EdblockViewModel.ServicesVM;

namespace EdblockViewModel.CommandsVM;

public class NavigateCommandParameter<TParameter, TViewModel>(IParameterNavigationService<TParameter, TViewModel> parameterNavigationService) : CommandBase where TViewModel : BaseVM
{
    public override void Execute(object? parameter = null)
    {
        if (parameter is TParameter resultParameter)
        {
            parameterNavigationService.Navigate(resultParameter);
        }  
    }
}