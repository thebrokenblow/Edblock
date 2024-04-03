using System;
using System.Windows;
using System.Windows.Threading;
using EdblockViewModel.Symbols;
using EdblockViewModel.AbstractionsVM;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace EdblockViewModel.ComponentsVM;

public class ScalingCanvasSymbolsVM : INotifyPropertyChanged
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

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CoordinateSymbolVM _coordinateSymbolVM;

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

    public ScalingCanvasSymbolsVM(CanvasSymbolsVM canvasSymbolsVM)
    {
        _canvasSymbolsVM = canvasSymbolsVM;
        _coordinateSymbolVM = new(canvasSymbolsVM);
    }

    public void CalculateSizeUnscaledCanvas(int widthWindow, int heightWindow)
    {
        if (_canvasSymbolsVM.Width < widthWindow)
        {
            widthUnscaledCanvas = CalculateWidthUnscaledCanvas(widthWindow);
            _canvasSymbolsVM.Width = widthUnscaledCanvas;
        }

        if (_canvasSymbolsVM.Height < heightWindow)
        {
            heightUnscaledCanvas = CalculateHeightUnscaledCanvas(heightWindow);
            _canvasSymbolsVM.Height = heightUnscaledCanvas;
        }
    }

    public void SubscribeСanvasScalingEvents(Point cursotPoint)
    {
        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;
        var countBlockSymbolsVM = _canvasSymbolsVM.BlockSymbolsVM.Count;

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

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public void Resize()
    {
        var maxXCoordinateSymbol = _coordinateSymbolVM.GetMaxX() + minIndentation;
        var maxYCoordinateSymbol = _coordinateSymbolVM.GetMaxY() + minIndentation;

        _canvasSymbolsVM.Width = Math.Max(maxXCoordinateSymbol, widthUnscaledCanvas);
        _canvasSymbolsVM.Height = Math.Max(maxYCoordinateSymbol, heightUnscaledCanvas);
    }

    private void ScalingCanvas(object? sender, EventArgs e)
    {
        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;

        if (movableBlockSymbol is null)
        {
            return;
        }

        if (sideLeave == SideLeave.Right)
        {
            HorizontalOffset += offsetLeave;
            _canvasSymbolsVM.Width += offsetLeave;

            movableBlockSymbol.XCoordinate += offsetLeave;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            VerticalOffset += offsetLeave;
            _canvasSymbolsVM.Height += offsetLeave;

            movableBlockSymbol.YCoordinate += offsetLeave;
        }
        else if (
            sideLeave == SideLeave.Left
            && _canvasSymbolsVM.Width > widthUnscaledCanvas
            && _canvasSymbolsVM.Width > _coordinateSymbolVM.GetMaxX() + minIndentation)
        {
            HorizontalOffset -= offsetLeave;
            _canvasSymbolsVM.Width -= offsetLeave;

            if (IsSymbolLeftLeave(movableBlockSymbol))
            {
                movableBlockSymbol.XCoordinate -= offsetLeave;
            }
        }
        else if (
            sideLeave == SideLeave.Top
            && _canvasSymbolsVM.Height > heightUnscaledCanvas
            && _canvasSymbolsVM.Height > _coordinateSymbolVM.GetMaxY() + minIndentation)
        {
            VerticalOffset -= offsetLeave;
            _canvasSymbolsVM.Height -= offsetLeave;

            if (IsSymbolTopLeave(movableBlockSymbol))
            {
                movableBlockSymbol.YCoordinate -= offsetLeave;
            }
        }

        _canvasSymbolsVM.RedrawDrawnLinesSymbol(movableBlockSymbol);
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