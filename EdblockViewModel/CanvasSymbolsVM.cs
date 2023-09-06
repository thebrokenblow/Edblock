using EdblockModel;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel.Symbols;
using System.Collections.ObjectModel;

namespace EdblockViewModel;

public class CanvasSymbolsVM
{
    public Rect Grid { get; init; }
    
    private Cursor cursor = Cursors.Arrow;
    public Cursor Cursor
    {
        get => cursor;
        set
        {
            cursor = value;
        }
    }

    private int _panelX;
    public int PanelX
    {
        get => _panelX;
        set
        {
            if (value.Equals(_panelX))
            {
                return;
            }

            _panelX = value;
        }
    }

    private int _panelY;
    public int PanelY
    {
        get => _panelY;
        set
        {
            if (value.Equals(_panelY))
            {
                return;
            }

            _panelY = value;
        }
    }

    public ObservableCollection<Symbol> Symbols { get; init; }
    public CanvasSymbolsVM()
    {
        Symbols = new();
        var lengthGrid = CanvasSymbols.LENGTH_GRID;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }
}