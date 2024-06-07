using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface IFormatVerticalAlignComponentVM
{
    int PreviousIndexFormatVerticalAlign { get; set; }
    int IndexFormatVerticalAlign {  get; set; }
    void SetFormatVerticalAlignment(IHasTextFieldVM selectedSymbolHasTextField);
}