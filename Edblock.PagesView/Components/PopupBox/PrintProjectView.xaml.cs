using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Printing;
using Edblock.PagesViewModel.Pages;

namespace Edblock.PagesView.Components.PopupBox;

/// <summary>
/// Логика взаимодействия для PrintProjectView.xaml
/// </summary>
public partial class PrintProjectView : UserControl
{
    private readonly PrintDialog printDialog = new();
    public PrintProjectView() =>
           InitializeComponent();

    private void PrintImg(object sender, RoutedEventArgs e)
    {
        //if (CanvasSymbols.Canvas is null)
        //{
        //    return;
        //}

        //var canvasView = CanvasSymbols.Canvas;

        //var editorViewModel = (EditorViewModel)DataContext;
        //var canvasSymbolsVM = editorViewModel.CanvasSymbolsVM;
        //canvasSymbolsVM.RemoveSelectedSymbol();

        //if (printDialog.ShowDialog() == false)
        //{
        //    return;
        //}

        //var actualWidth = (int)canvasView.ActualWidth;
        //var actualHeight = (int)canvasView.ActualHeight;

        //var size = new Size(actualWidth, actualHeight);
        //var rect = new Rect(size);

        //canvasView.Measure(size);
        //canvasView.Arrange(rect);

        //printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

        //var originalBackground = canvasView.Background;

        //canvasView.Background = Brushes.White;

        //printDialog.PrintVisual(canvasView, "Распечатываем элемент Canvas");

        //canvasView.Background = originalBackground;
    }
}
