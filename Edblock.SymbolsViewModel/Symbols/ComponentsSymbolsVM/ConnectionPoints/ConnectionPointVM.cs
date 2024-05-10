using System.Windows.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Prism.Commands;
using EdblockModel.EnumsModel;
using EdblockModel.SymbolsModel.LineSymbolsModel;
using Edblock.SymbolsViewModel.Symbols.LineSymbols;
using Edblock.SymbolsViewModel.Core;
using Edblock.PagesViewModel.Components;
using Edblock.PagesViewModel.Components.PopupBox;

namespace Edblock.SymbolsViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

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
    public BlockSymbolViewModel BlockSymbolVM { get; init; }
    public IHasConnection BlockSymbolHasConnectionPoint { get; init; }
    public SideSymbol Position { get; init; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private const int diametr = 8;

    private readonly CanvasSymbolsViewModel _canvasSymbolsViewModel;
    private readonly LineGostViewModel _lineGostViewModel;

    public ConnectionPointVM(
        CanvasSymbolsViewModel canvasSymbolsViewModel, 
        BlockSymbolViewModel blockSymbolViewModel,
        LineGostViewModel lineGostViewModel, 
        SideSymbol sideSymbol)
    {
        _canvasSymbolsViewModel = canvasSymbolsViewModel;
        _lineGostViewModel = lineGostViewModel;

        Position = sideSymbol;

        BlockSymbolVM = blockSymbolViewModel;
        BlockSymbolHasConnectionPoint = (IHasConnection)blockSymbolViewModel;

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
        if (_canvasSymbolsViewModel.CurrentDrawnLineSymbol == null)
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
        if (_canvasSymbolsViewModel.ScalePartBlockSymbol == null) //Код выполняется, если символ не масштабируется
        {
            var connectionPoints = BlockSymbolHasConnectionPoint.ConnectionPointsVM;

            SetDisplayConnectionPoints(connectionPoints, isEnterConnectionPoint);

            _canvasSymbolsViewModel.Cursor = cursorDisplaying;

            IsSelected = isSelectConnectionPoint;
        }
    }

    public void StarDrawLine()
    {
        var isDrawingLinesAccordingGOST = _lineGostViewModel.IsDrawingLinesAccordingGOST;

        var drawnLineSymbolVM = new DrawnLineSymbolVM(_canvasSymbolsViewModel)
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

        _canvasSymbolsViewModel.DrawnLinesSymbolVM.Add(drawnLineSymbolVM);
        _canvasSymbolsViewModel.CurrentDrawnLineSymbol = drawnLineSymbolVM;
    }

    public void FinishDrawLine()
    {
        if (_canvasSymbolsViewModel.CurrentDrawnLineSymbol is null)
        {
            return;
        }

        var drawnLineSymbolVM = _canvasSymbolsViewModel.CurrentDrawnLineSymbol;

        if (drawnLineSymbolVM == null)
        {
            return;
        }
        drawnLineSymbolVM.IncomingPosition = Position;


        var isLineIncomingAccordingGOST = _canvasSymbolsViewModel.CurrentDrawnLineSymbol.IsLineIncomingAccordingGOST();
        var isDrawingLinesAccordingGOST = _lineGostViewModel.IsDrawingLinesAccordingGOST;


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

        _canvasSymbolsViewModel.CurrentDrawnLineSymbol = null;
    }

    private void AddBlockToLine(BlockSymbolViewModel blockSymbolVM, DrawnLineSymbolVM drawnLineSymbolVM)
    {
        var blockByDrawnLines = _canvasSymbolsViewModel.BlockByDrawnLines;

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