using System.Windows;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols.ConnectionPoints;

internal class CoordinateConnectionPoint
{
    private readonly BlockSymbol _blockSymbol;
    public CoordinateConnectionPoint(BlockSymbol blockSymbol) =>
        _blockSymbol = blockSymbol;

    public Point GetCoordinateLeftCP(double diametr, int offset)
    {
        double connectionPointsX = -offset - diametr;
        double connectionPointsY = _blockSymbol.Height / 2 - diametr / 2;
        var coordinateLeftConnectionPoint = new Point(connectionPointsX, connectionPointsY);

        return coordinateLeftConnectionPoint;
    }

    public Point GetCoordinateRightCP(double diametr, int offset)
    {
        double connectionPointsX = _blockSymbol.Width + offset;
        double connectionPointsY = _blockSymbol.Height / 2 - diametr / 2;
        var coordinateRightConnectionPoint = new Point(connectionPointsX, connectionPointsY);

        return coordinateRightConnectionPoint;
    }

    public Point GetCoordinateTopCP(double diametr, int offset)
    {
        double connectionPointsX = _blockSymbol.Width / 2 - diametr / 2;
        double connectionPointsY = -offset - diametr;
        var coordinateTopConnectionPoint = new Point(connectionPointsX, connectionPointsY);

        return coordinateTopConnectionPoint;
    }

    public Point GetCoordinateBottomCP(double diametr, int offset)
    {
        double connectionPointsX = _blockSymbol.Width / 2 - diametr / 2;
        double connectionPointsY = _blockSymbol.Height + offset;
        var coordinateTopConnectionPoint = new Point(connectionPointsX, connectionPointsY);

        return coordinateTopConnectionPoint;
    }
}
