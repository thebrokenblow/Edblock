using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace Edblock.SymbolsView.Symbols;

/// <summary>
/// Логика взаимодействия для HorizontalConditionSymbolView.xaml
/// </summary>
public partial class HorizontalConditionSymbolView : UserControl
{
    public HorizontalConditionSymbolView() =>
        InitializeComponent();

    private void TrackStageDrawLine(object sender, MouseButtonEventArgs e)
    {
        if (sender is Ellipse ellipse)
        {
            var contextEllipse = ellipse.DataContext;

            if (contextEllipse is ConnectionPointVM connectionPointVM)
            {
                connectionPointVM.DrawLine();
            }
        }

        e.Handled = true;
    }
}