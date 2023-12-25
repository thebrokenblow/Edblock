using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockView.SymbolsView;

/// <summary>
/// Логика взаимодействия для ActionSymbol.xaml
/// </summary>
public partial class ActionSymbolView : UserControl
{
    public ActionSymbolView()
    {
        InitializeComponent();
    }

    private void MouseDownOnSymbol(object sender, MouseButtonEventArgs e)
    {
        var canvasSymbol = (Canvas)sender;
        var contextCanvasSymbol = canvasSymbol.DataContext;
        var blockSymbolVM = (BlockSymbolVM)contextCanvasSymbol;
        blockSymbolVM.Select();

        e.Handled = true;
    }
}