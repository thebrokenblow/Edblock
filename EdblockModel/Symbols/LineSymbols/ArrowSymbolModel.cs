namespace EdblockModel.Symbols.LineSymbols;

public class ArrowSymbolModel
{
    private const int WidthArrow = 8;
    private const int HeightArrow = 8;

    public static List<(int, int)> GetCoordinateRigth(int x, int y)
    {
        var coordinateArrow = new List<(int, int)>
        {
            (x - WidthArrow, y + HeightArrow / 2),
            (x - WidthArrow, y - HeightArrow / 2),
            (x, y)
        };

        return coordinateArrow;
    }

    public static List<(int, int)> GetCoordinateLeft(int x, int y)
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

    public static List<(int, int)> GetCoordinateUpper(int x, int y)
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