namespace EdblockModel.SymbolsModel.LineSymbolsModel.DecoratorLineSymbolsModel;

public class InversionYCoordinateDecorator : ICoordinateDecorator
{
    public double X
    {
        get => _coordinateDecorator.X;
        set => _coordinateDecorator.X = value;
    }

    public double Y
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