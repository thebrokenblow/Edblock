using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.Symbols.LineSymbols;

namespace EdblockView.Symbols;

/// <summary>
/// Логика взаимодействия для LineSymbol.xaml
/// </summary>
public partial class LineSymbol : UserControl
{
    public LineSymbol()
    {
        InitializeComponent();
    }

    private void SelectLine(object sender, MouseButtonEventArgs e)
    {
        var itemsControlLines = (ItemsControl)sender;
        var drawnLineSymbolVM = (DrawnLineSymbolVM)itemsControlLines.DataContext;

        drawnLineSymbolVM.SelectLine();

        e.Handled = true;
    }
}