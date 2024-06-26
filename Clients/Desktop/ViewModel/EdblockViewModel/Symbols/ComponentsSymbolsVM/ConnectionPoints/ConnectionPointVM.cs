﻿using System.Windows.Input;
using System.Collections.Generic;
using Prism.Commands;
using EdblockModel.EnumsModel;
using EdblockViewModel.Core;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Components.TopSettingsMenu.PopupBoxMenu.Interfaces;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.LinesSymbolVM;
using System;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class ConnectionPointVM : ObservableObject
{
    private double xCoordinate;
    public double XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value - diametr / 2;
            OnPropertyChanged();
        }
    }

    private double yCoordinate;
    public double YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value - diametr / 2;
            OnPropertyChanged();
        }
    }

    private bool isShow = false;
    public bool IsShow
    {
        get => isShow;
        set
        {
            isShow = value;
            OnPropertyChanged();
        }
    }

    private bool isSelected = false;
    public bool IsSelected
    {
        get => isSelected;
        set
        {
            isSelected = value;
            OnPropertyChanged();
        }
    }

    private bool isHasConnectingLine = false;
    public bool IsHasConnectingLine
    {
        get => isHasConnectingLine;
        set
        {
            isHasConnectingLine = value;
            OnPropertyChanged();
        }
    }

    public double XCoordinateLineDraw { get; set; }
    public double YCoordinateLineDraw { get; set; }

    public DelegateCommand EnterCursor { get; }
    public DelegateCommand LeaveCursor { get; }
    public BlockSymbolVM BlockSymbolVM { get; }
    public IHasConnectionPoint BlockSymbolHasConnectionPoint { get; }
    public SideSymbol Position { get; }

    private const int diametr = 8;

    private readonly Func<(double, double)> _getCoordinateDrawLine;
    private readonly ICanvasSymbolsComponentVM _canvasSymbolsVM;
    private readonly ILineStateStandardComponentVM _lineStateStandardComponentVM;

    public ConnectionPointVM(
        ICanvasSymbolsComponentVM canvasSymbolsVM, 
        ILineStateStandardComponentVM lineStateStandardComponentVM, 
        BlockSymbolVM blockSymbolVM, 
        SideSymbol position,
        Func<(double, double)> getCoordinateDrawLine)
    {
        _getCoordinateDrawLine = getCoordinateDrawLine;
        _canvasSymbolsVM = canvasSymbolsVM;
        _lineStateStandardComponentVM = lineStateStandardComponentVM;

        Position = position;

        BlockSymbolVM = blockSymbolVM;
        BlockSymbolHasConnectionPoint = (IHasConnectionPoint)blockSymbolVM;

        EnterCursor = new(ShowConnectionPoints);
        LeaveCursor = new(HideConnectionPoints);
    }

    public void ShowConnectionPoints()
    {
        SetDisplayConnectionPoint(Cursors.Hand, true, true);
    }

    public void HideConnectionPoints()
    {
        SetDisplayConnectionPoint(Cursors.Arrow, false, false);
    }

    public static void SetDisplayConnectionPoints(List<ConnectionPointVM> connectionPoints, bool isShow)
    {
        foreach (var connectionPoint in connectionPoints)
        {
            connectionPoint.IsShow = isShow;
        }
    }

    private void SetDisplayConnectionPoint(Cursor cursorDisplaying, bool isEnterConnectionPoint, bool isSelectConnectionPoint)
    {
        if (_canvasSymbolsVM.ScalePartBlockSymbol == null) //Код выполняется, если символ не масштабируется
        {
            var connectionPoints = BlockSymbolHasConnectionPoint.ConnectionPointsVM;

            SetDisplayConnectionPoints(connectionPoints, isEnterConnectionPoint);

            _canvasSymbolsVM.Cursor = cursorDisplaying;

            IsSelected = isSelectConnectionPoint;
        }
    }

    public void DrawLine()
    {
        if (_canvasSymbolsVM.CurrentDrawnLineSymbolVM is null)
        {
            CreateDrawnLine();
            _canvasSymbolsVM.ClearSelectedSymbols();
        }
        else
        {
            var (xCoordinateDranLine, yCoordinateDranLine) = _getCoordinateDrawLine.Invoke();
            _canvasSymbolsVM.CurrentDrawnLineSymbolVM.FinishDrawing(Position, (int)xCoordinateDranLine, (int)yCoordinateDranLine);
            _canvasSymbolsVM.CurrentDrawnLineSymbolVM = null;
        }
    }

    public void CreateDrawnLine()
    {
        var (xCoordinateDranLine, yCoordinateDranLine) = _getCoordinateDrawLine.Invoke();
        var currentDrawnLineSymbolVM = new DrawnLineSymbolVM();
        currentDrawnLineSymbolVM.CreateFirstLine(this, (int)xCoordinateDranLine, (int)yCoordinateDranLine);

        _canvasSymbolsVM.CurrentDrawnLineSymbolVM = currentDrawnLineSymbolVM;
        _canvasSymbolsVM.ListCanvasSymbolsComponentVM.DrawnLinesVM.Add(currentDrawnLineSymbolVM);
    }
}