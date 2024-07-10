using EdblockModel.Lines.DecoratorLine.Interfaces;

namespace EdblockModel.Lines.DecoratorLine;

public class SwapCoordinateDecorator(ICoordinateDecorator coordinateDecorator) : ICoordinateDecorator
{
    public double X
    {
        get => coordinateDecorator.Y;
        set => coordinateDecorator.Y = value;
    }

    public double Y
    {
        get => coordinateDecorator.X;
        set => coordinateDecorator.X = value;
    }
}
