using EdblockViewModel.Symbols.ComponentsSymbolsVM;

namespace EdblockViewModel.AbstractionsVM;

public interface IHasTextFieldVM
{
    public TextFieldSymbolVM TextFieldSymbolVM { get; init; }
    public double GetTextFieldWidth();
    public double GetTextFieldHeight();
    public double GetTextFieldLeftOffset();
    public double GetTextFieldTopOffset();
}