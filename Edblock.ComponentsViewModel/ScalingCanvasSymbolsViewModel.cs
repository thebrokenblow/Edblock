using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Edblock.SymbolsViewModel.Core;

namespace Edblock.ComponentsViewModel;

public class ScalingCanvasSymbolsViewModel(CanvasSymbolsViewModel canvasSymbolsViewModel) : INotifyPropertyChanged
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

    private readonly CanvasSymbolsViewModel canvasSymbolsViewModel = canvasSymbolsViewModel;
    private readonly CoordinateSymbolViewModel coordinateSymbolVM = new(canvasSymbolsViewModel);

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
        if (canvasSymbolsViewModel.Width < widthWindow)
        {
            widthUnscaledCanvas = CalculateWidthUnscaledCanvas(widthWindow);
            canvasSymbolsViewModel.Width = widthUnscaledCanvas;
        }

        if (canvasSymbolsViewModel.Height < heightWindow)
        {
            heightUnscaledCanvas = CalculateHeightUnscaledCanvas(heightWindow);
            canvasSymbolsViewModel.Height = heightUnscaledCanvas;
        }
    }

    public void SubscribeСanvasScalingEvents(Point cursotPoint)
    {
        var movableBlockSymbol = canvasSymbolsViewModel.MovableBlockSymbol;
        var countBlockSymbolsVM = canvasSymbolsViewModel.BlockSymbolsVM.Count;

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
        var maxXCoordinateSymbol = coordinateSymbolVM.GetMaxX() + minIndentation;
        var maxYCoordinateSymbol = coordinateSymbolVM.GetMaxY() + minIndentation;

        canvasSymbolsViewModel.Width = Math.Max(maxXCoordinateSymbol, widthUnscaledCanvas);
        canvasSymbolsViewModel.Height = Math.Max(maxYCoordinateSymbol, heightUnscaledCanvas);
    }

    private void ScalingCanvas(object? sender, EventArgs e)
    {
        var movableBlockSymbol = canvasSymbolsViewModel.MovableBlockSymbol;

        if (movableBlockSymbol is null)
        {
            return;
        }

        if (sideLeave == SideLeave.Right)
        {
            HorizontalOffset += offsetLeave;
            canvasSymbolsViewModel.Width += offsetLeave;

            movableBlockSymbol.XCoordinate += offsetLeave;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            VerticalOffset += offsetLeave;
            canvasSymbolsViewModel.Height += offsetLeave;

            movableBlockSymbol.YCoordinate += offsetLeave;
        }
        else if (
            sideLeave == SideLeave.Left
            && canvasSymbolsViewModel.Width > widthUnscaledCanvas
            && canvasSymbolsViewModel.Width > coordinateSymbolVM.GetMaxX() + minIndentation)
        {
            HorizontalOffset -= offsetLeave;
            canvasSymbolsViewModel.Width -= offsetLeave;

            if (IsSymbolLeftLeave(movableBlockSymbol))
            {
                movableBlockSymbol.XCoordinate -= offsetLeave;
            }
        }
        else if (
            sideLeave == SideLeave.Top
            && canvasSymbolsViewModel.Height > heightUnscaledCanvas
            && canvasSymbolsViewModel.Height > coordinateSymbolVM.GetMaxY() + minIndentation)
        {
            VerticalOffset -= offsetLeave;
            canvasSymbolsViewModel.Height -= offsetLeave;

            if (IsSymbolTopLeave(movableBlockSymbol))
            {
                movableBlockSymbol.YCoordinate -= offsetLeave;
            }
        }

        canvasSymbolsViewModel.RedrawDrawnLinesSymbol(movableBlockSymbol);
    }

    private bool IsSymbolLeftLeave(BlockSymbolViewModel movableBlockSymbol) =>
       WidthPanelSymbols <= movableBlockSymbol.XCoordinate + movableBlockSymbol.Width / 2;

    private bool IsSymbolTopLeave(BlockSymbolViewModel movableBlockSymbol) =>
       HeightTopSettingsPanel <= movableBlockSymbol.YCoordinate + movableBlockSymbol.Height / 2;

    private int CalculateWidthUnscaledCanvas(int widthWindow) =>
     widthWindow - WidthPanelSymbols - (int)SystemParameters.VerticalScrollBarWidth;

    private int CalculateHeightUnscaledCanvas(int heightWindow) =>
         heightWindow - HeightTopSettingsPanel - (int)SystemParameters.HorizontalScrollBarHeight;
}