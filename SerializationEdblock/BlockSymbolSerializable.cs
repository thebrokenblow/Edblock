namespace SerializationEdblock;

public class BlockSymbolSerializable
{
    public string? Id { get; init; }
    public string? NameSymbol { get; init; }
    public TextFieldSerializable? TextFieldSerializable { get; init; }
    public int Width { get; init; }
    public int Height { get; init; }
    public int XCoordinate { get; init; }
    public int YCoordinate { get; init; }
}