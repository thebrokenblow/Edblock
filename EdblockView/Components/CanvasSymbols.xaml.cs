using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.ComponentsVM;
using EdblockViewModel.AbstractionsVM;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbols : UserControl
{
    public static Canvas? Canvas { get; set; }

    public CanvasSymbols()
    {
        InitializeComponent();
    }

    private void LoadedCanvas(object sender, RoutedEventArgs e)
    {
        Canvas = (Canvas)sender;
    }

    private void SelectBlockSymbol(object sender, MouseButtonEventArgs e)
    {
        var blockSymbolView = (UserControl)sender;
        var blockSymbolVM = (BlockSymbolVM)blockSymbolView.DataContext;
        blockSymbolVM.Select();

        e.Handled = true;
    }

    private void LeaveCursor(object sender, MouseEventArgs e)
    {
        var canvasSymbolsVM = (CanvasSymbolsVM)DataContext;

        var cursorPosition = e.GetPosition(this);
        var scrollOffset = GetScrollOffset(canvasSymbolsVM, cursorPosition);

        if (scrollOffset is null)
        {
            return;
        }

        canvasSymbolsVM.ScalingCanvasSymbolsVM.SubscribeСanvasScalingEvents(scrollOffset, cursorPosition);
    }

    private Action? GetScrollOffset(CanvasSymbolsVM canvasSymbolsVM, Point cursorPosition)
    {
        var sideLeave = canvasSymbolsVM.ScalingCanvasSymbolsVM.GetSideLeave(cursorPosition);

        if (sideLeave == ScalingCanvasSymbolsVM.SideLeave.Right)
        {
            return ScrollToRightOffset;
        }

        if (sideLeave == ScalingCanvasSymbolsVM.SideLeave.Left)
        {
            return ScrollToLeftOffset;
        }

        if (sideLeave == ScalingCanvasSymbolsVM.SideLeave.Bottom)
        {
            return ScrollToBottomOffset;
        }

        if (sideLeave == ScalingCanvasSymbolsVM.SideLeave.Top)
        {
            return ScrollToTopOffset;
        }

        return null;
    }

    private void EnterCursor(object sender, MouseEventArgs e)
    {
        var canvasSymbolsVM = (CanvasSymbolsVM)DataContext;
        canvasSymbolsVM.ScalingCanvasSymbolsVM.UnsubscribeСanvasScalingEvents();
    }

    private void ScrollToRightOffset()
    {
        var scrollOffset = scrollViewer.ContentHorizontalOffset + ScalingCanvasSymbolsVM.OFFSET_LEAVE;
        scrollViewer.ScrollToHorizontalOffset(scrollOffset);
    }

    private void ScrollToLeftOffset()
    {
        var scrollOffset = scrollViewer.ContentHorizontalOffset - ScalingCanvasSymbolsVM.OFFSET_LEAVE;
        scrollViewer.ScrollToHorizontalOffset(scrollOffset);
    }

    private void ScrollToBottomOffset()
    {
        var scrollOffset = scrollViewer.ContentVerticalOffset + ScalingCanvasSymbolsVM.OFFSET_LEAVE;
        scrollViewer.ScrollToVerticalOffset(scrollOffset);
    }

    private void ScrollToTopOffset()
    {
        var scrollOffset = scrollViewer.ContentVerticalOffset + ScalingCanvasSymbolsVM.OFFSET_LEAVE;
        scrollViewer.ScrollToVerticalOffset(scrollOffset);
    }

    private void Canvas_SizeChanged(object sender, SizeChangedEventArgs e)
    {
        var canvasSymbolsVM = (CanvasSymbolsVM)DataContext;
        if (canvasSymbolsVM is not null)
        {
           var t =  SystemParameters.HorizontalScrollBarHeight;

            //var t = scrollViewer.Hor;

            var horizontalOffset = scrollViewer.ActualWidth - scrollViewer.ContentHorizontalOffset;
            var verticalOffset = scrollViewer.ActualHeight - scrollViewer.ContentVerticalOffset;

            canvasSymbolsVM.ScalingCanvasSymbolsVM.SetOffsetScrollViewer(horizontalOffset, verticalOffset);
        }
    }
}