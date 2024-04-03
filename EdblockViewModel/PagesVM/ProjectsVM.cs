﻿using System.Windows.Input;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.CommandsVM.FactoriesVM;

namespace EdblockViewModel.PagesVM;

public class ProjectsVM(NavigationStore navigationStoreMainWindow, NavigationStore navigationStoreMenu) : BaseVM
{
    public ICommand NavigateToEditor { get; init; } =
        FactoryNavigateCommand.CreateNavigateCommand(
            navigationStoreMainWindow,
            () => new EditorVM(navigationStoreMainWindow, navigationStoreMenu));
}