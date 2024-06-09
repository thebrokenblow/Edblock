using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu;

public class FontFamilyComponentVM(IListCanvasSymbolsComponentVM listCanvasSymbolsVM) : IFontFamilyComponentVM
{
    public List<string> FontFamilys { get; } =
    [
        defaultFontFamily,
        "Times New Roman",
    ];

    private const string defaultFontFamily = "Segoe UI";

    public string? selectedFontFamily = defaultFontFamily;
    public string? SelectedFontFamily
    {
        get => selectedFontFamily;
        set
        {
            selectedFontFamily = value;
            SetFontFamily();
        }
    }

    public void SetFontFamily(IHasTextFieldVM selectedSymbolHasTextField)
    {
        selectedSymbolHasTextField.TextFieldSymbolVM.FontFamily = selectedFontFamily;
    }

    private void SetFontFamily()
    {
        foreach (var selectedSymbolHasTextField in listCanvasSymbolsVM.SelectedSymbolsHasTextField)
        {
            SetFontFamily(selectedSymbolHasTextField);
        }
    }
}