using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace Edblock.SymbolsView.Symbols.Components;

/// <summary>
/// Логика взаимодействия для ConnectionPoints.xaml
/// </summary>
public partial class ConnectionPointsView : UserControl
{
    public ConnectionPointsView() =>
        InitializeComponent();

    private void TrackStageDrawLine(object sender, MouseButtonEventArgs e)
    {
        if (sender is Ellipse ellipse)
        {
            var contextEllipse = ellipse.DataContext;

            if (contextEllipse is ConnectionPointVM connectionPointVM)
            {
                //connectionPointVM.Click();
            }           
        }

        e.Handled = true;
    }
}