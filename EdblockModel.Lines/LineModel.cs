using EdblockModel.Lines.DecoratorLine.Interfaces;

namespace EdblockModel.Lines;

public class LineModel(ICoordinateDecorator firstCoordinate, ICoordinateDecorator secondCoordinate)
{
    public ICoordinateDecorator FirstCoordinate { get; set; } = firstCoordinate;
    public ICoordinateDecorator SecondCoordinate { get; set; } = secondCoordinate;
}