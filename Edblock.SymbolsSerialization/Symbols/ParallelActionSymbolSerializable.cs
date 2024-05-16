namespace Edblock.SymbolsSerialization.Symbols;

public class ParallelActionSymbolSerializable
{
    public required string Id { get; init; }
    public required string NameSymbol { get; init; }
    public required string Color { get; init; }
    public required double Width { get; init; }
    public required double Height { get; init; }
    public required double XCoordinate { get; init; }
    public required double YCoordinate { get; init; }
    public required int CountSymbolsIncoming { get; set; }
    public required int CountSymbolsOutgoing { get; set; }
}
