using Edblock.PagesViewModel.Components;
using Edblock.SymbolsViewModel.Core;
using Edblock.SymbolsViewModel.Symbols.LineSymbols;

namespace Edblock.SymbolsViewModel.Symbols;

public class CoordinateSymbolViewModel(CanvasSymbolsViewModel canvasSymbolsViewModel)
{
    public double GetMaxX() =>
        GetMax(blockSymbolVM => blockSymbolVM.XCoordinate + blockSymbolVM.Width,
            lineSymbolVM => Math.Max(lineSymbolVM.X1, lineSymbolVM.X2));

    public double GetMaxY() =>
        GetMax(blockSymbolVM => blockSymbolVM.YCoordinate + blockSymbolVM.Height,
            lineSymbolVM => Math.Max(lineSymbolVM.Y1, lineSymbolVM.Y2));

    private double GetMax(Func<BlockSymbolViewModel, double> getMaxCoordinateBlockSymbol, Func<LineSymbolVM, double> getMaxCoordinateLineSymbol)
    {
        var blockSymbolsVM = canvasSymbolsViewModel.BlockSymbolsVM;
        var drawnLinesSymbolVM = canvasSymbolsViewModel.DrawnLinesSymbolVM;

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