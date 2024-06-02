using System.Windows.Input;
using System.Windows.Shapes;
using System.Windows.Controls;
using EdblockViewModel.Symbols.ComponentsSymbolsVM.ConnectionPoints;

namespace EdblockView.SymbolsView;

/// <summary>
/// Логика взаимодействия для VerticalConditionSymbolView.xaml
/// </summary>
public partial class VerticalConditionSymbolView : UserControl
{
    public VerticalConditionSymbolView() =>
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