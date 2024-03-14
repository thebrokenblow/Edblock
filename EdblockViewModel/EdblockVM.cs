using Prism.Commands;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel;

public class EdblockVM
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

    private readonly ProjectVM projectVM;

    private const int cellHeightTopSettingsPanel = 60;
    private const int cellWidthPanelSymbols = 50;

    public EdblockVM()
    {
        var selectedBlockSymbols = CanvasSymbolsVM.SelectedBlockSymbols;

        CanvasSymbolsVM.ScalingCanvasSymbolsVM.HeightTopSettingsPanel = cellHeightTopSettingsPanel;
        CanvasSymbolsVM.ScalingCanvasSymbolsVM.WidthPanelSymbols = cellWidthPanelSymbols;

        FontSizeControlVM = new FontSizeControlVM(selectedBlockSymbols);
        FontFamilyControlVM = new FontFamilyControlVM(selectedBlockSymbols);
        TextAlignmentControlVM = new TextAlignmentControlVM(selectedBlockSymbols);
        FormatTextControlVM = new FormatTextControlVM(selectedBlockSymbols);

        PopupBoxMenuVM = new(this);

        ClickDelete = new(CanvasSymbolsVM.DeleteSymbols);
        projectVM = new(this);
    }

    public void SaveProject(string filePath)
    {
        projectVM.Save(filePath);
    }

    public void LoadProject(string filePath)
    {
        projectVM.Load(filePath);
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        CanvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
    }
}