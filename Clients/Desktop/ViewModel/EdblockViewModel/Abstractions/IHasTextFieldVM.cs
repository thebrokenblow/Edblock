using EdblockViewModel.Symbols.ComponentsSymbolsVM;

namespace EdblockViewModel.Abstractions;

public interface IHasTextFieldVM
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
}