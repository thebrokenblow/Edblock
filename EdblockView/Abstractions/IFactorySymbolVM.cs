using EdblockViewModel.PagesVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.Abstractions;

internal interface IFactorySymbolVM
{
    public BlockSymbolVM CreateBlockSymbolVM(EditorVM editorVM);
}