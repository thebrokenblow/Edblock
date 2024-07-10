using EdblockModel.Lines.DecoratorLine;
using EdblockModel.Lines.Factories.Interfaces;

namespace EdblockModel.Lines.Factories;

public class FactoryCoordinateDecorator : IFactoryCoordinateDecorator
{
    public CoordinateDecorator Create(double xCoordinate, double yCoordinate) =>
        new(xCoordinate, yCoordinate);
}
