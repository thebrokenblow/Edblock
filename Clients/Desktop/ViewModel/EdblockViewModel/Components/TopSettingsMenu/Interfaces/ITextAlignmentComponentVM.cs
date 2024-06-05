using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface ITextAlignmentComponentVM
{
    int IndexFormatAlign { get; set; }
    int PreviousIndexFormatAlign { get; set; }
    void SetFormatAlignment(IHasTextFieldVM selectedSymbolHasTextField);
}