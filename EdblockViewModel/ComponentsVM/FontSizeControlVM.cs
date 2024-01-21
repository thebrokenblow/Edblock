using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM;

public class FontSizeControlVM
{
    public List<double> FontSizes { get; init; }

    public double fontSize;
    public double FontSize
    {
        get => fontSize;
        set
        {
            fontSize = value;
            SetFontSize();
        }
    }

    private readonly List<BlockSymbolVM> _selectedBlockSymbols;
    public FontSizeControlVM(List<BlockSymbolVM> selectedBlockSymbols)
    {
        _selectedBlockSymbols = selectedBlockSymbols;

        FontSizes = new();

        for (int fontSize = 2; fontSize <= 50; fontSize++)
        {
            FontSizes.Add(fontSize);
        }
    }

    public void SetFontSize(BlockSymbolVM selectedBlockSymbol)
    {
        if (selectedBlockSymbol is IHasTextFieldVM symbolHasTextFieldVM)
        {
            symbolHasTextFieldVM.TextFieldSymbolVM.FontSize = fontSize;
        }
    }

    private void SetFontSize()
    {
        foreach (var selectedBlockSymbol in _selectedBlockSymbols)
        {
            SetFontSize(selectedBlockSymbol);
        }
    }
}