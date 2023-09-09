namespace EdblockModel;

public static class CanvasSymbols
{
    public const int LENGTH_GRID = 20;

    public static int ChangeCoordinateSymbol(int coordinate) =>
        coordinate - coordinate % (LENGTH_GRID / 2);

    public static void SetXCoordinateSymbol(SymolModel symolModel, int currentCoordinate, int previousCoordinate)
    {
        if (symolModel.X == 0)
        {
            symolModel.X = currentCoordinate - symolModel.Width / 2;
        }
        else
        {
            symolModel.X = currentCoordinate - (previousCoordinate - symolModel.X);
        }
    }

    public static void SetYCoordinateSymbol(SymolModel symolModel, int currentCoordinate, int previousCoordinate)
    {
        if (symolModel.Y == 0)
        {
            symolModel.Y = currentCoordinate - symolModel.Height / 2;
        }
        else
        {
            symolModel.Y = currentCoordinate - (previousCoordinate - symolModel.Y);
        }
    }
}
