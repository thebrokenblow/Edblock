using System.Windows.Media;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu;

public class ColorSymbolComponentVM(IListCanvasSymbolsComponentVM listCanvasSymbolsVM) : IColorSymbolComponentVM
{
    private Color selectColor;
    public Color SelectColor 
    {
        get => selectColor;
        set
        {
            selectColor = value;
            SetFormatVerticalAlignment();
        }
    }

    private void SetFormatVerticalAlignment()
    {
        foreach (var selectedBlockSymbol in listCanvasSymbolsVM.SelectedBlockSymbolsVM)
        {
            selectedBlockSymbol.Color = selectColor.ToString();
        }
    }
}