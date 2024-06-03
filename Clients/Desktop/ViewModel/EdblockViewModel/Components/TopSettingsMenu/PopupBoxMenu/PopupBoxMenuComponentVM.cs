using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

namespace EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu;

public class PopupBoxMenuComponentVM(
    ILineStateStandardComponentVM lineStateStandardComponentVM,
    IScaleAllSymbolComponentVM scaleAllSymbolComponentVM) : IPopupBoxMenuComponentVM
{
    public ILineStateStandardComponentVM LineStateStandardComponentVM { get; } =
        lineStateStandardComponentVM;

    public IScaleAllSymbolComponentVM ScaleAllSymbolComponentVM { get; } =
        scaleAllSymbolComponentVM;
}