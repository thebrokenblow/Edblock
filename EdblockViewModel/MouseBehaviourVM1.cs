using System.Windows;
using System.Windows.Input;
using Microsoft.Xaml.Behaviors;

namespace EdblockViewModel;

public class MouseBehaviourVM1 : Behavior<FrameworkElement>
{
    public static readonly DependencyProperty MouseY1Property = DependencyProperty.Register(
        "MouseY1", typeof(double), typeof(MouseBehaviourVM1), new PropertyMetadata(default(double)));

    public double MouseY1
    {
        get { return (double)GetValue(MouseY1Property); }
        set { SetValue(MouseY1Property, value); }
    }

    public static readonly DependencyProperty MouseX1Property = DependencyProperty.Register(
        "MouseX1", typeof(double), typeof(MouseBehaviourVM1), new PropertyMetadata(default(double)));

    public double MouseX1
    {
        get { return (double)GetValue(MouseX1Property); }
        set { SetValue(MouseX1Property, value); }
    }

    protected override void OnAttached()
    {
        AssociatedObject.PreviewMouseLeftButtonDown += AssociatedObjectOnMouseMove;
    }

    private void AssociatedObjectOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
    {
        var pos = mouseEventArgs.GetPosition(AssociatedObject);
        MouseX1 = pos.X;
        MouseY1 = pos.Y;
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PreviewMouseLeftButtonDown -= AssociatedObjectOnMouseMove;
    }
}