namespace EdblockModel;

public static class CanvasSymbols
{
    public const int LENGTH_GRID = 20;

    public static int ChangeCoordinateSymbol(int coordinate) =>
        coordinate - coordinate % (LENGTH_GRID / 2);
}
