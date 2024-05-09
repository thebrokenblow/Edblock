using System.Windows.Controls;
using Edblock.PagesViewModel.ComponentsViewModel;

namespace EdblockView.Components.PopupBoxControl;

/// <summary>
/// Логика взаимодействия для ScaleAllSymbol.xaml
/// </summary>
public partial class ScaleAllSymbol : UserControl
{
    public ScaleAllSymbolVM ScaleAllSymbolVM
    {
        set
        {
            DataContext = value;
        }
    }

    public ScaleAllSymbol() =>
        InitializeComponent();
}