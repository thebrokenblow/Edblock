using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockView.SymbolsView;

/// <summary>
/// Логика взаимодействия для ConnectionPoints.xaml
/// </summary>
public partial class ConnectionPointsView : UserControl
{
    public ConnectionPointsView()
    {
        InitializeComponent();
    }

    private void TrackStageDrawLine(object sender, MouseButtonEventArgs e)
    {
        var ellipse = (Ellipse)sender;
        var contextEllipse = ellipse.DataContext;
        var connectionPoint = (ConnectionPointVM)contextEllipse;
        connectionPoint.TrackStageDrawLine();

        e.Handled = true;
    }
}