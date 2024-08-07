﻿using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Windows.Media;

using System.IO;
using Microsoft.Win32;

namespace EdblockView.Components.TopSettingsMenu.PopupBoxMenu;

/// <summary>
/// Логика взаимодействия для SaveImgComponent.xaml
/// </summary>
public partial class SaveImgComponent : UserControl
{
    private const string fileName = "Edblock";
    private const string FileExtension = ".PNG";
    private const string FileFilter = "Image (.PNG)|*.PNG";
    private const double dpiX = 96d;
    private const double dpiY = 96d;
    public Canvas? CanvasSymbolsComponent { get; set; }
    public SaveImgComponent()
    {
        InitializeComponent();
    }

    private void CreateImg(object sender, RoutedEventArgs e)
    {
        if (CanvasSymbolsComponent is null)
        {
            return;
        }

        var saveFileDialog = new SaveFileDialog
        {
            DefaultExt = FileExtension,
            Filter = FileFilter,
            FileName = fileName + FileExtension
        };

        if (saveFileDialog.ShowDialog() == false)
        {
            return;
        }

        var originalBackground = CanvasSymbolsComponent.Background;
        CanvasSymbolsComponent.Background = Brushes.White;

        ToImageSource(CanvasSymbolsComponent, saveFileDialog.FileName);

        CanvasSymbolsComponent.Background = originalBackground;
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
            MessageBox.Show("Ошибка при сохранении фотографии");
        }
    }
}
