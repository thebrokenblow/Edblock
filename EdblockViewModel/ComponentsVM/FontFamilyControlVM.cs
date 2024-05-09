using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM;

public class FontFamilyControlVM
{
    public List<string> FontFamilys { get; init; }

    public string? fontFamily;
    public string? FontFamily 
    {
        get => fontFamily;
        set
        {
            fontFamily = value;
            SetFontFamily();
        }
    }

    private readonly List<BlockSymbolVM> _selectedBlockSymbols;
    public FontFamilyControlVM(List<BlockSymbolVM> selectedBlockSymbols)
    {
        _selectedBlockSymbols = selectedBlockSymbols;

        FontFamilys = new()
        {
            "Times New Roman"
        };
    }

    public void SetFontFamily(BlockSymbolVM selectedBlockSymbol) 
    {
        if (selectedBlockSymbol is IHasTextFieldVM symbolHasTextFieldVM)
        {
            symbolHasTextFieldVM.TextFieldSymbolVM.FontFamily = fontFamily;
        }
    }

    private void SetFontFamily()
    {
        foreach (var selectedBlockSymbol in _selectedBlockSymbols)
        {
            SetFontFamily(selectedBlockSymbol);
        }    
    }
}