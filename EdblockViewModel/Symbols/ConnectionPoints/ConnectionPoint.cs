using System;
using Prism.Commands;
using System.Windows;
using EdblockModel.Symbols;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.Abstraction;
using EdblockModel.SymbolsModel.Enum;
using EdblockModel.SymbolsModel.LineSymbolsModel;

namespace EdblockViewModel.Symbols.ConnectionPoints;

public class ConnectionPoint : INotifyPropertyChanged
{
    private int xCoordinate;
    public int XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = value;
            OnPropertyChanged();
        }
    }

    private int yCoordinate;
    public int YCoordinate
    {
        get => yCoordinate;
        set
        {
            yCoordinate = value;
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

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public BlockSymbolVM BlockSymbolVM { get; init; }
    public Func<(int, int)> GetCoordinate { get; init; }
    public PositionConnectionPoint Position { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CheckBoxLineGostVM _checkBoxLineGostVM;
    private readonly ConnectionPointModel _connectionPointModel;

    public ConnectionPoint(
        CanvasSymbolsVM canvasSymbolsVM, 
        BlockSymbolVM blockSymbolVM, 
        CheckBoxLineGostVM checkBoxLineGostVM,
        Func<(int, int)> getCoordinate, 
        PositionConnectionPoint position)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;
        _connectionPointModel = new(position);

        GetCoordinate = getCoordinate;

        Position = position;
        BlockSymbolVM = blockSymbolVM;
        
        EnterCursor = new(ShowConnectionPoints);
        LeaveCursor = new(HideConnectionPoints);

        (XCoordinate, YCoordinate) = getCoordinate.Invoke();
    }

    public void ChangeCoordination()
    {
        (XCoordinate, YCoordinate) = GetCoordinate.Invoke();
    }

    public void ShowConnectionPoints()
    {
        SetDisplayConnectionPoint(Cursors.Hand, true, true);
    }

    public void HideConnectionPoints()
    {
        SetDisplayConnectionPoint(Cursors.Arrow, false, false);
    }

    public void TrackStageDrawLine()
    {
        if (_canvasSymbolsVM.DrawnLineSymbol == null)
        {
            StarDrawLine();
        }
        else
        {
            FinishDrawLine();
        }
    }

    public void OnPropertyChanged([CallerMemberName] string nameProperty = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameProperty));
    }

    public static void SetDisplayConnectionPoints(List<ConnectionPoint> connectionPoints, bool isShow)
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
            var connectionPoints = BlockSymbolVM.ConnectionPoints;

            SetDisplayConnectionPoints(connectionPoints, isEnterConnectionPoint);

            _canvasSymbolsVM.Cursor = cursorDisplaying;
            IsSelected = isSelectConnectionPoint;
        }
    }

    private void StarDrawLine()
    {
        var isLineOutputAccordingGOST = _connectionPointModel.IsLineOutputAccordingGOST();
        var isDrawingLinesAccordingGOST = _checkBoxLineGostVM.IsDrawingLinesAccordingGOST;

        if (isDrawingLinesAccordingGOST && !isLineOutputAccordingGOST)
        {
            MessageBox.Show("Выход линии должен быть снизу или справа");
            return;
        }

        IsHasConnectingLine = true;

        var drawnLineSymbolVM = new DrawnLineSymbolVM(_canvasSymbolsVM)
        {
            SymbolOutgoingLine = BlockSymbolVM,
            OutgoingPosition = Position,
            OutgoingConnectionPoint = this
        };

        drawnLineSymbolVM.AddFirstLine();
        drawnLineSymbolVM.RedrawPartLines();

        _canvasSymbolsVM.SymbolsVM.Add(drawnLineSymbolVM);
        _canvasSymbolsVM.DrawnLineSymbol = drawnLineSymbolVM;
    }

    private void FinishDrawLine()
    {
        var isLineIncomingAccordingGOST = _connectionPointModel.IsLineIncomingAccordingGOST();
        var isDrawingLinesAccordingGOST = _checkBoxLineGostVM.IsDrawingLinesAccordingGOST;

        if (isDrawingLinesAccordingGOST && !isLineIncomingAccordingGOST)
        {
            MessageBox.Show("Вход линии должен быть сверху или снизу");
            return;
        }

        var drawnLineSymbolVM = _canvasSymbolsVM.DrawnLineSymbol;

        if (drawnLineSymbolVM == null)
        {
            return;
        }

        var symbolOutgoingLine = drawnLineSymbolVM.SymbolOutgoingLine;

        if (symbolOutgoingLine == null)
        {
            return;
        }

        IsHasConnectingLine = true;

        drawnLineSymbolVM.IncomingPosition = Position;
        drawnLineSymbolVM.IncomingConnectionPoint = this;
        drawnLineSymbolVM.SymbolIncomingLine = BlockSymbolVM;

        var drawnLineSymbolModel = drawnLineSymbolVM.DrawnLineSymbolModel;
        var symbolIncomingLineModel = drawnLineSymbolVM.SymbolIncomingLine.BlockSymbolModel;

        var finalLineCoordinate = symbolIncomingLineModel.GetBorderCoordinate(Position);

        var completedLineModel = new CompletedLine(drawnLineSymbolModel, finalLineCoordinate);
        var completeLinesSymbolModel = completedLineModel.GetCompleteLines();

        drawnLineSymbolModel.LinesSymbolModel = completeLinesSymbolModel;

        drawnLineSymbolVM.RedrawAllLines();

        AddBlockToLine(symbolOutgoingLine, drawnLineSymbolVM);
        AddBlockToLine(BlockSymbolVM, drawnLineSymbolVM);

        _canvasSymbolsVM.DrawnLineSymbol = null;
    }

    private void AddBlockToLine(BlockSymbolVM blockSymbolVM, DrawnLineSymbolVM drawnLineSymbolVM)
    {
        var blockByDrawnLines = _canvasSymbolsVM.BlockByDrawnLines;

        if (!blockByDrawnLines.ContainsKey(blockSymbolVM))
        {
            var drawnLinesSymbolVM = new List<DrawnLineSymbolVM>
            {
                drawnLineSymbolVM
            };

            blockByDrawnLines.Add(blockSymbolVM, drawnLinesSymbolVM);
        }
        else
        {
            blockByDrawnLines[blockSymbolVM].Add(drawnLineSymbolVM);
        }
    }
}