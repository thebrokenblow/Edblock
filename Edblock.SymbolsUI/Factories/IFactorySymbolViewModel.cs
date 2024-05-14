using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

namespace Edblock.SymbolsUI.Factories;

public interface IFactorySymbolViewModel
{
    BlockSymbolVM CreateBlockSymbolViewModel(EditorVM editorVM);
}