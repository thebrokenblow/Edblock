namespace EdblockModel.SymbolsModel.LineSymbolsModel.DecoratorLineSymbolsModel;

public class InversionXCoordinateDecorator : ICoordinateDecorator
{
    public double X
    {
        get => _coordinateDecorator.X * -1;
        set => _coordinateDecorator.X = value * -1;

    }

    public double Y
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