using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols.ConnectionPoints;

public enum PositionConnectionPoint
{
    Top,
    Right,
    Bottom,
    Left
}

public class ConnectionPointModel
{
    public const int offsetPosition = 10;
    private readonly BlockSymbolModel _blockSymbolModel;

    public ConnectionPointModel(BlockSymbolModel blockSymbolModel) =>
        _blockSymbolModel = blockSymbolModel;

    public (int, int) GetCoordinateLeft()
    {
        int pointsX = -offsetPosition;
        int pointsY = _blockSymbolModel.Height / 2;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateRight()
    {
        int pointsX = _blockSymbolModel.Width + offsetPosition;
        int pointsY = _blockSymbolModel.Height / 2;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateTop()
    {
        int pointsX = _blockSymbolModel.Width / 2;
        int pointsY = -offsetPosition;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateBottom()
    {
        int pointsX = _blockSymbolModel.Width / 2;
        int pointsY = _blockSymbolModel.Height + offsetPosition;

        return (pointsX, pointsY);
    }
}