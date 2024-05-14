using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.Abstractions;

internal interface IFactorySymbolVM
{
    BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM);
}