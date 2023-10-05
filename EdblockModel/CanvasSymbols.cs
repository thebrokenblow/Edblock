namespace EdblockModel;

public static class CanvasSymbols
{
    public const int LengthGrid = 20;

    public static int СorrectionCoordinateSymbol(int coordinate) =>
        coordinate - coordinate % (LengthGrid / 2);

    public static int GetCoordinateSymbol(int coordinateSymbol, int currentCoordinateCursor, int previousCoordinateCursor, int sizeSymbol)
    {
        currentCoordinateCursor = СorrectionCoordinateSymbol(currentCoordinateCursor);

        if (coordinateSymbol == 0)
        {
            return currentCoordinateCursor - sizeSymbol / 2;
        }
        else
        {
            return currentCoordinateCursor - (previousCoordinateCursor - coordinateSymbol);
        }
    }
}