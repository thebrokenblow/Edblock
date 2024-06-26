﻿using EdblockModel.EnumsModel;

namespace Edblock.SymbolsSerialization.Symbols;

public class DrawnLineSymbolSerializable
{
    public BlockSymbolSerializable? SymbolOutgoingLine { get; init; }
    public BlockSymbolSerializable? SymbolIncomingLine { get; init; }
    public SideSymbol? OutgoingPosition { get; init; }
    public SideSymbol? IncomingPosition { get; init; }
    public List<LineSymbolSerializable>? LinesSymbolSerializable { get; init; }
    public string Text { get; init; } = string.Empty;
    public string Color { get; init; } = string.Empty;
}