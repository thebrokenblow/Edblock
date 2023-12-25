using EdblockModel.SymbolsModel.Enum;

namespace SerializationEdblock;

public class DrawnLineSymbolSerializable
{
    public BlockSymbolSerializable? SymbolOutgoingLine { get; init; }
    public BlockSymbolSerializable? SymbolIncomingLine { get; init; }
    public PositionConnectionPoint OutgoingPosition { get; init; }
    public PositionConnectionPoint IncomingPosition { get; init; }
    public List<LineSymbolSerializable>? LinesSymbolSerializable { get; init; }
    public string? Text { get; init; }
    public string? Color { get; init; }
}