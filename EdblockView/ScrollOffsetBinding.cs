using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace EdblockView;

/// <summary>
/// Attached behaviour which exposes the horizontal and vertical offset values
/// for a ScrollViewer, permitting binding.
/// NOTE: This code could be simplified a little by finding common code between vertical / horizontal
/// scrollbars. However, this was not doen for clarity in the associated blog post!
/// </summary>
public static class ScrollOffsetBinding
{
    #region VerticalOffset attached property

    /// <summary>
    /// Gets the vertical offset value
    /// </summary>
    public static double GetVerticalOffset(DependencyObject depObj)
    {
        return (double)depObj.GetValue(VerticalOffsetProperty);
    }

    /// <summary>
    /// Sets the vertical offset value
    /// </summary>
    public static void SetVerticalOffset(DependencyObject depObj, double value)
    {
        depObj.SetValue(VerticalOffsetProperty, value);
    }

    /// <summary>
    /// VerticalOffset attached property
    /// </summary>
    public static readonly DependencyProperty VerticalOffsetProperty =
        DependencyProperty.RegisterAttached("VerticalOffset", typeof(double),
        typeof(ScrollOffsetBinding), new PropertyMetadata(0.0, OnVerticalOffsetPropertyChanged));

    #endregion

    #region HorizontalOffset attached property

    /// <summary>
    /// Gets the horizontal offset value
    /// </summary>
    public static double GetHorizontalOffset(DependencyObject depObj)
    {
        return (double)depObj.GetValue(HorizontalOffsetProperty);
    }

    /// <summary>
    /// Sets the horizontal offset value
    /// </summary>
    public static void SetHorizontalOffset(DependencyObject depObj, double value)
    {
        depObj.SetValue(HorizontalOffsetProperty, value);
    }

    /// <summary>
    /// HorizontalOffset attached property
    /// </summary>
    public static readonly DependencyProperty HorizontalOffsetProperty =
        DependencyProperty.RegisterAttached("HorizontalOffset", typeof(double),
        typeof(ScrollOffsetBinding), new PropertyMetadata(0.0, OnHorizontalOffsetPropertyChanged));

    #endregion

    #region VerticalScrollBar attached property

    /// <summary>
    /// An attached property which holds a reference to the vertical scrollbar which
    /// is extracted from the visual tree of a ScrollViewer
    /// </summary>
    private static readonly DependencyProperty VerticalScrollBarProperty =
        DependencyProperty.RegisterAttached("VerticalScrollBar", typeof(ScrollBar),
        typeof(ScrollOffsetBinding), new PropertyMetadata(null));


    /// <summary>
    /// An attached property which holds a reference to the horizontal scrollbar which
    /// is extracted from the visual tree of a ScrollViewer
    /// </summary>
    private static readonly DependencyProperty HorizontalScrollBarProperty =
        DependencyProperty.RegisterAttached("HorizontalScrollBar", typeof(ScrollBar),
        typeof(ScrollOffsetBinding), new PropertyMetadata(null));

    #endregion


    /// <summary>
    /// Invoked when the VerticalOffset attached property changes
    /// </summary>
    private static void OnVerticalOffsetPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
    {
        if (dependencyObject is ScrollViewer scrollViewer)
        {
            // check whether we have a reference to the vertical scrollbar
            if (scrollViewer.GetValue(VerticalScrollBarProperty) == null)
            {
                // if not, handle LayoutUpdated, which will be invoked after the
                // template is applied and extract the scrollbar
                scrollViewer.LayoutUpdated += (s, ev) =>
                {
                    if (scrollViewer.GetValue(VerticalScrollBarProperty) == null)
                    {
                        GetScrollBarsForScrollViewer(scrollViewer);
                    }
                };
            }
            else
            {
                // update the scrollviewer offset
                scrollViewer.ScrollToVerticalOffset((double)eventArgs.NewValue);
            }
        }
    }

    /// <summary>
    /// Invoked when the HorizontalOffset attached property changes
    /// </summary>
    private static void OnHorizontalOffsetPropertyChanged(DependencyObject dependencyObject, DependencyPropertyChangedEventArgs eventArgs)
    {
        if (dependencyObject is ScrollViewer scrollViewer)
        {
            // check whether we have a reference to the vertical scrollbar
            if (scrollViewer.GetValue(HorizontalScrollBarProperty) == null)
            {
                // if not, handle LayoutUpdated, which will be invoked after the
                // template is applied and extract the scrollbar
                scrollViewer.LayoutUpdated += (s, ev) =>
                {
                    if (scrollViewer.GetValue(HorizontalScrollBarProperty) == null)
                    {
                        GetScrollBarsForScrollViewer(scrollViewer);
                    }
                };
            }
            else
            {
                // update the scrollviewer offset
                scrollViewer.ScrollToHorizontalOffset((double)eventArgs.NewValue);
            }
        }
    }

    /// <summary>
    /// Attempts to extract the scrollbars that are within the scrollviewers
    /// visual tree. When extracted, event handlers are added to their ValueChanged events.
    /// </summary>
    private static void GetScrollBarsForScrollViewer(ScrollViewer scrollViewer)
    {
        var scroll = GetScrollBar(scrollViewer, Orientation.Vertical);
        if (scroll != null)
        {
            // save a reference to this scrollbar on the attached property
            scrollViewer.SetValue(VerticalScrollBarProperty, scroll);

            // scroll the scrollviewer
            scrollViewer.ScrollToVerticalOffset(GetVerticalOffset(scrollViewer));

            // handle the changed event to update the exposed VerticalOffset
            scroll.ValueChanged += (s, e) =>
            {
                SetVerticalOffset(scrollViewer, e.NewValue);
            };
        }

        scroll = GetScrollBar(scrollViewer, Orientation.Horizontal);
        if (scroll != null)
        {
            // save a reference to this scrollbar on the attached property
            scrollViewer.SetValue(HorizontalScrollBarProperty, scroll);

            // scroll the scrollviewer
            scrollViewer.ScrollToHorizontalOffset(ScrollOffsetBinding.GetHorizontalOffset(scrollViewer));

            // handle the changed event to update the exposed HorizontalOffset
            scroll.ValueChanged += (s, e) =>
            {
                scrollViewer.SetValue(HorizontalOffsetProperty, e.NewValue);
            };
        }
    }

    /// <summary>
    /// Searches the descendants of the given element, looking for a scrollbar
    /// with the given orientation.
    /// </summary>
    private static ScrollBar GetScrollBar(FrameworkElement frameworkElement, Orientation orientation)
    {
        return frameworkElement.Descendants().OfType<ScrollBar>().First(scrollBar => scrollBar.Orientation == orientation);
    }
}