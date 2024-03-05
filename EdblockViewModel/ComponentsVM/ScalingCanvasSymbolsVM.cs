using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.LineSymbols;

namespace EdblockViewModel.ComponentsVM;

public class ScalingCanvasSymbolsVM
{
    private readonly DispatcherTimer dispatcherTimer = new()
    {
        Interval = TimeSpan.FromSeconds(scalingInterval)
    };

    private readonly CanvasSymbolsVM _canvasSymbolsVM;

    private Action? _scrollOffset;

    private int _widthWindow;
    private int _heightWindow;
    private int _widthPanelSymbols;
    private int _heightTopSettingsPanel;

    private const int minIndentation = 40;
    private const int thicknessScroll = 14;
    private const int minNumberSymbolsScaling = 2;
    private const double scalingInterval = 0.05;

    public const int OFFSET_LEAVE = 40;

    public enum SideLeave
    {
        Top,
        Right,
        Bottom,
        Left
    }

    public ScalingCanvasSymbolsVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
    }

    public void SetActualSize(int widthWindow, int heightWindow, int widthPanelSymbols, int heightTopSettingsPanel)
    {
        _widthWindow = widthWindow;
        _heightWindow = heightWindow;
        _widthPanelSymbols = widthPanelSymbols;
        _heightTopSettingsPanel = heightTopSettingsPanel;

        if (_canvasSymbolsVM.Width < widthWindow)
        {
            _canvasSymbolsVM.Width = CalculateWidthCanvas();
        }

        if (_canvasSymbolsVM.Height < heightWindow)
        {
            _canvasSymbolsVM.Height = CalculateHeightCanvas();
        }
    }

    public void SubscribeСanvasScalingEvents(Action scrollOffset, Point cursotPoint)
    {
        _scrollOffset = scrollOffset;

        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;
        var countBlockSymbolsVM = _canvasSymbolsVM.BlockSymbolsVM.Count;
        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if ((movableBlockSymbol is null && currentDrawnLineSymbol is null) || (currentDrawnLineSymbol is null && countBlockSymbolsVM < minNumberSymbolsScaling))
        {
            return;
        }

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.MoveMiddle = true;
        }

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

    private void ScaleCanvas()
    {
        
    }

    public void UnsubscribeСanvasScalingEvents()
    {
        dispatcherTimer.Tick -= IncreaseSizeHorizontal;
        dispatcherTimer.Tick -= DecreaseSizeHorizontal;
        dispatcherTimer.Tick -= IncreaseSizeVertical;
        dispatcherTimer.Tick -= DecreaseSizeVertical;

        dispatcherTimer.Stop();
    }

    public SideLeave GetSideLeave(Point cursotPoint)
    {
        if (cursotPoint.X >= CalculateWidthCanvas())
        {
            return SideLeave.Right;
        }

        if (cursotPoint.X <= _widthPanelSymbols)
        {
            return SideLeave.Left;
        }

        if (cursotPoint.Y >= CalculateHeightCanvas())
        {
            return SideLeave.Bottom;
        }

        return SideLeave.Top;
    }

    private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    {
        _canvasSymbolsVM.Width += OFFSET_LEAVE;

        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.XCoordinate += OFFSET_LEAVE;
        }

        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (currentDrawnLineSymbol is not null)
        {
            ChangeCoordinateHorizontalLine(currentDrawnLineSymbol, OFFSET_LEAVE);
        }

        _scrollOffset?.Invoke();
        _canvasSymbolsVM.RedrawDrawnLinesSymbol(movableBlockSymbol);
    }

    private void IncreaseSizeVertical(object? sender, EventArgs e)
    {
        _canvasSymbolsVM.Height += OFFSET_LEAVE;

        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.YCoordinate += OFFSET_LEAVE;
        }

        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (currentDrawnLineSymbol is not null)
        {
            ChangeCoordinateVerticalLine(currentDrawnLineSymbol, OFFSET_LEAVE);
        }

        _scrollOffset?.Invoke();
        _canvasSymbolsVM.RedrawDrawnLinesSymbol(movableBlockSymbol);
    }

    private void DecreaseSizeHorizontal(object? sender, EventArgs e)
    {
        if (_canvasSymbolsVM.Width < CalculateWidthCanvas() + OFFSET_LEAVE / 2)
        {
            return;
        }

        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;

        if (movableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = movableBlockSymbol.XCoordinate + movableBlockSymbol.Width / 2;

            if (_widthPanelSymbols <= extremeCoordinateSymbolBehind)
            {
                movableBlockSymbol.XCoordinate -= OFFSET_LEAVE;
            }
        }

        var maxXCoordinate = GetMaxXCoordinateSymbol();

        if (_canvasSymbolsVM.Width > maxXCoordinate + minIndentation)
        {
            _canvasSymbolsVM.Width -= OFFSET_LEAVE;
        }

        _scrollOffset?.Invoke();
        _canvasSymbolsVM.RedrawDrawnLinesSymbol(movableBlockSymbol);

        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (currentDrawnLineSymbol is not null)
        {
            ChangeCoordinateHorizontalLine(currentDrawnLineSymbol, -OFFSET_LEAVE);
        }
    }

    private void DecreaseSizeVertical(object? sender, EventArgs e)
    {
        if (_canvasSymbolsVM.Height < CalculateHeightCanvas() + OFFSET_LEAVE / 2)
        {
            return;
        }

        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;

        if (movableBlockSymbol is not null)
        {

            var extremeCoordinateSymbolBehind = movableBlockSymbol.YCoordinate + movableBlockSymbol.Height / 2;

            if (_heightTopSettingsPanel <= extremeCoordinateSymbolBehind)
            {
                movableBlockSymbol.YCoordinate -= OFFSET_LEAVE;
            }
        }

        var maxYCoordinate = GetMaxYCoordinateSymbol();

        if (_canvasSymbolsVM.Height > maxYCoordinate + minIndentation)
        {
            _canvasSymbolsVM.Height -= OFFSET_LEAVE;
        }

        _scrollOffset?.Invoke();
        _canvasSymbolsVM.RedrawDrawnLinesSymbol(movableBlockSymbol);

        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (currentDrawnLineSymbol is not null)
        {
            ChangeCoordinateVerticalLine(currentDrawnLineSymbol, -OFFSET_LEAVE);
        }
    }

    private static bool IsTheLineHorizontal(LineSymbolVM lineSymbolVM) =>
        lineSymbolVM.Y1 == lineSymbolVM.Y1;

    private static void ChangeCoordinateHorizontalLine(DrawnLineSymbolVM currentDrawnLineSymbol, int offset)
    {
        var linesSymbolVM = currentDrawnLineSymbol.LinesSymbolVM;
        var lastLineSymbolVM = linesSymbolVM.Last();

        if (IsTheLineHorizontal(lastLineSymbolVM))
        {
            lastLineSymbolVM.X2 += offset;
        }
        else
        {
            if (linesSymbolVM.Count > 1)
            {
                var penultimateLineSymbolVM = linesSymbolVM[^2];
                penultimateLineSymbolVM.X2 += offset;
            }

            lastLineSymbolVM.X1 += offset;
            lastLineSymbolVM.X2 += offset;
        }

        currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
    }

    private static void ChangeCoordinateVerticalLine(DrawnLineSymbolVM currentDrawnLineSymbol, int offset)
    {
        var linesSymbolVM = currentDrawnLineSymbol.LinesSymbolVM;
        var lastLineSymbolVM = linesSymbolVM.Last();

        if (!IsTheLineHorizontal(lastLineSymbolVM))
        {
            lastLineSymbolVM.Y2 += offset;
        }
        else
        {
            if (linesSymbolVM.Count > 1)
            {
                var penultimateLineSymbolVM = linesSymbolVM[^2];
                penultimateLineSymbolVM.Y2 += offset;
            }

            lastLineSymbolVM.Y1 += offset;
            lastLineSymbolVM.Y2 += offset;
        }

        currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
    }

    private int CalculateWidthCanvas() =>
        _widthWindow - _widthPanelSymbols - OFFSET_LEAVE / 2 - thicknessScroll;

    private int CalculateHeightCanvas() =>
        _heightWindow - _heightTopSettingsPanel - OFFSET_LEAVE - thicknessScroll;

    private double GetMaxXCoordinateSymbol()
    {
        return GetMaxCoordinateSymbol(
            blockSymbolVM => blockSymbolVM.XCoordinate + blockSymbolVM.Width,
            lineSymbolVM => Math.Max(lineSymbolVM.X1, lineSymbolVM.X2));
    }

    private double GetMaxYCoordinateSymbol()
    {
        return GetMaxCoordinateSymbol(
            blockSymbolVM => blockSymbolVM.YCoordinate + blockSymbolVM.Height,
            lineSymbolVM => Math.Max(lineSymbolVM.Y1, lineSymbolVM.Y2));
    }

    private double GetMaxCoordinateSymbol(Func<BlockSymbolVM, double> getMaxCoordinateBlockSymbol, Func<LineSymbolVM, double> getMaxCoordinateLineSymbol)
    {
        var blockSymbolsVM = _canvasSymbolsVM.BlockSymbolsVM;
        var drawnLinesSymbolVM = _canvasSymbolsVM.DrawnLinesSymbolVM;

        var maxCoordinate = blockSymbolsVM.Max(blockSymbolVM => getMaxCoordinateBlockSymbol(blockSymbolVM));

        foreach (var drawnLineSymbol in drawnLinesSymbolVM)
        {
            maxCoordinate = drawnLineSymbol.LinesSymbolVM.Max(
                lineSymbolVM => Math.Max(maxCoordinate, getMaxCoordinateLineSymbol(lineSymbolVM)));
        }

        return maxCoordinate;
    }
}