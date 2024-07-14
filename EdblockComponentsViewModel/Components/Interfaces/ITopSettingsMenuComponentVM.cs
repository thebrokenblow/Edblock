using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockComponentsViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockComponentsViewModel.Components.Interfaces;

public interface ITopSettingsMenuComponentVM
{
    public IColorSubject ColorSubject { get; }
    public IFontFamilySubject FontFamilySubject { get; }
    public IFontSizeSubject<int> FontSizeSubject { get; }
    public IFormatTextSubject FormatTextSubject { get; }
    public ITextAlignmentSubject TextAlignmentSubject { get; }
    public IPopupBoxMenuComponentVM PopupBoxMenuComponentVM { get; }
}