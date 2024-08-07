﻿using Prism.Commands;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel.Core;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.Symbols.LinesSymbolVM;
using EdblockViewModel.Symbols.LinesSymbolVM.Components;
using EdblockViewModel.Components.Interfaces;

namespace EdblockViewModel.Components.CanvasSymbols;

public class CanvasSymbolsComponentVM : ObservableObject, ICanvasSymbolsComponentVM
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
    public DelegateCommand AddLineCommand { get; }
    public ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }
    public MovableRectangleLineVM? MovableRectangleLineVM { get; set; }
    public DrawnLineSymbolVM? CurrentDrawnLineSymbolVM { get; set; }
    public ScalingCanvasSymbolsComponentVM ScalingCanvasSymbolsVM { get; }
    public IListCanvasSymbolsComponentVM ListCanvasSymbolsComponentVM { get; }
    public ITopSettingsMenuComponentVM TopSettingsMenuComponentVM { get; }

    private const int lengthGridCell = 20;
    public CanvasSymbolsComponentVM(ITopSettingsMenuComponentVM topSettingsMenuComponentVM, IListCanvasSymbolsComponentVM listCanvasSymbolsComponentVM)
    {
        TopSettingsMenuComponentVM = topSettingsMenuComponentVM;
        ListCanvasSymbolsComponentVM = listCanvasSymbolsComponentVM;
        ScalingCanvasSymbolsVM = new(this);

        AddLineCommand = new(AddLine);
        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValues);
        MouseLeftButtonDown = new(ClearSelectedSymbols);
        RemoveSelectedSymbolsCommand = new(RemoveSelectedSymbols);
    }

    public void AddLine()
    {
        CurrentDrawnLineSymbolVM?.CreateFirstLine(
            CurrentDrawnLineSymbolVM.OutgoingConnectionPoint,
            xCoordinate,
            yCoordinate);

        ListCanvasSymbolsComponentVM.ClearSelectedDrawnLinesVM();
    }

    public void RemoveSelectedSymbols()
    {
        if (CurrentDrawnLineSymbolVM is not null)
        {
            if (CurrentDrawnLineSymbolVM.OutgoingConnectionPoint is not null)
            {
                CurrentDrawnLineSymbolVM.OutgoingConnectionPoint.IsHasConnectingLine = false;
            }

            ListCanvasSymbolsComponentVM.DrawnLinesVM.Remove(CurrentDrawnLineSymbolVM);
            CurrentDrawnLineSymbolVM = null;
        }


        ListCanvasSymbolsComponentVM.RemoveSelectedDrawnLinesVM();
        ListCanvasSymbolsComponentVM.RemoveSelectedBlockSymbols();
        ScalingCanvasSymbolsVM.Resize();
    }

    public void SetDefaultValues()
    {
        Cursor = Cursors.Arrow;

        ListCanvasSymbolsComponentVM.ChangeFocusTextField();

        var movableBlockSymbol = ListCanvasSymbolsComponentVM.MovableBlockSymbol;
        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.MoveMiddle = false;
        }
        
        ScalePartBlockSymbol = null;
        MovableRectangleLineVM?.UnsubscribeToChangeCoordinates();
        ListCanvasSymbolsComponentVM.MovableBlockSymbol = null;

        previousXCoordinate = 0;
        previousYCoordinate = 0;
    }

    public void RedrawSymbols()
    {
        var currentCoordinate = (xCoordinate, yCoordinate);
        var previousCoordinate = (previousXCoordinate, previousYCoordinate);

        ListCanvasSymbolsComponentVM.MovableBlockSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
        ScalePartBlockSymbol?.SetSize(this);
        MovableRectangleLineVM?.ChangeCoordinateLine(xCoordinate, yCoordinate);
        CurrentDrawnLineSymbolVM?.DrawnLine(xCoordinate, yCoordinate);

        previousXCoordinate = xCoordinate;
        previousYCoordinate = yCoordinate;
    }

    public void ClearSelectedSymbols()
    {
        TopSettingsMenuComponentVM.ColorSubject.Observers.Clear();
        TopSettingsMenuComponentVM.FontFamilySubject.Observers.Clear();
        TopSettingsMenuComponentVM.FontSizeSubject.Observers.Clear();
        TopSettingsMenuComponentVM.FormatTextSubject.Observers.Clear();
        TopSettingsMenuComponentVM.TextAlignmentSubject.Observers.Clear();

        ListCanvasSymbolsComponentVM.ChangeFocusTextField();
        ListCanvasSymbolsComponentVM.ClearSelectedDrawnLinesVM();
        ListCanvasSymbolsComponentVM.ClearSelectedBlockSymbols();
    }

    private static int RoundCoordinate(int coordinate) => //Округление координат, чтобы символ перемещался по сетке
        coordinate - coordinate % (lengthGridCell / 2);
}