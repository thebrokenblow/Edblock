using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.AbstractionsVM;
using Edblock.PagesViewModel.ComponentsViewModel;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbols : UserControl
{
    public static Canvas? Canvas { get; set; }

    private CanvasSymbolsVM? canvasSymbolsVM;

    public CanvasSymbols() =>
        InitializeComponent();

    private void LoadedCanvas(object sender, RoutedEventArgs e) =>
         Canvas = (Canvas)sender;

    private void SelectBlockSymbol(object sender, MouseButtonEventArgs e)
    {
        var blockSymbolView = (UserControl)sender;
        var blockSymbolVM = (BlockSymbolVM)blockSymbolView.DataContext;
        blockSymbolVM.Select();

        e.Handled = true;
    }

    private void LeaveCursor(object sender, MouseEventArgs e)
    {
        canvasSymbolsVM ??= (CanvasSymbolsVM)DataContext;

        var cursorPosition = e.GetPosition(this);
        canvasSymbolsVM.ScalingCanvasSymbolsVM.SubscribeСanvasScalingEvents(cursorPosition);
    }

    private void EnterCursor(object sender, MouseEventArgs e)
    {
        canvasSymbolsVM ??= (CanvasSymbolsVM)DataContext;

        canvasSymbolsVM.ScalingCanvasSymbolsVM.UnsubscribeСanvasScalingEvents();
    }
}