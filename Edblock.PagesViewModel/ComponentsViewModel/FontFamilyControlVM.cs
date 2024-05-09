using System.Collections.Generic;
using Edblock.SymbolsViewModel.Core;
using EdblockViewModel.AbstractionsVM;

namespace Edblock.PagesViewModel.ComponentsViewModel;

public class FontFamilyControlVM(List<BlockSymbolViewModel> selectedBlockSymbols)
{
    public List<string> FontFamilys { get; init; } =
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

    public void SetFontFamily(BlockSymbolViewModel selectedBlockSymbol)
    {
        if (selectedBlockSymbol is IHasTextField symbolHasTextFieldVM)
        {
            symbolHasTextFieldVM.TextFieldSymbol.FontFamily = fontFamily;
        }
    }

    private void SetFontFamily()
    {
        foreach (var selectedBlockSymbol in selectedBlockSymbols)
        {
            SetFontFamily(selectedBlockSymbol);
        }
    }
}