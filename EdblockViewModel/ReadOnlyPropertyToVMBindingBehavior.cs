using System.Windows;
using Microsoft.Xaml.Behaviors;

namespace EdblockView;

public class ReadOnlyPropertyToVMBindingBehavior : Behavior<UIElement>
{
    public object ReadOnlyDependencyProperty
    {
        get { return GetValue(ReadOnlyDependencyPropertyProperty); }
        set { SetValue(ReadOnlyDependencyPropertyProperty, value); }
    }

    public static readonly DependencyProperty ReadOnlyDependencyPropertyProperty =
        DependencyProperty.Register("ReadOnlyDependencyProperty", typeof(object), typeof(ReadOnlyPropertyToVMBindingBehavior),
            new PropertyMetadata(null, OnReadOnlyDependencyPropertyPropertyChanged));

    public object ModelProperty
    {
        get { return GetValue(ModelPropertyProperty); }
        set { SetValue(ModelPropertyProperty, value); }
    }

    public static readonly DependencyProperty ModelPropertyProperty =
        DependencyProperty.Register("ModelProperty", typeof(object), typeof(ReadOnlyPropertyToVMBindingBehavior), 
            new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault));

    private static void OnReadOnlyDependencyPropertyPropertyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
    {
        if (obj is ReadOnlyPropertyToVMBindingBehavior b)
        {
            b.ModelProperty = e.NewValue;
        }
    }
}