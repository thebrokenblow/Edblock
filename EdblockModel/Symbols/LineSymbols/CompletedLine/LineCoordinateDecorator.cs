namespace EdblockModel.Symbols.LineSymbols.CompletedLine;

internal class LineCoordinateDecorator : ILineCoordinateDecorator
{
    public int LineCoordinateX { get; set; }
    public int LineCoordinateY { get; set; }
    public int FinalCoordinateX { get; set; }
    public int FinalCoordinateY { get; set; }

    public LineCoordinateDecorator((int x, int y) lineCoordinate, (int x, int y) finalCoordinate)
    {
        LineCoordinateX = lineCoordinate.x;
        LineCoordinateY = lineCoordinate.y;
        FinalCoordinateX = finalCoordinate.x;
        FinalCoordinateY = finalCoordinate.y;
    }
}