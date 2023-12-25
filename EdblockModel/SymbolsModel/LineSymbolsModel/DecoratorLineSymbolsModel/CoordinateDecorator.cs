namespace EdblockModel.SymbolsModel.LineSymbolsModel.DecoratorLineSymbolsModel;

public class CoordinateDecorator : ICoordinateDecorator
{
    public int X { get; set; }
    public int Y { get; set; }

    public CoordinateDecorator() =>
        (X, Y) = (0, 0);

    public CoordinateDecorator((int x, int y) borderCoordinateBlockSymbol) =>
        (X, Y) = (borderCoordinateBlockSymbol.x, borderCoordinateBlockSymbol.y);
}