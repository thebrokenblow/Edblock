using Prism.Mvvm;
using EdblockModel;
using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using EdblockViewModel.Symbols;
using System.Collections.ObjectModel;

namespace EdblockViewModel;

public class CanvasSymbolsVM : BindableBase
{
    public Rect Grid { get; init; }
    
    private Cursor cursor = Cursors.Arrow;
    public Cursor Cursor
    {
        get => cursor;
        set
        {
            cursor = value;
            SetProperty(ref cursor, value);
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

            if (DraggableSymbol != null)
            {
                DraggableSymbol.XCoordinate = value;
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

            if (DraggableSymbol != null)
            {
                DraggableSymbol.YCoordinate = value;
            }

            _panelY = value;
        }
    }

    public ObservableCollection<Symbol> Symbols { get; init; }
    public Symbol? DraggableSymbol { get; set; }
    public DelegateCommand ClickSymbol { get; init; }
    public DelegateCommand<Symbol> MouseMoveSymbol { get; init; }
    public DelegateCommand MouseUpSymbol { get; init; }
    public CanvasSymbolsVM()
    {
        Symbols = new();
        ClickSymbol = new(CreateSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        MouseUpSymbol = new(RemoveSymbol);
        var lengthGrid = CanvasSymbols.LENGTH_GRID;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }

    private void CreateSymbol()
    {
        var currentSymbol = new ActionSymbol();
        DraggableSymbol = currentSymbol;
        Symbols.Add(currentSymbol);
    }

    private void MoveSymbol(Symbol currentSymbol)
    {
        DraggableSymbol = currentSymbol;
    }

    private void RemoveSymbol()
    {
        DraggableSymbol = null;
    }
}