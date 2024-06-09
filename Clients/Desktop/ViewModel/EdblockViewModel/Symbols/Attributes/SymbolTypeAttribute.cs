using System;

namespace EdblockViewModel.Symbols.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SymbolTypeAttribute(string nameSymbol) : Attribute
{
    public string NameSymbol { get; } = nameSymbol;
}