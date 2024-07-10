using EdblockModel.Lines.DecoratorLine.Interfaces;

namespace EdblockModel.Lines.DecoratorLine;

public class CoordinateDecorator(double x, double y) : ICoordinateDecorator
{
    public double X { get; set; } = x;
    public double Y { get; set; } = y;
}