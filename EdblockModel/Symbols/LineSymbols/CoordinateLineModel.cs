using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class CoordinateLineModel
{
    private static readonly Dictionary<PositionConnectionPoint, Func<(int, int), (int x, int y)>> startCoordinateByPosition = new()
    {
        { PositionConnectionPoint.Bottom, startCoordinate => GetStartCoordinateBottom(startCoordinate) },
        { PositionConnectionPoint.Top, startCoordinate => GetStartCoordinateTop(startCoordinate) },
        { PositionConnectionPoint.Left, startCoordinate => GetStartCoordinateLeft(startCoordinate) },
        { PositionConnectionPoint.Right, startCoordinate => GetStartCoordinateRight(startCoordinate) }
    };

    public static (int, int) GetStartCoordinateLine(BlockSymbolModel blockSymbolModel, 
                                                    (int x, int y) coordinateConnectionPoint,
                                                    PositionConnectionPoint positionConnectionPoint)
    {
        int x = coordinateConnectionPoint.x + blockSymbolModel.XCoordinate;
        int y = coordinateConnectionPoint.y + blockSymbolModel.YCoordinate;

        var startCoordinate = startCoordinateByPosition[positionConnectionPoint].Invoke((x, y));
       
        startCoordinate.x -= startCoordinate.x % 10;
        startCoordinate.y -= startCoordinate.y % 10;

        return startCoordinate;
    }

    public static (int, int) GetStartCoordinate(List<LineSymbolModel> linesSymbols) //Получение начальных координат в зависимости от уже нарисованных линий
    {
        if (linesSymbols.Count == 1 || linesSymbols.Count == 2)
        {
            var firstLine = linesSymbols[0];

            return (firstLine.X1, firstLine.Y1);
        }
        else if (linesSymbols.Count % 2 == 1)
        {
            var lastLine = linesSymbols[^1];

            return (lastLine.X1, lastLine.Y1);
        }
        else
        {
            var penultimateLine = linesSymbols[^2];

            return (penultimateLine.X1, penultimateLine.Y1);
        }
    }

    public static void ChangeCoordinatesVerticalLines(List<LineSymbolModel> linesSymbols, LineSymbolModel lastLineSymbol, int currentX, int currentY)
    {
        if (linesSymbols.Count % 2 == 0)
        {
            var penultimateLine = linesSymbols[^2];
            penultimateLine.Y2 = currentY;

            lastLineSymbol = linesSymbols[^1];
            lastLineSymbol.X2 = currentX;
            lastLineSymbol.Y1 = penultimateLine.Y2;
            lastLineSymbol.Y2 = penultimateLine.Y2;

            RemoveNewLine(linesSymbols, lastLineSymbol, lastLineSymbol.X2, penultimateLine.X2);
        }
        else
        {
            lastLineSymbol.Y2 = currentY;
            AddNewLine(linesSymbols, lastLineSymbol, lastLineSymbol.X2, currentX);
        }
    }

    public static void ChangeCoordinatesHorizontalLines(List<LineSymbolModel> linesSymbols, LineSymbolModel lastLineSymbol, int currentX, int currentY)
    {
        if (linesSymbols.Count % 2 == 0)
        {
            var penultimateLine = linesSymbols[^2];
            penultimateLine.X2 = currentX;

            lastLineSymbol = linesSymbols[^1];
            lastLineSymbol.Y2 = currentY;
            lastLineSymbol.X1 = penultimateLine.X2;
            lastLineSymbol.X2 = penultimateLine.X2;

            RemoveNewLine(linesSymbols, lastLineSymbol, lastLineSymbol.Y2, penultimateLine.Y2);
        }
        else
        {
            lastLineSymbol.X2 = currentX;
            AddNewLine(linesSymbols, lastLineSymbol, lastLineSymbol.Y2, currentY);
        }
    }

    public static void RemoveNewLine(List<LineSymbolModel> linesSymbols, LineSymbolModel lastLineSymbol, int coordinateLastLine, int coordinatePenultimateLine)
    {
        if (coordinateLastLine == coordinatePenultimateLine)
        {
            linesSymbols.Remove(lastLineSymbol);
        }
    }

    public static void AddNewLine(List<LineSymbolModel> linesSymbols, LineSymbolModel lastLineSymbol, int coordinateCurrentLine, int coordinate)
    {
        if (coordinateCurrentLine != coordinate)
        {
            var lineSymbol = FactoryLineSymbolModel.CreateCloneLine(lastLineSymbol);
            linesSymbols.Add(lineSymbol);
        }
    }

    private static (int, int) GetStartCoordinateBottom((int x, int y) startCoordinate)
    {
        startCoordinate.x += ConnectionPointModel.diametr / 2;
        startCoordinate.y -= ConnectionPointModel.offsetPosition;

        return startCoordinate;
    }

    private static (int, int) GetStartCoordinateTop((int x, int y) startCoordinate)
    {
        startCoordinate.x += ConnectionPointModel.diametr / 2;
        startCoordinate.y += ConnectionPointModel.offsetPosition * 2;

        return startCoordinate;
    }

    private static (int, int) GetStartCoordinateRight((int x, int y) startCoordinate)
    {
        startCoordinate.x -= ConnectionPointModel.diametr / 2;
        startCoordinate.y += ConnectionPointModel.offsetPosition;

        return startCoordinate;
    }

    private static (int, int) GetStartCoordinateLeft((int x, int y) startCoordinate)
    {
        startCoordinate.x += ConnectionPointModel.offsetPosition * 2;
        startCoordinate.y += ConnectionPointModel.diametr;

        return startCoordinate;
    }
}
