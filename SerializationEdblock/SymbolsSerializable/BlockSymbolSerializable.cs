namespace SerializationEdblock.SymbolsSerializable;

public class BlockSymbolSerializable
{
    public string Id { get; init; } = string.Empty;
    public string NameSymbol { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
    public TextFieldSerializable? TextFieldSerializable { get; init; }
    public double Width { get; init; }
    public double Height { get; init; }
    public double XCoordinate { get; init; }
    public double YCoordinate { get; init; }
}