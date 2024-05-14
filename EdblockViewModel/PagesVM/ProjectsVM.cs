using System.Windows.Input;
using System.Collections.ObjectModel;
using EdblockViewModel.CoreVM;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Service;

namespace EdblockViewModel.PagesVM;

public class ProjectsVM(INavigationService navigationService) : BaseViewModel
{
    public ICommand NavigateToEditor { get; } = 
        FactoryNavigateService<EditorVM>.Create(navigationService, true);

    public ObservableCollection<CardProjectVM> Projects { get; } =
        [
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description = "Уравнение ax + b > 0",
                Project = ""
            }
        ];
}