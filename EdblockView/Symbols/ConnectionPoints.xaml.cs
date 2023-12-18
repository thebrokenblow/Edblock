using System.Windows.Shapes;
using System.Windows.Controls;
using EdblockViewModel.Symbols.ConnectionPoints;

namespace EdblockView.Symbols;

/// <summary>
/// Логика взаимодействия для ConnectionPoints.xaml
/// </summary>
public partial class ConnectionPoints : UserControl
{
    public ConnectionPoints()
    {
        InitializeComponent();
    }

    private void MouseDownConnectionPoint(object sender, System.Windows.Input.MouseButtonEventArgs e)
    {
        var ellipse = (Ellipse)sender;
        var contextEllipse = ellipse.DataContext;
        var connectionPoint = (ConnectionPoint)contextEllipse;
        connectionPoint.TrackStageDrawLine();

        e.Handled = true;
    }
}