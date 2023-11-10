using Prism.Commands;

namespace EdblockViewModel;

public class EdblockVM
{
    public CanvasSymbolsVM CanvasSymbolsVM { get; init; }
    public DelegateCommand ClickEsc { get; init; }
    public DelegateCommand MouseUpSymbol { get; init; }
    public DelegateCommand<string> ClickSymbol { get; init; }
    public EdblockVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        CanvasSymbolsVM = canvasSymbolsVM;
        ClickEsc = new(CanvasSymbolsVM.DeleteCurrentLine);
        MouseUpSymbol = new(CanvasSymbolsVM.FinishMovingBlockSymbol);
        ClickSymbol = new(CanvasSymbolsVM.CreateBlockSymbol);
    }
}
