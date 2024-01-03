using EdblockModel.SymbolsModel.Enum;

namespace EdblockModel.SymbolsModel;

public abstract class BlockSymbolModel
{
    public string? Id { get; set; }
    public string? NameSymbol { get; set; }
    public double Width { get; set; }
    public double Height { get; set; }
    public double XCoordinate { get; set; }
    public double YCoordinate { get; set; }
    public TextFieldModel TextFieldModel { get; set; }
    public string? Color { get; set; }

    private const int minWidth = 40;
    public int MinWidth
    {
        get => minWidth;
    }
    private const int minHeight = 20;
    public int MinHeight
    {
        get => minHeight;
    }

    public abstract double GetTextFieldWidth();
    public abstract double GetTextFieldHeight();
    public abstract double GetTextFieldLeftOffset();
    public abstract double GetTextFieldTopOffset();

    private readonly Dictionary<PositionConnectionPoint, Func<(double x, double y)>> borderCoordinateByPosition;

    public BlockSymbolModel()
    {
        TextFieldModel = new();

        borderCoordinateByPosition = new()
        {
            { PositionConnectionPoint.Top, GetTopBorderCoordinate },
            { PositionConnectionPoint.Bottom, GetBottomBorderCoordinate },
            { PositionConnectionPoint.Left, GetLeftBorderCoordinate },
            { PositionConnectionPoint.Right, GetRightBorderCoordinate }
        };
    }

    public (double x, double y) GetBorderCoordinate(PositionConnectionPoint positionConnectionPoint)
    {
        return borderCoordinateByPosition[positionConnectionPoint].Invoke();
    }

    private (double x, double y) GetTopBorderCoordinate()
    {
        return (XCoordinate + Width / 2, YCoordinate);
    }

    private (double x, double y) GetBottomBorderCoordinate()
    {
        return (XCoordinate + Width / 2, YCoordinate + Height);
    }

    private (double x, double y) GetLeftBorderCoordinate()
    {
        return (XCoordinate, YCoordinate + Height / 2);
    }

    private (double x, double y) GetRightBorderCoordinate()
    {
        return (XCoordinate + Width, YCoordinate + Height / 2);
    }
}