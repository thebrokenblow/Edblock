using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu.Interfaces;

public interface ITopSettingsMenuComponentVM
{
    IFontFamilyComponentVM FontFamilyComponentVM { get; }
    IFontSizeComponentVM FontSizeComponentVM { get; }
    IFormatTextComponentVM FormatTextComponentVM { get; }
    ITextAlignmentComponentVM TextAlignmentComponentVM { get; }
    IPopupBoxMenuComponentVM PopupBoxMenuComponentVM { get; }
    ICanvasSymbolsVM CanvasSymbolsVM { get; }
}