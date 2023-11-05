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

    private CanvasSymbolsVM? canvasSymbolsVM;
    public CanvasSymbolsVM? CanvasSymbolsVM 
    {
        get => canvasSymbolsVM;
        set
        {
            canvasSymbolsVM = value;
            DataContext = canvasSymbolsVM;
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

        if (CanvasSymbolsVM?.DraggableSymbol == null)
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