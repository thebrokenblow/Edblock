using EdblockViewModel;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockView.Abstraction;

internal interface IFactorySymbolVM
{
    public BlockSymbolVM CreateBlockSymbolVM(EdblockVM edblockVM);
}