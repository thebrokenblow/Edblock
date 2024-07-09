using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface ITopSettingsMenuComponentVM
{
    IColorSymbolComponentVM ColorSymbolComponent { get; }
    IFontFamilyComponentVM FontFamilyComponentVM { get; }
    IFontSizeComponentVM FontSizeComponentVM { get; }
    IFormatTextComponentVM FormatTextComponentVM { get; }
    ITextAlignmentComponentVM TextAlignmentComponentVM { get; }
    IFormatVerticalAlignComponentVM FormatVerticalAlignComponentVM { get; }
    IPopupBoxMenuComponentVM PopupBoxMenuComponentVM { get; }
    ICanvasSymbolsComponentVM CanvasSymbolsComponentVM { get; }
    IFontSizeSubject<int> FontSizeSubject { get; }
}