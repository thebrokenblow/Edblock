using EdblockModel.Enum;

namespace EdblockModel.SymbolsModel.LineSymbolsModel;

public class ArrowSymbolModel
{
    private const int WidthArrow = 7;
    private const int HeightArrow = 7;

    private readonly static Dictionary<SideSymbol, Func<(double, double), List<(double, double)>>> coordinateArrorByPosition = new()
    {
        { SideSymbol.Right, coordinate => GetCoordinateLeft(coordinate) },
        { SideSymbol.Left, coordinate => GetCoordinateRigth(coordinate) },
        { SideSymbol.Top, coordinate => GetCoordinateBottom(coordinate) },
        { SideSymbol.Bottom, coordinate => GetCoordinateTop(coordinate) }
    };

    //Получение координат стрелки во время рисования линии
    public static List<(double, double)> GetCoordinateArrow((double x, double y) startCoordinateLine, (double x, double y) currentCoordinateLine, SideSymbol positionConnectionPoint)
    {
        if (positionConnectionPoint == SideSymbol.Top || positionConnectionPoint == SideSymbol.Bottom)
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
    public static List<(double, double)> GetCoordinateArrow((double x, double y) finalCoordinate, SideSymbol positionConnectionPoint)
    {
        var coordinateArror = coordinateArrorByPosition[positionConnectionPoint].Invoke(finalCoordinate);

        return coordinateArror;
    }


    //Получение координа стрелки, если линия выходит из левой и правой точки соединения
    private static List<(double, double)> GetCoordinateHorizontalCP((double x, double y) startCoordinateLine, (double x, double y) currentCoordinateLine)
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
    private static List<(double, double)> GetCoordinateVerticalCP((double x, double y) startCoordinateLine, (double x, double y) currentCoordinateLine)
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

    private static List<(double, double)> GetCoordinateRigth((double x, double y) coordinate)
    {
        var coordinateArrow = new List<(double, double)>
        {
            (coordinate.x - WidthArrow, coordinate.y - HeightArrow / 2),
            (coordinate.x, coordinate.y),
            (coordinate.x - WidthArrow, coordinate.y + HeightArrow / 2)
        };

        return coordinateArrow;
    }

    private static List<(double, double)> GetCoordinateLeft((double x, double y) coordinate)
    {
        var coordinateArrow = new List<(double, double)>
        {
            (coordinate.x + WidthArrow, coordinate.y - HeightArrow / 2),
            (coordinate.x, coordinate.y),
            (coordinate.x + WidthArrow, coordinate.y + HeightArrow / 2),
        };

        return coordinateArrow;
    }

    private static List<(double, double)> GetCoordinateBottom((double x, double y) coordinate)
    {
        var coordinateArrow = new List<(double, double)>
        {
            (coordinate.x - WidthArrow / 2,coordinate.y - HeightArrow),
            (coordinate.x, coordinate.y),
            (coordinate.x + WidthArrow / 2, coordinate.y - HeightArrow),
        };

        return coordinateArrow;
    }

    private static List<(double, double)> GetCoordinateTop((double x, double y) coordinate)
    {
        var coordinateArrow = new List<(double, double)>
        {
            (coordinate.x - WidthArrow / 2, coordinate.y + HeightArrow),
            (coordinate.x + WidthArrow / 2, coordinate.y + HeightArrow),
            (coordinate.x, coordinate.y ),
        };

        return coordinateArrow;
    }
}