using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu;

public class FontSizeComponentVM : IFontSizeComponentVM
{
    public List<double> FontSizes { get; } = [];

    private const int defaultFontSize = 12;
    public double selectedFontSize = defaultFontSize;
    public double SelectedFontSize
    {
        get => selectedFontSize;
        set
        {
            selectedFontSize = value;
            SetFontSize();
        }
    }

    private const int minFontSize = 2;
    private const int maxFontSize = 50;

    private readonly List<IHasTextFieldVM> selectedSymbolsHasTextField;
    public FontSizeComponentVM(IListCanvasSymbolsComponentVM listCanvasSymbolsVM)
    {
        selectedSymbolsHasTextField = listCanvasSymbolsVM.SelectedSymbolsHasTextField;

        for (int fontSize = minFontSize; fontSize <= maxFontSize; fontSize++)
        {
            FontSizes.Add(fontSize);
        }
    }

    public void SetFontSize(IHasTextFieldVM selectedSymbolHasTextField)
    {
        selectedSymbolHasTextField.TextFieldSymbolVM.FontSize = selectedFontSize;
    }

    private void SetFontSize()
    {
        foreach (var selectedSymbolHasTextField in selectedSymbolsHasTextField)
        {
            SetFontSize(selectedSymbolHasTextField);
        }
    }
}