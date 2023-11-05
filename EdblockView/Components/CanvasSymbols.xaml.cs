using EdblockViewModel;
using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbols : UserControl
{
    private const int canvasOffset = 25;
    
    public CanvasSymbolsVM CanvasSymbolsVM 
    {
        set
        {
            DataContext = value;
        }
    }

    public BlockSymbol? DraggableSymbol { get; set; }

    public CanvasSymbols()
    {
        InitializeComponent();
    }

    private void ScrollViewer_MouseLeave(object sender, MouseEventArgs e)
    {
        var cursorPosition = e.MouseDevice.GetPosition(this);
        var scrollViewer = (ScrollViewer)sender;
        var canvasSymbols = (Canvas)scrollViewer.Content;

        if (DraggableSymbol == null)
        {
            return;
        }

        if (cursorPosition.X > 0)
        {
            if (scrollViewer.ScrollableWidth - scrollViewer.ViewportWidth - scrollViewer.HorizontalOffset < canvasOffset)
            {
                canvasSymbols.Width = canvasSymbols.ActualWidth + canvasOffset;
            }
            scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + canvasOffset);
        }
        else
        {
            if (scrollViewer.HorizontalOffset >= canvasOffset)
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset - canvasOffset);
            }
        }
    }
}