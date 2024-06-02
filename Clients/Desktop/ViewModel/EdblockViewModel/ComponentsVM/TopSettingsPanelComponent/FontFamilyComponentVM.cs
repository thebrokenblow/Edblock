using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols.Interfaces;
using EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;

namespace EdblockViewModel.ComponentsVM.TopSettingsPanelComponent;

public class FontFamilyComponentVM(IListCanvasSymbolsVM listCanvasSymbolsVM) : IFontFamilyComponentVM
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

    public void SetFontFamily(IHasTextFieldVM selectedSymbolHasTextField)
    {
        selectedSymbolHasTextField.TextFieldSymbolVM.FontFamily = fontFamily;
    }

    private void SetFontFamily()
    {
        foreach (var selectedSymbolHasTextField in listCanvasSymbolsVM.SelectedSymbolsHasTextField)
        {
            SetFontFamily(selectedSymbolHasTextField);
        }
    }
}