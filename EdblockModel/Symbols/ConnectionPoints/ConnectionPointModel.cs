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
    public const int diametr = 8;
    public const int offsetPosition = 10;
    public const string HexFocusFill = "#777777";
    public const string HexFocusStroke = "#00ff00";
    public const string HexNotFocusFill = "#00FFFFFF";
    public const string HexNotFocusStroke = "#00FFFFFF";
    private readonly BlockSymbolModel _blockSymbolModel;

    public ConnectionPointModel(BlockSymbolModel blockSymbolModel) =>
        _blockSymbolModel = blockSymbolModel;

    public (int, int) GetCoordinateLeft()
    {
        int pointsX = -offsetPosition - diametr;
        int pointsY = _blockSymbolModel.Height / 2 - diametr / 2;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateRight()
    {
        int pointsX = _blockSymbolModel.Width + offsetPosition;
        int pointsY = _blockSymbolModel.Height / 2 - diametr / 2;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateTop()
    {
        int pointsX = _blockSymbolModel.Width / 2 - diametr / 2;
        int pointsY = -offsetPosition - diametr;

        return (pointsX, pointsY);
    }

    public (int, int) GetCoordinateBottomCP()
    {
        int pointsX = _blockSymbolModel.Width / 2 - diametr / 2;
        int pointsY = _blockSymbolModel.Height + offsetPosition;

        return (pointsX, pointsY);
    }
}