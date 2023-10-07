using EdblockModel.Symbols.Abstraction;
using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

internal class CoordinateLineModel
{
    private static readonly Dictionary<PositionConnectionPoint, Func<(int, int), (int, int)>> startCoordinateByPosition = new()
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
        int startCoordinateX = coordinateConnectionPoint.x + blockSymbolModel.X;
        int startCoordinateY = coordinateConnectionPoint.y + blockSymbolModel.Y;

        var startCoordinate = (startCoordinateX, startCoordinateY);

        startCoordinate = startCoordinateByPosition[positionConnectionPoint].Invoke(startCoordinate);

        return startCoordinate;
    }

    private static (int, int) GetStartCoordinateBottom((int x, int y) startCoordinate)
    {
        startCoordinate.x += ConnectionPointModel.diametr / 2;
        startCoordinate.y -= ConnectionPointModel.offsetPosition;

        startCoordinate.x = CanvasSymbols.СorrectionCoordinateSymbol(startCoordinate.x);
        startCoordinate.y = CanvasSymbols.СorrectionCoordinateSymbol(startCoordinate.y);

        return startCoordinate;
    }

    private static (int, int) GetStartCoordinateTop((int x, int y) startCoordinate)
    {
        startCoordinate.x += ConnectionPointModel.diametr / 2;
        startCoordinate.y += ConnectionPointModel.offsetPosition * 2;

        startCoordinate.x = CanvasSymbols.СorrectionCoordinateSymbol(startCoordinate.x);
        startCoordinate.y = CanvasSymbols.СorrectionCoordinateSymbol(startCoordinate.y);

        return startCoordinate;
    }

    private static (int, int) GetStartCoordinateRight((int x, int y) startCoordinate)
    {
        startCoordinate.x -= ConnectionPointModel.diametr / 2;
        startCoordinate.y += ConnectionPointModel.offsetPosition;

        startCoordinate.x = CanvasSymbols.СorrectionCoordinateSymbol(startCoordinate.x);
        startCoordinate.y = CanvasSymbols.СorrectionCoordinateSymbol(startCoordinate.y);

        return startCoordinate;
    }

    private static (int, int) GetStartCoordinateLeft((int x, int y) startCoordinate)
    {
        startCoordinate.x += ConnectionPointModel.offsetPosition * 2;
        startCoordinate.y += ConnectionPointModel.diametr;

        startCoordinate.x = CanvasSymbols.СorrectionCoordinateSymbol(startCoordinate.x);
        startCoordinate.y = CanvasSymbols.СorrectionCoordinateSymbol(startCoordinate.y);

        return startCoordinate;
    }
}
