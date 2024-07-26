using EdblockViewModel.Symbols.LinesSymbolVM;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Edblock.SymbolsView.Symbols.Lines;

/// <summary>
/// Логика взаимодействия для LineSymbolView.xaml
/// </summary>
public partial class LineSymbolView : UserControl
{
    public LineSymbolView() =>
        InitializeComponent();

    private void FinishDrawingLine(object sender, MouseButtonEventArgs e)
    {
        if (sender is Line lineView)
        {
            if (lineView.DataContext is LineSymbolVM lineSymbolVM && lineSymbolVM.DrawnLineSymbolVM.CanvasSymbolsComponentVM.CurrentDrawnLineSymbolVM is not null)
            {
                lineSymbolVM.FinishDrawingLine();
                e.Handled = true;
            }
        }
    }
}
