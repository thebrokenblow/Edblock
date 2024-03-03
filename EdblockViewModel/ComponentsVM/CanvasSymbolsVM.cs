using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.ComponentModel;
using System.Windows.Threading;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using Prism.Commands;
using static EdblockViewModel.ComponentsVM.CanvasSymbolsVM;

namespace EdblockViewModel.ComponentsVM;

public class CanvasSymbolsVM : INotifyPropertyChanged
{
    public Rect Grid { get; init; }

    private int xCoordinate;
    private int previousXCoordinate;
    public int XCoordinate
    {
        get => xCoordinate;
        set
        {
            xCoordinate = RoundCoordinate(value);
            ScalePartBlockSymbol?.SetWidthBlockSymbol(this);
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
            ScalePartBlockSymbol?.SetHeightBlockSymbol(this);
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

    private int width;
    public int Width
    {
        get => width;
        set
        {
            width = value;
            OnPropertyChanged();
        }
    }

    private int height;  
    public int Height
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

    public ObservableCollection<BlockSymbolVM> BlockSymbolsVM { get; init; }
    public ObservableCollection<DrawnLineSymbolVM> DrawnLinesSymbolVM { get; init; }

    public Dictionary<BlockSymbolVM, List<DrawnLineSymbolVM>> BlockByDrawnLines { get; init; }

    public BlockSymbolVM? MovableBlockSymbol { get; set; }
    public ScalePartBlockSymbol? ScalePartBlockSymbol { get; set; }

    public DrawnLineSymbolVM? CurrentDrawnLineSymbol { get; set; }
    public DrawnLineSymbolVM? SelectedDrawnLineSymbol { get; set; }
    public List<BlockSymbolVM> SelectedBlockSymbols { get; set; }
    private List<DrawnLineSymbolVM>? DrawnLines { get; set; }
    public MovableRectangleLine? MovableRectangleLine { get; set; }

    public event PropertyChangedEventHandler? PropertyChanged;

    private const int lengthGridCell = 20;


    private readonly DispatcherTimer dispatcherTimer;

    private int widthWindow;
    private int heightWindow;
    private int widthPanelSymbols;
    private int heightTopSettingsPanel;

    private const int offsetLeaveCanvas = 40;
    private const int minIndentation = 40;
    private const int heightScroll = 14;

    public CanvasSymbolsVM()
    {
        dispatcherTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(0.05)
        };

        BlockSymbolsVM = new();
        BlockByDrawnLines = new();
        DrawnLinesSymbolVM = new();
        SelectedBlockSymbols = new();
        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValue);
        MouseLeftButtonDown = new(AddLine);

        cursor = Cursors.Arrow;

        Grid = new Rect(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);
    }

    public void DeleteSymbols()
    {
        MovableBlockSymbol = null;
        DeleteCurrentDrawnLineSymbol();
        DeleteSelectedDrawnLineSymbol();

        foreach (var symbol in SelectedBlockSymbols)
        {
            if (BlockByDrawnLines.ContainsKey(symbol))
            {
                var lines = BlockByDrawnLines[symbol];

                foreach (var line in lines)
                {
                    var symbolOut = line.SymbolOutgoingLine;
                    var symbolInc = line.SymbolIncomingLine;

                    if (symbol == symbolOut)
                    {
                        BlockByDrawnLines[symbolInc].Remove(line);
                    }
                    else
                    {
                        BlockByDrawnLines[symbolOut].Remove(line);
                    }

                    var outgoingConnectionPoint = line.OutgoingConnectionPoint;
                    var incomingConnectionPoint = line.IncomingConnectionPoint;

                    if (outgoingConnectionPoint is not null && incomingConnectionPoint is not null)
                    {
                        outgoingConnectionPoint.IsHasConnectingLine = false;
                        incomingConnectionPoint.IsHasConnectingLine = false;
                    }

                    DrawnLinesSymbolVM.Remove(line);
                }
                BlockByDrawnLines.Remove(symbol);
            }

            BlockSymbolsVM.Remove(symbol);
        }
    }

