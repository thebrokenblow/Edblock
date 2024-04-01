using Prism.Commands;
using EdblockViewModel.CoreVM;
using EdblockViewModel.StoresVM;
using EdblockViewModel.ComponentsVM;

namespace EdblockViewModel.PagesVM;

public class EditorVM : BaseVM
{
    public static int CellWidthPanelSymbols
    {
        get => cellWidthPanelSymbols;
    }

    public static int CellHeightTopSettingsPanel
    {
        get => cellHeightTopSettingsPanel;
    }

    public DelegateCommand ClickDelete { get; init; }
    public CanvasSymbolsVM CanvasSymbolsVM { get; init; } = new();
    public FontSizeControlVM FontSizeControlVM { get; init; }
    public FontFamilyControlVM FontFamilyControlVM { get; init; }
    public TextAlignmentControlVM TextAlignmentControlVM { get; init; }
    public FormatTextControlVM FormatTextControlVM { get; init; }
    public PopupBoxMenuVM PopupBoxMenuVM { get; init; }
    public ListSymbolsVM ListSymbolsVM { get; init; }

    private readonly ProjectVM projectVM;

    private const int cellHeightTopSettingsPanel = 60;
    private const int cellWidthPanelSymbols = 50;

    public EditorVM(NavigationStore navigationStoreMenu)
    {
        var selectedBlockSymbols = CanvasSymbolsVM.SelectedBlockSymbols;

        CanvasSymbolsVM.ScalingCanvasSymbolsVM.HeightTopSettingsPanel = cellHeightTopSettingsPanel;
        CanvasSymbolsVM.ScalingCanvasSymbolsVM.WidthPanelSymbols = cellWidthPanelSymbols;

        PopupBoxMenuVM = new(this);
        FontSizeControlVM = new(selectedBlockSymbols);
        FontFamilyControlVM = new(selectedBlockSymbols);
        FormatTextControlVM = new(selectedBlockSymbols);
        TextAlignmentControlVM = new(selectedBlockSymbols);

        projectVM = new(this);
        ListSymbolsVM = new(this);

        ClickDelete = new(CanvasSymbolsVM.DeleteSymbols);
    }

    public void SaveProject(string filePath)
    {
        projectVM.Save(filePath);
    }

    public void LoadProject(string filePath)
    {
        projectVM.Load(filePath);
    }
}