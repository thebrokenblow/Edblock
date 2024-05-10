using Edblock.PagesViewModel.Components;
using Edblock.PagesViewModel.Core;
using Edblock.PagesViewModel.Service;
using System.Collections.ObjectModel;

namespace Edblock.PagesViewModel.Pages;

public class ProjectsViewModel(INavigationService navigationService) : BaseViewModel
{
    public ObservableCollection<ProjectViewModel> Projects { get; } = [];
    public RelayCommand NavigateToEditor { get; set; } =
        FactoryNavigateService<EditorViewModel>.Create(navigationService, true);
}