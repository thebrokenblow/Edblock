using System.Windows;

namespace EdblockViewModel.Symbols;

internal class CoordinateSymbols
{
    public static Point GetPointCoordinate((int, int) coordinate)
    {
        var pointCoordinate = new Point(coordinate.Item1, coordinate.Item2);
        return pointCoordinate;
    }
}
