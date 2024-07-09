using EdblockComponentsViewModel.Subjects;
using EdblockComponentsViewModel.Subjects.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu;

public class TopSettingsMenuComponentVM(
    IColorSymbolComponentVM colorSymbolComponentVM,
    IFontFamilyComponentVM fontFamilyComponentVM,
    IFontSizeComponentVM fontSizeComponentVM,
    IFormatTextComponentVM formatTextComponentVM,
    ITextAlignmentComponentVM textAlignmentComponentVM,
    IFormatVerticalAlignComponentVM formatVerticalAlignComponentVM,
    IPopupBoxMenuComponentVM popupBoxMenuComponentVM,
    ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
    IFontSizeSubject<int> fontSizeSubject) : ITopSettingsMenuComponentVM
{
    public IColorSymbolComponentVM ColorSymbolComponent { get; } = colorSymbolComponentVM;
    public IFontFamilyComponentVM FontFamilyComponentVM { get; } = fontFamilyComponentVM;
    public IFontSizeComponentVM FontSizeComponentVM { get; } = fontSizeComponentVM;
    public IFormatTextComponentVM FormatTextComponentVM { get; } = formatTextComponentVM;
    public ITextAlignmentComponentVM TextAlignmentComponentVM { get; } = textAlignmentComponentVM;
    public IPopupBoxMenuComponentVM PopupBoxMenuComponentVM { get; } = popupBoxMenuComponentVM;
    public ICanvasSymbolsComponentVM CanvasSymbolsComponentVM { get; } = canvasSymbolsComponentVM;
    public IFormatVerticalAlignComponentVM FormatVerticalAlignComponentVM { get; } = formatVerticalAlignComponentVM;
    public IFontSizeSubject<int> IFontSizeSubject { get; } = fontSizeSubject;
}