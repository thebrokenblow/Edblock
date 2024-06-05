using System;

namespace EdblockViewModel.Symbols.Attributes;

[AttributeUsage(AttributeTargets.Class)]
public class SymbolTypeAttribute : Attribute
{
    public string NameSymbol { get; init; }

    public SymbolTypeAttribute(string nameSymbol)
    {
        NameSymbol = nameSymbol;
    }
}