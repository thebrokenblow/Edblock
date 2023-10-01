namespace EdblockModel.Symbols.LineSymbols;

public class FactoryLineSymbolModel
{
    public static LineSymbolModel CreateLineByLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolModel(lineSymbolModel.Orientation)
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X1,
            Y2 = lineSymbolModel.Y1
        };

        return lineSymbol;
    }

    public static LineSymbolModel CreateLine(Orientation orientation)
    {
        var lineSymbol = new LineSymbolModel(orientation);
        return lineSymbol;
    }
}
