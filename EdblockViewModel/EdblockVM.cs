using Prism.Commands;
using EdblockViewModel.Symbols;
using EdblockViewModel.ComponentsVM;

namespace EdblockViewModel;

public class EdblockVM
{
    public DelegateCommand ClickDelete { get; init; }
    public DelegateCommand<string> ClickSymbol { get; init; }

    private readonly ProjectVM _projectVM;
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly FactoryBlockSymbolVM _factoryBlockSymbol;

    public EdblockVM(
        CanvasSymbolsVM canvasSymbolsVM,
        ScaleAllSymbolVM scaleAllSymbolVM, 
        CheckBoxLineGostVM checkBoxLineGostVM,
        FontFamilyControlVM fontFamilyControlVM,
        FontSizeControlVM fontSizeControlVM, 
        TextAlignmentControlVM textAlignmentControlVM,
        FormatTextControlVM formatTextControlVM)
    {
        ClickDelete = new(canvasSymbolsVM.DeleteSymbols);
        ClickSymbol = new(CreateBlockSymbol);

        _canvasSymbolsVM = canvasSymbolsVM;

        _projectVM = new(canvasSymbolsVM, scaleAllSymbolVM, checkBoxLineGostVM, fontFamilyControlVM, fontSizeControlVM, textAlignmentControlVM, formatTextControlVM);
        _factoryBlockSymbol = new(canvasSymbolsVM, scaleAllSymbolVM,checkBoxLineGostVM, fontFamilyControlVM, fontSizeControlVM, textAlignmentControlVM, formatTextControlVM);
    }

    public void SaveProject(string filePath)
    {
        _projectVM.Save(filePath);
    }

    public void LoadProject(string filePath)
    {
        _projectVM.Load(filePath);
    }

    public void CreateBlockSymbol(string nameBlockSymbol)
    {
        var blockSymbolVM = _factoryBlockSymbol.Create(nameBlockSymbol);

        _canvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
    }
}