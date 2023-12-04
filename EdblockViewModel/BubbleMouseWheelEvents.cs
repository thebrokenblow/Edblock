using Microsoft.Xaml.Behaviors;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows;
using System.Windows.Controls;

namespace EdblockViewModel;

public class BubbleMouseWheelEvents : Behavior<UIElement>
{
    protected override void OnAttached()
    {
        base.OnAttached();
        this.AssociatedObject.PreviewMouseWheel += PreviewMouseWheel;
    }

    protected override void OnDetaching()
    {
        this.AssociatedObject.PreviewMouseWheel -= PreviewMouseWheel;
        base.OnDetaching();
    }

    private void PreviewMouseWheel(object sender, MouseWheelEventArgs e)
    {
        e.Handled = true;
    }
}
