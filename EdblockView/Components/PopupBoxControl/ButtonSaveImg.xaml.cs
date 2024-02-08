using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using EdblockViewModel;
using EdblockViewModel.ComponentsVM;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ButtonSaveImg.xaml
/// </summary>
public partial class ButtonSaveImg : UserControl
{
    private const string FileName = "Edblock";
    private const string FileExtension = ".PNG";
    private const string FileFilter = "Image (.PNG)|*.PNG";
    private const double dpiX = 96d;
    private const double dpiY = 96d;

    public ButtonSaveImg()
    {
        InitializeComponent();
    }

    private void CreateImg(object sender, RoutedEventArgs e)
    {
        if (CanvasSymbols.Canvas is null)
        {
            return;
        }

        var edblockVM = (EdblockVM)DataContext;
        var canvasView = CanvasSymbols.Canvas;
        var canvasSymbolsVM = edblockVM.CanvasSymbolsVM;

        var saveFileDialog = new SaveFileDialog
        {
            DefaultExt = FileExtension,
            Filter = FileFilter,
            FileName = FileName + FileExtension
        };


        RemoveSelectedSymbol(canvasSymbolsVM);

        if (saveFileDialog.ShowDialog() == true)
        {
            var originalBackground = canvasView.Background;

            canvasView.Background = Brushes.White;

            ToImageSource(canvasView, saveFileDialog.FileName);

            canvasView.Background = originalBackground;
        }
    }

    private static void RemoveSelectedSymbol(CanvasSymbolsVM canvasSymbolsVM)
    {
        var selectedBlockSymbols = canvasSymbolsVM.SelectedBlockSymbols;

        foreach (var selectedBlockSymbol in selectedBlockSymbols)
        {
            selectedBlockSymbol.IsSelected = false;
        }

        canvasSymbolsVM.SelectedDrawnLineSymbol = null;
    }

    private static void ToImageSource(Canvas canvasView, string fileName)
    {
        try
        {
            var actualWidth = (int)canvasView.ActualWidth;
            var actualHeight = (int)canvasView.ActualHeight;

            var renderTargetBitmap = new RenderTargetBitmap(actualWidth, actualHeight, dpiX, dpiY, PixelFormats.Pbgra32);

            var size = new Size(actualWidth, actualHeight);
            var rect = new Rect(size);

            canvasView.Measure(size);
            canvasView.Arrange(rect);
            renderTargetBitmap.Render(canvasView);

            var pngBitmapEncoder = new PngBitmapEncoder();
            pngBitmapEncoder.Frames.Add(BitmapFrame.Create(renderTargetBitmap));

            using var fileStream = File.Create(fileName);
            pngBitmapEncoder.Save(fileStream);
        }
        catch
        {
            MessageBox.Show("Ошибка при сохранении файла");
        }
    }
}