using EdblockViewModel.Core;
using EdblockViewModel.Components.ListSymbols.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.Interfaces;
using EdblockViewModel.Project.Interfaces;

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

    public IListSymbolsComponentVM ListSymbolsComponentVM { get; }
    public ICanvasSymbolsComponentVM CanvasSymbolsComponentVM { get; }
    public ITopSettingsMenuComponentVM TopSettingsMenuComponentVM { get; }

    private readonly IProjectVM _projectVM;

    private const int cellHeightTopSettingsPanel = 60;
    private const int cellWidthPanelSymbols = 50;

    public EditorVM(
        IProjectVM projectVM,
        IListSymbolsComponentVM listSymbolsComponentVM,
        ICanvasSymbolsComponentVM canvasSymbolsComponentVM,
        ITopSettingsMenuComponentVM topSettingsMenuComponentVM)
    {
        ListSymbolsComponentVM = listSymbolsComponentVM;
        CanvasSymbolsComponentVM = canvasSymbolsComponentVM;
        TopSettingsMenuComponentVM = topSettingsMenuComponentVM;

        CanvasSymbolsComponentVM.ScalingCanvasSymbolsVM.HeightTopSettingsPanel = cellHeightTopSettingsPanel;
        CanvasSymbolsComponentVM.ScalingCanvasSymbolsVM.WidthPanelSymbols = cellWidthPanelSymbols;

        _projectVM = projectVM;
    }

    public void SaveProject(string filePath)
    {
        _projectVM.SaveProject(filePath);
    }

    public void LoadProject(string filePath)
    {
        _projectVM.LoadProject(filePath);
    }
}