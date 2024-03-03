using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;
using EdblockModel.EnumsModel;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbols : UserControl
{
    public static Canvas? Canvas { get; set; }
    private CanvasSymbolsVM? canvasSymbolsVM;

    private readonly DispatcherTimer dispatcherTimer;
    public CanvasSymbols()
    {
        InitializeComponent();

        dispatcherTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(0.05)
        };
    }

    private void SelectBlockSymbol(object sender, MouseButtonEventArgs e)
    {
        var blockSymbolView = (UserControl)sender;
        var blockSymbolVM = (BlockSymbolVM)blockSymbolView.DataContext;
        blockSymbolVM.Select();

        e.Handled = true;
    }

    private void LoadedCanvas(object sender, RoutedEventArgs e)
    {
        Canvas = (Canvas)sender;
        canvasSymbolsVM = (CanvasSymbolsVM)Canvas.DataContext;
    }

    private MouseEventArgs? ev;

    private void LeaveSymbol(object sender, MouseEventArgs e)
    {
        if (canvasSymbolsVM is not null && (canvasSymbolsVM.MovableBlockSymbol is not null || canvasSymbolsVM.CurrentDrawnLineSymbol is not null))
        {
            ev ??= e;
            var positionCursor = e.GetPosition(this);
            canvasSymbolsVM.SubscribeScalingMethod(positionCursor);

            dispatcherTimer.Tick += ScrollScrollViewer;
            dispatcherTimer.Start();
        }
    }

    private void ScrollScrollViewer(object? sender, EventArgs e)
    {
        if (canvasSymbolsVM is not null)
        {
            var positionCursor = ev.GetPosition(this);

            var sideLeave = canvasSymbolsVM.GetSideLeave(positionCursor);

            if (sideLeave == CanvasSymbolsVM.SideLeave.Top)
            {
                var verticalOffsetScrollViewer = scrollViewer.ContentVerticalOffset - 40;
                scrollViewer.ScrollToVerticalOffset(verticalOffsetScrollViewer);
            }

            if (sideLeave == CanvasSymbolsVM.SideLeave.Bottom)
            {
                var verticalOffsetScrollViewer = scrollViewer.ContentVerticalOffset + 40;
                scrollViewer.ScrollToVerticalOffset(verticalOffsetScrollViewer);
            }

            if (sideLeave == CanvasSymbolsVM.SideLeave.Left)
            {
                var verticalOffsetScrollViewer = scrollViewer.ContentHorizontalOffset - 40;
                scrollViewer.ScrollToHorizontalOffset(verticalOffsetScrollViewer);
            }

            if (sideLeave == CanvasSymbolsVM.SideLeave.Right)
            {
                var verticalOffsetScrollViewer = scrollViewer.ContentHorizontalOffset + 40;
                scrollViewer.ScrollToHorizontalOffset(verticalOffsetScrollViewer);
            }

        }
    }

    private void EnterSymbol(object sender, MouseEventArgs e)
    {
        canvasSymbolsVM?.UnsubscribeScalingMethod();
        dispatcherTimer.Stop();
        dispatcherTimer.Tick -= ScrollScrollViewer;
    }
}