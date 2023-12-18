namespace SerializationEdblock;

public class ProjectSerializable
{
    public bool IsScaleAllSymbolVM { get; init; }
    public bool IsDrawingLinesAccordingGOST { get; init; }
    public List<BlockSymbolSerializable>? BlocksSymbolSerializable { get; init; }
    public List<DrawnLineSymbolSerializable>? DrawnLinesSymbolSerializable { get; init; }
}