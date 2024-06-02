using System.IO;
using System.Windows;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using Microsoft.Win32;
using EdblockViewModel.PagesVM;

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

    public ButtonSaveImg() =>
        InitializeComponent();

    private void CreateImg(object sender, RoutedEventArgs e)
    {
        if (CanvasSymbols.Canvas is null)
        {
            return;
        }

        var editorVM = (EditorVM)DataContext;
        var canvasView = CanvasSymbols.Canvas;
        var canvasSymbolsVM = editorVM.CanvasSymbolsVM;

        var saveFileDialog = new SaveFileDialog
        {
            DefaultExt = FileExtension,
            Filter = FileFilter,
            FileName = FileName + FileExtension
        };

        canvasSymbolsVM.ListCanvasSymbolsVM.ClearSelectedBlockSymbols();

        if (saveFileDialog.ShowDialog() == false)
        {
            return;
        }

        var originalBackground = canvasView.Background;
        canvasView.Background = Brushes.White;

        ToImageSource(canvasView, saveFileDialog.FileName);

        canvasView.Background = originalBackground;
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
            var bitmapFrame = BitmapFrame.Create(renderTargetBitmap);

            pngBitmapEncoder.Frames.Add(bitmapFrame);

            using var fileStream = File.Create(fileName);
            pngBitmapEncoder.Save(fileStream);
        }
        catch
        {
            MessageBox.Show("Ошибка при сохранении файла");
        }
    }
}