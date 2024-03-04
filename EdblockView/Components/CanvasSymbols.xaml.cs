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

        if (canvasSymbolsVM.MovableBlockSymbol is not null || canvasSymbolsVM.CurrentDrawnLineSymbol is not null)
        {
            var cursorPosition = e.GetPosition(this);

            var sideLeave = canvasSymbolsVM.GetSideLeave(cursorPosition);

            if (sideLeave == CanvasSymbolsVM.SideLeave.Right)
            {
                canvasSymbolsVM.SubscribeСanvasScalingEvents(ScrollToRightOffset, cursorPosition);
            }
            else if (sideLeave == CanvasSymbolsVM.SideLeave.Left)
            {
                canvasSymbolsVM.SubscribeСanvasScalingEvents(ScrollToLeftOffset, cursorPosition);
            }
            else if (sideLeave == CanvasSymbolsVM.SideLeave.Top)
            {
                canvasSymbolsVM.SubscribeСanvasScalingEvents(ScrollToTopOffset, cursorPosition);
            }
            else if (sideLeave == CanvasSymbolsVM.SideLeave.Bottom)
            {
                canvasSymbolsVM.SubscribeСanvasScalingEvents(ScrollToBottomOffset, cursorPosition);
            }
        }
    }

    private void EnterCursor(object sender, MouseEventArgs e)
    {
        var canvasSymbolsVM = (CanvasSymbolsVM)DataContext;
        canvasSymbolsVM.UnsubscribeСanvasScalingEvents();
    }

    private void ScrollToRightOffset()
    {
        var scrollOffset = scrollViewer.ContentHorizontalOffset + CanvasSymbolsVM.OFFSET_LEAVE;
        scrollViewer.ScrollToHorizontalOffset(scrollOffset);
    }

    private void ScrollToLeftOffset()
    {
        var scrollOffset = scrollViewer.ContentHorizontalOffset - CanvasSymbolsVM.OFFSET_LEAVE;
        scrollViewer.ScrollToHorizontalOffset(scrollOffset);
    }

    private void ScrollToTopOffset()
    {
        var scrollOffset = scrollViewer.ContentVerticalOffset - CanvasSymbolsVM.OFFSET_LEAVE;
        scrollViewer.ScrollToVerticalOffset(scrollOffset);
    }

    private void ScrollToBottomOffset()
    {
        var scrollOffset = scrollViewer.ContentVerticalOffset + CanvasSymbolsVM.OFFSET_LEAVE;
        scrollViewer.ScrollToVerticalOffset(scrollOffset);
    }
}