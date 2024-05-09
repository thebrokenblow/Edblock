using EdblockViewModel.Symbols.ComponentsSymbolsVM;

namespace Edblock.SymbolsViewModel.Core;

public interface IHasTextField
{
    public TextFieldSymbol TextFieldSymbol { get; init; }
}