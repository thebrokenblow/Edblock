using Prism.Commands;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel;

public class EdblockVM
{
    public DelegateCommand ClickDelete { get; init; }
    public CanvasSymbolsVM CanvasSymbolsVM { get; init; }
    public FontSizeControlVM FontSizeControlVM { get; init; }
    public FontFamilyControlVM FontFamilyControlVM { get; init; }
    public TextAlignmentControlVM TextAlignmentControlVM { get; init; }
    public FormatTextControlVM FormatTextControlVM { get; init; }
    public PopupBoxMenuVM PopupBoxMenuVM { get; init; }

    private readonly ProjectVM projectVM;

    public EdblockVM()
    {
        CanvasSymbolsVM = new CanvasSymbolsVM();

        var selectedBlockSymbols = CanvasSymbolsVM.SelectedBlockSymbols;

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