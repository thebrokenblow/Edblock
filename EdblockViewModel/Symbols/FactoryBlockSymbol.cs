using System;
using System.Collections.Generic;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal class FactoryBlockSymbol
{
    private readonly Dictionary<string, Func<string, BlockSymbol>> instanceSymbolByName = new();
    public FactoryBlockSymbol(CanvasSymbolsVM canvasSymbolsVM)
    {
        instanceSymbolByName = new()
        {
            { "ActionSymbol", _ => new ActionSymbol("ActionSymbol", canvasSymbolsVM) }
        };

    }

    public BlockSymbol Create(string nameBlockSymbol)
    {
        return instanceSymbolByName[nameBlockSymbol].Invoke(nameBlockSymbol);
    }
}