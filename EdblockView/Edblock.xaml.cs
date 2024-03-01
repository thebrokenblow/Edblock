using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using EdblockViewModel;
using EdblockView.Abstraction;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView;

/// <summary>
/// Логика взаимодействия для Edblock.xaml
/// </summary>
public partial class Edblock : Window
{
    private readonly EdblockVM edblockVM = new();

    private readonly DispatcherTimer dispatcherTimer;

    private MouseEventArgs? eventArgs;
    private BlockSymbolVM? movableBlockSymbol;

    private double widthWindow;
    private double heightWindow;

    private const int offsetLeaveCanvas = 40;
    private const int heightScroll = 14;

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
        var blockSymbolsVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM;
        movableBlockSymbol = edblockVM.CanvasSymbolsVM.MovableBlockSymbol;

        if (movableBlockSymbol is null || blockSymbolsVM.Count < 2)
        {
            return;
        }

        var positionCursor = e.GetPosition(this);

        movableBlockSymbol.FirstMove = true;
        eventArgs ??= e;

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

    private void ChangeWidthSizeCanvas()
    {
        var firstBlockSymbolVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM.First();

        var maxXCoordinate = firstBlockSymbolVM.XCoordinate;

        for (int i = 1; i < edblockVM.CanvasSymbolsVM.BlockSymbolsVM.Count; i++)
        {
            if (edblockVM.CanvasSymbolsVM.BlockSymbolsVM[i].XCoordinate > maxXCoordinate)
            {
                maxXCoordinate = edblockVM.CanvasSymbolsVM.BlockSymbolsVM[i].XCoordinate;
            }
        }

        if (CanvasSymbolsView.ActualWidth > maxXCoordinate)
        {
            CanvasSymbolsView.Width -= offsetLeaveCanvas;
        }
    }

    private void ChangeHeightSizeCanvas()
    {
        var firstBlockSymbolVM = edblockVM.CanvasSymbolsVM.BlockSymbolsVM.First();

        var maxYCoordinate = firstBlockSymbolVM.YCoordinate;

        for (int i = 1; i < edblockVM.CanvasSymbolsVM.BlockSymbolsVM.Count; i++)
        {
            if (edblockVM.CanvasSymbolsVM.BlockSymbolsVM[i].YCoordinate > maxYCoordinate)
            {
                maxYCoordinate = edblockVM.CanvasSymbolsVM.BlockSymbolsVM[i].YCoordinate;
            }
        }

        if (CanvasSymbolsView.ActualHeight > maxYCoordinate)
        {
            CanvasSymbolsView.Height -= offsetLeaveCanvas;
        }
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

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.YCoordinate += offsetLeaveCanvas;

            var verticalOffsetScrollViewer = scrollViewer.ContentVerticalOffset + offsetLeaveCanvas;
            scrollViewer.ScrollToVerticalOffset(verticalOffsetScrollViewer);
        }
    }

    private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    {
        CanvasSymbolsView.Width = CanvasSymbolsView.ActualWidth + offsetLeaveCanvas;

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.XCoordinate += offsetLeaveCanvas;

            var horizontalOffsetScrollViewer = scrollViewer.ContentHorizontalOffset + offsetLeaveCanvas;
            scrollViewer.ScrollToHorizontalOffset(horizontalOffsetScrollViewer);
        }
    }

    private void DecreaseSizeHorizontal(object? sender, EventArgs e)
    {
        if (movableBlockSymbol is null || eventArgs is null)
        {
            return;
        }

        var widthCanvas = (int)(widthWindow - PanelSymbols.ActualWidth);
        var actualWidthCanvas = (int)CanvasSymbolsView.ActualWidth;

        if (actualWidthCanvas < widthCanvas)
        {
            return;
        }

        var positionCursor = eventArgs.GetPosition(this);

        if (widthCanvas > positionCursor.X)
        {
            movableBlockSymbol.XCoordinate -= offsetLeaveCanvas;
        }

        var horizontalOffsetScrollViewer = scrollViewer.ContentHorizontalOffset - offsetLeaveCanvas;
        scrollViewer.ScrollToHorizontalOffset(horizontalOffsetScrollViewer);

        ChangeWidthSizeCanvas();
    }

    private void DecreaseSizeVertical(object? sender, EventArgs e)
    {
        if (movableBlockSymbol is null || eventArgs is null)
        {
            return;
        }

        var heightCanvas = (int)(heightWindow - TopSettingsPanelUI.ActualHeight);
        var actualHeightCanvas = (int)CanvasSymbolsView.ActualHeight + heightScroll;

        if (actualHeightCanvas < heightCanvas)
        {
            return;
        }

        var positionCursor = eventArgs.GetPosition(this);

        if (heightCanvas > positionCursor.Y)
        {
            movableBlockSymbol.YCoordinate -= offsetLeaveCanvas;
        }

        var verticalOffsetScrollViewer = scrollViewer.ContentVerticalOffset - offsetLeaveCanvas;
        scrollViewer.ScrollToVerticalOffset(verticalOffsetScrollViewer);

        ChangeHeightSizeCanvas();
    }
}