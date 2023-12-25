using System.Windows.Input;
using System.Windows.Controls;
using EdblockViewModel.Symbols.Abstraction;

namespace EdblockView.SymbolsView;

/// <summary>
/// Логика взаимодействия для ConditionSymbol.xaml
/// </summary>
public partial class ConditionSymbolView : UserControl
{
    public ConditionSymbolView()
    {
        InitializeComponent();
    }

    private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
    {
        var canvasSymbol = (Canvas)sender;
        var contextCanvasSymbol = canvasSymbol.DataContext;
        var blockSymbolVM = (BlockSymbolVM)contextCanvasSymbol;
        blockSymbolVM.Select();

        e.Handled = true;
    }
}