using EdblockModel.Symbols.Enum;

namespace EdblockModel.Symbols.Abstraction;

public abstract class BlockSymbolModel : SymbolModel
{
    public int Width { get; set; }
    public int Height { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public int MinWidth { get; } = 40;
    public int MinHeight { get; } = 20;
    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);
    public abstract int GetTextFieldWidth(int width);
    public abstract int GetTextFieldHeight(int height);

    private readonly Dictionary<PositionConnectionPoint, Func<(int x, int y)>> borderCoordinateByPosition;
    public BlockSymbolModel()
    {
        borderCoordinateByPosition = new()
        {
            { PositionConnectionPoint.Top, GetTopBorderCoordinate },
            { PositionConnectionPoint.Bottom, GetBottomBorderCoordinate },
            { PositionConnectionPoint.Left, GetLeftBorderCoordinate },
            { PositionConnectionPoint.Right, GetRightBorderCoordinate }
        };
    }

    public (int x, int y) GetBorderCoordinate(PositionConnectionPoint positionConnectionPoint)
    {
        return borderCoordinateByPosition[positionConnectionPoint].Invoke();
    }

    private (int x, int y) GetTopBorderCoordinate()
    {
        return (XCoordinate + Width / 2, YCoordinate);
    }

    private (int x, int y) GetBottomBorderCoordinate()
    {
        return (XCoordinate + Width / 2, YCoordinate + Height);
    }

    private (int x, int y) GetLeftBorderCoordinate()
    {
        return (XCoordinate, YCoordinate + Height / 2);
    }

    private (int x, int y) GetRightBorderCoordinate()
    {
        return (XCoordinate + Width, YCoordinate + Height / 2);
    }
}