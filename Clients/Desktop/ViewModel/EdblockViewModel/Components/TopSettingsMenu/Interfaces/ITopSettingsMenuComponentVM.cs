using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface ITopSettingsMenuComponentVM
{
    public IFontFamilyComponentVM FontFamilyComponentVM { get; }
    public IFontSizeComponentVM FontSizeComponentVM { get; }
    public IFormatTextComponentVM FormatTextComponentVM { get; }
    public ITextAlignmentComponentVM TextAlignmentComponentVM { get; }
    public IPopupBoxMenuComponentVM PopupBoxMenuComponentVM { get;}
}