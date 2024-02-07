namespace SerializationEdblock.SymbolsSerializable;

public class ParallelActionSymbolSerializable
{
    public string? Id { get; init; }
    public string? NameSymbol { get; init; }
    public string? Color { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public double XCoordinate { get; init; }
    public double YCoordinate { get; init; }
    public int CountSymbolsIncoming { get; set; }
    public int CountSymbolsOutgoing { get; set; }
}