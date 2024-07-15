using EdblockViewModel.Components.Subjects.Interfaces;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.Interfaces;

public interface ITopSettingsMenuComponentVM
{
    IColorSubject ColorSubject { get; }
    IFontFamilySubject FontFamilySubject { get; }
    IFontSizeSubject<int> FontSizeSubject { get; }
    IFormatTextSubject FormatTextSubject { get; }
    ITextAlignmentSubject TextAlignmentSubject { get; }
    IPopupBoxMenuComponentVM PopupBoxMenuComponentVM { get; }
    IProject? Project { get; set; }
}