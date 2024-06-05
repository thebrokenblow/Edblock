using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Pages;

namespace EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu;

public class PopupBoxMenuComponentVM(
    ILineStateStandardComponentVM lineStateStandardComponentVM,
    IScaleAllSymbolComponentVM scaleAllSymbolComponentVM,
    EditorVM editorVM) : IPopupBoxMenuComponentVM
{
    public ILineStateStandardComponentVM LineStateStandardComponentVM { get; } =
        lineStateStandardComponentVM;

    public IScaleAllSymbolComponentVM ScaleAllSymbolComponentVM { get; } =
        scaleAllSymbolComponentVM;

    public EditorVM EditorVM { get; } = editorVM;
}