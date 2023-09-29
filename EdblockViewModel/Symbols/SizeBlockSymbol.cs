using System;
using EdblockViewModel.Symbols.ScaleRectangles;

namespace EdblockViewModel.Symbols;

internal class SizeBlockSymbol
{
    public static void SetSize(ScaleData scaleData, CanvasSymbolsVM canvasSymbolsVM, Func<ScaleData, CanvasSymbolsVM, int>? getSizeSymbol, Action<int>? setSize)
    {
        if (getSizeSymbol == null)
        {
            return;
        }

        if (setSize == null)
        {
            return;
        }

        int size = getSizeSymbol.Invoke(scaleData, canvasSymbolsVM);
        setSize.Invoke(size);
    }
}
