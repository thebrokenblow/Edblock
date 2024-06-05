using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu;

public class FormatTextComponentVM(IListCanvasSymbolsComponentVM listCanvasSymbolsVM) : IFormatTextComponentVM
{
    private const string fontWeightBold = "Bold";
    private const string fontWeightNormal = "Normal";

    private string? currentFontWeight;

    private bool isTextBold;

    private int selectedIndex;
    public int SelectedIndex
    {
        get => selectedIndex;
        set
        {
            selectedIndex = value;
        }
    }
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
                currentFontStyle = fontStyleNormal;
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

    public void SetFontText(IHasTextFieldVM selectedSymbolHasTextField)
    {
        var textFieldSymbolVM = selectedSymbolHasTextField.TextFieldSymbolVM;

        textFieldSymbolVM.FontWeight = currentFontWeight;
        textFieldSymbolVM.FontStyle = currentFontStyle;
        textFieldSymbolVM.TextDecorations = currentTextDecorations;
    }

    private void SetFontText()
    {
        foreach (var selectedSymbolHasTextField in listCanvasSymbolsVM.SelectedSymbolsHasTextField)
        {
            SetFontText(selectedSymbolHasTextField);
        }
    }
}