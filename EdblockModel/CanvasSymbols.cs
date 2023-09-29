namespace EdblockModel;

public static class CanvasSymbols
{
    public const int LENGTH_GRID = 20;

    public static int ChangeCoordinateSymbol(int coordinate) =>
        coordinate - coordinate % (LENGTH_GRID / 2);

    public static int GetCoordinateSymbol(double coordinateSymbol, int currentCoordinateCursor, int previousCoordinateCursor, int sizeSymbol)
    {
        currentCoordinateCursor = ChangeCoordinateSymbol(currentCoordinateCursor);

        if (coordinateSymbol == 0)
        {
            return currentCoordinateCursor - sizeSymbol / 2;
        }
        else
        {
            return (int)(currentCoordinateCursor - (previousCoordinateCursor - coordinateSymbol));
        }
    }
}