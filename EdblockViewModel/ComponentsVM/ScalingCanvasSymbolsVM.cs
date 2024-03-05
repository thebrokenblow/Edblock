using System;
using System.Linq;
using System.Windows;
using System.Windows.Threading;

namespace EdblockViewModel.ComponentsVM;

public class ScalingCanvasSymbolsVM
{
    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly DispatcherTimer dispatcherTimer;

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

        dispatcherTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(scalingInterval)
        };
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

    private void DecreaseSizeHorizontal(object? sender, EventArgs e)
    {
        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;
        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        if (_canvasSymbolsVM.Width < CalculateWidthCanvas() + OFFSET_LEAVE / 2)
        {
            return;
        }

        if (movableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = movableBlockSymbol.XCoordinate + movableBlockSymbol.Width / 2;

            if (_widthPanelSymbols <= extremeCoordinateSymbolBehind)
            {
                movableBlockSymbol.XCoordinate -= OFFSET_LEAVE;

                _canvasSymbolsVM.SetCurrentRedrawLines(movableBlockSymbol);

                if (_canvasSymbolsVM.DrawnLines is not null)
                {
                    foreach (var redrawDrawnLine in _canvasSymbolsVM.DrawnLines)
                    {
                        redrawDrawnLine.Redraw();
                    }
                }
            }
        }
        else if (currentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];

            if (lastLineSymbolVM.Y1 == lastLineSymbolVM.Y2)
            {
                lastLineSymbolVM.X2 -= OFFSET_LEAVE;
            }
            else
            {
                if (currentDrawnLineSymbol.LinesSymbolVM.Count > 1)
                {
                    var penultimateLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^2];
                    penultimateLineSymbolVM.X2 -= OFFSET_LEAVE;
                }

                lastLineSymbolVM.X1 -= OFFSET_LEAVE;
                lastLineSymbolVM.X2 -= OFFSET_LEAVE;
            }

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var maxXCoordinate = GetMaxXCoordinateSymbol();

        if (_canvasSymbolsVM.Width > maxXCoordinate + minIndentation)
        {
            _canvasSymbolsVM.Width -= OFFSET_LEAVE;
        }

        _scrollOffset?.Invoke();
    }

    private void DecreaseSizeVertical(object? sender, EventArgs e)
    {
        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;
        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        if (_canvasSymbolsVM.Height < CalculateHeightCanvas() + OFFSET_LEAVE / 2)
        {
            return;
        }

        if (movableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = movableBlockSymbol.YCoordinate + movableBlockSymbol.Height / 2;

            if (_heightTopSettingsPanel <= extremeCoordinateSymbolBehind)
            {
                movableBlockSymbol.YCoordinate -= OFFSET_LEAVE;

                _canvasSymbolsVM.SetCurrentRedrawLines(movableBlockSymbol);

                if (_canvasSymbolsVM.DrawnLines is not null)
                {
                    foreach (var redrawDrawnLine in _canvasSymbolsVM.DrawnLines)
                    {
                        redrawDrawnLine.Redraw();
                    }
                }
            }
        }

        if (currentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];

            if (lastLineSymbolVM.X1 == lastLineSymbolVM.X2)
            {
                lastLineSymbolVM.Y2 -= OFFSET_LEAVE;
            }
            else
            {
                if (currentDrawnLineSymbol.LinesSymbolVM.Count > 1)
                {
                    var penultimateLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^2];
                    penultimateLineSymbolVM.Y2 -= OFFSET_LEAVE;
                }

                lastLineSymbolVM.Y1 -= OFFSET_LEAVE;
                lastLineSymbolVM.Y2 -= OFFSET_LEAVE;
            }

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var maxYCoordinate = GetMaxYCoordinateSymbol();


        if (_canvasSymbolsVM.Height > maxYCoordinate + minIndentation)
        {
            _canvasSymbolsVM.Height -= OFFSET_LEAVE;
        }

        _scrollOffset?.Invoke();
    }

    private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    {
        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;
        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        _canvasSymbolsVM.Width += OFFSET_LEAVE;

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.XCoordinate += OFFSET_LEAVE;

            _canvasSymbolsVM.SetCurrentRedrawLines(movableBlockSymbol);

            if (_canvasSymbolsVM.DrawnLines is not null)
            {
                foreach (var redrawDrawnLine in _canvasSymbolsVM.DrawnLines)
                {
                    redrawDrawnLine.Redraw();
                }
            }
            
        }
        else if (currentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];

            if (lastLineSymbolVM.Y1 == lastLineSymbolVM.Y2)
            {
                lastLineSymbolVM.X2 += OFFSET_LEAVE;
            }
            else
            {
                if (currentDrawnLineSymbol.LinesSymbolVM.Count > 1)
                {
                    var penultimateLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^2];
                    penultimateLineSymbolVM.X2 += OFFSET_LEAVE;
                }

                lastLineSymbolVM.X1 += OFFSET_LEAVE;
                lastLineSymbolVM.X2 += OFFSET_LEAVE;
            }

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        _scrollOffset?.Invoke();
    }

    private void IncreaseSizeVertical(object? sender, EventArgs e)
    {
        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;
        var currentDrawnLineSymbol = _canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        _canvasSymbolsVM.Height += OFFSET_LEAVE;

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.YCoordinate += OFFSET_LEAVE;

            _canvasSymbolsVM.SetCurrentRedrawLines(movableBlockSymbol);

            if (_canvasSymbolsVM.DrawnLines is not null)
            {
                foreach (var redrawDrawnLine in _canvasSymbolsVM.DrawnLines)
                {
                    redrawDrawnLine.Redraw();
                }
            }
        }
        else if (currentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];

            if (lastLineSymbolVM.X1 == lastLineSymbolVM.X2)
            {
                lastLineSymbolVM.Y2 += OFFSET_LEAVE;
            }
            else
            {
                if (currentDrawnLineSymbol.LinesSymbolVM.Count > 1)
                {
                    var penultimateLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^2];
                    penultimateLineSymbolVM.Y2 += OFFSET_LEAVE;
                }

                lastLineSymbolVM.Y1 += OFFSET_LEAVE;
                lastLineSymbolVM.Y2 += OFFSET_LEAVE;
            }

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        _scrollOffset?.Invoke();
    }

    private int CalculateWidthCanvas() =>
        _widthWindow - _widthPanelSymbols - OFFSET_LEAVE / 2 - thicknessScroll;

    private int CalculateHeightCanvas() =>
        _heightWindow - OFFSET_LEAVE - thicknessScroll - _heightTopSettingsPanel;

    private double GetMaxXCoordinateSymbol()
    {
        var blockSymbolsVM = _canvasSymbolsVM.BlockSymbolsVM;
        var drawnLinesSymbolVM = _canvasSymbolsVM.DrawnLinesSymbolVM;

        var maxXCoordinate = blockSymbolsVM.Max(blockSymbolVM => blockSymbolVM.XCoordinate + blockSymbolVM.Width);

        foreach (var drawnLineSymbolVM in drawnLinesSymbolVM)
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
        var blockSymbolsVM = _canvasSymbolsVM.BlockSymbolsVM;
        var drawnLinesSymbolVM = _canvasSymbolsVM.DrawnLinesSymbolVM;

        var maxYCoordinate = blockSymbolsVM.Max(blockSymbolVM => blockSymbolVM.YCoordinate + blockSymbolVM.Height);

        foreach (var drawnLinesSymbol in drawnLinesSymbolVM)
        {
            foreach (var linesSymbol in drawnLinesSymbol.LinesSymbolVM)
            {
                maxYCoordinate = Math.Max(maxYCoordinate, Math.Max(linesSymbol.Y1, linesSymbol.Y2));
            }
        }
        return maxYCoordinate;
    }
}