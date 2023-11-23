using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.Enum;

namespace EdblockModel.Symbols.LineSymbols;

public class FactoryLineSymbolModel
{
    public static LineSymbolModel CreateNewLine(LineSymbolModel lineSymbolModel)
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

    public static LineSymbolModel CreateCloneLine(LineSymbolModel lineSymbolModel)
    {
        var lineSymbol = new LineSymbolModel()
        {
            X1 = lineSymbolModel.X1,
            Y1 = lineSymbolModel.Y1,
            X2 = lineSymbolModel.X1,
            Y2 = lineSymbolModel.Y1
        };

        return lineSymbol;
    }

    public static LineSymbolModel CreateFirstLine(PositionConnectionPoint positionConnectionPoint, BlockSymbolModel blockSymbolModel)
    {
        var firstLineSymbolModel = new LineSymbolModel();

        (firstLineSymbolModel.X1, firstLineSymbolModel.Y1) = blockSymbolModel.GetBorderCoordinate(positionConnectionPoint);
        firstLineSymbolModel.X2 = firstLineSymbolModel.X1;
        firstLineSymbolModel.Y2 = firstLineSymbolModel.Y1;
        return firstLineSymbolModel;
    }
}