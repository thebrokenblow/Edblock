using System.Windows.Input;
using System.Collections.Generic;
using Prism.Commands;
using EdblockViewModel.Core;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Symbols.LinesSymbolVM;
using System;
using System.Windows;
using EdblockViewModel.Components.PopupBoxMenu.Interfaces;
using EdblockModel.Lines;


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
    private readonly Func<(double, double)> _getCoordinateConnectionPoint;

    private readonly ICanvasSymbolsComponentVM _canvasSymbolsVM;
    private readonly ILineStateStandardComponentVM _lineStateStandardComponentVM;

    public ConnectionPointVM(
        ICanvasSymbolsComponentVM canvasSymbolsVM, 
        ILineStateStandardComponentVM lineStateStandardComponentVM, 
        BlockSymbolVM blockSymbolVM, 
        SideSymbol position,
        Func<(double, double)> getCoordinateConnectionPoint,
        Func<(double, double)> getCoordinateDrawLine)
    {
        _getCoordinateDrawLine = getCoordinateDrawLine;
        _getCoordinateConnectionPoint = getCoordinateConnectionPoint;
        _canvasSymbolsVM = canvasSymbolsVM;
        _lineStateStandardComponentVM = lineStateStandardComponentVM;

        Position = position;

        BlockSymbolVM = blockSymbolVM;
        BlockSymbolHasConnectionPoint = (IHasConnectionPoint)blockSymbolVM;

        EnterCursor = new(ShowConnectionPoints);
        LeaveCursor = new(HideConnectionPoints);

        SetCoordinate();
    }

    public void SetCoordinate()
    {
        (XCoordinate, YCoordinate) = _getCoordinateConnectionPoint.Invoke();
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
            _canvasSymbolsVM.CurrentDrawnLineSymbolVM?.SetCoordinateTextField();

            IsHasConnectingLine = true;
        }
        else
        {
            if (_canvasSymbolsVM.CurrentDrawnLineSymbolVM.OutgoingConnectionPoint is null)
            {
                return;
            }

            var outgoingConnectionPoint = _canvasSymbolsVM.CurrentDrawnLineSymbolVM.OutgoingConnectionPoint;
            if (_canvasSymbolsVM.CurrentDrawnLineSymbolVM.OutgoingBlockSymbol == BlockSymbolVM)
            {
                outgoingConnectionPoint.IsHasConnectingLine = false;
                MessageBox.Show("Линия не должна входить в тот же символ из которого выходит");
                return;
            }

            IsHasConnectingLine = true;

            var (xCoordinateDranLine, yCoordinateDranLine) = _getCoordinateDrawLine.Invoke();
            _canvasSymbolsVM.CurrentDrawnLineSymbolVM.FinishDrawing(this, xCoordinateDranLine, yCoordinateDranLine);

            if (!_canvasSymbolsVM.ListCanvasSymbolsComponentVM.DrawnLinesByBlockSymbol.TryGetValue(BlockSymbolVM, out List<DrawnLineSymbolVM>? drawnLines))
            {
                _canvasSymbolsVM.ListCanvasSymbolsComponentVM.DrawnLinesByBlockSymbol.Add(BlockSymbolVM, [_canvasSymbolsVM.CurrentDrawnLineSymbolVM]);
            }
            else
            {
                drawnLines.Add(_canvasSymbolsVM.CurrentDrawnLineSymbolVM);
            }

            _canvasSymbolsVM.CurrentDrawnLineSymbolVM = null;
        }
    }

    public void CreateDrawnLine()
    {
        var (xCoordinateDranLine, yCoordinateDranLine) = _getCoordinateDrawLine.Invoke();
        var currentDrawnLineSymbolVM = new DrawnLineSymbolVM(_canvasSymbolsVM);
        currentDrawnLineSymbolVM.CreateFirstLine(this, xCoordinateDranLine, yCoordinateDranLine);

        if (!_canvasSymbolsVM.ListCanvasSymbolsComponentVM.DrawnLinesByBlockSymbol.TryGetValue(BlockSymbolVM, out List<DrawnLineSymbolVM>? drawnLines))
        {
            _canvasSymbolsVM.ListCanvasSymbolsComponentVM.DrawnLinesByBlockSymbol.Add(BlockSymbolVM, [currentDrawnLineSymbolVM]);
        }
        else
        {
            drawnLines.Add(currentDrawnLineSymbolVM);
        }
        
        _canvasSymbolsVM.CurrentDrawnLineSymbolVM = currentDrawnLineSymbolVM;
        _canvasSymbolsVM.ListCanvasSymbolsComponentVM.DrawnLinesVM.Add(currentDrawnLineSymbolVM);
    }
}