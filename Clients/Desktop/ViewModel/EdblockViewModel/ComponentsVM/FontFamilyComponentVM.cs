using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM;

public class FontFamilyComponentVM(List<BlockSymbolVM> selectedBlocksSymbols)
{
    public List<string> FontFamilys { get; } =
    [
        "Times New Roman"
    ];

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

    public void SetFontFamily(BlockSymbolVM selectedBlockSymbol) 
    {
        if (selectedBlockSymbol is IHasTextFieldVM symbolHasTextFieldVM)
        {
            symbolHasTextFieldVM.TextFieldSymbolVM.FontFamily = fontFamily;
        }
    }

    private void SetFontFamily()
    {
        foreach (var selectedBlockSymbol in selectedBlocksSymbols)
        {
            SetFontFamily(selectedBlockSymbol);
        }    
    }
}