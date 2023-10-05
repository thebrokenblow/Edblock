using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols;

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
    private readonly BlockSymbolModel _blockSymbolModel;

    public ConnectionPointModel(BlockSymbolModel blockSymbolModel)
    {
        _blockSymbolModel = blockSymbolModel;
    }

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
