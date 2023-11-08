using EdblockViewModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols.Decorators;

public class InversionXCoordinateDecorator : ICoordinateDecorator
{
    public int X
    {
        get => _coordinateDecorator.X * -1;
        set => _coordinateDecorator.X = value * -1;

    }

    public int Y
    {
        get => _coordinateDecorator.Y;
        set => _coordinateDecorator.Y = value;
    }

    private readonly ICoordinateDecorator _coordinateDecorator;

    public InversionXCoordinateDecorator(ICoordinateDecorator coordinateDecorator)
    {
        _coordinateDecorator = coordinateDecorator;
    }
}