using EdblockViewModel.Symbols.ComponentsSymbolsVM;

namespace EdblockViewModel.AbstractionsVM;

public interface IHasTextFieldVM
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
}