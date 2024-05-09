using Edblock.SymbolsModel.EnumsModel;

namespace Edblock.SymbolsModel.Symbols.LineSymbolsModel;

public class ArrowSymbolModel
{
    private const int WidthArrow = 7;
    private const int HeightArrow = 7;

    private static ArrowOrientation arrowOrientation;

    private readonly static Dictionary<SideSymbol, Func<(double, double), List<(double, double)>>> coordinateArrorByPosition = new()
    {
        { SideSymbol.Right, coordinate => GetCoordinateLeft(coordinate) },
        { SideSymbol.Left, coordinate => GetCoordinateRigth(coordinate) },
        { SideSymbol.Top, coordinate => GetCoordinateBottom(coordinate) },
        { SideSymbol.Bottom, coordinate => GetCoordinateTop(coordinate) }
    };

    public static List<(double, double)> GetFinalCoordinate((double x, double y) finalCoordinate)
    {

        if (arrowOrientation == ArrowOrientation.Top)
        {
            var coordinateArrow = GetCoordinateTop(finalCoordinate);

            return coordinateArrow;
        }
        else if (arrowOrientation == ArrowOrientation.Right)
        {
            var coordinateArrow = GetCoordinateRigth(finalCoordinate);

            return coordinateArrow;

        }
        else if (arrowOrientation == ArrowOrientation.Bottom)
        {
            var coordinateArrow = GetCoordinateBottom(finalCoordinate);

            return coordinateArrow;
        }
        else
        {
            var coordinateArrow = GetCoordinateLeft(finalCoordinate);

            return coordinateArrow;
        }
    }

    //Получение координат стрелки во время рисования линии
    public static List<(double, double)> GetCoordinateArrow((double x, double y) startCoordinateLine, (double x, double y) currentCoordinateLine, SideSymbol? positionConnectionPoint)
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
    public static List<(double, double)> GetCoordinateArrow((double x, double y) finalCoordinate, SideSymbol? positionConnectionPoint)
    {
        if (positionConnectionPoint is null)
        {
            return null;
        }

        var coordinateArror = coordinateArrorByPosition[(SideSymbol)positionConnectionPoint].Invoke(finalCoordinate);

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
                arrowOrientation = ArrowOrientation.Right;

                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateLeft(currentCoordinateLine);
                arrowOrientation = ArrowOrientation.Left;

                return coordinateArrow;
            }
        }
        else
        {
            if (currentCoordinateLine.y > startCoordinateLine.y)
            {
                var coordinateArrow = GetCoordinateBottom(currentCoordinateLine);
                arrowOrientation = ArrowOrientation.Bottom;

                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateTop(currentCoordinateLine);
                arrowOrientation = ArrowOrientation.Top;

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
                arrowOrientation = ArrowOrientation.Bottom;

                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateTop(currentCoordinateLine);
                arrowOrientation = ArrowOrientation.Top;

                return coordinateArrow;
            }
        }
        else
        {
            if (currentCoordinateLine.x > startCoordinateLine.x)
            {
                var coordinateArrow = GetCoordinateRigth(currentCoordinateLine);
                arrowOrientation = ArrowOrientation.Right;

                return coordinateArrow;
            }
            else
            {
                var coordinateArrow = GetCoordinateLeft(currentCoordinateLine);
                arrowOrientation = ArrowOrientation.Left;

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