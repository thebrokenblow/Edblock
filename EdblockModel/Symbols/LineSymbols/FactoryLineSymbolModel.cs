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
}