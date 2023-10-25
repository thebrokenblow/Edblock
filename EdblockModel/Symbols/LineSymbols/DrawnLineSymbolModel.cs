using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class DrawnLineSymbolModel
{
    public List<LineSymbolModel> LinesSymbols { get; set; } = new();
    private readonly PositionConnectionPoint _positionConnectionPoint;

    public DrawnLineSymbolModel(PositionConnectionPoint positionConnectionPoint)
    {
        _positionConnectionPoint = positionConnectionPoint;
    }

    public (int x, int y) RoundingCoordinatesLines((int x, int y) startCoordinate, (int x, int y) currentCoordinate)
    {
        if (_positionConnectionPoint == PositionConnectionPoint.Bottom || _positionConnectionPoint == PositionConnectionPoint.Top)
        {
            if (LinesSymbols.Count % 2 == 1)
            {
                if (startCoordinate.y - 10 > currentCoordinate.y)
                {
                    currentCoordinate.y += 10;
                }
            }
            else
            {
                if (startCoordinate.x - 20 > currentCoordinate.x)
                {
                    currentCoordinate.x += 10;
                }
            }
        }

        if (_positionConnectionPoint == PositionConnectionPoint.Left || _positionConnectionPoint == PositionConnectionPoint.Right)
        {
            if (LinesSymbols.Count % 2 == 1)
            {
                if (startCoordinate.x - 10 > currentCoordinate.x)
                {
                    currentCoordinate.x += 10;
                }
            }
            else
            {
                if (startCoordinate.y - 10 > currentCoordinate.y)
                {
                    currentCoordinate.y += 10;
                }
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