using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu;

public class FormatVerticalAlignComponentVM(IListCanvasSymbolsComponentVM listCanvasSymbolsVM) : IFormatVerticalAlignComponentVM
{
    private const int defaultIndexFormatAlign = 0;
    public int PreviousIndexFormatVerticalAlign { get; set; } = defaultIndexFormatAlign;

    private int indexFormatVerticalAlign = defaultIndexFormatAlign;
    public int IndexFormatVerticalAlign
    {
        get => indexFormatVerticalAlign;
        set
        {
            PreviousIndexFormatVerticalAlign = indexFormatVerticalAlign;
            indexFormatVerticalAlign = value;
            SetFormatVerticalAlignment();
        }
    }

    private readonly Dictionary<int, string> formatVerticalAlignByIndex = new()
    {
        { 0, "Bottom" },
        { 1, "Center" },
        { 2, "Top" }
    };

    public void SetFormatVerticalAlignment(IHasTextFieldVM selectedSymbolHasTextField)
    {
        var textFieldSymbolVM = selectedSymbolHasTextField.TextFieldSymbolVM;

        if (indexFormatVerticalAlign == -1)
        {
            textFieldSymbolVM.VerticalAlign = formatVerticalAlignByIndex[PreviousIndexFormatVerticalAlign];
        }
        else
        {
            textFieldSymbolVM.VerticalAlign = formatVerticalAlignByIndex[indexFormatVerticalAlign];
        }
    }

    private void SetFormatVerticalAlignment()
    {
        foreach (var selectedSymbolHasTextField in listCanvasSymbolsVM.SelectedSymbolsHasTextField)
        {
            SetFormatVerticalAlignment(selectedSymbolHasTextField);
        }
    }
}