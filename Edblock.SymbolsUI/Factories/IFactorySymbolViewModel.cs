using Edblock.PagesViewModel.Pages;
using Edblock.SymbolsViewModel.Core;

namespace Edblock.SymbolsUI.Factories;

internal interface IFactorySymbolViewModel
{
    BlockSymbolViewModel CreateBlockSymbolViewModel(EditorViewModel editorViewModel);
}