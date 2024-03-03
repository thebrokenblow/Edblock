using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.AbstractionsVM;
using System;
using System.Windows.Threading;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbols : UserControl
{
    public static Canvas? Canvas { get; set; }

    private readonly DispatcherTimer dispatcherTimer;
    public CanvasSymbols()
    {
        InitializeComponent();

        dispatcherTimer = new DispatcherTimer()
        {
            Interval = TimeSpan.FromSeconds(3)
        };
    }

    private void ScrollViewer_MouseLeave(object sender, MouseEventArgs e)
    {
        //var cursorPosition = e.MouseDevice.GetPosition(this);
        //var scrollViewer = (ScrollViewer)sender;
        //var canvasSymbols = (Canvas)scrollViewer.Content;
        //var canvasSymbolsVM = (CanvasSymbolsVM)DataContext;

        //canvasSymbols.Width = canvasSymbols.ActualWidth + 10;
        //scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 10);

    }

    private void SelectBlockSymbol(object sender, MouseButtonEventArgs e)
    {
        var blockSymbolView = (UserControl)sender;
        var blockSymbolVM = (BlockSymbolVM)blockSymbolView.DataContext;
        blockSymbolVM.Select();

        e.Handled = true;
    }

    private void LoadedCanvas(object sender, RoutedEventArgs e)
    {
        Canvas = (Canvas)sender;
    }

    private void Canvas_MouseLeave(object sender, MouseEventArgs e)
    {
        //var cursorPosition = e.MouseDevice.GetPosition(this);
        //var scrollViewer = (ScrollViewer)sender;
        //var canvasSymbols = (Canvas)scrollViewer.Content;
        //var canvasSymbolsVM = (CanvasSymbolsVM)DataContext;

        //canvasSymbols.Width = canvasSymbols.ActualWidth + 10;
        //scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + 10);
    }

}