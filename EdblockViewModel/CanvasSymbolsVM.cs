using EdblockModel;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel.Symbols;
using System.Collections.ObjectModel;

namespace EdblockViewModel;

public class CanvasSymbolsVM
{
    public Rect Grid { get; init; }
    private Cursor cursor;
    public Cursor Cursor
    {
        get => cursor;
        set
        {
            cursor = value;
        }
    }
    public ObservableCollection<Symbol> Symbols { get; init; }
    public CanvasSymbolsVM()
    {
        Symbols = new();
        cursor = Cursors.Hand;
        var actionSymbol = new ActionSymbol();
        actionSymbol.Width = 140;
        actionSymbol.Height = 60;
        actionSymbol.XCoordinate = 100;
        actionSymbol.YCoordinate = 100;
        Symbols.Add(actionSymbol);
        var lengthGrid = CanvasSymbols.LENGTH_GRID;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }
}