using Edblock.SymbolsSerialization.Symbols;

namespace Edblock.SymbolsSerialization;

public class ProjectSerializable
{
    public required bool IsScaleAllSymbolVM { get; init; }
    public required bool IsDrawingLinesAccordingGOST { get; init; }
    public required double WidthCanvas { get; init; }
    public required double HeightCanvas { get; init; }
    public required List<BlockSymbolSerializable>? BlockSymbolsSerializable { get; init; }
    public required List<ParallelActionSymbolSerializable>? ParallelActionSymbolsSerializable { get; init; }
    public required List<SwitchCaseSymbolsSerializable>? SwitchCaseSymbolsSerializable { get; set; }
    public required List<DrawnLineSymbolSerializable>? DrawnLinesSymbolSerializable { get; init; }
}