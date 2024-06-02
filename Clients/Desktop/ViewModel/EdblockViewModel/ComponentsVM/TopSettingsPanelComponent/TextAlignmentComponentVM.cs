using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols.Interfaces;
using EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;

namespace EdblockViewModel.ComponentsVM.TopSettingsPanelComponent;

public class TextAlignmentComponentVM(IListCanvasSymbolsVM listCanvasSymbolsVM) : ITextAlignmentComponentVM
{
    private const int defaultIndexFormatAlign = 1;
    public int PreviousIndexFormatAlign { get; set; } = defaultIndexFormatAlign;

    private int indexFormatAlign = defaultIndexFormatAlign;
    public int IndexFormatAlign
    {
        get => indexFormatAlign;
        set
        {
            PreviousIndexFormatAlign = indexFormatAlign;
            indexFormatAlign = value;
            SetFormatAlignment();
        }
    }

    private readonly Dictionary<int, string> textAlignmentByIndex = new()
    {
        { 0, "Left" },
        { 1, "Center" },
        { 2, "Right" },
        { 3, "Justify" }
    };

    public void SetFormatAlignment(IHasTextFieldVM selectedSymbolHasTextField)
    {
        var textFieldSymbolVM = selectedSymbolHasTextField.TextFieldSymbolVM;

        if (indexFormatAlign == -1)
        {
            textFieldSymbolVM.TextAlignment = textAlignmentByIndex[PreviousIndexFormatAlign];
        }
        else
        {
            textFieldSymbolVM.TextAlignment = textAlignmentByIndex[indexFormatAlign];
        }
    }

    private void SetFormatAlignment()
    {
        foreach (var selectedSymbolHasTextField in listCanvasSymbolsVM.SelectedSymbolsHasTextField)
        {
            SetFormatAlignment(selectedSymbolHasTextField);
        }
    }
}