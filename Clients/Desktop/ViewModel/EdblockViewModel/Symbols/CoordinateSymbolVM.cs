using System;
using System.Linq;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.ComponentsVM.CanvasSymbols;

namespace EdblockViewModel.Symbols;

public class CoordinateSymbolVM(CanvasSymbolsVM canvasSymbolsVM)
{
    public double GetMaxX() =>
        GetMax(blockSymbolVM => blockSymbolVM.XCoordinate + blockSymbolVM.Width);

    public double GetMaxY() =>
        GetMax(blockSymbolVM => blockSymbolVM.YCoordinate + blockSymbolVM.Height);

    private double GetMax(Func<BlockSymbolVM, double> getMaxCoordinateBlockSymbol)
    {
        var blockSymbolsVM = canvasSymbolsVM.ListCanvasSymbolsVM.BlockSymbolsVM;

        if (blockSymbolsVM.Count < 1)
        {
            return 0;
        }

        var maxCoordinate = blockSymbolsVM.Max(getMaxCoordinateBlockSymbol.Invoke);

        return maxCoordinate;
    }
}