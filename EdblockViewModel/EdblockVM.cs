using Prism.Commands;
using EdblockViewModel.Symbols;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.Symbols.Abstraction;

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

    private readonly ProjectVM _projectVM;

    private readonly FactoryBlockSymbolVM _factoryBlockSymbol;

    public EdblockVM()
    {
        CanvasSymbolsVM = new CanvasSymbolsVM();

        var selectedBlockSymbols = CanvasSymbolsVM.SelectedBlockSymbols;

        FontSizeControlVM = new FontSizeControlVM(selectedBlockSymbols);
        FontFamilyControlVM = new FontFamilyControlVM(selectedBlockSymbols);
        TextAlignmentControlVM = new TextAlignmentControlVM(selectedBlockSymbols);
        FormatTextControlVM = new FormatTextControlVM(selectedBlockSymbols);

        PopupBoxMenuVM = new();

        ClickDelete = new(CanvasSymbolsVM.DeleteSymbols);
        //_projectVM = new(this);
        _factoryBlockSymbol = new(this);
    }

    public void SaveProject(string filePath)
    {
        _projectVM.Save(filePath);
    }

    public void LoadProject(string filePath)
    {
        _projectVM.Load(filePath);
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        CanvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
    }
}