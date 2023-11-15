namespace EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

public class InversionYCoordinateDecorator : ICoordinateDecorator
{
    public int X
    {
        get => _coordinateDecorator.X;
        set => _coordinateDecorator.X = value;
    }

    public int Y
    {
        get => _coordinateDecorator.Y * -1;
        set => _coordinateDecorator.Y = value * -1;
    }

    private readonly ICoordinateDecorator _coordinateDecorator;

    public InversionYCoordinateDecorator(ICoordinateDecorator coordinateDecorator)
    {
        _coordinateDecorator = coordinateDecorator;
    }
}