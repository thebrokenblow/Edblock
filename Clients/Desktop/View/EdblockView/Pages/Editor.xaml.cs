using System.Windows;
using System.Windows.Controls;
using EdblockView.Components;
using EdblockViewModel.Pages;

namespace EdblockView.Pages;

/// <summary>
/// Логика взаимодействия для Editor.xaml
/// </summary>
public partial class Editor : UserControl
{
    private EditorVM? edblockVM;
    public Editor()
    {
        InitializeComponent();
    }

    private void ChangedSizeWindow(object sender, SizeChangedEventArgs e)
    {
        var edblock = (Editor)sender;

        var widthWindow = (int)edblock.ActualWidth;
        var heightWindow = (int)edblock.ActualHeight;

        edblockVM ??= (EditorVM)DataContext;

        edblockVM?.CanvasSymbolsComponentVM.ScalingCanvasSymbolsVM.CalculateSizeUnscaledCanvas(
            widthWindow,
            heightWindow);
    }

    private void CanvasSymbolsComponent_Loaded(object sender, RoutedEventArgs e)
    {
        var canvasSymbolsComponent = (CanvasSymbolsComponent)sender;
        topSettingsMenuComponent.CanvasSymbolsComponent = canvasSymbolsComponent.canvasSymbolsComponent;
    }
}