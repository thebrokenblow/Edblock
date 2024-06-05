using System;
using System.Windows;
using System.Windows.Threading;
using EdblockViewModel.Symbols;
using EdblockViewModel.Core;
using EdblockViewModel.Components.CanvasSymbols.Interfaces;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockViewModel.Components.CanvasSymbols;

public class ScalingCanvasSymbolsVM(ICanvasSymbolsVM canvasSymbolsVM) : ObservableObject
{
    private double verticalOffset = offsetLeave;
    public double VerticalOffset
    {
        get => verticalOffset;
        set
        {
            verticalOffset = value;
            OnPropertyChanged();
        }
    }

    private double horizontalOffset = offsetLeave;
    public double HorizontalOffset
    {
        get => horizontalOffset;
        set
        {
            horizontalOffset = value;
            OnPropertyChanged();
        }
    }

    public int HeightTopSettingsPanel { get; set; }
    public int WidthPanelSymbols { get; set; }

    private readonly DispatcherTimer dispatcherTimer = new()
    {
        Interval = TimeSpan.FromMilliseconds(scalingInterval)
    };

    private readonly CoordinateSymbolVM coordinateSymbolVM = new(canvasSymbolsVM);

    private SideLeave? sideLeave;

    private int widthUnscaledCanvas;
    private int heightUnscaledCanvas;

    private const int minIndentation = 40;
    private const int minNumberSymbolsScaling = 2;
    private const double scalingInterval = 50;

    private const int offsetLeave = 40;

    private enum SideLeave
    {
        Top,
        Right,
        Bottom,
        Left
    }

    public void CalculateSizeUnscaledCanvas(int widthWindow, int heightWindow)
    {
        if (canvasSymbolsVM.Width < widthWindow)
        {
            widthUnscaledCanvas = CalculateWidthUnscaledCanvas(widthWindow);
            canvasSymbolsVM.Width = widthUnscaledCanvas;
        }

        if (canvasSymbolsVM.Height < heightWindow)
        {
            heightUnscaledCanvas = CalculateHeightUnscaledCanvas(heightWindow);
            canvasSymbolsVM.Height = heightUnscaledCanvas;
        }
    }

    public void SubscribeСanvasScalingEvents(Point cursotPoint)
    {
        var movableBlockSymbol = canvasSymbolsVM.ListCanvasSymbolsVM.MovableBlockSymbol;
        var countBlockSymbolsVM = canvasSymbolsVM.ListCanvasSymbolsVM.BlockSymbolsVM.Count;

        if (movableBlockSymbol is not null && countBlockSymbolsVM >= minNumberSymbolsScaling)
        {
            movableBlockSymbol.MoveMiddle = true;

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

    private SideLeave? GetSideLeave(Point cursotPoint)
    {
        if (cursotPoint.X >= widthUnscaledCanvas)
        {
            return SideLeave.Right;
        }

        if (cursotPoint.X <= WidthPanelSymbols)
        {
            return SideLeave.Left;
        }

        if (cursotPoint.Y >= heightUnscaledCanvas)
        {
            return SideLeave.Bottom;
        }

        if (cursotPoint.Y <= HeightTopSettingsPanel)
        {
            return SideLeave.Top;
        }

        return null;
    }

    public void Resize()
    {
        var maxXCoordinateSymbol = coordinateSymbolVM.GetMaxX() + minIndentation;
        var maxYCoordinateSymbol = coordinateSymbolVM.GetMaxY() + minIndentation;

        canvasSymbolsVM.Width = Math.Max(maxXCoordinateSymbol, widthUnscaledCanvas);
        canvasSymbolsVM.Height = Math.Max(maxYCoordinateSymbol, heightUnscaledCanvas);
    }

    private void ScalingCanvas(object? sender, EventArgs e)
    {
        var movableBlockSymbol = canvasSymbolsVM.ListCanvasSymbolsVM.MovableBlockSymbol;

        if (movableBlockSymbol is null)
        {
            return;
        }

        if (sideLeave == SideLeave.Right)
        {
            HorizontalOffset += offsetLeave;
            canvasSymbolsVM.Width += offsetLeave;

            movableBlockSymbol.XCoordinate += offsetLeave;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            VerticalOffset += offsetLeave;
            canvasSymbolsVM.Height += offsetLeave;

            movableBlockSymbol.YCoordinate += offsetLeave;
        }
        else if (
            sideLeave == SideLeave.Left
            && canvasSymbolsVM.Width > widthUnscaledCanvas
            && canvasSymbolsVM.Width > coordinateSymbolVM.GetMaxX() + minIndentation)
        {
            HorizontalOffset -= offsetLeave;
            canvasSymbolsVM.Width -= offsetLeave;

            if (IsSymbolLeftLeave(movableBlockSymbol))
            {
                movableBlockSymbol.XCoordinate -= offsetLeave;
            }
        }
        else if (
            sideLeave == SideLeave.Top
            && canvasSymbolsVM.Height > heightUnscaledCanvas
            && canvasSymbolsVM.Height > coordinateSymbolVM.GetMaxY() + minIndentation)
        {
            VerticalOffset -= offsetLeave;
            canvasSymbolsVM.Height -= offsetLeave;

            if (IsSymbolTopLeave(movableBlockSymbol))
            {
                movableBlockSymbol.YCoordinate -= offsetLeave;
            }
        }
    }

    private bool IsSymbolLeftLeave(BlockSymbolVM movableBlockSymbol) =>
       WidthPanelSymbols <= movableBlockSymbol.XCoordinate + movableBlockSymbol.Width / 2;

    private bool IsSymbolTopLeave(BlockSymbolVM movableBlockSymbol) =>
       HeightTopSettingsPanel <= movableBlockSymbol.YCoordinate + movableBlockSymbol.Height / 2;

    private int CalculateWidthUnscaledCanvas(int widthWindow) =>
     widthWindow - WidthPanelSymbols - (int)SystemParameters.VerticalScrollBarWidth;

    private int CalculateHeightUnscaledCanvas(int heightWindow) =>
         heightWindow - HeightTopSettingsPanel - (int)SystemParameters.HorizontalScrollBarHeight;
}