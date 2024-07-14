using EdblockViewModel.Components.CanvasSymbols;
using EdblockViewModel.Pages;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для SaveImgComponent.xaml
/// </summary>
public partial class SaveImgComponent : UserControl
{
    private const string FileExtension = ".PNG";
    private const string FileFilter = "Image (.PNG)|*.PNG";
    private const double dpiX = 96d;
    private const double dpiY = 96d;

    private readonly EditorVM? editorVM;
    public SaveImgComponent()
    {
        InitializeComponent();
    }

    private void CreateImg(object sender, RoutedEventArgs e)
    {
        //if (CanvasSymbolsComponent.Canvas is null || editorVM is null)
        //{
        //    return;
        //}

        //var canvasView = CanvasSymbolsComponent.Canvas;
        //var canvasSymbolsVM = editorVM.CanvasSymbolsComponentVM;

        //var saveFileDialog = new SaveFileDialog
        //{
        //    DefaultExt = FileExtension,
        //    Filter = FileFilter,
        //    FileName = FileName + FileExtension
        //};

        //canvasSymbolsVM.ListCanvasSymbolsComponentVM.ClearSelectedBlockSymbols();

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
        //try
        //{
        //    var actualWidth = (int)canvasView.ActualWidth;
        //    var actualHeight = (int)canvasView.ActualHeight;

        //    var renderTargetBitmap = new RenderTargetBitmap(actualWidth, actualHeight, dpiX, dpiY, PixelFormats.Pbgra32);

        //    var size = new Size(actualWidth, actualHeight);
        //    var rect = new Rect(size);

        //    canvasView.Measure(size);
        //    canvasView.Arrange(rect);
        //    renderTargetBitmap.Render(canvasView);

        //    var pngBitmapEncoder = new PngBitmapEncoder();
        //    var bitmapFrame = BitmapFrame.Create(renderTargetBitmap);

        //    pngBitmapEncoder.Frames.Add(bitmapFrame);

        //    using var fileStream = File.Create(fileName);
        //    pngBitmapEncoder.Save(fileStream);
        //}
        //catch
        //{
        //    MessageBox.Show("Ошибка при сохранении файла");
        //}
    }
}
