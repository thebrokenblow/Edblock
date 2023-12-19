using Prism.Commands;
using EdblockViewModel.Symbols;
using EdblockViewModel.ComponentsVM;

namespace EdblockViewModel;

public class EdblockVM
{
    public DelegateCommand ClickEsc { get; init; }
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
        TextAlignmentControlVM textAlignmentControlVM)
    {
        ClickEsc = new(canvasSymbolsVM.DeleteLine);
        ClickSymbol = new(CreateBlockSymbol);

        _canvasSymbolsVM = canvasSymbolsVM;

        _projectVM = new(canvasSymbolsVM, scaleAllSymbolVM, checkBoxLineGostVM, fontFamilyControlVM, fontSizeControlVM, textAlignmentControlVM);
        _factoryBlockSymbol = new(canvasSymbolsVM, scaleAllSymbolVM,checkBoxLineGostVM, fontFamilyControlVM, fontSizeControlVM, textAlignmentControlVM);
    }

    public void SaveProject(string filePath)
    {
        _projectVM.Save(filePath);
    }

    public void LoadProject(string filePath)
    {
        _projectVM.Load(filePath);
    }

    private void CreateBlockSymbol(string nameBlockSymbol)
    {
        var blockSymbolVM = _factoryBlockSymbol.Create(nameBlockSymbol);

        _canvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
    }
}