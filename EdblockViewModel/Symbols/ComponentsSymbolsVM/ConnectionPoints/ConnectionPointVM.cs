using Prism.Commands;
using System.Windows;
using EdblockModel.Symbols;
using System.Windows.Input;
using System.ComponentModel;
using System.Collections.Generic;
using EdblockViewModel.ComponentsVM;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using EdblockViewModel.AbstractionsVM;
using EdblockModel.Enum;

namespace EdblockViewModel.Symbols.ConnectionPoints;

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
    private readonly ConnectionPointModel _connectionPointModel;

    public ConnectionPointVM(CanvasSymbolsVM canvasSymbolsVM, BlockSymbolVM blockSymbolVM, CheckBoxLineGostVM checkBoxLineGostVM, SideSymbol position)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _checkBoxLineGostVM = checkBoxLineGostVM;
        _connectionPointModel = new(position);

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
            var connectionPoints = BlockSymbolHasConnectionPoint.ConnectionPoints;

            SetDisplayConnectionPoints(connectionPoints, isEnterConnectionPoint);

            _canvasSymbolsVM.Cursor = cursorDisplaying;
            IsSelected = isSelectConnectionPoint;
        }
    }

    public void StarDrawLine()
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

        drawnLineSymbolVM.AddFirstLine(BlockSymbolVM.XCoordinate + XCoordinateLineDraw, BlockSymbolVM.YCoordinate + YCoordinateLineDraw);
        drawnLineSymbolVM.RedrawPartLines();

        _canvasSymbolsVM.SymbolsVM.Add(drawnLineSymbolVM);
        _canvasSymbolsVM.СurrentDrawnLineSymbol = drawnLineSymbolVM;
    }

    public void FinishDrawLine()
    {
        var isLineIncomingAccordingGOST = _connectionPointModel.IsLineIncomingAccordingGOST();
        var isDrawingLinesAccordingGOST = _checkBoxLineGostVM.IsDrawingLinesAccordingGOST;

        if (isDrawingLinesAccordingGOST && !isLineIncomingAccordingGOST)
        {
            MessageBox.Show("Вход линии должен быть сверху или снизу");
            return;
        }

        var drawnLineSymbolVM = _canvasSymbolsVM.СurrentDrawnLineSymbol;

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