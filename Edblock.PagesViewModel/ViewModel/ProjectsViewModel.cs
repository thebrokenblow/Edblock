using Edblock.PagesViewModel.ComponentsViewModel;
using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;
using System.Collections.ObjectModel;

namespace Edblock.PagesViewModel.ViewModel;

public class ProjectsViewModel(INavigationService navigationService) : BaseViewModel
{
    public ObservableCollection<ProjectViewModel> Projects { get; set; } = [];

    public RelayCommand NavigateToEditor { get; } =
        FactoryNavigateService<AuthenticationViewModel>.Create(navigationService, true);
}