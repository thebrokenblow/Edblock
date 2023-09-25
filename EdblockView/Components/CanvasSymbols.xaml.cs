using System.Windows.Input;
using System.Windows.Controls;

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
        //var pos = e.MouseDevice.GetPosition(this);

        //int current = constX;
        //if (pos.X > 500)
        //{
        //    var scrollViewer = (ScrollViewer)sender;
        //    var t = (Canvas)scrollViewer.Content;
        //    while (!t.IsMouseOver && current != 0)
        //    {
        //        t.Width = t.ActualWidth + 50;
        //        x += 50;
        //        current -= 50;
        //        scrollViewer.ScrollToHorizontalOffset(x);
        //    }
        //} else
        //{
        //    var scrollViewer = (ScrollViewer)sender;
        //    var t = (Canvas)scrollViewer.Content;
        //    t.Width = t.ActualWidth - 50;
        //    x -= 50;
        //    scrollViewer.ScrollToHorizontalOffset(x);
        //}
    }
}