using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockComponentsViewModel.Components.Interfaces;
using EdblockComponentsViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockComponentsViewModel.Components;

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
}