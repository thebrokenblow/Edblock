using EdblockViewModel;
using System.Windows.Input;
using System.Windows.Controls;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbols : UserControl
{
    public CanvasSymbolsVM CanvasSymbolsVM 
    {
        set
        {
            DataContext = value;
        }
    }

    public CanvasSymbols()
    {
        InitializeComponent();
    }

    private void ScrollViewer_MouseLeave(object sender, MouseEventArgs e)
    {
        var cursorPosition = e.MouseDevice.GetPosition(this);
        var scrollViewer = (ScrollViewer)sender;
        var canvasSymbols = (Canvas)scrollViewer.Content;

        var x = cursorPosition.X - (cursorPosition.X % 100);
        var x1 = scrollViewer.ActualWidth - (scrollViewer.ActualWidth % 100);

        if (x == x1)
        {
            var pos = scrollViewer.HorizontalOffset;
            if (scrollViewer.ScrollableWidth - scrollViewer.ViewportWidth - scrollViewer.HorizontalOffset <25)
            {
                canvasSymbols.Width = canvasSymbols.ActualWidth + 25;
            }
            
            
            scrollViewer.ScrollToHorizontalOffset(pos+25);
            
        }
        else if (x <= 0)
        {
          //  canvasSymbols.Width = canvasSymbols.ActualWidth + 25;
          if (scrollViewer.HorizontalOffset>=25)
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset-25);
        }
    }
}