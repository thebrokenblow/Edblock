namespace EdblockModel.Symbols.LineSymbols;

public class ArrowSymbolModel
{
    private const int WidthArrow = 8;
    private const int HeightArrow = 8;

    public static List<(int, int)> ChangeOrientationArrow(int startX, int startY, int currentX, int currentY, PositionConnectionPoint positionConnectionPoint)
    {
        if (positionConnectionPoint == PositionConnectionPoint.Top ||
            positionConnectionPoint == PositionConnectionPoint.Bottom)
        {

            if (currentX == startX)
            {
                if (currentY > startY)
                {
                    var coordinateArrow = GetCoordinateBottom(currentX, currentY);
                    return coordinateArrow;
                }
                else
                {
                    var coordinateArrow = GetCoordinateUpper(currentX, currentY);
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
        else
        {
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
                    var coordinateArrow = GetCoordinateUpper(currentX, currentY);
                    return coordinateArrow;
                }
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

    private static List<(int, int)> GetCoordinateBottom(int x, int y)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (x - WidthArrow / 2, y),
            (x, y + HeightArrow),
            (x + WidthArrow / 2, y),
        };

        return coordinateArrow;
    }

    private static List<(int, int)> GetCoordinateUpper(int x, int y)
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