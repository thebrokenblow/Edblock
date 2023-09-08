using Prism.Mvvm;
using EdblockModel;
using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using EdblockViewModel.Symbols;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Controls;

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
            value -= value % (CanvasSymbols.LENGTH_GRID / 2);

            if (value.Equals(_panelX))
            {
                return;
            }


            if (DraggableSymbol != null)
            {
                //offset.X = value - DraggableSymbol.XCoordinate;
                DraggableSymbol.XCoordinate = value - (int)offset.X;
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
            value -= value % (CanvasSymbols.LENGTH_GRID / 2);

            if (value.Equals(_panelY))
            {
                return;
            }

           
            if (DraggableSymbol != null)
            {
                //offset.Y = value - DraggableSymbol.YCoordinate;
                DraggableSymbol.YCoordinate = value - (int)offset.Y;
            }

            _panelY = value;
        }
    }

    public ObservableCollection<Symbol> Symbols { get; init; }
    public Symbol? DraggableSymbol { get; set; }
    public DelegateCommand ClickSymbol { get; init; }
    public DelegateCommand<Symbol> MouseMoveSymbol { get; init; }
    public DelegateCommand MouseUpSymbol { get; init; }
    public DelegateCommand FocusableRemove { get; init; }
    public CanvasSymbolsVM()
    {
        Symbols = new();
        var actionSymbol = new ActionSymbol();
        actionSymbol.XCoordinate = 0;
        actionSymbol.YCoordinate = 0;
        Symbols.Add(actionSymbol);
        ClickSymbol = new(CreateSymbol);
        MouseMoveSymbol = new(MoveSymbol);
        MouseUpSymbol = new(RemoveSymbol);
        FocusableRemove = new(ChangeFocus);

        var lengthGrid = CanvasSymbols.LENGTH_GRID;
        Grid = new Rect(-lengthGrid, -lengthGrid, lengthGrid, lengthGrid);
    }

    private void CreateSymbol()
    {
        var currentSymbol = new ActionSymbol();
        currentSymbol.TextField.Cursor = Cursors.SizeAll;

        DraggableSymbol = currentSymbol;
        Symbols.Add(currentSymbol);
    }

    private void MoveSymbol(Symbol currentSymbol)
    {
        Cursor = Cursors.SizeAll;
        currentSymbol.TextField.Cursor = Cursors.SizeAll;
        DraggableSymbol = currentSymbol;
    }

    private void RemoveSymbol()
    {
        DraggableSymbol = null;
    }

    public void ChangeFocus()
    {
        foreach (var symbol in Symbols)
        {
            if (symbol.Focus)
            {
                symbol.Focus = false;
            }
        }
    }

    private double x;
    private double y;

    public double X
    {
        get { return X; }
        set
        {
            if (value.Equals(x))
            {
                return;
            }
            x = value;
            if (DraggableSymbol == null)
            {
                DraggableSymbol = Symbols[0];
            }
            if (DraggableSymbol != null)
            {
                offset.X = _panelX - DraggableSymbol.XCoordinate;
            }
        }
    }

    public double Y
    {
        get { return y; }
        set
        {
            if (value.Equals(y))
            {
                return;
            }
            y = value;
            if (DraggableSymbol == null)
            {
                DraggableSymbol = Symbols[0];
            }
            if (DraggableSymbol != null)
            {
                offset.Y = _panelY - DraggableSymbol.YCoordinate;
            }
        }
    }

    Point offset;
}