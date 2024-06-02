using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;

public interface IFormatTextComponentVM
{
    public int SelectedIndex { get; set; }
    public bool IsTextBold { get; set; }
    public bool IsFormatItalic { get; set; }
    public bool IsFormatUnderline { get; set; }

    public void SetFontText(IHasTextFieldVM selectedSymbolHasTextField);
}
