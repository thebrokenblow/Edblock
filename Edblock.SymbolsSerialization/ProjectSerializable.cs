using Edblock.SymbolsSerialization.Symbols;

namespace Edblock.SymbolsSerialization;

public class ProjectSerializable
{
    public bool IsScaleAllSymbolVM { get; init; }
    public bool IsDrawingLinesAccordingGOST { get; init; }
    public List<BlockSymbolSerializable>? BlockSymbolsSerializable { get; init; }
    public List<ParallelActionSymbolSerializable>? ParallelActionSymbolsSerializable { get; init; }
    public List<SwitchCaseSymbolsSerializable>? SwitchCaseSymbolsSerializable { get; set; }
    public List<DrawnLineSymbolSerializable>? DrawnLinesSymbolSerializable { get; init; }
}