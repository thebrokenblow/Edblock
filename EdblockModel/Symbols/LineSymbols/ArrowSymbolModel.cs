using EdblockModel.Symbols.Enum;

namespace EdblockModel.Symbols.LineSymbols;

public class ArrowSymbolModel
{
    private const int WidthArrow = 8;
    private const int HeightArrow = 8;
    private readonly static Dictionary<PositionConnectionPoint, Func<(int, int), List<(int, int)>>> coordinateArrorByPosition = new()
    {
        { PositionConnectionPoint.Right, coordinate => GetCoordinateLeft(coordinate) },
        { PositionConnectionPoint.Left, coordinate => GetCoordinateRigth(coordinate) },
        { PositionConnectionPoint.Top, coordinate => GetCoordinateBottom(coordinate) },
        { PositionConnectionPoint.Bottom, coordinate => GetCoordinateTop(coordinate) }
    };

    //Получение координат стрелки во время рисования линии
    public static List<(int, int)> GetCoordinateArrow((int x, int y) startCoordinateLine, (int x, int y) currentCoordinateLine, PositionConnectionPoint positionConnectionPoint)
    {
        if (positionConnectionPoint == PositionConnectionPoint.Top ||
            positionConnectionPoint == PositionConnectionPoint.Bottom)
        {
            var coordinateArrow = GetCoordinateVerticalCP(startCoordinateLine, currentCoordinateLine);
            return coordinateArrow;
        }
        else
        {
            var coordinateArrow = GetCoordinateHorizontalCP(startCoordinateLine, currentCoordinateLine);
            return coordinateArrow;
        }
    }

    //Получение координат стрелки при присоединение линии
    public static List<(int, int)> GetCoordinateArrow((int x, int y) finalCoordinate, PositionConnectionPoint positionConnectionPoint)
    {
        var coordinateArror = coordinateArrorByPosition[positionConnectionPoint].Invoke(finalCoordinate);
        return coordinateArror;
    }


    //Получение координа стрелки, если линия выходит из левой и правой точки соединения
    private static List<(int, int)> GetCoordinateHorizontalCP((int x, int y) startCoordinateLine, (int x, int y) currentCoordinateLine)
    {
        if (currentCoordinateLine.y == startCoordinateLine.y)
        {
            if (currentCoordinateLine.x > startCoordinateLine.x)
            {
                var coordinateArrow = GetCoordinateRigth(currentCoordinateLine);
                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateLeft(currentCoordinateLine);
                return coordinateArrow;
            }
        }
        else
        {
            if (currentCoordinateLine.y > startCoordinateLine.y)
            {
                var coordinateArrow = GetCoordinateBottom(currentCoordinateLine);
                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateTop(currentCoordinateLine);
                return coordinateArrow;
            }
        }
    }

    //Получение координа стрелки, если линия выходит из верхней и нижней точки соединения
    private static List<(int, int)> GetCoordinateVerticalCP((int x, int y) startCoordinateLine, (int x, int y) currentCoordinateLine)
    {
        if (currentCoordinateLine.x == startCoordinateLine.x)
        {
            if (currentCoordinateLine.y > startCoordinateLine.y)
            {
                var coordinateArrow = GetCoordinateBottom(currentCoordinateLine);
                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateTop(currentCoordinateLine);
                return coordinateArrow;
            }
        }
        else
        {
            if (currentCoordinateLine.x > startCoordinateLine.x)
            {
                var coordinateArrow = GetCoordinateRigth(currentCoordinateLine);
                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateLeft(currentCoordinateLine);
                return coordinateArrow;
            }
        }
    }

    private static List<(int, int)> GetCoordinateRigth((int x, int y) coordinate)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (coordinate.x - WidthArrow, coordinate.y + HeightArrow / 2),
            (coordinate.x - WidthArrow, coordinate.y - HeightArrow / 2),
            (coordinate.x, coordinate.y)
        };

        return coordinateArrow;
    }

    private static List<(int, int)> GetCoordinateLeft((int x, int y) coordinate)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (coordinate.x + WidthArrow, coordinate.y + HeightArrow / 2),
            (coordinate.x + WidthArrow, coordinate.y - HeightArrow / 2),
            (coordinate.x, coordinate.y),
        };

        return coordinateArrow;
    }

    private static List<(int, int)> GetCoordinateBottom((int x, int y) coordinate)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (coordinate.x - WidthArrow / 2,coordinate.y - HeightArrow),
            (coordinate.x, coordinate.y),
            (coordinate.x + WidthArrow / 2, coordinate.y - HeightArrow),
        };

        return coordinateArrow;
    }

    private static List<(int, int)> GetCoordinateTop((int x, int y) coordinate)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (coordinate.x - WidthArrow / 2, coordinate.y + HeightArrow),
            (coordinate.x + WidthArrow / 2, coordinate.y + HeightArrow),
            (coordinate.x, coordinate.y),
        };

        return coordinateArrow;
    }
}