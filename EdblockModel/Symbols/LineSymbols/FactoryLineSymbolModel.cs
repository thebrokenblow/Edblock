namespace EdblockModel.Symbols.LineSymbols;

public class FactoryLineSymbolModel
{
    public static LineSymbolModel CreateLineByPreviousLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolModel()
        {
            X1 = lineSymbolModel.X2,
            Y1 = lineSymbolModel.Y2,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2
        };

        return lineSymbol;
    }

    public static LineSymbolModel CreateLineByPenulteLine(LineSymbolModel penultimateLine, (int x2, int y2) finalCoordinate)
    {
        var lastLine = new LineSymbolModel
        {
            X1 = penultimateLine.X2,
            Y1 = penultimateLine.Y2,
            X2 = finalCoordinate.x2,
            Y2 = finalCoordinate.y2,
        };

        return lastLine;
    }
}