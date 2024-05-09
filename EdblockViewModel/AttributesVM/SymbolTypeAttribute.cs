using System;

namespace EdblockViewModel.AttributesVM;

[AttributeUsage(AttributeTargets.Class)]
public class SymbolTypeAttribute(string nameSymbol) : Attribute
{
    public string NameSymbol { get; init; } = nameSymbol;
}