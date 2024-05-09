﻿namespace Edblock.SymbolsModel.Symbols;

public class BlockSymbolModel
{
    public string Id { get; set; } = string.Empty;
    public string NameSymbol { get; set; } = string.Empty;
    public double XCoordinate { get; set; }
    public double YCoordinate { get; set; }
    public string Color { get; set; } = string.Empty;
    public double Width { get; set; }
    public double Height { get; set; }
    public TextFieldSymbolModel? TextFieldSymbolModel { get; set; }
}