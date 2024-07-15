using EdblockViewModel.Components.Subjects.Interfaces;
using EdblockViewModel.Components.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components;

public class TopSettingsMenuVM(
    IColorSubject colorSubject,
    IFontFamilySubject fontFamilySubject,
    IFontSizeSubject<int> fontSizeSubject,
    IFormatTextSubject formatTextSubject,
    ITextAlignmentSubject textAlignmentSubject,
    IPopupBoxMenuComponentVM popupBoxMenuComponentVM) : ITopSettingsMenuComponentVM
{
    public IColorSubject ColorSubject { get; } = colorSubject;
    public IFontFamilySubject FontFamilySubject { get; } = fontFamilySubject;
    public IFontSizeSubject<int> FontSizeSubject { get; } = fontSizeSubject;
    public IFormatTextSubject FormatTextSubject { get; } = formatTextSubject;
    public ITextAlignmentSubject TextAlignmentSubject { get; } = textAlignmentSubject;
    public IPopupBoxMenuComponentVM PopupBoxMenuComponentVM { get; } = popupBoxMenuComponentVM;
    public IProject? Project { get; set; }
}