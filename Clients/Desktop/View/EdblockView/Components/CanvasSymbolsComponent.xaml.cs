using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.Components.CanvasSymbols;
using EdblockViewModel.Symbols.Abstractions;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbolsComponent : UserControl
{
    public static Canvas? Canvas { get; set; }

    private CanvasSymbolsComponentVM? canvasSymbolsVM;

    public CanvasSymbolsComponent() =>
        InitializeComponent();

    private void LoadedCanvas(object sender, RoutedEventArgs e) =>
         Canvas = (Canvas)sender;

    private void SelectBlockSymbol(object sender, MouseButtonEventArgs e)
    {
        var blockSymbolView = (UserControl)sender;
        var blockSymbolVM = (BlockSymbolVM)blockSymbolView.DataContext;
        blockSymbolVM.SetSelectedProperties();

        e.Handled = true;
    }

    private void LeaveCursor(object sender, MouseEventArgs e)
    {
        canvasSymbolsVM ??= (CanvasSymbolsComponentVM)DataContext;

        var cursorPosition = e.GetPosition(this);
        canvasSymbolsVM.ScalingCanvasSymbolsVM.SubscribeСanvasScalingEvents(cursorPosition);
    }

    private void EnterCursor(object sender, MouseEventArgs e)
    {
        canvasSymbolsVM ??= (CanvasSymbolsComponentVM)DataContext;

        canvasSymbolsVM.ScalingCanvasSymbolsVM.UnsubscribeСanvasScalingEvents();
    }
}