namespace Edblock.SymbolsSerialization.Symbols;

public class BlockSymbolSerializable
{
    public required string Id { get; init; }
    public required string NameSymbol { get; init; }
    public required string Color { get; init; }
    public TextFieldSerializable? TextFieldSerializable { get; init; }
    public required double Width { get; init; }
    public required double Height { get; init; }
    public required double XCoordinate { get; init; }
    public required double YCoordinate { get; init; }
}