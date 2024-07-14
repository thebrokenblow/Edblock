using EdblockViewModel.Core;
using EdblockViewModel.Components.ListSymbols.Interfaces;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Project.Interfaces;
using System.Threading.Tasks;
using EdblockViewModel.Components.Interfaces;

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

    private bool isLoadingProject;
    public bool IsLoadingProject
    {
        get => isLoadingProject;
        set
        {
            isLoadingProject = value;
            OnPropertyChanged();
        }
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

    public async Task LoadProject(string filePath)
    {
        //IsLoadingProject = true;   
        await _projectVM.LoadProject(filePath);
        //IsLoadingProject = false;
    }
}