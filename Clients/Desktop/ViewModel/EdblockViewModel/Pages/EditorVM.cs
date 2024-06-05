using EdblockViewModel.Core;
using EdblockViewModel.Components.ListSymbols.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;

namespace EdblockViewModel.Pages;

public class EditorVM : BaseViewModel
{
    public static int CellWidthPanelSymbols
    {
        get => cellWidthPanelSymbols;
    }

    public static int CellHeightTopSettingsPanel
    {
        get => cellHeightTopSettingsPanel;
    }

    public IListSymbolsVM ListSymbolsVM { get; }
    public ICanvasSymbolsVM CanvasSymbolsVM { get; }
    public ITopSettingsMenuComponentVM TopSettingsMenuComponentVM { get; }

    private readonly ProjectVM projectVM;

    private const int cellHeightTopSettingsPanel = 60;
    private const int cellWidthPanelSymbols = 50;

    public EditorVM(
        IListSymbolsVM listSymbolsVM,
        ICanvasSymbolsVM canvasSymbolsVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM)
    {
        ListSymbolsVM = listSymbolsVM;
        CanvasSymbolsVM = canvasSymbolsVM;
        TopSettingsMenuComponentVM = topSettingsMenuComponentVM;

        CanvasSymbolsVM.ScalingCanvasSymbolsVM.HeightTopSettingsPanel = cellHeightTopSettingsPanel;
        CanvasSymbolsVM.ScalingCanvasSymbolsVM.WidthPanelSymbols = cellWidthPanelSymbols;

        projectVM = new(this);
    }

    public void SaveProject(string filePath)
    {
        projectVM.SaveProject(filePath);
    }

    public void LoadProject(string filePath)
    {
        projectVM.LoadProject(filePath);
    }
}