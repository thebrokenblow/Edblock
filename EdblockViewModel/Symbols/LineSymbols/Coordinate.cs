namespace EdblockViewModel.Symbols.LineSymbols;

public class Coordinate : ICoordinateDecorator
{
    public int X { get; set; }
    public int Y { get; set; }

    public Coordinate() => (X, Y) = (0, 0);
    public Coordinate((int x, int y) borderCoordinateSymbol) => (X, Y) = (borderCoordinateSymbol.x, borderCoordinateSymbol.y);
}