    private void DeleteCurrentDrawnLineSymbol()
    {
        if (CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (CurrentDrawnLineSymbol.OutgoingConnectionPoint is null)
        {
            return;
        }

        DrawnLinesSymbolVM.Remove(CurrentDrawnLineSymbol);

        var outgoingConnectionPoint = CurrentDrawnLineSymbol.OutgoingConnectionPoint;
        outgoingConnectionPoint.IsHasConnectingLine = false;

        CurrentDrawnLineSymbol = null;
    }

    private void DeleteSelectedDrawnLineSymbol()
    {
        if (SelectedDrawnLineSymbol is null)
        {
            return;
        }

        var symbolOutgoingLine = SelectedDrawnLineSymbol.SymbolOutgoingLine;
        var symbolIncomingLine = SelectedDrawnLineSymbol.SymbolIncomingLine;

        if (symbolOutgoingLine is not null && symbolIncomingLine is not null)
        {
            BlockByDrawnLines[symbolOutgoingLine].Remove(SelectedDrawnLineSymbol);
            BlockByDrawnLines[symbolIncomingLine].Remove(SelectedDrawnLineSymbol);
        }

        var outgoingConnectionPoint = SelectedDrawnLineSymbol.OutgoingConnectionPoint;
        var incomingConnectionPoint = SelectedDrawnLineSymbol.IncomingConnectionPoint;

        if (outgoingConnectionPoint is not null && incomingConnectionPoint is not null)
        {
            outgoingConnectionPoint.IsHasConnectingLine = false;
            incomingConnectionPoint.IsHasConnectingLine = false;
        }

        DrawnLinesSymbolVM.Remove(SelectedDrawnLineSymbol);

        SelectedDrawnLineSymbol = null;
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        RemoveSelectDrawnLine();
        ClearSelectedBlockSymbols();

        blockSymbolVM.IsMovingThroughMiddle = true;
        MovableBlockSymbol = blockSymbolVM;
        MovableBlockSymbol.Select();
        
        BlockSymbolsVM.Add(blockSymbolVM);
    }

    //TODO: Подумать над названием метода
    private void AddLine()
    {
        CurrentDrawnLineSymbol?.AddLine();
        ClearSelectedBlockSymbols();
        RemoveSelectDrawnLine();
    }

    public void RemoveSelectDrawnLine()
    {
        if (SelectedDrawnLineSymbol != null && MovableRectangleLine == null)
        {
            var copySelectDrawnLineSymbol = SelectedDrawnLineSymbol;
            SelectedDrawnLineSymbol = null;
            copySelectDrawnLineSymbol.SetDefaultColorLines();
        }
    }

    private void SetDefaultValue()
    {
        Cursor = Cursors.Arrow;

        DrawnLines = null;

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.IsMovingThroughMiddle = false;
        }

        MovableBlockSymbol = null;

        MovableRectangleLine = null;
        ScalePartBlockSymbol = null;

        previousXCoordinate = 0;
        previousYCoordinate = 0;

        TextFieldSymbolVM.ChangeFocus(BlockSymbolsVM);
    }

    private void ClearSelectedBlockSymbols()
    {
        foreach (var selectedBlockSymbols in SelectedBlockSymbols)
        {
            if (selectedBlockSymbols != MovableBlockSymbol)
            {
                selectedBlockSymbols.IsSelected = false;
            }
        }

        SelectedBlockSymbols.RemoveAll(x => x != MovableBlockSymbol);
    }

    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    private static int RoundCoordinate(int coordinate) //Округление координат, чтобы символ перемещался по сетке
    {
        int roundedCoordinate = coordinate - coordinate % (lengthGridCell / 2);

        return roundedCoordinate;
    }

    public void SetCurrentRedrawLines(BlockSymbolVM blockSymbolVM)
    {
        if (BlockByDrawnLines.ContainsKey(blockSymbolVM))
        {
            DrawnLines = BlockByDrawnLines[blockSymbolVM];
        }
    }

    private void RedrawSymbols()
    {
        var currentCoordinate = (xCoordinate, yCoordinate);
        var previousCoordinate = (previousXCoordinate, previousYCoordinate);

        MovableBlockSymbol?.SetCoordinate(currentCoordinate, previousCoordinate);
        CurrentDrawnLineSymbol?.ChangeCoordination(currentCoordinate);
        MovableRectangleLine?.ChangeCoordinateLine(currentCoordinate);

        previousXCoordinate = xCoordinate;
        previousYCoordinate = yCoordinate;

        if (DrawnLines is not null)
        {
            foreach (var redrawDrawnLine in DrawnLines)
            {
                redrawDrawnLine.Redraw();
            }
        }
    }

    public void RedrawnAllDrawnLines()
    {
        foreach (var item in BlockByDrawnLines)
        {
            var drawnLines = BlockByDrawnLines[item.Key];

            foreach (var redrawDrawnLine in drawnLines)
            {
                redrawDrawnLine.Redraw();
            }
        }
    }

    public void RemoveSelectedSymbol()
    {
        foreach (var selectedBlockSymbol in SelectedBlockSymbols)
        {
            selectedBlockSymbol.IsSelected = false;
        }

        SelectedDrawnLineSymbol = null;
    }

    public void SetActualSizeWindow(int actualWidth, int actualHeight, int actualWidthPanelSymbols, int actualHeightTopSettingsPanel)
    {
        widthWindow = actualWidth;
        heightWindow = actualHeight;
        widthPanelSymbols = actualWidthPanelSymbols;
        heightTopSettingsPanel = actualHeightTopSettingsPanel;

        if (Width < widthWindow)
        {
            Width = widthWindow - widthPanelSymbols - offsetLeaveCanvas;
        }

        if (Height < heightWindow)
        {
            Height = heightWindow - heightTopSettingsPanel - offsetLeaveCanvas - heightScroll;
        }
    }

