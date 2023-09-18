using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

public class ActionSymbol : BlockSymbol
{
    public ActionSymbol(CanvasSymbolsVM canvasSymbolsVM) : base(canvasSymbolsVM)
    {
        TextField.Width = 140;
        TextField.Height = 60;
    }

    public override void SetWidth(int width)
    {
        Width = width;
        TextField.Width = width;

        foreach (var item in ConnectionPoints)
        {
            item.ChangeCoordination();
        }

        foreach (var item in ScaleRectangles)
        {
            item.ChangeCoordination();
        }
    }
}