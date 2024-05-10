using Edblock.PagesViewModel.Pages;
using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.IO;

namespace Edblock.PagesView.Components.PopupBox;

/// <summary>
/// Логика взаимодействия для SaveImg.xaml
/// </summary>
public partial class SaveImgView : UserControl
{
    private const string FileName = "Edblock";
    private const string FileExtension = ".PNG";
    private const string FileFilter = "Image (.PNG)|*.PNG";
    private const double dpiX = 96d;
    private const double dpiY = 96d;

    public SaveImgView() =>
        InitializeComponent();

    private void CreateImg(object sender, RoutedEventArgs e)
    {
        //if (CanvasSymbols.Canvas is null)
        //{
        //    return;
        //}

        //var editorViewModel = (EditorViewModel)DataContext;
        //var canvasView = CanvasSymbols.Canvas;
        //var canvasSymbolsVM = editorViewModel.CanvasSymbolsVM;

        //var saveFileDialog = new SaveFileDialog
        //{
        //    DefaultExt = FileExtension,
        //    Filter = FileFilter,
        //    FileName = FileName + FileExtension
        //};

        //canvasSymbolsVM.RemoveSelectedSymbol();

        //if (saveFileDialog.ShowDialog() == false)
        //{
        //    return;
        //}

        //var originalBackground = canvasView.Background;
        //canvasView.Background = Brushes.White;

        //ToImageSource(canvasView, saveFileDialog.FileName);

        //canvasView.Background = originalBackground;
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
