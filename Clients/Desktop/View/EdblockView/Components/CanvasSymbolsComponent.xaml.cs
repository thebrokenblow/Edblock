using System.Windows;
using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.Symbols.Abstractions;
using EdblockViewModel.Components.CanvasSymbols;
using EdblockViewModel.Symbols.LinesSymbolVM;
using Edblock.SymbolsView.Symbols.Lines;

namespace EdblockView.Components;

/// <summary>
/// Логика взаимодействия для CanvasSymbols.xaml
/// </summary>
public partial class CanvasSymbolsComponent : UserControl
{
    public static Canvas? Canvas { get; set; }

    private CanvasSymbolsComponentVM? canvasSymbolsComponentVM;

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
        var cursorPosition = e.GetPosition(this);
        canvasSymbolsComponentVM ??= (CanvasSymbolsComponentVM)DataContext;
        canvasSymbolsComponentVM.ScalingCanvasSymbolsVM.SubscribeСanvasScalingEvents(cursorPosition);
    }

    private void EnterCursor(object sender, MouseEventArgs e)
    {
        canvasSymbolsComponentVM ??= (CanvasSymbolsComponentVM)DataContext;
        canvasSymbolsComponentVM.ScalingCanvasSymbolsVM.UnsubscribeСanvasScalingEvents();
    }

    private void DrawnLineSymbolView_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is DrawnLineSymbolView drawnLineSymbolView)
        {
            if (drawnLineSymbolView.DataContext is DrawnLineSymbolVM drawnLineSymbolVM)
            {
                drawnLineSymbolVM.Select();
            }
        }

        e.Handled = true;
    }
}