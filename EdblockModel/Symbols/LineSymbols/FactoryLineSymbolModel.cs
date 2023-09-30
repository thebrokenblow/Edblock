namespace EdblockModel.Symbols.LineSymbols;

internal class FactoryLineSymbolModel
{
    public static LineSymbolModel CreateLine(LineSymbolModel lineSymbolModel)
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
}
