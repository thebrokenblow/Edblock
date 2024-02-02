using EdblockViewModel.Symbols.LineSymbols;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace EdblockView.SymbolsView.LineSymbolView;

/// <summary>
/// Логика взаимодействия для DrawnLineSymbolView.xaml
/// </summary>
public partial class DrawnLineSymbolView : UserControl
{
    public DrawnLineSymbolView()
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

    private void ShowTextField(object sender, MouseButtonEventArgs e)
    {
        var itemsControlLines = (ItemsControl)sender;
        var drawnLineSymbolVM = (DrawnLineSymbolVM)itemsControlLines.DataContext;

        drawnLineSymbolVM.ShowTextField();

        e.Handled = true;
    }

    private void Line_MouseDown(object sender, MouseButtonEventArgs e)
    {
        if (sender is Line lineView)
        {
            if (lineView.DataContext is LineSymbolVM lineSymbolVM)
            {
                lineSymbolVM.FinishDrawingLine();
            }
        }
    }
}