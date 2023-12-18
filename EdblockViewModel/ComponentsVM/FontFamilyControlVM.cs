using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.ComponentsVM;

public class FontFamilyControlVM
{
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
    }

    public void SetFontFamily(BlockSymbolVM blockSymbolVM) 
    {
        blockSymbolVM.TextField.FontFamily = fontFamily;
    }

    private void SetFontFamily()
    {
        foreach (var selectedBlockSymbol in _selectedBlockSymbols)
        {
            selectedBlockSymbol.TextField.FontFamily = fontFamily;
        }    
    }
}