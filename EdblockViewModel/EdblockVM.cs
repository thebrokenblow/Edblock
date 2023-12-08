using Prism.Commands;

namespace EdblockViewModel;

public class EdblockVM
{
    public DelegateCommand ClickEsc { get; init; }
    public DelegateCommand MouseUpSymbol { get; init; }
    public DelegateCommand<string> ClickSymbol { get; init; }
    public EdblockVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        ClickEsc = new(canvasSymbolsVM.DeleteLine);
        MouseUpSymbol = new(Get);
        ClickSymbol = new(canvasSymbolsVM.CreateBlockSymbol);
    }

    private void Get()
    {
        
    }
}
