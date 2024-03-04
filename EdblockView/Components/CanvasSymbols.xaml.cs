using System.Windows;
using System.Windows.Input;
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
        if (DataContext is CanvasSymbolsVM canvasSymbolsVM)
        {
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
            }
        }
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

    private void Canvas_MouseEnter(object sender, MouseEventArgs e)
    {
        if (DataContext is CanvasSymbolsVM canvasSymbolsVM)
        {
            canvasSymbolsVM.UnsubscribeСanvasScalingEvents();
        }
    }
}