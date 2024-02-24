using Prism.Commands;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using EdblockViewModel.AbstractionsVM;
using EdblockModel.EnumsModel;
using System;

namespace EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

public class ConnectionPointVM : INotifyPropertyChanged
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

    public DelegateCommand EnterCursor { get; init; }
    public DelegateCommand LeaveCursor { get; init; }
    public BlockSymbolVM BlockSymbolVM { get; init; }
    public IHasConnectionPoint BlockSymbolHasConnectionPoint { get; init; }
    public SideSymbol Position { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private const int diametr = 8;

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CheckBoxLineGostVM _checkBoxLineGostVM;

    public ConnectionPointVM(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM, CheckBoxLineGostVM checkBoxLineGostVM, SideSymbol position)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;

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

    public void Click()
    {
        if (_canvasSymbolsVM.СurrentDrawnLineSymbol == null)
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

    public void StarDrawLine()
    {
        var isDrawingLinesAccordingGOST = _checkBoxLineGostVM.IsDrawingLinesAccordingGOST;

        var drawnLineSymbolVM = new DrawnLineSymbolVM(_canvasSymbolsVM)
        {
            SymbolOutgoingLine = BlockSymbolVM,
            OutgoingPosition = Position,
            OutgoingConnectionPoint = this
        };

        var isLineOutputAccordingGOST = drawnLineSymbolVM.IsLineOutputAccordingGOST();
        
        if (isDrawingLinesAccordingGOST && !isLineOutputAccordingGOST)
        {
            throw new Exception("Выход линии должен быть снизу или справа");
        }

        IsHasConnectingLine = true;

        drawnLineSymbolVM.AddFirstLine(BlockSymbolVM.XCoordinate + XCoordinateLineDraw, BlockSymbolVM.YCoordinate + YCoordinateLineDraw);
        drawnLineSymbolVM.RedrawPartLines();

        _canvasSymbolsVM.DrawnLinesSymbolVM.Add(drawnLineSymbolVM);
        _canvasSymbolsVM.СurrentDrawnLineSymbol = drawnLineSymbolVM;
    }

    public void FinishDrawLine()
    {
        if (_canvasSymbolsVM.СurrentDrawnLineSymbol is null)
        {
            return;
        }

        var drawnLineSymbolVM = _canvasSymbolsVM.СurrentDrawnLineSymbol;

        if (drawnLineSymbolVM == null)
        {
            return;
        }
        drawnLineSymbolVM.IncomingPosition = Position;


        var isLineIncomingAccordingGOST = _canvasSymbolsVM.СurrentDrawnLineSymbol.IsLineIncomingAccordingGOST();
        var isDrawingLinesAccordingGOST = _checkBoxLineGostVM.IsDrawingLinesAccordingGOST;


        if (isDrawingLinesAccordingGOST && !isLineIncomingAccordingGOST)
        {
            throw new Exception("Вход линии должен быть сверху или снизу");
        }


        var symbolOutgoingLine = drawnLineSymbolVM.SymbolOutgoingLine;

        if (symbolOutgoingLine == null)
        {
            return;
        }

        IsHasConnectingLine = true;

        drawnLineSymbolVM.IncomingConnectionPoint = this;
        drawnLineSymbolVM.SymbolIncomingLine = BlockSymbolVM;

        var drawnLineSymbolModel = drawnLineSymbolVM.DrawnLineSymbolModel;

        var finalLineCoordinate = (BlockSymbolVM.XCoordinate + XCoordinateLineDraw, BlockSymbolVM.YCoordinate + YCoordinateLineDraw);

        var completedLineModel = new CompletedLine(drawnLineSymbolModel, finalLineCoordinate);
        var completeLinesSymbolModel = completedLineModel.GetCompleteLines();

        drawnLineSymbolModel.LinesSymbolModel = completeLinesSymbolModel;

        drawnLineSymbolVM.RedrawAllLines();

        AddBlockToLine(symbolOutgoingLine, drawnLineSymbolVM);
        AddBlockToLine(BlockSymbolVM, drawnLineSymbolVM);

        _canvasSymbolsVM.СurrentDrawnLineSymbol = null;
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