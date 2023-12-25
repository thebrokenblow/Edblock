using System.Windows.Input;
using System.Windows.Controls;
using System.Windows;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbols : UserControl
{
    public CanvasSymbols()
    {
        InitializeComponent();
    }

    private void ScrollViewer_MouseLeave(object sender, MouseEventArgs e)
    {
        //var cursorPosition = e.MouseDevice.GetPosition(this);
        //var scrollViewer = (ScrollViewer)sender;
        //var canvasSymbols = (Canvas)scrollViewer.Content;

        //if (CanvasSymbolsVM?.MovableBlockSymbol == null)
        //{
        //    return;
        //}

        //if (cursorPosition.X - cursorPosition.X % 10 == scrollViewer.ActualWidth - scrollViewer.ActualWidth % 10)
        //{
        //    if (scrollViewer.ScrollableWidth - scrollViewer.ViewportWidth - scrollViewer.HorizontalOffset < canvasOffset)
        //    {
        //        canvasSymbols.Width = canvasSymbols.ActualWidth + canvasOffset;
        //    }
        //    scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + canvasOffset);
        //}
        //else if (cursorPosition.X <= 0)
        //{
        //    if (scrollViewer.HorizontalOffset >= canvasOffset)
        //    {
        //        scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - canvasOffset);
        //    }
        //}
        //if (cursorPosition.Y - cursorPosition.Y % 10 == scrollViewer.ActualHeight - scrollViewer.ActualHeight % 10)
        //{
        //    if (scrollViewer.ScrollableHeight - scrollViewer.ViewportHeight - scrollViewer.VerticalOffset < canvasOffset)
        //    {
        //        canvasSymbols.Height = canvasSymbols.ActualHeight + canvasOffset;
        //    }
        //    scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset + canvasOffset);
        //}
    }

    private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
    {
        MessageBox.Show("ПРИВЕТ");
    }
}