using EdblockModel.Lines.DecoratorLine.Interfaces;

namespace EdblockModel.Lines;

public class LineModel(ICoordinateDecorator firstCoordinate, ICoordinateDecorator secondCoordinate)
{
    public ICoordinateDecorator FirstCoordinate { get; set; } = firstCoordinate;
    public ICoordinateDecorator SecondCoordinate { get; set; } = secondCoordinate;

    public bool IsVertical() => 
        FirstCoordinate.X == SecondCoordinate.X;

    public bool IsHorizontal() =>
        FirstCoordinate.Y == SecondCoordinate.Y;

    public bool IsZero() =>
        IsVertical() && IsHorizontal();
}