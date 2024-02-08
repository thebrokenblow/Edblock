using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Printing;
using EdblockViewModel;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ButtonPrintProject.xaml
/// </summary>
public partial class ButtonPrintProject : UserControl
{
    private readonly PrintDialog printDialog = new();

    public ButtonPrintProject()
    {
        InitializeComponent();
    }

    private void PrintImg(object sender, RoutedEventArgs e)
    {
        if (CanvasSymbols.Canvas is null)
        {
            return;
        }

        var canvasView = CanvasSymbols.Canvas;

        var edblockVM = (EdblockVM)DataContext;
        var canvasSymbolsVM = edblockVM.CanvasSymbolsVM;
        canvasSymbolsVM.RemoveSelectedSymbol();

        if (printDialog.ShowDialog() == true)
        {
            var actualWidth = (int)canvasView.ActualWidth;
            var actualHeight = (int)canvasView.ActualHeight;

            var size = new Size(actualWidth, actualHeight);
            var rect = new Rect(size);

            canvasView.Measure(size);
            canvasView.Arrange(rect);

            printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

            var originalBackground = canvasView.Background;

            canvasView.Background = Brushes.White;

            printDialog.PrintVisual(canvasView, "Распечатываем элемент Canvas");

            canvasView.Background = originalBackground;
        }
    }
}