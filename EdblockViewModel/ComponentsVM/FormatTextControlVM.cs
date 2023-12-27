using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.ComponentsVM;

public class FormatTextControlVM
{
    private const string fontWeightBold = "Bold";
    private const string fontWeightNormal = "Normal";
    private string? currentFontWeight;

    private bool isTextBold;
    public bool IsTextBold
    {
        get => isTextBold;
        set
        {
            isTextBold = value;

            if (isTextBold)
            {
                currentFontWeight = fontWeightBold;
            }
            else
            {
                currentFontWeight = fontWeightNormal;
            }

            SetFontText();
        }
    }

    
    private const string fontStyleItalic = "Italic";
    private const string fontStyleNormal = "Normal";
    private string? currentFontStyle;

    private bool isFormatItalic;
    public bool IsFormatItalic
    {
        get => isFormatItalic;
        set
        {
            isFormatItalic = value;

            if (isFormatItalic)
            {
                currentFontStyle = fontStyleItalic;
            }
            else
            {
                currentFontStyle= fontStyleNormal;
            }

            SetFontText();
        }
    }

    private const string textDecorationsNone = "None";
    private const string textDecorationsUnderline = "Underline";
    private string? currentTextDecorations;

    private bool isFormatUnderline;
    public bool IsFormatUnderline
    {
        get => isFormatUnderline;
        set
        { 
            isFormatUnderline = value;

            if (isFormatUnderline)
            {
                currentTextDecorations = textDecorationsUnderline;
            }
            else 
            {
                currentTextDecorations = textDecorationsNone;
            }

            SetFontText();
        }
    } 

    private readonly List<BlockSymbolVM> _selectedBlockSymbols;
    public FormatTextControlVM(List<BlockSymbolVM> selectedBlockSymbols)
    {
        _selectedBlockSymbols = selectedBlockSymbols;
    }

    public void SetFontText(BlockSymbolVM selectedBlockSymbol)
    {
        selectedBlockSymbol.TextFieldVM.FontWeight = currentFontWeight;
        selectedBlockSymbol.TextFieldVM.FontStyle = currentFontStyle;
        selectedBlockSymbol.TextFieldVM.TextDecorations = currentTextDecorations;

    }

    private void SetFontText()
    {
        foreach (var selectedBlockSymbol in _selectedBlockSymbols)
        {
            SetFontText(selectedBlockSymbol);
        }
    }
}