    public void SubscribeScalingMethod(Point positionCursor)
    {
        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.IsMovingThroughMiddle = true;
        }

        var sideLeave = GetSideLeave(positionCursor);

        if (sideLeave == SideLeave.Top)
        {
            dispatcherTimer.Tick += DecreaseSizeVertical;
        }
        else if (sideLeave == SideLeave.Right)
        {
            dispatcherTimer.Tick += IncreaseSizeHorizontal;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            dispatcherTimer.Tick += IncreaseSizeVertical;
        }
        else if (sideLeave == SideLeave.Left)
        {
            dispatcherTimer.Tick += DecreaseSizeHorizontal;
        }

        dispatcherTimer.Start();
    }

    public void UnsubscribeScalingMethod()
    {
        dispatcherTimer.Tick -= IncreaseSizeVertical;
        dispatcherTimer.Tick -= DecreaseSizeVertical;

        dispatcherTimer.Tick -= IncreaseSizeHorizontal;
        dispatcherTimer.Tick -= DecreaseSizeHorizontal;

        dispatcherTimer.Stop();
    }

    private void IncreaseSizeVertical(object? sender, EventArgs e)
    {
        Height += offsetLeaveCanvas;

        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.YCoordinate += offsetLeaveCanvas;
        }
        else if (CurrentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = CurrentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.Y2 += offsetLeaveCanvas;

            CurrentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }
    }

    private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    {
        Width += offsetLeaveCanvas;

        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.XCoordinate += offsetLeaveCanvas;
        }
        else if (CurrentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = CurrentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.X2 += offsetLeaveCanvas;

            CurrentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }
    }

    private void DecreaseSizeHorizontal(object? sender, EventArgs e)
    {
        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (Width < widthWindow - widthPanelSymbols - offsetLeaveCanvas)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = MovableBlockSymbol.XCoordinate + MovableBlockSymbol.Width / 2;

            if (Width < widthWindow)
            {
                Width = widthWindow - widthPanelSymbols - offsetLeaveCanvas;
            }

            if (widthPanelSymbols <= extremeCoordinateSymbolBehind)
            {
                MovableBlockSymbol.XCoordinate -= offsetLeaveCanvas;
            }
        }

        if (CurrentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = CurrentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.X2 -= offsetLeaveCanvas;

            CurrentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var maxXCoordinate = GetMaxXCoordinateSymbol();

        if (Width > maxXCoordinate + minIndentation)
        {
            Width -= offsetLeaveCanvas;
        }
    }

    private void DecreaseSizeVertical(object? sender, EventArgs e)
    {
        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (heightWindow - heightTopSettingsPanel - offsetLeaveCanvas >= Height)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = MovableBlockSymbol.YCoordinate + MovableBlockSymbol.Height / 2;

            if (heightTopSettingsPanel <= extremeCoordinateSymbolBehind)
            {
                MovableBlockSymbol.YCoordinate -= offsetLeaveCanvas;
            }
        }

        if (CurrentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = CurrentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.Y2 -= offsetLeaveCanvas;

            CurrentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var maxYCoordinate = GetMaxYCoordinateSymbol();

        if (Height > maxYCoordinate + minIndentation)
        {
            Height -= offsetLeaveCanvas;
        }
    }

    private double GetMaxXCoordinateSymbol()
    {
        var maxXCoordinate = BlockSymbolsVM.Max(blockSymbolVM => blockSymbolVM.XCoordinate + blockSymbolVM.Width);

        foreach (var drawnLineSymbolVM in DrawnLinesSymbolVM)
        {
            foreach (var lineSymbolVM in drawnLineSymbolVM.LinesSymbolVM)
            {
                maxXCoordinate = Math.Max(maxXCoordinate, Math.Max(lineSymbolVM.X1, lineSymbolVM.X2));
            }
        }

        return maxXCoordinate;
    }

    private double GetMaxYCoordinateSymbol()
    {
        var maxYCoordinate = BlockSymbolsVM.Max(blockSymbolsVM => blockSymbolsVM.YCoordinate + blockSymbolsVM.Height);

        foreach (var drawnLinesSymbol in DrawnLinesSymbolVM)
        {
            foreach (var linesSymbol in drawnLinesSymbol.LinesSymbolVM)
            {
                maxYCoordinate = Math.Max(maxYCoordinate, Math.Max(linesSymbol.Y1, linesSymbol.Y2));
            }
        }

        return maxYCoordinate;
    }

    public enum SideLeave
    {
        Top,
        Right,
        Bottom,
        Left,
    }

    public SideLeave GetSideLeave(Point positionCursor)
    {
        if (positionCursor.X >= (widthWindow - widthPanelSymbols - offsetLeaveCanvas - heightScroll))
        {
            return SideLeave.Right;
        }

        if (positionCursor.X <= widthPanelSymbols)
        {
            return SideLeave.Left;
        }

        if (positionCursor.Y >= heightWindow - heightTopSettingsPanel - offsetLeaveCanvas - heightScroll)
        {
            return SideLeave.Bottom;
        }
        
        return SideLeave.Top;
    }
}