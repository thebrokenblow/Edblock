using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu;

public class TopSettingsMenuComponentVM(
    IFontFamilyComponentVM fontFamilyComponentVM,
    IFontSizeComponentVM fontSizeComponentVM,
    IFormatTextComponentVM formatTextComponentVM,
    ITextAlignmentComponentVM textAlignmentComponentVM,
    IPopupBoxMenuComponentVM popupBoxMenuComponentVM) : ITopSettingsMenuComponentVM
{
    public IFontFamilyComponentVM FontFamilyComponentVM { get; } = fontFamilyComponentVM;
    public IFontSizeComponentVM FontSizeComponentVM { get; } = fontSizeComponentVM;
    public IFormatTextComponentVM FormatTextComponentVM { get; } = formatTextComponentVM;
    public ITextAlignmentComponentVM TextAlignmentComponentVM { get; } = textAlignmentComponentVM;
    public IPopupBoxMenuComponentVM PopupBoxMenuComponentVM { get; } = popupBoxMenuComponentVM;
}