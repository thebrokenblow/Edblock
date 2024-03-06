using System;
using System.Windows;
using System.Windows.Threading;
using EdblockViewModel.Symbols;
using EdblockViewModel.AbstractionsVM;

namespace EdblockViewModel.ComponentsVM;

public class ScalingCanvasSymbolsVM
{
    private readonly DispatcherTimer dispatcherTimer = new()
    {
        Interval = TimeSpan.FromMilliseconds(scalingInterval) 
    };

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CoordinateSymbolVM _coordinateSymbolVM;

    private SideLeave? sideLeave;
    private Action? _scrollOffset;

    private int _widthWindow;
    private int _heightWindow;
    private int _widthPanelSymbols;
    private int _heightTopSettingsPanel;

    private const int minIndentation = 40;
    private const int thicknessScroll = 14;
    private const int minNumberSymbolsScaling = 2;
    private const double scalingInterval = 50;

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
        _coordinateSymbolVM = new(canvasSymbolsVM);
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

        if (movableBlockSymbol is not null && countBlockSymbolsVM >= minNumberSymbolsScaling)
        {
            if (movableBlockSymbol is not null)
            {
                movableBlockSymbol.MoveMiddle = true;
            }

            sideLeave = GetSideLeave(cursotPoint);

            if (sideLeave is null)
            {
                return;
            }

            dispatcherTimer.Tick += ScalingCanvas;

            dispatcherTimer.Start();
        }
    }

    public void UnsubscribeСanvasScalingEvents()
    {
        sideLeave = null;

        dispatcherTimer.Tick -= ScalingCanvas;
        dispatcherTimer.Stop();
    }

    public SideLeave? GetSideLeave(Point cursotPoint)
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

        if (cursotPoint.Y <= _heightTopSettingsPanel)
        {
            return SideLeave.Top;
        }

        return null;
    }

    private double predMaxXCoordinateSymbol;
    private double predMaxYCoordinateSymbol;

    public void SetMaxCoordinate()
    {
        predMaxXCoordinateSymbol = _coordinateSymbolVM.GetMaxXCoordinateSymbol();
        predMaxYCoordinateSymbol = _coordinateSymbolVM.GetMaxYCoordinateSymbol();
    }

    public void Resize()
    {
        var maxXCoordinateSymbol = _coordinateSymbolVM.GetMaxXCoordinateSymbol();
        var maxYCoordinateSymbol = _coordinateSymbolVM.GetMaxYCoordinateSymbol();

        if (predMaxXCoordinateSymbol != maxXCoordinateSymbol)
        {
            var widthCanvas = CalculateWidthCanvas();

            if (maxXCoordinateSymbol + minIndentation > widthCanvas)
            {
                _canvasSymbolsVM.Width = (int)maxXCoordinateSymbol + minIndentation;
            }
            else
            {
                _canvasSymbolsVM.Width = widthCanvas;
            }
        }

        if (predMaxYCoordinateSymbol != maxYCoordinateSymbol)
        {
            var heightCanvas = CalculateHeightCanvas();

            if (maxYCoordinateSymbol + minIndentation > heightCanvas)
            {
                _canvasSymbolsVM.Height = (int)maxYCoordinateSymbol + minIndentation;
            }
            else
            {
                _canvasSymbolsVM.Height = heightCanvas;
            }
        }
    }

    private void ScalingCanvas(object? sender, EventArgs e)
    {
        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;

        SetSizeCanvas();
        ChangeCoordinateMovableBlockSymbol(movableBlockSymbol);
    }

    private void SetSizeCanvas()
    {
        if (sideLeave == SideLeave.Right)
        {
            _canvasSymbolsVM.Width += OFFSET_LEAVE;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            _canvasSymbolsVM.Height += OFFSET_LEAVE;
        }
        else if (
            sideLeave == SideLeave.Left
            && _canvasSymbolsVM.Width >= CalculateWidthCanvas() + OFFSET_LEAVE / 2
            && _canvasSymbolsVM.Width > _coordinateSymbolVM.GetMaxXCoordinateSymbol() + minIndentation)
        {
            _canvasSymbolsVM.Width -= OFFSET_LEAVE;
        }
        else if (
            sideLeave == SideLeave.Top
            && _canvasSymbolsVM.Height >= CalculateHeightCanvas() + OFFSET_LEAVE / 2
            && _canvasSymbolsVM.Height > _coordinateSymbolVM.GetMaxYCoordinateSymbol() + minIndentation)
        {
            _canvasSymbolsVM.Height -= OFFSET_LEAVE;
        }

        _scrollOffset?.Invoke();
    }

    private void ChangeCoordinateMovableBlockSymbol(BlockSymbolVM? movableBlockSymbol)
    {
        if (movableBlockSymbol is null)
        {
            return;
        }

        if (sideLeave == SideLeave.Right)
        {
            movableBlockSymbol.XCoordinate += OFFSET_LEAVE;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            movableBlockSymbol.YCoordinate += OFFSET_LEAVE;
        }
        else if (sideLeave == SideLeave.Left && IsSymbolLeftLeave(movableBlockSymbol))
        {
            movableBlockSymbol.XCoordinate -= OFFSET_LEAVE;
        }
        else if (sideLeave == SideLeave.Top && IsSymbolTopLeave(movableBlockSymbol))
        {
            movableBlockSymbol.YCoordinate -= OFFSET_LEAVE;
        }

        _canvasSymbolsVM.RedrawDrawnLinesSymbol(movableBlockSymbol);
    }

    private bool IsSymbolLeftLeave(BlockSymbolVM movableBlockSymbol) =>
       _widthPanelSymbols <= movableBlockSymbol.XCoordinate + movableBlockSymbol.Width / 2;

    private bool IsSymbolTopLeave(BlockSymbolVM movableBlockSymbol) =>
       _heightTopSettingsPanel <= movableBlockSymbol.YCoordinate + movableBlockSymbol.Height / 2;

    private int CalculateWidthCanvas() =>
        _widthWindow - _widthPanelSymbols - OFFSET_LEAVE / 2 - thicknessScroll;

    private int CalculateHeightCanvas() =>
        _heightWindow - _heightTopSettingsPanel - OFFSET_LEAVE - thicknessScroll;
}