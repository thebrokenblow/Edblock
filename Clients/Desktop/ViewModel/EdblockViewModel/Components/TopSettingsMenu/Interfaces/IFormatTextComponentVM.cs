using EdblockViewModel.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface IFormatTextComponentVM
{
    int SelectedIndex { get; set; }
    bool IsTextBold { get; set; }
    bool IsFormatItalic { get; set; }
    bool IsFormatUnderline { get; set; }

    void SetFontText(IHasTextFieldVM selectedSymbolHasTextField);
}
