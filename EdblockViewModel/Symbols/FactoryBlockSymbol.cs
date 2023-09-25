using EdblockViewModel.Symbols.Abstraction;

namespace EdblockViewModel.Symbols;

internal static class FactoryBlockSymbol
{
    public static BlockSymbol Create(string nameBlockSymbol, CanvasSymbolsVM canvasSymbolsVM)
    {
        if (nameBlockSymbol == "ActionSymbol")
        {
            return new ActionSymbol(canvasSymbolsVM);
        }
        else return new ActionSymbol(canvasSymbolsVM);
    }
}
