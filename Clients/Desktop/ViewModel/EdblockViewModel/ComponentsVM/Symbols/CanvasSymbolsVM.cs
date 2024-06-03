using System.Windows;
using System.Windows.Input;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.AbstractionsVM;
using Prism.Commands;
using EdblockViewModel.CoreVM;
using EdblockViewModel.ComponentsVM.Symbols.Interfaces;
using System;

namespace EdblockViewModel.ComponentsVM.Symbols;

public class CanvasSymbolsVM : ObservableObject
{
    public Rect Grid { get; }

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

    private Cursor cursor;
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

    public DelegateCommand MouseMove { get; init; }
    public DelegateCommand MouseUp { get; init; }
    public DelegateCommand MouseLeftButtonDown { get; init; }
    public BlockSymbolVM? MovableBlockSymbol { get; set; }
    public ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }
    public MovableRectangleLine? MovableRectangleLine { get; set; }

    public ScalingCanvasSymbolsVM ScalingCanvasSymbolsVM { get; init; }

    private const int lengthGridCell = 20;

    private readonly IListCanvasSymbolsVM _listCanvasSymbolsVM;
    public CanvasSymbolsVM(IListCanvasSymbolsVM listCanvasSymbolsVM)
    {
        _listCanvasSymbolsVM = listCanvasSymbolsVM;

        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValue);

        ScalingCanvasSymbolsVM = new(this, listCanvasSymbolsVM);

        cursor = Cursors.Arrow;

        Grid = new Rect(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        _listCanvasSymbolsVM.RemoveSelectedSymbols();

        MovableBlockSymbol = blockSymbolVM;
        MovableBlockSymbol.MoveMiddle = true;
        MovableBlockSymbol.Select();

        _listCanvasSymbolsVM.AddSymbol(blockSymbolVM);
    }

    private void SetDefaultValue()
    {
        Cursor = Cursors.Arrow;

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.MoveMiddle = false;
        }

        MovableBlockSymbol = null;

        MovableRectangleLine = null;
        ScalePartBlockSymbol = null;

        previousXCoordinate = 0;
        previousYCoordinate = 0;

        TextFieldSymbolVM.ChangeFocus(_listCanvasSymbolsVM.BlockSymbolsHasTextField);
    }

    private static int RoundCoordinate(int coordinate) //Округление координат, чтобы символ перемещался по сетке
    {
        int roundedCoordinate = coordinate - coordinate % (lengthGridCell / 2);

        return roundedCoordinate;
    }

    public void RedrawSymbols()
    {
        var currentCoordinate = (xCoordinate, yCoordinate);
        var previousCoordinate = (previousXCoordinate, previousYCoordinate);

        MovableRectangleLine?.ChangeCoordinateLine(currentCoordinate);
        MovableBlockSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
        ScalePartBlockSymbol?.SetWidthBlockSymbol(this);
        ScalePartBlockSymbol?.SetHeightBlockSymbol(this);

        previousXCoordinate = xCoordinate;
        previousYCoordinate = yCoordinate;
    }

    internal void DeleteSymbols()
    {
        throw new NotImplementedException();
    }
}