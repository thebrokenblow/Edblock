using EdblockModel.Symbols.Enum;
using EdblockModel.Symbols.Abstraction;

namespace EdblockModel.Symbols.LineSymbols;

public class DrawnLineSymbolModel : SymbolModel
{
    public List<LineSymbolModel> LinesSymbolModel { get; set; }
    public BlockSymbolModel? SymbolOutgoingLine { get; set; }
    public BlockSymbolModel? SymbolIncomingLine { get; set; }
    public CoordinateLineModel CoordinateLineModel { get; set; }
    public PositionConnectionPoint OutgoingPosition { get; set; }
    public PositionConnectionPoint IncomingPosition { get; set; }
    public string? Text { get; set; }

    private readonly int offsetLine = 10;

    public DrawnLineSymbolModel(BlockSymbolModel symbolOutgoingLine, PositionConnectionPoint outgoingPosition, string? color)
    {
        LinesSymbolModel = new();
        CoordinateLineModel = new(LinesSymbolModel);
        SymbolOutgoingLine = symbolOutgoingLine;
        OutgoingPosition = outgoingPosition;
        Color = color;
    }

    public (int x, int y) RoundingCoordinatesLines((int x, int y) startCoordinate, (int x, int y) currentCoordinate)
    {
        if (OutgoingPosition == PositionConnectionPoint.Bottom || OutgoingPosition == PositionConnectionPoint.Top)
        {
            return HorizontalRounding(startCoordinate, currentCoordinate);
        }
        else
        {
            return VerticallyRounding(startCoordinate, currentCoordinate);
        }
    }

    public void ChangeCoordinateLine((int x, int y) currentCoordinte)
    {
        if (OutgoingPosition == PositionConnectionPoint.Bottom || OutgoingPosition == PositionConnectionPoint.Top)
        {
            CoordinateLineModel.ChangeCoordinatesVerticalLines(currentCoordinte);
        }
        else
        {
            CoordinateLineModel.ChangeCoordinatesHorizontalLines(currentCoordinte);
        }
    }

    public void AddFirstLine()
    {
        var (x, y) = SymbolOutgoingLine.GetBorderCoordinate(OutgoingPosition);

        var firstLineSymbolModel = new LineSymbolModel
        {
            X1 = x,
            Y1 = y,
            X2 = x,
            Y2 = y,
        };

        LinesSymbolModel.Add(firstLineSymbolModel);
    }

    public LineSymbolModel GetNewLine()
    {
        var lastLineSymbol = LinesSymbolModel[^1];
        var newLineSymbolModel = FactoryLineSymbolModel.CreateLineByPreviousLine(lastLineSymbol);

        LinesSymbolModel.Add(newLineSymbolModel);

        return newLineSymbolModel;
    }

    private (int x, int y) HorizontalRounding((int x, int y) startCoordinate, (int x, int y) currentCoordinate)
    {
        if (LinesSymbolModel.Count % 2 == 1)
        {
            if (startCoordinate.y > currentCoordinate.y)
            {
                currentCoordinate.y += offsetLine;
            }
        }
        else
        {
            if (startCoordinate.x - offsetLine > currentCoordinate.x)
            {
                currentCoordinate.x += offsetLine;
            }
        }

        return currentCoordinate;
    }

    private (int x, int y) VerticallyRounding((int x, int y) startCoordinate, (int x, int y) currentCoordinate)
    {
        if (LinesSymbolModel.Count % 2 == 1)
        {
            if (startCoordinate.x - offsetLine > currentCoordinate.x)
            {
                currentCoordinate.x += offsetLine;
            }
        }
        else
        {
            if (startCoordinate.y - offsetLine > currentCoordinate.y)
            {
                currentCoordinate.y += offsetLine;
            }
        }

        return currentCoordinate;
    }
}