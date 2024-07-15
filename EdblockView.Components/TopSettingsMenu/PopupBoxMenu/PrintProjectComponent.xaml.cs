using System.Printing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для PrintProjectComponent.xaml
/// </summary>
public partial class PrintProjectComponent : UserControl
{
    public Canvas? CanvasSymbolsComponent { get; set; }

    private readonly PrintDialog printDialog = new();
    private const string printVisualDescription = "Печать проекта";

    public PrintProjectComponent()
    {
        InitializeComponent();
    }

    private void PrintImg(object sender, RoutedEventArgs e)
    {
        if (CanvasSymbolsComponent is null)
        {
            return;
        }

        if (printDialog.ShowDialog() == false)
        {
            return;
        }

        var actualWidth = CanvasSymbolsComponent.ActualWidth;
        var actualHeight = CanvasSymbolsComponent.ActualHeight;

        var size = new Size(actualWidth, actualHeight);
        var rect = new Rect(size);

        CanvasSymbolsComponent.Measure(size);
        CanvasSymbolsComponent.Arrange(rect);

        printDialog.PrintTicket.PageOrientation = PageOrientation.Landscape;

        var originalBackground = CanvasSymbolsComponent.Background;
        CanvasSymbolsComponent.Background = Brushes.White;
        printDialog.PrintVisual(CanvasSymbolsComponent, printVisualDescription);
        CanvasSymbolsComponent.Background = originalBackground;
    }
}