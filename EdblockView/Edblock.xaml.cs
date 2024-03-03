using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using EdblockViewModel;
using EdblockView.Abstraction;
using EdblockViewModel.AbstractionsVM;
using EdblockViewModel.Symbols.LineSymbols;

namespace EdblockView;

/// <summary>
/// Логика взаимодействия для Edblock.xaml
/// </summary>
public partial class Edblock : Window
{
    private readonly EdblockVM edblockVM = new();

    private readonly DispatcherTimer dispatcherTimer;

    private BlockSymbolVM? movableBlockSymbol;
    private DrawnLineSymbolVM? currentDrawnLineSymbol;

    private double widthWindow;
    private double heightWindow;

    private const int offsetLeaveCanvas = 40;
    private const int minIndentation = 40;
    private const int heightScroll = 14;
    
    //TODO: устранить дублирование кода в этом классе
    public Edblock()
    {
        InitializeComponent();

        DataContext = edblockVM;

        dispatcherTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(0.05)
        };

        SizeChanged += Edblock_SizeChanged;
    }

    private void AddSymbolView(object sender, MouseButtonEventArgs e)
    {
        if (sender is IFactorySymbolVM factorySymbolVM)
        {
            try
            {
                var blockSymbolVM = factorySymbolVM.CreateBlockSymbolVM(edblockVM);

                var firstBlockSymbolsVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM.FirstOrDefault();
                var isScaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM.IsScaleAllSymbolVM;

                if (isScaleAllSymbolVM && firstBlockSymbolsVM is BlockSymbolVM firstBlockSymbolVM)
                {
                    blockSymbolVM.SetWidth(firstBlockSymbolVM.Width);
                    blockSymbolVM.SetHeight(firstBlockSymbolVM.Height);
                }

                edblockVM.AddBlockSymbol(blockSymbolVM);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }

    private void Edblock_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        if (sender is Edblock edblock)
        {
            widthWindow = edblock.ActualWidth;
            heightWindow = edblock.ActualHeight;

            if (CanvasSymbolsView.Width < widthWindow)
            {
                CanvasSymbolsView.Width = widthWindow;
            }

            if (CanvasSymbolsView.Height < heightWindow)
            {
                CanvasSymbolsView.Height = heightWindow;
            }
        }
    }

    private void CanvasSymbols_MouseLeave(object sender, MouseEventArgs e)
    {
        var canvasSymbolsVM = edblockVM.CanvasSymbolsVM;
        var blockSymbolsVM = canvasSymbolsVM.BlockSymbolsVM;

        movableBlockSymbol = canvasSymbolsVM.MovableBlockSymbol;
        currentDrawnLineSymbol = canvasSymbolsVM.CurrentDrawnLineSymbol;

        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.FirstMove = true;
        }

        var positionCursor = e.GetPosition(this);

        if (positionCursor.X >= widthWindow - PanelSymbols.ActualWidth)
        {
            dispatcherTimer.Tick += IncreaseSizeHorizontal;
        }

        if (positionCursor.X <= PanelSymbols.ActualWidth)
        {
            dispatcherTimer.Tick += DecreaseSizeHorizontal;
        }

        if (positionCursor.Y >= heightWindow - TopSettingsPanelUI.ActualHeight)
        {
            dispatcherTimer.Tick += IncreaseSizeVertical;
        }

        if (positionCursor.Y <= TopSettingsPanelUI.ActualHeight)
        {
            dispatcherTimer.Tick += DecreaseSizeVertical;
        }

        dispatcherTimer.Start();
    }

    private double GetMaxXCoordinateSymbol()
    {
        var blockSymbolsVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM;
        var drawnLinesSymbolVM = edblockVM.CanvasSymbolsVM.DrawnLinesSymbolVM;

        var maxXCoordinate = blockSymbolsVM.Max(vm => vm.XCoordinate + vm.Width);

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
        var blockSymbolsVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM;
        var drawnLinesSymbolVM = edblockVM.CanvasSymbolsVM.DrawnLinesSymbolVM;

        var maxYCoordinate = blockSymbolsVM.Max(b => b.YCoordinate + b.Height);

        foreach (var drawnLinesSymbol in drawnLinesSymbolVM)
        {
            foreach (var linesSymbol in drawnLinesSymbol.LinesSymbolVM)
            {
                maxYCoordinate = Math.Max(maxYCoordinate, Math.Max(linesSymbol.Y1, linesSymbol.Y2));
            }
        }

        return maxYCoordinate;
    }

    private void CanvasSymbols_MouseEnter(object sender, MouseEventArgs e)
    {
        movableBlockSymbol = null;

        dispatcherTimer.Tick -= IncreaseSizeVertical;
        dispatcherTimer.Tick -= DecreaseSizeVertical;

        dispatcherTimer.Tick -= IncreaseSizeHorizontal;
        dispatcherTimer.Tick -= DecreaseSizeHorizontal;

        dispatcherTimer.Stop();
    }

    private void IncreaseSizeVertical(object? sender, EventArgs e)
    {
        CanvasSymbolsView.Height = CanvasSymbolsView.ActualHeight + offsetLeaveCanvas;

        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.YCoordinate += offsetLeaveCanvas;
        }
        else if (currentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.Y2 += offsetLeaveCanvas;

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var verticalOffsetScrollViewer = scrollViewer.ContentVerticalOffset + offsetLeaveCanvas;
        scrollViewer.ScrollToVerticalOffset(verticalOffsetScrollViewer);
    }

    private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    {
        CanvasSymbolsView.Width = CanvasSymbolsView.ActualWidth + offsetLeaveCanvas;

        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.XCoordinate += offsetLeaveCanvas;
        }
        else if (currentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.X2 += offsetLeaveCanvas;

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var horizontalOffsetScrollViewer = scrollViewer.ContentHorizontalOffset + offsetLeaveCanvas;
        scrollViewer.ScrollToHorizontalOffset(horizontalOffsetScrollViewer);
    }

    private void DecreaseSizeHorizontal(object? sender, EventArgs e)
    {
        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        var widthCanvas = (int)(widthWindow - PanelSymbols.ActualWidth);
        var actualWidthCanvas = (int)CanvasSymbolsView.ActualWidth;

        if (actualWidthCanvas < widthCanvas)
        {
            return;
        }

        if (movableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = movableBlockSymbol.XCoordinate + movableBlockSymbol.Width / 2;

            if (PanelSymbols.ActualWidth <= extremeCoordinateSymbolBehind)
            {
                movableBlockSymbol.XCoordinate -= offsetLeaveCanvas;
            }
        }

        if (currentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.X2 -= offsetLeaveCanvas;

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var maxXCoordinate = GetMaxXCoordinateSymbol();

        if (CanvasSymbolsView.ActualWidth > maxXCoordinate + minIndentation)
        {
            CanvasSymbolsView.Width -= offsetLeaveCanvas;
        }

        var horizontalOffsetScrollViewer = scrollViewer.ContentHorizontalOffset - offsetLeaveCanvas;
        scrollViewer.ScrollToHorizontalOffset(horizontalOffsetScrollViewer);
    }

    private void DecreaseSizeVertical(object? sender, EventArgs e)
    {
        if (movableBlockSymbol is null && currentDrawnLineSymbol is null)
        {
            return;
        }

        var heightCanvas = (int)(heightWindow - TopSettingsPanelUI.ActualHeight);
        var actualHeightCanvas = (int)CanvasSymbolsView.ActualHeight + heightScroll;

        if (actualHeightCanvas < heightCanvas)
        {
            return;
        }

        if (movableBlockSymbol is not null)
        {
            var extremeCoordinateSymbolBehind = movableBlockSymbol.YCoordinate + movableBlockSymbol.Height / 2;

            if (TopSettingsPanelUI.ActualHeight <= extremeCoordinateSymbolBehind)
            {
                movableBlockSymbol.YCoordinate -= offsetLeaveCanvas;
            }
        }

        if (currentDrawnLineSymbol is not null)
        {
            var lastLineSymbolVM = currentDrawnLineSymbol.LinesSymbolVM[^1];
            lastLineSymbolVM.Y2 -= offsetLeaveCanvas;

            currentDrawnLineSymbol.ArrowSymbol.ChangePosition((lastLineSymbolVM.X2, lastLineSymbolVM.Y2));
        }

        var maxYCoordinate = GetMaxYCoordinateSymbol();

        if (CanvasSymbolsView.ActualHeight > maxYCoordinate + minIndentation)
        {
            CanvasSymbolsView.Height -= offsetLeaveCanvas;
        }

        var verticalOffsetScrollViewer = scrollViewer.ContentVerticalOffset - offsetLeaveCanvas;
        scrollViewer.ScrollToVerticalOffset(verticalOffsetScrollViewer);
    }
}