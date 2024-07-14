using EdblockViewModel.Components.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.PopupBoxMenu;

public class PopupBoxMenuComponentVM(
    ILineStateStandardComponentVM lineStateStandardComponentVM,
    IScaleAllSymbolComponentVM scaleAllSymbolComponentVM) : IPopupBoxMenuComponentVM
{
    public ILineStateStandardComponentVM LineStateStandardComponentVM { get; } =
        lineStateStandardComponentVM;

    public IScaleAllSymbolComponentVM ScaleAllSymbolComponentVM { get; } =
        scaleAllSymbolComponentVM;
}