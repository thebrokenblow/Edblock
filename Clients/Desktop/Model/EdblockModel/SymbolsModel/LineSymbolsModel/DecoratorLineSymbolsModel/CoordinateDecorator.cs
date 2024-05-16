namespace EdblockModel.SymbolsModel.LineSymbolsModel.DecoratorLineSymbolsModel;

public class CoordinateDecorator : ICoordinateDecorator
{
    public double X { get; set; }
    public double Y { get; set; }

    public CoordinateDecorator() =>
        (X, Y) = (0, 0);

    public CoordinateDecorator((double x, double y) borderCoordinateBlockSymbol) =>
        (X, Y) = (borderCoordinateBlockSymbol.x, borderCoordinateBlockSymbol.y);
}