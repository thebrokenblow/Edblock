namespace SerializationEdblock.SymbolsSerializable;

public class ParallelActionSymbolSerializable
{
    public string Id { get; init; } = string.Empty;
    public string NameSymbol { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public double Width { get; init; }
    public double Height { get; init; }
    public double XCoordinate { get; init; }
    public double YCoordinate { get; init; }
    public int CountSymbolsIncoming { get; set; }
    public int CountSymbolsOutgoing { get; set; }
}