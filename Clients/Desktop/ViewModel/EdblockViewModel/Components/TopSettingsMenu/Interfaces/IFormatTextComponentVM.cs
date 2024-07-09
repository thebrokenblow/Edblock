using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface IFormatTextComponentVM
{
    bool IsTextBold { get; set; }
    bool IsFormatItalic { get; set; }
    bool IsFormatUnderline { get; set; }

    void SetFontText(IHasTextFieldVM selectedSymbolHasTextField);
}
