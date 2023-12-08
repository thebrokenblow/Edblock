using EdblockModel.Symbols.Enum;
using System.Text.Json.Serialization;

namespace EdblockModel.Symbols.Abstraction;

[Serializable]
public abstract class BlockSymbolModel : SymbolModel
{
    public string Id { get; set; }
    public string NameOfSymbol { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
    public int XCoordinate { get; set; }
    public int YCoordinate { get; set; }
    public string? Text { get; set; }
    [JsonIgnore]
    public int MinWidth { get; set; }
    [JsonIgnore]
    public int MinHeight { get; set; }
    public abstract void SetWidth(int width);
    public abstract void SetHeight(int height);
    public abstract int GetTextFieldWidth(int width);
    public abstract int GetTextFieldHeight(int height);

    private readonly Dictionary<PositionConnectionPoint, Func<(int x, int y)>> borderCoordinateByPosition;

    public BlockSymbolModel(string id, string nameBlockSymbol)
    {
        Id = id;
        NameOfSymbol = nameBlockSymbol;

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