using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class LineSymbolModel
{
    public int X1 { get; set; }

    public int Y1 { get; set; }

    public int X2 { get; set; }

    public int Y2 { get; set; }

    public void SetStarCoordinate((int x, int y) coordinateConnectionPoint, PositionConnectionPoint positionConnectionPoint, BlockSymbolModel blockSymbolModel)
    {
        (X1, Y1) = CoordinateLineModel.GetStartCoordinateLine(blockSymbolModel, coordinateConnectionPoint, positionConnectionPoint);
        X2 = X1;
        Y2 = Y1;
    }
}