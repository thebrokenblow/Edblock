using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM;

public class TextAlignmentControlVM(List<BlockSymbolVM> selectedBlockSymbols)
{
    private const int defaultIndexFormatAlign = 1;
    private int PreviousIndexFormatAlign { get; set; } = defaultIndexFormatAlign;

    private int indexFormatAlign = defaultIndexFormatAlign;
    public int IndexFormatAlign
    {
        get => indexFormatAlign;
        set
        {
            if (value != -1)
            {
                indexFormatAlign = value;
                SetFormatAlignment();
            }
        }
    }

    private readonly Dictionary<int, string> textAlignmentByIndex = new()
    {
        { 0, "Left" },
        { 1, "Justify" },
        { 2, "Center" },
        { 3, "Right" }
    };

    public void SetFormatAlignment(BlockSymbolVM selectedBlockSymbol)
    {
        if (selectedBlockSymbol is IHasTextFieldVM symbolHasTextFieldVM)
        {
            symbolHasTextFieldVM.TextFieldSymbolVM.TextAlignment = textAlignmentByIndex[IndexFormatAlign];
        }
    }

    private void SetFormatAlignment()
    {
        foreach (var selectedBlockSymbol in selectedBlockSymbols)
        {
            SetFormatAlignment(selectedBlockSymbol);
        }
    }
}