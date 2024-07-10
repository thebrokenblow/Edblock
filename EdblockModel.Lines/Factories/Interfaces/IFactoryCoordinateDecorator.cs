using EdblockModel.Lines.DecoratorLine;

namespace EdblockModel.Lines.Factories.Interfaces;

public interface IFactoryCoordinateDecorator
{
    CoordinateDecorator Create(double xCoordinate, double yCoordinate);
}
