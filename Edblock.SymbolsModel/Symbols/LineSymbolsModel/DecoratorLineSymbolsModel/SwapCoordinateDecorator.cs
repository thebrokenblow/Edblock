namespace Edblock.SymbolsModel.Symbols.LineSymbolsModel.DecoratorLineSymbolsModel;

public class SwapCoordinateDecorator : ICoordinateDecorator
{
    public double X
    {
        get => _coordinateDecorator.Y;
        set => _coordinateDecorator.Y = value;
    }

    public double Y
    {
        get => _coordinateDecorator.X;
        set => _coordinateDecorator.X = value;
    }

    private readonly ICoordinateDecorator _coordinateDecorator;

    public SwapCoordinateDecorator(ICoordinateDecorator coordinateDecorator)
    {
        _coordinateDecorator = coordinateDecorator;
    }
}