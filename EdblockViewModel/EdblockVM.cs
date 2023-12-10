using Prism.Commands;
using EdblockViewModel.Symbols;

namespace EdblockViewModel;

public class EdblockVM
{
    public DelegateCommand ClickEsc { get; init; }
    public DelegateCommand<string> ClickSymbol { get; init; }

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly FactoryBlockSymbol _factoryBlockSymbol;
    public EdblockVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        ClickEsc = new(canvasSymbolsVM.DeleteLine);
        ClickSymbol = new(CreateBlockSymbol);

        _canvasSymbolsVM = canvasSymbolsVM;
        _factoryBlockSymbol = new(canvasSymbolsVM);
    }

    private void CreateBlockSymbol(string nameBlockSymbol)
    {
        var blockSymbolVM = _factoryBlockSymbol.Create(nameBlockSymbol);

        _canvasSymbolsVM.AddBlockSymbol(blockSymbolVM);
    }
}
