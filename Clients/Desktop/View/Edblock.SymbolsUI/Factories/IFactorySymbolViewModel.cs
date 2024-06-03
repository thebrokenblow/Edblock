using EdblockViewModel.Abstractions;
using EdblockViewModel.Pages;

namespace Edblock.SymbolsUI.Factories;

public interface IFactorySymbolViewModel
{
    BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM);
}