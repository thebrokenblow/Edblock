using EdblockModel.Symbols.LineSymbols;

namespace EdblockViewModel.Symbols.LineSymbols;

public class FactoryLineSymbol
{
    public static LineSymbol CreateNewLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbol
        {
            X1 = lineSymbolModel.X2,
            Y1 = lineSymbolModel.Y2,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2
        };

        return lineSymbol;
    }

    public static LineSymbol CreateStartLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbol
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X1,
            Y2 = lineSymbolModel.Y1
        };

        return lineSymbol;
    }

    public static LineSymbol CreateLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbol
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2
        };

        return lineSymbol;
    }
}
