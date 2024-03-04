using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using EdblockViewModel.Symbols.LineSymbols;
using EdblockViewModel.Symbols.ComponentsSymbolsVM;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ScaleRectangles;
using EdblockViewModel.AbstractionsVM;
using Prism.Commands;

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
    private readonly DispatcherTimer dispatcherTimer;

    private const int lengthGridCell = 20;

    public const int OFFSET_LEAVE = 40;
    private const int minIndentation = 40;
    private const int thicknessScroll = 14;

    public CanvasSymbolsVM()
    {
        BlockSymbolsVM = new();
        BlockByDrawnLines = new();
        DrawnLinesSymbolVM = new();
        SelectedBlockSymbols = new();
        MouseMove = new(RedrawSymbols);
        MouseUp = new(SetDefaultValue);
        MouseLeftButtonDown = new(AddLine);

        cursor = Cursors.Arrow;

        Grid = new Rect(-lengthGridCell, -lengthGridCell, lengthGridCell, lengthGridCell);

        dispatcherTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(0.05)
        };
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

        if (symbolOutgoingLine is not null)
        {
            BlockByDrawnLines[symbolOutgoingLine].Remove(SelectedDrawnLineSymbol);
        }

        if (symbolIncomingLine is not null)
        {
            BlockByDrawnLines[symbolIncomingLine].Remove(SelectedDrawnLineSymbol);
        }

        var outgoingConnectionPoint = SelectedDrawnLineSymbol.OutgoingConnectionPoint;
        var incomingConnectionPoint = SelectedDrawnLineSymbol.IncomingConnectionPoint;

        if (outgoingConnectionPoint is not null)
        {
            outgoingConnectionPoint.IsHasConnectingLine = false;
        }

        if (incomingConnectionPoint is not null)
        {
            incomingConnectionPoint.IsHasConnectingLine = false;
        }

        DrawnLinesSymbolVM.Remove(SelectedDrawnLineSymbol);

        SelectedDrawnLineSymbol = null;
    }

    public void AddBlockSymbol(BlockSymbolVM blockSymbolVM)
    {
        RemoveSelectDrawnLine();
        ClearSelectedBlockSymbols();

        blockSymbolVM.FirstMove = true;
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
            MovableBlockSymbol.FirstMove = false;
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

    private int _widthWindow;
    private int _heightWindow;
    private int _widthPanelSymbols;
    private int _heightTopSettingsPanel;

    public void SetActualSize(int widthWindow, int heightWindow, int widthPanelSymbols, int heightTopSettingsPanel)
    {
        _widthWindow = widthWindow;
        _heightWindow = heightWindow;
        _widthPanelSymbols = widthPanelSymbols;
        _heightTopSettingsPanel = heightTopSettingsPanel;

        if (Width < widthWindow)
        {
            Width = widthWindow - widthPanelSymbols - OFFSET_LEAVE / 2 - thicknessScroll;
        }

        if (Height < heightWindow)
        {
            Height = heightWindow - OFFSET_LEAVE - thicknessScroll - _heightTopSettingsPanel;
        }
    }

    private Action? _scrollOffset;
    public void SubscribeСanvasScalingEvents(Action scrollOffset, Point cursotPoint)
    {
        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.FirstMove = true;
        }

        _scrollOffset = scrollOffset;

        var sideLeave = GetSideLeave(cursotPoint);

        if (sideLeave == SideLeave.Right)
        {
            dispatcherTimer.Tick += IncreaseSizeHorizontal;
        }
        else if (sideLeave == SideLeave.Left)
        {
            dispatcherTimer.Tick += DecreaseSizeHorizontal;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            dispatcherTimer.Tick += IncreaseSizeVertical;
        }
        else
        {
            dispatcherTimer.Tick += DecreaseSizeVertical;
        }

        dispatcherTimer.Start();
    }
    private void DecreaseSizeHorizontal(object? sender, EventArgs e)
    {
        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (Width < _widthWindow - _widthPanelSymbols - thicknessScroll)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = MovableBlockSymbol.XCoordinate + MovableBlockSymbol.Width / 2;

            if (_widthPanelSymbols <= extremeCoordinateSymbolBehind)
            {
                MovableBlockSymbol.XCoordinate -= OFFSET_LEAVE;
            }
        }
        else if (CurrentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = CurrentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.X2 -= OFFSET_LEAVE;

            CurrentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var maxXCoordinate = GetMaxXCoordinateSymbol();

        if (Width > maxXCoordinate + minIndentation)
        {
            Width -= OFFSET_LEAVE;
        }

        _scrollOffset?.Invoke();
    }

    private void DecreaseSizeVertical(object? sender, EventArgs e)
    {
        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (Height < _heightWindow - OFFSET_LEAVE / 2 - thicknessScroll - _heightTopSettingsPanel)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = MovableBlockSymbol.YCoordinate + MovableBlockSymbol.Height / 2;

            if (_heightTopSettingsPanel <= extremeCoordinateSymbolBehind)
            {
                MovableBlockSymbol.YCoordinate -= OFFSET_LEAVE;
            }
        }

        if (CurrentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = CurrentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.Y2 -= OFFSET_LEAVE;

            CurrentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var maxYCoordinate = GetMaxYCoordinateSymbol();


        if (Height > maxYCoordinate + minIndentation)
        {
            Height -= OFFSET_LEAVE;
        }

        _scrollOffset?.Invoke();
    }

    private double GetMaxYCoordinateSymbol()
    {
        var maxYCoordinate = BlockSymbolsVM.Max(b => b.YCoordinate + b.Height);

        foreach (var drawnLinesSymbol in DrawnLinesSymbolVM)
        {
            foreach (var linesSymbol in drawnLinesSymbol.LinesSymbolVM)
            {
                maxYCoordinate = Math.Max(maxYCoordinate, Math.Max(linesSymbol.Y1, linesSymbol.Y2));
            }
        }

        return maxYCoordinate;
    }

    private double GetMaxXCoordinateSymbol()
    {
        var maxXCoordinate = BlockSymbolsVM.Max(vm => vm.XCoordinate + vm.Width);

        foreach (var drawnLineSymbolVM in DrawnLinesSymbolVM)
        {
            foreach (var lineSymbolVM in drawnLineSymbolVM.LinesSymbolVM)
            {
                maxXCoordinate = Math.Max(maxXCoordinate, Math.Max(lineSymbolVM.X1, lineSymbolVM.X2));
            }
        }

        return maxXCoordinate;
    }


    public enum SideLeave
    {
        Top,
        Right,
        Bottom,
        Left
    }

    public SideLeave GetSideLeave(Point cursotPoint)
    {
        if (cursotPoint.X >= _widthWindow - _widthPanelSymbols - OFFSET_LEAVE / 2 - thicknessScroll)
        {
            return SideLeave.Right;
        }
        
        if (cursotPoint.X <= _widthPanelSymbols)
        {
            return SideLeave.Left;
        }

        if (cursotPoint.Y >= _heightWindow - OFFSET_LEAVE - thicknessScroll - _heightTopSettingsPanel)
        {
            return SideLeave.Bottom;
        }

        return SideLeave.Top;
    }

    private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    {
        Width += OFFSET_LEAVE;

        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.XCoordinate += OFFSET_LEAVE;
        }
        else if (CurrentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = CurrentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.X2 += OFFSET_LEAVE;

            CurrentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        _scrollOffset?.Invoke();
    }

    private void IncreaseSizeVertical(object? sender, EventArgs e)
    {
        Height += OFFSET_LEAVE;

        if (MovableBlockSymbol is null && CurrentDrawnLineSymbol is null)
        {
            return;
        }

        if (MovableBlockSymbol is not null)
        {
            MovableBlockSymbol.YCoordinate += OFFSET_LEAVE;
        }
        else if (CurrentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = CurrentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.Y2 += OFFSET_LEAVE;

            CurrentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        _scrollOffset?.Invoke();
    }


    public void UnsubscribeСanvasScalingEvents()
    {
        dispatcherTimer.Tick -= IncreaseSizeHorizontal;
        dispatcherTimer.Tick -= DecreaseSizeHorizontal;
        dispatcherTimer.Tick -= IncreaseSizeVertical;
        dispatcherTimer.Tick -= DecreaseSizeVertical;

        dispatcherTimer.Stop();
    }
}