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

    private readonly DispatcherTimer dispatcherTimer = new()
    {
        Interval = TimeSpan.FromMilliseconds(scalingInterval) 
    };

    private readonly CanvasSymbolsVM _canvasSymbolsVM;
    private readonly CoordinateSymbolVM _coordinateSymbolVM;

    private SideLeave? sideLeave;

    private int widthCanvas;
    private int heightCanvas;

    private int _widthPanelSymbols;
    private int _heightTopSettingsPanel;

    private const int minIndentation = 40;
    private const int minNumberSymbolsScaling = 2;
    private const double scalingInterval = 50;

    private const int offsetLeave = 40;

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
        _widthPanelSymbols = widthPanelSymbols;
        _heightTopSettingsPanel = heightTopSettingsPanel;

        if (_canvasSymbolsVM.Width < widthWindow)
        {
            widthCanvas = widthWindow - _widthPanelSymbols - offsetLeave / 2 - (int)SystemParameters.VerticalScrollBarWidth;
            _canvasSymbolsVM.Width = widthCanvas;
        }

        if (_canvasSymbolsVM.Height < heightWindow)
        {
            heightCanvas = heightWindow - _heightTopSettingsPanel - offsetLeave - (int)SystemParameters.HorizontalScrollBarHeight;
            _canvasSymbolsVM.Height = heightCanvas;
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

    public SideLeave? GetSideLeave(Point cursotPoint)
    {
        if (cursotPoint.X >= widthCanvas)
        {
            return SideLeave.Right;
        }

        if (cursotPoint.X <= _widthPanelSymbols)
        {
            return SideLeave.Left;
        }

        if (cursotPoint.Y >= heightCanvas)
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

    public event PropertyChangedEventHandler? PropertyChanged;
    public void OnPropertyChanged([CallerMemberName] string prop = "")
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
    }

    public void SetMaxCoordinate()
    {
        predMaxXCoordinateSymbol = _coordinateSymbolVM.GetMaxX();
        predMaxYCoordinateSymbol = _coordinateSymbolVM.GetMaxY();
    }

    public void Resize()
    {
        var maxXCoordinateSymbol = _coordinateSymbolVM.GetMaxX();
        var maxYCoordinateSymbol = _coordinateSymbolVM.GetMaxY();

        if (predMaxXCoordinateSymbol != maxXCoordinateSymbol)
        {
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
        SetSizeCanvas();
        ChangeCoordinateMovableBlockSymbol();


        if (sideLeave == SideLeave.Right)
        {
           HorizontalOffset += offsetLeave;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            VerticalOffset += offsetLeave;
        }
        else if (sideLeave == SideLeave.Left)
        {
            HorizontalOffset -= offsetLeave;
        }
        else if (sideLeave == SideLeave.Top)
        {
            VerticalOffset -= offsetLeave;
        }
    }

    private void SetSizeCanvas()
    {
        if (sideLeave == SideLeave.Right)
        {
            _canvasSymbolsVM.Width += offsetLeave;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            _canvasSymbolsVM.Height += offsetLeave;
        }
        else if (
            sideLeave == SideLeave.Left
            && _canvasSymbolsVM.Width >= widthCanvas + offsetLeave / 2
            && _canvasSymbolsVM.Width > _coordinateSymbolVM.GetMaxX() + minIndentation)
        {
            _canvasSymbolsVM.Width -= offsetLeave;
        }
        else if (
            sideLeave == SideLeave.Top
            && _canvasSymbolsVM.Height >= heightCanvas + offsetLeave / 2
            && _canvasSymbolsVM.Height > _coordinateSymbolVM.GetMaxY() + minIndentation)
        {
            _canvasSymbolsVM.Height -= offsetLeave;
        }
    }

    private void ChangeCoordinateMovableBlockSymbol()
    {
        var movableBlockSymbol = _canvasSymbolsVM.MovableBlockSymbol;

        if (movableBlockSymbol is null)
        {
            return;
        }

        if (sideLeave == SideLeave.Right)
        {
            movableBlockSymbol.XCoordinate += offsetLeave;
        }
        else if (sideLeave == SideLeave.Bottom)
        {
            movableBlockSymbol.YCoordinate += offsetLeave;
        }
        else if (sideLeave == SideLeave.Left && IsSymbolLeftLeave(movableBlockSymbol))
        {
            movableBlockSymbol.XCoordinate -= offsetLeave;
        }
        else if (sideLeave == SideLeave.Top && IsSymbolTopLeave(movableBlockSymbol))
        {
            movableBlockSymbol.YCoordinate -= offsetLeave;
        }

        _canvasSymbolsVM.RedrawDrawnLinesSymbol(movableBlockSymbol);
    }

    private bool IsSymbolLeftLeave(BlockSymbolVM movableBlockSymbol) =>
       _widthPanelSymbols <= movableBlockSymbol.XCoordinate + movableBlockSymbol.Width / 2;

    private bool IsSymbolTopLeave(BlockSymbolVM movableBlockSymbol) =>
       _heightTopSettingsPanel <= movableBlockSymbol.YCoordinate + movableBlockSymbol.Height / 2;
}