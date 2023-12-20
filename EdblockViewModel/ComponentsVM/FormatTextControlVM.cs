using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.ComponentsVM;

public class FormatTextControlVM
{
    private const string fontWeightBold = "Bold";
    private const string fontWeightNormal = "Normal";
    private string? fontWeight;
    private bool isTextBold;
    public bool IsTextBold
    {
        get => isTextBold;
        set
        {
            isTextBold = value;

            if (isTextBold)
            {
                fontWeight = fontWeightBold;
            }
            else
            {
                fontWeight = fontWeightNormal;
            }

            SetFontText();
        }
    }

    private readonly List<BlockSymbolVM> _selectedBlockSymbols;
    public FormatTextControlVM(List<BlockSymbolVM> selectedBlockSymbols)
    {
        _selectedBlockSymbols = selectedBlockSymbols;
    }

    public void SetFontText(BlockSymbolVM blockSymbolVM)
    {
        blockSymbolVM.TextField.FontWeight = fontWeight;
    }

    private void SetFontText()
    {
        foreach (var selectedBlockSymbol in _selectedBlockSymbols)
        {
            selectedBlockSymbol.TextField.FontWeight = fontWeight;
        }
    }
}
