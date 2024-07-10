using Edblock.SymbolsSerialization.Symbols;
using EdblockViewModel.Symbols.SwitchCaseConditionSymbolsVM;

namespace EdblockViewModel.Symbols.Abstractions;

public interface IFactoryBlockSymbolVM
{
    BlockSymbolVM CreateBlockSymbolVM(BlockSymbolSerializable blockSymbolSerializable);
    SwitchCaseSymbolVM CreateBlockSymbolVM(SwitchCaseSymbolsSerializable switchCaseSymbolsSerializable);
}