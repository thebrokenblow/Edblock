using System.Collections.Generic;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM;

public class TextAlignmentControlVM(List<BlockSymbolVM> selectedBlockSymbols)
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

    public void SetFormatAlignment(BlockSymbolVM selectedBlockSymbol)
    {
        if (selectedBlockSymbol is IHasTextFieldVM symbolHasTextFieldVM)
        {
            if (indexFormatAlign == -1)
            {
                symbolHasTextFieldVM.TextFieldSymbolVM.TextAlignment = textAlignmentByIndex[PreviousIndexFormatAlign];
            }
            else
            {
                symbolHasTextFieldVM.TextFieldSymbolVM.TextAlignment = textAlignmentByIndex[indexFormatAlign];
            }
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