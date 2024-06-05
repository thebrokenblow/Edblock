using EdblockViewModel.Pages;

namespace EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;

public interface IPopupBoxMenuComponentVM
{
    ILineStateStandardComponentVM LineStateStandardComponentVM { get; }
    IScaleAllSymbolComponentVM ScaleAllSymbolComponentVM { get; }
    EditorVM EditorVM { get; }
}