using System;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal static class FactoryBlockSymbol
{
    public static BlockSymbol Create(string nameBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        if (nameBlockSymbol == "ActionSymbol")
        {
            return new ActionSymbol(nameBlockSymbol, canvasSymbolsVM);
        }

        throw new Exception("Нет символа с таким именем, возможно вы ошиблись в названии");
    }
}