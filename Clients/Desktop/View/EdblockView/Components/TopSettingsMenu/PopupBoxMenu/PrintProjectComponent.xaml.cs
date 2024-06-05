using System.Printing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using EdblockViewModel.Pages;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для ButtonPrintProjectComponent.xaml
/// </summary>
public partial class PrintProjectComponent : UserControl
{
    private readonly PrintDialog printDialog = new();
    private const string printVisualDescription = "Распечатываем элемент Canvas";
    public PrintProjectComponent() =>
         InitializeComponent();

    private void PrintImg(object sender, RoutedEventArgs e)
    {
        if (CanvasSymbolsComponent.Canvas is null)
        {
            return;
        }

        var canvasView = CanvasSymbolsComponent.Canvas;

        var editorVM = (EditorVM)DataContext;
        var canvasSymbolsVM = editorVM.CanvasSymbolsComponentVM;
        canvasSymbolsVM.ListCanvasSymbolsComponentVM.ClearSelectedBlockSymbols();

        if (printDialog.ShowDialog() == false)
        {
            return;
        }

        var actualWidth = (int)canvasView.ActualWidth;
        var actualHeight = (int)canvasView.ActualHeight;

        var size = new Size(actualWidth, actualHeight);
        var rect = new Rect(size);

        canvasView.Measure(size);
        canvasView.Arrange(rect);

        printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

        var originalBackground = canvasView.Background;

        canvasView.Background = Brushes.White;

        printDialog.PrintVisual(canvasView, printVisualDescription);

        canvasView.Background = originalBackground;
    }
}