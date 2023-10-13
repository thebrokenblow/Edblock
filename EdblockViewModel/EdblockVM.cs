namespace EdblockViewModel;

public class EdblockVM
{
    public CanvasSymbolsVM CanvasSymbolsVM { get; init; }    
    public EdblockVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        CanvasSymbolsVM = canvasSymbolsVM;
    }
}
