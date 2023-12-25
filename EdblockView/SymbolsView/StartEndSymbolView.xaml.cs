using EdblockViewModel;
using EdblockView.Abstraction;
using System.Windows.Controls;
using EdblockViewModel.Symbols;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockView.SymbolsView;

/// <summary>
/// Логика взаимодействия для StartEndSymbolView.xaml
/// </summary>
public partial class StartEndSymbolView : UserControl
{
    public StartEndSymbolView()
    {
        InitializeComponent();
    }

    private void Rectangle_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var canvasSymbol = (Canvas)sender;
        var contextCanvasSymbol = canvasSymbol.DataContext;
        var blockSymbolVM = (BlockSymbolVM)contextCanvasSymbol;
        blockSymbolVM.Select();

        e.Handled = true;
    }
}