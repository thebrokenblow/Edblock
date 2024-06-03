using EdblockViewModel.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface ITextAlignmentComponentVM
{
    int PreviousIndexFormatAlign { get; set; }
    int IndexFormatAlign { get; set; }
    void SetFormatAlignment(IHasTextFieldVM selectedSymbolHasTextField);
}