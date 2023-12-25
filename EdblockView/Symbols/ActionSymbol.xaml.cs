using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockView.Symbols;

/// <summary>
/// Логика взаимодействия для ActionSymbol.xaml
/// </summary>
public partial class ActionSymbol : UserControl
{
    public ActionSymbol()
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