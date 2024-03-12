using System;
using System.Linq;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.LineSymbols;

namespace EdblockViewModel.Symbols;

public class CoordinateSymbolVM
{
    private readonly CanvasSymbolsVM _canvasSymbolsVM;

    public CoordinateSymbolVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
    }

    public double GetMaxX() =>
        GetMax(blockSymbolVM => blockSymbolVM.XCoordinate + blockSymbolVM.Width,
            lineSymbolVM => Math.Max(lineSymbolVM.X1, lineSymbolVM.X2));

    public double GetMaxY() =>
        GetMax(blockSymbolVM => blockSymbolVM.YCoordinate + blockSymbolVM.Height,
            lineSymbolVM => Math.Max(lineSymbolVM.Y1, lineSymbolVM.Y2));

    private double GetMax(Func<BlockSymbolVM, double> getMaxCoordinateBlockSymbol, Func<LineSymbolVM, double> getMaxCoordinateLineSymbol)
    {
        var blockSymbolsVM = _canvasSymbolsVM.BlockSymbolsVM;
        var drawnLinesSymbolVM = _canvasSymbolsVM.DrawnLinesSymbolVM;

        if (blockSymbolsVM.Count < 1)
        {
            return 0;
        }

        var maxCoordinate = blockSymbolsVM.Max(blockSymbolVM => getMaxCoordinateBlockSymbol.Invoke(blockSymbolVM));

        foreach (var drawnLineSymbolVM in drawnLinesSymbolVM)
        {
            foreach (var lineSymbolVM in drawnLineSymbolVM.LinesSymbolVM)
            {
                maxCoordinate = Math.Max(maxCoordinate, getMaxCoordinateLineSymbol.Invoke(lineSymbolVM));
            }
        }

        return maxCoordinate;
    }
}