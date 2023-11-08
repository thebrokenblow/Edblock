using EdblockViewModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols.Decorators;

public class SwapCoordinateDecorator : ICoordinateDecorator
{
    public int X
    {
        get => _coordinateDecorator.Y;
        set => _coordinateDecorator.Y = value;
    }

    public int Y
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