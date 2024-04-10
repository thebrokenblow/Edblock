using System.Windows.Input;
using System.Collections.ObjectModel;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.CommandsVM.FactoriesVM;

namespace EdblockViewModel.PagesVM;

public class ProjectsVM(NavigationStore navigationStoreMainWindow, NavigationStore navigationStoreMenu) : BaseVM
{
    public ICommand NavigateToEditor { get; init; } =
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new EditorVM(navigationStoreMainWindow, navigationStoreMenu));

    public ObservableCollection<CardProjectVM> Projects { get; init; } =
        [
        new()
            {
                Name = "Неравенство",
                Description="Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description="Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description="Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description="Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description="Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description="Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description="Уравнение ax + b > 0",
                Project = ""
            },
        new()
            {
                Name = "Неравенство",
                Description="Уравнение ax + b > 0",
                Project = ""
            }
        ];
}