using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class FactoryLineSymbolModel
{
    public static LineSymbolModel CreateNewLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolModel(lineSymbolModel.PositionConnectionPoint)
        {
            X1 = lineSymbolModel.X2,
            Y1 = lineSymbolModel.Y2,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2
        };

        return lineSymbol;
    }

    public static LineSymbolModel CreateCloneLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolModel(lineSymbolModel.PositionConnectionPoint)
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X1,
            Y2 = lineSymbolModel.Y1
        };

        return lineSymbol;
    }

    public static LineSymbolModel CreateLine(PositionConnectionPoint positionConnectionPoint)
    {
        var lineSymbol = new LineSymbolModel(positionConnectionPoint);
        return lineSymbol;
    }
}