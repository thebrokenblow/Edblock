using EdblockModel.Lines.Factories.Interfaces;

namespace EdblockModel.Lines.Factories;

public class FactoryLineModel(IFactoryCoordinateDecorator factoryCoordinateDecorator) : IFactoryLineModel
{
    public LineModel Create(double firstXCoordinate, double firstYCoordinate, double secondXCoordinate, double secondYCoordinate)
    {
        var firstCoordinate = factoryCoordinateDecorator.Create(firstXCoordinate, firstYCoordinate);
        var secondCoordinate = factoryCoordinateDecorator.Create(secondXCoordinate, secondYCoordinate);

        return new(firstCoordinate, secondCoordinate);
    }
}
