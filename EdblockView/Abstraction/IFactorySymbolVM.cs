using EdblockViewModel;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.Abstraction;

internal interface IFactorySymbolVM
{
    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM);
}