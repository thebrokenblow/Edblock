using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;
using System.Collections.ObjectModel;

namespace EdblockViewModel.PagesVM;

public class ProjectsVM(NavigationStore navigationStoreMainWindow, NavigationStore navigationStoreMenu) : BaseVM
{
    public ICommand NavigateToEditor { get; init; } =
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new EditorVM(navigationStoreMainWindow, navigationStoreMenu));

    public ObservableCollection<string> Projects { get; init; } =
        [
            "test",
            "test",
            "test",
            "test",
            "test",
            "test",
            "test",
            "test",
            "test",
            "test",
        ];
}