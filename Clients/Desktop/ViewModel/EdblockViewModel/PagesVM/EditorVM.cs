using Prism.Commands;
using EdblockViewModel.CoreVM;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols;
using EdblockViewModel.ComponentsVM.TopSettingsPanelComponent.Interfaces;

namespace EdblockViewModel.PagesVM;

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

    public DelegateCommand RemoveSelectedSymbols { get; init; }
    public CanvasSymbolsVM CanvasSymbolsVM { get; init; }
    public IFontSizeComponentVM FontSizeControlVM { get; init; }
    public IFontFamilyComponentVM FontFamilyControlVM { get; init; }
    public ITextAlignmentComponentVM TextAlignmentControlVM { get; init; }
    public IFormatTextComponentVM FormatTextControlVM { get; init; }
    public PopupBoxMenuVM PopupBoxMenuVM { get; init; }
    public ListSymbolsVM ListSymbolsVM { get; init; }

    private readonly ProjectVM projectVM;

    private const int cellHeightTopSettingsPanel = 60;
    private const int cellWidthPanelSymbols = 50;

    public EditorVM(
        CanvasSymbolsVM canvasSymbolsVM, 
        IFormatTextComponentVM formatTextControlVM, 
        IFontSizeComponentVM fontSizeControlVM, 
        IFontFamilyComponentVM fontFamilyComponentVM,
        ITextAlignmentComponentVM textAlignmentComponentVM)
    {
        CanvasSymbolsVM = canvasSymbolsVM;

        var selectedBlockSymbols = CanvasSymbolsVM.ListCanvasSymbolsVM.SelectedBlockSymbols;

        CanvasSymbolsVM.ScalingCanvasSymbolsVM.HeightTopSettingsPanel = cellHeightTopSettingsPanel;
        CanvasSymbolsVM.ScalingCanvasSymbolsVM.WidthPanelSymbols = cellWidthPanelSymbols;

        PopupBoxMenuVM = new();
        FontSizeControlVM = fontSizeControlVM;
        FontFamilyControlVM = fontFamilyComponentVM;
        FormatTextControlVM = formatTextControlVM;
        TextAlignmentControlVM = textAlignmentComponentVM;

        projectVM = new(this);
        ListSymbolsVM = new(this);

        RemoveSelectedSymbols = new(CanvasSymbolsVM.RemoveSelectedSymbols);
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