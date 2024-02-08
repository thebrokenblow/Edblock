using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using Microsoft.Win32;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ButtonSaveImg.xaml
/// </summary>
public partial class ButtonSaveImg : UserControl
{
    public ButtonSaveImg()
    {
        InitializeComponent();
    }

    private void CreateImg(object sender, RoutedEventArgs e)
    {
        var saveimg = new SaveFileDialog
        {
            DefaultExt = ".PNG",
            Filter = "Image (.PNG)|*.PNG"
        };

        if (saveimg.ShowDialog() == true)
        {
            if (CanvasSymbols.Canvas is not null)
            {
                ToImageSource(CanvasSymbols.Canvas
                    , saveimg.FileName);
            }
        }
    }

    public static void ToImageSource(Canvas canvas, string filename)
    {
        RenderTargetBitmap bmp = new RenderTargetBitmap((int)canvas.ActualWidth, (int)canvas.ActualHeight, 96d, 96d, PixelFormats.Pbgra32);
        canvas.Measure(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight));
        canvas.Arrange(new Rect(new Size((int)canvas.ActualWidth, (int)canvas.ActualHeight)));
        bmp.Render(canvas);
        PngBitmapEncoder encoder = new PngBitmapEncoder();
        encoder.Frames.Add(BitmapFrame.Create(bmp));
        using (FileStream file = File.Create(filename))
        {
            encoder.Save(file);
        }
    }
}