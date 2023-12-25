namespace EdblockModel.SymbolsModel.LineSymbols.DecoratorLineSymbols;

public class CoordinateLine
{
    public ICoordinateDecorator CoordinateSymbolOutgoing { get; set; }
    public ICoordinateDecorator CoordinateSymbolIncoming { get; set; }

    public CoordinateLine(ICoordinateDecorator coordinateSymbolOutgoing, ICoordinateDecorator coordinateSymbolIncoming)
    {
        CoordinateSymbolOutgoing = coordinateSymbolOutgoing;
        CoordinateSymbolIncoming = coordinateSymbolIncoming;
    }
}