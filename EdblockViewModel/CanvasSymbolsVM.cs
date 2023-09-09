using EdblockModel;
using System.Windows;
using Prism.Commands;
using System.Windows.Input;
using EdblockViewModel.Symbols;
using System.Collections.ObjectModel;

namespace EdblockViewModel;

public class CanvasSymbolsVM
{
    public Rect Grid { get; init; }

    private int x;
    public int X
    {
        get => x;
        set
        {
            value = CanvasSymbols.ChangeCoordinateSymbol(value);

            if (DraggableSymbol != null)
            {
                CanvasSymbols.SetXCoordinateSymbol(DraggableSymbol.SymolModel, value, x);
                DraggableSymbol.XCoordinate = DraggableSymbol.SymolModel.X;
            }

            x = value;
        }
    }

    private int y;
    public int Y
    {
        get => y;
        set
        {
            value = CanvasSymbols.ChangeCoordinateSymbol(value);

            if (DraggableSymbol != null)
            {
                CanvasSymbols.SetYCoordinateSymbol(DraggableSymbol.SymolModel, value, y);
                DraggableSymbol.YCoordinate = DraggableSymbol.SymolModel.Y;
            }

            y = value;
        }
    }

    public ObservableCollection<Symbol> Symbols { get; init; }
    public DelegateCommand ClickSymbol { get; init; }
    public DelegateCommand<Symbol> MouseMoveSymbol { get; init; }
    public DelegateCommand MouseUpSymbol { get; init; }
    public DelegateCommand FocusableRemove { get; init; }
    public Symbol? DraggableSymbol { get; set; }
    public CanvasSymbolsVM()
    {
        Symbols = new();
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
        if (!currentSymbol.TextField.Focus)
        {
            currentSymbol.TextField.Cursor = Cursors.SizeAll;
        }

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
            if (symbol.TextField.Focus)
            {
                symbol.TextField.Focus = false;
            }
        }
    }
}