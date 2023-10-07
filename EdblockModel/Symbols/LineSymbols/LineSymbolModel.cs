using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class LineSymbolModel
{
    public int X1 { get; set; }

    public int Y1 { get; set; }

    public int X2 { get; set; }

    public int Y2 { get; set; }

    public PositionConnectionPoint PositionConnectionPoint { get; init; }

    public LineSymbolModel(PositionConnectionPoint positionConnectionPoint) =>
        PositionConnectionPoint = positionConnectionPoint;

    public void SetStarCoordinate((int x, int y) coordinateConnectionPoint, BlockSymbolModel blockSymbolModel)
    {
        (X1, Y1) = CoordinateLineModel.GetStartCoordinateLine(blockSymbolModel, coordinateConnectionPoint, PositionConnectionPoint);
        X2 = X1;
        Y2 = Y1;
    }
}