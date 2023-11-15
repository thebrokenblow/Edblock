namespace EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

public class CoordinateDecorator : ICoordinateDecorator
{
    public int X { get; set; }
    public int Y { get; set; }

    public CoordinateDecorator() => 
        (X, Y) = (0, 0);

    public CoordinateDecorator((int x, int y) borderCoordinateSymbol) => 
        (X, Y) = (borderCoordinateSymbol.x, borderCoordinateSymbol.y);
}