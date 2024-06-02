using EdblockViewModel.AbstractionsVM;
namespace EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;

public interface ITextAlignmentComponentVM
{
    public int PreviousIndexFormatAlign { get; set; }
    public int IndexFormatAlign { get; set; }

    public void SetFormatAlignment(IHasTextFieldVM selectedSymbolHasTextField);
}