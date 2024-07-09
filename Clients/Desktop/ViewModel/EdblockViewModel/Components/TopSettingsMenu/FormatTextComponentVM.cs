using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Core;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu;

public class FormatTextComponentVM(IListCanvasSymbolsComponentVM listCanvasSymbolsVM) : ObservableObject, IFormatTextComponentVM
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
            if (isTextBold && value)
            {
                isTextBold = false;
            }
            else
            {
                isTextBold = true;
            }

            OnPropertyChanged();

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
            if (isFormatItalic && value)
            {
                isFormatItalic = false;
            }
            else
            {
                isFormatItalic = true;
            }

            OnPropertyChanged();

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
            if (isFormatUnderline && value)
            {
                isFormatUnderline = false;
            }
            else
            {
                isFormatUnderline = true;
            }

            OnPropertyChanged();

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