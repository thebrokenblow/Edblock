namespace EdblockModel;

public static class CanvasSymbols
{ 

    public static int СorrectionCoordinateSymbol(int coordinate) =>
        coordinate - coordinate % (10);

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