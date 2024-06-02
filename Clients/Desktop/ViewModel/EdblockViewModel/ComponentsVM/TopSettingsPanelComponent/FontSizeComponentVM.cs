using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols.Interfaces;
using EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;

namespace EdblockViewModel.ComponentsVM.TopSettingsPanelComponent;

public class FontSizeComponentVM : IFontSizeComponentVM
{
    public List<double> FontSizes { get; } = [];

    public double fontSize;
    public double FontSize
    {
        get => fontSize;
        set
        {
            fontSize = value;
            SetFontSize();
        }
    }

    private const int minFontSize = 2;
    private const int maxFontSize = 50;

    private readonly IListCanvasSymbolsVM _listCanvasSymbolsVM;
    public FontSizeComponentVM(IListCanvasSymbolsVM listCanvasSymbolsVM)
    {
        _listCanvasSymbolsVM = listCanvasSymbolsVM;

        for (int fontSize = minFontSize; fontSize <= maxFontSize; fontSize++)
        {
            FontSizes.Add(fontSize);
        }
    }

    public void SetFontSize(IHasTextFieldVM selectedSymbolHasTextField)
    {
        selectedSymbolHasTextField.TextFieldSymbolVM.FontSize = fontSize;
    }

    private void SetFontSize()
    {
        foreach (var selectedSymbolHasTextField in _listCanvasSymbolsVM.SelectedSymbolsHasTextField)
        {
            SetFontSize(selectedSymbolHasTextField);
        }
    }
}