using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel.Core;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;

namespace EdblockViewModel.Components.CanvasSymbols;

public class CanvasSymbolsVM : ObservableObject, ICanvasSymbolsVM
{
    public Rect Grid { get; } =
        new(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);


    private int xCoordinate;
    private int previousXCoordinate;
    public int XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = RoundCoordinate(value);
        }
    }

    private int yCoordinate;
    private int previousYCoordinate;
    public int YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = RoundCoordinate(value);
        }
    }

    private Cursor cursor = Cursors.Arrow;
    public Cursor Cursor
    {
        get => cursor;
        set
        {
            cursor = value;
            OnPropertyChanged();
        }
    }

    private double width;
    public double Width
    {
        get => width;
        set
        {
            width = value;
            OnPropertyChanged();
        }
    }

    private double height;
    public double Height
    {
        get => height;
        set
        {
            height = value;
            OnPropertyChanged();
        }
    }

    public DelegateCommand MouseMove { get; }
    public DelegateCommand MouseUp { get; }

    public DelegateCommand MouseLeftButtonDown { get; }
    public DelegateCommand RemoveSelectedSymbolsCommand { get; }
    public ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }
    public ScalingCanvasSymbolsVM ScalingCanvasSymbolsVM { get; }
    public IListCanvasSymbolsVM ListCanvasSymbolsVM { get; }

    private const int lengthGridCell = 20;
    public CanvasSymbolsVM(IListCanvasSymbolsVM listCanvasSymbolsVM)
    {
        ListCanvasSymbolsVM = listCanvasSymbolsVM;
        ScalingCanvasSymbolsVM = new(this);

        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValues);
        MouseLeftButtonDown = new(ClearSelectedSymbols);
        RemoveSelectedSymbolsCommand = new(RemoveSelectedSymbols);
    }

    public void RemoveSelectedSymbols()
    {
        ListCanvasSymbolsVM.RemoveSelectedBlockSymbols();
        ScalingCanvasSymbolsVM.Resize();
    }

    public void SetDefaultValues()
    {
        Cursor = Cursors.Arrow;

        ListCanvasSymbolsVM.ChangeFocusTextField();

        var movableBlockSymbol = ListCanvasSymbolsVM.MovableBlockSymbol;
        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.MoveMiddle = false;
        }

        ScalePartBlockSymbol = null;
        ListCanvasSymbolsVM.MovableBlockSymbol = null;

        previousXCoordinate = 0;
        previousYCoordinate = 0;
    }

    public void RedrawSymbols()
    {
        var currentCoordinate = (xCoordinate, yCoordinate);
        var previousCoordinate = (previousXCoordinate, previousYCoordinate);

        ListCanvasSymbolsVM.MovableBlockSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
        ScalePartBlockSymbol?.SetWidthBlockSymbol(this);
        ScalePartBlockSymbol?.SetHeightBlockSymbol(this);

        previousXCoordinate = xCoordinate;
        previousYCoordinate = yCoordinate;
    }

    private void ClearSelectedSymbols()
    {
        ListCanvasSymbolsVM.ChangeFocusTextField();
        ListCanvasSymbolsVM.ClearSelectedBlockSymbols();
    }

    private static int RoundCoordinate(int coordinate) => //Округление координат, чтобы символ перемещался по сетке
        coordinate - coordinate % (lengthGridCell / 2);
}