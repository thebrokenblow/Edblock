using System.Collections.Generic;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu;

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

    private readonly IListCanvasSymbolsComponentVM _listCanvasSymbolsVM;
    public FontSizeComponentVM(IListCanvasSymbolsComponentVM listCanvasSymbolsVM)
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