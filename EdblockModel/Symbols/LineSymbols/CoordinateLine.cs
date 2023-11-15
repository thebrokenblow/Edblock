using EdblockModel.Symbols.LineSymbols.DecoratorLineSymbols;

namespace EdblockViewModel.Symbols.LineSymbols;

public class CoordinateLine
{
    public ICoordinateDecorator FirstCoordinate { get; set; }
    public ICoordinateDecorator SecondCoordinate { get; set; }

    public CoordinateLine(ICoordinateDecorator firstCoordinate, ICoordinateDecorator secondCoordinate)
    {
        FirstCoordinate = firstCoordinate;
        SecondCoordinate = secondCoordinate;
    }
}
