using EdblockModel;
using System.Windows;

namespace EdblockViewModel;

public class CanvasSymbolsVM
{
    public Rect Grid { get; init; }
    public CanvasSymbolsVM()
    {
        var lengthGrid = CanvasSymbols.LENGTH_GRID;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }
}
