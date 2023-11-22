using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.Enum;

namespace EdblockModel.Symbols.LineSymbols;

public class FactoryLineSymbolModel
{
    public static SymbolLineModel CreateNewLine(SymbolLineModel lineSymbolModel)
    {
        var lineSymbol = new SymbolLineModel()
        {
            X1 = lineSymbolModel.X2,
            Y1 = lineSymbolModel.Y2,
            X2 = lineSymbolModel.X2,
            Y2 = lineSymbolModel.Y2
        };

        return lineSymbol;
    }

    public static SymbolLineModel CreateCloneLine(SymbolLineModel lineSymbolModel)
    {
        var lineSymbol = new SymbolLineModel()
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X1,
            Y2 = lineSymbolModel.Y1
        };

        return lineSymbol;
    }

    public static SymbolLineModel CreateFirstLine(PositionConnectionPoint positionConnectionPoint, BlockSymbolModel blockSymbolModel)
    {
        var firstLineSymbolModel = new SymbolLineModel();

        (firstLineSymbolModel.X1, firstLineSymbolModel.Y1) = blockSymbolModel.GetBorderCoordinate(positionConnectionPoint);
        firstLineSymbolModel.X2 = firstLineSymbolModel.X1;
        firstLineSymbolModel.Y2 = firstLineSymbolModel.Y1;
        return firstLineSymbolModel;
    }
}