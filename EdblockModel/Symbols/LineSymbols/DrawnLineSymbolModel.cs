using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class DrawnLineSymbolModel
{
    public List<LineSymbolModel> LinesSymbols { get; set; } = new();
    private readonly PositionConnectionPoint _positionConnectionPoint;
    private readonly int offsetLine = CanvasSymbols.LengthGrid / 2;

    public DrawnLineSymbolModel(PositionConnectionPoint positionConnectionPoint)
    {
        _positionConnectionPoint = positionConnectionPoint;
    }

    public (int x, int y) RoundingCoordinatesLines((int x, int y) startCoordinate, (int x, int y) currentCoordinate)
    {
        if (_positionConnectionPoint == PositionConnectionPoint.Bottom || 
            _positionConnectionPoint == PositionConnectionPoint.Top)
        {
            return HorizontalRounding(startCoordinate, currentCoordinate);
        }
        else
        {
            return VerticallyRounding(startCoordinate, currentCoordinate);
        }
    }

    private (int x, int y) HorizontalRounding((int x, int y) startCoordinate, (int x, int y) currentCoordinate)
    {
        if (LinesSymbols.Count % 2 == 1)
        {
            if (startCoordinate.y - offsetLine > currentCoordinate.y)
            {
                currentCoordinate.y += offsetLine;
            }
            else if (startCoordinate.y + offsetLine < currentCoordinate.y)
            {
                currentCoordinate.y -= offsetLine;
            }
        }
        else
        {
            if (startCoordinate.x - offsetLine > currentCoordinate.x)
            {
                currentCoordinate.x += offsetLine;
            }
            else if (startCoordinate.x + offsetLine < currentCoordinate.x)
            {
                currentCoordinate.x -= offsetLine;
            }
        }

        return currentCoordinate;
    }

    private (int x, int y) VerticallyRounding((int x, int y) startCoordinate, (int x, int y) currentCoordinate)
    {
        if (LinesSymbols.Count % 2 == 0)
        {
            if (currentCoordinate.y - 10 <= startCoordinate.y)
            {
                currentCoordinate.y += offsetLine;
            }
        }

        return currentCoordinate;
    }

    public void ChangeCoordinateLine(int currentX, int currentY)
    {
        var lastLineSymbol = LinesSymbols[^1];

        if (_positionConnectionPoint == PositionConnectionPoint.Bottom || _positionConnectionPoint == PositionConnectionPoint.Top)
        {
            CoordinateLineModel.ChangeCoordinatesVerticalLines(LinesSymbols, lastLineSymbol, currentX, currentY);
        }
        else
        {
            CoordinateLineModel.ChangeCoordinatesHorizontalLines(LinesSymbols, lastLineSymbol, currentX, currentY);
        }
    }

    public void AddFirstLine((int x, int y) coordinateConnectionPoint, PositionConnectionPoint positionConnectionPoint, BlockSymbolModel blockSymbolModel)
    {
        var lineSymbolModel = FactoryLineSymbolModel.CreateFirstLine(coordinateConnectionPoint, positionConnectionPoint, blockSymbolModel);
        LinesSymbols.Add(lineSymbolModel);
    }

    public LineSymbolModel GetNewLine()
    {
        var lastLineSymbol = LinesSymbols[^1];
        var newLineSymbolModel = FactoryLineSymbolModel.CreateNewLine(lastLineSymbol);

        LinesSymbols.Add(newLineSymbolModel);

        return newLineSymbolModel;
    }
}