using EdblockModel.Symbols.ConnectionPoints;

namespace EdblockModel.Symbols.LineSymbols;

public class ArrowSymbolModel
{
    private const int WidthArrow = 8;
    private const int HeightArrow = 8;

    //Получение координат стрелки во время рисования линии
    public static List<(int, int)> GetCoordinateArrow(Tuple<int, int> startCoordinate, int currentX, int currentY, PositionConnectionPoint positionConnectionPoint)
    {
        if (positionConnectionPoint == PositionConnectionPoint.Top ||
            positionConnectionPoint == PositionConnectionPoint.Bottom)
        {
            var coordinateArrow = GetCoordinateArrowVerticalCP(startCoordinate, currentX, currentY);
            return coordinateArrow;
        }
        else
        {
            var coordinateArrow = GetCoordinateArrowHorizontalCP(startCoordinate, currentX, currentY);
            return coordinateArrow;
        }
    }

    //Получение координат стрелки при присоединение линии
    public static List<(int, int)> GetCoordinateArrow(int x, int y, PositionConnectionPoint positionConnectionPoint)
    {
        if (positionConnectionPoint == PositionConnectionPoint.Right)
        {
            return GetCoordinateLeft(x, y);
        }
        else if (positionConnectionPoint == PositionConnectionPoint.Left)
        {
            return GetCoordinateRigth(x, y);
        }
        else if (positionConnectionPoint == PositionConnectionPoint.Top)
        {
            return GetCoordinateBottom(x, y);
        }
        else
        {
            return GetCoordinateTop(x, y);
        }
    }


    //Получение координа стрелки, если линия выходит из левой и правой точки соединения
    private static List<(int, int)> GetCoordinateArrowHorizontalCP(Tuple<int, int> startCoordinate, int currentX, int currentY)
    {
        int startX = startCoordinate.Item1;
        int startY = startCoordinate.Item2;

        if (currentY == startY)
        {
            if (currentX > startX)
            {
                var coordinateArrow = GetCoordinateRigth(currentX, currentY);
                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateLeft(currentX, currentY);
                return coordinateArrow;
            }
        }
        else
        {
            if (currentY > startY)
            {
                var coordinateArrow = GetCoordinateBottom(currentX, currentY);
                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateTop(currentX, currentY);
                return coordinateArrow;
            }
        }
    }

    //Получение координа стрелки, если линия выходит из верхней и нижней точки соединения
    private static List<(int, int)> GetCoordinateArrowVerticalCP(Tuple<int, int> startCoordinate, int currentX, int currentY)
    {
        int startX = startCoordinate.Item1;
        int startY = startCoordinate.Item2;

        if (currentX == startX)
        {
            if (currentY > startY)
            {
                var coordinateArrow = GetCoordinateBottom(currentX, currentY);
                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateTop(currentX, currentY);
                return coordinateArrow;
            }
        }
        else
        {
            if (currentX > startX)
            {
                var coordinateArrow = GetCoordinateRigth(currentX, currentY);
                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateLeft(currentX, currentY);
                return coordinateArrow;
            }
        }
    }

    private static List<(int, int)> GetCoordinateRigth(int x, int y)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (x - WidthArrow, y + HeightArrow / 2),
            (x - WidthArrow, y - HeightArrow / 2),
            (x, y)
        };

        return coordinateArrow;
    }

    private static List<(int, int)> GetCoordinateLeft(int x, int y)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (x + WidthArrow, y + HeightArrow / 2),
            (x + WidthArrow, y - HeightArrow / 2),
            (x, y),
        };

        return coordinateArrow;
    }

    public static List<(int, int)> GetCoordinateBottom(int x, int y)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (x - WidthArrow / 2, y),
            (x, y + HeightArrow),
            (x + WidthArrow / 2, y),
        };

        return coordinateArrow;
    }

    private static List<(int, int)> GetCoordinateTop(int x, int y)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (x - WidthArrow / 2, y),
            (x + WidthArrow / 2, y),
            (x, y - HeightArrow),
        };

        return coordinateArrow;
    }
}