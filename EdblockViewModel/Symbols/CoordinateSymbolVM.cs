using System;
using System.Linq;
using EdblockViewModel.ComponentsVM;

namespace EdblockViewModel.Symbols;

public class CoordinateSymbolVM
{
    private readonly CanvasSymbolsVM _canvasSymbolsVM;

    public CoordinateSymbolVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM; 
    }

    public double GetMaxXCoordinateSymbol()
    {
        var blockSymbolsVM = _canvasSymbolsVM.BlockSymbolsVM;
        var drawnLinesSymbolVM = _canvasSymbolsVM.DrawnLinesSymbolVM;

        var maxXCoordinate = blockSymbolsVM.Max(blockSymbolVM => blockSymbolVM.XCoordinate + blockSymbolVM.Width);

        foreach (var drawnLineSymbolVM in drawnLinesSymbolVM)
        {
            foreach (var lineSymbolVM in drawnLineSymbolVM.LinesSymbolVM)
            {
                maxXCoordinate = Math.Max(maxXCoordinate, Math.Max(lineSymbolVM.X1, lineSymbolVM.X2));
            }
        }

        return maxXCoordinate;
    }

    public double GetMaxYCoordinateSymbol()
    {
        var blockSymbolsVM = _canvasSymbolsVM.BlockSymbolsVM;
        var drawnLinesSymbolVM = _canvasSymbolsVM.DrawnLinesSymbolVM;

        var maxYCoordinate = blockSymbolsVM.Max(blockSymbolVM => blockSymbolVM.YCoordinate + blockSymbolVM.Height);

        foreach (var drawnLinesSymbol in drawnLinesSymbolVM)
        {
            foreach (var linesSymbol in drawnLinesSymbol.LinesSymbolVM)
            {
                maxYCoordinate = Math.Max(maxYCoordinate, Math.Max(linesSymbol.Y1, linesSymbol.Y2));
            }
        }

        return maxYCoordinate;
    }
}
