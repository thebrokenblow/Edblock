using Prism.Commands;
using EdblockViewModel.Symbols;
using EdblockViewModel.ComponentsVM;

namespace EdblockViewModel;

public class EdblockVM
{
    public DelegateCommand ClickEsc { get; init; }
    public DelegateCommand<string> ClickSymbol { get; init; }

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly FactoryBlockSymbolVM _factoryBlockSymbol;
    public EdblockVM(CanvasSymbolsVM canvasSymbolsVM, CheckBoxLineGostVM checkBoxLineGostVM)
    {
        ClickEsc = new(canvasSymbolsVM.DeleteLine);
        ClickSymbol = new(CreateBlockSymbol);

        _canvasSymbolsVM = canvasSymbolsVM;
        _factoryBlockSymbol = new(canvasSymbolsVM, checkBoxLineGostVM);
    }

    private void CreateBlockSymbol(string nameBlockSymbol)
    {
        var blockSymbolVM = _factoryBlockSymbol.Create(nameBlockSymbol);

        _canvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
    }
}
