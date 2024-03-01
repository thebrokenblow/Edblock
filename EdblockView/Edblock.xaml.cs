using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using EdblockViewModel;
using EdblockView.Abstraction;
using EdblockViewModel.AbstractionsVM;
using System.Windows.Threading;

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

                var isScaleAllSymbolVM = edblockVM.PopupBoxMenuVM.ScaleAllSymbolVM.IsScaleAllSymbolVM;

                if (isScaleAllSymbolVM && edblockVM.CanvasSymbolsVM.BlockSymbolVM.FirstOrDefault() is BlockSymbolVM firstBlockSymbolVM)
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
        var t1 = scrollViewer.ContentHorizontalOffset;
        var t2 = scrollViewer.ContentVerticalOffset;

        var t3 = scrollViewer.ExtentHeight;
        var t4 = scrollViewer.ExtentWidth;
        var t5 = scrollViewer.HorizontalOffset;
        var t6 = scrollViewer.PanningDeceleration;

        var t7 = scrollViewer.PanningRatio;
        var t8 = scrollViewer.ScrollableHeight;

        var t9 = scrollViewer.ScrollableWidth;
        var t10 = scrollViewer.VerticalOffset;
        var t11 = scrollViewer.ViewportWidth;

        if (edblockVM.CanvasSymbolsVM.MovableBlockSymbol is null)
        {
            return;
        }

        var positionCursor = e.GetPosition(this);

        movableBlockSymbol = edblockVM.CanvasSymbolsVM.MovableBlockSymbol;
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
        var firstBlockSymbolVM = edblockVM.CanvasSymbolsVM.BlockSymbolVM.First();

        var maxXCoordinate = firstBlockSymbolVM.XCoordinate;

        for (int i = 1; i < edblockVM.CanvasSymbolsVM.BlockSymbolVM.Count; i++)
        {
            if (edblockVM.CanvasSymbolsVM.BlockSymbolVM[i].XCoordinate > maxXCoordinate)
            {
                maxXCoordinate = edblockVM.CanvasSymbolsVM.BlockSymbolVM[i].XCoordinate;
            }
        }

        if (CanvasSymbolsView.ActualWidth > maxXCoordinate)
        {
            CanvasSymbolsView.Width -= offsetLeaveCanvas;
        }
    }

    private void ChangeHeightSizeCanvas()
    {
        var firstBlockSymbolVM = edblockVM.CanvasSymbolsVM.BlockSymbolVM.First();

        var maxYCoordinate = firstBlockSymbolVM.YCoordinate;

        for (int i = 1; i < edblockVM.CanvasSymbolsVM.BlockSymbolVM.Count; i++)
        {
            if (edblockVM.CanvasSymbolsVM.BlockSymbolVM[i].YCoordinate > maxYCoordinate)
            {
                maxYCoordinate = edblockVM.CanvasSymbolsVM.BlockSymbolVM[i].YCoordinate;
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

            var contentVerticalOffset = scrollViewer.ContentVerticalOffset;
            scrollViewer.ScrollToVerticalOffset(contentVerticalOffset + offsetLeaveCanvas);
        }
    }

    private void DecreaseSizeVertical(object? sender, EventArgs e)
    {
        if (movableBlockSymbol is not null && (int)CanvasSymbolsView.ActualHeight + heightScroll >= (int)(heightWindow - TopSettingsPanelUI.ActualHeight))
        {
            var cursorYCoordinate = eventArgs.GetPosition(this).Y;

            if (heightWindow - TopSettingsPanelUI.ActualHeight > cursorYCoordinate)
            {
                movableBlockSymbol.YCoordinate -= offsetLeaveCanvas;
            }

            var contentVerticalOffset = scrollViewer.ContentVerticalOffset;
            scrollViewer.ScrollToVerticalOffset(contentVerticalOffset - offsetLeaveCanvas);

            ChangeHeightSizeCanvas();
        }
    }

    private void IncreaseSizeHorizontal(object? sender, EventArgs e)
    {
        CanvasSymbolsView.Width = CanvasSymbolsView.ActualWidth + offsetLeaveCanvas;

        if (movableBlockSymbol is not null)
        {
            movableBlockSymbol.XCoordinate += offsetLeaveCanvas;

            var contentHorizontalOffset = scrollViewer.ContentHorizontalOffset;
            scrollViewer.ScrollToHorizontalOffset(contentHorizontalOffset + offsetLeaveCanvas);
        }
    }

    private void DecreaseSizeHorizontal(object? sender, EventArgs e)
    {
        if (movableBlockSymbol is not null && (int)CanvasSymbolsView.ActualWidth >= (int)(widthWindow - PanelSymbols.ActualWidth))
        {
            var cursorXCoordinate = eventArgs.GetPosition(this).X;

            if (widthWindow - PanelSymbols.ActualWidth > cursorXCoordinate)
            {
                movableBlockSymbol.XCoordinate -= offsetLeaveCanvas;
            }

            var contentHorizontalOffset = scrollViewer.ContentHorizontalOffset;
            scrollViewer.ScrollToHorizontalOffset(contentHorizontalOffset - offsetLeaveCanvas);

            ChangeWidthSizeCanvas();
        }
    }
}